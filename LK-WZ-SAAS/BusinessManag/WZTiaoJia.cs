using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtWinContrl.com.datagrid;
using ChSys;
using YtPlugin;
using YtUtil.tool;
using BusinessManag.form;
using YtClient;

namespace BusinessManag
{
    public partial class WZTiaoJia : Form,IPlug
    {
        public WZTiaoJia()
        {
            InitializeComponent();
        }

        int rowid;

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
        private void WZTiaoJia_Load(object sender, EventArgs e)
        {


            TvList.newBind().add("全部", "9").add("已生效", "6").add("已审核", "2").add("等待审核", "1").add("作废", "0").Bind(this.ytComboBox_Status);
            //TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.comboBox1);
            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.Column2);
            TvList.newBind().add("全部", "9").add("已结转", "6").add("已审核", "2").add("等待审核", "1").add("作废", "0").Bind(this.Column4);
          
            this.dateTimeDuan2.SelectChange += new EventHandler(dateTimeDuan2_SelectChange);
            this.dateTimeDuan2.InitCorl();
            // this.dateTimeDuan2.SelectedIndex = 3;
            this.dateTimeDuan2.SelectedIndex = -1;
            this.dateTimePicker3.Value = DateTime.Now.AddMonths(-1);
            //this.dateTimePicker4.Value = DateTime.Now;
            this.ytComboBox_Status.SelectedIndex = 0;
            this.selTextInpt_Ware.Sql = "LKWZ_GetWare_ye";
            this.selTextInpt_Ware.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";

        }
        void dateTimeDuan2_SelectChange(object sender, EventArgs e)
        {

            this.dateTimePicker3 = this.dateTimeDuan2.Start;
            this.dateTimePicker4 = this.dateTimeDuan2.End;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGView1.ClearData();//1127
            this.dataGView2.ClearData();//1127
            SqlStr sql = SqlStr.newSql();//创建SqlStr对象
            if (this.ytComboBox_Status.Value != null)
            {
                if (this.ytComboBox_Status.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    if (this.ytComboBox_Status.Value != "9")
                        sql.Add("and STATUS = ?", this.ytComboBox_Status.Value.Trim());

                }
            }
            if (this.selTextInpt_Ware.Value != null)
            {
                if (this.selTextInpt_Ware.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    sql.Add("and WARECODE = ?", this.selTextInpt_Ware.Value.Trim());

                }
            }
            else
            {
                WJs.alert("请选择库房！");
                return;
            }


            //添加查询条件及其参数
            sql.Add("and TJDATE >= ?", this.dateTimePicker3.Value);

            sql.Add("and TJDATE <= ?", this.dateTimePicker4.Value);


            //加载查询数据
            this.dataGView1.Url = "FindWZTiaoJiaMainInfo";
            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sql);

            if (this.dataGView1.RowCount > 0)
            {
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {
                    if (this.dataGView1["Column1", i].Value != null)
                    {
                        string string1 = LData.Es("WZTiaoJiaDetailInfo1", "LKWZ", new object[] { this.dataGView1["Column1", i].Value, His.his.Choscode });
                        this.dataGView1["Column9", i].Value = string1;
                    }



                }
            }

            this.dataGView1.setFocus(0, 0);
            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔";
        }

        public void callback(string a)//子窗体成功保存后，调用改方法
        {


            this.ytComboBox_Status.Value ="1";

            //this.comboBox1.Value = a;

            this.selTextInpt_Ware.Value = a;
            this.selTextInpt_Ware.Text = LData.Es("LKWZ_GetWareName_ye", null, new object[] { His.his.Choscode, a });
            button2_Click(null, null);
        
            if (this.dataGView1.RowCount <= rowid)
            {
                this.dataGView1.setFocus(0, 0);

            }
            else
            {
                this.dataGView1.setFocus(rowid, 0);
            }

        }

        public void ReLoadData() //成功返回事件
        {

            //this.dataGView1.Url = "ScanWZTiaoJiaInfo";

            //this.dataGView1.reLoad(new object[] { His.his.Choscode,this.comboBox1.Value });
            button2_Click(null, null);
          
            if (this.dataGView1.RowCount <= rowid)
            {
                this.dataGView1.setFocus(0, 0);

            }
            else
            {
                this.dataGView1.setFocus(rowid, 0);
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e) //新增
        {
            DataRow r1 = this.dataGView1.GetRowData();


            WZTiaoJia_DetailScan ks = new WZTiaoJia_DetailScan(r1, true);
            ks.Main = this;
            ks.ShowDialog();
            //if (ks.isSc)
            //{
            //    ReLoadData();

            //}
        }

        private void toolStripButton5_Click(object sender, EventArgs e) //修改
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (r1["STATUS"].ToString() == "1" )
                {

                    rowid = this.dataGView1.CurrentRow.Index;
                    WZTiaoJia_DetailScan ksd = new WZTiaoJia_DetailScan(r1, false);
                    ksd.Main = this;
                    ksd.ShowDialog();
                    //if (ksd.isSc)
                    //{
                    //    ReLoadData();

                    //}
                }
                else
                {
                    WJs.alert("只能对状态为等待审核的信息进行修改");
                }

            }
            else
            {
                WJs.alert("请选择要修改的调价信息");
            }
        }
        void ac_ServiceDeleteLoad(object sender, YtClient.data.events.LoadEvent e) //提交成功返回
        {
            WJs.alert(e.Msg.Msg);
            //ReLoadData();
            button2_Click(null, null);
        }

        void ac_ServiceFaiLoad(object sender, YtClient.data.events.LoadFaiEvent e) //提交失败返回后触发
        {
            WJs.alert(e.Msg.Msg);
        }



        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)//成功返回事件
        {
            WJs.alert(e.Msg.Msg);
            ReLoadData();
           
        }


        private void toolStripButton3_Click(object sender, EventArgs e) //删除
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {

                if (WJs.confirmFb("确定要删除选择的调价信息吗？"))
                {
                    if (r1["STATUS"].ToString() == "1")
                    {
                      
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkwz.BusinessManag.WZTiaoJiaSvr";
                        ac.Sql = "DelWZTiaoJiaInfo";
                        ac.Add("TJID", r1["TJID"]);
                        ac.Add("STATUS", "0");
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceDeleteLoad);
                        ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
                        ac.Post();
                    }
                    else
                    {
                        WJs.alert("只能删除待审核的盘点信息！");
                    }
                }
            }
            else
            {
                WJs.alert("请选择要删除的调价信息");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e) //审核
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (r1["STATUS"].ToString() == "1")
                {
                    if (WJs.confirmFb("确定审核吗？"))
                    {
                        rowid = this.dataGView1.CurrentRow.Index;
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkwz.BusinessManag.WZTiaoJiaSvr";
                        ac.Sql = "ShenHeWZTiaoJiaInfo";
                        ac.Add("TJID", r1["TJID"]);
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.Add("STATUS", "2");
                        ac.Add("SHUSERID", His.his.UserId);
                        ac.Add("SHUSERNAME", His.his.UserName);
                        //ac.Add("SHDATE", DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();


                    }
                }
                else
                {
                    WJs.alert("状态错误，不能审核！");
                }
            }
            else
            {
                WJs.alert("请选择要审核的盘点信息");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e) //取消审核
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (r1["STATUS"].ToString() == "2")
                {
                    if (WJs.confirmFb("确定取消审核吗？"))
                    {
                        rowid = this.dataGView1.CurrentRow.Index;
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkwz.BusinessManag.WZTiaoJiaSvr";
                        ac.Sql = "CancelShenHeWZTiaoJianInfo";
                        ac.Add("TJID", r1["TJID"]);
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.Add("STATUS", "1");


                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();


                    }
                }
                else
                {
                    WJs.alert("状态错误，不能取消审核！");
                }
            }
            else
            {
                WJs.alert("请选择要取消审核的盘点信息");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)//确认
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (r1["STATUS"].ToString() == "2")
                {
                    if (WJs.confirmFb("是否确认？"))
                    {
                        rowid = this.dataGView1.CurrentRow.Index;
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkwz.BusinessManag.WZTiaoJiaSvr";
                        ac.Sql = "SHInWZPanDianInfo";
                        ac.Add("TJID", r1["TJID"]);
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.Add("STATUS", "6");
                        //ac.Add("JZDATE", DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
                        ac.Add("SHINUSERID", His.his.UserId);
                        ac.Add("SHINUSERNAME", His.his.UserName);



                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();


                    }
                }
                else
                {
                    WJs.alert("状态错误，不能确认！");
                }
            }
            else
            {
                WJs.alert("请选择要确认的调价信息");
            }
        }

      

        private void toolStripButton2_Click(object sender, EventArgs e)//查看
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 == null)
            {
                WJs.alert("请选择一行要查看的数据！");
                return;
            }
            WZTiaoJia_DetailScan ks = new WZTiaoJia_DetailScan(r1);
            ks.Main = this;
            ks.ShowDialog();
        }

        private void dataGView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
       

            toolStripButton2_Click(null, null);
       
        }

       

      


    }
}
