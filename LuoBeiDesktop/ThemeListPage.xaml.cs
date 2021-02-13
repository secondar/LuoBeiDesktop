using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LuoBeiDesktop
{
    /// <summary>
    /// ThemeListPage.xaml 的交互逻辑
    /// </summary>
    public partial class ThemeListPage : Page
    {
        private MainWindow _parentWin;
        public MainWindow ParentWindow
        {
            get { return _parentWin; }
            set { _parentWin = value; }
        }
        public ThemeListPage()
        {
            InitializeComponent();
            Init();
        }

        private void MenuUse_Click(object sender, RoutedEventArgs e)
        {
            extend.ThemeList item = (extend.ThemeList)lb.SelectedItem;
            if (item == null) return;
            try
            {
                SQLiteConnection con = new extend.Common().NewSQLite();
                con.Open();
                SQLiteCommand com = new SQLiteCommand();
                com.Connection = con;
                string Tsql = "UPDATE \"themelist\" SET \"use\" = 0";
                com.CommandText = Tsql;
                int show = Convert.ToInt32(com.ExecuteScalar());
                con.Close();

                con.Open();
                com.Connection = con;
                Tsql = "UPDATE \"themelist\" SET \"use\" = 1 WHERE id=" + item.Id.ToString();
                com.CommandText = Tsql;
                show = Convert.ToInt32(com.ExecuteScalar());
                con.Close();

                ParentWindow.SetMediaBackground(item.Networks, item.Type, item.Path);


            }
            catch (Exception ex)
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Error", ex.Message, "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int type) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                extend.ThemeList item = (extend.ThemeList)lb.SelectedItem;
                if (item == null) return; 
                SQLiteConnection con = new extend.Common().NewSQLite();
                con.Open();
                SQLiteCommand com = new SQLiteCommand();
                com.Connection = con;
                string Tsql = "DELETE FROM themelist WHERE id = " + item.Id.ToString();
                com.CommandText = Tsql;
                int show = Convert.ToInt32(com.ExecuteScalar());
                con.Close();
                Init();
            }
            catch (Exception ex) {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Error", ex.Message, "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int type) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
        }
        public void Init()
        {
            try
            {
                List<extend.ThemeList> list = new List<extend.ThemeList>();
                SQLiteConnection con = new extend.Common().NewSQLite();
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM themelist";
                SQLiteDataReader sr = cmd.ExecuteReader();
                List<string> tables = new List<string>();
                while (sr.Read())
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

                    list.Add(new extend.ThemeList(id, name, thumbnail, path, type, networks, use, author, remarks, addtime));

                }
                sr.Close();
                con.Close();
                lb.ItemsSource = list;
            }catch(Exception e)
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Error", e.Message, "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int type) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
        }
    }
}
