namespace YtMain
{
    using DevComponents.DotNetBar;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using YtClient;
    using YtClient.data.events;
    using YtClient.locks;
    using YTMain;
    using YtPlugin;
    using YtSys;
    using YtUtil.tool;
    using YtWinContrl.com;
    using YtWinContrl.com.contrl;

    public class Main : YtForm
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private IContainer components = null;
        private int count;
        private GroupBox groupBox1;
        private bool isJz = false;
        private bool isSelUser = false;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label labelLink;
        private Label labelMsg;
        public static string Lid = "-1";
        private LinkLabel linkLabel1;
        private Thread load;
        public static bool LoadSuc = false;
        public bool Ok = false;
        public static Dictionary<string, System.Type> plugins = new Dictionary<string, System.Type>();
        private ProgressBar progressBar1;
        private SelTextInpt selTextInpt1;
        private SelTextInpt selTextInpt2;
        public static YTMain.SetLocakDataContrl SetLocakDataContrl;
        private string sjh = "";
        private System.Windows.Forms.TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private YTextBox textBox1;
        private YTextBox textBox2;
        private YTextBox textBox3;
        private YTextBox textBox4;
        private YTextBox textBox5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        public static string ulogin = "";
        private string userid = "";
        public static YTextBox UserNameContrl;
        public static string userPwd;
        public static YTextBox UserPwdContrl;

        public Main()
        {
            this.InitializeComponent();
            UserNameContrl = this.textBox5;
            UserPwdContrl = this.textBox4;
            base.Width = SysSet.LoginWidth;
            base.Height = SysSet.LoginHeight;
            this.groupBox1.Location = SysSet.UserInputLoc;
            this.tabControl1.Location = SysSet.UserInputLoc;
            Control.CheckForIllegalCrossThreadCalls = false;
            if (SysSet.LoginImg != null)
            {
                this.BackgroundImage = SysSet.LoginImg;
            }
            this.label3.Text = SysSet.LoginDepViewText;
            this.label1.Text = SysSet.LoginUserViewText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActionLoad load;
            this.selTextInpt2.SelectedRow = null;
            string str = this.textBox2.Text.Trim();
            userPwd = str;
            if (this.tabControl1.SelectedIndex == 1)
            {
                this.sjh = this.textBox1.Text.Trim();
                if (this.sjh.Length == 0)
                {
                    WJs.alert("请输入手机号！");
                    this.textBox1.Focus();
                    return;
                }
            }
            else
            {
                string str2 = this.selTextInpt2.Text.Trim();
                string str3 = this.selTextInpt1.Text.Trim();
                if (str2.Equals("admin") && ((str3.Length == 0) || (str3.IndexOf("加密") > -1)))
                {
                    if (LData.Do(new SvrParam(SysSet.SysLoginInc, null).Add("UserName", str2).Add("UserPwd", str)))
                    {
                        LoginUtil.AdminLogin(str);
                        this.timer2.Enabled = false;
                        base.Close();
                        this.Ok = true;
                    }
                    return;
                }
                if (((str3.IndexOf("加密") > -1) && (str2.IndexOf("admin") < 0)) && (this.selTextInpt1.Text.IndexOf("加密锁") > -1))
                {
                    WJs.alert("请检查加密锁是否与电脑正常连接！");
                    return;
                }
                if (str2.IndexOf("admin") > -1)
                {
                    str3 = str2.Replace("admin", "");
                    if (SysSet.IsDepartCodeBl)
                    {
                        str3 = WJs.Bl(str3, SysSet.DepartCodeBlLength);
                    }
                    str2 = "admin";
                    this.selTextInpt2.Value = str2;
                    this.selTextInpt1.Value = str3;
                    this.isSelUser = true;
                    Lid = "-1";
                }
                if (str3.Length == 0)
                {
                    WJs.alert("请输入" + SysSet.LoginDepViewText.Replace(" ", "") + "！");
                    this.selTextInpt1.Focus();
                    return;
                }
                if (str2.Length == 0)
                {
                    WJs.alert("请输入" + SysSet.LoginUserViewText.Replace(" ", "") + "！");
                    this.selTextInpt2.Focus();
                    return;
                }
            }
            if (this.selTextInpt1.Value == null)
            {
                WJs.alert("请通过查询选择机构信息，进行登录\n按下空格键进行查询！");
                this.selTextInpt1.Focus();
            }
            else
            {
                string text = "";
                if (this.selTextInpt2.Value != null)
                {
                    if (this.selTextInpt2.Text.Equals(ulogin) || this.isSelUser)
                    {
                        text = this.selTextInpt2.Value;
                    }
                    else
                    {
                        text = this.selTextInpt2.Text;
                    }
                }
                else
                {
                    text = this.selTextInpt2.Text;
                }
                userPwd = str;
                ClientState.UseName = this.selTextInpt1.Value + text;
                ClientState.Pwd = userPwd;
                load = ActionLoad.Conn();
                load.Action = SysSet.LoginInc;
                load.Add("LID", Lid);
                if (this.tabControl1.SelectedIndex == 1)
                {
                    load.Add("sjh", this.sjh);
                }
                else
                {
                    load.Add("ChosCode", this.selTextInpt1.Value);
                }
                load.ServiceLoad += new LoadEventHandle(this.load_ServiceLoad);
                load.ServiceFaiLoad += new LoadFaiEventHandle(this.load_ServiceFaiLoad);
                base.ExeEvent((EventHandler) ((, ) => load.Post()));
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

        private void button3_Click_1(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.button1_Click(null, null);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!FormUtil.Null(this.textBox5, "用户名不能为空！") && !FormUtil.Null(this.textBox4, "密码不能为空！"))
            {
                ClientState.UseName = this.textBox5.Text;
                ClientState.Pwd = this.textBox4.Text;
                ActionLoad load = ActionLoad.Conn();
                if (ClientState.UseName.Equals("admin"))
                {
                    if (LData.Do(new SvrParam(SysSet.SysLoginInc, null).Add("UserName", ClientState.UseName).Add("UserPwd", ClientState.Pwd)))
                    {
                        LoginUtil.AdminLogin(ClientState.Pwd);
                        this.timer2.Enabled = false;
                        base.Close();
                        this.Ok = true;
                    }
                }
                else
                {
                    load.Action = SysSet.LoginInc;
                    load.Add("LID", Lid);
                    load.Add("UserName", this.textBox5.Text);
                    load.Add("UserPwd", this.textBox4.Text);
                    load.ServiceLoad += new LoadEventHandle(this.load_ServiceLoad);
                    load.ServiceFaiLoad += new LoadFaiEventHandle(this.load_ServiceFaiLoad);
                    load.Post();
                }
            }
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Main));
            this.progressBar1 = new ProgressBar();
            this.labelMsg = new Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.tabPage2 = new TabPage();
            this.label6 = new Label();
            this.textBox3 = new YTextBox();
            this.textBox1 = new YTextBox();
            this.label5 = new Label();
            this.button4 = new Button();
            this.button5 = new Button();
            this.tabPage1 = new TabPage();
            this.selTextInpt2 = new SelTextInpt();
            this.button2 = new Button();
            this.selTextInpt1 = new SelTextInpt();
            this.label1 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.textBox2 = new YTextBox();
            this.button1 = new Button();
            this.label11 = new Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.button6 = new Button();
            this.button3 = new Button();
            this.textBox4 = new YTextBox();
            this.label9 = new Label();
            this.textBox5 = new YTextBox();
            this.label10 = new Label();
            this.groupBox1 = new GroupBox();
            this.labelLink = new Label();
            this.label12 = new Label();
            this.linkLabel1 = new LinkLabel();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.progressBar1.Dock = DockStyle.Bottom;
            this.progressBar1.Location = new Point(0, 0xf7);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x29d, 0x11);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 0;
            this.labelMsg.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.labelMsg.AutoSize = true;
            this.labelMsg.BackColor = Color.Transparent;
            this.labelMsg.Location = new Point(0, 0xe8);
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
            this.timer2.Interval = 0x3e8;
            this.timer2.Tick += new EventHandler(this.timer2_Tick);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Location = new Point(4, 0x16);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new Size(330, 0x8b);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "手机号登录";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.label6.AutoSize = true;
            this.label6.Font = new Font("宋体", 10f);
            this.label6.ForeColor = Color.Black;
            this.label6.Location = new Point(15, 0x4b);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x3f, 14);
            this.label6.TabIndex = 11;
            this.label6.Text = "密    码";
            this.textBox3.Border.Class = "TextBoxBorder";
            this.textBox3.Border.CornerType = eCornerType.Square;
            this.textBox3.Location = new Point(0x55, 0x47);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new Size(240, 0x15);
            this.textBox3.TabIndex = 1;
            this.textBox3.TextChanged += new EventHandler(this.textBox3_TextChanged);
            this.textBox1.Border.Class = "TextBoxBorder";
            this.textBox1.Border.CornerType = eCornerType.Square;
            this.textBox1.Location = new Point(0x55, 0x2b);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(240, 0x15);
            this.textBox1.TabIndex = 0;
            this.label5.AutoSize = true;
            this.label5.Font = new Font("宋体", 10f);
            this.label5.ForeColor = Color.Black;
            this.label5.Location = new Point(0x10, 0x2e);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x3f, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "手 机 号";
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = FlatStyle.Flat;
            this.button4.ForeColor = SystemColors.ControlText;
            this.button4.Image = (Image) manager.GetObject("button4.Image");
            this.button4.Location = new Point(0xfc, 0x65);
            this.button4.Name = "button4";
            this.button4.Size = new Size(0x48, 0x1a);
            this.button4.TabIndex = 3;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new EventHandler(this.button4_Click);
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = FlatStyle.Flat;
            this.button5.ForeColor = SystemColors.ControlText;
            this.button5.Image = (Image) manager.GetObject("button5.Image");
            this.button5.Location = new Point(0xa5, 0x65);
            this.button5.Name = "button5";
            this.button5.Size = new Size(0x48, 0x1a);
            this.button5.TabIndex = 2;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new EventHandler(this.button5_Click);
            this.tabPage1.Controls.Add(this.selTextInpt2);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.selTextInpt1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Location = new Point(4, 0x16);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new Size(330, 0x8b);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "普通登录";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.selTextInpt2.ColDefText = null;
            this.selTextInpt2.ColStyle = null;
            this.selTextInpt2.DataType = null;
            this.selTextInpt2.DbConn = null;
            this.selTextInpt2.Location = new Point(0x55, 0x2b);
            this.selTextInpt2.Name = "selTextInpt2";
            this.selTextInpt2.NextFocusControl = null;
            this.selTextInpt2.ReadOnly = false;
            this.selTextInpt2.SelParam = null;
            this.selTextInpt2.ShowColNum = 0;
            this.selTextInpt2.ShowWidth = 0;
            this.selTextInpt2.Size = new Size(0xef, 0x15);
            this.selTextInpt2.Sql = "LoginFindUser";
            this.selTextInpt2.SqlStr = null;
            this.selTextInpt2.TabIndex = 1;
            this.selTextInpt2.TvColName = null;
            this.selTextInpt2.Value = null;
            this.selTextInpt2.WatermarkText = "";
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = FlatStyle.Flat;
            this.button2.ForeColor = SystemColors.ControlText;
            this.button2.Image = (Image) manager.GetObject("button2.Image");
            this.button2.Location = new Point(0xfc, 0x65);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x48, 0x1a);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.selTextInpt1.ColDefText = null;
            this.selTextInpt1.ColStyle = null;
            this.selTextInpt1.DataType = null;
            this.selTextInpt1.DbConn = null;
            this.selTextInpt1.Location = new Point(0x55, 12);
            this.selTextInpt1.Name = "selTextInpt1";
            this.selTextInpt1.NextFocusControl = null;
            this.selTextInpt1.ReadOnly = false;
            this.selTextInpt1.SelParam = null;
            this.selTextInpt1.ShowColNum = 0;
            this.selTextInpt1.ShowWidth = 0;
            this.selTextInpt1.Size = new Size(0xef, 0x15);
            this.selTextInpt1.Sql = "LoginFindDepart";
            this.selTextInpt1.SqlStr = null;
            this.selTextInpt1.TabIndex = 0;
            this.selTextInpt1.TvColName = null;
            this.selTextInpt1.Value = null;
            this.selTextInpt1.WatermarkText = "";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10f);
            this.label1.ForeColor = Color.Black;
            this.label1.Location = new Point(0x10, 0x2e);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3f, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "用 户 名";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 10f);
            this.label3.ForeColor = Color.Black;
            this.label3.Location = new Point(0x10, 14);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3f, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "卫生机构";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10f);
            this.label2.ForeColor = Color.Black;
            this.label2.Location = new Point(0x10, 0x4e);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3f, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "密    码";
            this.textBox2.Border.Class = "TextBoxBorder";
            this.textBox2.Border.CornerType = eCornerType.Square;
            this.textBox2.Location = new Point(0x55, 0x4a);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new Size(0xef, 0x15);
            this.textBox2.TabIndex = 2;
            this.textBox2.KeyDown += new KeyEventHandler(this.textBox2_KeyDown);
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.ForeColor = SystemColors.ControlText;
            this.button1.Image = (Image) manager.GetObject("button1.Image");
            this.button1.Location = new Point(0xa5, 0x65);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x48, 0x1a);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label11.Font = new Font("宋体", 10f, FontStyle.Bold);
            this.label11.ForeColor = Color.Black;
            this.label11.Location = new Point(0x13, 0x65);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x134, 0x1b);
            this.label11.TabIndex = 7;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new Point(0x18, 0x1d);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x152, 0xa5);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.Visible = false;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = FlatStyle.Flat;
            this.button6.ForeColor = SystemColors.ControlText;
            this.button6.Image = (Image) manager.GetObject("button6.Image");
            this.button6.Location = new Point(0x6a, 0x52);
            this.button6.Name = "button6";
            this.button6.Size = new Size(0x48, 0x1a);
            this.button6.TabIndex = 2;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new EventHandler(this.button6_Click);
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = FlatStyle.Flat;
            this.button3.ForeColor = SystemColors.ControlText;
            this.button3.Image = (Image) manager.GetObject("button3.Image");
            this.button3.Location = new Point(0xc1, 0x52);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x48, 0x1a);
            this.button3.TabIndex = 3;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.button3_Click_1);
            this.textBox4.Border.Class = "TextBoxBorder";
            this.textBox4.Border.CornerType = eCornerType.Square;
            this.textBox4.Location = new Point(0x55, 0x37);
            this.textBox4.Name = "textBox4";
            this.textBox4.PasswordChar = '*';
            this.textBox4.Size = new Size(180, 0x15);
            this.textBox4.TabIndex = 1;
            this.textBox4.KeyDown += new KeyEventHandler(this.textBox4_KeyDown);
            this.label9.AutoSize = true;
            this.label9.Font = new Font("宋体", 10f);
            this.label9.ForeColor = Color.Black;
            this.label9.Location = new Point(0x10, 0x39);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x31, 14);
            this.label9.TabIndex = 10;
            this.label9.Text = "密  码";
            this.textBox5.Border.Class = "TextBoxBorder";
            this.textBox5.Border.CornerType = eCornerType.Square;
            this.textBox5.Location = new Point(0x55, 0x1a);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Size(180, 0x15);
            this.textBox5.TabIndex = 0;
            this.label10.AutoSize = true;
            this.label10.Font = new Font("宋体", 10f);
            this.label10.ForeColor = Color.Black;
            this.label10.Location = new Point(0x10, 30);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x31, 14);
            this.label10.TabIndex = 12;
            this.label10.Text = "用户名";
            this.groupBox1.BackColor = Color.Transparent;
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Location = new Point(0x16c, 0x45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(280, 0x79);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户登录";
            this.groupBox1.Visible = false;
            this.labelLink.AutoSize = true;
            this.labelLink.BackColor = Color.Transparent;
            this.labelLink.Font = new Font("宋体", 10f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.labelLink.ForeColor = Color.Navy;
            this.labelLink.Location = new Point(0x18, 0xd9);
            this.labelLink.Name = "labelLink";
            this.labelLink.Size = new Size(0x4f, 14);
            this.labelLink.TabIndex = 9;
            this.labelLink.Text = "labelLink";
            this.label12.BackColor = Color.Transparent;
            this.label12.Cursor = Cursors.Hand;
            this.label12.Font = new Font("宋体", 10f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label12.ForeColor = Color.Navy;
            this.label12.Location = new Point(0x19, 0xec);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x1a5, 20);
            this.label12.TabIndex = 10;
            this.label12.Click += new EventHandler(this.label12_DoubleClick);
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = Color.Transparent;
            this.linkLabel1.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.linkLabel1.LinkColor = Color.White;
            this.linkLabel1.Location = new Point(0x10, 0x5c);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new Size(0x58, 0x10);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            this.linkLabel1.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.ClientSize = new Size(0x29d, 0x108);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.labelMsg);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.labelLink);
            base.Controls.Add(this.label12);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Main";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "系统登录";
            base.Load += new EventHandler(this.Main_Load);
            base.Activated += new EventHandler(this.Main_Activated);
            base.FormClosed += new FormClosedEventHandler(this.Main_FormClosed);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void inputPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
                SendKeys.Flush();
            }
        }

        private bool IsValidPlugin(System.Type t)
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

        private void label12_DoubleClick(object sender, EventArgs e)
        {
            WJs.RunExe(LoginUtil.UAlter.Text.Replace("安装地址 ", ""));
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
            if (e.Msg == null)
            {
                WJs.alert("连接服务器失败！");
            }
            else
            {
                WJs.alert(e.Msg.Msg.Replace(this.selTextInpt1.Value, ""));
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox2.Focus();
            }
        }

        private void load_ServiceLoad(object sender, LoadEvent e)
        {
            base.Invoke(delegate {
                EventHandler upOk = null;
                if (e.Msg.Success)
                {
                    ulogin = this.selTextInpt2.Text;
                    this.isSelUser = false;
                    this.textBox2.Text = "";
                    this.textBox3.Text = "";
                    LoginUtil.LoginSuc(e, this.sjh, userPwd);
                    if (!SysSet.MySelfRunLogin)
                    {
                        DateTime now = DateTime.Now;
                        try
                        {
                            now = DateTime.Parse(e.Msg.GetValue(3).Trim());
                        }
                        catch
                        {
                            now = DateTime.Parse(e.Msg.GetValue(2).Trim());
                        }
                        TimeSpan span = (TimeSpan) (now - DateTime.Now);
                        if (Math.Abs(span.TotalMinutes) > 2.0)
                        {
                            try
                            {
                                short y = short.Parse(now.Year);
                                short m = short.Parse(now.Month);
                                short d = short.Parse(now.Day);
                                short h = short.Parse(now.Hour);
                                short mi = short.Parse(now.Minute);
                                Cmd.SetSysTime(y, m, d, h, mi, short.Parse(now.Second));
                            }
                            catch
                            {
                                WJs.alert("同步本机时间与服务器时间失败，请手动更改本机时间！\n服务器当前时间为【" + now.ToString("yyyy-MM-dd HH:ss") + "】");
                                return;
                            }
                        }
                    }
                    this.timer2.Enabled = false;
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
                }
            });
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
                SysSet.RegLink = this.linkLabel1;
                this.selTextInpt1.textBox1.KeyDown += new KeyEventHandler(this.textBox1_KeyDown);
                this.selTextInpt2.textBox1.KeyDown += new KeyEventHandler(this.inputPwd_KeyDown);
                LoginUtil.UAlter = this.label12;
                this.labelLink.Text = SysSet.LinkTel;
                this.timer2_Tick(null, null);
                if (!SysSet.SjLogin)
                {
                    this.tabControl1.Controls.Remove(this.tabPage2);
                }
                if (SysSet.CheckSuo)
                {
                    this.timer2.Enabled = true;
                    this.selTextInpt1.Enabled = false;
                    this.selTextInpt1.textBox1.Text = "";
                }
                this.selTextInpt1.ShowColNum = 2;
                this.selTextInpt2.ShowColNum = 4;
                this.selTextInpt2.ShowWidth = 500;
                this.selTextInpt1.Sql = SysSet.LoginFindDepart;
                this.selTextInpt2.Sql = SysSet.LoginFindUser;
                this.selTextInpt1.NextFocusControl = this.selTextInpt2;
                this.selTextInpt2.NextFocusControl = this.textBox2;
                if (!LoadSuc)
                {
                    this.load = new Thread(new ThreadStart(LoginUtil.LoadAllPlugins));
                    LoginUtil.progressBar1 = this.progressBar1;
                    LoginUtil.labelMsg = this.labelMsg;
                    LoginUtil.load = this.load;
                    this.load.Start();
                }
                else if (SysSet.PtLogin)
                {
                    this.groupBox1.Visible = true;
                }
                else
                {
                    this.tabControl1.Visible = true;
                }
            }
            catch (Exception exception)
            {
                WJs.alert(exception.Message + exception.StackTrace);
            }
        }

        private void RegPlugin(System.Type t)
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

        private void selTextInpt1_TextChanged(object sender, EventArgs e)
        {
            this.selTextInpt2.SelParam = this.selTextInpt1.Value ?? "";
            if (this.selTextInpt1.Value != null)
            {
            }
        }

        private void selTextInpt2_TextChanged(object sender, EventArgs e)
        {
            if (this.selTextInpt2.SelectedRow != null)
            {
                this.userid = this.selTextInpt2.SelectedRow.Cells["用户ID"].Value.ToString();
                this.isSelUser = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.selTextInpt2.textBox1.SelectionLength = this.selTextInpt2.Text.Length;
                this.selTextInpt2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(null, null);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.Text = this.textBox3.Text.Trim();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button6_Click(null, null);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (LoginUtil.LoadSuc)
            {
                this.timer1.Enabled = !this.timer1.Enabled;
                this.labelMsg.Visible = false;
                this.progressBar1.Visible = false;
                if (SysSet.PtLogin)
                {
                    this.groupBox1.Visible = true;
                    this.textBox5.Focus();
                }
                else
                {
                    this.tabControl1.Visible = true;
                    this.selTextInpt1.Focus();
                }
                this.selTextInpt1.TextChanged += new EventHandler(this.selTextInpt1_TextChanged);
                this.selTextInpt2.TextChanged += new EventHandler(this.selTextInpt2_TextChanged);
                try
                {
                    Sys sys = (Sys) WJs.DeserializeObject("Login.dat");
                    if (sys != null)
                    {
                        this.userid = sys.UserID ?? "";
                        this.selTextInpt1.Text = sys.DepName;
                        this.selTextInpt1.Value = sys.DepCode;
                        this.selTextInpt2.Text = sys.UserName;
                        ulogin = sys.UserName;
                        this.selTextInpt2.Value = sys.Useraccount;
                        this.textBox2.Focus();
                        this.selTextInpt2.SelParam = sys.DepCode ?? "";
                        this.textBox1.Text = sys.Tel;
                        this.textBox5.Text = sys.UserName;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (SysSet.CheckSuo)
            {
                string[] strArray;
                try
                {
                    if (SetLocakDataContrl == null)
                    {
                        strArray = LockTool.GetData().Split(new char[] { '|' });
                        Lid = strArray[0];
                        this.selTextInpt1.Value = strArray[1];
                        this.selTextInpt1.Text = strArray[2];
                        this.selTextInpt2.SelParam = this.selTextInpt1.Value ?? "";
                        if (strArray.Length > 3)
                        {
                            this.label11.Text = strArray[3];
                        }
                    }
                    else
                    {
                        Lid = SetLocakDataContrl(this.selTextInpt1, this.selTextInpt2, LockTool.GetData());
                    }
                }
                catch
                {
                    if (!WJs.IsClickOnceRun)
                    {
                        string suostr = WJs.AppStr("LockValue");
                        if (suostr == null)
                        {
                            this.selTextInpt1.Text = "请插入加密锁！";
                        }
                        else if (SetLocakDataContrl == null)
                        {
                            strArray = suostr.Split(new char[] { '|' });
                            Lid = strArray[0];
                            this.selTextInpt1.Value = strArray[1];
                            this.selTextInpt1.Text = strArray[2];
                            this.selTextInpt2.SelParam = this.selTextInpt1.Value ?? "";
                            if (strArray.Length > 3)
                            {
                                this.label11.Text = strArray[3];
                            }
                        }
                        else
                        {
                            Lid = SetLocakDataContrl(this.selTextInpt1, this.selTextInpt2, suostr);
                        }
                    }
                    else
                    {
                        this.selTextInpt1.Text = "请插入加密锁！";
                    }
                }
            }
        }
    }
}

