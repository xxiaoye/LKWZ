namespace YtMain
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using YtClient;
    using YtClient.data.events;
    using YTMain;
    using YtSys;
    using YtUtil.tool;

    public class UMain : YtForm
    {
        private IContainer components = null;
        private bool isJz = false;
        public bool isSelUser;
        private Label label4;
        private Label label7;
        private Label label8;
        private Label labelMsg;
        private Thread load;
        public static bool LoadSuc = false;
        public bool Ok = false;
        private ProgressBar progressBar1;
        private string sjh = "";
        private System.Windows.Forms.Timer timer1;
        public static string ulogin = "";
        private string userid = "";
        public static string userPwd;

        public UMain()
        {
            this.InitializeComponent();
            base.Width = SysSet.LoginWidth;
            base.Height = SysSet.LoginHeight;
            Control.CheckForIllegalCrossThreadCalls = false;
            if (SysSet.LoginImg != null)
            {
                this.BackgroundImage = SysSet.LoginImg;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            SendKeys.Send("{Tab}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(UMain));
            this.progressBar1 = new ProgressBar();
            this.labelMsg = new Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            base.SuspendLayout();
            this.progressBar1.Dock = DockStyle.Bottom;
            this.progressBar1.Location = new Point(0, 0xf1);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x29d, 0x11);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 0;
            this.labelMsg.AutoSize = true;
            this.labelMsg.BackColor = Color.Transparent;
            this.labelMsg.Location = new Point(5, 0xe2);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new Size(0x83, 12);
            this.labelMsg.TabIndex = 1;
            this.labelMsg.Text = "请稍等，模块载入中...";
            this.timer1.Enabled = true;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.label4.BackColor = Color.Transparent;
            this.label4.Location = new Point(0x15, -2);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x17);
            this.label4.TabIndex = 5;
            this.label4.Click += new EventHandler(this.label4_Click);
            this.label7.AutoSize = true;
            this.label7.BackColor = Color.Transparent;
            this.label7.ForeColor = SystemColors.ActiveCaptionText;
            this.label7.Location = new Point(0x2d, 9);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0, 12);
            this.label7.TabIndex = 6;
            this.label8.BackColor = Color.Transparent;
            this.label8.Location = new Point(0x3e, 3);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x10, 15);
            this.label8.TabIndex = 7;
            this.label8.Click += new EventHandler(this.label8_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.ClientSize = new Size(0x29d, 0x102);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.labelMsg);
            base.Controls.Add(this.progressBar1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "UMain";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "系统登录";
            base.Load += new EventHandler(this.Main_Load);
            base.Activated += new EventHandler(this.Main_Activated);
            base.FormClosed += new FormClosedEventHandler(this.Main_FormClosed);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (ActionLoad.QxErr)
            {
                ActionLoad.QxErr = false;
                this.label7.Text = "";
            }
            else
            {
                ActionLoad.QxErr = true;
                this.label7.Text = "详细错误提示已开启";
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            CHMain.IsShowMInput = true;
        }

        private void load_ServiceFaiLoad(object sender, LoadFaiEvent e)
        {
            base.Invoke((, ) => SysSet.LoginForm.LoginFai(e.Msg));
        }

        private void load_ServiceLoad(object sender, LoadEvent e)
        {
            EventHandler ms = null;
            if (e.Msg.Success)
            {
                if (ms == null)
                {
                    ms = delegate {
                        EventHandler upOk = null;
                        this.isSelUser = false;
                        LoginUtil.LoginSuc(e, this.sjh, userPwd);
                        DateTime time = DateTime.Parse(e.Msg.GetValue(2).Trim());
                        TimeSpan span = (TimeSpan) (time - DateTime.Now);
                        if (Math.Abs(span.TotalMinutes) > 2.0)
                        {
                            try
                            {
                                short y = short.Parse(time.Year);
                                short m = short.Parse(time.Month);
                                short d = short.Parse(time.Day);
                                short h = short.Parse(time.Hour);
                                short mi = short.Parse(time.Minute);
                                Cmd.SetSysTime(y, m, d, h, mi, short.Parse(time.Second));
                            }
                            catch
                            {
                                WJs.alert("同步本机时间与服务器时间失败，请手动更改本机时间！\n服务器当前时间为【" + time.ToString("yyyy-MM-dd HH:ss") + "】");
                                return;
                            }
                        }
                        SysSet.LoginForm.LoginSuc(e.Msg);
                        if (SysSet.IsUpdate)
                        {
                            if (SysSet.UpdateOpenWin)
                            {
                                LoginUtil.UpdateData();
                                this.Ok = true;
                                this.Close();
                            }
                            else
                            {
                                if (upOk == null)
                                {
                                    upOk = delegate {
                                        this.Ok = true;
                                        this.Close();
                                    };
                                }
                                LoginUtil.UpdateData(upOk, this.progressBar1, this.labelMsg, this);
                            }
                        }
                        else
                        {
                            this.Ok = true;
                            this.Close();
                        }
                    };
                }
                base.Invoke(ms);
            }
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            if (!this.isJz)
            {
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                if (!LoadSuc)
                {
                    this.load = new Thread(new ThreadStart(LoginUtil.LoadAllPlugins));
                    LoginUtil.progressBar1 = this.progressBar1;
                    LoginUtil.labelMsg = this.labelMsg;
                    LoginUtil.load = this.load;
                    this.load.Start();
                }
            }
            catch (Exception exception)
            {
                WJs.alert(exception.Message + exception.StackTrace);
            }
        }

        public int SubLogin(string userName, string pwd, string dpart, string sjh, bool isSJdl, string JMs)
        {
            EventHandler ev = null;
            userPwd = pwd;
            ClientState.UseName = userName;
            ClientState.Pwd = userPwd;
            Main.userPwd = pwd;
            if (userName.Equals("admin") && (dpart == null))
            {
                if (ev == null)
                {
                    ev = delegate {
                        EventHandler ms = null;
                        if (LData.Do(new SvrParam(SysSet.SysLoginInc, null).Add("UserName", userName).Add("Pwd", pwd)))
                        {
                            LoginUtil.AdminLogin(pwd);
                            if (ms == null)
                            {
                                ms = delegate {
                                    this.Ok = true;
                                    this.Close();
                                };
                            }
                            this.Invoke(ms);
                        }
                    };
                }
                base.ExeEvent(ev);
                return 9;
            }
            ActionLoad load = ActionLoad.Conn();
            load.Action = SysSet.LoginInc;
            load.Add("LID", JMs);
            SysSet.LoginForm.LoginAction(load);
            if (isSJdl)
            {
                load.Add("sjh", sjh);
            }
            else
            {
                load.Add("ChosCode", dpart);
            }
            load.ServiceLoad += new LoadEventHandle(this.load_ServiceLoad);
            load.ServiceFaiLoad += new LoadFaiEventHandle(this.load_ServiceFaiLoad);
            base.ExeEvent(delegate {
                load.Post();
            });
            return 9;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (LoginUtil.LoadSuc)
            {
                this.timer1.Enabled = !this.timer1.Enabled;
                this.labelMsg.Visible = false;
                this.progressBar1.Visible = false;
                if (SysSet.LoginForm == null)
                {
                    WJs.alert("请设置SysSet.LoginForm用户登录界面!");
                }
                else
                {
                    SysSet.LoginForm.SetUMain(this);
                    Control control = SysSet.LoginForm.GetControl();
                    control.Location = SysSet.UserInputLoc;
                    base.Controls.Add(control);
                    try
                    {
                        Sys ui = (Sys) WJs.DeserializeObject("Login.dat");
                        if (ui != null)
                        {
                            this.userid = ui.UserID ?? "";
                            SysSet.LoginForm.InitUser(ui);
                            ulogin = ui.UserName;
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}

