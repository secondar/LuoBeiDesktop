using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LuoBeiDesktop
{
    /// <summary>
    /// AddThemePage.xaml 的交互逻辑
    /// </summary>
    public partial class AddThemePage : Page
    {
        private string fileType = "file|*.AVI;*.VCD;*.SVCD;*.DVD;*.MPG;*.ASF;*.WMV;*.RM;*.RMVB;*.MOV;*.QT;*.MP4;*.3GP;*.SDP;*.YUV";
        public AddThemePage()
        {
            InitializeComponent();
            btn_Path.Visibility = Visibility.Hidden;
            List<extend.ThemeType> list = new List<extend.ThemeType>();
            list.Add(new extend.ThemeType(1, extend.Language.LocalVideo));
            list.Add(new extend.ThemeType(2, extend.Language.NetworkVideo));
            list.Add(new extend.ThemeType(3, extend.Language.LocalImage));
            list.Add(new extend.ThemeType(4, extend.Language.InternetPictures));
            list.Add(new extend.ThemeType(5, extend.Language.Webpage));
            cb_Type.ItemsSource = list;



            tbk_Name.Text = extend.Language.AddName;
            tbk_Name_Tip.Text = extend.Language.AddName_Tip;
            tbk_Thumbnail.Text = extend.Language.Thumbnail;
            tbk_Thumbnail_Tip.Text = extend.Language.AddThumbnail_Tip;
            btn_tbk_Thumbnail_Election.Text = extend.Language.Election;
            tbk_Type.Text = extend.Language.Type;
            tbk_Type_Tip.Text = extend.Language.AddType_Tip;
            tbk_Path.Text = extend.Language.Path;
            btn_tbk_Path_Election.Text = extend.Language.Election;
            tbk_Path_Tip.Text = extend.Language.AddPath_Tip;
            tbk_Author.Text = extend.Language.Author;
            tbk_Author_Tip.Text = extend.Language.AddAuthor_Tip;
            tbk_Notes.Text = extend.Language.AddNotes;
            tbk_Notes_Tip.Text = extend.Language.Notes_Tip;
            btn_tbk_Save.Text = extend.Language.Save;

        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow messageBoxWindow = null;
            if (tb_Name.Text.Trim(' ') == "")
            {
                messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Error", "Please enter a topic name!", "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int type) =>
                {

                };
                messageBoxWindow.ShowDialog();
                return;
            }
            
            if (tb_Path.Text.Trim(' ') == "")
            {
                messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Error", "Please select or select a theme file!", "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int type) =>
                {

                };
                messageBoxWindow.ShowDialog();
                return;
            }
            if (cb_Type.SelectedItem == null)
            {
                messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Error", "Please select topic type!", "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int type) =>
                {

                };
                messageBoxWindow.ShowDialog();
                return;
            }
            extend.ThemeType Type = cb_Type.SelectedItem as extend.ThemeType;
            //随机生成一个ID
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string id = Convert.ToInt64(ts.TotalSeconds).ToString();

            try
            {
                SQLiteConnection con = new extend.Common().NewSQLite();
                con.Open();
                SQLiteCommand com = new SQLiteCommand();
                com.Connection = con;
                //string Tsql = "insert into themelist (id=@Id,name=@Name,thumbnail=@Thumbnail,path=@Path,type=@Type,networks=@Networks,use=@Use,author=@Author,remarks=@Remarks,addtime=@Addtime);";
                //com.CommandText = Tsql;
                //com.Parameters.AddWithValue("Id", id);
                //com.Parameters.AddWithValue("Name", tb_Name.Text.Trim(' '));
                //com.Parameters.AddWithValue("Thumbnail", tb_thumbnail.Text.Trim(' '));
                //com.Parameters.AddWithValue("Path", tb_Path.Text.Trim(' '));
                //com.Parameters.AddWithValue("Type", Type.Type);
                //com.Parameters.AddWithValue("networks", Type.Type == 2 || Type.Type == 4 ? 1 : 0);
                //com.Parameters.AddWithValue("Use", 0);
                //com.Parameters.AddWithValue("Author", tb_Atuhor.Text.Trim(' ') != "" ? tb_Atuhor.Text.Trim(' ') : "NULL");
                //com.Parameters.AddWithValue("Remarks", tb_Remarks.Text.Trim(' '));
                //com.Parameters.AddWithValue("Addtime", id);
                string thumbnail = tb_thumbnail.Text.Trim(' ') != "" ? tb_thumbnail.Text.Trim(' ') : AppDomain.CurrentDomain.BaseDirectory + "Resources\\Default.jpg";
                int networks = Type.Type == 2 || Type.Type == 4 ? 1 : 0;
                string Atuhor = tb_Atuhor.Text.Trim(' ') != "" ? tb_Atuhor.Text.Trim(' ') : "NULL";
                string Tsql = "INSERT INTO \"themelist\"(\"id\", \"name\", \"thumbnail\", \"path\", \"type\", \"networks\", \"use\", \"author\", \"remarks\", \"addtime\") VALUES";
                Tsql += "(" + id + ", '" + tb_Name.Text.Trim(' ') + "', '" + tb_thumbnail.Text.Trim(' ') + "', '" + tb_Path.Text.Trim(' ') + "', " + Type.Type;
                Tsql += ", " + networks + ", 0, '" + Atuhor + "', '" + tb_Remarks.Text.Trim(' ') + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "');";
                
                com.CommandText = Tsql;
                int show = Convert.ToInt32(com.ExecuteScalar());
                con.Close();
                //重置表单

                List<extend.ThemeType> list = new List<extend.ThemeType>();
                list.Add(new extend.ThemeType(1, "Local video"));
                list.Add(new extend.ThemeType(2, "Network video"));
                list.Add(new extend.ThemeType(3, "Local image"));
                list.Add(new extend.ThemeType(4, "Internet pictures"));
                list.Add(new extend.ThemeType(5, "Webpage"));
                cb_Type.ItemsSource = list;

                tb_Name.Text = "";
                tb_thumbnail.Text = "";
                tb_Path.Text = "";
                tb_Atuhor.Text = "";
                tb_Remarks.Text = "";

                btn_Path.Visibility = Visibility.Hidden;

                ///
                messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("success", @"success", "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int type) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Error", ex.Message, "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int type) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
}

        private void btn_thumbnail_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "Please select a thumbnail";
            dialog.Filter = "image|*.jpg;*.gif;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb_thumbnail.Text = dialog.FileName;
            }
        }

        private void cb_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((extend.ThemeType)cb_Type.SelectedItem == null) return;
            extend.ThemeType item = (extend.ThemeType)cb_Type.SelectedItem;
            if (item.Type == 1|| item.Type == 3) btn_Path.Visibility = Visibility.Visible;
            else btn_Path.Visibility = Visibility.Hidden;
            if (item.Type == 1) fileType = "file|*.AVI;*.VCD;*.SVCD;*.DVD;*.MPG;*.ASF;*.WMV;*.RM;*.RMVB;*.MOV;*.QT;*.MP4;*.3GP;*.SDP;*.YUV";
            else fileType = "image|*.jpg;*.gif;*.bmp";
        }

        private void btn_Path_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "Please select media or picture file";
            dialog.Filter = fileType;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb_Path.Text = dialog.FileName;
            }
        }
    }
}
