namespace YtMain
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using YiTian.log;
    using YtWinContrl.com.contrl;

    public class LogLook : Form
    {
        private Button button1;
        private IContainer components = null;
        private Label label1;
        private YTextBox textBox1;

        public LogLook()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            this.textBox1 = new YTextBox();
            this.button1 = new Button();
            this.label1 = new Label();
            base.SuspendLayout();
            this.textBox1.Dock = DockStyle.Top;
            this.textBox1.Location = new Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = ScrollBars.Both;
            this.textBox1.Size = new Size(0x259, 330);
            this.textBox1.TabIndex = 0;
            this.button1.Location = new Point(0x2c, 0x173);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label1.AutoSize = true;
            this.label1.ForeColor = SystemColors.ActiveCaption;
            this.label1.Location = new Point(0xaf, 0x17d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x259, 0x1bf);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.textBox1);
            base.Name = "LogLook";
            this.Text = "LogLook";
            base.Load += new EventHandler(this.LogLook_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LogLook_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = Log.get();
        }
    }
}

