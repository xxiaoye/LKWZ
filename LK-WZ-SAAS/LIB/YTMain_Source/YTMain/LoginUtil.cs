namespace YtMain
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using YtClient;
    using YtClient.data.events;
    using YtDict.update;
    using YTMain;
    using YtPlugin;
    using YtSys;
    using YtUtil.tool;

    public class LoginUtil
    {
        private static Image BackImg = null;
        public static int count;
        public static bool isJz;
        public static Label labelMsg;
        public static Thread load;
        public static bool LoadSuc;
        public static string MenuStr;
        public static Dictionary<string, System.Type> plugins = new Dictionary<string, System.Type>();
        public static ProgressBar progressBar1;
        public static Label UAlter;

        private static void ac_ServiceFaiLoad(object sender, LoadFaiEvent e)
        {
            if (e.Msg != null)
            {
                WJs.alert(e.Msg.Msg);
            }
            else
            {
                WJs.alert(e.ErrMsg);
            }
            labelMsg.Text = "连接服务器失败！";
        }

        private static void ac_ServiceLoad(object sender, LoadEvent e)
        {
            MenuStr = e.Msg.GetValue();
            LoadSuc = true;
            labelMsg.Text = "成功连接服务器！";
            if (SysSet.LoadSucEvent != null)
            {
                SysSet.LoadSucEvent(null, null);
            }
        }

        public static void AdminLogin(string userPwd)
        {
            Ui.User = new Sys();
            Ui.User.UserID = "admin";
            Ui.User.UserName = "超级用户";
            Ui.User.XName = "超级用户";
            Ui.User.DepCode = "0";
            Ui.User.DepName = "超级机构";
            Ui.User.DeptID = "0";
            Ui.User.DeptName = "";
            Ui.User.Useraccount = "admin";
            if (SysSet.LoginAdSuc != null)
            {
                SysSet.LoginAdSuc(null, null, userPwd, Ui.User);
            }
        }

        public static Image GetUrlImg(string img)
        {
            if (BackImg != null)
            {
                return BackImg;
            }
            string baseUrl = ActionLoad.GetBaseUrl();
            if ((baseUrl != null) && (baseUrl.IndexOf("gzwsxxh") < 0))
            {
                try
                {
                    baseUrl = baseUrl.Replace("OptService.svc", "") + img;
                    if (UrlFile.IsExist(baseUrl))
                    {
                        new WebClient().DownloadFile(baseUrl, "main.jpg");
                        BackImg = Image.FromFile("main.jpg");
                        return BackImg;
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static bool HaveRight(int i)
        {
            if (Ui.User.DepCode.Equals("0") && Ui.User.Useraccount.Equals("admin"))
            {
                return true;
            }
            if (SysSet.HaveRightEvent != null)
            {
                return SysSet.HaveRightEvent(i);
            }
            return Ui.HaveRight(i);
        }

        public static bool IsGradeRight()
        {
            return (Ui.User.DepCode.Equals("0") && Ui.User.Useraccount.Equals("admin"));
        }

        private static bool IsValidPlugin(System.Type t)
        {
            System.Type[] interfaces = t.GetInterfaces();
            foreach (System.Type type in interfaces)
            {
                if (type.FullName == "YtPlugin.IPlug")
                {
                    return true;
                }
            }
            return false;
        }

        public static void LoadAllPlugins()
        {
            Thread.Sleep(50);
            try
            {
                isJz = true;
                LoadSuc = false;
                string[] files = null;
                if (Directory.Exists(Application.StartupPath + @"\plugins"))
                {
                    files = Directory.GetFiles(Application.StartupPath + @"\plugins");
                }
                string str = WJs.AppStr("RootPlugs");
                if (!WJs.isNull(str))
                {
                    List<string> list = new List<string>();
                    string[] strArray2 = str.Split(new char[] { ',' });
                    foreach (string str2 in strArray2)
                    {
                        string path = Application.StartupPath + @"\" + str2 + ".dll";
                        if (System.IO.File.Exists(path))
                        {
                            list.Add(path);
                        }
                    }
                    if (list.Count > 0)
                    {
                        if (files != null)
                        {
                            foreach (string str4 in files)
                            {
                                list.Add(str4);
                            }
                        }
                        files = list.ToArray();
                    }
                }
                progressBar1.Maximum = files.Length;
                foreach (string str5 in files)
                {
                    if (str5.Substring(str5.LastIndexOf(".")) == ".dll")
                    {
                        try
                        {
                            System.Type[] types = Assembly.LoadFile(str5).GetTypes();
                            foreach (System.Type type in types)
                            {
                                RegPlugin(type);
                                if (IsValidPlugin(type))
                                {
                                    count++;
                                    progressBar1.Maximum++;
                                    plugins[type.FullName] = type;
                                    progressBar1.Value += progressBar1.Step;
                                    Mouse.Sleep(5);
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show("模块【" + str5.Substring(str5.LastIndexOf('\\') + 1) + "】实现的接口不是最新的版本！" + exception.Message, "加载插件出错", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                    }
                }
                progressBar1.Value = progressBar1.Maximum;
                labelMsg.Text = "模块加载完毕！";
                RegActivex();
                labelMsg.Text = "正在建立服务器连接，请稍等！";
                ActionLoad load = ActionLoad.Conn();
                load.Action = "TestCon";
                load.Add("mName", SysSet.MenuName);
                load.Add("EmName", SysSet.MenuName);
                load.ServiceLoad += new LoadEventHandle(LoginUtil.ac_ServiceLoad);
                load.ServiceFaiLoad += new LoadFaiEventHandle(LoginUtil.ac_ServiceFaiLoad);
                load.Post();
                if (UAlter != null)
                {
                    if (SysSet.UpdateAddress == null)
                    {
                        string str6 = ActionLoad.GetBaseUrl().Replace("http://", "");
                        str6 = str6.Substring(0, str6.IndexOf('/'));
                        str6 = "http://" + str6 + "/install";
                        UAlter.Text = "安装地址 " + str6;
                        SysSet.UpdateAddress = str6;
                    }
                    else
                    {
                        UAlter.Text = "安装地址 " + SysSet.UpdateAddress;
                    }
                }
            }
            catch (Exception exception2)
            {
                WJs.alert(exception2.Message + "::" + exception2.StackTrace);
            }
            LoginUtil.load.Abort();
        }

        public static void LoginSuc(LoadEvent e, string sjh, string userPwd)
        {
            DataTable dataTable;
            Ui.User = new Sys();
            if (!SysSet.MySelfRunLogin)
            {
                dataTable = e.Msg.GetDataTable("dataTwo");
                if ((dataTable != null) && (dataTable.Rows.Count > 0))
                {
                    Ui.Rights = e.Msg.GetDataTable();
                }
                Ui.UInfo = dataTable.Rows[0];
                Ui.User.UserID = e.Msg.GetValue(3) ?? "";
                Ui.User.UserName = e.Msg.GetValue(4) ?? "";
                Ui.User.XName = e.Msg.GetValue(5) ?? "";
                Ui.User.DepCode = e.Msg.GetValue(6) ?? "";
                Ui.User.DepName = e.Msg.GetValue(7) ?? "";
                Ui.User.DeptID = e.Msg.GetValue(8) ?? "";
                Ui.User.DeptName = e.Msg.GetValue(9) ?? "";
                Ui.User.Tel = sjh;
                Ui.User.Useraccount = e.Msg.GetValue(10) ?? "";
                if (WJs.isNull(Ui.User.Useraccount))
                {
                    Ui.User.Useraccount = Ui.User.UserName;
                }
                if (SysSet.LoginSuc != null)
                {
                    SysSet.LoginSuc(e, sjh, userPwd, Ui.User);
                }
            }
            else if (SysSet.LoginSuc != null)
            {
                dataTable = e.Msg.GetDataTable("dataTwo");
                if ((dataTable != null) && (dataTable.Rows.Count > 0))
                {
                    Ui.Rights = e.Msg.GetDataTable();
                }
                Ui.UInfo = dataTable.Rows[0];
                SysSet.LoginSuc(e, sjh, userPwd, Ui.User);
            }
            WJs.SerializeObject(Ui.User, "Login.dat");
        }

        private static void RegActivex()
        {
            string str = null;
            if (ConfigurationSettings.AppSettings["RegActivexControl"] != null)
            {
                str = ConfigurationSettings.AppSettings["RegActivexControl"].ToString();
            }
            if (((str != null) && (str.Trim().Length != 0)) && (str.IndexOf(',') >= 0))
            {
                string[] strArray = str.Split(new char[] { ';' });
                foreach (string str2 in strArray)
                {
                    Exception exception;
                    string[] strArray2 = str2.Split(new char[] { ',' });
                    labelMsg.Text = "正在检查[" + strArray2[0] + "]版本！";
                    RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"CLSID\{" + strArray2[1] + @"}\InprocServer32\");
                    Mouse.Sleep(100);
                    if (key == null)
                    {
                        try
                        {
                            labelMsg.Text = "正在注册[" + strArray2[0] + "]！";
                            WJs.RegDll(strArray2[2]);
                            Mouse.Sleep(0x3e8);
                            labelMsg.Text = "[" + strArray2[0] + "]注册成功！";
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            labelMsg.Text = "[" + strArray2[0] + "]注册失败！" + exception.Message;
                            break;
                        }
                    }
                    else
                    {
                        string startupPath = Application.StartupPath;
                        object obj2 = key.GetValue("");
                        if (obj2.ToString().ToLower().IndexOf(startupPath.ToLower()) < 0)
                        {
                            try
                            {
                                labelMsg.Text = "正在反注册[" + strArray2[0] + "]！";
                                WJs.UnRegDll(obj2.ToString());
                                Mouse.Sleep(0x3e8);
                                labelMsg.Text = "正在注册[" + strArray2[0] + "]！";
                                WJs.RegDll(strArray2[2]);
                                Mouse.Sleep(0x3e8);
                                labelMsg.Text = "[" + strArray2[0] + "]注册成功！";
                            }
                            catch (Exception exception2)
                            {
                                exception = exception2;
                                labelMsg.Text = "[" + strArray2[0] + "]注册失败！";
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static void RegPlugin(System.Type t)
        {
            System.Type[] interfaces = t.GetInterfaces();
            foreach (System.Type type in interfaces)
            {
                if (type.FullName == "YtPlugin.IPlugReg")
                {
                    IPlugReg reg = (IPlugReg) Activator.CreateInstance(t);
                    if (!reg.IsReg())
                    {
                        reg.Reg();
                    }
                    break;
                }
            }
        }

        public static string ShowText()
        {
            if (SysSet.BottomAlert != null)
            {
                return (SysSet.BottomAlert + "  当前版本：" + SysSet.GetVersiion() + SysSet.CopyRight);
            }
            return ("   当前单位：" + Ui.User.DepName + "，用户：" + Ui.User.XName + "，科室：" + Ui.User.DeptName + "  当前版本：" + SysSet.GetVersiion() + SysSet.CopyRight);
        }

        public static string SuoText()
        {
            return ("当前用户：" + Ui.User.XName);
        }

        public static void UpdateData()
        {
            UpdateData(null, null, null, null);
        }

        public static void UpdateData(EventHandler UpOk, ProgressBar progressBar, Label labMsg, IYtForm f)
        {
            if (WJs.IsClickOnceRun || SysSet.UpdateDataTest)
            {
                UpDateDict.UseSys = "ALL";
                UpDateDict.UserID = Ui.User.UserID ?? "";
                UpDateDict.DepCode = Ui.User.DepCode;
                UpDateDict.DbConn = SysSet.UpdateDbConn;
                if (UpOk == null)
                {
                    new SvrUpdate().ShowDialog();
                }
                else
                {
                    UpDateDict dict = new UpDateDict(f, progressBar, labMsg, UpOk);
                }
            }
            else if (UpOk != null)
            {
                UpOk(null, null);
            }
        }
    }
}

