namespace YTMain.contrl
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MenuTree : UserControl
    {
        private DevComponents.AdvTree.AdvTree advTree1;
        private IContainer components = null;
        private ElementStyle elementStyle1;
        private NodeConnector nodeConnector1;

        public event TreeNodeMouseEventHandler NodeClick
        {
            add
            {
                this.advTree1.NodeClick += value;
            }
            remove
            {
                this.advTree1.NodeClick -= value;
            }
        }

        public MenuTree()
        {
            this.InitializeComponent();
            this.AllowDrop = false;
        }

        public void Add(Node node)
        {
            if (node.HasChildNodes)
            {
                Node[] nodes = new Node[node.Nodes.Count];
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    nodes[i] = node.Nodes[i];
                }
                this.advTree1.Nodes.AddRange(nodes);
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
            this.advTree1 = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new NodeConnector();
            this.elementStyle1 = new ElementStyle();
            this.advTree1.BeginInit();
            base.SuspendLayout();
            this.advTree1.AccessibleRole = AccessibleRole.Outline;
            this.advTree1.AllowDrop = true;
            this.advTree1.BackColor = SystemColors.Window;
            this.advTree1.BackgroundStyle.Class = "TreeBorderKey";
            this.advTree1.BackgroundStyle.CornerType = eCornerType.Square;
            this.advTree1.Dock = DockStyle.Fill;
            this.advTree1.Location = new Point(0, 0);
            this.advTree1.Name = "advTree1";
            this.advTree1.NodesConnector = this.nodeConnector1;
            this.advTree1.NodeStyle = this.elementStyle1;
            this.advTree1.PathSeparator = ";";
            this.advTree1.Size = new Size(150, 150);
            this.advTree1.Styles.Add(this.elementStyle1);
            this.advTree1.TabIndex = 0;
            this.advTree1.Text = "advTree1";
            this.nodeConnector1.LineColor = SystemColors.ControlText;
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = SystemColors.ControlText;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.advTree1);
            base.Name = "MenuTree";
            this.advTree1.EndInit();
            base.ResumeLayout(false);
        }
    }
}

