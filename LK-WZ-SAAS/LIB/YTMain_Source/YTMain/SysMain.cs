namespace YTMain
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using YtClient;
    using YtClient.data;
    using YtDict.update;
    using YtMain;
    using YTMain.contrl;
    using YTMain.tool;
    using YtPlugin;
    using YtSys;
    using YtUtil.tool;
    using YtWinContrl.com.contrl;
    using YtWinContrl.com.panel;

    public class SysMain : NYtForm, IAppContent
    {
        private string bz = null;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        public NodeValue CurrNode = null;
        private ExpandableSplitter Expend;
        private FlowLayoutPanel flowLayoutPanel1;
        public bool isZxSys = false;
        private Label label1;
        private Label label2;
        public static SysMain Main;
        private MenuStrip menuStrip1;
        private MenuStrip MergeMenu;
        private NavigationPanePanel navigationPanePanel1;
        private SysMenu NMenu;
        private NotifyIcon notifyIcon1;
        private Panel panel1;
        private SysQd s;
        private int ShowStaTextCount = 0;
        private StatusStrip statusStrip1;
        private int SysMenuCount;
        private TabItem tabItem1;
        private TabStrip tabStrip1;
        private Timer timer1;
        private Timer timer2;
        private ToolStripStatusLabel toolStatus;
        private ToolStripStatusLabel toolStatusMsg;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private YtButton ytButton1;
        private YtButton ytButton2;
        private YtButton ytButton3;
        private YtButton ytButton4;
        private ToolStripMenuItem 报表ToolStripMenuItem;
        private ToolStripMenuItem 菜单ToolStripMenuItem;
        private ToolStripMenuItem 打开主面板ToolStripMenuItem;
        private ToolStripMenuItem 获取运行地址ToolStripMenuItem;
        private ToolStripMenuItem 清除本地缓存ToolStripMenuItem;
        private ToolStripMenuItem 日志ToolStripMenuItem;
        private ToolStripMenuItem 锁屏ToolStripMenuItem;
        private ToolStripMenuItem 退出ToolStripMenuItem;
        private ToolStripMenuItem 退出系统ToolStripMenuItem;
        private ToolStripMenuItem 系统ToolStripMenuItem;
        private ToolStripMenuItem 系统设置ToolStripMenuItem;
        private ToolStripMenuItem 重新下载字典数据ToolStripMenuItem;
        private ToolStripMenuItem 注销ToolStripMenuItem;

        public SysMain()
        {
            this.InitializeComponent();
            Main = this;
            if (SysSet.MenuWidth != 220)
            {
                this.NMenu.Width = SysSet.MenuWidth;
            }
            if ((SysSet.BottomUrls != null) && (SysSet.BottomUrls.Count > 0))
            {
                foreach (UrlLink link in SysSet.BottomUrls)
                {
                    ToolStripStatusLabel label = new ToolStripStatusLabel {
                        Text = link.Text,
                        Font = new Font("微软雅黑", 9f, FontStyle.Bold),
                        LinkColor = Color.Green,
                        IsLink = true,
                        Tag = link
                    };
                    label.Click += new EventHandler(this.ll_Click);
                    this.statusStrip1.Items.Insert(this.statusStrip1.Items.Count - 2, label);
                }
            }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ExitSys()
        {
            this.notifyIcon1.Visible = false;
            base.Close();
            Application.Exit();
        }

        private void f_Activated(object sender, EventArgs e)
        {
            Form form = sender as Form;
            if (form != null)
            {
                form.WindowState = FormWindowState.Normal;
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(-6, -30);
                form.WindowState = FormWindowState.Maximized;
                this.ShowStatus(form.Text);
            }
        }

        public Control GetOpenConTree(Node node)
        {
            NodeValue tag = node.Tag as NodeValue;
            if (tag != null)
            {
                return (this.LoadPlug(tag.Value, null, false).getMainForm().Tag as Control);
            }
            return null;
        }

        public Form GetOpenForm(string pname, string name)
        {
            foreach (Form form in base.MdiChildren)
            {
                if (form is IYtForm)
                {
                    IYtForm form2 = form as IYtForm;
                    if ((form2.GetOnlyText() != null) && form2.GetOnlyText().Equals(name))
                    {
                        return form;
                    }
                }
                if ((form.Tag != null) && form.Tag.ToString().Equals(pname))
                {
                    return form;
                }
                if ((form.Text ?? "").IndexOf(name) > -1)
                {
                    return form;
                }
            }
            return null;
        }

        public void HideForm(IHide h)
        {
            YLabel b = new YLabel {
                Text = h.GetText(),
                Font = new Font("微软雅黑", 9f, FontStyle.Bold),
                AutoSize = true
            };
            h.GetForm().Visible = false;
            b.Click += delegate {
                h.Click();
                h.GetForm().Visible = true;
                this.flowLayoutPanel1.Controls.Remove(b);
            };
            this.flowLayoutPanel1.Controls.Add(b);
        }

        public void InitForm()
        {
            this.toolStripStatusLabel1.Text = LoginUtil.ShowText();
            this.notifyIcon1.Visible = SysSet.NotifyShow;
            this.NMenu.Main = this;
            if (!this.NMenu.InitMenu())
            {
                base.Close();
            }
            else
            {
                LoginUtil.plugins["YTMain.Index"] = typeof(YTMain.Index);
                this.LoadPlug(SysSet.IndexPage, null, true);
                if ((SysSet.InitPlug != null) && (SysSet.InitPlug.Trim().Length > 0))
                {
                    this.LoadPlug(SysSet.InitPlug, null, false);
                }
                if (SysSet.InitRep == null)
                {
                    if (Ui.User != null)
                    {
                        this.LoadPlug("RepEdit.RepInit", new object[] { LoginUtil.IsGradeRight() ? 1 : 2, (Ui.User.DepCode == null) ? "0" : Ui.User.DepCode, (Ui.User.DepName == null) ? "管理机构" : Ui.User.DepName, (Ui.User.UserID == null) ? "0" : Ui.User.UserID, (Ui.User.UserName == null) ? "超级用户" : Ui.User.UserName }, false);
                    }
                }
                else
                {
                    this.LoadPlug("RepEdit.RepInit", SysSet.InitRep(), false);
                }
                this.SelectMenu();
                if (SysSet.AlertList.Count > 0)
                {
                    EventHandler ev = null;
                    StringBuilder sql = new StringBuilder();
                    List<YwAlert> li = new List<YwAlert>();
                    foreach (YwAlert alert in SysSet.AlertList)
                    {
                        if (Ui.HaveRight(alert.Right) || (alert.Right == 0))
                        {
                            sql.Append(alert.Url + ",");
                            li.Add(alert);
                        }
                    }
                    if (sql.Length > 0)
                    {
                        sql.Length--;
                        if (ev == null)
                        {
                            ev = delegate {
                                ServiceMsg msg = LData.Exe(new SvrParam("Finds", SysSet.AlertDbConn, sql.ToString()).Add("UserID", Ui.User.UserID).Add("DepCode", Ui.User.DepCode).Add("UserName", Ui.User.UserName).Add("DeptID", Ui.User.DeptID).Add("NowDate", DateTime.Now));
                                if ((msg != null) && msg.Success)
                                {
                                    int lll = 0;
                                    for (int j = 0; j < li.Count; j++)
                                    {
                                        DataTable dataTable = msg.GetDataTable(j);
                                        if ((dataTable != null) && (dataTable.Rows.Count > 0))
                                        {
                                            EventHandler ms = null;
                                            YwAlert al = li[j];
                                            string ll = dataTable.Rows[0][0];
                                            if (!"0".Equals(ll))
                                            {
                                                if (ms == null)
                                                {
                                                    ms = delegate {
                                                        lll += 0x1f40;
                                                        WJs.Delay(delegate {
                                                            AlertClick c = null;
                                                            if (WJs.isNull(al.Api))
                                                            {
                                                                WJs.alert_TaskSuc(string.Format(al.Msg, ll));
                                                            }
                                                            else
                                                            {
                                                                if (c == null)
                                                                {
                                                                    c = (AlertClick) (() => this.LoadPlug(al.Api, null, true));
                                                                }
                                                                WJs.alert_TaskSuc(string.Format(al.Msg, ll), c);
                                                            }
                                                        }, lll);
                                                    };
                                                }
                                                this.Invoke(ms);
                                            }
                                        }
                                    }
                                }
                            };
                        }
                        base.ExeEvent(ev);
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SysMain));
            this.menuStrip1 = new MenuStrip();
            this.系统ToolStripMenuItem = new ToolStripMenuItem();
            this.锁屏ToolStripMenuItem = new ToolStripMenuItem();
            this.注销ToolStripMenuItem = new ToolStripMenuItem();
            this.退出ToolStripMenuItem = new ToolStripMenuItem();
            this.日志ToolStripMenuItem = new ToolStripMenuItem();
            this.报表ToolStripMenuItem = new ToolStripMenuItem();
            this.菜单ToolStripMenuItem = new ToolStripMenuItem();
            this.清除本地缓存ToolStripMenuItem = new ToolStripMenuItem();
            this.重新下载字典数据ToolStripMenuItem = new ToolStripMenuItem();
            this.获取运行地址ToolStripMenuItem = new ToolStripMenuItem();
            this.panel1 = new Panel();
            this.label2 = new Label();
            this.label1 = new Label();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.打开主面板ToolStripMenuItem = new ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.退出系统ToolStripMenuItem = new ToolStripMenuItem();
            this.notifyIcon1 = new NotifyIcon(this.components);
            this.Expend = new ExpandableSplitter();
            this.statusStrip1 = new StatusStrip();
            this.toolStatus = new ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new ToolStripStatusLabel();
            this.toolStatusMsg = new ToolStripStatusLabel();
            this.timer1 = new Timer(this.components);
            this.tabStrip1 = new TabStrip();
            this.tabItem1 = new TabItem(this.components);
            this.timer2 = new Timer(this.components);
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.ytButton4 = new YtButton();
            this.ytButton3 = new YtButton();
            this.ytButton2 = new YtButton();
            this.ytButton1 = new YtButton();
            this.NMenu = new SysMenu();
            this.navigationPanePanel1 = new NavigationPanePanel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.NMenu.SuspendLayout();
            base.SuspendLayout();
            this.menuStrip1.BackgroundImage = (Image) manager.GetObject("menuStrip1.BackgroundImage");
            this.menuStrip1.Items.AddRange(new ToolStripItem[] { this.系统ToolStripMenuItem });
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(0x3d8, 0x19);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.锁屏ToolStripMenuItem, this.注销ToolStripMenuItem, this.退出ToolStripMenuItem, this.日志ToolStripMenuItem, this.报表ToolStripMenuItem, this.菜单ToolStripMenuItem, this.清除本地缓存ToolStripMenuItem, this.重新下载字典数据ToolStripMenuItem, this.获取运行地址ToolStripMenuItem });
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new Size(0x2c, 0x15);
            this.系统ToolStripMenuItem.Text = "系统";
            this.锁屏ToolStripMenuItem.Image = (Image) manager.GetObject("锁屏ToolStripMenuItem.Image");
            this.锁屏ToolStripMenuItem.Name = "锁屏ToolStripMenuItem";
            this.锁屏ToolStripMenuItem.Size = new Size(0xac, 0x16);
            this.锁屏ToolStripMenuItem.Text = "锁屏";
            this.锁屏ToolStripMenuItem.Click += new EventHandler(this.锁屏ToolStripMenuItem_Click);
            this.注销ToolStripMenuItem.Image = (Image) manager.GetObject("注销ToolStripMenuItem.Image");
            this.注销ToolStripMenuItem.Name = "注销ToolStripMenuItem";
            this.注销ToolStripMenuItem.Size = new Size(0xac, 0x16);
            this.注销ToolStripMenuItem.Text = "注销";
            this.注销ToolStripMenuItem.Click += new EventHandler(this.注销ToolStripMenuItem_Click);
            this.退出ToolStripMenuItem.Image = (Image) manager.GetObject("退出ToolStripMenuItem.Image");
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new Size(0xac, 0x16);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new EventHandler(this.退出ToolStripMenuItem_Click);
            this.日志ToolStripMenuItem.Name = "日志ToolStripMenuItem";
            this.日志ToolStripMenuItem.Size = new Size(0xac, 0x16);
            this.日志ToolStripMenuItem.Text = "日志";
            this.日志ToolStripMenuItem.Click += new EventHandler(this.日志ToolStripMenuItem_Click);
            this.报表ToolStripMenuItem.Name = "报表ToolStripMenuItem";
            this.报表ToolStripMenuItem.Size = new Size(0xac, 0x16);
            this.报表ToolStripMenuItem.Text = "报表";
            this.报表ToolStripMenuItem.Click += new EventHandler(this.报表ToolStripMenuItem_Click);
            this.菜单ToolStripMenuItem.Name = "菜单ToolStripMenuItem";
            this.菜单ToolStripMenuItem.Size = new Size(0xac, 0x16);
            this.菜单ToolStripMenuItem.Text = "菜单";
            this.菜单ToolStripMenuItem.Click += new EventHandler(this.菜单ToolStripMenuItem_Click);
            this.清除本地缓存ToolStripMenuItem.Name = "清除本地缓存ToolStripMenuItem";
            this.清除本地缓存ToolStripMenuItem.Size = new Size(0xac, 0x16);
            this.清除本地缓存ToolStripMenuItem.Text = "清除本地缓存";
            this.清除本地缓存ToolStripMenuItem.Visible = false;
            this.清除本地缓存ToolStripMenuItem.Click += new EventHandler(this.清除本地缓存ToolStripMenuItem_Click);
            this.重新下载字典数据ToolStripMenuItem.Image = (Image) manager.GetObject("重新下载字典数据ToolStripMenuItem.Image");
            this.重新下载字典数据ToolStripMenuItem.Name = "重新下载字典数据ToolStripMenuItem";
            this.重新下载字典数据ToolStripMenuItem.Size = new Size(0xac, 0x16);
            this.重新下载字典数据ToolStripMenuItem.Text = "重新下载字典数据";
            this.重新下载字典数据ToolStripMenuItem.Click += new EventHandler(this.重新下载字典数据ToolStripMenuItem_Click);
            this.获取运行地址ToolStripMenuItem.Name = "获取运行地址ToolStripMenuItem";
            this.获取运行地址ToolStripMenuItem.Size = new Size(0xac, 0x16);
            this.获取运行地址ToolStripMenuItem.Text = "获取运行地址";
            this.获取运行地址ToolStripMenuItem.Click += new EventHandler(this.获取运行地址ToolStripMenuItem_Click);
            this.panel1.Controls.Add(this.ytButton4);
            this.panel1.Controls.Add(this.ytButton3);
            this.panel1.Controls.Add(this.ytButton2);
            this.panel1.Controls.Add(this.ytButton1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0x19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x3d8, 0x2f);
            this.panel1.TabIndex = 1;
            this.label2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.Font = new Font("宋体", 10f, FontStyle.Bold);
            this.label2.ForeColor = Color.DarkGreen;
            this.label2.Location = new Point(310, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x95, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "企业QQ:  800013506";
            this.label2.Visible = false;
            this.label1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Font = new Font("宋体", 10f, FontStyle.Bold);
            this.label1.ForeColor = Color.DarkGreen;
            this.label1.Location = new Point(0x1d1, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xef, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "24小时免费服务电话:40066-13506";
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
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.Expend.BackColor = SystemColors.ControlLight;
            this.Expend.BackColor2 = Color.Empty;
            this.Expend.BackColor2SchemePart = eColorSchemePart.None;
            this.Expend.BackColorSchemePart = eColorSchemePart.None;
            this.Expend.ExpandFillColor = Color.FromArgb(0x98, 0xb5, 0xe2);
            this.Expend.ExpandFillColorSchemePart = eColorSchemePart.ItemPressedBackground;
            this.Expend.ExpandLineColor = Color.FromArgb(0x4b, 0x4b, 0x6f);
            this.Expend.ExpandLineColorSchemePart = eColorSchemePart.ItemPressedBorder;
            this.Expend.GripDarkColor = Color.FromArgb(0x4b, 0x4b, 0x6f);
            this.Expend.GripDarkColorSchemePart = eColorSchemePart.ItemPressedBorder;
            this.Expend.GripLightColor = Color.FromArgb(0xfc, 0xfc, 0xf9);
            this.Expend.GripLightColorSchemePart = eColorSchemePart.MenuBackground;
            this.Expend.HotBackColor = Color.FromArgb(0xe1, 230, 0xe8);
            this.Expend.HotBackColor2 = Color.Empty;
            this.Expend.HotBackColor2SchemePart = eColorSchemePart.None;
            this.Expend.HotBackColorSchemePart = eColorSchemePart.ItemCheckedBackground;
            this.Expend.HotExpandFillColor = Color.FromArgb(0x98, 0xb5, 0xe2);
            this.Expend.HotExpandFillColorSchemePart = eColorSchemePart.ItemPressedBackground;
            this.Expend.HotExpandLineColor = Color.FromArgb(0x4b, 0x4b, 0x6f);
            this.Expend.HotExpandLineColorSchemePart = eColorSchemePart.ItemPressedBorder;
            this.Expend.HotGripDarkColor = Color.FromArgb(0x4b, 0x4b, 0x6f);
            this.Expend.HotGripDarkColorSchemePart = eColorSchemePart.ItemPressedBorder;
            this.Expend.HotGripLightColor = Color.FromArgb(0xfc, 0xfc, 0xf9);
            this.Expend.HotGripLightColorSchemePart = eColorSchemePart.MenuBackground;
            this.Expend.Location = new Point(220, 0x48);
            this.Expend.Name = "Expend";
            this.Expend.Size = new Size(5, 0x238);
            this.Expend.Style = eSplitterStyle.Mozilla;
            this.Expend.TabIndex = 3;
            this.Expend.TabStop = false;
            this.statusStrip1.BackColor = Color.Transparent;
            this.statusStrip1.BackgroundImage = (Image) manager.GetObject("statusStrip1.BackgroundImage");
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.toolStatus, this.toolStripStatusLabel1, this.toolStripStatusLabel2, this.toolStatusMsg });
            this.statusStrip1.Location = new Point(0, 640);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(0x3d8, 0x16);
            this.statusStrip1.TabIndex = 5;
            this.toolStatus.AutoSize = false;
            this.toolStatus.Name = "toolStatus";
            this.toolStatus.Size = new Size(180, 0x11);
            this.toolStatus.Text = "2011";
            this.toolStatus.TextAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel1.BackgroundImage = (Image) manager.GetObject("toolStripStatusLabel1.BackgroundImage");
            this.toolStripStatusLabel1.BackgroundImageLayout = ImageLayout.None;
            this.toolStripStatusLabel1.ImageAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = RightToLeft.No;
            this.toolStripStatusLabel1.Size = new Size(0x44, 0x11);
            this.toolStripStatusLabel1.Text = "   当前用户";
            this.toolStripStatusLabel2.Font = new Font("微软雅黑", 9f, FontStyle.Bold);
            this.toolStripStatusLabel2.ForeColor = Color.Green;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new Size(20, 0x11);
            this.toolStripStatusLabel2.Text = "   ";
            this.toolStatusMsg.AutoSize = false;
            this.toolStatusMsg.BorderStyle = Border3DStyle.Bump;
            this.toolStatusMsg.Font = new Font("微软雅黑", 9f, FontStyle.Bold);
            this.toolStatusMsg.ForeColor = Color.Blue;
            this.toolStatusMsg.Name = "toolStatusMsg";
            this.toolStatusMsg.Size = new Size(0x2bd, 0x11);
            this.toolStatusMsg.Spring = true;
            this.toolStatusMsg.Text = "提示";
            this.toolStatusMsg.TextAlign = ContentAlignment.MiddleRight;
            this.timer1.Enabled = true;
            this.timer1.Interval = 0x3e8;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.tabStrip1.AutoSelectAttachedControl = true;
            this.tabStrip1.CanReorderTabs = true;
            this.tabStrip1.CloseButtonOnTabsVisible = true;
            this.tabStrip1.CloseButtonPosition = eTabCloseButtonPosition.Right;
            this.tabStrip1.CloseButtonVisible = true;
            this.tabStrip1.Dock = DockStyle.Top;
            this.tabStrip1.Location = new Point(0xe1, 0x48);
            this.tabStrip1.MdiAutoHide = false;
            this.tabStrip1.MdiForm = this;
            this.tabStrip1.MdiTabbedDocuments = true;
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.SelectedTab = this.tabItem1;
            this.tabStrip1.SelectedTabFont = new Font("宋体", 9f, FontStyle.Bold);
            this.tabStrip1.Size = new Size(0x2f7, 0x19);
            this.tabStrip1.Style = eTabStripStyle.Office2007Document;
            this.tabStrip1.TabAlignment = eTabStripAlignment.Top;
            this.tabStrip1.TabIndex = 0;
            this.tabStrip1.TabLayoutType = eTabLayoutType.FixedWithNavigationBox;
            this.tabStrip1.Tabs.Add(this.tabItem1);
            this.tabStrip1.Text = "tabStrip1";
            this.tabStrip1.TabItemClose += new TabStrip.UserActionEventHandler(this.tabStrip1_TabItemClose);
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "tabItem1";
            this.timer2.Interval = 500;
            this.timer2.Tick += new EventHandler(this.timer2_Tick);
            this.flowLayoutPanel1.BackColor = Color.Transparent;
            this.flowLayoutPanel1.Dock = DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = FlowDirection.BottomUp;
            this.flowLayoutPanel1.Location = new Point(0, 0x10);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new Size(0x3d8, 0x1f);
            this.flowLayoutPanel1.TabIndex = 11;
            this.ytButton4.AccessibleRole = AccessibleRole.PushButton;
            this.ytButton4.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.ytButton4.CacheKey = null;
            this.ytButton4.ColorTable = eButtonColor.OrangeWithBackground;
            this.ytButton4.DbConn = null;
            this.ytButton4.DefText = null;
            this.ytButton4.DefValue = null;
            this.ytButton4.EnableEmpty = true;
            this.ytButton4.FirstText = null;
            this.ytButton4.Fomart = null;
            this.ytButton4.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.ytButton4.Image = (Image) manager.GetObject("ytButton4.Image");
            this.ytButton4.Location = new Point(0x2cb, 9);
            this.ytButton4.Name = "ytButton4";
            this.ytButton4.Param = null;
            this.ytButton4.SelectedIndex = -1;
            this.ytButton4.Size = new Size(0x3d, 0x17);
            this.ytButton4.Sql = null;
            this.ytButton4.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ytButton4.TabIndex = 10;
            this.ytButton4.Text = "帮助";
            this.ytButton4.Value = null;
            this.ytButton4.Visible = false;
            this.ytButton4.Click += new EventHandler(this.ytButton4_Click);
            this.ytButton3.AccessibleRole = AccessibleRole.PushButton;
            this.ytButton3.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.ytButton3.CacheKey = null;
            this.ytButton3.ColorTable = eButtonColor.OrangeWithBackground;
            this.ytButton3.DbConn = null;
            this.ytButton3.DefText = null;
            this.ytButton3.DefValue = null;
            this.ytButton3.EnableEmpty = true;
            this.ytButton3.FirstText = null;
            this.ytButton3.Fomart = null;
            this.ytButton3.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.ytButton3.Image = (Image) manager.GetObject("ytButton3.Image");
            this.ytButton3.Location = new Point(0x30e, 9);
            this.ytButton3.Name = "ytButton3";
            this.ytButton3.Param = null;
            this.ytButton3.SelectedIndex = -1;
            this.ytButton3.Size = new Size(0x3d, 0x17);
            this.ytButton3.Sql = null;
            this.ytButton3.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ytButton3.TabIndex = 9;
            this.ytButton3.Text = "锁屏";
            this.ytButton3.Value = null;
            this.ytButton3.Click += new EventHandler(this.锁屏ToolStripMenuItem_Click);
            this.ytButton2.AccessibleRole = AccessibleRole.PushButton;
            this.ytButton2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.ytButton2.CacheKey = null;
            this.ytButton2.ColorTable = eButtonColor.OrangeWithBackground;
            this.ytButton2.DbConn = null;
            this.ytButton2.DefText = null;
            this.ytButton2.DefValue = null;
            this.ytButton2.EnableEmpty = true;
            this.ytButton2.FirstText = null;
            this.ytButton2.Fomart = null;
            this.ytButton2.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.ytButton2.Image = (Image) manager.GetObject("ytButton2.Image");
            this.ytButton2.Location = new Point(0x350, 9);
            this.ytButton2.Name = "ytButton2";
            this.ytButton2.Param = null;
            this.ytButton2.SelectedIndex = -1;
            this.ytButton2.Size = new Size(0x3d, 0x17);
            this.ytButton2.Sql = null;
            this.ytButton2.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ytButton2.TabIndex = 8;
            this.ytButton2.Text = "注销";
            this.ytButton2.Value = null;
            this.ytButton2.Click += new EventHandler(this.注销ToolStripMenuItem_Click);
            this.ytButton1.AccessibleRole = AccessibleRole.PushButton;
            this.ytButton1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.ytButton1.CacheKey = null;
            this.ytButton1.ColorTable = eButtonColor.OrangeWithBackground;
            this.ytButton1.DbConn = null;
            this.ytButton1.DefText = null;
            this.ytButton1.DefValue = null;
            this.ytButton1.EnableEmpty = true;
            this.ytButton1.FirstText = null;
            this.ytButton1.Fomart = null;
            this.ytButton1.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.ytButton1.Image = (Image) manager.GetObject("ytButton1.Image");
            this.ytButton1.Location = new Point(0x392, 9);
            this.ytButton1.Name = "ytButton1";
            this.ytButton1.Param = null;
            this.ytButton1.SelectedIndex = -1;
            this.ytButton1.Size = new Size(0x3d, 0x17);
            this.ytButton1.Sql = null;
            this.ytButton1.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ytButton1.TabIndex = 7;
            this.ytButton1.Text = "退出";
            this.ytButton1.Value = null;
            this.ytButton1.Click += new EventHandler(this.退出ToolStripMenuItem_Click);
            this.NMenu.CanCollapse = true;
            this.NMenu.ConfigureAddRemoveVisible = false;
            this.NMenu.ConfigureItemVisible = false;
            this.NMenu.ConfigureNavOptionsVisible = false;
            this.NMenu.ConfigureShowHideVisible = false;
            this.NMenu.Controls.Add(this.navigationPanePanel1);
            this.NMenu.Dock = DockStyle.Left;
            this.NMenu.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.NMenu.ItemPaddingBottom = 2;
            this.NMenu.ItemPaddingTop = 2;
            this.NMenu.Location = new Point(0, 0x48);
            this.NMenu.Main = null;
            this.NMenu.Name = "NMenu";
            this.NMenu.NavigationBarHeight = 0x26;
            this.NMenu.NoContextMenu = this.contextMenuStrip1;
            this.NMenu.Notify = this.notifyIcon1;
            this.NMenu.Padding = new System.Windows.Forms.Padding(1);
            this.NMenu.Size = new Size(220, 0x238);
            this.NMenu.Style = eDotNetBarStyle.StyleManagerControlled;
            this.NMenu.TabIndex = 2;
            this.NMenu.TitlePanel.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.NMenu.TitlePanel.Dock = DockStyle.Top;
            this.NMenu.TitlePanel.Font = new Font("Tahoma", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.NMenu.TitlePanel.Location = new Point(1, 1);
            this.NMenu.TitlePanel.Name = "panelTitle";
            this.NMenu.TitlePanel.Size = new Size(0xda, 0x18);
            this.NMenu.TitlePanel.Style.BackColor1.ColorSchemePart = eColorSchemePart.PanelBackground;
            this.NMenu.TitlePanel.Style.BackColor2.ColorSchemePart = eColorSchemePart.PanelBackground2;
            this.NMenu.TitlePanel.Style.Border = eBorderType.RaisedInner;
            this.NMenu.TitlePanel.Style.BorderColor.ColorSchemePart = eColorSchemePart.PanelBorder;
            this.NMenu.TitlePanel.Style.BorderSide = eBorderSide.Bottom;
            this.NMenu.TitlePanel.Style.ForeColor.ColorSchemePart = eColorSchemePart.PanelText;
            this.NMenu.TitlePanel.Style.GradientAngle = 90;
            this.NMenu.TitlePanel.Style.MarginLeft = 4;
            this.NMenu.TitlePanel.TabIndex = 0;
            this.NMenu.TitlePanel.Text = "buttonItem1";
            this.NMenu.ExpandedChanged += new ExpandChangeEventHandler(this.NMenu_ExpandedChanged);
            this.NMenu.ItemClick += new EventHandler(this.NMenu_ItemClick);
            this.navigationPanePanel1.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.navigationPanePanel1.Dock = DockStyle.Fill;
            this.navigationPanePanel1.Location = new Point(1, 1);
            this.navigationPanePanel1.Name = "navigationPanePanel1";
            this.navigationPanePanel1.ParentItem = null;
            this.navigationPanePanel1.Size = new Size(0xda, 0x210);
            this.navigationPanePanel1.Style.Alignment = StringAlignment.Center;
            this.navigationPanePanel1.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground;
            this.navigationPanePanel1.Style.BorderColor.ColorSchemePart = eColorSchemePart.PanelBorder;
            this.navigationPanePanel1.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText;
            this.navigationPanePanel1.Style.GradientAngle = 90;
            this.navigationPanePanel1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3d8, 0x296);
            base.Controls.Add(this.tabStrip1);
            base.Controls.Add(this.Expend);
            base.Controls.Add(this.NMenu);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.statusStrip1);
            base.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            base.IsMdiContainer = true;
            base.MainMenuStrip = this.menuStrip1;
            base.Name = "SysMain";
            this.Text = "SysMian";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.SysMian_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.NMenu.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void ll_Click(object sender, EventArgs e)
        {
            ToolStripStatusLabel label = sender as ToolStripStatusLabel;
            if (label != null)
            {
                UrlLink tag = label.Tag as UrlLink;
                WJs.RunExe(tag.Url);
            }
        }

        public IPlug LoadPlug(string pname, object[] param, bool isShow)
        {
            if ((pname.IndexOf('?') > -1) && (param == null))
            {
                string str = pname.Substring(pname.IndexOf('?') + 1);
                pname = pname.Substring(0, pname.IndexOf('?'));
                string[] strArray = str.Split(new char[] { ',' });
                object[] objArray = new object[strArray.Length];
                for (int i = 0; i < strArray.Length; i++)
                {
                    string[] strArray2 = strArray[i].Split(new char[] { '=' });
                    if (strArray2.Length > 1)
                    {
                        objArray[i] = strArray2[1];
                    }
                    else
                    {
                        objArray[i] = strArray2[0];
                    }
                }
                param = objArray;
            }
            try
            {
                Form openForm;
                if ((isShow && (this.CurrNode != null)) && (this.CurrNode.Value.IndexOf(pname) > -1))
                {
                    openForm = this.GetOpenForm(this.CurrNode.Value, this.CurrNode.Name);
                    if ((openForm != null) && this.CurrNode.IsOnlyOne)
                    {
                        openForm.Activate();
                        this.ShowStatus(openForm.Text);
                        this.CurrNode = null;
                        return null;
                    }
                }
                if (LoginUtil.plugins.ContainsKey(pname))
                {
                    System.Type type = LoginUtil.plugins[pname];
                    if (type != null)
                    {
                        IPlug plug = (IPlug) Activator.CreateInstance(type);
                        plug.initPlug(this, param);
                        openForm = plug.getMainForm();
                        if (openForm != null)
                        {
                            if ((openForm.Text != null) && (openForm.Text.IndexOf("Index") > -1))
                            {
                                openForm.Text = "主页";
                            }
                            if ((openForm.Tag != null) && (openForm.Tag is Control))
                            {
                                return plug;
                            }
                            if ((this.CurrNode != null) && this.CurrNode.IsDiag)
                            {
                                openForm.StartPosition = FormStartPosition.CenterScreen;
                                openForm.ShowDialog();
                            }
                            else
                            {
                                if (!openForm.IsMdiContainer)
                                {
                                    if (openForm.Tag == null)
                                    {
                                        openForm.Tag = pname;
                                        if ((this.CurrNode != null) && (((openForm.Text.IndexOf('-') <= 0) && (openForm.Text.IndexOf('[') <= 0)) && (openForm.Text.IndexOf('【') <= 0)))
                                        {
                                            openForm.Text = this.CurrNode.Name;
                                        }
                                    }
                                    if (isShow)
                                    {
                                        openForm.MdiParent = this;
                                        openForm.MaximizeBox = false;
                                        openForm.MinimizeBox = false;
                                        openForm.ControlBox = false;
                                        openForm.Location = new Point(0, 0);
                                        openForm.Width = ((base.Width - this.NMenu.Width) - 4) - this.Expend.Width;
                                        openForm.Height = ((base.Height - this.menuStrip1.Height) - this.tabStrip1.Height) - this.panel1.Height;
                                        openForm.FormBorderStyle = FormBorderStyle.Sizable;
                                        openForm.Activated += new EventHandler(this.f_Activated);
                                        this.ShowStatus(openForm.Text);
                                    }
                                }
                                if (isShow)
                                {
                                    openForm.Show();
                                }
                            }
                        }
                        this.CurrNode = null;
                        return plug;
                    }
                }
            }
            catch (Exception exception)
            {
                WJs.alert(exception.Message + "\n" + ((exception.InnerException != null) ? (exception.InnerException.Message + "\n" + exception.InnerException.StackTrace) : ""));
            }
            return null;
        }

        private void NMenu_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            this.Expend.Enabled = e.NewExpandedValue;
        }

        private void NMenu_ItemClick(object sender, EventArgs e)
        {
            this.SelectMenu();
        }

        public void Open(Node node)
        {
            NodeValue tag = node.Tag as NodeValue;
            if (tag != null)
            {
                this.CurrNode = tag;
                this.LoadPlug(tag.Value, null, true);
            }
            else
            {
                this.CurrNode = null;
            }
        }

        public void Open(Form f)
        {
            f.MdiParent = this;
            f.MaximizeBox = false;
            f.MinimizeBox = false;
            f.ControlBox = false;
            f.Width = ((base.Width - this.NMenu.Width) - 4) - this.Expend.Width;
            f.Height = ((base.Height - this.menuStrip1.Height) - this.tabStrip1.Height) - this.panel1.Height;
            if (f is IPlug)
            {
                (f as IPlug).initPlug(this, null);
            }
            f.Location = new Point(-3, -30);
            f.FormBorderStyle = FormBorderStyle.Sizable;
            f.Activated += new EventHandler(this.f_Activated);
            try
            {
                f.Show();
            }
            catch (Exception exception)
            {
                WJs.alert("打开窗体失败：" + exception.Message);
            }
        }

        public void SelectMenu()
        {
            if (this.MergeMenu != null)
            {
                while (this.menuStrip1.Items.Count > this.SysMenuCount)
                {
                    this.MergeMenu.Items.Add(this.menuStrip1.Items[this.menuStrip1.Items.Count - 1]);
                }
            }
            if ((this.NMenu.SelectedPanel != null) && (this.NMenu.SelectedPanel.Controls.Count > 0))
            {
                IMenu menu = this.NMenu.SelectedPanel.Controls[0] as IMenu;
                if (menu != null)
                {
                    menu.MenuSelect();
                    this.MergeMenu = menu.GetMergeMenu();
                    if ((this.MergeMenu != null) && (this.MergeMenu.Items.Count > 0))
                    {
                        while (this.MergeMenu.Items.Count > 0)
                        {
                            this.menuStrip1.Items.Add(this.MergeMenu.Items[this.menuStrip1.Items.Count - 1]);
                        }
                    }
                }
            }
        }

        public void SetMenuSelectIndex(int i)
        {
            this.NMenu.SetMenuSelected(i);
        }

        public void ShowStatus(string msg)
        {
            this.toolStatusMsg.Text = "提示：" + msg;
        }

        private void SysMian_Load(object sender, EventArgs e)
        {
            try
            {
                WJs.ClearLocalCache();
            }
            catch
            {
            }
            this.bz = WJs.AppStr("HelpDocUrl");
            if ((this.bz != null) && (this.bz.Trim().Length > 0))
            {
                this.ytButton4.Visible = true;
            }
            if (!SysSet.HideSysMenu)
            {
                this.日志ToolStripMenuItem.Visible = !WJs.IsClickOnceRun;
                this.报表ToolStripMenuItem.Visible = !WJs.IsClickOnceRun;
                this.菜单ToolStripMenuItem.Visible = !WJs.IsClickOnceRun;
            }
            else
            {
                this.日志ToolStripMenuItem.Visible = false;
                this.报表ToolStripMenuItem.Visible = false;
                this.菜单ToolStripMenuItem.Visible = false;
            }
            this.timer1.Enabled = true;
            this.SysMenuCount = this.menuStrip1.Items.Count;
            this.InitForm();
            this.s = (SysQd) WJs.DeserializeObject("Sys.db");
            if (this.s == null)
            {
                if (SysSet.IsRunStart)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\程序\" + SysSet.SysPath + @"\" + SysSet.SysName + ".appref-ms";
                    if (!File.Exists(path))
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\Programs\" + SysSet.SysPath + @"\" + SysSet.SysName + ".appref-ms";
                    }
                    WJs.RunWhenStart(true, SysSet.MainTitle, path);
                }
                this.s = new SysQd();
                this.s.IsReg = true;
                this.s.IsOut = false;
                this.s.IsAlert = true;
                WJs.SerializeObject(this.s, "Sys.db");
            }
        }

        private void tabStrip1_TabItemClose(object sender, TabStripActionEventArgs e)
        {
            if (this.tabStrip1.SelectedTabIndex == 0)
            {
                e.Cancel = true;
                this.ShowStatus("主页不能被关闭！");
            }
            else if (this.tabStrip1.SelectedTabIndex < base.MdiChildren.Length)
            {
                Form form = base.MdiChildren[this.tabStrip1.SelectedTabIndex];
                IPlug plug = form as IPlug;
                if ((plug != null) && !plug.unLoad())
                {
                    e.Cancel = true;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStatus.Text = DateTime.Now.ToString("yyyy-MM-dd ") + this.Week() + " " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void timer2_Tick(object sender, EventArgs e)
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
                            this.ExitSys();
                        }
                    }
                    else if (WJs.confirm(SysSet.ExitAlert))
                    {
                        this.ExitSys();
                    }
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void ytButton4_Click(object sender, EventArgs e)
        {
            WJs.RunExe(this.bz);
        }

        private void 报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadPlug("RepEdit.RepDes", null, true);
        }

        private void 菜单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadPlug("YtDict.dict.MenuManage?mName=" + SysSet.MenuName, null, true);
        }

        private void 打开主面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Show();
        }

        private void 获取运行地址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WJs.alert(Application.StartupPath);
        }

        private void 清除本地缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WJs.ClearLocalCache();
            WJs.alert("缓存已经清除！");
        }

        private void 日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadPlug("YtDict.dict.Log", null, true);
        }

        private void 锁屏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Hide();
            new SuoForm { sysMain = this }.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WJs.confirm(SysSet.ExitAlert))
            {
                this.ExitSys();
            }
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WJs.confirm(SysSet.ExitAlert))
            {
                this.ExitSys();
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

        private void 重新下载字典数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpDateDict.UseSys = "ALL";
            UpDateDict.UserID = Ui.User.UserID ?? "";
            UpDateDict.DepCode = Ui.User.DepCode;
            UpDateDict.DbConn = SysSet.UpdateDbConn;
            UpDateDict.IsQzUpdate = true;
            new SvrUpdate().ShowDialog();
            UpDateDict.IsQzUpdate = false;
            try
            {
                WJs.ClearLocalCache();
            }
            catch
            {
            }
        }

        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WJs.confirm("确定注销系统吗？"))
            {
                this.isZxSys = true;
                while (base.MdiChildren.Length > 0)
                {
                    base.MdiChildren[0].Close();
                }
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
                        this.ExitSys();
                    }
                }
                else
                {
                    YtMain.Main main2 = new YtMain.Main();
                    main2.ShowDialog();
                    this.isZxSys = false;
                    if (main2.Ok)
                    {
                        base.Show();
                        this.InitForm();
                    }
                    else
                    {
                        this.ExitSys();
                    }
                }
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                return base.CreateParams;
            }
        }
    }
}

