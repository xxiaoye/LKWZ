namespace YTMain.tool
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using YTMain;
    using YtUtil.tool;

    public class AlertForm : Form
    {
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
        private IContainer components = null;
        public bool IsAlert;
        public bool isOk;
        public bool IsOut;
        public Label label1;
        private Panel panel1;
        private PictureBox pictureBox1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;

        public AlertForm()
        {
            this.InitializeComponent();
        }

        private void AlertForm_Load(object sender, EventArgs e)
        {
            SysQd qd = (SysQd) WJs.DeserializeObject("Sys.db");
            this.radioButton2.Checked = qd.IsOut;
            this.radioButton1.Checked = !qd.IsOut;
            this.checkBox1.Checked = !qd.IsAlert;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.IsAlert = !this.checkBox1.Checked;
            this.IsOut = this.radioButton2.Checked;
            SysQd o = new SysQd {
                IsReg = true,
                IsOut = this.IsOut,
                IsAlert = this.IsAlert
            };
            this.isOk = true;
            WJs.SerializeObject(o, "Sys.db");
            base.Close();
        }

        private void button2_Click(object sender, EventArgs e)
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AlertForm));
            this.radioButton1 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.checkBox1 = new CheckBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.pictureBox1 = new PictureBox();
            this.panel1 = new Panel();
            this.label1 = new Label();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new Point(0x60, 30);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0xcb, 0x10);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "最小化到系统托盘区，不退出程序";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(0x60, 0x34);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x47, 0x10);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "退出程序";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(0x20, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x48, 0x10);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "不再提示";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.button1.Location = new Point(0xdf, 6);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定(&Q)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(0x130, 6);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 4;
            this.button2.Text = "取消(&C)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.pictureBox1.BackgroundImage = (Image) manager.GetObject("pictureBox1.BackgroundImage");
            this.pictureBox1.BackgroundImageLayout = ImageLayout.None;
            this.pictureBox1.Location = new Point(0x17, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x38, 0x2b);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.panel1.BackColor = SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x18b, 0x22);
            this.panel1.TabIndex = 6;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x60, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x89, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "你点击了关闭，你是想：";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x18b, 0x76);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.radioButton1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AlertForm";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "关闭提示";
            base.Load += new EventHandler(this.AlertForm_Load);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

