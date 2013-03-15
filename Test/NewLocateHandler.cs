using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vOrient.Locate.Protocol;
using System.Collections.Concurrent;
using vOrient.Business.Devices;
using vOrient.Common.Tools;
using vOrient.Locate.Core;
using vOrient.Business;
using vOrient.Business.GIS;
using System.Timers;

namespace vOrient.Locate.Service
{
    // 定位计算逻辑部分
    public class NewLocateHandler : IProtocolModuleCallback
    {
        #region Singleton
        private NewLocateHandler()
        {
            //加载路径信息和关联基站
            LoadPathRelation();
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Interval = 500;
            timer.Start();
        }
        //路径信息和基站关联，因为是配置性的数据所以不需要做线程安全处理。
        static Dictionary<int, List<Route>> Map = new Dictionary<int, List<Route>>();
        //加载路径信息和关联基站
        void LoadPathRelation()
        {
            LocateManager.ReloadData();
            // 获取所有路径
            LocatePath[] Path = BusinessManager.GetLocatePaths();
            // 遍历路径和基站进行关联
            foreach (LocatePath p in Path)
            {
                Dev_LocateStations stationA = LocateManager.GetLocateStationByID(p.StationFrom);
                Dev_LocateStations stationB = LocateManager.GetLocateStationByID(p.StationTo);
                // 路径对象
                Route route = new Route();
                route.RouteID = p.RouteID;
                route.X1 = stationA.GetLocation().XLocation;
                route.Y1 = stationA.GetLocation().YLocation;
                route.X2 = stationB.GetLocation().XLocation;
                route.Y2 = stationB.GetLocation().YLocation;
                if (!Map.ContainsKey(stationA.ID))
                {
                    Map.Add(stationA.ID, new List<Route>());
                }
                if (Map[stationA.ID].Find(v => v.RouteID == p.RouteID) == null)
                {
                    Map[stationA.ID].Add(route);
                }
                if (!Map.ContainsKey(stationB.ID))
                {
                    Map.Add(stationB.ID, new List<Route>());
                }
                if (Map[stationB.ID].Find(v => v.RouteID == p.RouteID) == null)
                {
                    Map[stationB.ID].Add(route);
                }
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CalculateLoop();
        }
        private static NewLocateHandler instance = new NewLocateHandler();
        public static NewLocateHandler Instance
        {
            get { return instance; }
        }
        #endregion
        public void OnAPReport(string apIP, byte apFlag)
        {
            //todo
        }



        const double c_errorRange = 3.00;
        // 超时时间秒数 11秒以内的数据都可以参与计算
        const int expiration = 11;
        // 原始数据缓存
        public static ConcurrentDictionary<string, OriginData> OriginDataCache = new ConcurrentDictionary<string, OriginData>();
        // 计算结果缓存
        public static ConcurrentDictionary<int, ResultData> ResultDataCache = new ConcurrentDictionary<int, ResultData>();
        // 定位卡集合
        static ConcurrentBag<int> deviceIDs = new ConcurrentBag<int>();
        // 基站上报数据
        public void OnLocateStationData(string stationMac, uint serialNo, byte stationFlag, LocateDeviceData[] locateDevices)
        {
            // 遍历上报数据并转换成原始数据缓存
            foreach (LocateDeviceData deviceData in locateDevices)
            {
                // 定位基站
                Dev_LocateStations station = LocateManager.GetLocateStationByMac(stationMac);
                if (station == null)
                    continue;
                // 定位卡
                Dev_LocateDevices device = LocateManager.GetLocateDeviceByMac(deviceData.Mac);
                if (device == null)
                    continue;
                // 未与人绑定的定位卡直接过滤掉
                vOrient.Business.Enterprise.Ent_Persons person =
                    BusinessManager.GetAttachedPersonByLocateDevice(device.DeviceID);
                if (person == null)
                    continue;
                // 添加到卡集合
                if (!deviceIDs.Contains(device.DeviceID))
                    deviceIDs.Add(device.DeviceID);

                double d1 = (double)deviceData.Distance1 / 100.00;
                double d2 = (double)deviceData.Distance2 / 100.00;
                // 双路测量超出范围
                if (Math.Abs(d1 - d2) > c_errorRange)
                    continue;
                // 平均值作为距离
                double distance = (d1 + d2) / 2;
                // 调整误差 OffsetValue默认值是1.8 
                //if (distance < station.OffsetValue * 2)
                //    distance /= 2;
                //else
                //    distance -= station.OffsetValue;
                // 原始数据对象
                OriginData data = new OriginData();
                data.OriginDataID = IdFactory.NewGuid();
                data.DataTime = TimeHandler.ConvertIntToDatetime((int)deviceData.TimeStamp); //时间戳
                data.StationMac = stationMac;//基站ID
                data.StationID = station.ID;
                data.StationX = station.GetLocation().XLocation;//基站X坐标
                data.StationY = station.GetLocation().YLocation;//基站Y坐标
                data.LayerID = station.GetLocation().PlaneID;
                data.LocateDeviceID = device.DeviceID;//定位卡ID
                data.Distance = distance;//距离


                // 缓存
                OriginDataCache.TryAdd(data.OriginDataID, data);
            }
        }
        // 清理超时数据,原始数据和计算数据
        void RefreshData()
        {
            // 原始数据的过期时间都设定为11秒
            TimeSpan span = new TimeSpan(0, 0, 8);
            // 计算数据的过期时间都设定为15秒
            TimeSpan span2 = new TimeSpan(0, 0, 10);

            List<string> keys = new List<string>();
            foreach (var key in OriginDataCache.Keys)
            {
                if (CheckExpiration(DateTime.Now, OriginDataCache[key].DataTime, span))
                {
                    keys.Add(key);
                }
            }
            foreach (var key in keys)
            {
                OriginData data;
                OriginDataCache.TryRemove(key, out data);
            }
            List<int> rkeys = new List<int>();
            foreach (var key in ResultDataCache.Keys)
            {
                if (CheckExpiration(DateTime.Now, ResultDataCache[key].DataTime, span2))
                {
                    rkeys.Add(key);
                }
            }
            foreach (var key in rkeys)
            {
                ResultData data;
                ResultDataCache.TryRemove(key, out data);
            }
        }
        // 检查超时
        bool CheckExpiration(DateTime nowDateTime, DateTime lastUsed, TimeSpan slidingExpiration)
        {
            DateTime tmpNowDateTime = nowDateTime.ToUniversalTime();
            DateTime tmpLastUsed = lastUsed.ToUniversalTime();
            long expirationTicks = tmpLastUsed.Ticks + slidingExpiration.Ticks;
            bool expired = (tmpNowDateTime.Ticks >= expirationTicks) ? true : false;
            return expired;
        }
        // 计算循环
        public void CalculateLoop()
        {
            // 清理原始数据
            RefreshData();
            OriginData[] OriginDataSnap = OriginDataCache.Values.ToArray();

            // 按现有的数据计算所有有数据的定位卡
            foreach (int deviceid in deviceIDs.ToArray())
            {
                ResultData rdata = null;
                // 定位卡ID是deviceid的所有数据
                var datas = OriginDataSnap.Where(p => p.LocateDeviceID == deviceid).ToArray();
                // 按基站分组
                var q = from c in datas
                        group c by c.StationMac into g
                        select g.Key;
                // 所有组的分站
                string[] macs = q.ToArray();
                // 选出用于计算的数据，这里不对数据的新旧程度再做筛选，只要是11秒之内的数据即可参与计算
                List<OriginData> list = new List<OriginData>();
                foreach (var mac in macs)
                {   // 取出组中最新的一条数据并加入计算选择集合
                    OriginData temp = datas.Where(p => p.StationMac == mac).OrderByDescending(p => p.DataTime).First();
                    list.Add(temp);
                }
                if (list.Count == 1)
                {   // 只有一条记录时，分两种情况
                    ResultData rd;
                    ResultDataCache.TryGetValue(deviceid, out rd);
                    if (rd == null)
                    {   // 没有历史记录
                        // 是单基站带倾向方向的,目前数据结构还不支持这个问题
                        {
                            // 按倾向方向计算
                        }
                        // 不是单基站,数据不足不处理
                        {
                            continue;
                        }
                    }
                    else
                    {
                        //取当前基站数据的历史数据。选择新的位置距离历史的一个
                        OriginData od = list[0];
                        //计算x
                        double x = GetXFromSingleSourceX(od.StationX, od.StationY, rd.X, rd.Y, od.Distance);
                        //交换x轴y轴方式可计算y
                        double y = GetXFromSingleSourceX(od.StationY, od.StationX, rd.Y, rd.X, od.Distance);
                        rdata = new ResultData();
                        rdata.DataTime = DateTime.Now;
                        rdata.LocateDevice = deviceid;
                        rdata.X = x;
                        rdata.Y = y;
                        rdata.LayerID = od.LayerID;
                        rdata.OriginStationIDs = new int[] { od.StationID };
                    }
                }
                else if (list.Count == 2)
                {   // 两条记录按根轴与圆心连线交点计算
                    OriginData originData1 = list[0];
                    OriginData originData2 = list[1];
                    if (originData1.LayerID == originData2.LayerID)
                    {

                    }
                    else
                    {
                        continue;
                    }
                    double x = GetBowStringMidX(originData1.StationX, originData1.StationY, originData2.StationX, originData2.StationY, originData1.Distance, originData2.Distance);
                    double y = GetThreePointY(originData1.StationX, originData1.StationY, originData2.StationX, originData2.StationY, x);
                    rdata = new ResultData();
                    rdata.DataTime = DateTime.Now;
                    rdata.LocateDevice = deviceid;
                    rdata.X = x;
                    rdata.Y = y;
                    rdata.LayerID = originData1.LayerID;
                    rdata.OriginStationIDs = new int[] { originData1.StationID, originData2.StationID };

                    Console.WriteLine("({0},{1},{2})-({3},{4},{5})[{9}] ID:{6}({7},{8})".FormatWith(
                        originData1.StationX, originData1.StationY, originData1.Distance,
                        originData2.StationX, originData2.StationY, originData2.Distance,
                        deviceid, x, y, GetDistanceBetweenPoint(originData1.StationX, originData1.StationY, originData2.StationX, originData2.StationY)
                        ));
                }
                else if (list.Count >= 3)
                {   // 多条记录时，按平面定位计算

                    OriginData originData1 = list[0];
                    OriginData originData2 = list[1];
                    OriginData originData3 = list[2];
                    if (originData1.LayerID == originData2.LayerID && originData2.LayerID == originData3.LayerID)
                    {
                    }
                    else
                    {
                        continue;
                    }
                    double y = GetTriCircleRootHeartY(originData1.StationX, originData2.StationX, originData3.StationX,
                                                      originData1.StationY, originData2.StationY, originData3.StationY,
                                                      originData1.Distance, originData2.Distance, originData3.Distance);
                    double x = GetTriCircleRootHeartY(originData1.StationY, originData2.StationY, originData3.StationY,
                                                      originData1.StationX, originData2.StationX, originData3.StationX,
                                                      originData1.Distance, originData2.Distance, originData3.Distance);
                    rdata = new ResultData();
                    rdata.DataTime = DateTime.Now;
                    rdata.LocateDevice = deviceid;
                    rdata.X = x;
                    rdata.Y = y;
                    rdata.LayerID = originData1.LayerID;
                    rdata.OriginStationIDs = new int[] { originData1.StationID, originData2.StationID, originData3.StationID };
                }
                //根据计算的结果
                if (rdata != null)
                {
                    rdata = Projection(rdata);
                    ResultDataCache.AddOrUpdate(rdata.LocateDevice, rdata, (k, v) => rdata);
                }

            }
        }
        // 投影，对于有路径的时候需要做投影
        public ResultData Projection(ResultData rdata)
        {   //在投影的时候需要知道数据来源的基站与路径的关联信息
            double ShortestDistance = double.MaxValue;
            Point? resultp = null;
            //找出需要计算的关联路径
            foreach (int sid in rdata.OriginStationIDs)
            {
                foreach (Route route in Map[sid])
                {
                    //计算投影点
                    Point? p = GetProjectionPoint(route.X1, route.Y1, route.X2, route.Y2, rdata.X, rdata.Y);
                    if (p != null)
                    {
                        double dis = GetDistanceBetweenPoint(p.Value.X, p.Value.Y, rdata.X, rdata.Y);
                        if (dis < ShortestDistance)
                        {   //取投影距离最短的投影点
                            ShortestDistance = dis;
                            resultp = p.Value;
                        }
                    }
                }
            }
            if (resultp != null)
            {
                rdata.X = resultp.Value.X;
                rdata.Y = resultp.Value.Y;
            }
            return rdata;
        }
        // 已知起点和终点坐标，一点在终点方向到起点的距离,求此点坐标
        // 解法：直线段比例等于坐标差值比例 d/D=（x-x1）/（x2-x1） 
        public double GetXFromSingleSourceX(double x1, double y1, double x2, double y2, double distance)
        {
            if (x1 == x2 && y1 == y2)
            {
                return x1;
            }
            return x1 + distance * (x2 - x1) / GetDistanceBetweenPoint(x1, y1, x2, y2);
        }
        // 计算两点的距离
        public double GetDistanceBetweenPoint(double x1, double y1, double x2, double y2)
        {
            double sum = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
            return Math.Sqrt(sum);
        }
        // 平面定位3点坐标和到3点的距离 
        // 求x，y 两圆方程想减得到的直线叫做圆的根轴，也叫等幂轴，根轴上不在圆内的任意一点引两圆的切线 切线长相等
        // 圆心不共线的三圆两两想减得到的三条根轴交于一点，叫做根心
        public double GetTriCircleRootHeartY(double x1, double x2, double x3, double y1, double y2, double y3, double d1, double d2, double d3)
        {

            double fenzi = (x3 - x1) * (d1 * d1 - d2 * d2 - y1 * y1 + y2 * y2 - x1 * x1 + x2 * x2)
                - (x2 - x1) * (d1 * d1 - d3 * d3 + y3 * y3 - y1 * y1 + x3 * x3 - x1 * x1);
            double fenmu = (x2 - x1) * (2 * y1 - 2 * y3) - (x3 - x1) * (2 * y1 - 2 * y2);
            return fenzi / fenmu;
        }
        // 点到线段的投影,落在区间外的返回空值
        public Point? GetProjectionPoint(double x1, double y1, double x2, double y2, double xp, double yp)
        {
            Point p = new Point();
            //点到直线投影的X坐标
            p.X = GetGetProjectionX(x1, y1, x2, y2, xp, yp);
            //点到直线投影的Y坐标
            p.Y = GetThreePointY(x1, y1, x2, y2, p.X);
            //点到A端点距离
            double d1 = GetDistanceBetweenPoint(p.X, p.Y, x1, y1);
            //点到B端点距离
            double d2 = GetDistanceBetweenPoint(p.X, p.Y, x2, y2);
            //AB端点的距离
            double d = GetDistanceBetweenPoint(x1, y1, x2, y2);
            if (d1 + d2 - d > 0)
            {   //两个距离大于AB基站距离认为投影没落在AB的线段上。
                return null;
            }
            return p;
        }
        // 求点到直线的投影的X坐标
        double GetGetProjectionX(double x1, double y1, double x2, double y2, double x0, double y0)
        {
            double fenzi = (y0 - y1) * (x1 - x2) * (y1 - y2) + x0 * (x1 - x2) * (x1 - x2) + x1 * (y1 - y2) * (y1 - y2);
            double fenmu = (y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2);
            return fenzi / fenmu;
        }

        //取两圆根轴与圆心连线焦点
        public double GetBowStringMidX(double x1, double y1, double x2, double y2, double d1, double d2)
        {
            double fenzi = (y2 - y1) * (y2 * x1 - y1 * x2) - 0.5 * (d1 * d1 - d2 * d2 - x1 * x1 + x2 * x2 - y1 * y1 + y2 * y2) * (x1 - x2);
            double fenmu = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
            return fenzi / fenmu;
        }
        //已知两点加一点的X求Y
        public double GetThreePointY(double x1, double y1, double x2, double y2, double x)
        {
            if (x1 == x2) return y1;
            double y = ((y1 - y2) * x + (y2 * x1 - y1 * x2)) / (x1 - x2);
            return y;
        }

    }
    // 原始数据
    public class OriginData
    {
        public string OriginDataID { get; set; }
        public int LayerID { get; set; }
        public int StationID { get; set; }
        public string StationMac { get; set; }
        public double StationX { get; set; }
        public double StationY { get; set; }
        public int LocateDeviceID { get; set; }
        public double Distance { get; set; }
        public DateTime DataTime { get; set; }
        public int CalcCount { get; set; }
    }
    // 计算结果
    public class ResultData
    {
        public int[] OriginStationIDs { get; set; }
        public int LayerID { get; set; }
        public int LocateDevice { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public DateTime DataTime { get; set; }
    }

    public class Route
    {
        public int RouteID { get; set; }
        public double X1 { get; set; }
        public double Y1 { get; set; }

        public double X2 { get; set; }
        public double Y2 { get; set; }
    }
}
