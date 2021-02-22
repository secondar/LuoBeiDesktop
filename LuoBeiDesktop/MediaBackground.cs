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
            vlcControl.Left = 0;
            vlcControl.Top = 0;
            webBrowser.Left = 0;
            webBrowser.Top = 0;
            pictureBox.Left = 0;
            pictureBox.Top = 0;
            vlcControl.Height = this.Height;
            vlcControl.Width = this.Width;
            webBrowser.Height = this.Height;
            webBrowser.Width = this.Width;
            pictureBox.Height = this.Height;
            pictureBox.Width = this.Width;
            vlcControl.Visible = false;
            webBrowser.Visible = false;
            pictureBox.Visible = false;
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
            if (webBrowser.Visible)
            {
                webBrowser.Visible = false;
            }
            if(pictureBox.Visible) pictureBox.Visible = false;
            if(!vlcControl.Visible) vlcControl.Visible = true;
            if (!File.Exists(Pahtn)) new extend.ResultState(false, extend.Language.NoFile, 1);
            try
            {
                mediaPath = Pahtn;
                isNetWork = false;
                vlcControl.SetMedia(new System.IO.FileInfo(Pahtn));
                return new extend.ResultState(true, extend.Language.Ok, 0);
            }
            catch (Exception e)
            {
                return new extend.ResultState(false, extend.Language.Fail, 1);
            }
        }
        /// <summary>
        /// 设置播放源 _ 网络
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public extend.ResultState SetMediaNetwork(string url)
        {
            if (webBrowser.Visible)
            {
                webBrowser.Visible = false;
            }
            if (pictureBox.Visible) pictureBox.Visible = false;
            if (!vlcControl.Visible) vlcControl.Visible = true;

            extend.Network network = new extend.Network();
            if (!network.PingIpOrDomainName("www.baidu.com")) return new extend.ResultState(false, "没有网络连接", 1);
            try
            {
                mediaPath = url;
                isNetWork = true;
                vlcControl.SetMedia(new Uri(url));
                return new extend.ResultState(true, extend.Language.Ok, 0);

            }
            catch (Exception e)
            {
                return new extend.ResultState(false, extend.Language.Fail, 1);

            }
        }
        /// <summary>
        /// 设置图片背景
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public extend.ResultState SetImage(string Path)
        {
            if (webBrowser.Visible)
            {
                webBrowser.Visible = false;
            }
            if (!pictureBox.Visible) pictureBox.Visible = true;
            if (vlcControl.Visible) vlcControl.Visible = false;
            SetVoice(0);
            try
            {
                pictureBox.ImageLocation = Path;
                return new extend.ResultState(true, extend.Language.Ok, 0);

            }
            catch (Exception e)
            {
                return new extend.ResultState(false, extend.Language.Fail, 1);

            }

        }

        /// <summary>
        /// 设置图片背景
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public extend.ResultState SetWebPage(string Url)
        {
            if (!webBrowser.Visible) webBrowser.Visible = true;

            if (!pictureBox.Visible) pictureBox.Visible = false;
            if (vlcControl.Visible) vlcControl.Visible = false;
            SetVoice(0);
            try
            {
                webBrowser.Url = new Uri(Url);
                return new extend.ResultState(true, extend.Language.Ok, 0);

            }
            catch (Exception e)
            {
                return new extend.ResultState(false, extend.Language.Fail, 1);

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
            return new extend.ResultState(true, extend.Language.Ok, 0);
        }
    }
}
