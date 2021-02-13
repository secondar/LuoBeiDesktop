using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoBeiDesktop.extend
{
    /// <summary>
    /// 统一状态反馈
    /// </summary>
    public class ResultState
    {
        private bool _ok;
        private string _msg;
        private int _code;
        public ResultState(bool Ok,string Msg="",int Code = 0)
        {
            this._ok = Ok;
            this._msg = Msg;
            this._code = Code;
        }
        public bool Ok
        {
            get { return _ok; }
            set { _ok = value; }
        }
        public string Msg
        {
            get { return _msg; }
            set { _msg = value; }
        }
        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }
    }
    /// <summary>
    /// 视频状态反馈
    /// </summary>
    public class BackgroundState
    {
        private bool _init;
        private int _type;
        public BackgroundState(bool Init , int Type)
        {
            this._init = Init;
            this._type = Type;
        }
        public bool Init
        {
            get { return _init; }
            set { _init = value; }
        }
        /// <summary>
        /// Type  0= 未初始化 1 = 视频
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
    public class ThemeList
    {
        private Int64 _Id;
        private string _Name;
        private string _Thumbnail;
        private string _Path;
        private int _Type;
        private bool _Networks;
        private bool _Use;
        private string _Author;
        private string _Remarks;
        private string _Addtime;
        public ThemeList(Int64 Id,string Name, string Thumbnail, string Path,int Type,bool Networks, bool Use, string Author, string Remarks, string Addtime)
        {
            this._Id = Id;
            this._Name = Name;
            this._Thumbnail = Thumbnail;
            this._Path = Path;
            this._Type= Type;
            this._Networks = Networks;
            this._Use = Use;
            this._Author= Author;
            this._Remarks= Remarks;
            this._Addtime = Addtime;
        }
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Thumbnail
        {
            get { return _Thumbnail; }
            set { _Thumbnail = value; }
        }

        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public bool Use
        {
            get { return _Use; }
            set { _Use = value; }
        }
        public bool Networks
        {
            get { return _Networks; }
            set { _Networks = value; }
        }
        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        public string Addtime
        {
            get { return _Addtime; }
            set { _Addtime = value; }
        }
    }
    public class ThemeType
    {
        private int _Type;
        private string _Name;
        public ThemeType(int Type, string Name)
        {
            this._Type = Type;
            this._Name = Name;
        }
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }

    public class ConfigRun
    {
        private int _Type;
        private string _Name;
        public ConfigRun(int Type, string Name)
        {
            this._Type = Type;
            this._Name = Name;
        }
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
    public class ConfigLanguage
    {
        private string _Field;
        private string _Title;
        public ConfigLanguage(string Field, string Title)
        {
            this._Field = Field;
            this._Title = Title;
        }
        public string Field
        {
            get { return _Field; }
            set { _Field = value; }
        }
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
    }

    public static class Minimized
    {
        private static bool _Minimized;
  
        public static bool Minimizeds
        {
            get { return _Minimized; }
            set { _Minimized = value; }
        }
    }

    public static class SystemConfig
    {
        private static int _Id;
        private static int _Volume;
        private static string _Language;
        private static int _Run;

        public static int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public static int Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }
        public static string Language
        {
            get { return _Language; }
            set { _Language = value; }
        }
        public static int Run
        {
            get { return _Run; }
            set { _Run = value; }
        }
    }

    public static class Language
    {
        private static string _Home = "Home";
        private static string _Add = "Add";
        private static string _Shop = "Shop";
        private static string _Extend = "Extend";
        private static string _Feedback = "Feedback";
        private static string _Seting = "Seting";
        private static string _Error = "Error";
        private static string _GetConfFail = "Configuration acquisition failed, please restart the program!";
        private static string _GetLanguageFail = "Language information acquisition may fail, default to English!";
        private static string _NotThemeConf = "Topic configuration data not found";
        private static string _InitFail = "initialization failed!";
        private static string _Ok = "Ok";
        private static string _MainWindow = "MainWindow";
        private static string _CloseWallpaper = "CloseWallpaper";
        private static string _OpenWallpaper = "OpenWallpaper";
        private static string _Exit = "Exit";
        private static string _NumberedMode = "Not supported at present, only local video and network video are supported at present";
        private static string _InitWallpaperFail = "Wallpaper initialization failed";
        private static string _InitWallpaperSuccess = "Initialize wallpaper successfully";
        private static string _InitPlayerFail = "Player initialization failed";
        private static string _InitPlayerSuccess = "Player initialization successful";
        private static string _ExitMsg = "You are closing the Luobei desktop. Click OK to reduce it to the tray position. You can double-click to open the window again and right-click to display the menu. If you click Cancel, you will exit the Luobei desktop!";
        private static string _Tips = "Tips";
        private static string _Confirm = "Confirm";
        private static string _Cancel = "Cancel";
        public static string Home
        {
            get { return _Home; }
            set { _Home = value; }
        }
        public static string Add
        {
            get { return _Add; }
            set { _Add = value; }
        }
        public static string Shop
        {
            get { return _Shop; }
            set { _Shop = value; }
        }
        public static string Extend
        {
            get { return _Extend; }
            set { _Extend = value; }
        }
        public static string Feedback
        {
            get { return _Feedback; }
            set { _Feedback = value; }
        }
        public static string Seting
        {
            get { return _Seting; }
            set { _Seting = value; }
        }
        public static string Error
        {
            get { return _Error; }
            set { _Error = value; }
        }
        public static string NotThemeConf
        {
            get { return _NotThemeConf; }
            set { _NotThemeConf = value; }
        }
        public static string GetConfFail
        {
            get { return _GetConfFail; }
            set { _GetConfFail = value; }
        }
        public static string Ok
        {
            get { return _Ok; }
            set { _Ok = value; }
        }
        public static string GetLanguageFailk
        {
            get { return _GetLanguageFail; }
            set { _GetLanguageFail = value; }
        }
        public static string InitFail
        {
            get { return _InitFail; }
            set { _InitFail = value; }
        }
        public static string MainWindow
        {
            get { return _MainWindow; }
            set { _MainWindow = value; }
        }
        public static string CloseWallpaper
        {
            get { return _CloseWallpaper; }
            set { _CloseWallpaper = value; }
        }
        public static string OpenWallpaper
        {
            get { return _OpenWallpaper; }
            set { _OpenWallpaper = value; }
        }
        public static string Exit
        {
            get { return _Exit; }
            set { _Exit = value; }
        }
        public static string NumberedMode
        {
            get { return _NumberedMode; }
            set { _NumberedMode = value; }
        }
        public static string InitWallpaperFail
        {
            get { return _InitWallpaperFail; }
            set { _InitWallpaperFail = value; }
        }
        public static string InitWallpaperSuccess
        {
            get { return _InitWallpaperSuccess; }
            set { _InitWallpaperSuccess = value; }
        }
        public static string InitPlayerFail
        {
            get { return _InitPlayerFail; }
            set { _InitPlayerFail = value; }
        }
        public static string InitPlayerSuccess
        {
            get { return _InitPlayerSuccess; }
            set { _InitPlayerSuccess = value; }
        }
        public static string ExitMsg
        {
            get { return _ExitMsg; }
            set { _ExitMsg = value; }
        }
        public static string Tips
        {
            get { return _Tips; }
            set { _Tips = value; }
        }
        public static string Confirm
        {
            get { return _Confirm; }
            set { _Confirm = value; }
        }
        public static string Cancel
        {
            get { return _Cancel; }
            set { _Cancel = value; }
        }
    }

}
