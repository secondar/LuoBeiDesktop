using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LuoBeiDesktop
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {


        void OnStartup(object sender, StartupEventArgs e)

        {
            if (e.Args.Length >= 1) extend.Minimized.Minimizeds = true;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
