using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoBeiDesktop.extend
{
    public class Common
    {
        public SQLiteConnection NewSQLite()
        {
            if (!File.Exists(@"Config\DataBase.db"))
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
                messageBoxWindow.setInfo("Error", @"Configuration file does not exist, path config\ DataBase.db!", "", "OK", 130, 350, false);
                messageBoxWindow.getTextHandler += (int type) =>
                {

                };
                messageBoxWindow.ShowDialog();
                return null;
            }
            SQLiteConnection con = new SQLiteConnection();
            con.ConnectionString = @"Data Source=Config\DataBase.db;Pooling=true;FailIfMissing=false";
            return con;
        }
    }
}
