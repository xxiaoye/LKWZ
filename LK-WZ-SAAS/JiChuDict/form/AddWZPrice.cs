using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YiTian.db;
using YtWinContrl.com.datagrid;
using YtWinContrl.com;
using ChSys;
using YtUtil.tool;
using YtClient;

namespace JiChuDict.form
{
    public partial class AddWZPrice : Form
    {
        public WZPriceManag Main;
        private YtWinContrl.com.datagrid.DataGView dataGViewPL;
        Dictionary<string, ObjItem> dr;
        bool isAdd;
        public bool isOk = false;
        TvList xmList;
        TvList jxList;
        public AddWZPrice()
        {
            isAdd = true;
            InitializeComponent();
        }
        public AddWZPrice(YtWinContrl.com.datagrid.DataGView gv,
                   Dictionary<string, ObjItem> dr, TvList xmList, TvList jxList)
        {
            isAdd = false;
            this.dr = dr;
            this.xmList = xmList;
            this.jxList = jxList;
            this.dataGViewPL = gv;
            InitializeComponent();
        }
        private void AddWZPrice_Load(object sender, EventArgs e)
        {
            ControlUtil.RegKeyEnter(this);
            /*
            ifall_ytComboBox.Items.Add("是");
            ifall_ytComboBox.Items.Add("否");
            ifuse_ytComboBox.Items.Add("是");
            ifuse_ytComboBox.Items.Add("否");
             * */

            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ifuse_ytComboBox);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ifall_ytComboBox);
           // TvList.newBind().add("加价", "1").add("不加价", "0").Bind(this.rate_ytComboBox);

            this.choscode_yTextBox.Text = His.his.Choscode.ToString();
            this.userid_yTextBox.Text = His.his.UserId.ToString();
            this.username_yTextBox.Text = His.his.UserName;
            this.ifuse_ytComboBox.SelectedIndex = 0;

            pricename_yTextBox.TextChanged += new EventHandler(pricename_yTextBox_TextChanged);
            this.pricename_yTextBox.Focus();
            if (isAdd)
            {
                this.ifuse_ytComboBox.SelectedIndex = 0;
                this.ifall_ytComboBox.SelectedIndex = 0;
            }
            if (!isAdd)
            {
                this.priceid_yTextBox.Text = dr["计价体系id"].ToString();
                this.pricename_yTextBox.Text = dr["计价体系名称"].ToString();
                this.rate_yTextBox.Text = dr["加价率"].ToString();
                TvList.setSelectIndex(this.ifall_ytComboBox, dr["是否统一浮动"].ToString());
                //TvList.setSelectIndex(this.rate_ytComboBox, dr["加价率"].ToString());
                TvList.setSelectIndex(this.ifuse_ytComboBox, dr["是否使用"].ToString());
                this.memo_yTextBox.Text = dr["备注"].ToString();
                this.pycode_yTextBox5.Text = dr["拼音码"].ToString();
                this.wbcode_yTextBox5.Text = dr["五笔码"].ToString();

               
            }


        }
        void pricename_yTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.pricename_yTextBox.Text.Trim().Length > 0)
            {

                pycode_yTextBox5.Text = PyWbCode.getPyCode(this.pricename_yTextBox.Text).ToLower();
                wbcode_yTextBox5.Text = PyWbCode.getWbCode(this.pricename_yTextBox.Text).ToLower();
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (this.pricename_yTextBox.Text.Trim().Length == 0)
            {
                WJs.alert("请输入计价体系名称！");
                pricename_yTextBox.Focus();
                return;
            }
            //if(this.pricename_yTextBox.Text.Trim())
            if (this.ifall_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请设置价格是否统一浮动！");
                ifall_ytComboBox.Focus();
                return;
            }
            if (this.rate_yTextBox.Text.Trim().Length==0)
            {
                WJs.alert("请设置加价率！");
                rate_yTextBox.Focus();
                return;
            }
            if (!WJs.IsNum(this.rate_yTextBox.Text.Trim()))
            {
                WJs.alert("加价率必须是数字！");
                rate_yTextBox.Focus();
                return;
            }
            if (this.ifuse_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请设置是否使用！");
                ifuse_ytComboBox.Focus();
                return;
            }
            if (this.userid_yTextBox.Text.Trim().Length == 0)
            {
                WJs.alert("请输入操作员ID！");
                userid_yTextBox.Focus();
                return;
            }
            if (this.username_yTextBox.Text.Trim().Length == 0)
            {
                WJs.alert("请输入操作员名称！");
                username_yTextBox.Focus();
                return;
            }

            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.his.WZPriceManag.WZPrice";
            ac.Sql = "Save";
            ac.Add("CHOSCODE", this.choscode_yTextBox.Text);
            ac.Add("PRICENAME", this.pricename_yTextBox.Text);

            
            ac.Add("IFALL",TvList.getValue(this.ifall_ytComboBox).ToInt());
           // ac.Add("RATE", TvList.getValue(this.rate_ytComboBox).ToInt());
            ac.Add("RATE", this.rate_yTextBox.Text);
          
            ac.Add("USERID", this.userid_yTextBox.Text);
            ac.Add("USERNAME", this.username_yTextBox.Text);
            ac.Add("PYCODE", this.pycode_yTextBox5.Text);
            ac.Add("WBCODE", this.wbcode_yTextBox5.Text);
            ac.Add("IFUSE",TvList.getValue(this.ifuse_ytComboBox).ToInt());
            ac.Add("RECDATE", this.dateTimePicker1.Value);
            ac.Add("MEMO", this.memo_yTextBox.Text);

            if (!isAdd)
            {
                ac.Add("PRICEID", dr["计价体系id"].ToString());
              
            }
            
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();

        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            //isOk = true;
            //WJs.alert(e.Msg.Msg);
            if (this.isAdd)
            {
                if (e.Msg.Msg.Equals("已经存该计价体系信息！") || e.Msg.Msg.Equals("已经存在该计价体系信息，不能修改成该名称！"))
                {
                    WJs.alert(e.Msg.Msg);
                    //InitForm();
                }
                 else if (!WJs.confirm("保存成功，是否继续添加？"))
                {
                    this.Close();
                }
                else
                {
                    InitForm();
                }
            }
            else
            {
                WJs.alert(e.Msg.Msg);
                this.Close();
            }

        }
        private void InitForm()
        {
            //this.priceid_yTextBox.Text = "";
            this.pricename_yTextBox.Text = "";
            this.pycode_yTextBox5.Text = "";
            this.wbcode_yTextBox5.Text = "";
            this.memo_yTextBox.Text = "";
            this.rate_yTextBox.Text = "";

            this.ifall_ytComboBox.SelectedItem = "";
            this.ifuse_ytComboBox.SelectedItem = "";
           // this.rate_ytComboBox.SelectedItem = "";

            //this.ifall_ytComboBox.SelectedIndex = -1;
            //this.ifuse_ytComboBox.SelectedIndex = -1;
            //this.rate_ytComboBox.SelectedIndex = -1;
            this.ifuse_ytComboBox.SelectedIndex = 0;
            this.ifall_ytComboBox.SelectedIndex = 0;
            this.pricename_yTextBox.Focus();
        }
        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
