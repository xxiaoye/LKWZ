namespace YTMain.tool
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using YTMain;
    using YtUtil.tool;

    public class SetForm : Form
    {
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        public bool IsAlert;
        public bool isOk;
        public bool IsOut;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;

        public SetForm()
        {
            this.InitializeComponent();
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
            if (this.radioButton4.Checked)
            {
                o.IsReg = true;
                WJs.RunWhenStart(true, SysSet.MainTitle, Application.StartupPath + @"\" + SysSet.ExeName + ".exe");
            }
            else
            {
                o.IsReg = false;
                WJs.RunWhenStart(false, SysSet.MainTitle, Application.StartupPath + @"\" + SysSet.ExeName + ".exe");
            }
            WJs.SerializeObject(o, "Sys.db");
            MessageBox.Show("应用已生效！", "提示");
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SetForm));
            this.groupBox1 = new GroupBox();
            this.checkBox1 = new CheckBox();
            this.radioButton2 = new RadioButton();
            this.radioButton1 = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.radioButton3 = new RadioButton();
            this.radioButton4 = new RadioButton();
            this.button1 = new Button();
            this.button2 = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(410, 0x5b);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "关闭提示设置";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(0x2b, 0x45);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(120, 0x10);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "关闭程序时不提示";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(0x2b, 0x2a);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x47, 0x10);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "退出程序";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new Point(0x2b, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0xcb, 0x10);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "最小化到系统托盘区，不退出程序";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Location = new Point(12, 0x6d);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(410, 0x47);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "开机启动";
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new Point(0x2b, 0x2a);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new Size(0x53, 0x10);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "开机不启动";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new Point(0x2b, 20);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new Size(0x6b, 0x10);
            this.radioButton4.TabIndex = 2;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "开机随系统启动";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.button1.Location = new Point(0x10a, 0xc5);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 5;
            this.button1.Text = "应用(&Q)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(0x15b, 0xc5);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 6;
            this.button2.Text = "取消(&C)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1b2, 0xe8);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "SetForm";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "设置";
            base.Load += new EventHandler(this.SetForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

        private void SetForm_Load(object sender, EventArgs e)
        {
            SysQd qd = (SysQd) WJs.DeserializeObject("Sys.db");
            if (qd != null)
            {
                this.radioButton2.Checked = qd.IsOut;
                this.radioButton1.Checked = !qd.IsOut;
                this.checkBox1.Checked = !qd.IsAlert;
                this.radioButton4.Checked = qd.IsReg;
                this.radioButton3.Checked = !qd.IsReg;
            }
        }
    }
}

