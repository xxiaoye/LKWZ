using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtClient;
using YtUtil.tool;
using ChSys;
using YtWinContrl.com.datagrid;
using YtPlugin;
using YiTian.db;
using YtWinContrl.com;  

namespace JiChuDict.form
{
    public partial class WZKind_Add : Form
    {


        
        DataRow r;
        private bool isAdd;
        //DataTable dt;
        //string t;
        public WZKind_Add(DataRow r, bool _isAdd)
        {
            isAdd = _isAdd;
            this.r = r;
            InitializeComponent();
        }
        public bool isSc = false;
        public WZKindManag pr;
        private void button1_Click(object sender, EventArgs e)//新增&修改 提出问题
        {
            ActionLoad bc = ActionLoad.Conn();
           
            bc.Action = "LKWZSVR.lkwz.JiChuDict.WZKind";
           
            if (this.isAdd)
            {
                //保存
                bc.Sql = "SaveWZKindInfo";
                
               
            }
            else
            {
                //修改
                bc.Sql = "UpdataWZKindInfo";
                bc.Add("SELECTCODE", r["KINDCODE"].ToString());

            }
            if (this.yTextBox_Name.Text.Trim().Length <= 0)
            {
                WJs.alert("请填写类别名称");
                return;
            }
            else
            {
                bc.Add("KINDNAME", this.yTextBox_Name.Text);
            }
        
                bc.Add("PYCODE", this.yTextBox_PY.Text);
           
   
                bc.Add("WBCODE", this.yTextBox_WB.Text);
          
            if (this.ytComboBox_ifUse.Value==null)
            {
                WJs.alert("请选择是否使用");
                return;
            }
            else
            {
                bc.Add("IFUSE", TvList.getValue(this.ytComboBox_ifUse).ToInt());
            }
            if (this.ytComboBox_IfEnd.Value == null)
            {
                WJs.alert("请选择是否末节点");
                return;
            }
            else
            {
                bc.Add("IFEND", TvList.getValue(this.ytComboBox_IfEnd).ToInt());
            }
     
            bc.Add("USERID", His.his.UserId);
            bc.Add("USERNAME", His.his.UserName);
            bc.Add("CHOSCODE", His.his.Choscode);
            if (yTextBox_Rec != null)
            {
                bc.Add("MEMO", this.yTextBox_Rec.Text);
            }
            else
            {
                bc.Add("MEMO", null);
            }

            bc.Add("KINDCODE", this.yTextBox_LeiCode.Text);
            bc.Add("SUPERCODE", this.yTextBox_UpCode.Text);
       
          
            bc.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            bc.Post();
           
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
              pr.ReLoadData(this.yTextBox_Name.Text);
                isSc = true;
                this.Close();
           
        


        }
        void yTextBox_Name_TextChanged(object sender, EventArgs e)//加载拼音码和五笔码 
        {
            string n = this.yTextBox_Name.Text.Trim();
            if (n.Length > 0)
            {
                this.yTextBox_PY.Text = PyWbCode.getPyCode(n).ToLower();
                this.yTextBox_WB.Text = PyWbCode.getWbCode(n).ToLower();
            }
        }

        private void WZKind_Add_Load(object sender, EventArgs e)
        {
            this.yTextBox_Name.TextChanged += new EventHandler(yTextBox_Name_TextChanged);
            TvList.newBind().add("启用", "1").add("停用", "0").Bind(this.ytComboBox_ifUse);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ytComboBox_IfEnd);
            this.yTextBox_User.Text = His.his.UserName.ToString();
            this.yTextBox_UserID.Text = His.his.UserId.ToString();
            this.ytComboBox_IfEnd.Enabled = false;
            this.yTextBox_UpCode.ReadOnly = true;
            this.yTextBox_LeiCode.ReadOnly = true;
            this.yTextBox_User.ReadOnly = true;
            this.yTextBox_ChCode.ReadOnly = true;
            this.yTextBox_UserID.ReadOnly = true;
            this.ytComboBox_ifUse.SelectedIndex = 0;
            if (!this.isAdd)
            {
                this.yTextBox_Name.Text = r["KINDNAME"].ToString();
                this.yTextBox_PY.Text = r["PYCODE"].ToString();
                this.yTextBox_WB.Text = r["WBCODE"].ToString();
               
                this.yTextBox_User.Text = r["USERNAME"].ToString();
                this.yTextBox_Rec.Text = r["MEMO"].ToString();
                this.ytComboBox_ifUse.Value = r["IFUSE"].ToString();
               
                this.yTextBox_ChCode.Text = r["CHOSCODE"].ToString();
                this.ytComboBox_IfEnd.Value = r["IFEND"].ToString();
                this.yTextBox_LeiCode.Text = r["KINDCODE"].ToString();
                this.yTextBox_UpCode.Text = r["SUPERCODE"].ToString();
              
                this.yTextBox_UserID.Text = r["USERID"].ToString();
                
            }
            else 
            {
               
                this.yTextBox_User.Text = His.his.UserName;             
                this.yTextBox_UserID.Text =His.his.UserId.ToString();
                this.yTextBox_ChCode.Text = His.his.Choscode;
                this.ytComboBox_IfEnd.Value = "1";
               
                //设置上级编码和类别编码
                if (r == null)
                {
                    this.yTextBox_UpCode.Text = "0";
                    //bc.Add("SUPERCODE", 0);
                   
                    //dt = LData.LoadDataTable("FindWZKind_Code", new object[] { int.Parse("0".ToString()) });


                    string t = LData.Exe("FindWZKind_Code", "LKWZ", new object[] { "0" ,His.his.Choscode});


                    if (Convert.ToInt32(t).ToString().Length <= 1 && (Convert.ToInt32(t) + 1).ToString().Length<=1)//判断是否前面有个0（0801）
                    {
                       t = "0" + (Convert.ToInt32(t) + 1).ToString();
                    }
                    else
                    {
                        t =  (Convert.ToInt32(t) + 1).ToString();

                    }
                    this.yTextBox_LeiCode.Text = t;

                }
                else
                {
                    this.yTextBox_UpCode.Text =  r["KINDCODE"].ToString();
                    //dt = LData.LoadDataTable("FindWZKind_Code", new object[] { r["KINDCODE"].ToString() });
                    string t= LData.Exe("FindWZKind_Code","LKWZ" ,new object[] { r["KINDCODE"].ToString(),His.his.Choscode});
                    if (t == null)
                    {
                        t = r["KINDCODE"].ToString() + "01";
                    }
                    else if (t.Trim().Length > Convert.ToInt32(t).ToString().Length)
                    {
                        t = "0" + (Convert.ToInt32(t) + 1).ToString();

                    }
                    else
                    {
                        t = (Convert.ToInt32(t) + 1).ToString();
                    }
                    this.yTextBox_LeiCode.Text = t;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isSc = false;
            this.Close();
        }




    }
}