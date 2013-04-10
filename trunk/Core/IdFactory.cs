using System.Runtime.InteropServices;
namespace System
{
    // 时间组合Guid类。
    public class IdFactory
    {
        // 时间后置的GUID。
        public static Guid NewDateGuid()
        {
            // Sql写法：CAST(CAST(NEWID() AS BINARY(10)) + CAST(GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER)
            byte[] guidArray = System.Guid.NewGuid().ToByteArray();
            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            // 此处除以3.333为了使精度上与sqlserver中的GETDATE()方法获取时间的精度保持一致。
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));
            // 日期(baseDate为19000101，到2079年夏季为一个周期，因为随Days的增加BitConverter.GetBytes(days.Days)结果溢出)。
            Array.Reverse(daysArray);
            // 毫秒
            Array.Reverse(msecsArray);
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            // 毫秒部分的数值最大时23时59分59秒后面略，转成16进制最高位（倒数8位）上始终为0。
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new System.Guid(guidArray);
        }
        // 时间前置的GUID。
        public static string NewGuid()
        {
            //CAST(CAST(NEWID() AS BINARY(10)) + CAST(GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER)
            byte[] guidArray = System.Guid.NewGuid().ToByteArray();
            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString());
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));
            //日期
            Array.Copy(daysArray, 0, guidArray, 2, 2);
            //毫秒高位
            Array.Copy(msecsArray, 2, guidArray, 0, 2);
            //毫秒低位
            Array.Copy(msecsArray, 0, guidArray, 4, 2);
            return new System.Guid(guidArray).ToString();
        }
        // 取GUID中的时间。
        public static DateTime GetDateFromComb(System.Guid guid)
        {
            DateTime baseDate = new DateTime(1900, 1, 1);
            byte[] daysArray = new byte[4];
            byte[] msecsArray = new byte[4];
            byte[] guidArray = guid.ToByteArray();
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
            Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);
            int days = BitConverter.ToInt32(daysArray, 0);
            int msecs = BitConverter.ToInt32(msecsArray, 0);
            DateTime date = baseDate.AddDays(days);
            date = date.AddMilliseconds(msecs * 3.333333);
            return date;
        }
        // 生成有序GUID. 如果生成失败则使用默认的乱序Guid.  
        public static Guid NewSeqGuid()
        {
            Guid sequentialGuid;
            int hResult = UuidCreateSequential(out sequentialGuid);
            if (hResult == RPC_S_OK)
            {
                return sequentialGuid;
            }
            else
            {
                return Guid.NewGuid();
            }
        }
        [DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern int UuidCreateSequential(out Guid guid);
        private const int RPC_S_OK = 0;
    }
}
