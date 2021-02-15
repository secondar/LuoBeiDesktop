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
            string MName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;
            string PName = System.IO.Path.GetFileNameWithoutExtension(MName);
            System.Diagnostics.Process[] myProcess = System.Diagnostics.Process.GetProcessesByName(PName);

            if (myProcess.Length > 1)
            {
                MessageBox.Show("This program can only run one instance at a time!", "Error");
                Application.Current.Shutdown();
                return;
            }
            else
            {
                if (e.Args.Length >= 1) extend.Minimized.Minimizeds = true;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }

        }
    }
}
