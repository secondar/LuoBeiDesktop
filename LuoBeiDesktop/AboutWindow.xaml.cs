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
    /// AboutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            this.Title = extend.Language.About;
            tbk_Author.Text = extend.Language.Author;
            tbk_Blog.Text = extend.Language.Blog;
            tbk_Donation.Text = extend.Language.Donation;
            tbk_SdkList.Text = extend.Language.SdkList;
            tbk_Version.Text = extend.Language.Versions;
            tbk_Website.Text = extend.Language.Website;
            tbk_Versions.Text = extend.SystemConfig.Versions;
        }
    }
}
