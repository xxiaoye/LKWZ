namespace YTMain
{
    using System;
    using System.Collections.Generic;
    using System.Deployment.Application;
    using System.Drawing;
    using System.Windows.Forms;
    using YtMain;

    public class SysSet
    {
        public static string AlertDbConn = null;
        public static List<YwAlert> AlertList = new List<YwAlert>();
        public static string BottomAlert = null;
        public static List<UrlLink> BottomUrls = new List<UrlLink>();
        public static bool CheckSuo = false;
        public static string CopyRight = "";
        public static int DepartCodeBlLength = 8;
        public static string ExeName = "YTPriSys";
        public static string ExitAlert = "确定退出系统吗？";
        public static HaveRightHandle HaveRightEvent;
        public static bool HideSysMenu = false;
        public static string IndexPage = "YTMain.Index";
        public static string InitPlug = null;
        public static InitRepHandle InitRep;
        public static string InstallUrlTitle = null;
        public static bool IsDepartCodeBl = false;
        public static bool IsRunStart = false;
        public static bool IsRunUnInstall = false;
        public static bool IsUpdate = false;
        public static string LinkQQ = "企业QQ:  316212393";
        public static string LinkTel = "24小时免费服务电话:";
        public static EventHandler LoadSucEvent;
        public static LoginSucHandle LoginAdSuc;
        public static string LoginDepViewText = "卫生机构";
        public static EventHandler LoginEvent;
        public static string LoginFindDepart = "LoginFindDepart";
        public static string LoginFindUser = "LoginFindUser";
        public static ILoginForm LoginForm;
        public static int LoginHeight = 290;
        public static Image LoginImg;
        public static string LoginInc = "Login";
        public static LoginSucHandle LoginSuc;
        public static string LoginUserViewText = "用 户 名";
        public static int LoginWidth = 500;
        public static Icon MainIco = null;
        public static Image MainImg;
        public static string MainTitle = "信息管理系统";
        public static string MenuName = "menuinfo";
        public static int MenuWidth = 220;
        public static bool MySelfRunLogin = false;
        public static Icon NotifyIco = null;
        public static bool NotifyShow = false;
        public static string NotifyText = "信息管理系统";
        public static bool PtLogin = true;
        public static LinkLabel RegLink;
        public static bool RunNewMain = true;
        public static bool SjLogin = false;
        public static string SysLoginInc = "SysLogin";
        public static string SysName;
        public static string SysPath;
        public static Image TopImg;
        public static string UpdateAddress = null;
        public static bool UpdateDataTest = false;
        public static string UpdateDbConn = null;
        public static bool UpdateOpenWin = true;
        public static Point UserInputLoc = new Point(0x18, 0x36);

        public static string GetVersiion()
        {
            string str = "测试版本";
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment currentDeployment = ApplicationDeployment.CurrentDeployment;
                string str2 = currentDeployment.CurrentVersion.Major.ToString();
                string str3 = currentDeployment.CurrentVersion.Minor.ToString();
                string str4 = currentDeployment.CurrentVersion.Revision.ToString();
                string str5 = currentDeployment.CurrentVersion.Build.ToString();
                str = str2 + "." + str3 + "." + str5 + "." + str4;
            }
            return str;
        }

        public static YTMain.SetLocakDataContrl SetLocakDataContrl
        {
            set
            {
                Main.SetLocakDataContrl = value;
            }
        }
    }
}

