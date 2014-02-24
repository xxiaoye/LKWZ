namespace YTMain.contrl
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Form1 : Form
    {
        private Button button1;
        private IContainer components = null;
        private ToolStripMenuItem ddToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem sssToolStripMenuItem;
        private ToolStripMenuItem sssToolStripMenuItem1;

        public Form1()
        {
            this.InitializeComponent();
            base.ControlBox = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.menuStrip1 = new MenuStrip();
            this.sssToolStripMenuItem = new ToolStripMenuItem();
            this.sssToolStripMenuItem1 = new ToolStripMenuItem();
            this.ddToolStripMenuItem = new ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.button1.Location = new Point(0x77, 0x67);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.menuStrip1.Items.AddRange(new ToolStripItem[] { this.sssToolStripMenuItem });
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(0x11c, 0x19);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            this.sssToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.sssToolStripMenuItem1, this.ddToolStripMenuItem });
            this.sssToolStripMenuItem.Name = "sssToolStripMenuItem";
            this.sssToolStripMenuItem.Size = new Size(0x26, 0x15);
            this.sssToolStripMenuItem.Text = "sss";
            this.sssToolStripMenuItem1.Name = "sssToolStripMenuItem1";
            this.sssToolStripMenuItem1.Size = new Size(0x98, 0x16);
            this.sssToolStripMenuItem1.Text = "sss";
            this.ddToolStripMenuItem.Name = "ddToolStripMenuItem";
            this.ddToolStripMenuItem.Size = new Size(0x98, 0x16);
            this.ddToolStripMenuItem.Text = "dd";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x11c, 0x106);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.menuStrip1);
            base.MainMenuStrip = this.menuStrip1;
            base.Name = "Form1";
            this.Text = "Form1";
            base.Load += new EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

