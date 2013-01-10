namespace WeiboTimer
{
    using NetDimension.Weibo;
    using NetDimension.Weibo.Entities.status;
    using NetDimension.Weibo.Entities.user;
    using System;
    using System.Globalization;
    using System.Timers;

    internal class Program
    {
        private static string AppKey = "82966982";
        private static string AppSecret = "72d4545a28a46a6f329c4f2b1e949e6a";
        private static string CallbackUrl = "https://api.weibo.com/oauth2/default.html";
        private static OAuth oauth = null;
        private static Client Sina = null;
        private static Timer t = new Timer();
        static string[] s = null;
        private static OAuth Authorize()
        {
            s = "".ReadFromFile("900.txt").Split('\n');
            OAuth o = new OAuth(AppKey, AppSecret, CallbackUrl);
            while (!ClientLogin(o))
            {
                Console.WriteLine("登录失败，请重试。");
            }
            return o;
        }

        private static bool ClientLogin(OAuth o)
        {
            Console.Write("微博账号:");
            string passport = Console.ReadLine();
            Console.Write("登录密码:");
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            string password = Console.ReadLine();
            Console.ForegroundColor = foregroundColor;
            return o.ClientLogin(passport, password);
        }

        public void logger(string content)
        {
            DateTime now = DateTime.Now;
            Console.Write(now.ToString("d") + " " + now.ToString("T"));
            Console.WriteLine(" " + content);
        }

        private static void Main(string[] args)
        {
            oauth = Authorize();
            if (!string.IsNullOrEmpty(oauth.AccessToken))
            {
                Console.Write("登录成功！");
            }
            Sina = new Client(oauth);
            try
            {
                string uID = Sina.API.Entity.Account.GetUID();
                NetDimension.Weibo.Entities.user.Entity entity = Sina.API.Entity.Users.Show(uID, "");
              
               


                Console.WriteLine("昵称：{0}，微博地址：http://weibo.com/{1}", entity.ScreenName, entity.ProfileUrl);
                try
                {
                    t.Interval = 5000.0;
                    t.Elapsed += new ElapsedEventHandler(Program.TimerElapsedEvent);
                    t.AutoReset = true;
                    t.Start();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("定时任务异常：" + exception.Message);
                    t.Stop();
                }
            }
            catch (WeiboException exception2)
            {
                Console.WriteLine("出错啦！" + exception2.Message);
            }
            Console.WriteLine("定时任务开始工作。。。");
            Console.ReadKey();
        }
        
        static int i = 0;
        private static void TimerElapsedEvent(object sender, ElapsedEventArgs e)
        {
            DateTime now = DateTime.Now;
            try
            {



                string str = s[i++].Trim() + "【自动定时微博测试】";// +" 测试时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " GUID:" + Guid.NewGuid();
                if (!string.IsNullOrEmpty(str))
                {
                    NetDimension.Weibo.Entities.status.Entity entity = Sina.API.Entity.Statuses.Update(str, 0f, 0f, "");
                    DateTime time2 = DateTime.ParseExact(entity.CreatedAt, "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.InvariantCulture);
                    Console.WriteLine("本机时间：{0}, 微博时间：{1}, 发布内容：{2}", string.Format("{0:G}", now), string.Format("{0:G}", time2), entity.Text);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("定时任务异常：" + exception.Message);
            }
        }
    }
}

