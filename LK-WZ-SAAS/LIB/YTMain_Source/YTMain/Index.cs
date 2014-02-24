namespace YTMain
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using YtClient;
    using YtMain;
    using YtPlugin;

    public class Index : NYtForm, IPlug
    {
        private IContainer components = null;

        public Index()
        {
            this.InitializeComponent();
            Image urlImg = LoginUtil.GetUrlImg("img/main.jpg");
            if (urlImg != null)
            {
                this.BackgroundImage = urlImg;
            }
            else
            {
                this.BackgroundImage = SysSet.MainImg;
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

        public Form getMainForm()
        {
            return this;
        }

        private void Index_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.ClientSize = new Size(0x2aa, 0x1c5);
            base.Name = "Index";
            this.Text = "Index";
            base.Load += new EventHandler(this.Index_Load);
            base.ResumeLayout(false);
        }

        public void initPlug(IAppContent app, object[] param)
        {
        }

        public bool unLoad()
        {
            return true;
        }
    }
}

