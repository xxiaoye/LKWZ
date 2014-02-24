using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YiTian.db;
using YtWinContrl.com;
using YtUtil.tool;
using YtClient;
using ChSys;

namespace JiChuDict.form
{
    public partial class SetWZDetail : Form
    {
        public WZWareManag Main;
        private YtWinContrl.com.datagrid.DataGView dataGViewPL;
        Dictionary<string, ObjItem> dr;
       // bool flag;
        string wd;
        public SetWZDetail()
        {
            InitializeComponent();
        }
        //public SetWZDetail(YtWinContrl.com.datagrid.DataGView gv,
        //           Dictionary<string, ObjItem> dr)
        //{
        //    this.flag = false;
        //    this.dr = dr;
        //    this.dataGViewPL = gv;
        //    InitializeComponent();
        //}
        public SetWZDetail(YtWinContrl.com.datagrid.DataGView gv,
                   Dictionary<string, ObjItem> dr,string wd)
        {
            //this.flag = true;
            this.wd = wd;//库房编码
            this.dr = dr;
            this.dataGViewPL = gv;
            InitializeComponent();
        }
        private void ok_button_Click(object sender, EventArgs e)
        {
            if (this.kindcode_selTextInpt.Text.Trim().Length == 0)
            {
                WJs.alert("请输入类别编码！");
                kindcode_selTextInpt.Focus();
                return;
            }

            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.his.WZWareManag.SetWZManagKind";
            ac.Sql = "Save";
            ac.Add("WARECODE", this.warecode_yTextBox.Text);
            ac.Add("CHOSCODE", this.choscode_yTextBox.Text);
            ac.Add("KINDCODE", this.kindcode_selTextInpt.Value);
           // ac.SetKeyValue("WARECODE,CHOSCODE,KINDCODE");

            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();
            
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            this.dataGView1.reLoad(new object[] { His.his.Choscode, wd });
            InitForm();
            //if (!WJs.confirm("设置成功，是否继续添加？"))
            //{
            //    this.dataGView1.reLoad(new object[] { His.his.Choscode, wd });
            //   // this.Close();
            //}
            //else
            //{
            //    this.dataGView1.reLoad(new object[] { His.his.Choscode, wd });
            //    InitForm();
            //}
  
        }
      
        private void InitForm()
        {
            this.kindcode_selTextInpt.Text = "";
        }

        
        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetWZDetail_Load(object sender, EventArgs e)
        {
            ControlUtil.RegKeyEnter(this);
            
            //if (this.flag)
            //{
            //    //int warecode_int = DaoTool.ExecuteScalar(dao, OptContent.get("SaveWzInfo_seq"), data).ToInt();
            //  //  string warecode_s=LData.Es("SaveWzInfo_seq","LKWZ",new object[]{His.his.Choscode});

            //    if (wd.Length==1)
            //    {
            //        this.warecode_yTextBox.Text = "0" + wd;
            //    }
            //    else
            //    {
            //        this.warecode_yTextBox.Text = wd;
            //    }
            //    // this.warecode_yTextBox.Text = DaoTool.ExecuteScalar(dao, OptContent.get("SaveWzInfo_seq"), data).ToInt() + 1;
            //}
            //else
            //{
            //    this.warecode_yTextBox.Text = dr["库房编码"].ToString();
            //   // this.kindcode_selTextInpt.Text=dr[""]
            //}
            ////this.warecode_yTextBox.Text = dr["库房编码"].ToString();
            this.warecode_yTextBox.Text = wd;
            this.choscode_yTextBox.Text = His.his.Choscode;
            this.kindcode_selTextInpt.SelParam = His.his.Choscode+"|{key}|{key}|{key}|{key}";;
            
           
           
            this.kindcode_selTextInpt.Sql = "Getkindcode";
            this.dataGView1.Url = "MannageWZKind";
            this.dataGView1.reLoad(new object[] { His.his.Choscode,wd });
            //this.kindcode_selTextInpt.SelectedRow=1;
           
            
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (WJs.confirmFb("您确定要删除选择的物资类别吗？"))
                {
                    decimal isusekincode = Convert.ToDecimal(LData.Es("IsUseKindeCode", "LKWZ", new object[] { dr["类别编码"].ToString(), this.warecode_yTextBox.Text }));
                    if (isusekincode > 0)
                    {
                        WJs.alert("该类别已被使用，不能删除！");
                        return;
                    }
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.his.WZWareManag.SetWZManagKind";
                    // ac.Action = "Save";
                    ac.Sql = "DeleteWZKind";
                    //ac.Sql = "DelWZInfo";

                    ac.Add("CHOSCODE", dr["医疗机构编码"].ToString());
                    ac.Add("WARECODE", this.warecode_yTextBox.Text);
                    ac.Add("KINDCODE", dr["类别编码"].ToString());
                   // ac.SetKeyValue("CHOSCODE,WARECODE,KINDCODE");

                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的物资类别信息！");
            }
        }
    }
}
