namespace YtMain
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using YtClient;
    using YTMain;
    using YtPlugin;
    using YtUtil.tool;
    using YtWinContrl.com.contrl;

    public class SuoForm : Form
    {
        private Button button1;
        private Button button2;
        private bool closeOk = false;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel1;
        private PictureBox pictureBox1;
        public Form sysMain;
        private TextBox textBox1;

        public SuoForm()
        {
            this.InitializeComponent();
            this.BackgroundImage = SysSet.MainImg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Equals(ClientState.Pwd))
            {
                this.closeOk = true;
                base.Close();
                this.sysMain.Show();
            }
            else
            {
                WJs.alert("密码错误，请重新输入！");
                this.textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IAppContent sysMain = this.sysMain as IAppContent;
            if (sysMain != null)
            {
                sysMain.ExitSys();
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SuoForm));
            this.button1 = new Button();
            this.textBox1 = new YTextBox();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.pictureBox1 = new PictureBox();
            this.button2 = new Button();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.button1.Location = new Point(0x112, 0x6c);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x41, 0x17);
            this.button1.TabIndex = 5;
            this.button1.Text = "进入系统";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.textBox1.Location = new Point(0x77, 0x6d);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new Size(0x92, 0x15);
            this.textBox1.TabIndex = 4;
            this.textBox1.KeyDown += new KeyEventHandler(this.textBox1_KeyDown);
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 10f);
            this.label3.Location = new Point(0x2e, 0x4f);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4d, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "当前用户：";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10f);
            this.label2.Location = new Point(0x2e, 110);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户密码：";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 11f, FontStyle.Bold);
            this.label1.Location = new Point(0x38, 0x30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x117, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "系统进入锁屏状态，请输入密码解锁！";
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new Point(0x149, 0x130);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x16d, 0xae);
            this.panel1.TabIndex = 1;
            this.pictureBox1.Image = (Image) manager.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(0x10, 0x17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(50, 50);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.button2.Location = new Point(0x112, 0x4f);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x41, 0x17);
            this.button2.TabIndex = 7;
            this.button2.Text = "退出系统";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.ClientSize = new Size(0x3e5, 0x24a);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "SuoForm";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Load += new EventHandler(this.SuoForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
        }

        private void SuoForm_Load(object sender, EventArgs e)
        {
            this.label3.Text = LoginUtil.SuoText();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(null, null);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (((m.Msg != 0x112) || (((int) m.WParam) != 0xf060)) || this.closeOk)
            {
                base.WndProc(ref m);
            }
        }
    }
}

