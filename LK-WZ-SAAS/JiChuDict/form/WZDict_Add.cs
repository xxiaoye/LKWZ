using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtWinContrl.com.datagrid;
using YtUtil.tool;
using ChSys;
using YtWinContrl.com;
using YtClient;

namespace JiChuDict.form
{
    public partial class WZDict_Add : Form
    {
        public WZDict_Add()
        {
            InitializeComponent();
        }
        DataRow r;
        private bool isAdd;
        private bool ifuseupdata=false;
        //DataTable dt;
        //string t;

        public WZDict_Add(DataRow r, bool _isAdd)
        {
            isAdd = _isAdd;
            this.r = r;
            InitializeComponent();
        }
        public WZDict_Add(DataRow r, bool _isAdd, bool _ifuseupdata)
        {
            ifuseupdata = _ifuseupdata;
            isAdd = _isAdd;
            this.r = r;
            InitializeComponent();
        }
        public bool isSc = false;
        public WZDictManag WZDM;
        void yTextBox_Name_TextChanged(object sender, EventArgs e)
        {
            string n = this.yTextBox_Name.Text.Trim();
            if (n.Length > 0)
            {
                this.yTextBox_PY.Text = PyWbCode.getPyCode(n).ToLower();
                this.yTextBox_WB.Text = PyWbCode.getWbCode(n).ToLower();
            }
        }
        void yTextBox_JC_TextChanged(object sender, EventArgs e)
        {
            string n = this.yTextBox_JC.Text.Trim();
            if (n.Length > 0)
            {
                this.yTextBox_JM.Text = PyWbCode.getPyCode(n);
              
            }
        }
        void yTextBox_BM_TextChanged(object sender, EventArgs e)
        {
            string n = this.yTextBox_BM.Text.Trim();
            if (n.Length > 0)
            {
                this.yTextBox_BMJM.Text = PyWbCode.getPyCode(n);

            }
        }

        void ytComboBox_SingerCodeorSmallestCode_TextChanged(object sender, EventArgs e)
        {
            if (this.selTextInpt_Unicode.Value != null && this.selTextInpt_Unicode.Value != null)
            {
                if (this.selTextInpt_Unicode.Value == this.selTextInpt_SmallestCode.Value)
                {
                    this.yTextBox_HSXS.Text = "1";
                    //this.yTextBox_HSXS.Text = "1" + selTextInpt_Unicode.Text + "=" + "1" + this.selTextInpt_SmallestCode.Text;
                    this.yTextBox_HSXS.ReadOnly = true;
                    //this.label_HSXS.Text = "";
                }
                else
                {
                    this.yTextBox_HSXS.ReadOnly = false;
                }

                this.label_HSXS.Text = this.selTextInpt_SmallestCode.Text + "/" + selTextInpt_Unicode.Text;
               
            
            }
              
        
        }
        private void WZDict_Add_Load(object sender, EventArgs e)
        {
         

            this.selTextInpt_Unicode.TextChanged += new EventHandler(ytComboBox_SingerCodeorSmallestCode_TextChanged);
            this.selTextInpt_SmallestCode.TextChanged += new EventHandler(ytComboBox_SingerCodeorSmallestCode_TextChanged);
           
            
            //this.ytComboBox_SingerCode.SelectedIndexChanged += new EventHandler(ytComboBox_SingerCodeorSmallestCode_TextChanged);
            //this.ytComboBox_SmallestCode.SelectedIndexChanged += new EventHandler(ytComboBox_SingerCodeorSmallestCode_TextChanged);
            this.yTextBox_Name.TextChanged += new EventHandler(yTextBox_Name_TextChanged);
            this.yTextBox_JC.TextChanged += new EventHandler(yTextBox_JC_TextChanged);
            this.yTextBox_BM.TextChanged += new EventHandler(yTextBox_BM_TextChanged);

            TvList.newBind().add("启用", "1").add("停用", "0").Bind(this.ytComboBox_ifUse);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ytComboBox_IfUseMore);

            //TvList.newBind().Load("WZDict_KindCode", new object[] { His.his.Choscode }).Bind(this.ytComboBox_KindCode);
            TvList.newBind().Load("WZDict_CountCode", new object[] { His.his.Choscode }).Bind(this.ytComboBox_CountCode); //这里修改了，查询所有
            //TvList.newBind().Load("WZDict_SingerCode", null).Bind(this.ytComboBox_SingerCode);
            //TvList.newBind().Load("WZDict_SmallestCode", null).Bind(this.ytComboBox_SmallestCode);
          
            
            //这里更改了单位编码
            //TvList.newBind().SetCacheKey("sfxmbm").Load("WZDict_SFXMBM",new object[]{ "010705"}).Bind(this.ytComboBox_SFXMBM);
            this.selTextInpt1.SelParam = "{key}|{key}|{key}|" + His.his.Choscode;//为什么要这样写？
            selTextInpt1.BxSr = false;//必须输入查询关键字
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt1.Sql = "WZDict_SFXMBM";


            this.selTextInpt_Unicode.SelParam = "{key}|{key}|{key}|";//为什么要这样写？
            selTextInpt1.BxSr = false;//必须输入查询关键字
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt_Unicode.Sql = "WZDict_SingerCode";


            this.selTextInpt_KindCode.SelParam = His.his.Choscode+"|{key}|{key}|{key}|{key}|";//为什么要这样写？
            selTextInpt_KindCode.BxSr = false;//必须输入查询关键字
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt_KindCode.Sql = "WZDict_KindCode1";


            this.selTextInpt_SmallestCode.SelParam = "{key}|{key}|{key}|";//为什么要这样写？
            selTextInpt1.BxSr = false;//必须输入查询关键字
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt_SmallestCode.Sql = "WZDict_SmallestCode";
          


          
            this.yTextBox_UserID.ReadOnly = true;
            this.yTextBox_User.ReadOnly = true;
            this.yTextBox_WZID.ReadOnly = true;
            this.yTextBox_HSXS.ReadOnly = true;
            this.yTextBox_Choscode.ReadOnly = true;
            this.ytComboBox_ifUse.SelectedIndex = 0;
            if (!this.isAdd)
            {
                //this.label_HSXS.Visible = false;

                if (ifuseupdata)
                {
                    this.selTextInpt_SmallestCode.Enabled = false;
                    this.selTextInpt_Unicode.Enabled = false;
                    this.yTextBox_HSXS.ReadOnly = true;
                }
               
                this.yTextBox_WZID.Text = r["WZID"].ToString();
                this.yTextBox_Name.Text = r["WZNAME"].ToString();
                this.selTextInpt_KindCode.Value = r["KINDCODE"].ToString();
                this.selTextInpt_KindCode.Text = r["KINDNAME"].ToString();
                //LData.Es("WZDict_KindCode2", null, new object[] { r["KINDCODE"].ToString(), });


                this.ytComboBox_CountCode.Value = r["COUNTCODE"].ToString();

                this.selTextInpt_Unicode.Value = r["UNITCODE"].ToString();
                this.selTextInpt_Unicode.Text = LData.Exe("WZDict_SingerCode1", "LKWZ", new object[] { r["UNITCODE"].ToString() });

                this.selTextInpt_SmallestCode.Value = r["LSUNITCODE"].ToString();

                this.selTextInpt_SmallestCode.Text = LData.Exe("WZDict_SmallestCode1", "LKWZ", new object[] { r["LSUNITCODE"].ToString() });
                this.yTextBox_SingerPrice.Text = r["PRICE"].ToString();
                this.yTextBox_HSXS.Text = r["CHANGERATE"].ToString();
                this.yTextBox_LowestCount.Text = r["LOWNUM"].ToString();
                this.yTextBox_HighestCount.Text = r["HIGHNUM"].ToString();
                this.yTextBox_JiaJL.Text = r["RATE"].ToString();
                this.ytComboBox_IfUseMore.Value = r["IFNY"].ToString();
                this.ytComboBox_ifUse.Value = r["IFUSE"].ToString();
                this.yTextBox_UserID.Text = r["USERID"].ToString();
                this.yTextBox_User.Text = r["USERNAME"].ToString();
                this.selTextInpt1.Text = r["FARECODE"].ToString();
                this.yTextBox_Choscode.Text = r["CHOSCODE"].ToString();

                if (r["PYCODE"] != null)
                {
                    this.yTextBox_PY.Text = r["PYCODE"].ToString();
                }
                if (r["WBCODE"] != null)
                {
                    this.yTextBox_WB.Text = r["WBCODE"].ToString();
                } if (r["SHORTNAME"] != null)
                {
                    this.yTextBox_JC.Text = r["SHORTNAME"].ToString();
                }
                if (r["SHORTCODE"] != null)
                {
                    this.yTextBox_JM.Text = r["SHORTCODE"].ToString();
                } if (r["ALIASNAME"] != null)
                {
                    this.yTextBox_BM.Text = r["ALIASNAME"].ToString();
                }
                if (r["ALIASCODE"] != null)
                {
                    this.yTextBox_BMJM.Text = r["ALIASCODE"].ToString();
                }
                if (r["GG"] != null)
                {
                    this.yTextBox_GuiG.Text = r["GG"].ToString();
                } if (r["VALIDE"] != null)
                {
                    this.yTextBox_YXQ.Text = r["VALIDE"].ToString();
                }
                if (r["TXM"] != null)
                {
                    this.yTextBox_TiaoXM.Text = r["TXM"].ToString();
                } if (r["MEMO"] != null)
                {
                    this.yTextBox_Rec.Text = r["MEMO"].ToString();
                }
                
                //ControlUtil.SetSelectRadioValue(this.groupBox2, r["WZID"]);

            }
            else
            {
                //this.ytComboBox_KindCode.Value = r["KINDCODE"].ToString();
                if (r != null)
                {
                    this.selTextInpt_KindCode.Value = r["KINDCODE"].ToString();
                    this.selTextInpt_KindCode.Text = LData.Es("WZDictFindKindName", "LKWZ", new object[] { r["KINDCODE"].ToString(), His.his.Choscode });
                }
                this.yTextBox_User.Text = His.his.UserName;
               
                this.yTextBox_UserID.Text = His.his.UserId.ToString();
              
                this.yTextBox_Choscode.Text = His.his.Choscode;
        
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (this.yTextBox_Name.Text.Trim().Length == 0)
            {
                WJs.alert("请输入物资名称！");
                this.yTextBox_Name.Focus();
                return;
            }
            if (this.selTextInpt_KindCode.Value ==null)
            {
                WJs.alert("请选择所属类别！");
                selTextInpt_KindCode.Focus();
                return;
            }
            if (this.ytComboBox_CountCode.SelectedIndex == -1)
            {
                WJs.alert("请选择统计编码！");
                ytComboBox_CountCode.Focus();
                return;
            }
            if (this.selTextInpt_Unicode.Value == null)
            {
                WJs.alert("请选择包装单位！");
                selTextInpt_Unicode.Focus();
                return;
            }


            if (this.yTextBox_SingerPrice.Text.Trim().Length == 0)
            {
                WJs.alert("请输入单价！");
                this.yTextBox_SingerPrice.Focus();
                return;
            }
            decimal price;
            if (!decimal.TryParse(this.yTextBox_SingerPrice.Text.ToString(), out  price))
            {
                WJs.alert("单价只能为大于0的数！");
                this.yTextBox_SingerPrice.Focus();
                return;
            }
    
            if (this.selTextInpt_SmallestCode.Value == null)
            {
                WJs.alert("请选择最小计量单位！");
                selTextInpt_SmallestCode.Focus();
                return;
            }
            if (this.yTextBox_HSXS.Text.Trim().Length == 0)
            {
                WJs.alert("请输入包装系数！");
                this.yTextBox_HSXS.Focus();
                return;
            }

            if (!WJs.IsZs(this.yTextBox_HSXS.Text.ToString()))
            {
               
                WJs.alert("包装系数只能为大于0的整数！");
                this.yTextBox_HSXS.Focus();
                return;
            }

            if (this.yTextBox_LowestCount.Text.Trim().Length == 0)
            {
                WJs.alert("请输入最低库存量！");
                this.yTextBox_LowestCount.Focus();
                return;
            }
            if (!WJs.IsZs(this.yTextBox_LowestCount.Text.ToString()))
            {
                WJs.alert("最低库存量只能为大于0的整数！");
                this.yTextBox_LowestCount.Focus();
                return;
            }
            if (this.yTextBox_HighestCount.Text.Trim().Length == 0)
            {
                WJs.alert("请输入最高库存量！");
                this.yTextBox_HighestCount.Focus();
                return;
            }
            decimal l = 0;
            decimal h = 0;
            if (this.yTextBox_LowestCount.Text.Trim().Length != 0)
            {
                l = Convert.ToDecimal(this.yTextBox_LowestCount.Text.Trim());

            }
            if (this.yTextBox_HighestCount.Text.Trim().Length != 0)
            {
                h = Convert.ToDecimal(this.yTextBox_HighestCount.Text.Trim());
            }
            if (l != 0&&h!=0)
            {
                if (l > h)
                {
                    WJs.alert("输入最高库存量不能小于最低库存量！");
                    this.yTextBox_HighestCount.Focus();
                    return;
                }
            }
            if (!WJs.IsZs(this.yTextBox_HighestCount.Text.ToString()))
            {
                WJs.alert("最高库存量只能为大于0的整数！");
                this.yTextBox_HighestCount.Focus();
                return;
            }
            if (this.yTextBox_JiaJL.Text.Trim().Length == 0)
            {
                WJs.alert("请输入加价率！");
                this.yTextBox_JiaJL.Focus();
                return;
            }
            if (!decimal.TryParse(this.yTextBox_JiaJL.Text.ToString(), out  price))
            {
                WJs.alert("加价率必须是数字！");
                this.yTextBox_JiaJL.Focus();
                return;
            }
            if (this.ytComboBox_IfUseMore.SelectedIndex == -1)
            {
                WJs.alert("请选择是否耐用！");
                ytComboBox_IfUseMore.Focus();
                return;
            }
         
            if (this.ytComboBox_ifUse.SelectedIndex == -1)
            {
                WJs.alert("请选择是否使用！");
                ytComboBox_ifUse.Focus();
                return;
            }
            if (this.selTextInpt1.Text.Trim().Length > 0)
            {
                string valid = LData.Es("WZDict_SFXMBM_Valid", "LKWZ", new object[] { this.selTextInpt1.Text.Trim().ToString(), His.his.Choscode });
               if (valid == null)
               {
                WJs.alert("收费项目编码输入错误！");
                selTextInpt1.Focus();
                return;
               }
            }
           int yxq = 0;
           if (this.yTextBox_YXQ.Text.ToString().Trim().Length > 0)
           {
               bool flag = int.TryParse(this.yTextBox_YXQ.Text.ToString(), out yxq);
               if (!flag)
               {

                   WJs.alert("输入有效期必须是大于0的整数！");
                   this.yTextBox_YXQ.Focus();
                   return;

               }
               if (yxq <= 0)
               {
                   WJs.alert("输入有效期必须是大于0的整数！");
                   this.yTextBox_YXQ.Focus();
                   return;
               }
           }
            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkwz.JiChuDict.WZDictSvr";
            ac.Sql = "SaveDictWZInfo";
            ac.Add("WZNAME", this.yTextBox_Name.Text);
            ac.Add("KINDCODE",this.selTextInpt_KindCode.Value);
            ac.Add("COUNTCODE", TvList.getValue(this.ytComboBox_CountCode).ToString());
            ac.Add("UNITCODE", this.selTextInpt_Unicode.Value);
            ac.Add("LSUNITCODE",this.selTextInpt_SmallestCode.Value);

            ac.Add("PRICE",this.yTextBox_SingerPrice.Text);
            //decimal dec;
            //if (decimal.TryParse(this.yTextBox_HSXS.Text, out dec))
            //{
            //    this.yTextBox_HSXS.Text = "1" + selTextInpt_Unicode.Text + "=" + "1" + this.selTextInpt_SmallestCode.Text;

            //}
            ac.Add("CHANGERATE", this.yTextBox_HSXS.Text);
            ac.Add("LOWNUM", this.yTextBox_LowestCount.Text);
            ac.Add("HIGHNUM", this.yTextBox_HighestCount.Text);
            ac.Add("RATE", this.yTextBox_JiaJL.Text);
            ac.Add("IFNY", TvList.getValue(this.ytComboBox_IfUseMore).ToInt());
            ac.Add("IFUSE", TvList.getValue(this.ytComboBox_ifUse).ToInt());
            ac.Add("USERID",  His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);

            ac.Add("FARECODE", this.selTextInpt1.Text);
            ac.Add("CHOSCODE",His.his.Choscode);

            ac.Add("PYCODE", this.yTextBox_PY.Text);
            ac.Add("WBCODE", this.yTextBox_WB.Text);
            ac.Add("SHORTNAME", this.yTextBox_JC.Text);
            ac.Add("SHORTCODE", this.yTextBox_JM.Text);

            ac.Add("ALIASNAME", this.yTextBox_BM.Text);
            ac.Add("ALIASCODE", this.yTextBox_BMJM.Text);
            ac.Add("GG", this.yTextBox_GuiG.Text);
            ac.Add("VALIDE", this.yTextBox_YXQ.Text);
            ac.Add("TXM", this.yTextBox_TiaoXM.Text);
            ac.Add("MEMO", this.yTextBox_Rec.Text);

            if (!isAdd)
            {
                ac.Add("WZID", r["WZID"]);

            }
            else 
            {
                ac.Add("WZID",null);
            }
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
           
            WZDM.ReLoadData(this.selTextInpt_KindCode.Value);

            if (!isAdd || !WJs.confirm("是否继续添加物资信息？"))
            {
                this.Close();
            }
            else
            {
                this.ytComboBox_CountCode.SelectedIndex = -1;
                this.ytComboBox_ifUse.SelectedIndex = 0;
                this.yTextBox_Name.Focus();
                this.ytComboBox_IfUseMore.SelectedIndex = -1;
                this.selTextInpt1.Value = null;
                this.selTextInpt1.Text = null;

                this.selTextInpt_SmallestCode.Value = null;
                this.selTextInpt_SmallestCode.Text = null;
                this.selTextInpt_Unicode.Value = null;
                this.selTextInpt_Unicode.Text = null;
            
                this.yTextBox_BM.Clear();
                this.yTextBox_BMJM.Clear();
                this.yTextBox_GuiG.Clear();
                this.yTextBox_HighestCount.Clear();
                this.yTextBox_HSXS.Clear();
                this.yTextBox_JC.Clear();
                this.yTextBox_JiaJL.Clear();
                this.yTextBox_JM.Clear();
                this.yTextBox_LowestCount.Clear();
                this.yTextBox_Name.Clear();
                this.yTextBox_PY.Clear();
                this.yTextBox_Rec.Clear();
                this.yTextBox_SingerPrice.Clear();
                this.yTextBox_TiaoXM.Clear();
                this.yTextBox_WB.Clear();
                this.yTextBox_WZID.Clear();
                this.yTextBox_YXQ.Clear();
                this.label_HSXS.Text = "";
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            isSc = false;//什么标志位
            this.Close();
        }

        //private void yTxtBox_HSXS_MouseClick(object sender, MouseEventArgs e)
        //{
        //    this.yTextBox_HSXS.Text = "";
        //    this.label_HSXS.Visible = true;
        //}

    }
}
