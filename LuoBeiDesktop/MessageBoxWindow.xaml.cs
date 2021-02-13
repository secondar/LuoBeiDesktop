using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// MessageBoxWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        public MessageBoxWindow()
        {
            InitializeComponent();
        }
        public delegate void GetTextHandler(int type);                       //声明委托,回复0=右上角关闭时间,1=按钮left,2=right
        public GetTextHandler getTextHandler;                                //委托对象

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            getTextHandler(0);
        }
        private void btn_Left_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            getTextHandler(1);
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
            this.Close();
            getTextHandler(2);
        }
        /// <summary>
        /// 设置消息框内容
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="btn_left">左边按钮</param>
        /// <param name="btn_Right">右边按钮</param>
        public void setInfo(string title,string content,string btn_left = "",string btn_Right = "",int thisHeight = 100,int thisWidth = 250,bool showLeft = true, bool showRight = true)
        {
            this.Height = thisHeight;
            this.Width = thisWidth;
            this.tbk_Right.Text = btn_Right;
            this.tbk_Left.Text = btn_left;
            this.tbk_Content.Text = content;
            this.tbk_Title.Text = title;
            if (!showLeft) this.btn_Left.Visibility = Visibility.Hidden;
            if (!showRight) this.btn_Right.Visibility = Visibility.Hidden;
        }
    }
}
