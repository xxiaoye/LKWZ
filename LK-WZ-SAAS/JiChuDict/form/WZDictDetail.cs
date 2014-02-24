using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtWinContrl.com.datagrid;
using YtClient;
using ChSys;
using YtUtil.tool;
using YtPlugin;

namespace JiChuDict.form
{
    public partial class WZDictDetail : Form 
    {
        
        public WZDictDetail()
        {
            InitializeComponent();
        }
        DataRow r;
        int a;
        decimal decm;
        private bool isAdd;
        public WZDictDetail(DataRow r, bool _isAdd)
        {
            isAdd = _isAdd;
            this.r = r;
            InitializeComponent();
        }
        public bool isSc = false;
        public WZDictManag WZDMang;
        private void WZDictDetail_Load(object sender, EventArgs e)
        {
            this.selTextInpt1.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode ;//为什么要这样写？
            selTextInpt1.BxSr = false;//必须输入查询关键字
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt1.Sql = "WZDict_SupplyID";
            //this.yTextBox_WZID.Enabled = false;
            this.yTextBox_ChCode.Enabled = false;

            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ytComboBox_IfMake);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ytComboBox_IfAfford);
          //  TvList.newBind().Load("WZDictDetial_MakeID", new object[] { His.his.Choscode}).Bind(this.ytComboBox_MakeID);
            //TvList.newBind().SetCacheKey("cjid").Load("WZDictDetial_MakeID", new object[] { His.his.Choscode }).Bind(this.ytComboBox_MakeID);
           
          //  this.ytComboBox_MakeID.SelectedIndexChanged += new EventHandler(yTextBox_MakeName_TextChanged);
          //  this.ytComboBox_MakeID.SelectedIndexChanged += new EventHandler(yTextBox_IfMake_TextChanged);
           // this.ytComboBox_MakeID.SelectedIndexChanged += new EventHandler(yTextBox_IfAfford_TextChanged);
            
            this.selTextInpt1.TextChanged += new EventHandler(yTextBox_MakeName_TextChanged);
            this.selTextInpt1.TextChanged += new EventHandler(yTextBox_IfMake_TextChanged);
            this.selTextInpt1.TextChanged += new EventHandler(yTextBox_IfAfford_TextChanged);
            if (r != null)
            {
                //if (r["WZID"].ToString().Trim().Length > 0)
                //{
                    this.yTextBox_WZID.Text = r["WZID"].ToString();
                //}
            }
            if (!this.isAdd)
            {


                this.selTextInpt1.Value = r["SUPPLYID"].ToString();
                this.selTextInpt1.Text = r["SUPPLYID"].ToString();
              //  this.ytComboBox_MakeID.Value =  r["SUPPLYID"].ToString();
                this.yTextBox_MakeName.Text = r["SUPPLYNAME"].ToString();
                this.ytComboBox_IfMake.Value = r["IFFACTORY"].ToString();
                this.ytComboBox_IfAfford.Value = r["IFSUPPLY"].ToString();
                this.yTextBox_ChCode.Text = r["CHOSCODE"].ToString();
                //if (r["SUPPLYID"] != null)
                //{
                    //this.ytComboBox_MakeID.Text = r["SUPPLYID"].ToString();
                //}

            }
            else
            {


                this.yTextBox_ChCode.Text = His.his.Choscode;

            }
        }
        void yTextBox_MakeName_TextChanged(object sender, EventArgs e)
        {
            
            string n = this.selTextInpt1.Text.Trim();
            if (n.Length > 0 && decimal.TryParse(n,out decm))
            {
                n = LData.Exe("WZDictDetial_MakeName", "LKWZ", new object[] { His.his.Choscode, Convert.ToDecimal(n) });
                if (n != null)
                {
                    this.yTextBox_MakeName.Text = n.ToString();

                }

            }
        }
        void yTextBox_IfMake_TextChanged(object sender, EventArgs e)
        {
           string n = this.selTextInpt1.Text.Trim();
           if (n.Length > 0 && decimal.TryParse(n, out decm))
            {
                n = LData.Exe("WZDictDetial_IfMake", "LKWZ", new object[] { His.his.Choscode, Convert.ToDecimal(n) });
                if (n != null)
                {
                    this.ytComboBox_IfMake.Value = n.ToString();

                }

            }
        }
        void yTextBox_IfAfford_TextChanged(object sender, EventArgs e)
        {
            string n = this.selTextInpt1.Text.Trim();
            if (n.Length > 0 && decimal.TryParse(n, out decm))
            {
                n = LData.Exe("WZDictDetial_IfAfford", "LKWZ", new object[] { His.his.Choscode, Convert.ToDecimal(n) });
                if (n != null)
                {
                    this.ytComboBox_IfAfford.Value = n.ToString();

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.yTextBox_WZID.Text.Trim().Length == 0)
            {
                WJs.alert("请输入物资ID！");
                this.yTextBox_WZID.Focus();
                return;
            }
            if (this.ytComboBox_IfAfford.SelectedIndex == -1)
            {
                WJs.alert("请选择是否供应商！");
                ytComboBox_IfAfford.Focus();
                return;
            }
            if (this.ytComboBox_IfMake.SelectedIndex == -1)
            {
                WJs.alert("请选择是否生产厂家！");
                ytComboBox_IfMake.Focus();
                return;
            }
            if (this.yTextBox_MakeName.Text.ToString ().Trim().Length == 0)
            {
                WJs.alert("请输入生产厂家名称！");
                this.yTextBox_MakeName.Focus();
                return;
            }
            if (this.selTextInpt1.Text.Trim().Length > 0)
            {
                string valid = LData.Es("WZDict_SupplyID_Vlid", "LKWZ", new object[] { this.selTextInpt1.Text.Trim().ToString(), His.his.Choscode });
                if (valid == null)
                {
                    WJs.alert("生产厂商ID输入错误！");
                    this.selTextInpt1.Focus();
                    return;
                }
            }
            else
            {
                WJs.alert("请输入生产厂家ID！");
                this.selTextInpt1.Focus();
                return;
            }
            ActionLoad acl = ActionLoad.Conn();
           
            acl.Action = "LKWZSVR.lkwz.JiChuDict.WZDictDetailSvr";
            acl.Sql = "SaveDictDetailWZInfo";
            acl.DbConn = "LKWZ";
            
            acl.Add("WZID", this.yTextBox_WZID.Text);
            acl.Add("SUPPLYNAME", this.yTextBox_MakeName.Text);
            //acl.Add("SUPPLYID", TvList.getValue(this.ytComboBox_MakeID));
            //acl.Add("SUPPLYID", TvList.getValue(this.ytComboBox_MakeID).ToInt());
            acl.Add("SUPPLYID", this.selTextInpt1.Text.ToString());
            acl.Add("IFFACTORY", TvList.getValue(this.ytComboBox_IfMake).ToInt());
            acl.Add("IFSUPPLY", TvList.getValue(this.ytComboBox_IfAfford).ToInt());
            acl.Add("CHOSCODE", His.his.Choscode);
            if (isAdd)
               { a = 1; }
            else { a = 0; }
            acl.Add("ISADD", a);//标志位，不然到服务端不知是新增还是修改
            acl.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            acl.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
           
                acl.Post(); 
        
            
        }
        void ac_ServiceFaiLoad(object sender, YtClient.data.events.LoadFaiEvent e)
        {
            WJs.alert(e.Msg.Msg);
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {

            WJs.alert(e.Msg.Msg);
            WZDMang.ReLoadDataDetail(this.yTextBox_WZID.Text.ToString());
            this.Close();
            //if (!isAdd || !WJs.confirm("是否继续添加物资信息？"))
            //{
            //    isSc = true;
            //    this.Close();
            //}
            //else
            //{
            //    this.ytComboBox_IfAfford.SelectedIndex = -1;
            //    this.ytComboBox_IfMake.SelectedIndex = -1;
            //    this.yTextBox_MakeName.Clear();
            //    this.selTextInpt1.Value = null;
            //    this.selTextInpt1.Text = null;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isSc = false;//什么标志位
            this.Close();
        }


   
    }
    }

