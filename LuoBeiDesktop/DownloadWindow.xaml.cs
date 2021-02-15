using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LuoBeiDesktop
{

    /// <summary>
    /// DownloadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadWindow : Window
    {
        List<extend.DownloadList> downloadLists = new List<extend.DownloadList>();

        public delegate void GetTextHandler(bool Fail);              //声明委托
        public GetTextHandler getTextHandler;                        //委托对象
        private double Ratio = 1;
        private bool Fail = false;
        private double Downloadeds = 0;
        private double downNum = 0;
        public DownloadWindow()
        {
            InitializeComponent();
            this.tbk_Title.Text = extend.Language.Download;
            FileName.Header = extend.Language.FileName;
            Downloaded.Header = extend.Language.Downloaded;
            State.Header = extend.Language.State;
            AllDownloadedTitle.Text = extend.Language.Downloaded;
            tbk_Right.Text = extend.Language.Confirm;
            
        }


        public void SetDownloadList(List<extend.DownloadList> Lists)
        {
            downloadLists = Lists;
            listView.ItemsSource = downloadLists;
            Ratio = 100.00 / downloadLists.Count;
            Thread thread = new Thread(Startdownloading);
            thread.IsBackground = true;
            thread.Start();
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

        private void btn_Right_Click(object sender, RoutedEventArgs e)
        {
            if (AllDownloaded.Value >= 100)
            {
                getTextHandler(Fail);
                this.Close();
            }
            else
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo(extend.Language.Error, extend.Language.NotFinished, "", extend.Language.Ok, 130, 350, false);
                messageBoxWindow.getTextHandler += (int _) =>
                {

                };
                messageBoxWindow.ShowDialog();

            }
        }
        void Startdownloading()
        {
            int index = 0;
            while (true)
            {
                if(downloadLists.Count -1 >= index && downNum < 2)
                {
                    Thread thread = new Thread(DowanlodFile);
                    thread.IsBackground = true;
                    thread.Start(index + "");
                    index++;
                    downNum++;
                    Console.WriteLine("次数"+downNum);
                }else if (downloadLists.Count - 1 <= index)
                {
                    Console.WriteLine("退出");
                    break;
                }
            }

        }
        void DowanlodFile(object obj)
        {

            string index = obj as string;
            new extend.Common().Mkdir(downloadLists[(int)Convert.ToInt64(index)].Path);
            //开始下载文件
            float percent = 0;
            try
            {
                //判断文件是否存在,存在就删除
                if (File.Exists(downloadLists[(int)Convert.ToInt64(index)].Path + downloadLists[(int)Convert.ToInt64(index)].FileName))
                {
                    System.IO.File.Delete(downloadLists[(int)Convert.ToInt64(index)].Path + downloadLists[(int)Convert.ToInt64(index)].FileName);
                }

                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(downloadLists[(int)Convert.ToInt64(index)].Url);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(downloadLists[(int)Convert.ToInt64(index)].Path + downloadLists[(int)Convert.ToInt64(index)].FileName, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);


                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    downloadLists[(int)Convert.ToInt64(index)].Ratio = percent;
                    this.listView.Dispatcher.Invoke(new Action(delegate ()
                        {
                            listView.ItemsSource = downloadLists;
                            ICollectionView view = CollectionViewSource.GetDefaultView(listView.ItemsSource);
                            view.Refresh();

                        }));
                }
                so.Close();
                st.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                downloadLists[(int)Convert.ToInt64(index)].State = extend.Language.Fail;
                this.listView.Dispatcher.Invoke(new Action(delegate ()
                {
                    listView.ItemsSource = downloadLists;
                }));
                Fail = true;
            }

            this.AllDownloaded.Dispatcher.Invoke(new Action(delegate ()
            {
                AllDownloaded.Value += Ratio;
            }));
            Downloadeds += Ratio;
            if (Downloadeds >= 100)
            {
                this.btn_Right.Dispatcher.Invoke(new Action(delegate ()
                {
                    btn_Right.IsEnabled = true;
                }));
            }
            downNum--;
        }
    }
}
