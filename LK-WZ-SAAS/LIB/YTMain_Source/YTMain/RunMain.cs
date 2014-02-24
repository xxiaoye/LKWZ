namespace YTMain
{
    using IWshRuntimeLibrary;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Windows.Forms;
    using YtMain;
    using YtUtil.tool;

    public class RunMain
    {
        private static void creatKjFs()
        {
            CreatUnInstallKJFS();
            if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + SysSet.SysName + ".appref-ms"))
            {
                System.IO.File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\程序\" + SysSet.SysPath + @"\" + SysSet.SysName + ".appref-ms", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + SysSet.SysName + ".appref-ms");
            }
        }

        private static void CreatUnInstallKJFS()
        {
            string path = Application.StartupPath + @"\DelApp.exe";
            if (!System.IO.File.Exists(path))
            {
                Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("YTMain.DelApp.exe");
                System.IO.File.WriteAllBytes(path, StreamToBytes(manifestResourceStream));
                if (manifestResourceStream != null)
                {
                    manifestResourceStream.Close();
                    manifestResourceStream.Dispose();
                }
                try
                {
                    if (!System.IO.File.Exists(Application.StartupPath + @"\DelApp.exe.config"))
                    {
                        string str2 = string.Concat(new object[] { "<?xml version=\"1.0\" encoding=\"utf-8\" ?><configuration><appSettings><add key=\"UnInstal\" value=\"", SysSet.SysName, "\" /><add key=\"PathName\" value=\"", SysSet.SysPath, "\" /><add key=\"InstallAddress\" value=\"", SysSet.UpdateAddress, "\" /><add key=\"IsRunUnInstall\" value=\"", SysSet.IsRunUnInstall ? 1 : 0, "\" />", (SysSet.InstallUrlTitle == null) ? "" : ("<add key=\"InstallUrlTitle\" value=\"" + SysSet.InstallUrlTitle + "\" />"), "</appSettings></configuration>" });
                        using (StreamWriter writer = new StreamWriter(Application.StartupPath + @"\DelApp.exe.config", false, Encoding.GetEncoding("utf-8")))
                        {
                            writer.Write(str2);
                            writer.Flush();
                            writer.Close();
                        }
                    }
                }
                catch
                {
                    WJs.alert("写入卸载配置文件失败！");
                }
                string fileName = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\程序\" + SysSet.SysPath + @"\" + SysSet.SysName + "卸载程序.lnk";
                FileInfo info = new FileInfo(fileName);
                if (info.Exists)
                {
                    System.IO.File.Delete(fileName);
                }
                WshShell shell = new WshShellClass();
                IWshShortcut shortcut = (IWshShortcut) shell.CreateShortcut(fileName);
                shortcut.TargetPath = Application.StartupPath + @"\DelApp.exe";
                shortcut.WorkingDirectory = Environment.CurrentDirectory;
                shortcut.WindowStyle = 1;
                shortcut.Description = SysSet.SysName;
                shortcut.IconLocation = Application.ExecutablePath;
                shortcut.Save();
            }
            if (SysSet.IsRunUnInstall)
            {
                Process.Start(Application.StartupPath + @"\DelApp.exe");
            }
        }

        public static void Main()
        {
            try
            {
                creatKjFs();
            }
            catch
            {
            }
            if (!SysSet.IsRunUnInstall)
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                }
                catch
                {
                }
                YtMain.Main main = new YtMain.Main();
                main.ShowDialog();
                if (main.Ok)
                {
                    if (SysSet.RunNewMain)
                    {
                        Application.Run(new SysMain());
                    }
                    else
                    {
                        Application.Run(new CHMain());
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Seek(0L, SeekOrigin.Begin);
            return buffer;
        }

        private static void Test()
        {
            Application.Run(new SysMain());
            Application.Exit();
        }

        public static void UMain(EventHandler InitControl)
        {
            try
            {
                creatKjFs();
            }
            catch
            {
            }
            if (!SysSet.IsRunUnInstall)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (InitControl != null)
                {
                    InitControl(null, null);
                }
                YtMain.UMain main = new YtMain.UMain();
                main.ShowDialog();
                if (main.Ok)
                {
                    if (SysSet.RunNewMain)
                    {
                        Application.Run(new SysMain());
                    }
                    else
                    {
                        Application.Run(new CHMain());
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}

