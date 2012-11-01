using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using System.Windows.Controls;
using Win8Style.View;

namespace Win8Style
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = AppStartup.CreateApp(null);
            Window w = AppStartup.InitApp();
            w.Show();
            app.Run();
        }
    }
    public static class AppStartup
    {
        public static Application CreateApp(Application app)
        {
            if (app == null)
                app = new Application();
            return app;
        }
        public static Window InitApp()
        {
            ThemeManager.ApplicationThemeName = Theme.MetropolisLight.Name;
            Window main = new MainWindow();
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(200));
            main.Title = "Win8 Style";
            main.Width = 1300.0;
            main.Height = 730.0;
            main.MinWidth = 1000.0;
            main.MinHeight = 600.0;
            SetCultureInfo();
            return main;
        }
        static void SetCultureInfo()
        {
            CultureInfo demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            demoCI.NumberFormat.CurrencySymbol = "$";
            Thread.CurrentThread.CurrentCulture = demoCI;
        }
    }
}