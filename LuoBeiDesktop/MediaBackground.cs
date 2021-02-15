using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuoBeiDesktop
{
    public partial class MediaBackground : Form
    {
        private string mediaPath = "";
        private bool isNetWork = false;
        public MediaBackground()
        {
            InitializeComponent();
        }
        public void Init()
        {
            this.Left = 0;
            this.Top = 0;
            this.Width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
            vlcControl.Height = this.Height;
            vlcControl.Width = this.Width;
        }
        private void vlcControl_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;

            if (currentDirectory == null)
                return;
            if (IntPtr.Size == 4)
                e.VlcLibDirectory = new DirectoryInfo(System.IO.Path.GetFullPath(@".\libvlc\win-x86\"));
            else
                e.VlcLibDirectory = new DirectoryInfo(System.IO.Path.GetFullPath(@".\libvlc\win-x64\"));
        }


        /// <summary>
        /// 设置播放源 _ 本地
        /// </summary>
        /// <param name="Pahtn">路径,支持URL</param>
        /// <returns></returns>
        /// 
        public extend.ResultState SetMediaLocal(string Pahtn)
        {
            if (!File.Exists(Pahtn)) new extend.ResultState(false, "文件不存在", 1);
            try
            {
                mediaPath = Pahtn;
                isNetWork = false;
                vlcControl.SetMedia(new System.IO.FileInfo(Pahtn));
                return new extend.ResultState(true, "成功", 1);
            }
            catch (Exception e)
            {
                return new extend.ResultState(false, "设置本地播放源失败,错误信息:" + e.Message, 1);
            }
        }
        /// <summary>
        /// 设置播放源 _ 网络
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public extend.ResultState SetMediaNetwork(string url)
        {
            extend.Network network = new extend.Network();
            if (!network.PingIpOrDomainName("www.baidu.com")) return new extend.ResultState(false, "没有网络连接", 1);
            //if (!network.UrlIsExist(url)) return new extend.ResultState(false, "无法访问网络媒体路径", 1);
            try
            {
                mediaPath = url;
                isNetWork = true;
                vlcControl.SetMedia(new Uri(url));
                return new extend.ResultState(true, "成功", 1);
            }
            catch (Exception e)
            {
                return new extend.ResultState(false, "设置网络播放源失败,错误信息:" + e.Message, 1);
            }
        }

        /// <summary>
        /// 播放视频
        /// </summary>
        /// <returns></returns>
        public extend.ResultState Play()
        {
            try
            {
                vlcControl.Play();
                //播放结束监听
                vlcControl.EndReached += (sender, e) =>
                {

                    Task.Factory.StartNew(() =>
                    {
                        vlcControl.BeginInvoke(new Action(() =>
                        {
                            if (isNetWork)
                            {
                                SetMediaNetwork(mediaPath);
                            }
                            else
                            {
                                SetMediaLocal(mediaPath);
                            }
                            Play();
                        }));
                    });
                };
                //播放进度监听
                vlcControl.PositionChanged += (sender, e) =>
                {
                };
                return new extend.ResultState(true, "成功", 1);
            }
            catch (Exception e)
            {
                return new extend.ResultState(false, "播放失败,错误信息:" + e.Message, 1);
            }
        }

        public extend.ResultState SetVoice(int voice)
        {
            vlcControl.Audio.Volume = voice;
            return new extend.ResultState(true, "成功", 0);
        }
    }
}
