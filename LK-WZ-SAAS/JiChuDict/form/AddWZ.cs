using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtUtil.tool;
using YtWinContrl.com;
using YtWinContrl.com.datagrid;
using YiTian.db;
using ChSys;
using YtClient;

namespace JiChuDict.form
{
    public partial class AddWZ : Form
    {
        public WZWareManag Main;
        private YtWinContrl.com.datagrid.DataGView dataGViewPL;
        Dictionary<string, ObjItem> dr;
        bool isAdd;
        public bool isOk = false;
        string wd;
        string ms;
        TvList xmList;
        TvList jxList;

        public AddWZ()
        {
            isAdd = true;
            InitializeComponent();
        }
        public AddWZ(YtWinContrl.com.datagrid.DataGView gv,
                   Dictionary<string, ObjItem> dr, TvList xmList, TvList jxList)
        {
            isAdd = false;
            this.dr = dr;
            this.xmList = xmList;
            this.jxList = jxList;
            this.dataGViewPL = gv;
            InitializeComponent();
        }
        void warename_yTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.warename_yTextBox.Text.Trim().Length > 0)
            {
                
                pycode_yTextBox5.Text = PyWbCode.getPyCode(this.warename_yTextBox.Text).ToLower();
                wbcode_yTextBox5.Text = PyWbCode.getWbCode(this.warename_yTextBox.Text).ToLower();
            }
        }
        /*
        void selTextInpt1_TextChanged(object sender, EventArgs e)
        {
            if (this.selTextInpt1.Text.Trim().Length > 0)
            {
                memo_yTextBox.Text = this.selTextInpt1.Text;
            }
        }*/
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*if (this.warecode_yTextBox.Text.Trim().Length == 0)
            {
                WJs.alert("请输入库房编码！");
                warecode_yTextBox.Focus();
                return;
            }*/
            /*
            if (this.selTextInpt3.Text.Trim().Length == 0)
            {
                WJs.alert("请输入医疗机构编码！");
                selTextInpt3.Focus();
                return;
            }*/
            if (this.warename_yTextBox.Text.Trim().Length == 0)
            {
                WJs.alert("请输入库房名称！");
                warename_yTextBox.Focus();
                return;
            }
            if (this.warekind_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择库房类别！");
                warekind_ytComboBox.Focus();
                return;
            }
            if (this.selTextInpt1.Value==null)
            {
                WJs.alert("请输入科室！");
                selTextInpt1.Focus();
                return;
            }
            if (this.selTextInpt2.Value==null)
            {
                WJs.alert("请输入计价体系！");
                selTextInpt2.Focus();
                return;
            }
            if (this.outorder_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择出库顺序！");
                outorder_ytComboBox.Focus();
                return;
            }
            if (this.ifuse_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请设置是否使用！");
                ifuse_ytComboBox.Focus();
                return;
            }
            if (this.ifall_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择是否管理所有物资类别！");
                ifall_ytComboBox.Focus();
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
            ac.Action = "LKWZSVR.his.WZWareManag.WZWare";
            ac.Sql = "Save";
            ac.Add("choscode", this.choscode_yTextBox.Text);
            ac.Add("warename", this.warename_yTextBox.Text);

            if (this.warekind_ytComboBox.SelectedIndex > -1)
            {
                if (this.warekind_ytComboBox.SelectedItem.ToString() == "一级库房")
                {
                    ac.Add("ifstware", "1");
                    ac.Add("ifndware", "0");
                    ac.Add("ifrdware", "0");
                }
                if (this.warekind_ytComboBox.SelectedItem.ToString() == "二级库房")
                {
                    ac.Add("ifstware", "0");
                    ac.Add("ifndware", "1");
                    ac.Add("ifrdware", "0");
                }
                if (this.warekind_ytComboBox.SelectedItem.ToString() == "三级库房")
                {
                    ac.Add("ifstware", "0");
                    ac.Add("ifndware", "0");
                    ac.Add("ifrdware", "1");
                }
            }
            
            ////判断所输入的科室ID是否存在
            //string depid_exsit_s =  LData.Es("IsExsitDepId", null, new object[] { this.selTextInpt1.Text.Trim() });
            //if (depid_exsit_s == "")
            //{
            //    WJs.alert("请科室ID！");
            //    selTextInpt1.Focus();
            //    return;
            //}

            ac.Add("deptid",this.selTextInpt1.Value);
            ac.Add("priceid", this.selTextInpt2.Value);
            ac.Add("pycode",this.pycode_yTextBox5.Text);
            ac.Add("wbcode",this.wbcode_yTextBox5.Text);
            if(this.outorder_ytComboBox.SelectedIndex>-1)
            {
                if(this.outorder_ytComboBox.SelectedItem.ToString()=="先进先出")
                {
                    ac.Add("outorder","1");
                }
                if (this.outorder_ytComboBox.SelectedItem.ToString() == "先产先出")
                {
                    ac.Add("outorder", "2");
                }
                if (this.outorder_ytComboBox.SelectedItem.ToString() == "先变先出")
                {
                    ac.Add("outorder", "3");
                }
                if (this.outorder_ytComboBox.SelectedItem.ToString() == "近期先出")
                {
                    ac.Add("outorder", "4");
                }
            }

            if (this.ifuse_ytComboBox.SelectedIndex > -1)
            {
                if (this.ifuse_ytComboBox.SelectedItem.ToString() == "启用")
                    ac.Add("ifuse", "1");
                else
                    ac.Add("ifuse", "0");
                
                
            }
            if (this.ifall_ytComboBox.SelectedIndex > -1)
            {
                if (this.ifall_ytComboBox.SelectedItem.ToString() == "是")
                    ac.Add("ifall", "1");
                else
                {
                    ac.Add("ifall", "0");
                   
                   
                }
            }
            ac.Add("userid", this.userid_yTextBox.Text);
            ac.Add("username", this.username_yTextBox.Text);
            ac.Add("recdate", this.dateTimePicker1.Value);
          
            ac.Add("memo", this.memo_yTextBox.Text);
           
           
            if (!isAdd)
            {
                ac.Add("warecode", dr["库房编码"].ToString());
                //ac.Add("choscode", dr["医疗机构编码"].ToString());
                ac.SetKeyValue("choscode,warecode");
                /*
                if (this.ifall_ytComboBox.SelectedItem.ToString() == "否")
                {
                    SetWZDetail form = new SetWZDetail(null, dr);
                    form.Main = this.Main;
                    form.ShowDialog();
                }*/
            }
            
           // ac.SetKeyValue("choscode", "warecode");
            //bool t;

            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();

            
            if (isOk)
            {
                //保存
                if (!this.isAdd)
                {
                    wd = dr["库房编码"].ToString();
                }
                if (this.ifall_ytComboBox.SelectedItem.ToString() == "否")
                {
                    // WJs.alert("您需要设置管理物资类别细表");
                   // SetWZDetail form = new SetWZDetail(null, dr, wd);//这里wd为库房编码
                   // form.Main = this.Main;
                   // form.ShowDialog();
                    //设置并保存管理物资类别
                    string str = this.dataGView2.GetDataToXml();
                    if (str != null)
                    {
                        ActionLoad ac2 = ActionLoad.Conn();
                        ac2.Action = "LKWZSVR.his.WZWareManag.SetWZManagKind";
                        ac2.Sql = "AllSave";
                        ac2.Add("WARECODE", wd);
                        ac2.Add("CHOSCODE", this.choscode_yTextBox.Text);
                        ac2.Add("DanJuMx", str);
                        ac2.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac2_ServiceLoad);
                        ac2.Post();
                    }


                }
                if (isAdd)
                {
                    if (!WJs.confirm("保存成功，是否继续添加？"))
                    {

                        this.Close();
                        // return true;
                    }
                    else
                    {
                        InitForm();
                        // return false;
                    }
                }
                else
                {
                    this.Close();
                }
                
                
               
            }
            else
            {
               // WJs.alert("保存物资库房信息失败！！！");
                
            }

           
            
        }
        
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {

            if (e.Msg.Msg.Equals("已经存在该库房信息,不能修改成该名称！") || e.Msg.Msg.Equals("已经存在该库房信息！"))
            {
                isOk = false;
                WJs.alert(e.Msg.Msg);
            }
            else
            {
                isOk = true;
                wd = e.Msg.Msg.ToString().Split(',')[0];
                ms = e.Msg.Msg.ToString().Split(',')[1];
            }
            
        }
        void ac2_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        { 
        }
        private void InitForm()
        {
            this.outorder_ytComboBox.SelectedItem = "";
            this.ifall_ytComboBox.SelectedItem = "";
            this.ifuse_ytComboBox.SelectedItem = "";
            this.warekind_ytComboBox.SelectedItem = "";
            

            
            //this.outorder_ytComboBox.SelectedIndex = -1;
            //this.ifall_ytComboBox.SelectedIndex = -1;
            //this.ifuse_ytComboBox.SelectedIndex = -1;
            this.warekind_ytComboBox.SelectedIndex = -1;

            this.warecode_yTextBox.Text = "";
            this.warename_yTextBox.Text = "";
            this.pycode_yTextBox5.Text = "";
            this.wbcode_yTextBox5.Text = "";
            this.memo_yTextBox.Text = "";
            
            this.selTextInpt1.Text = "";
            this.selTextInpt2.Text = "";
           // this.choscode_yTextBox.Text = "";


            this.selTextInpt1.Value = "";
            this.selTextInpt2.Value = "";
            //this.selTextInpt3.Value = "";

            this.outorder_ytComboBox.SelectedIndex = 0;
            this.ifuse_ytComboBox.SelectedIndex = 0;
            this.ifall_ytComboBox.SelectedIndex = 0;
            this.warename_yTextBox.Focus();
          
        }
        private void AddWZ_Load(object sender, EventArgs e)
        {
            ControlUtil.RegKeyEnter(this);
            this.selTextInpt1.Sql = "FindKSID";
           // this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt1.SelParam = His.his.Choscode + "|{key}|{key}|{key}";
            this.selTextInpt2.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
           // this.selTextInpt2.textBox1.ReadOnly = true;
            this.selTextInpt2.Sql = "Getpriceid";
            this.dataGView1.Url = "Getkindcode2";

            //this.selTextInpt3.Sql = "Getchoscode";

            //object[] param = new object[] { dr["库房编码"].ToString(), His.his.Choscode };

            this.choscode_yTextBox.Text = His.his.Choscode.ToString();
            this.userid_yTextBox.Text = His.his.UserId.ToString();
            this.username_yTextBox.Text = His.his.UserName;

            TvList.newBind().add("先进先出", "1").add("先产先出", "2").add("先变先出", "3").
                add("近期先出","4").Bind(this.outorder_ytComboBox);
           
          
            TvList.newBind().add("启用", "1").add("停用", "0").Bind(this.ifuse_ytComboBox);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ifall_ytComboBox);
            this.ifall_ytComboBox.SelectedIndexChanged += new EventHandler(ifall_ytComboBox_SelectedIndexChanged);
           // this.ifuse_ytComboBox.SelectedIndex = 1;
           // this.ifall_ytComboBox.SelectedIndex = 0;
            warekind_ytComboBox.Items.Add("一级库房");
            warekind_ytComboBox.Items.Add("二级库房");
            warekind_ytComboBox.Items.Add("三级库房");
            this.ifuse_ytComboBox.SelectedIndex = 0;
           
            warename_yTextBox.TextChanged += new EventHandler(warename_yTextBox_TextChanged);

            if (isAdd)
            {
                this.outorder_ytComboBox.SelectedIndex = 0;
                this.ifuse_ytComboBox.SelectedIndex = 0;
                this.ifall_ytComboBox.SelectedIndex = 0;
            }
            this.dataGView1.reLoad(new object[] { His.his.Choscode });
            if (!isAdd)
            {
                this.warecode_yTextBox.Text = dr["库房编码"].ToString();
              
                this.warename_yTextBox.Text = dr["库房名称"].ToString();            
                if (dr["是否一级库房"].ToString() == "1")
                {
                    this.warekind_ytComboBox.SelectedIndex = 0;
                 
 
                }
                if (dr["是否二级库房"].ToString() == "1")
                {
                    this.warekind_ytComboBox.SelectedIndex = 1;
                    

                }
                if (dr["是否三级库房"].ToString() == "1")
                {
                    this.warekind_ytComboBox.SelectedIndex = 2;
                  

                }
                //string ksname = LData.Es("GetKSName", null, new object[] { dr["科室ID"].ToString() });
               // this.selTextInpt1.Text = dr["科室ID"].ToString();
                //this.selTextInpt1.Text = ksname;
                //this.selTextInpt1.Value = dr["科室ID"].ToString();
                string depid = LData.Es("Ware_GetDepID", "LKWZ", new object[] { dr["医疗机构编码"].ToString(), dr["库房编码"].ToString() });
                this.selTextInpt1.Text = dr["科室"].ToString();
                this.selTextInpt1.Value = depid;
               // string pricename = LData.Es("GetPriceName", null, new object[] { dr["计价体系"].ToString() });
                //this.selTextInpt2.Text = dr["计价体系ID"].ToString();
               // this.selTextInpt2.Text = pricename;
                this.selTextInpt2.Text = dr["计价体系"].ToString();
                string priceid = LData.Es("Ware_GetPriceID", "LKWZ", new object[] { dr["医疗机构编码"].ToString(), dr["库房编码"].ToString() });
                this.selTextInpt2.Value = priceid;
               
                
                if (dr["出库顺序"].ToString() == "1")
                {
                    this.outorder_ytComboBox.SelectedIndex = 0;
                }
                if (dr["出库顺序"].ToString() == "2")
                {
                    this.outorder_ytComboBox.SelectedIndex = 1;
                }
                if (dr["出库顺序"].ToString() == "3")
                {
                    this.outorder_ytComboBox.SelectedIndex = 2;
                }
                if (dr["出库顺序"].ToString() == "4")
                {
                    this.outorder_ytComboBox.SelectedIndex = 3;
                }
                if (dr["是否使用"].ToString() == "1")
                {
                    this.ifuse_ytComboBox.SelectedIndex = 0;
                }
                else
                {
                    this.ifuse_ytComboBox.SelectedIndex = 1;
                }
                if (dr["IFALL"].ToString() == "1")
                {
                    this.ifall_ytComboBox.SelectedIndex = 0;

                }
                else
                {
                    this.ifall_ytComboBox.SelectedIndex = 1;
                    //修改时，加载管理的物资类别
                    this.dataGView2.Url = "GetWZKind";
                    this.dataGView2.reLoad(new object[] {this.choscode_yTextBox.Text, this.warecode_yTextBox.Text });

                    this.dataGView1.Url = "GetWZKind0";
                    this.dataGView1.reLoad(new object[] { this.choscode_yTextBox.Text, this.choscode_yTextBox.Text, this.warecode_yTextBox.Text });

                }

               
                this.memo_yTextBox.Text = dr["备注"].ToString();
                this.pycode_yTextBox5.Text = dr["拼音码"].ToString();
                this.wbcode_yTextBox5.Text = dr["五笔码"].ToString();
            }
           
            
        }

        void ifall_ytComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (this.ifall_ytComboBox.SelectedIndex == 0)
            {
                //管理所有物资类别
                //this.groupBox2.Enabled = false;
                this.dataGView1.Enabled = false;
                this.dataGView2.Enabled = false;
                this.AllSeclbutton.Enabled = false;
                this.AllClearbutton.Enabled = false;
                this.Selectbutton.Enabled = false;
                this.Cancelbutton.Enabled = false;
                this.dataGView2.Url = "Getkindcode2";
                this.dataGView2.reLoad(new object[] { null });
            }
            else
            {
                this.dataGView1.Enabled = true;
                this.dataGView2.Enabled = true;
                this.AllSeclbutton.Enabled = true;
                this.AllClearbutton.Enabled = true;
                this.Selectbutton.Enabled = true;
                this.Cancelbutton.Enabled = true;
            }
        }

        private void AllSeclbutton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                this.dataGView2.Url = "Getkindcode2";
                this.dataGView2.reLoad(new object[] { His.his.Choscode });
                this.dataGView1.Url = "Getkindcode2";
                this.dataGView1.reLoad(new object[] { null });
                this.AllSeclbutton.Enabled = false;
                this.AllClearbutton.Enabled = true;
                //string str = this.dataGView2.GetDataToXml();
                //if (str != null)
                //{
                //    ActionLoad ac = ActionLoad.Conn();
                //    ac.Action = "LKWZSVR.his.WZWareManag.SetWZManagKind";
                //    ac.Sql = "Save";
                //    ac.Add("WARECODE", this.choscode_yTextBox.Text);
                //    ac.Add("CHOSCODE", this.choscode_yTextBox.Text);
                //    ac.Add("DanJuMx", str);
                //    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac2_ServiceLoad);
                //    ac.Post();
                //}
            }
        }

        private void AllClearbutton_Click(object sender, EventArgs e)
        {
            this.dataGView2.Url = "Getkindcode2";
            this.dataGView2.reLoad(new object[] {null});
            this.dataGView1.Url = "Getkindcode2";
            this.dataGView1.reLoad(new object[] { His.his.Choscode });
            this.AllSeclbutton.Enabled = true;
            this.AllClearbutton.Enabled = false;
            //this.dataGView2.Rows.
               
        }

        private void Selectbutton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                Dictionary<string, object> de = new Dictionary<string, object>();

                de["类别名称"] = dr["类别名称"];
                de["类别编码"] = dr["类别编码"];
                //de["上级编码"] = dr["上级编码"];
                //de["备注"] = dr["备注"];
                //de["医疗机构编码"] = dr["医疗机构编码"];
                this.dataGView2.AddRow(de, 0);
                this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                //ActionLoad ac = ActionLoad.Conn();
                //ac.Action = "LKWZSVR.his.WZWareManag.SetWZManagKind";
                //ac.Sql = "SelectSave";
                //ac.Add("WARECODE", this.warecode_yTextBox.Text);
                //ac.Add("CHOSCODE", this.choscode_yTextBox.Text);
                //ac.Add("KINDCODE", this.kindcode_selTextInpt.Value);
                //// ac.SetKeyValue("WARECODE,CHOSCODE,KINDCODE");

                //ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                //ac.Post();
               // this.dataGView2.reLoad(new object[] { His.his.Choscode });
            }
            else
            {
                WJs.alert("请选择要添加的物资类别！");
            }

        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
             Dictionary<string, ObjItem> dr = this.dataGView2.getRowData();
             if (dr != null)
             {
                 Dictionary<string, object> de = new Dictionary<string, object>();
                 de["类别名称"] = dr["类别名称"];
                 de["类别编码"] = dr["类别编码"];
                 //de["上级编码"] = dr["上级编码"];
                 //de["备注"] = dr["备注"];
                 //de["医疗机构编码"] = dr["医疗机构编码"];
                 this.dataGView1.AddRow(de, 0);
                 this.dataGView2.Rows.Remove(this.dataGView2.CurrentRow);
                 
             }
        }

   
    }
}
