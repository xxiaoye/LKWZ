using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChSys;
using YtWinContrl.com.datagrid;
using YiTian.db;
using YtUtil.tool;
using YtClient;

namespace BusinessManag.form
{
    public partial class WZPanDian_DetailScan : Form
    {
        public WZPanDian_DetailScan()
        {
            InitializeComponent();
        }
        DataRow r;
        string PDID;
        string PDID1=""; 
        private bool isAdd;        // 保存（true），修改（false）标志位
        private bool isScan=false;  //查看标志位
        //DataTable dt;
        //string t;
        public WZPanDian_DetailScan(DataRow r, bool _isAdd)
        {
            isAdd = _isAdd;
            this.r = r;
            InitializeComponent();
        }
        public WZPanDian_DetailScan(DataRow r)
        {
            isScan = true;
            this.r = r;
            InitializeComponent();
        }
        public bool isSc = false;
        public WZPanDian Main;
        // private TvList dwList;
        private void WZPanDian_DetailScan_Load(object sender, EventArgs e) //加载窗体
        {

            this.selTextInpt_Ware.Sql = "LKWZ_GetWare_ye";
            this.selTextInpt_Ware.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";

            this.WindowState = FormWindowState.Maximized;
            TvList.newBind().Load("Chang_LSDanWeiBianMa",null).Bind(this.Column24);
            this.dateTimePicker3.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker4.Value=new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            //this.dataGView1.Url = "WZPD_ScanStockDetail";
            //this.dataGView1.reLoad(new object[] { r["WARECODE"].ToString(), His.his.Choscode });
            this.selTextInpt_Ware.Enabled = true;


            //TvList.newBind().SetCacheKey("pdkfbm").Load("WZPanDian_WareCode", new object[] { His.his.Choscode }).Bind(this.ytComboBox_PDWareNum);

            if (!this.isAdd)
            {

                this.selTextInpt_Ware.Value = r["WARECODE"].ToString();
                this.selTextInpt_Ware.Text = LData.Es("LKWZ_GetWareName_ye", null, new object[] { His.his.Choscode, r["WARECODE"].ToString() });


                if (r["MEMO"] != null)
                {
                    this.yTextBox_Rec.Text = r["MEMO"].ToString();
                }
                this.selTextInpt_Ware.Enabled = false;
                button1_Click_2(null, null);  //查询数据
              
            }
            if (this.isScan)
            {
                this.toolStripButton3.Enabled = false;
                //this.toolStripButton5.Enabled = false;
                this.selTextInpt_Ware.Enabled = false;
                this.yTextBox_Rec.Enabled = false;
                this.btn_Save.Enabled = false;
                button1_Click_2(null, null);  //查询数据
            }
        }


  

        void ac_ServiceFaiLoad(object sender, YtClient.data.events.LoadFaiEvent e) //提交失败返回后触发（保存）
        {
            
         
            WJs.alert(e.Msg.Msg);
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)  //提交成功返回后触发
        {


            string[] t = e.Msg.Msg.Split('|');
            PDID1 = t[1];
            WJs.alert(t[0]);
            //WZPDZ.ReLoadData();
            //Main.toolStripButton4_Click(null, null);
            Main.callback(this.selTextInpt_Ware.Value);
            isSc = true;
            //this.Close();

        }

        private void button1_Click_2(object sender, EventArgs e) 
        {
            
            if (this.selTextInpt_Ware.Value == null)
            {
                WJs.alert("请选择库房！");
                this.selTextInpt_Ware.Focus();
                return;
            }

          
            SqlStr sql = SqlStr.newSql();

           
            if (this.checkBox1.Checked)
            {
                sql.Add(" and a.NUM > 0 ");
            }
            if (this.checkBox2.Checked)
            {
                sql.Add(" and e.YKNUM is not null ");
            }
            if (this.dateTimePicker3.Value < this.dateTimePicker4.Value)
            {
                sql.Add("and a.INDATE >= ?", this.dateTimePicker3.Value);

                sql.Add("and a.INDATE <= ?", this.dateTimePicker4.Value);

            }
            else if (this.dateTimePicker3.Value != this.dateTimePicker4.Value)
            {
                WJs.alert("查询时间段有误！");
               
                return;

            }
            this.dataGView1.ClearData();
            if ( isScan)
            {

                this.dataGView1.Url = "WZPD_ScanStockDetail";
                this.dataGView1.reLoad(new object[] { r["PDID"].ToString(), r["PDID"].ToString(), His.his.Choscode, this.selTextInpt_Ware.Value }, sql);

                


            }
            else if (!isAdd)
            {
                if (this.PDID1.ToString().Length > 0)
                {
                    this.dataGView1.Url = "WZPD_EditStockDetail";
                    this.dataGView1.reLoad(new object[] { PDID1, His.his.Choscode, this.selTextInpt_Ware.Value, PDID1, PDID1, His.his.Choscode, this.selTextInpt_Ware.Value }, sql);

                }
                else if (r != null)
                {

                    this.dataGView1.Url = "WZPD_EditStockDetail";
                    this.dataGView1.reLoad(new object[] { r["PDID"].ToString(), His.his.Choscode, this.selTextInpt_Ware.Value, r["PDID"].ToString(), r["PDID"].ToString(), His.his.Choscode, this.selTextInpt_Ware.Value }, sql);
                }
                else 
                {

                    this.dataGView1.Url = "WZPD_SearchStockDetail";
                    this.dataGView1.reLoad(new object[] { this.selTextInpt_Ware.Value, His.his.Choscode }, sql);

                }

            }
            else 
            {

                this.dataGView1.Url = "WZPD_SearchStockDetail";
                this.dataGView1.reLoad(new object[] { this.selTextInpt_Ware.Value, His.his.Choscode }, sql);

                
            }

            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.Column24);

          
                if (this.dataGView1.RowCount > 0)
                {
                    for (int i = 0; i < this.dataGView1.RowCount; i++)
                    {
                        if (this.dataGView1["Column2", i].Value ==null)
                        {
                            this.dataGView1["Column2", i].Value = this.dataGView1["Column12", i].Value;
                        }
                        if (this.dataGView1["Column18", i].Value != null && this.dataGView1["Column25", i].Value != null && this.dataGView1["Column24", i].Value != null)
                        {

                           string string1 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView1["Column18", i].Value });
                            string string2 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView1["Column24", i].Value });
                           this.dataGView1["Column25", i].Value = "1" + string1 + "=" + this.dataGView1["Column25", i].Value+string2;
                            //this.dataGView1["Column25", i].Value = "1" + this.dataGView1["Column18", i].EditedFormattedValue + "=" + this.dataGView1["Column25", i].Value + this.dataGView1["Column24", i].EditedFormattedValue;
                           
                        }

                    }
                }

                this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔"; 
        } //点击查询按钮触发事件

        private void toolStripButton5_Click(object sender, EventArgs e) //工具栏保存按钮
        {
            //dataGView1_CellEndEdit_1(null, null);
            // var k=this.dataGView1.CurrentCell.Value;
            //if (this.ytComboBox_PDWareNum.Text.Trim().Length == 0)
            //{
            //    WJs.alert("请选择库房！");
            //    this.ytComboBox_PDWareNum.Focus();
            //    return;
            //}

            ActionLoad ac = ActionLoad.Conn();
            if (!isAdd)
            {
                //修改盘点主表信息

                if (PDID1.Trim().Length > 0)
                {
                    ac.Add("PDID", PDID1);
                    //r["PDID"] = PDID1;
                   
                }
                else if(r!=null)
                {
                    ac.Add("PDID", r["PDID"].ToString());
                }


                ac.Action = "LKWZSVR.lkwz.BusinessManag.WZPanDianSvr";
                ac.Sql = "UpdataWZPanDianInfo";
                ac.Add("CHOSCODE", His.his.Choscode);
                ac.Add("WARECODE", this.selTextInpt_Ware.Value);
                ac.Add("MEMO", this.yTextBox_Rec.Text.ToString());
                ac.Add("USERID", His.his.UserId);
                ac.Add("USERNAME", His.his.UserName);

               
               

            }
            else
            {
                //保存盘点主表信息
                ac.Add("PDID", null);
                ac.Action = "LKWZSVR.lkwz.BusinessManag.WZPanDianSvr";
                ac.Sql = "SaveWZPanDianInfo";
                ac.Add("WARECODE", this.selTextInpt_Ware.Value);

                //ac.Add("PDDATE", DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

                ac.Add("MEMO", this.yTextBox_Rec.Text.ToString());
                ac.Add("USERID", His.his.UserId);
                ac.Add("USERNAME", His.his.UserName);
                //ac.Add("RECDATE", DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                ac.Add("CHOSCODE", His.his.Choscode);
                ac.Add("STATUS", "1");
               
              
                //ac.Add("SHDATE", DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
                //ac.Add("SHUSERID", His.his.UserId);
                //ac.Add("SHUSERNAME", His.his.UserName);
                //ac.Add("JZDATE", DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
                //ac.Add("JZUSERID", His.his.UserId);
                //ac.Add("JZUSERNAME", His.his.UserName);

                //ac.Add("INID", this.yTextBox_StockInNum.Text);
                //ac.Add("OUTID", this.yTextBox_StockOutNum.Text);

                //保存盘点细表信息
            

                //decimal a=0;
                //decimal b;
                //decimal c;

                //ac.Add("PDID", PDID);
            }
           
            //保存盘点细表信息
            if (this.dataGView1.RowCount > 0)
            {


                int cout = 1;
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {


                    if (this.dataGView1["Column9", i].Value!=null  && this.dataGView1["Column12", i].Value.ToString().Length > 0 && this.dataGView1["Column2", i].Value.ToString().Length>0)
                    {
                        ac.Add("WZID" + cout, this.dataGView1["Column16", i].Value);
                        ac.Add("STOCKFLOWNO" + cout, this.dataGView1["Column15", i].Value);
                        ac.Add("FACTNUM" + cout, this.dataGView1["Column2", i].Value);
                        ac.Add("YKNUM" + cout, this.dataGView1["Column9", i].Value);
                        cout++;
                    }
                }
                ac.Add("MyCount", cout);

                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
                ac.Post();
            }
            else {
                WJs.alert("详细盘点数据不能为空！");
                return;
            }
               
               
             
          

        }

        private void btn_Save_Click(object sender, EventArgs e) //窗体上的保存按钮
        {
            //toolStripButton5_Click(null, null);
            if (this.selTextInpt_Ware.Value == null)
            {
                WJs.alert("请选择库房！");
                this.selTextInpt_Ware.Focus();
                return;
            }
            toolStripButton5_Click(null, null);
            if (WJs.confirmFb("是否继续录入盘点信息？"))
            {
                this.isAdd = false;
                this.selTextInpt_Ware.Enabled = false;
                button1_Click_2(null, null);

            }
            else 
            {
                this.Close();
            }
        }

        private void dataGView1_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)  //表格中的单元格结束编辑时触发事件
        {
           //1127 if (this.dataGView1.CurrentCell.ReadOnly == false && this.dataGView1.CurrentRow.Cells["Column12"].Value !=null)
            if (this.dataGView1.CurrentCell.ColumnIndex==5 && this.dataGView1.CurrentRow.Cells["Column12"].Value != null)
            {
                var a = this.dataGView1.CurrentCell.Value;
                int Count = 0;
                if (a != null && a.ToString().Length>0)
                {

                    if (int.TryParse(a.ToString().Trim(), out Count) && Count >= 0)
                    {
                        int Value =Count- Convert.ToInt32(this.dataGView1.CurrentRow.Cells["Column12"].Value);
                        //if (Value != 0)
                        //{
                        this.dataGView1.CurrentRow.Cells["Column9"].Value = Value;
                        //}
                        //else
                        //{
                        //    this.dataGView1.CurrentRow.Cells["Column9"].Value = null;
                        //}
                    }
                    else
                    {
                        this.dataGView1.CurrentCell.Value = null;//1127
                        WJs.alert("输入的实际数量有误，请输入大于零的整数。");
                      
                    }
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e) //窗体上的取消按钮
        {
            if (!this.isScan)
            {
                if (WJs.confirmFb("是否确定不保存退出？"))
                {
                    this.Close();
                }
            }
            else 
            {
                this.Close();
            
            }
        }

        void ac_ServiceDetailDeleteLoad(object sender, YtClient.data.events.LoadEvent e) //提交成功返回后触发(删除）
        {
            WJs.alert(e.Msg.Msg);
            button1_Click_2(null, null);  //查询数据
        }
        private void toolStripButton3_Click(object sender, EventArgs e)  //工具栏删除按钮
        { 
            DataRow row = this.dataGView1.GetRowData();

            if (row != null)
            {
                if (WJs.confirmFb("确定要删除选择的盘点详细信息吗？"))
                {

                    this.dataGView1.SelectedRows[0].Cells["Column9"].Value = null;
                    this.dataGView1.SelectedRows[0].Cells["Column2"].Value = this.dataGView1.SelectedRows[0].Cells["Column12"].Value;

                    if (!isAdd && r["PDID"]!=null)
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkwz.BusinessManag.WZPanDianSvr";
                        ac.Sql = "DelWZPanDianDetailInfo";
                        ac.Add("PDID", r["PDID"].ToString());
                        ac.Add("STOCKFLOWNO", Convert.ToDecimal(row["FLOWNO"]));
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceDetailDeleteLoad);
                        ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
                        ac.Post();
                    }         

                }
            }
            else
            {
                WJs.alert("请选择要删除的盘点信息");
            }
        }
       
    }
}

