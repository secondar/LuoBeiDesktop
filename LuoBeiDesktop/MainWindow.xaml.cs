using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LuoBeiDesktop
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaBackground mediaBackground = null;
        extend.BackgroundState BackgroundState = new extend.BackgroundState(false, 0);
        private readonly System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();

        private readonly TaskScheduler _syncContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                InitSystem();
                initNotifyIcon();
                Init();
                ThemeListPage themeListPage = new ThemeListPage();
                this.frame.Content = themeListPage;
                themeListPage.ParentWindow = this;
                if (extend.Minimized.Minimizeds)
                {
                    Task.Factory.StartNew(SchedulerWork);
                }
            }catch(Exception e)
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Error", extend.Language.InitFail, "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int types) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
        }

        private void InitSystem()
        {
            SQLiteConnection con = new extend.Common().NewSQLite();
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM system WHERE id = 1";
            SQLiteDataReader sr = cmd.ExecuteReader();
            if (sr.Read())
            {
                extend.SystemConfig.Id = (int)Convert.ToInt64(sr["id"]);
                extend.SystemConfig.Volume = (int)Convert.ToInt64(sr["volume"]);
                extend.SystemConfig.Run = (int)Convert.ToInt64(sr["run"]);
                extend.SystemConfig.Language = sr["language"].ToString();
            }
            else{
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo(extend.Language.Error, extend.Language.GetConfFail, "", extend.Language.Ok, 130, 350, false);
                messageBoxWindow.getTextHandler += (int types) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
            sr.Close();
            con.Close();
            con.Open();
            cmd.CommandText = "SELECT * FROM language WHERE type = '"+extend.SystemConfig.Language+"'";
            sr = cmd.ExecuteReader();
            while(sr.Read())
            {
                if (sr["field"].ToString() == "home") extend.Language.Home = sr["text"].ToString();
                if (sr["field"].ToString() == "add") extend.Language.Add = sr["text"].ToString();
                if (sr["field"].ToString() == "shop") extend.Language.Shop = sr["text"].ToString();
                if (sr["field"].ToString() == "seting") extend.Language.Seting = sr["text"].ToString();
                if (sr["field"].ToString() == "extend") extend.Language.Extend = sr["text"].ToString();
                if (sr["field"].ToString() == "feedback") extend.Language.Feedback = sr["text"].ToString();
                if (sr["field"].ToString() == "feedback") extend.Language.Feedback = sr["text"].ToString();
                if (sr["field"].ToString() == "feedback") extend.Language.Feedback = sr["text"].ToString();
                if (sr["field"].ToString() == "feedback") extend.Language.Feedback = sr["text"].ToString();
                if (sr["field"].ToString() == "notthemeconf") extend.Language.NotThemeConf = sr["text"].ToString();
                if (sr["field"].ToString() == "mainwindow") extend.Language.MainWindow = sr["text"].ToString();
                if (sr["field"].ToString() == "mainwindow") extend.Language.MainWindow = sr["text"].ToString();
                if (sr["field"].ToString() == "exit") extend.Language.Exit = sr["text"].ToString();
                if (sr["field"].ToString() == "closewallpaper") extend.Language.CloseWallpaper = sr["text"].ToString();
                if (sr["field"].ToString() == "openwallpaper") extend.Language.OpenWallpaper = sr["text"].ToString();
                if (sr["field"].ToString() == "numberedmode") extend.Language.NumberedMode = sr["text"].ToString();
                if (sr["field"].ToString() == "Initwallpaperfail") extend.Language.InitWallpaperFail = sr["text"].ToString();
                if (sr["field"].ToString() == "initwallpapersuccess") extend.Language.InitWallpaperSuccess = sr["text"].ToString();
                if (sr["field"].ToString() == "exitmsg") extend.Language.Extend = sr["text"].ToString();
                if (sr["field"].ToString() == "tips") extend.Language.Tips = sr["text"].ToString();
                if (sr["field"].ToString() == "confirm") extend.Language.Confirm = sr["text"].ToString();
                if (sr["field"].ToString() == "cancel") extend.Language.Cancel = sr["text"].ToString();

            }
            if (extend.Language.Home == null|| extend.Language.Home == "")
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo(extend.Language.Error, extend.Language.GetLanguageFailk, "", extend.Language.Ok, 130, 350, false);
                messageBoxWindow.getTextHandler += (int types) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
            if (extend.Language.Home != "") btn_Home_Text.Text = extend.Language.Home;
            if (extend.Language.Add != "") btn_Add_Text.Text = extend.Language.Add;
            if (extend.Language.Shop != "") btn_Shop_Text.Text = extend.Language.Shop;
            if (extend.Language.Extend != "") btn_Extend_Text.Text = extend.Language.Extend;
            if (extend.Language.Feedback != "") btn_Feedback_Text.Text = extend.Language.Feedback;
            if (extend.Language.Seting != "") btn_Seting_Text.Text = extend.Language.Seting;
            sr.Close();
            con.Close();



        }
        private void SchedulerWork()
        {
            Task.Factory.StartNew(Begin, this).Wait();
        }

        private void Begin(object obj)
        {
            MainWindow mw = obj as MainWindow;
            Thread.Sleep(1000);
            Task.Factory.StartNew(() => UpdateTb(mw), new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler).Wait();
        }

        private void UpdateTb(MainWindow mw)
        {
            mw.Hide();
        }

        private void Init()
        {
            SQLiteConnection con = new extend.Common().NewSQLite();
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM themelist WHERE use = 1";
            SQLiteDataReader sr = cmd.ExecuteReader();
            List<string> tables = new List<string>();
            if (sr.Read())
            {
                Int64 id = Convert.ToInt64(sr["id"]);
                string name = sr["name"].ToString();
                string thumbnail = sr["thumbnail"].ToString();
                string path = sr["path"].ToString();
                int type = (int)Convert.ToInt64(sr["type"]);
                bool networks = false;
                bool use = false;
                string author = sr["author"].ToString();
                string remarks = sr["remarks"].ToString();
                string addtime = sr["addtime"].ToString();
                if ((int)Convert.ToInt64(sr["networks"]) == 1) networks = true;
                if ((int)Convert.ToInt64(sr["use"]) == 1) use = true;
                sr.Close();
                con.Close();
                SetMediaBackground(networks, type, path);
            }
            else
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo(extend.Language.Error, extend.Language.NotThemeConf, "", extend.Language.Ok, 130, 350, false);
                messageBoxWindow.getTextHandler += (int types) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
        }
        //--
        private void initNotifyIcon()
        {
            nIcon.Visible = true;
            nIcon.Icon = new Icon("./Resources/favicon.ico");
            nIcon.Text = "LuoBeiDesktop";
            nIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(show_Click);
            nIcon.ContextMenu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem show = new System.Windows.Forms.MenuItem(extend.Language.MainWindow);
            System.Windows.Forms.MenuItem showBackground = new System.Windows.Forms.MenuItem(extend.Language.OpenWallpaper);
            System.Windows.Forms.MenuItem closeBackground = new System.Windows.Forms.MenuItem(extend.Language.CloseWallpaper);
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem(extend.Language.Exit);
            show.Click += new EventHandler(show_Click);
            showBackground.Click += new EventHandler(showBackground_Click);
            closeBackground.Click += new EventHandler(closeBackground_Click);
            exit.Click += new EventHandler(exit_Click);
            nIcon.ContextMenu.MenuItems.Add(show);
            nIcon.ContextMenu.MenuItems.Add(closeBackground);
            nIcon.ContextMenu.MenuItems.Add(showBackground);
            nIcon.ContextMenu.MenuItems.Add(exit);
        }
        private void closeBackground_Click(object sender, EventArgs e)
        {
            closeBackground();
        }
        private void showBackground_Click(object sender, EventArgs e)
        {
            showBackground();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void show_Click(object Sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                WindowState = WindowState.Normal;
            Show();
            Activate();
        }
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized) Hide();
            base.OnStateChanged(e);
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            base.OnClosing(e);
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        public void SetMediaBackground(bool networks,int type,string path)
        {
            if (type > 2)
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo(extend.Language.Error, extend.Language.NumberedMode, "", extend.Language.Ok, 130, 350, false);
                messageBoxWindow.getTextHandler += (int types) =>
                {

                };
                messageBoxWindow.ShowDialog();
                return;
            }
            extend.ResultState result = null;
            result = InitMediaBackground();
            if (!result.Ok) {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo(extend.Language.Error, result.Msg, "", extend.Language.Ok, 130, 350, false);
                messageBoxWindow.getTextHandler += (int types) =>
                {

                };
                messageBoxWindow.ShowDialog();
                return;
            }
            if (type <=2) result = MediaBackgroundPlay(networks, path);
            if (!result.Ok) {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo(extend.Language.Error, result.Msg, "", extend.Language.Ok, 130, 350, false);
                messageBoxWindow.getTextHandler += (int types) =>
                {

                };
                messageBoxWindow.ShowDialog();
                return;
            }
            else
            {
                SetMediaBackgroundPlayVolume(extend.SystemConfig.Volume);
            }
        }
        /// <summary>
        /// 初始化视频背景窗体
        /// </summary>
        public extend.ResultState InitMediaBackground()
        {
            if (!BackgroundState.Init)
            {
                try
                {
                    mediaBackground = new MediaBackground();
                    mediaBackground.Show();
                    // 通过类名查找一个窗口，返回窗口句柄。
                    IntPtr programIntPtr = IntPtr.Zero;
                    programIntPtr = extend.Win32.FindWindow("Progman", null);
                    // 窗口句柄有效
                    if (programIntPtr != IntPtr.Zero)
                    {
                        IntPtr result = IntPtr.Zero;
                        // 向 Program Manager 窗口发送 0x52c 的一个消息，超时设置为0x3e8（1秒）。
                        extend.Win32.SendMessageTimeout(programIntPtr, 0x52c, IntPtr.Zero, IntPtr.Zero, 0, 0x3e8, result);
                        // 遍历顶级窗口
                        extend.Win32.EnumWindows((hwnd, lParam) =>
                        {
                        // 找到包含 SHELLDLL_DefView 这个窗口句柄的 WorkerW
                        if (extend.Win32.FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", null) != IntPtr.Zero)
                            {
                            // 找到当前 WorkerW 窗口的，后一个 WorkerW 窗口。
                            IntPtr tempHwnd = extend.Win32.FindWindowEx(IntPtr.Zero, hwnd, "WorkerW", null);

                            // 隐藏这个窗口
                            extend.Win32.ShowWindow(tempHwnd, 0);
                            }
                            return true;
                        }, IntPtr.Zero);
                    }
                    extend.Win32.SetParent(mediaBackground.Handle, programIntPtr);
                    BackgroundState.Init = true;
                    BackgroundState.Type = 1;
                    return new extend.ResultState(true, extend.Language.InitWallpaperSuccess);
                }
                catch(Exception e)
                {
                    BackgroundState.Init = false;
                    BackgroundState.Type = 0;
                    return new extend.ResultState(false, extend.Language.InitWallpaperFail, 1);
                }
            }
            else
            {
                BackgroundState.Init = true;
                BackgroundState.Type = 1;
                return new extend.ResultState(true, extend.Language.InitWallpaperSuccess);
            }
        }
        /// <summary>
        /// 使用视频媒体背景进行播放
        /// </summary>
        /// <param name="network"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public extend.ResultState MediaBackgroundPlay(bool network,string Path)
        {
            extend.ResultState result = null;
            if (!BackgroundState.Init)
            {
                result =  InitMediaBackground();
                if (!result.Ok)
                {
                    if(result.Code == 1)
                    {
                        return new extend.ResultState(false, extend.Language.InitWallpaperFail, 1);
                    }
                }
            }
            if (network) result = mediaBackground.SetMediaNetwork(Path);
            else result = mediaBackground.SetMediaLocal(Path);
            if (result.Ok)
            {
                result = mediaBackground.Play();
                if (result.Ok)
                {
                    return new extend.ResultState(true);
                }
                else
                {
                    return new extend.ResultState(false, result.Msg, 1);
                }
            }
            else
            {
                return new extend.ResultState(false, result.Msg, 1);
            }
        }
 
        /// <summary>
        /// 设置媒体播放音量
        /// </summary>
        /// <param name="volume"></param>
        public void SetMediaBackgroundPlayVolume(int volume)
        {
            if (BackgroundState.Init && BackgroundState.Type <= 2) mediaBackground.SetVoice(volume);
        }
        /// <summary>
        /// 允许窗口拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            try
            {
                this.DragMove();
            }
            catch { }
        }
        /// <summary>
        /// 退出壁纸
        /// </summary>
        public void closeBackground()
        {
            if (BackgroundState.Init)
            {
                BackgroundState.Init = false;
                BackgroundState.Type = 0;
                mediaBackground.Dispose();
            }
        }

        public void showBackground()
        {
            //Init();
        }
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
            messageBoxWindow.setInfo(extend.Language.Tips, extend.Language.ExitMsg, extend.Language.Exit,extend.Language.Confirm,130,350);
            messageBoxWindow.getTextHandler += (int type)=>{
                if (type == 1) System.Environment.Exit(0);
                else this.Hide();
            };
            messageBoxWindow.ShowDialog();
        }

        private void btn_Tray_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void setBtnState()
        {

            btn_Home.Background = new SolidColorBrush(Colors.Transparent);
            btn_Home_Ico.Foreground = new SolidColorBrush(Colors.Black);
            btn_Home_Text.Foreground = new SolidColorBrush(Colors.Black);

            btn_Add.Background = new SolidColorBrush(Colors.Transparent);
            btn_Add_Ico.Foreground = new SolidColorBrush(Colors.Black);
            btn_Add_Text.Foreground = new SolidColorBrush(Colors.Black);

            btn_Shop.Background = new SolidColorBrush(Colors.Transparent);
            btn_Shop_Ico.Foreground = new SolidColorBrush(Colors.Black);
            btn_Shop_Text.Foreground = new SolidColorBrush(Colors.Black);

            btn_Extend.Background = new SolidColorBrush(Colors.Transparent);
            btn_Extend_Ico.Foreground = new SolidColorBrush(Colors.Black);
            btn_Extend_Text.Foreground = new SolidColorBrush(Colors.Black);

            btn_Feedback.Background = new SolidColorBrush(Colors.Transparent);
            btn_Feedback_Ico.Foreground = new SolidColorBrush(Colors.Black);
            btn_Feedback_Text.Foreground = new SolidColorBrush(Colors.Black);

            btn_Seting.Background = new SolidColorBrush(Colors.Transparent);
            btn_Seting_Ico.Foreground = new SolidColorBrush(Colors.Black);
            btn_Seting_Text.Foreground = new SolidColorBrush(Colors.Black);

        }
        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            setBtnState();
            btn_Home.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFB6A25");
            btn_Home_Ico.Foreground = new SolidColorBrush(Colors.White);
            btn_Home_Text.Foreground = new SolidColorBrush(Colors.White);

            ThemeListPage themeListPage = new ThemeListPage();
            this.frame.Content = themeListPage;
            themeListPage.ParentWindow = this;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            setBtnState();
            btn_Add.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFB6A25");
            btn_Add_Ico.Foreground = new SolidColorBrush(Colors.White);
            btn_Add_Text.Foreground = new SolidColorBrush(Colors.White);
            this.frame.Navigate(new Uri("AddThemePage.xaml", UriKind.Relative));
        }

        private void btn_Shop_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
            messageBoxWindow.setInfo("remind", "Coming soon", "", "OK", 130, 350, false);
            messageBoxWindow.getTextHandler += (int types) =>
            {

            };
            messageBoxWindow.ShowDialog();
            return;

            setBtnState();
            btn_Shop.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFB6A25");
            btn_Shop_Ico.Foreground = new SolidColorBrush(Colors.White);
            btn_Shop_Text.Foreground = new SolidColorBrush(Colors.White);
        }

        private void btn_Extend_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
            messageBoxWindow.setInfo("remind", "Coming soon", "", "OK", 130, 350, false);
            messageBoxWindow.getTextHandler += (int types) =>
            {

            };
            messageBoxWindow.ShowDialog();
            return;

            setBtnState();
            btn_Extend.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFB6A25");
            btn_Extend_Ico.Foreground = new SolidColorBrush(Colors.White);
            btn_Extend_Text.Foreground = new SolidColorBrush(Colors.White);
        }

        private void btn_Feedback_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
            messageBoxWindow.setInfo("remind", "Coming soon", "", "OK", 130, 350, false);
            messageBoxWindow.getTextHandler += (int types) =>
            {

            };
            messageBoxWindow.ShowDialog();
            return;

            setBtnState();
            btn_Feedback.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFB6A25");
            btn_Feedback_Ico.Foreground = new SolidColorBrush(Colors.White);
            btn_Feedback_Text.Foreground = new SolidColorBrush(Colors.White);
        }

        private void btn_Seting_Click(object sender, RoutedEventArgs e)
        {
            setBtnState();
            btn_Seting.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFB6A25");
            btn_Seting_Ico.Foreground = new SolidColorBrush(Colors.White);
            btn_Seting_Text.Foreground = new SolidColorBrush(Colors.White);
            SetingPage setingPage = new SetingPage();
            this.frame.Content = setingPage;
            setingPage.ParentWindow = this;
        }

        private void MainWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(this.WindowState == WindowState.Minimized) this.Hide();
        }

        private void btn_About_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog();;
        }
    }
}
