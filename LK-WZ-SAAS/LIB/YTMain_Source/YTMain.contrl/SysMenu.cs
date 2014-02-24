namespace YTMain.contrl
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml;
    using YiTian.util;
    using YtMain;
    using YTMain;
    using YtSys;
    using YtUtil.tool;
    using YtWinContrl.com.panel;

    public class SysMenu : NavigationPane
    {
        private Image Img;
        private bool IsNotify = false;
        private List<ButtonItem> itb = new List<ButtonItem>();
        private List<NavigationPanePanel> itp = new List<NavigationPanePanel>();

        public SysMenu()
        {
            this.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
        }

        private void addNode(Node node, XmlElement root)
        {
            XmlNodeList childNodes = root.ChildNodes;
            foreach (XmlNode node2 in childNodes)
            {
                XmlElement xe = (XmlElement) node2;
                string attribute = xe.GetAttribute("name");
                if (this.isHaveRight(xe))
                {
                    Node node3 = new Node(attribute) {
                        DragDropEnabled = false
                    };
                    NodeValue value2 = this.getByXmlElement(xe);
                    if ((value2.Ico != null) && (value2.Ico.Length > 0))
                    {
                        node3.Image = StringZip.DecompressToImg(value2.Ico);
                    }
                    else
                    {
                        node3.Image = this.Img;
                    }
                    node3.Tag = value2;
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

        private NodeValue getByXmlElement(XmlElement xe)
        {
            NodeValue value2 = new NodeValue(xe.GetAttribute("name"), xe.GetAttribute("typeName"), xe.GetAttribute("ico"), xe.GetAttribute("oico"));
            if (xe.HasAttribute("IsOnlyOne"))
            {
                value2.IsOnlyOne = "true".Equals(xe.GetAttribute("IsOnlyOne"));
                value2.IsShow = "true".Equals(xe.GetAttribute("IsShow"));
                value2.IsDiag = "true".Equals(xe.GetAttribute("IsDiag"));
            }
            else
            {
                value2.IsOnlyOne = true;
                value2.IsShow = true;
                value2.IsDiag = false;
            }
            if (xe.HasAttribute("right"))
            {
                value2.Right = xe.GetAttribute("right");
            }
            return value2;
        }

        private NavigationPanePanel GetNp(ButtonItem it)
        {
            NavigationPanePanel panel = new NavigationPanePanel {
                ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled,
                Dock = DockStyle.Fill,
                Location = new Point(1, 1),
                ParentItem = it,
                Size = new Size(0xe8, 0x1c1)
            };
            panel.Style.Alignment = StringAlignment.Center;
            panel.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground;
            panel.Style.BorderColor.ColorSchemePart = eColorSchemePart.PanelBorder;
            panel.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText;
            panel.Style.GradientAngle = 90;
            return panel;
        }

        public bool InitMenu()
        {
            if (this.itb.Count > 0)
            {
                for (int i = 0; i < this.itb.Count; i++)
                {
                    base.Items.Remove(this.itb[i]);
                    base.Controls.Remove(this.itp[i]);
                }
                this.itb.Clear();
                this.itp.Clear();
            }
            this.Img = Image.FromFile(Application.StartupPath + @"\Ico\t1.png");
            XmlDocument document = new XmlDocument();
            document.LoadXml(LoginUtil.MenuStr);
            XmlNodeList childNodes = document.SelectSingleNode("Plugs").ChildNodes;
            Node node2 = new Node();
            foreach (XmlNode node3 in childNodes)
            {
                XmlElement xe = (XmlElement) node3;
                string attribute = xe.GetAttribute("name");
                if (this.isHaveRight(xe))
                {
                    Node node = new Node(attribute) {
                        DragDropEnabled = false
                    };
                    NodeValue value2 = this.getByXmlElement(xe);
                    if ((value2.Ico != null) && (value2.Ico.Length > 0))
                    {
                        node.Image = StringZip.DecompressToImg(value2.Ico);
                    }
                    else
                    {
                        node.Image = this.Img;
                    }
                    node.Tag = value2;
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
            if (node2.Nodes.Count == 0)
            {
                WJs.alert("您没有任何权限，系统即将退出！");
                return false;
            }
            foreach (Node node5 in node2.Nodes)
            {
                NodeValue tag = node5.Tag as NodeValue;
                ButtonItem item = new ButtonItem();
                this.itb.Add(item);
                item.OptionGroup = "NmENU";
                item.Text = node5.Text;
                if ((tag != null) && ((tag.Ico != null) && (tag.Ico.Length > 0)))
                {
                    item.Image = StringZip.DecompressToImg(tag.Ico);
                }
                NavigationPanePanel np = this.GetNp(item);
                this.itp.Add(np);
                if (node5.Nodes.Count > 0)
                {
                    MenuTree tree = new MenuTree {
                        Dock = DockStyle.Fill
                    };
                    np.Controls.Add(tree);
                    tree.Add(node5);
                    tree.NodeClick += new TreeNodeMouseEventHandler(this.tree_Click);
                }
                else if (((tag != null) && (tag.Value != null)) && (tag.Value.Trim().Length > 0))
                {
                    Control openConTree = this.Main.GetOpenConTree(node5);
                    openConTree.Dock = DockStyle.Fill;
                    np.Controls.Add(openConTree);
                }
                base.Controls.Add(np);
                base.Items.Add(item);
            }
            string argStr = WJs.AppStr("DefSelectMenu");
            if (WJs.IsNum(argStr))
            {
                int num2 = int.Parse(argStr);
                if (num2 < base.Items.Count)
                {
                    (base.Items[num2] as ButtonItem).Checked = true;
                }
            }
            else
            {
                (base.Items[0] as ButtonItem).Checked = true;
            }
            base.NavigationBarHeight = node2.Nodes.Count * 0x26;
            if (!this.IsNotify && SysSet.NotifyShow)
            {
                this.IsNotify = true;
                string str3 = WJs.AppStr("NotifyMenu");
                if (str3 != null)
                {
                    string[] strArray = str3.Split(new char[] { ',' });
                    foreach (string str4 in strArray)
                    {
                        TreeNode node6 = null;
                        if (node6 != null)
                        {
                            NodeValue value4 = node6.Tag as NodeValue;
                            if (value4 != null)
                            {
                                int r = 0;
                                if (WJs.IsNum(value4.Right))
                                {
                                    r = int.Parse(value4.Right);
                                }
                                if (Ui.HaveRight(r) || (r == 0))
                                {
                                    ToolStripMenuItem item2 = new ToolStripMenuItem(str4);
                                    this.NoContextMenu.Items.Insert(0, item2);
                                }
                            }
                        }
                    }
                }
                this.Notify.Visible = true;
            }
            return true;
        }

        private bool isHaveRight(XmlElement xe)
        {
            if (xe.HasAttribute("right"))
            {
                string attribute = xe.GetAttribute("right");
                if (WJs.IsNum(attribute))
                {
                    return LoginUtil.HaveRight(int.Parse(xe.GetAttribute("right")));
                }
                if (LoginUtil.HaveRight(-1))
                {
                    return true;
                }
                bool flag = true;
                if (Ui.UInfo == null)
                {
                    return false;
                }
                string[] strArray = attribute.Split(new char[] { ';' });
                foreach (string str2 in strArray)
                {
                    string[] strArray2 = str2.Split(new char[] { ',' });
                    if (Ui.UInfo.Table.Columns.Contains(strArray2[0]))
                    {
                        if (strArray2[1].Equals(Ui.UInfo[strArray2[0]]))
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                    if (!flag)
                    {
                        return flag;
                    }
                }
                return flag;
            }
            return true;
        }

        public void SetMenuSelected(int index)
        {
            if ((index >= 0) && (index < base.Items.Count))
            {
                (base.Items[index] as ButtonItem).Checked = true;
            }
        }

        private void tree_Click(object o, TreeNodeMouseEventArgs e)
        {
            this.Main.Open(e.Node);
        }

        public SysMain Main { get; set; }

        public ContextMenuStrip NoContextMenu { get; set; }

        public NotifyIcon Notify { get; set; }
    }
}

