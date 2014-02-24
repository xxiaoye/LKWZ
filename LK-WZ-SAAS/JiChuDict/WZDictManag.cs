using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChSys;
using JiChuDict.form;
using YtPlugin;
using YtUtil.tool;
using YtClient;
using YtWinContrl.com.datagrid;

namespace JiChuDict
{
    public partial class WZDictManag : Form,IPlug
    {

        #region IPlug 成员

        public Form getMainForm()
        {
            return this;
        }

        public void initPlug(IAppContent app, object[] param)
        {
           
        }

        public bool unLoad()
        {
            return true;
        }

        #endregion

        public WZDictManag()
        {
            InitializeComponent();
        }
        int rowid = 0;
        int rowidxi = 0;
        private void WZDictManag_Load(object sender, EventArgs e)
        {
           // WJs.SetDictTimeOut();//什么意思？
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column20);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column22);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column4);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column5);

            TvList.newBind().Load("WZDict_CountCode", new object[] { His.his.Choscode }).Bind(this.Column10); //这里修改了，查询所有
            TvList.newBind().Load("WZDict_SingerCode2", null).Bind(this.Column12);
            TvList.newBind().Load("WZDict_SingerCode2", null).Bind(this.Column14);
            TvList.newBind().Load("WZDict_SFXMBM_Valid1",new object[]{His.his.Choscode}).Bind(this.Column27);

            this.selTextInpt_KindCode.SelParam = His.his.Choscode+"|{key}|{key}|{key}|";//为什么要这样写？
            selTextInpt_KindCode.BxSr = false;//必须输入查询关键字
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt_KindCode.Sql = "WZDict_KindCode1";

            ytTreeView_Main.vFiled = "KINDCODE";
            ytTreeView_Main.tFiled = "KINDNAME";
            ytTreeView_Main.pFiled = "SUPERCODE";
            ytTreeView_Main.sql = "ScanWZKind";
            this.ytTreeView_Main.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(ytTreeView1_FormatText);


            this.ytTreeView_Main.reLoad(new object[] { His.his.Choscode });
           

          
          
        }
        void ytTreeView1_FormatText(YtWinContrl.com.events.TextFormatEvent e)
        {


            e.FmtText = e.row["KINDNAME"].ToString();

        }

        private void toolStripButton4_Click(object sender, EventArgs e)//浏览
        {

            ytTreeView_Main.sql = "ScanWZKind";

            this.ytTreeView_Main.reLoad(new object[] { His.his.Choscode });
            this.dataGView_Main.ClearData();
            this.dataGView_Xi.ClearData();
            button1_Click(null, null);
           
        }
        public  void  ReLoadData()
        {
             //DataRow r1 = this.ytTreeView_Main.getSelectRow();

            button1_Click(null, null);
            //this.yTxtBox_Name.Text = "";
            //if (this.selTextInpt_KindCode.Value != null)
            //{
            //    this.dataGView_Main.Url = "FistScanWZDict";
            //    this.dataGView_Main.reLoad(new object[] { this.selTextInpt_KindCode.Value + "%", His.his.Choscode });
            //}
            //else
            //{
            //    this.dataGView_Main.Url = "FistScanWZDict1";
            //    this.dataGView_Main.reLoad(new object[] { His.his.Choscode });
            //}

           
            if (this.dataGView_Main.RowCount <= rowid)
            {
                this.dataGView_Main.setFocus(0, 0);

            }
            else
            {
                this.dataGView_Main.setFocus(rowid, 0);
            }
        }
        public void ReLoadData(string s)
        {

            SqlStr sql = SqlStr.newSql();//创建SqlStr对象
            this.dataGView_Main.Url = "FindWZDict";
            this.dataGView_Xi.ClearData();
           

            if (s != null)
            {
                sql.Add("and a.KINDCODE like ?", s + "%");
                this.selTextInpt_KindCode.Value = s;
                this.selTextInpt_KindCode.Text = LData.Es("WZDictFindKindName", null, new object[] { s, His.his.Choscode });
               this.yTxtBox_Name.Text = " ID、名称、拼音码、五笔码、简称、简码、别名、别名简码";

            }


            if (this.yTxtBox_Name.Text.Trim().Length > 0 && this.yTxtBox_Name.Text.Trim().ToString() != "ID、名称、拼音码、五笔码、简称、简码、别名、别名简码")
            {
                //添加查询条件及其参数
                sql.Add("and ( a.WZID like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.WZNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.PYCODE like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.WBCODE like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.SHORTNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.SHORTCODE like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.ALIASNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.ALIASCODE like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add(")");
            }

            this.dataGView_Main.reLoad(new object[] { His.his.Choscode }, sql);





            //this.yTxtBox_Name.Text = "";
            //this.dataGView_Main.Url = "FistScanWZDict";
            //if (s != null)
            //{
            //    this.dataGView_Main.reLoad(new object[] { s, His.his.Choscode });
            //}
            //else
            //{
            //    this.dataGView_Main.Url = "FistScanWZDict1";
            //    this.dataGView_Main.reLoad(new object[] { His.his.Choscode });
            //}
           
            if (this.dataGView_Main.RowCount <= rowid)
            {
                this.dataGView_Main.setFocus(0, 0);

            }
            else
            {
                this.dataGView_Main.setFocus(rowid, 0);
            }
        }
        public void ReLoadDataDetail()
        {
            DataRow r1 = this.dataGView_Xi.GetRowData();
            //ytTreeView1.sql = "ScanWZKind";
            //this.ytTreeView1.reLoad(new object[] { His.his.Choscode });
            this.dataGView_Xi.Url = "WZDictDetailInfo";
            if (r1 != null)
            {
                this.dataGView_Xi.reLoad(new object[] { r1["WZID"], His.his.Choscode });
            }
            //this.dataGView_Xi.setFocus(rowid, 0);
            if (this.dataGView_Xi.RowCount <= rowidxi)
            {
                this.dataGView_Xi.setFocus(0, 0);

            }
            else
            {
                this.dataGView_Xi.setFocus(rowidxi, 0);
            }
        }
        public void ReLoadDataDetail(string a)
        {
            //DataRow r1 = this.dataGView1.GetRowData();
            //ytTreeView1.sql = "ScanWZKind";
            //this.ytTreeView1.reLoad(new object[] { His.his.Choscode });
            this.dataGView_Xi.Url = "WZDictDetailInfo";
            this.dataGView_Xi.reLoad(new object[] {a, His.his.Choscode });
           // this.dataGView_Xi.setFocus(rowid, 0);

            if (this.dataGView_Xi.RowCount <= rowidxi)
            {
                this.dataGView_Xi.setFocus(0, 0);

            }
            else
            {
                this.dataGView_Xi.setFocus(rowidxi, 0);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DataRow r= this.ytTreeView_Main.getSelectRow();

              if (r != null)
            {
                    WZDict_Add ks = new WZDict_Add(r, true);
                    ks.WZDM = this;
                    ks.ShowDialog();
            }

              else
              {

                  WZDict_Add ks = new WZDict_Add(null, true);
                  ks.WZDM = this;
                  ks.ShowDialog();
              }
            
        }

        private void toolStripButton5_Click(object sender, EventArgs e)//修改
        {
            DataRow r = this.dataGView_Main.GetRowData();
            bool IfUse_Updata=false;
            if (r != null)
            {

                string str1 = LData.Es("DelWZDictInfo_Use1", "LKWZ", new object[] { r["WZID"], His.his.Choscode });
                string str2 = LData.Es("DelWZDictInfo_Use2", "LKWZ", new object[] { r["WZID"], His.his.Choscode });
                string str3 = LData.Es("DelWZDictInfo_Use3", "LKWZ", new object[] { r["WZID"], His.his.Choscode });
                string str4 = LData.Es("DelWZDictInfo_Use4", "LKWZ", new object[] { r["WZID"], His.his.Choscode });
                string str5 = LData.Es("DelWZDictInfo_Use5", "LKWZ", new object[] { r["WZID"], His.his.Choscode });
                string str6 = LData.Es("DelWZDictInfo_Use6", "LKWZ", new object[] { r["WZID"], His.his.Choscode });
                string str7 = LData.Es("DelWZDictInfo_Use7", "LKWZ", new object[] { r["WZID"], His.his.Choscode });
                string str8 = LData.Es("DelWZDictInfo_Use8", "LKWZ", new object[] { r["WZID"], His.his.Choscode });


           if (str1 != null || str1 != null || str1 != null || str1 != null || str1 != null || str1 != null || str1 != null || str1 != null)
            {
                IfUse_Updata = true;
            }


           
                rowid = this.dataGView_Main.CurrentRow.Index;
                WZDict_Add ks = new WZDict_Add(r, false,IfUse_Updata);
                ks.WZDM = this;
                ks.ShowDialog();
              
            }
           
            else
            {
                WJs.alert("请选择要编辑的物资信息");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
              ReLoadData();
         
        }
        void ac_ServiceLoadDetail(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            ReLoadDataDetail();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//删除
        {
           
            DataRow r7 = this.dataGView_Main.GetRowData();
            if (r7 != null)
            {
                if (WJs.confirmFb("确定要删除选择的物资信息吗？\n删除后不能恢复!"))
                {


                    rowid = this.dataGView_Main.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkwz.JiChuDict.WZDictSvr";
                    ac.Sql = "DelWZDictInfo";
                    ac.Add("WZID", r7["WZID"]);
                    ac.Add("IFUSE", r7["IFUSE"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.Add("flag", "0");
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的物资信息");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)//启用
        {
            DataRow r8 = this.dataGView_Main.GetRowData();
            if (r8 != null)
            {
                if (WJs.confirmFb("是否启用？"))
                {
                    rowid = this.dataGView_Main.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkwz.JiChuDict.WZDictSvr";
                    ac.Sql = "StartWZDictInfo";
                    ac.Add("WZID", r8["WZID"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要启用的物资信息");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)//停用
        {
            DataRow r9 = this.dataGView_Main.GetRowData();
            if (r9 != null)
            {
                if (WJs.confirmFb("是否停用？"))
                {
                    rowid = this.dataGView_Main.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkwz.JiChuDict.WZDictSvr";
                    ac.Sql = "StopWZDictInfo";
                    ac.Add("WZID", r9["WZID"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要停用的物资信息");
            }
        }

        private void button2_Click(object sender, EventArgs e)//清空
        {

            this.yTxtBox_Name.Text = "";
       
        }

        private void button1_Click(object sender, EventArgs e)//查询
        {
            SqlStr sql = SqlStr.newSql();//创建SqlStr对象
            this.dataGView_Main.Url = "FindWZDict";
            this.dataGView_Xi.ClearData();

            if (this.selTextInpt_KindCode.Value != null)
            {
                sql.Add("and a.KINDCODE like ?", this.selTextInpt_KindCode.Value + "%");
                this.ytTreeView_Main.SelectedNode = this.ytTreeView_Main.Nodes[this.selTextInpt_KindCode.Text];
  
            }
          

            if (this.yTxtBox_Name.Text.Trim().Length > 0 && this.yTxtBox_Name.Text.Trim().ToString() != "ID、名称、拼音码、五笔码、简称、简码、别名、别名简码")
            {
                //添加查询条件及其参数
                sql.Add("and ( a.WZID like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.WZNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.PYCODE like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.WBCODE like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.SHORTNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.SHORTCODE like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.ALIASNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.ALIASCODE like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add(")");
            }

            this.dataGView_Main.reLoad(new object[] { His.his.Choscode }, sql);
           
            //if (this.dataGView_Main.SelectedRows.Count > 0)
            //{
            //    DataGridViewSelectedRowCollection dvr = this.dataGView_Main.SelectedRows;

            //    if (dvr[0].Cells["WZID"] != null)
            //    {
            //        this.dataGView_Xi.Url = "WZDictDetailInfo";
            //        this.dataGView_Xi.reLoad(new object[] { dvr[0].Cells["WZID"], His.his.Choscode });
            //    }
            //}

        }

        private void ytTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DataRow r = this.ytTreeView_Main.getSelectRow();

            if (r != null)
            {
                this.dataGView_Xi.ClearData();
                this.dataGView_Main.Url = "ScanWZDict";
                if (r["IFEND"].ToString() == "1")
                    this.dataGView_Main.reLoad(new object[] { r["KINDCODE"], His.his.Choscode });

                //if (this.dataGView_Main.SelectedRows.Count > 0)
                //{
                //    DataGridViewSelectedRowCollection dvr = this.dataGView_Main.SelectedRows;

                //    if (dvr[0].Cells["WZID"] != null)
                //    {
                //        this.dataGView_Xi.Url = "WZDictDetailInfo";
                //        this.dataGView_Xi.reLoad(new object[] { dvr[0].Cells["WZID"], His.his.Choscode });
                //    }
                //}

            }
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    DataRow r12 = this.ytTreeView_Main.getSelectRow();
        //    this.dataGView_Xi.ClearData();
        //    if (r12 != null)
        //    {
                
               
        //            this.dataGView_Main.Url = "FilterWZDictInfo";
        //            this.dataGView_Main.reLoad(new object[] { r12["KINDCODE"].ToString()+"%", His.his.Choscode });
                
        //    }
        //    else
        //    {
        //        WJs.alert("请选择要过滤的物资信息");
        //    }
        //}

       

       




      
    

      

     

      

        private void yTxtBox_Name_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.yTxtBox_Name.Text.Trim() == "ID、名称、拼音码、五笔码、简称、简码、别名、别名简码")
            {
                this.yTxtBox_Name.Text = "";
            }
         
        }


        private void yTxtBox_Name_Leave(object sender, EventArgs e)
        {
            if (this.yTxtBox_Name.Text.Length <= 0)
            {
                this.yTxtBox_Name.Text = "ID、名称、拼音码、五笔码、简称、简码、别名、别名简码";
            }
        }

     

    

      
        private void ytTreeView_Main_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DataRow r = this.ytTreeView_Main.getSelectRow();

            if (r != null)
            {
                this.dataGView_Xi.ClearData();
                this.dataGView_Main.Url = "ScanWZDict";
                if (r["IFEND"].ToString() == "1")
                    this.dataGView_Main.reLoad(new object[] { r["KINDCODE"], His.his.Choscode });
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
              
             

            DataRow r2 = this.dataGView_Main.GetRowData();
            DataRow r3 = this.dataGView_Xi.GetRowData();
            if (r3 != null)
            {
                WJs.alert("该物资已经有明细数据了，不能再增加！");
                return;
            }


            WZDictDetail ksd = new WZDictDetail(r2, true);
            ksd.WZDMang = this;

            ksd.ShowDialog();

       
        }

        private void button6_Click(object sender, EventArgs e)
        {
             
            DataRow r = this.dataGView_Xi.GetRowData();
            if (r != null)
            {
                rowidxi = this.dataGView_Xi.CurrentRow.Index;
                WZDictDetail ks = new WZDictDetail(r, false);
                ks.WZDMang = this;
                ks.ShowDialog();

            }
            else
            {
                WJs.alert("请选择要编辑的详细物资信息");
            }
       

        }

        private void button4_Click(object sender, EventArgs e)
        {

            DataRow r = this.dataGView_Xi.GetRowData();
            if (r != null)
            {
                if (WJs.confirmFb("确定要删除选择的详细物资信息吗？\n删除后不能恢复!"))
                {
                    rowidxi = this.dataGView_Xi.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkwz.JiChuDict.WZDictSvr";
                    ac.Sql = "DelWZDictDetailInfo";
                    ac.Add("WZID", r["WZID"]);
                    ac.Add("CHOSCODE", His.his.Choscode);

                    ac.Add("flag", "1");
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoadDetail);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的详细物资信息");
            }
       
        }

        private void dataGView_Main_CellClick(object sender, DataGridViewCellEventArgs e) //单击主表
        {
            DataRow r = this.dataGView_Main.GetRowData();

            if (r != null)
            {
                this.dataGView_Xi.Url = "WZDictDetailInfo";
                this.dataGView_Xi.reLoad(new object[] { r["WZID"], His.his.Choscode });
            }
        }

     
       
       

       

    }
}
