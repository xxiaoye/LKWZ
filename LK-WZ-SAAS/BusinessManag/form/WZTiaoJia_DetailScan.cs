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
using YtUtil.tool;
using YtClient;

namespace BusinessManag.form
{
    public partial class WZTiaoJia_DetailScan : Form
    {
        public WZTiaoJia_DetailScan()
        {
            InitializeComponent();
        }
         DataRow r;
         string TJID;
         string TJID1="";
         private bool isAdd;        // 保存（true），修改（false）标志位
         private bool isScan = false;  //查看标志位
         public WZTiaoJia_DetailScan(DataRow r, bool _isAdd)
         {
             isAdd = _isAdd;
             this.r = r;
             InitializeComponent();
         }
         public WZTiaoJia_DetailScan(DataRow r)
        {
            isScan = true;
            this.r = r;
            InitializeComponent();
        }

         public WZTiaoJia Main;

         //public bool isSc = false;
        // private TvList dwList;

         private void WZTiaoJia_DetailScan_Load(object sender, EventArgs e)//窗体加载
         {
             this.selTextInpt_Ware.Sql = "LKWZ_GetWare_ye";
             this.selTextInpt_Ware.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
             
             this.WindowState = FormWindowState.Maximized;
             //this.dataGView1.Url = "WZTiaoJia_ScanStockDetail";
             //this.dataGView1.reLoad(new object[] { r["WARECODE"].ToString(), His.his.Choscode });
             TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.Column24);
             this.dateTimePicker3.Value = DateTime.Now.AddMonths(-1);
             this.dateTimePicker4.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
             this.selTextInpt_Ware.Enabled = true;
             //TvList.newBind().SetCacheKey("pdkfbm").Load("WZPanDian_WareCode", new object[] { His.his.Choscode }).Bind(this.ytComboBox_PDWareNum);
             if (!this.isAdd)
             {

                 this.selTextInpt_Ware.Value = r["WARECODE"].ToString();
                 this.selTextInpt_Ware.Text = LData.Es("LKWZ_GetWareName_ye", null, new object[] { His.his.Choscode, r["WARECODE"].ToString() });


                 this.yTextBox_Reason.Text = r["TJREASON"].ToString();
                 if (r["MEMO"] != null)
                 {
                     this.yTextBox_Rec.Text = r["MEMO"].ToString();
                 }
                 this.selTextInpt_Ware.Enabled = false;
                 button1_Click_1(null, null);  //查询数据

             }
             if (this.isScan)
             {
                 this.toolStripButton3.Enabled = false;
                 //this.toolStripButton5.Enabled = false;
                 this.selTextInpt_Ware.Enabled = false;
                 this.yTextBox_Reason.ReadOnly = true;
                 this.yTextBox_Rec.ReadOnly = true;
                 this.btn_Save.Enabled = false;
                 button1_Click_1(null, null);  //查询数据
             }

         }




         void ac_ServiceFaiLoad(object sender, YtClient.data.events.LoadFaiEvent e) //提交失败返回后触发（保存）
         {


             WJs.alert(e.Msg.Msg);
         }

         void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)  //提交成功返回后触发
         {


             string[] t = e.Msg.Msg.Split('|');
             TJID1 = t[1];
             WJs.alert(t[0]);
             //WZPDZ.ReLoadData();
             //Main.toolStripButton4_Click(null, null);
             Main.callback(this.selTextInpt_Ware.Value);
            // isSc = true;
             //this.Close();

         }


         private void btn_Save_Click(object sender, EventArgs e) //确定按钮触发事件（保存）
         {
             if (this.selTextInpt_Ware.Value==null)
             {
                 WJs.alert("请选择库房！");
                 this.selTextInpt_Ware.Focus();
                 return;
             }
         
             if (this.yTextBox_Reason.Text.Trim().Length == 0)
             {
                 WJs.alert("请填写调价原因！");
                 this.yTextBox_Reason.Focus();
                 return;
             }
        


             ActionLoad ac = ActionLoad.Conn();

             if (!isAdd)
             {
                 //修改调价主表信息

                 if (TJID1.Trim().Length > 0)
                 {
                     ac.Add("TJID", TJID1);
                     //r["PDID"] = PDID1;

                 }
                 else if(r!=null)
                 {
                     ac.Add("TJID", r["TJID"].ToString());
                 }



                 ac.Action = "LKWZSVR.lkwz.BusinessManag.WZTiaoJiaSvr";
                 ac.Sql = "UpdataWZTiaoJiaInfo";
                 ac.Add("CHOSCODE", His.his.Choscode);
                 ac.Add("WARECODE", this.selTextInpt_Ware.Value);
                 ac.Add("MEMO", this.yTextBox_Rec.Text.ToString());
                 ac.Add("TJREASON", this.yTextBox_Reason.Text.ToString());



             }
             else
             {

                 //保存调价主表信息
                 ac.Add("TJID", null);
                 ac.Action = "LKWZSVR.lkwz.BusinessManag.WZTiaoJiaSvr";
                 ac.Sql = "SaveWZTiaoJiaInfo";
                 ac.Add("WARECODE",this.selTextInpt_Ware.Value);
                 ac.Add("TJREASON", this.yTextBox_Reason.Text.ToString());
                 //ac.Add("PDDATE", DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

                 ac.Add("MEMO", this.yTextBox_Rec.Text.ToString());
                 ac.Add("USERID", His.his.UserId);
                 ac.Add("USERNAME", His.his.UserName);
                 //ac.Add("RECDATE", DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                 ac.Add("CHOSCODE", His.his.Choscode);
                 ac.Add("STATUS", "1");
          
              
             }

             //保存调价细表信息
             if (this.dataGView1.RowCount > 0)
             {


                 int cout = 1;
                 for (int i = 0; i < this.dataGView1.RowCount; i++)
                 {


                     if (this.dataGView1["Column2", i].Value.ToString().Trim() != this.dataGView1["Column28", i].Value.ToString().Trim())
                     {
                         ac.Add("WZID" + cout, this.dataGView1["Column16", i].Value);
                         ac.Add("STOCKFLOWNO" + cout, this.dataGView1["Column15", i].Value);
                         ac.Add("NEWPRICE" + cout, this.dataGView1["Column2", i].Value);
                         cout++;
                     }
                 }
                 ac.Add("MyCount", cout);

                 ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                 ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
                 ac.Post();
             }
             else
             {
                 WJs.alert("详细调价数据不能为空！");
                 return;
             }





             if (isAdd)
             {

                 if (WJs.confirmFb("是否继续录入调价信息？"))
                 {
                     this.isAdd = false;
                     this.selTextInpt_Ware.Enabled = false;
                     button1_Click_1(null, null);

                 }
                 else
                 {
                     this.Close();
                 }
             }
             else
             {
                 this.Close();
             }
         }

         private void button1_Click_1(object sender, EventArgs e) //查询
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

             if (isScan)
             {

                 this.dataGView1.Url = "WZTiaoJia_ScanStockDetail";
                 this.dataGView1.reLoad(new object[] { r["TJID"].ToString(), r["TJID"].ToString(), His.his.Choscode, this.selTextInpt_Ware.Value }, sql);



             }
             else if (!isAdd)
             {

                 if (this.TJID1.ToString().Length > 0)
                 {
                     this.dataGView1.Url = "WZTiaoJia_EditStockDetail";
                     this.dataGView1.reLoad(new object[] { TJID1, His.his.Choscode, this.selTextInpt_Ware.Value, TJID1, TJID1, His.his.Choscode, this.selTextInpt_Ware.Value }, sql);//1129

                 }
                 else if (r != null)
                 {
                     this.dataGView1.Url = "WZTiaoJia_EditStockDetail";
                     this.dataGView1.reLoad(new object[] { r["TJID"].ToString(), His.his.Choscode, this.selTextInpt_Ware.Value, r["TJID"].ToString(), r["TJID"].ToString(), His.his.Choscode, this.selTextInpt_Ware.Value }, sql);//1129
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

            if (this.dataGView1.RowCount > 0)
             {
                 for (int i = 0; i < this.dataGView1.RowCount; i++)
                 {
                     if (this.dataGView1["Column2", i].Value == null || this.dataGView1["Column2", i].Value.ToString().Length==0)
                     {
                         this.dataGView1["Column2", i].Value = this.dataGView1["Column28", i].Value;
                     }
                     if (this.dataGView1["Column17", i].Value != null && this.dataGView1["Column25", i].Value != null && this.dataGView1["Column24", i].Value != null)
                     {

                         string string1 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView1["Column17", i].Value });
                         string string2 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView1["Column24", i].Value });
                         this.dataGView1["Column25", i].Value = "1" + string1 + "=" + this.dataGView1["Column25", i].Value + string2;
                         //this.dataGView1["Column25", i].Value = "1" + this.dataGView1["Column17", i].EditedFormattedValue + "=" + this.dataGView1["Column25", i].Value + this.dataGView1["Column24", i].EditedFormattedValue;
                           
                     }
                 }
             }

            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "条";
         }

         private void btn_Cancel_Click(object sender, EventArgs e)//窗体上的取消按钮
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
             button1_Click_1(null, null);  //查询数据
         }

         private void toolStripButton3_Click(object sender, EventArgs e)//删除
         {
             DataRow row = this.dataGView1.GetRowData();

             if (row != null)
             {
                 if (WJs.confirmFb("确定要删除选择的调价详细信息吗？"))
                 {

                     this.dataGView1.SelectedRows[0].Cells["Column2"].Value = this.dataGView1.SelectedRows[0].Cells["Column28"].Value;

                     if (!isAdd &&  r["TJID"]!=null)
                     {
                         ActionLoad ac = ActionLoad.Conn();
                         ac.Action = "LKWZSVR.lkwz.BusinessManag.WZTiaoJiaSvr";
                         ac.Sql = "DelWZTiaoJiaDetailInfo";
                         ac.Add("TJID", r["TJID"].ToString());
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
                 WJs.alert("请选择要删除的调价详细信息");
             }
         }
    }
}
