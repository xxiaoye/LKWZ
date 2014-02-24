using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using ChSys;
using YtWinContrl.com.datagrid;
using BusinessManag.form;
using YiTian.db;
using YtUtil.tool;
using YtClient;

namespace BusinessManag
{
    public partial class WZUseRec : Form,IPlug
    {
        public WZUseRec()
        {
            InitializeComponent();
        }
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
        int rowid=0;
        private void WZUseRec_Load(object sender, EventArgs e)
        {
            TvList.newBind().add("全部", "2").add("有效", "1").add("作废", "0").Bind(this.ytComboBox_Status);
            TvList.newBind().add("全部", "2").add("有效", "1").add("作废", "0").Bind(this.Column15);
            //TvList.newBind().Load("WZUseRec2_Stock", new object[] { His.his.Choscode }).Bind(this.comboBox1);
            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.Column12);
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.Column24);
            TvList.newBind().Load("Chang_KaiJiedankeshi", new object[] { His.his.Choscode }).Bind(this.Column26);
            TvList.newBind().Load("Chang_KaiJiedankeshi", new object[] { His.his.Choscode }).Bind(this.Column19);
            this.dateTimeDuan2.SelectChange += new EventHandler(dateTimeDuan2_SelectChange);
            this.dateTimeDuan2.InitCorl();
            // this.dateTimeDuan2.SelectedIndex = 3;
            this.dateTimeDuan2.SelectedIndex = -1;
            this.dateTimePicker3.Value = DateTime.Now.AddMonths(-1);
           // this.dateTimePicker4.Value = DateTime.Now;
            this.ytComboBox_Status.SelectedIndex = 0;
            this.selTextInpt_Ware.Sql = "WZUseRec2_Stock1129";
            this.selTextInpt_Ware.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
          

        }
        void dateTimeDuan2_SelectChange(object sender, EventArgs e)
        {
            //button2_Click(null, null);
            this.dateTimePicker3= this.dateTimeDuan2.Start;
            this.dateTimePicker4 = this.dateTimeDuan2.End;
        }
        public void ReLoadData()
        {
            button2_Click(null, null);
        }
        public void ReLoad()
        {
            DataRow r = this.dataGView1.GetRowData();
           
            this.dataGView1.Url = "ScanSelectWZUseRec";
            this.dataGView1.reLoad(new object[] { r["USENO"], His.his.Choscode });
            //this.dataGView1.setFocus(rowid, 0);

            if (this.dataGView1.RowCount <= rowid)//设置焦点
            {
                this.dataGView1.setFocus(0, 0);

            }
            else
            {
                this.dataGView1.setFocus(rowid, 0);
            }
        }

        public void callback(string a,string b)//子窗体成功保存后，调用改方法 a :住院号， b:库房
        {


            this.ytComboBox_Status.Value = "1";
            this.selTextInpt_Ware.Value = a;
            this.selTextInpt_Ware.Text = LData.Es("LKWZ_GetWareName_ye", null, new object[] { His.his.Choscode, a });
            this.yTextBox_Num.Text = a;
            button2_Click(null, null);
            this.dataGView1.setFocus(rowid, 0);
            if (this.dataGView1.RowCount <= rowid)
            {
                this.dataGView1.setFocus(0, 0);

            }
            else
            {
                this.dataGView1.setFocus(rowid, 0);
            }
          
        }  
        private void toolStripButton8_Click(object sender, EventArgs e)//新增
        {
            //Dictionary<string,ObjItem> r2 = this.dataGView1.getRowData();
            DataRow r1 = this.dataGView1.GetRowData();

            WZUseRec_DetailScan ksd = new WZUseRec_DetailScan(r1, true);
            ksd.Main = this;
            ksd.ShowDialog();
            //if (ksd.isSc)
            //{
            //    button2_Click(null, null);

            //}    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGView1.ClearData();//1127
           
            SqlStr sql = SqlStr.newSql();//创建SqlStr对象
            if (this.ytComboBox_Status.Value != null)
            {
                if (this.ytComboBox_Status.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    if(this.ytComboBox_Status.Value !="2")
                    sql.Add("and a.STATUS = ?", this.ytComboBox_Status.Value.Trim());
                    
                }
            }
            if (this.selTextInpt_Ware.Value != null)
            {
                if (this.selTextInpt_Ware.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    sql.Add("and a.WARECODE = ?", this.selTextInpt_Ware.Value.Trim());

                }
            }
            else
            {
                WJs.alert("请选择库房！");
                return;
            }
                if (this.yTextBox_Num.Text.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    sql.Add("and a.SICKCODE = ?", this.yTextBox_Num.Text.Trim());
                }
          
          
                //添加查询条件及其参数
            sql.Add("and a.USEDATE >= ?", this.dateTimePicker3.Value);

            sql.Add("and a.USEDATE <= ?", this.dateTimePicker4.Value);
          

            //加载查询数据
            this.dataGView1.Url = "FindWZUseRecInfo";
            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sql);

            if (this.dataGView1.RowCount > 1)
            {
                if (this.dataGView1.RowCount <= rowid)
                {
                    this.dataGView1.setFocus(0, 0);

                }
                else
                {
                    this.dataGView1.setFocus(rowid, 0);
                }
              
            }
            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "条";


           // this.JinEHeJi.Text = this.dataGView1.Sum("零售金额").ToString() + "元";
            if (this.dataGView1.RowCount != 0)
            {
                decimal sum = 0;
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {
                    if (this.dataGView1["Column15", i].Value.ToString() == "1")
                    {
                        sum += Convert.ToDecimal(this.dataGView1["Column28", i].Value);
                    }

                }
                this.JinEHeJi.Text = sum.ToString() + "元";
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)//工具栏的刷新事件，已舍去
        {
            this.dataGView1.Url = "ScanWZUseRec";
            this.dataGView1.reLoad(new object[] { His.his.Choscode });
            this.dataGView1.setFocus(rowid, 0);//设置焦点
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DataRow r = this.dataGView1.GetRowData();

            if (r != null)
            {
                rowid = this.dataGView1.CurrentRow.Index;//获得当前行的行号
                if (r["STATUS"].ToString() != "1")
                {
                    WJs.alert("不能修改状态为作废的记录！");
                    return;

                }

                //if (ksd.isSc)
                //{
                //    ReLoad();
                //}
            }
            else {
               
                    WJs.alert("请选择一行要修改的数据！");
                    return;
               
            }
            WZUseRec_DetailScan ksd = new WZUseRec_DetailScan(r, false);

            ksd.Main = this;
            ksd.ShowDialog();

            //else
            //{
            //    WJs.alert("请选择要修改的使用信息");
            //}
        }
        void ac_ServiceFaiLoad(object sender, YtClient.data.events.LoadFaiEvent e)
        {
            WJs.alert(e.Msg.Msg);
          
        }

        void ac_ServiceLoad_Add(object sender, YtClient.data.events.LoadEvent e)
        {

            WJs.alert(e.Msg.Msg);
           
            ReLoadData();
        }
          
        
          
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DataRow r = this.dataGView1.GetRowData();


            if (r != null)
            {
            
                if (r["STATUS"].ToString() != "1")
                {
                    WJs.alert("只能删除状态为1的记录！");
                    return;
                }
                ActionLoad ac = ActionLoad.Conn();

                ac.Action = "LKWZSVR.lkwz.BusinessManag.WZUseRecSvr";
                ac.Sql = "DeleteWZUseRecInfo";
                 ac.Add("USENO", r["USENO"].ToString());
                 ac.Add("USENUM", r["USENUM"].ToString());
                 ac.Add("STOCKID", r["STOCKID"].ToString());
                 ac.Add("STOCKFLOWNO", r["STOCKFLOWNO"].ToString());
                 ac.Add("WZID", r["WZID"].ToString());
                 ac.Add("WARECODE", r["WARECODE"].ToString());
                ac.Add("CHOSCODE", r["CHOSCODE"].ToString());
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_Add);
            ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
            ac.Post();
                       
            }

            else
            {
                WJs.alert("请选择要删除的使用信息");
            }
        }

        
    }
}
