using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtUtil.tool;
using YtClient;
using ChSys;

namespace StatQuery.form
{
    public partial class WZStockAlarm_Set : Form
    {
        public WZStockAlarm_Set()
        {
            InitializeComponent();
        }

         DataRow r;

        //DataTable dt;
        //string t;
        public WZStockAlarm_Set(DataRow r)
        {
            this.r = r;
            InitializeComponent();
        }
        public bool isSc = false;
        public WZStockAlarm Main;
        private void WZStockAlarm_Set_Load(object sender, EventArgs e)
        {
           
            this.yTextBox_WZName.Text = r["WZNAME"].ToString();
            this.yTextBox_KCUp.Text = r["NUMSX"].ToString();
            this.yTextBox_KCDown.Text = r["NUMXX"].ToString();

            this.yTextBox_WZName.ReadOnly = true;

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.yTextBox_KCUp.Text = "";
            this.yTextBox_KCDown.Text = "";
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (this.yTextBox_KCUp.Text.Trim().Length == 0)
            {
                WJs.alert("请填写库存上限值！");
                this.yTextBox_KCUp.Focus();
            }
            else if (!decimal.TryParse(this.yTextBox_KCUp.Text.ToString(), out a))
            {
                WJs.alert("填写库存上限值格式错误！");
                this.yTextBox_KCUp.Focus();
            }

            if (this.yTextBox_KCDown.Text.Trim().Length == 0)
            {
                WJs.alert("请填写库存上限值！");
                this.yTextBox_KCDown.Focus();
            }
            else if (!decimal.TryParse(this.yTextBox_KCDown.Text.ToString(), out b))
            {
                WJs.alert("填写库存上限值格式错误！");
                this.yTextBox_KCDown.Focus();
            }


            ActionLoad ac = ActionLoad.Conn();

            ac.Add("STOCKID", r["STOCKID"].ToString());

            ac.Action = "LKWZSVR.lkwz.StatQuery.WZStockAlarmSvr";
            ac.Sql = "SetWZStockAlarmStockInfo";
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("NUMSX", this.yTextBox_KCUp.Text.ToString());
            ac.Add("NUMXX", this.yTextBox_KCDown.Text.ToString());
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
            ac.Post();

        }

        void ac_ServiceFaiLoad(object sender, YtClient.data.events.LoadFaiEvent e)
        {
            WJs.alert(e.Msg.Msg);
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {

            WJs.alert(e.Msg.Msg);

            //WZPDZ.ReLoadData();

            isSc = true;
            this.Close();

        }

        }
    
}
