using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
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
        private int TrayClickNum = 0;
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
                extend.SystemConfig.Versions = sr["versions"].ToString();
                extend.SystemConfig.Update = (int)Convert.ToInt64(sr["update"]);
                extend.SystemConfig.UpdateAppVersion = sr["updateappversion"].ToString();
            }
            else
            {
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
            cmd.CommandText = "SELECT * FROM language WHERE type = '" + extend.SystemConfig.Language + "'";
            sr = cmd.ExecuteReader();
            while (sr.Read())
            {
                if (sr["field"].ToString() == "home" && sr["field"].ToString() != "") extend.Language.Home = sr["text"].ToString();
                if (sr["field"].ToString() == "add" && sr["field"].ToString() != "") extend.Language.Add = sr["text"].ToString();
                if (sr["field"].ToString() == "shop" && sr["field"].ToString() != "") extend.Language.Shop = sr["text"].ToString();
                if (sr["field"].ToString() == "seting" && sr["field"].ToString() != "") extend.Language.Seting = sr["text"].ToString();
                if (sr["field"].ToString() == "extend" && sr["field"].ToString() != "") extend.Language.Extend = sr["text"].ToString();
                if (sr["field"].ToString() == "feedback" && sr["field"].ToString() != "") extend.Language.Feedback = sr["text"].ToString();
                if (sr["field"].ToString() == "feedback" && sr["field"].ToString() != "") extend.Language.Feedback = sr["text"].ToString();
                if (sr["field"].ToString() == "feedback" && sr["field"].ToString() != "") extend.Language.Feedback = sr["text"].ToString();
                if (sr["field"].ToString() == "feedback" && sr["field"].ToString() != "") extend.Language.Feedback = sr["text"].ToString();
                if (sr["field"].ToString() == "notthemeconf" && sr["field"].ToString() != "") extend.Language.NotThemeConf = sr["text"].ToString();
                if (sr["field"].ToString() == "mainwindow" && sr["field"].ToString() != "") extend.Language.MainWindow = sr["text"].ToString();
                if (sr["field"].ToString() == "mainwindow" && sr["field"].ToString() != "") extend.Language.MainWindow = sr["text"].ToString();
                if (sr["field"].ToString() == "exit" && sr["field"].ToString() != "") extend.Language.Exit = sr["text"].ToString();
                if (sr["field"].ToString() == "closewallpaper" && sr["field"].ToString() != "") extend.Language.CloseWallpaper = sr["text"].ToString();
                if (sr["field"].ToString() == "openwallpaper" && sr["field"].ToString() != "") extend.Language.OpenWallpaper = sr["text"].ToString();
                if (sr["field"].ToString() == "numberedmode" && sr["field"].ToString() != "") extend.Language.NumberedMode = sr["text"].ToString();
                if (sr["field"].ToString() == "Initwallpaperfail" && sr["field"].ToString() != "") extend.Language.InitWallpaperFail = sr["text"].ToString();
                if (sr["field"].ToString() == "initwallpapersuccess" && sr["field"].ToString() != "") extend.Language.InitWallpaperSuccess = sr["text"].ToString();
                if (sr["field"].ToString() == "exitmsg" && sr["field"].ToString() != "") extend.Language.ExitMsg = sr["text"].ToString();
                if (sr["field"].ToString() == "tips" && sr["field"].ToString() != "") extend.Language.Tips = sr["text"].ToString();
                if (sr["field"].ToString() == "confirm" && sr["field"].ToString() != "") extend.Language.Confirm = sr["text"].ToString();
                if (sr["field"].ToString() == "cancel" && sr["field"].ToString() != "") extend.Language.Cancel = sr["text"].ToString();
                if (sr["field"].ToString() == "thereisanerror" && sr["field"].ToString() != "") extend.Language.ThereIsAnError = sr["text"].ToString();
                if (sr["field"].ToString() == "setasdesktop" && sr["field"].ToString() != "") extend.Language.SetAsDesktop = sr["text"].ToString();
                if (sr["field"].ToString() == "deletetheme" && sr["field"].ToString() != "") extend.Language.DeleteTheme = sr["text"].ToString();
                if (sr["field"].ToString() == "about" && sr["field"].ToString() != "") extend.Language.About = sr["text"].ToString();
                if (sr["field"].ToString() == "author" && sr["field"].ToString() != "") extend.Language.Author = sr["text"].ToString();
                if (sr["field"].ToString() == "blog" && sr["field"].ToString() != "") extend.Language.Blog = sr["text"].ToString();
                if (sr["field"].ToString() == "donation" && sr["field"].ToString() != "") extend.Language.Donation = sr["text"].ToString();
                if (sr["field"].ToString() == "sdklist" && sr["field"].ToString() != "") extend.Language.SdkList = sr["text"].ToString();
                if (sr["field"].ToString() == "versions" && sr["field"].ToString() != "") extend.Language.Versions = sr["text"].ToString();
                if (sr["field"].ToString() == "website" && sr["field"].ToString() != "") extend.Language.Website = sr["text"].ToString();
                if (sr["field"].ToString() == "comingsoon" && sr["field"].ToString() != "") extend.Language.ComingSoon = sr["text"].ToString();
                if (sr["field"].ToString() == "on" && sr["field"].ToString() != "") extend.Language.ON = sr["text"].ToString();
                if (sr["field"].ToString() == "off" && sr["field"].ToString() != "") extend.Language.OFF = sr["text"].ToString();
                if (sr["field"].ToString() == "restarttotakeeffect" && sr["field"].ToString() != "") extend.Language.RestartTotakeEffect = sr["text"].ToString();
                if (sr["field"].ToString() == "success" && sr["field"].ToString() != "") extend.Language.Success = sr["text"].ToString();
                if (sr["field"].ToString() == "savefailed" && sr["field"].ToString() != "") extend.Language.SaveFailed = sr["text"].ToString();
                if (sr["field"].ToString() == "autoboot" && sr["field"].ToString() != "") extend.Language.Autoboot = sr["text"].ToString();
                if (sr["field"].ToString() == "language" && sr["field"].ToString() != "") extend.Language.Languages = sr["text"].ToString();
                if (sr["field"].ToString() == "volume" && sr["field"].ToString() != "") extend.Language.Volume = sr["text"].ToString();
                if (sr["field"].ToString() == "save" && sr["field"].ToString() != "") extend.Language.Save = sr["text"].ToString();
                if (sr["field"].ToString() == "localvideo" && sr["field"].ToString() != "") extend.Language.LocalVideo = sr["text"].ToString();
                if (sr["field"].ToString() == "networkvideo" && sr["field"].ToString() != "") extend.Language.NetworkVideo = sr["text"].ToString();
                if (sr["field"].ToString() == "localimage" && sr["field"].ToString() != "") extend.Language.LocalImage = sr["text"].ToString();
                if (sr["field"].ToString() == "webpage" && sr["field"].ToString() != "") extend.Language.Webpage = sr["text"].ToString();
                if (sr["field"].ToString() == "addname" && sr["field"].ToString() != "") extend.Language.AddName = sr["text"].ToString();
                if (sr["field"].ToString() == "addnametip" && sr["field"].ToString() != "") extend.Language.AddName_Tip = sr["text"].ToString();
                if (sr["field"].ToString() == "thumbnail" && sr["field"].ToString() != "") extend.Language.Thumbnail = sr["text"].ToString();
                if (sr["field"].ToString() == "addthumbnailtip" && sr["field"].ToString() != "") extend.Language.AddThumbnail_Tip = sr["text"].ToString();
                if (sr["field"].ToString() == "election" && sr["field"].ToString() != "") extend.Language.Election = sr["text"].ToString();
                if (sr["field"].ToString() == "type" && sr["field"].ToString() != "") extend.Language.Type = sr["text"].ToString();
                if (sr["field"].ToString() == "addtypetip" && sr["field"].ToString() != "") extend.Language.AddType_Tip = sr["text"].ToString();
                if (sr["field"].ToString() == "addpathtip" && sr["field"].ToString() != "") extend.Language.AddPath_Tip = sr["text"].ToString();
                if (sr["field"].ToString() == "path" && sr["field"].ToString() != "") extend.Language.Path = sr["text"].ToString();
                if (sr["field"].ToString() == "addauthortip" && sr["field"].ToString() != "") extend.Language.AddAuthor_Tip = sr["text"].ToString();
                if (sr["field"].ToString() == "addnotes" && sr["field"].ToString() != "") extend.Language.AddNotes = sr["text"].ToString();
                if (sr["field"].ToString() == "notestip" && sr["field"].ToString() != "") extend.Language.Notes_Tip = sr["text"].ToString();
                if (sr["field"].ToString() == "internetpictures" && sr["field"].ToString() != "") extend.Language.InternetPictures = sr["text"].ToString();
                if (sr["field"].ToString() == "checkupdates" && sr["field"].ToString() != "") extend.Language.CheckUpdates = sr["text"].ToString();
                if (sr["field"].ToString() == "newupdates" && sr["field"].ToString() != "") extend.Language.NewUpdates = sr["text"].ToString();
                if (sr["field"].ToString() == "update" && sr["field"].ToString() != "") extend.Language.Update = sr["text"].ToString();
                if (sr["field"].ToString() == "notupdateapp" && sr["field"].ToString() != "") extend.Language.NotUpdateApp = sr["text"].ToString();
                if (sr["field"].ToString() == "download" && sr["field"].ToString() != "") extend.Language.Download = sr["text"].ToString();
                if (sr["field"].ToString() == "filename" && sr["field"].ToString() != "") extend.Language.FileName = sr["text"].ToString();
                if (sr["field"].ToString() == "downloaded" && sr["field"].ToString() != "") extend.Language.Downloaded = sr["text"].ToString();
                if (sr["field"].ToString() == "opendownloadfail" && sr["field"].ToString() != "") extend.Language.  OpenDownloadFail = sr["text"].ToString();
                if (sr["field"].ToString() == "newupdateapp" && sr["field"].ToString() != "") extend.Language.NewUpdateApp = sr["text"].ToString();
                if (sr["field"].ToString() == "state" && sr["field"].ToString() != "") extend.Language.State = sr["text"].ToString();
                if (sr["field"].ToString() == "downloading" && sr["field"].ToString() != "") extend.Language.Downloading = sr["text"].ToString();
                if (sr["field"].ToString() == "fail" && sr["field"].ToString() != "") extend.Language.Fail = sr["text"].ToString();
                if (sr["field"].ToString() == "notfinished" && sr["field"].ToString() != "") extend.Language.NotFinished = sr["text"].ToString();
                if (sr["field"].ToString() == "downloadfail" && sr["field"].ToString() != "") extend.Language.DownloadFail = sr["text"].ToString();
                


    }
            
            if (extend.Language.Home == null || extend.Language.Home == "")
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo(extend.Language.Error, extend.Language.GetLanguageFailk, "", extend.Language.Ok, 130, 350, false);
                messageBoxWindow.getTextHandler += (int types) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
            btn_Home_Text.Text = extend.Language.Home;
            btn_Add_Text.Text = extend.Language.Add;
            btn_Shop_Text.Text = extend.Language.Shop;
            btn_Extend_Text.Text = extend.Language.Extend;
            btn_Feedback_Text.Text = extend.Language.Feedback;
            btn_Seting_Text.Text = extend.Language.Seting;
            sr.Close();
            con.Close();

            //委托检查更新和统计
            Task.Factory.StartNew(CheckUpdates);

        }
        //检查更新线程
        void CheckUpdates()
        {
            Task.Factory.StartNew(BeginCheckUpdates, this).Wait();
        }
        //检查更新线程
        private void BeginCheckUpdates(object obj)
        {
            MainWindow mw = obj as MainWindow;
            bool NewUpdate = false;

            try
            {
                string result = new extend.Network().Request("http://www.luobei.com/desktop/v1.common/statistics", "POST", "systemcode=" + new extend.GetSystemInfo().getRNum());
            }
            catch (Exception e) { }
            if (extend.SystemConfig.Update == 1)
            {

                try
                {
                    string result = new extend.Network().Request("http://www.luobei.com/desktop/v1.common/CheckUpdates", "POST", "systemcode=" + new extend.GetSystemInfo().getRNum());
                    try
                    {
                        JObject Data = (JObject)JsonConvert.DeserializeObject(result);
                        JObject DataInfo = (JObject)JsonConvert.DeserializeObject(Data["data"].ToString());

                        Version ThisVersion = new Version(extend.SystemConfig.Versions);
                        Version NewVersion = new Version(DataInfo["version"].ToString());
                        if (NewVersion > ThisVersion)
                        {
                            NewUpdate = true;

                        }

                    }
                    catch (Exception e) { }
                }
                catch (Exception e) { }
            }

            Task.Factory.StartNew(() => NewUpdateMaster(mw, NewUpdate), new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler).Wait();
        }
        //检查更新线程
        void NewUpdateMaster(MainWindow mw,bool NewUpdate)
        {
            if (NewUpdate)
            {
                mw.NewUpdateDialog();
            }
            
        }
        //检查更新线程
        void NewUpdateDialog()
        {
            MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
            messageBoxWindow.setInfo(extend.Language.Tips, extend.Language.NewUpdates, extend.Language.Cancel, extend.Language.Update, 130, 350);
            messageBoxWindow.getTextHandler += (int type) =>
            {
                if (type == 2)
                {
                    //先检查下载器是否有更新
                    try
                    {
                        string result = new extend.Network().Request("http://www.luobei.com/desktop/v1.common/getupdateapp", "POST", "systemcode=" + new extend.GetSystemInfo().getRNum());
                        JObject Data = (JObject)JsonConvert.DeserializeObject(result);
                        JObject DataInfo = (JObject)JsonConvert.DeserializeObject(Data["data"].ToString());
                        Version ThisVersion = new Version(extend.SystemConfig.Versions);
                        Version NewVersion = new Version(DataInfo["version"].ToString());

                        if (NewVersion > ThisVersion)
                        {
                            JArray jArray = JArray.Parse(DataInfo["data"].ToString());
                            List<extend.DownloadList> downloadLists = new List<extend.DownloadList>();
                            foreach (var JObject in jArray) downloadLists.Add(new extend.DownloadList(JObject["name"].ToString(), JObject["url"].ToString(), JObject["path"].ToString(), extend.Language.Downloading, 0));

                            messageBoxWindow = new MessageBoxWindow();
                            messageBoxWindow.setInfo(extend.Language.Tips, extend.Language.NewUpdateApp, "", extend.Language.Download, 130, 350,false);
                            messageBoxWindow.getTextHandler += (int toUpdateDwon) =>
                            {
                                DownloadWindow downloadWindow = new DownloadWindow();
                                downloadWindow.Show();
                                downloadWindow.SetDownloadList(downloadLists);
                                downloadWindow.getTextHandler += (bool Fail) =>
                                {
                                    if (!Fail)
                                    {
                                        if (!ToUpdate())
                                        {
                                            messageBoxWindow = new MessageBoxWindow();
                                            messageBoxWindow.setInfo(extend.Language.Tips, extend.Language.OpenDownloadFail, "", extend.Language.Ok, 130, 350, false);
                                            messageBoxWindow.getTextHandler += (int _) =>
                                            {
                                            };
                                            messageBoxWindow.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        messageBoxWindow = new MessageBoxWindow();
                                        messageBoxWindow.setInfo(extend.Language.Error, extend.Language.DownloadFail, "", extend.Language.Ok, 130, 350, false);
                                        messageBoxWindow.getTextHandler += (int _) =>
                                        {

                                        };
                                        messageBoxWindow.ShowDialog();
                                    }
                                };

                            };
                            messageBoxWindow.ShowDialog();
                        }
                        else {
                            if (!ToUpdate())
                            {
                                messageBoxWindow = new MessageBoxWindow();
                                messageBoxWindow.setInfo(extend.Language.Tips, extend.Language.NotUpdateApp, extend.Language.Cancel, extend.Language.Download, 130, 350);
                                messageBoxWindow.getTextHandler += (int toUpdate) =>
                                {
                                    if (toUpdate == 2)
                                    {
                                        //这里去下载更新程序
                                        //获取更新程序下载地址
                                        try
                                        {
                                            JArray jArray = JArray.Parse(DataInfo["data"].ToString());
                                            List<extend.DownloadList> downloadLists = new List<extend.DownloadList>();
                                            foreach (var JObject in jArray) downloadLists.Add(new extend.DownloadList(JObject["name"].ToString(), JObject["url"].ToString(), JObject["path"].ToString(),extend.Language.Downloading, 0));
                                            DownloadWindow downloadWindow = new DownloadWindow();
                                            downloadWindow.Show();
                                            downloadWindow.SetDownloadList(downloadLists);
                                            downloadWindow.getTextHandler += (bool Fail) =>
                                            {
                                                if (!Fail)
                                                {
                                                    if (!ToUpdate())
                                                    {
                                                        messageBoxWindow = new MessageBoxWindow();
                                                        messageBoxWindow.setInfo(extend.Language.Tips, extend.Language.OpenDownloadFail, "", extend.Language.Ok, 130, 350, false);
                                                        messageBoxWindow.getTextHandler += (int _) =>
                                                        {
                                                        };
                                                        messageBoxWindow.ShowDialog();
                                                    }
                                                }
                                                else
                                                {
                                                    messageBoxWindow = new MessageBoxWindow();
                                                    messageBoxWindow.setInfo(extend.Language.Error, extend.Language.DownloadFail, "", extend.Language.Ok, 130, 350, false);
                                                    messageBoxWindow.getTextHandler += (int _) =>
                                                    {

                                                    };
                                                    messageBoxWindow.ShowDialog();
                                                }
                                            };
                                        }
                                        catch (Exception ex)
                                        {
                                            messageBoxWindow = new MessageBoxWindow();
                                            messageBoxWindow.setInfo(extend.Language.Tips, extend.Language.OpenDownloadFail, "", extend.Language.Ok, 130, 350, false);
                                            messageBoxWindow.getTextHandler += (int _) =>
                                            {
                                            };
                                            messageBoxWindow.ShowDialog();
                                        }

                                    }
                                };
                                messageBoxWindow.ShowDialog();
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        messageBoxWindow = new MessageBoxWindow();
                        messageBoxWindow.setInfo(extend.Language.Tips, extend.Language.OpenDownloadFail, "", extend.Language.Ok, 130, 350, false);
                        messageBoxWindow.getTextHandler += (int _) =>
                        {
                        };
                        messageBoxWindow.ShowDialog();
                    }
                }
            };
            messageBoxWindow.ShowDialog();
        }
        bool ToUpdate()
        {
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            //设置外部程序名
            Info.FileName = "./Update/Update.exe";

            //设置外部程序的启动参数（命令行参数）为test.txt
            //Info.Arguments = "test.txt";
            //设置外部程序工作目录为C:\
            //Info.WorkingDirectory = "C:\\";
            //声明一个程序类
            System.Diagnostics.Process Proc;
            try
            {
                //启动外部程序//
                Proc = System.Diagnostics.Process.Start(Info);
                System.Environment.Exit(0);
                return true;
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                return false;
            }

        }
        //自启委托最小化更新UI
        private void SchedulerWork()
        {
            Task.Factory.StartNew(Begin, this).Wait();
        }
        //自启委托最小化更新UI
        private void Begin(object obj)
        {
            MainWindow mw = obj as MainWindow;
            Thread.Sleep(1000);
            Task.Factory.StartNew(() => UpdateUi(mw), new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler).Wait();
        }
        //自启委托最小化更新UI
        private void UpdateUi(MainWindow mw)
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
            TrayClickNum++;
            System.Timers.Timer t = new System.Timers.Timer(500);//实例化Timer类，设置间隔时间为10000毫秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler(((object source, System.Timers.ElapsedEventArgs es) => {
                TrayClickNum = 0;
            }));//到达时间的时候执行事件；
            t.AutoReset = false;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            if (TrayClickNum==2)
            {
                t.Dispose();
                TrayClickNum = 0;
                if (WindowState == WindowState.Minimized)
                    WindowState = WindowState.Normal;
                Show();
                Activate();
            }
            
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
                    mediaBackground.Init();
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
            Init();
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
            messageBoxWindow.setInfo(extend.Language.Tips, extend.Language.ComingSoon, "", extend.Language.Ok, 130, 350, false);
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
            messageBoxWindow.setInfo(extend.Language.Tips, extend.Language.ComingSoon, "", extend.Language.Ok, 130, 350, false);
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
            messageBoxWindow.setInfo(extend.Language.Tips, extend.Language.ComingSoon, "", extend.Language.Ok, 130, 350, false);
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
