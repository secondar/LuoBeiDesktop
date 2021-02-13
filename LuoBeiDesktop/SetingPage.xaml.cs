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
    /// SetingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SetingPage : Page
    {
        private MainWindow _parentWin;
        public MainWindow ParentWindow
        {
            get { return _parentWin; }
            set { _parentWin = value; }
        }

        public SetingPage()
        {
            InitializeComponent();
            List<extend.ConfigRun> list = new List<extend.ConfigRun>();
            list.Add(new extend.ConfigRun(1, "Yes"));
            list.Add(new extend.ConfigRun(2, "No"));
            cb_Run.ItemsSource = list;
            if (extend.SystemConfig.Run == 1) cb_Run.SelectedIndex = 0;
            else cb_Run.SelectedIndex = 1;

            sl_volume.Value = extend.SystemConfig.Volume;

            try
            {
                List<extend.ConfigLanguage> list_Language = new List<extend.ConfigLanguage>();

                SQLiteConnection con = new extend.Common().NewSQLite();
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM languagelist";
                SQLiteDataReader sr = cmd.ExecuteReader();
                int index = 0;
                int itemIndex = 0;
                while (sr.Read())
                {
                    if (extend.SystemConfig.Language == sr["field"].ToString()) itemIndex = index;
                    else index++;
                    list_Language.Add(new extend.ConfigLanguage(sr["field"].ToString(), sr["title"].ToString()));
                }
                sr.Close();
                con.Close();
                cb_Language.ItemsSource = list_Language;
                cb_Language.SelectedIndex = itemIndex;

            }
            catch(Exception e)
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Error", "Initialization failed, error message:"+e.Message, "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int types) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }

        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                extend.ConfigRun Run = cb_Run.SelectedItem as extend.ConfigRun;
                extend.ConfigLanguage language = cb_Language.SelectedItem as extend.ConfigLanguage;
                int volume = Convert.ToInt32(Convert.ToDouble(sl_volume.Value));

                SQLiteConnection con = new extend.Common().NewSQLite();
                con.Open();
                SQLiteCommand com = new SQLiteCommand();
                com.Connection = con;
                string Tsql = "UPDATE \"system\" SET volume = " + volume.ToString() + " ,language='" + language.Field + "',run=" + Run.Type.ToString() + " WHERE id=1";
                com.CommandText = Tsql;
                Convert.ToInt32(com.ExecuteScalar());
                con.Close();
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Success", "Success update language Restart to take effect", "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int types) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Error", "Save failed, error message:" + ex.Message, "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int types) =>
                {

                };
                messageBoxWindow.ShowDialog();
            }
        }

        private void sl_volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                ParentWindow.SetMediaBackgroundPlayVolume(Convert.ToInt32(Convert.ToDouble(e.NewValue)));
            }catch(Exception ex) { }
        }
    }
}
