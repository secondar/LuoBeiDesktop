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
        private string _SetAsDesktopText;
        private string _TeleteThemeText;
        public ThemeList(Int64 Id,string Name, string Thumbnail, string Path,int Type,bool Networks, bool Use, string Author, string Remarks, string Addtime,string SetAsDesktopText,string TeleteThemeText)
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
            this._SetAsDesktopText = SetAsDesktopText;
            this._TeleteThemeText = TeleteThemeText;
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
        public string SetAsDesktopText
        {
            get { return _SetAsDesktopText; }
            set { _SetAsDesktopText = value; }
        }
        public string TeleteThemeText
        {
            get { return _TeleteThemeText; }
            set { _TeleteThemeText = value; }
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
        private static string _Versions;
        private static int _Update;
        private static string _UpdateAppVersion;
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
        public static string Versions
        {
            get { return _Versions; }
            set { _Versions = value; }
        }
        public static int Update
        {
            get { return _Update; }
            set { _Update = value; }
        }
        public static string UpdateAppVersion
        {
            get { return _UpdateAppVersion; }
            set { _UpdateAppVersion = value; }
        }
    }

    public class DownloadList
{
        private string _Path;
        private string _Url;
        private string _FileName;
        private double _Ratio;
        private string _State;
        public DownloadList(string FileName,string Url, string Path, string State, double Ratio = 0)
        {
            this._Url = Url;
            this._Path = Path;
            this._FileName = FileName;
            this._Ratio = Ratio;
            this._State = State;
        }
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
        public double Ratio
        {
            get { return _Ratio; }
            set { _Ratio = value; }
        }
        public string State
        {
            get { return _State; }
            set { _State = value; }
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
        private static string _ThereIsAnError = "There is an error";
        private static string _SetAsDesktop = "Set as desktop ";
        private static string _DeleteTheme = "Delete";
        private static string _About = "About";
        private static string _Author = "Author:";
        private static string _Blog = "Blog:";
        private static string _Donation = "Donation:";
        private static string _SdkList = "SdkList:";
        private static string _Versions = "Versions:";
        private static string _Website = "Website:";
        private static string _ComingSoon = "Coming soon";
        private static string _OFF = "OFF";
        private static string _ON = "ON";
        private static string _RestartTotakeEffect = "Success update language Restart to take effect";
        private static string _Success = "Success";
        private static string _SaveFailed = "Save failed";
        private static string _Autoboot = "Autoboot:";
        private static string _Languages = "Language:";
        private static string _Volume = "Volume:";
        private static string _Save = "Save:";
        private static string _AddName = "Name:";
        private static string _AddName_Tip = "Theme Name（*must）";
        private static string _Thumbnail = "Thumbnail:";
        private static string _AddThumbnail_Tip = "(Optional)";
        private static string _Election = "election";
        private static string _Type = "Type:";
        private static string _AddType_Tip = "Select topic type（*must）";
        private static string _Path = "Path:";
        private static string _AddPath_Tip = "Topic file path or url（*must）";
        private static string _AddAuthor_Tip = "（Optional）";
        private static string _AddNotes = "Notes:";
        private static string _Notes_Tip = "Remarks(Optional)";
        private static string _LocalVideo = "Local video";
        private static string _NetworkVideo = "Network video";
        private static string _LocalImage = "Local image";
        private static string _InternetPictures = "Internet pictures";
        private static string _Webpage = "Webpage";
        private static string _CheckUpdates = "Check for updates:";
        private static string _Update = "Update";
        private static string _NewUpdates = "New version found, update?";
        private static string _Download = "Download";
        private static string _NotUpdateApp = "Update program not found, do you want to download?";
        private static string _FileName = "File name";
        private static string _Downloaded = "Downloaded";
        private static string _OpenDownloadFail = "Failed to open Updater!";
        private static string _NewUpdateApp = "The update program needs to be updated!";
        private static string _State = "State";
        private static string _Downloading = "Downloading";
        private static string _Fail = "Fail";
        private static string _NotFinished = "Not finished, please wait patiently";
        private static string _DownloadFail = "Download failed";
        
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
        public static string ThereIsAnError
        {
            get { return _ThereIsAnError; }
            set { _ThereIsAnError = value; }
        }
        public static string SetAsDesktop
        {
            get { return _SetAsDesktop; }
            set { _SetAsDesktop = value; }
        }
        public static string DeleteTheme
        {
            get { return _DeleteTheme; }
            set { _DeleteTheme = value; }
        }
        public static string About
        {
            get { return _About; }
            set { _About = value; }
        }
        public static string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }
        public static string Blog
        {
            get { return _Blog; }
            set { _Blog = value; }
        }
        public static string Donation
        {
            get { return _Donation; }
            set { _Donation = value; }
        }
        public static string SdkList
        {
            get { return _SdkList; }
            set { _SdkList = value; }
        }
        public static string Versions
        {
            get { return _Versions; }
            set { _Versions = value; }
        }
        public static string Website
        {
            get { return _Website; }
            set { _Website = value; }
        }
        public static string ComingSoon
        {
            get { return _ComingSoon; }
            set { _ComingSoon = value; }
        }
        public static string OFF
        {
            get { return _OFF; }
            set { _OFF = value; }
        }
        public static string ON
        {
            get { return _ON; }
            set { _ON = value; }
        }
        public static string RestartTotakeEffect
        {
            get { return _RestartTotakeEffect; }
            set { _RestartTotakeEffect = value; }
        }
        public static string Success
        {
            get { return _Success; }
            set { _Success = value; }
        }
        public static string SaveFailed
        {
            get { return _SaveFailed; }
            set { _SaveFailed = value; }
        }
        public static string Autoboot
        {
            get { return _Autoboot; }
            set { _Autoboot = value; }
        }
        public static string Languages
        {
            get { return _Languages; }
            set { _Languages = value; }
        }
        public static string Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }
        public static string Save
        {
            get { return _Save; }
            set { _Save = value; }
        }

        public static string AddName
        {
            get { return _AddName; }
            set { _AddName = value; }
        }
        public static string AddName_Tip
        {
            get { return _AddName_Tip; }
            set { _AddName_Tip = value; }
        }
        public static string Thumbnail
        {
            get { return _Thumbnail; }
            set { _Thumbnail = value; }
        }
        public static string AddThumbnail_Tip
        {
            get { return _AddThumbnail_Tip; }
            set { _AddThumbnail_Tip = value; }
        }
        public static string Election
        {
            get { return _Election; }
            set { _Election = value; }
        }
        public static string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public static string AddType_Tip
        {
            get { return _AddType_Tip; }
            set { _AddType_Tip = value; }
        }
        public static string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
        public static string AddPath_Tip
        {
            get { return _AddPath_Tip; }
            set { _AddPath_Tip = value; }
        }
        public static string AddAuthor_Tip
        {
            get { return _AddAuthor_Tip; }
            set { _AddAuthor_Tip = value; }
        }
        public static string AddNotes
        {
            get { return _AddNotes; }
            set { _AddNotes = value; }
        }
        public static string Notes_Tip
        {
            get { return _Notes_Tip; }
            set { _Notes_Tip = value; }
        }
        public static string LocalVideo
        {
            get { return _LocalVideo; }
            set { _LocalVideo = value; }
        }
        public static string NetworkVideo
        {
            get { return _NetworkVideo; }
            set { _NetworkVideo = value; }
        }
        public static string LocalImage
        {
            get { return _LocalImage; }
            set { _LocalImage = value; }
        }
        public static string InternetPictures
        {
            get { return _InternetPictures; }
            set { _InternetPictures = value; }
        }
        public static string Webpage
        {
            get { return _Webpage; }
            set { _Webpage = value; }
        }
        public static string CheckUpdates
        {
            get { return _CheckUpdates; }
            set { _CheckUpdates = value; }
        }
        public static string Update
        {
            get { return _Update; }
            set { _Update = value; }
        }
        public static string NewUpdates
        {
            get { return _NewUpdates; }
            set { _NewUpdates = value; }
        }
        public static string Download
        {
            get { return _Download; }
            set { _Download = value; }
        }
        public static string NotUpdateApp
        {
            get { return _NotUpdateApp; }
            set { _NotUpdateApp = value; }
        }
        public static string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        public static string Downloaded
        {
            get { return _Downloaded; }
            set { _Downloaded = value; }
        }
        public static string OpenDownloadFail
        {
            get { return _OpenDownloadFail; }
            set { _OpenDownloadFail = value; }
        }
        public static string NewUpdateApp
        {
            get { return _NewUpdateApp; }
            set { _NewUpdateApp = value; }
        }

        public static string State
        {
            get { return _State; }
            set { _State = value; }
        }
        public static string Downloading
        {
            get { return _Downloading; }
            set { _Downloading = value; }
        }
        public static string Fail
        {
            get { return _Fail; }
            set { _Fail = value; }
        }
        public static string NotFinished
        {
            get { return _NotFinished; }
            set { _NotFinished = value; }
        }
        public static string DownloadFail
        {
            get { return _DownloadFail; }
            set { _DownloadFail = value; }
        }
    }
}
