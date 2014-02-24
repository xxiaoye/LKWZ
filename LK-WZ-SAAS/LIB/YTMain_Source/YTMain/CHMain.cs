namespace YtMain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows.Forms;
    using System.Xml;
    using YtClient;
    using YTMain;
    using YTMain.tool;
    using YtPlugin;
    using YtSys;
    using YtUtil.tool;
    using YtWinContrl.com.contrl;
    using YtWinContrl.com.panel;

    public class CHMain : YtForm, IAppContent
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private bool IsNotify = false;
        public static bool IsShowMInput = false;
        public bool isZxSys = false;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private LabelTree labelTree1;
        public IList<IPlug> loads;
        public Form LoginForm;
        private Panel MenuCon;
        private MenuStrip menuStrip1;
        private NotifyIcon notifyIcon1;
        private Panel panel1;
        private Panel panel3;
        private SysQd s;
        private Splitter splitter1;
        private StatusStrip statusStrip1;
        private TabPage tabPage1;
        private TabControl tabShow;
        private YTextBox textBox1;
        private Timer timer1;
        private string title;
        private ToolStripStatusLabel toolStatus;
        private ToolStripStatusLabel toolStatusMsg;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        public const int WM_MDINEXT = 0x224;
        private ToolStripMenuItem 打开主面板ToolStripMenuItem;
        private ToolStripMenuItem 退出系统ToolStripMenuItem;
        private ToolStripMenuItem 系统设置ToolStripMenuItem;

        public CHMain()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            this.label1.Text = SysSet.LinkTel;
            this.label2.Text = SysSet.LinkQQ;
            this.Text = SysSet.MainTitle;
            if (SysSet.TopImg != null)
            {
                this.panel1.BackgroundImage = SysSet.TopImg;
            }
            if (SysSet.MainIco != null)
            {
                base.Icon = SysSet.MainIco;
            }
            this.notifyIcon1.Text = SysSet.NotifyText;
            if (SysSet.NotifyIco != null)
            {
                this.notifyIcon1.Icon = SysSet.NotifyIco;
            }
        }

        private void addNode(TreeNode node, XmlElement root)
        {
            XmlNodeList childNodes = root.ChildNodes;
            foreach (XmlNode node2 in childNodes)
            {
                XmlElement xe = (XmlElement) node2;
                string attribute = xe.GetAttribute("name");
                if (this.isHaveRight(xe))
                {
                    TreeNode node3 = new TreeNode(attribute) {
                        Tag = this.getByXmlElement(xe)
                    };
                    if (xe.HasChildNodes)
                    {
                        this.addNode(node3, xe);
                    }
                    if (xe.HasAttribute("typeName") || (node3.Nodes.Count != 0))
                    {
                        node.Nodes.Add(node3);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.tabShow.SelectedIndex;
            if ((selectedIndex > 0) && (selectedIndex < this.loads.Count))
            {
                this.loads[selectedIndex].getMainForm().Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Hide();
            new SuoForm { sysMain = this }.Show();
        }

        public void button3_Click(object sender, EventArgs e)
        {
            if (WJs.confirm(SysSet.ExitAlert))
            {
                this.exitSys();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (WJs.confirm("确定注销系统吗？"))
            {
                this.isZxSys = true;
                for (int i = 0; i < this.loads.Count; i++)
                {
                    this.loads[i].getMainForm().Close();
                }
                this.tabShow.TabPages.Clear();
                TabPage page = new TabPage("主页");
                this.tabShow.TabPages.Add(page);
                Application.DoEvents();
                base.Hide();
                this.notifyIcon1.Visible = false;
                if (SysSet.LoginForm != null)
                {
                    UMain main = new UMain();
                    main.ShowDialog();
                    this.isZxSys = false;
                    if (main.Ok)
                    {
                        base.Show();
                        this.InitForm();
                    }
                    else
                    {
                        this.exitSys();
                    }
                }
                else
                {
                    Main main2 = new Main();
                    main2.ShowDialog();
                    this.isZxSys = false;
                    if (main2.Ok)
                    {
                        base.Show();
                        this.InitForm();
                    }
                    else
                    {
                        this.exitSys();
                    }
                }
            }
        }

        private void ChangeMidBackStyle()
        {
            MdiClient client = new MdiClient();
            foreach (Control control in base.Controls)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.MdiClient")
                {
                    client = (MdiClient) control;
                    client.BackColor = Color.FromArgb(0xee, 0xf3, 250);
                    if (File.Exists("empty.gif"))
                    {
                        this.BackgroundImageLayout = ImageLayout.Center;
                        client.BackgroundImage = Image.FromFile("empty.gif");
                    }
                    break;
                }
            }
        }

        private void CHMain_Load(object sender, EventArgs e)
        {
            this.InitForm();
            this.s = (SysQd) WJs.DeserializeObject("Sys.db");
            if (this.s == null)
            {
                if (SysSet.IsRunStart)
                {
                    WJs.RunWhenStart(true, SysSet.MainTitle, Application.StartupPath + @"\" + SysSet.ExeName + ".exe");
                }
                this.s = new SysQd();
                this.s.IsReg = true;
                this.s.IsOut = false;
                this.s.IsAlert = true;
                WJs.SerializeObject(this.s, "Sys.db");
            }
        }

        public void Close(string wintxt)
        {
            foreach (Form form in base.MdiChildren)
            {
                if (form.Text.IndexOf(wintxt) > -1)
                {
                    IPlug plug = form as IPlug;
                    if ((plug != null) && plug.unLoad())
                    {
                        form.Close();
                    }
                    else
                    {
                        form.Close();
                    }
                }
            }
        }

        private void cm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                TreeNode node = this.labelTree1.Find(item.Text);
                NodeValue tag = (NodeValue) node.Tag;
                int r = 0;
                if (WJs.IsNum(tag.Right))
                {
                    r = int.Parse(tag.Right);
                }
                if (Ui.HaveRight(r) || (r == 0))
                {
                    if ((tag.Value != null) && (tag.Value.Trim().Length > 0))
                    {
                        this.title = node.Text;
                        this.LoadPlug(tag.Value.Trim(), null, true);
                        if (!base.Visible)
                        {
                            base.Show();
                        }
                        else
                        {
                            base.Activate();
                        }
                        if (base.WindowState == FormWindowState.Minimized)
                        {
                            base.WindowState = FormWindowState.Normal;
                        }
                    }
                }
                else
                {
                    WJs.alert("没有权限");
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

        private void exitSys()
        {
            this.notifyIcon1.Visible = false;
            base.Close();
            Application.Exit();
        }

        public void ExitSys()
        {
            this.exitSys();
        }

        private void f_Resize(object sender, EventArgs e)
        {
            Form form = sender as Form;
            if ((form != null) && (form.WindowState != FormWindowState.Maximized))
            {
                form.WindowState = FormWindowState.Maximized;
            }
        }

        public void FormClose(object sender, FormClosedEventArgs e)
        {
            try
            {
                int selectedIndex = this.tabShow.SelectedIndex;
                if (this.loads.Count > this.tabShow.SelectedIndex)
                {
                    this.loads[selectedIndex].unLoad();
                    this.loads.RemoveAt(selectedIndex);
                    if (selectedIndex != 1)
                    {
                        this.tabShow.SelectedIndex = selectedIndex - 1;
                    }
                    this.tabShow.TabPages.RemoveAt(selectedIndex);
                    if ((selectedIndex == 1) && (this.tabShow.TabCount > 1))
                    {
                        this.tabShow.SelectedIndex = this.tabShow.TabCount - 1;
                    }
                }
            }
            catch
            {
            }
        }

        private NodeValue getByXmlElement(XmlElement xe)
        {
            return new NodeValue(xe.GetAttribute("name"), xe.GetAttribute("typeName"), xe.GetAttribute("ico"), xe.GetAttribute("oico"));
        }

        public MdiClient GetMDIClient()
        {
            foreach (Control control in base.Controls)
            {
                if (control is MdiClient)
                {
                    return (MdiClient) control;
                }
            }
            throw new InvalidOperationException("No MDIClient !!!");
        }

        public Form GetOpenForm(string pname, string name)
        {
            throw new NotImplementedException();
        }

        public void HideForm(IHide h)
        {
            throw new NotImplementedException();
        }

        public void InitForm()
        {
            this.timer1.Enabled = !this.timer1.Enabled;
            this.toolStripStatusLabel1.Text = LoginUtil.ShowText();
            this.notifyIcon1.Visible = SysSet.NotifyShow;
            Application.DoEvents();
            base.MainMenuStrip = this.menuStrip1;
            this.ChangeMidBackStyle();
            this.loads = new List<IPlug>();
            this.LoadMenu();
            this.title = "主页";
            LoginUtil.plugins["YTMain.Index"] = typeof(Index);
            this.LoadPlug(SysSet.IndexPage, null, true);
            Label label = new Label {
                Text = "ddd"
            };
            if ((SysSet.InitPlug != null) && (SysSet.InitPlug.Trim().Length > 0))
            {
                this.LoadPlug(SysSet.InitPlug, null, false);
            }
            if (SysSet.InitRep == null)
            {
                this.LoadPlug("RepEdit.RepInit", new object[] { 2, Ui.User.DepCode, Ui.User.DepName, Ui.User.UserID, Ui.User.UserName }, false);
            }
            else
            {
                this.LoadPlug("RepEdit.RepInit", SysSet.InitRep(), false);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(CHMain));
            this.statusStrip1 = new StatusStrip();
            this.toolStatus = new ToolStripStatusLabel();
            this.toolStatusMsg = new ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new ToolStripStatusLabel();
            this.panel1 = new Panel();
            this.textBox1 = new YTextBox();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.button4 = new Button();
            this.button3 = new Button();
            this.button2 = new Button();
            this.splitter1 = new Splitter();
            this.menuStrip1 = new MenuStrip();
            this.tabShow = new TabControl();
            this.tabPage1 = new TabPage();
            this.MenuCon = new Panel();
            this.labelTree1 = new LabelTree();
            this.panel3 = new Panel();
            this.button1 = new Button();
            this.timer1 = new Timer(this.components);
            this.notifyIcon1 = new NotifyIcon(this.components);
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.打开主面板ToolStripMenuItem = new ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.退出系统ToolStripMenuItem = new ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabShow.SuspendLayout();
            this.MenuCon.SuspendLayout();
            this.panel3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.toolStatus, this.toolStatusMsg, this.toolStripStatusLabel1, this.toolStripStatusLabel2 });
            this.statusStrip1.Location = new Point(0, 0x1e6);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(0x345, 0x16);
            this.statusStrip1.TabIndex = 0;
            this.toolStatus.AutoSize = false;
            this.toolStatus.Name = "toolStatus";
            this.toolStatus.Size = new Size(180, 0x11);
            this.toolStatus.Text = "2011";
            this.toolStatus.TextAlign = ContentAlignment.MiddleLeft;
            this.toolStatusMsg.AutoSize = false;
            this.toolStatusMsg.Name = "toolStatusMsg";
            this.toolStatusMsg.Size = new Size(200, 0x11);
            this.toolStatusMsg.TextAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel1.BackgroundImage = (Image) manager.GetObject("toolStripStatusLabel1.BackgroundImage");
            this.toolStripStatusLabel1.BackgroundImageLayout = ImageLayout.None;
            this.toolStripStatusLabel1.ImageAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new Size(0x44, 0x11);
            this.toolStripStatusLabel1.Text = "   当前用户";
            this.toolStripStatusLabel1.TextAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new Size(20, 0x11);
            this.toolStripStatusLabel2.Text = "   ";
            this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x345, 0x38);
            this.panel1.TabIndex = 3;
            this.textBox1.Location = new Point(0x13f, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0, 0x15);
            this.textBox1.TabIndex = 9;
            this.textBox1.Visible = false;
            this.textBox1.MouseDoubleClick += new MouseEventHandler(this.textBox1_MouseDoubleClick);
            this.label6.BackColor = Color.Transparent;
            this.label6.Location = new Point(0x12f, 2);
            this.label6.Name = "label6";
            this.label6.Size = new Size(10, 10);
            this.label6.TabIndex = 8;
            this.label6.Click += new EventHandler(this.label6_Click);
            this.label5.AutoSize = true;
            this.label5.BackColor = Color.Transparent;
            this.label5.Location = new Point(0x54, 1);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0, 12);
            this.label5.TabIndex = 7;
            this.label4.BackColor = Color.Transparent;
            this.label4.Location = new Point(50, 1);
            this.label4.Name = "label4";
            this.label4.Size = new Size(11, 10);
            this.label4.TabIndex = 6;
            this.label4.Click += new EventHandler(this.label4_Click);
            this.label3.BackColor = Color.Transparent;
            this.label3.Location = new Point(0x16, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(11, 11);
            this.label3.TabIndex = 5;
            this.label3.Click += new EventHandler(this.label3_Click);
            this.label2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.Font = new Font("宋体", 10f, FontStyle.Bold);
            this.label2.ForeColor = Color.DarkGreen;
            this.label2.Location = new Point(0x1f0, 0x23);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x95, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "企业QQ:  800013506";
            this.label2.Click += new EventHandler(this.label2_Click);
            this.label1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Font = new Font("宋体", 10f, FontStyle.Bold);
            this.label1.ForeColor = Color.DarkGreen;
            this.label1.Location = new Point(0x196, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xef, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "24小时免费服务电话:40066-13506";
            this.button4.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.button4.Image = (Image) manager.GetObject("button4.Image");
            this.button4.ImageAlign = ContentAlignment.MiddleLeft;
            this.button4.Location = new Point(0x2ca, 0x15);
            this.button4.Name = "button4";
            this.button4.Size = new Size(0x39, 30);
            this.button4.TabIndex = 2;
            this.button4.Text = "注销";
            this.button4.TextAlign = ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new EventHandler(this.button4_Click);
            this.button3.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.button3.Image = (Image) manager.GetObject("button3.Image");
            this.button3.ImageAlign = ContentAlignment.MiddleLeft;
            this.button3.Location = new Point(0x309, 0x15);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x39, 30);
            this.button3.TabIndex = 1;
            this.button3.Text = "退出";
            this.button3.TextAlign = ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.button2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.button2.Image = (Image) manager.GetObject("button2.Image");
            this.button2.ImageAlign = ContentAlignment.MiddleLeft;
            this.button2.Location = new Point(0x28b, 0x15);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x39, 30);
            this.button2.TabIndex = 0;
            this.button2.Text = "锁屏";
            this.button2.TextAlign = ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.splitter1.BackColor = Color.LightCyan;
            this.splitter1.Location = new Point(0xb0, 0x38);
            this.splitter1.Margin = new Padding(0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new Size(4, 430);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(0x345, 0x18);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            this.tabShow.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tabShow.Controls.Add(this.tabPage1);
            this.tabShow.Location = new Point(0, 3);
            this.tabShow.Name = "tabShow";
            this.tabShow.SelectedIndex = 0;
            this.tabShow.ShowToolTips = true;
            this.tabShow.Size = new Size(0x274, 0x16);
            this.tabShow.TabIndex = 0;
            this.tabShow.MouseDoubleClick += new MouseEventHandler(this.tabShow_MouseDoubleClick);
            this.tabShow.SelectedIndexChanged += new EventHandler(this.tabShow_SelectedIndexChanged);
            this.tabPage1.Location = new Point(4, 0x16);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new Size(620, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "主页";
            this.tabPage1.ToolTipText = "双击关闭";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.MenuCon.Controls.Add(this.labelTree1);
            this.MenuCon.Dock = DockStyle.Left;
            this.MenuCon.Location = new Point(0, 0x38);
            this.MenuCon.Margin = new Padding(0);
            this.MenuCon.Name = "MenuCon";
            this.MenuCon.Size = new Size(0xb0, 430);
            this.MenuCon.TabIndex = 4;
            this.labelTree1.BackColor = Color.Transparent;
            this.labelTree1.Cursor = Cursors.Default;
            this.labelTree1.Dock = DockStyle.Fill;
            this.labelTree1.Location = new Point(0, 0);
            this.labelTree1.Name = "labelTree1";
            this.labelTree1.Size = new Size(0xb0, 430);
            this.labelTree1.TabIndex = 0;
            this.labelTree1.NodeDoubleClick += new EventHandler(this.labelTree1_NodeDoubleClick);
            this.panel3.BackColor = SystemColors.ActiveCaptionText;
            this.panel3.BackgroundImage = (Image) manager.GetObject("panel3.BackgroundImage");
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.tabShow);
            this.panel3.Dock = DockStyle.Top;
            this.panel3.Location = new Point(180, 0x38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x291, 0x15);
            this.panel3.TabIndex = 9;
            this.button1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.button1.BackColor = SystemColors.ControlLight;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.Image = (Image) manager.GetObject("button1.Image");
            this.button1.Location = new Point(0x27a, 1);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x15, 0x13);
            this.button1.TabIndex = 1;
            this.button1.Text = "   关闭";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.timer1.Interval = 0x3e8;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "打印耗材监控系统";
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.打开主面板ToolStripMenuItem, this.系统设置ToolStripMenuItem, this.toolStripSeparator1, this.退出系统ToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x89, 0x4c);
            this.打开主面板ToolStripMenuItem.Name = "打开主面板ToolStripMenuItem";
            this.打开主面板ToolStripMenuItem.Size = new Size(0x88, 0x16);
            this.打开主面板ToolStripMenuItem.Text = "打开主面板";
            this.打开主面板ToolStripMenuItem.Click += new EventHandler(this.打开主面板ToolStripMenuItem_Click);
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new Size(0x88, 0x16);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            this.系统设置ToolStripMenuItem.Click += new EventHandler(this.系统设置ToolStripMenuItem_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(0x85, 6);
            this.退出系统ToolStripMenuItem.Name = "退出系统ToolStripMenuItem";
            this.退出系统ToolStripMenuItem.Size = new Size(0x88, 0x16);
            this.退出系统ToolStripMenuItem.Text = "退出系统";
            this.退出系统ToolStripMenuItem.Click += new EventHandler(this.退出系统ToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x345, 0x1fc);
            base.Controls.Add(this.panel3);
            base.Controls.Add(this.splitter1);
            base.Controls.Add(this.MenuCon);
            base.Controls.Add(this.statusStrip1);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.menuStrip1);
            base.IsMdiContainer = true;
            base.MainMenuStrip = this.menuStrip1;
            base.Name = "CHMain";
            this.Text = "YTMain";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.CHMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabShow.ResumeLayout(false);
            this.MenuCon.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private bool isHaveRight(XmlElement xe)
        {
            if (xe.HasAttribute("right"))
            {
                if (WJs.IsNum(xe.GetAttribute("right")))
                {
                    return LoginUtil.HaveRight(int.Parse(xe.GetAttribute("right")));
                }
                return (LoginUtil.HaveRight(-1) || true);
            }
            return true;
        }

        private int isHaveTitleForm(string t)
        {
            int num = 0;
            foreach (TabPage page in this.tabShow.TabPages)
            {
                if (page.Text.Equals(t))
                {
                    return num;
                }
                num++;
            }
            return -1;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.title = "日志";
            this.LoadPlug("SysWH.GetLog", null, true);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (ActionLoad.QxErr)
            {
                ActionLoad.QxErr = false;
                this.label5.Text = "";
            }
            else
            {
                ActionLoad.QxErr = true;
                this.label5.Text = "详细错误提示已开启";
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (!this.textBox1.Visible)
            {
                if (IsShowMInput)
                {
                    this.textBox1.Visible = true;
                    this.textBox1.Width = 200;
                }
            }
            else
            {
                this.textBox1.Visible = false;
            }
        }

        private void labelTree1_NodeDoubleClick(object sender, EventArgs e)
        {
            TreeNode node = (TreeNode) sender;
            if (node != null)
            {
                NodeValue tag = (NodeValue) node.Tag;
                if ((tag.Value != null) && (tag.Value.Trim().Length > 0))
                {
                    this.title = node.Text;
                    this.LoadPlug(tag.Value.Trim(), null, true);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void LoadMenu()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(LoginUtil.MenuStr);
            XmlNodeList childNodes = document.SelectSingleNode("Plugs").ChildNodes;
            TreeNode node2 = new TreeNode();
            foreach (XmlNode node3 in childNodes)
            {
                XmlElement xe = (XmlElement) node3;
                string attribute = xe.GetAttribute("name");
                if (this.isHaveRight(xe))
                {
                    TreeNode node = new TreeNode(attribute) {
                        Tag = this.getByXmlElement(xe)
                    };
                    if (xe.HasChildNodes)
                    {
                        this.addNode(node, xe);
                    }
                    if (xe.HasAttribute("typeName") || (node.Nodes.Count != 0))
                    {
                        node2.Nodes.Add(node);
                    }
                }
            }
            this.labelTree1.DataSource = node2;
            if (!this.IsNotify && SysSet.NotifyShow)
            {
                this.IsNotify = true;
                string str2 = WJs.AppStr("NotifyMenu");
                if (str2 != null)
                {
                    string[] strArray = str2.Split(new char[] { ',' });
                    foreach (string str3 in strArray)
                    {
                        TreeNode node5 = this.labelTree1.Find(str3);
                        if (node5 != null)
                        {
                            NodeValue tag = node5.Tag as NodeValue;
                            if (tag != null)
                            {
                                int r = 0;
                                if (WJs.IsNum(tag.Right))
                                {
                                    r = int.Parse(tag.Right);
                                }
                                if (Ui.HaveRight(r) || (r == 0))
                                {
                                    ToolStripMenuItem item = new ToolStripMenuItem(str3);
                                    item.Click += new EventHandler(this.cm_Click);
                                    this.contextMenuStrip1.Items.Insert(0, item);
                                }
                            }
                        }
                    }
                }
                this.notifyIcon1.Visible = true;
            }
        }

        public IPlug LoadPlug(string pname, object[] param, bool isShow)
        {
            int num;
            if ((pname.IndexOf('?') > -1) && (param == null))
            {
                string str = pname.Substring(pname.IndexOf('?') + 1);
                pname = pname.Substring(0, pname.IndexOf('?'));
                string[] strArray = str.Split(new char[] { ',' });
                object[] objArray = new object[strArray.Length];
                for (num = 0; num < strArray.Length; num++)
                {
                    string[] strArray2 = strArray[num].Split(new char[] { '=' });
                    if (strArray2.Length > 1)
                    {
                        objArray[num] = strArray2[1];
                    }
                    else
                    {
                        objArray[num] = strArray2[0];
                    }
                }
                param = objArray;
            }
            if (isShow)
            {
                num = this.isHaveTitleForm(this.title);
                if (num > 0)
                {
                    this.tabShow.SelectedIndex = num;
                    return null;
                }
            }
            if (LoginUtil.plugins.ContainsKey(pname))
            {
                System.Type type = LoginUtil.plugins[pname];
                if (type != null)
                {
                    IPlug item = (IPlug) Activator.CreateInstance(type);
                    try
                    {
                        item.initPlug(this, param);
                        Form form = item.getMainForm();
                        if (form != null)
                        {
                            form.ShowInTaskbar = false;
                            if (isShow)
                            {
                                form.FormClosed += new FormClosedEventHandler(this.FormClose);
                                form.Resize += new EventHandler(this.f_Resize);
                                this.loads.Add(item);
                                form.MdiParent = this;
                                form.WindowState = FormWindowState.Maximized;
                                form.MaximizeBox = false;
                                form.MinimizeBox = false;
                                if (!this.title.Equals("主页"))
                                {
                                    TabPage page = new TabPage(this.title);
                                    this.tabShow.TabPages.Add(page);
                                    this.tabShow.SelectedIndex = this.loads.Count - 1;
                                }
                                form.Show();
                            }
                            else
                            {
                                form.StartPosition = FormStartPosition.CenterScreen;
                            }
                        }
                        this.ShowStatus(this.title);
                        if (form != null)
                        {
                            form.Activate();
                        }
                        return item;
                    }
                    catch (Exception exception)
                    {
                        WJs.alert(exception.Message + exception.StackTrace);
                    }
                }
            }
            else
            {
                WJs.alert("没有找到模块【" + pname + "】，请检查系统是否更新！");
            }
            return null;
        }

        public void Open(Form f)
        {
            throw new NotImplementedException();
        }

        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public void SetFormFoucs(Form childToActivate)
        {
            if (base.ActiveMdiChild != childToActivate)
            {
                MdiClient mDIClient = this.GetMDIClient();
                int length = base.MdiChildren.Length;
                Control control = null;
                int index = mDIClient.Controls.IndexOf(childToActivate);
                if (index < 0)
                {
                    throw new InvalidOperationException("MDIChild form not found");
                }
                if (index == 0)
                {
                    control = mDIClient.Controls[1];
                }
                else
                {
                    control = mDIClient.Controls[index - 1];
                }
                IntPtr lParam = new IntPtr((index == 0) ? 1 : 0);
                SendMessage(mDIClient.Handle, 0x224, control.Handle, lParam);
            }
        }

        public void SetMenuSelectIndex(int i)
        {
        }

        public void ShowStatus(string msg)
        {
            this.toolStatusMsg.Text = msg;
            Application.DoEvents();
        }

        private void tabShow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.button1_Click(null, null);
        }

        private void tabShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.loads.Count > this.tabShow.SelectedIndex)
                {
                    Form childToActivate = this.loads[this.tabShow.SelectedIndex].getMainForm();
                    if (!childToActivate.Focused)
                    {
                        this.SetFormFoucs(childToActivate);
                        this.ShowStatus(childToActivate.Text);
                    }
                }
            }
            catch
            {
            }
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.LoadPlug(this.textBox1.Text, null, true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStatus.Text = DateTime.Now.ToString("yyyy-MM-dd ") + this.Week() + " " + DateTime.Now.ToString("hh:mm:ss");
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            string fileName = "http://xnh.gzxnh.gov.cn/guizhou/";
            Process.Start(fileName);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        public string Week()
        {
            return new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" }[Convert.ToInt32(DateTime.Now.DayOfWeek)];
        }

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == 0x112) && (((int) m.WParam) == 0xf060))
            {
                if (!this.isZxSys)
                {
                    if (SysSet.NotifyShow)
                    {
                        if (this.s.IsAlert)
                        {
                            AlertForm form = new AlertForm();
                            form.ShowDialog();
                            if (form.isOk)
                            {
                                this.s.IsOut = form.IsOut;
                                this.s.IsAlert = form.IsAlert;
                                if (!form.IsOut)
                                {
                                    base.Hide();
                                    this.notifyIcon1.Visible = true;
                                }
                            }
                        }
                        else if (!this.s.IsOut)
                        {
                            base.Hide();
                            this.notifyIcon1.Visible = true;
                        }
                        else if (WJs.confirm(SysSet.ExitAlert))
                        {
                            this.exitSys();
                        }
                    }
                    else if (WJs.confirm(SysSet.ExitAlert))
                    {
                        this.exitSys();
                    }
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void 打开主面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Show();
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WJs.confirm(SysSet.ExitAlert))
            {
                this.exitSys();
            }
        }

        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetForm form = new SetForm();
            form.ShowDialog();
            if (form.isOk)
            {
                this.s.IsOut = form.IsOut;
                this.s.IsAlert = form.IsAlert;
            }
        }
    }
}

