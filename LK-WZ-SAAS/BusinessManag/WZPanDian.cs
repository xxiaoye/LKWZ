using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using BusinessManag.form;
using ChSys;
using YtUtil.tool;
using YtClient;
using YiTian.db;
using YtWinContrl.com.datagrid;


namespace BusinessManag
{
    public partial class WZPanDian : Form,IPlug
    {
        public WZPanDian()
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

        int  rowid=0;
       
        private void toolStripButton8_Click(object sender, EventArgs e)  //工具栏新建按钮点击事件
        {
            DataRow r1 = this.dataGView1.GetRowData();


            WZPanDian_DetailScan ks = new WZPanDian_DetailScan(r1, true);
            ks.Main = this;
           
            ks.ShowDialog();
         
            
           
            //if (ks.isSc)
            //{
            //    toolStripButton4_Click(null, null);

            //}
            //Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
#region 注释代码
		            //DataRow r1 = this.dataGView1.GetRowData();
           
            ////{
            //WZPanDian_Add ks = new WZPanDian_Add();
            //    ks.ShowDialog();
            //    string MeMo = ks.MeMo.Trim();
            //    string PDWareNum = ks.PDWareNum.Trim();
            //    if (PDWareNum.Length > 0)
            //    {
                    
                
            // ActionLoad ac = ActionLoad.Conn();
            //if (r1 != null)
            //{
            //    ac.Add("PDID", r1["PDID"]);

            //}
            //else
            //{
            //    ac.Add("PDID", null);
            //}
           
            //ac.Action = "LKWZSVR.lkwz.BusinessManag.WZPanDianSvr";
            //ac.Sql = "SaveWZPanDicanInfo";

            //ac.Add("WARECODE",PDWareNum);

            //ac.Add("PDDATE", DateTime.Parse(DateTime.Now.Date.ToString("yyyy-MM-dd")));
           
            //ac.Add("MEMO",MeMo);
            //ac.Add("USERID",His.his.UserId);
            //ac.Add("USERNAME", His.his.UserName);
            //ac.Add("RECDATE", DateTime.Parse(DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss")));
            //ac.Add("CHOSCODE", His.his.Choscode);
            //ac.Add("STATUS", "1");

           
            ////ac.Add("SHDATE", DateTime.Parse(DateTime.Now.Date.ToString("yyyy-MM-dd")));
            ////ac.Add("SHUSERID", His.his.UserId);
            ////ac.Add("SHUSERNAME", His.his.UserName);
            ////ac.Add("JZDATE", DateTime.Parse(DateTime.Now.Date.ToString("yyyy-MM-dd")));
            ////ac.Add("JZUSERID", His.his.UserId);
            ////ac.Add("JZUSERNAME", His.his.UserName);
         
            
            //  ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_Add);
            //ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
            //ac.Post(); 

            //}
#endregion
        }


        public void toolStripButton4_Click(object sender, EventArgs e)  //工具栏浏览按钮点击事件（原来功能但现在被调用该方法）
        {

           
            this.dataGView2.ClearData();
            button2_Click(null, null);  //窗体查找按钮函数方法
           

            if (this.dataGView1.RowCount <= rowid)
            {
                this.dataGView1.setFocus(0, 0);

            }
            else
            {
                this.dataGView1.setFocus(rowid, 0);
            }
       
        }


        public void callback(string a)//子窗体成功保存后，调用改方法
        {


            //this.ytComboBox_Status.Value = a;
           
            this.selTextInpt_Ware.Value = a;
            this.selTextInpt_Ware.Text = LData.Es("LKWZ_GetWareName_ye", null, new object[]{His.his.Choscode,a});

            this.comboBox1.Value = "1";
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

       

        private void toolStripButton5_Click(object sender, EventArgs e) //工具栏修改按钮点击事件
        {
            DataRow r1 = this.dataGView1.GetRowData();
           
            if (r1!= null)
            {
                if (r1["STATUS"].ToString() == "1")
                {

                    rowid = this.dataGView1.CurrentRow.Index;
                    WZPanDian_DetailScan ks = new WZPanDian_DetailScan(r1, false);
                    ks.Main = this;
                    ks.ShowDialog();
                    //if (ks.isSc)
                    //{
                    //    toolStripButton4_Click(null, null);

                    //}
                }
                else 
                {
                    WJs.alert("只能对状态为等待审核的信息进行修改！");
                }
             
                
            }
            else
            {
                WJs.alert("请选择要编辑的盘点信息");
            }
        }

        void ac_ServiceLoad_ShenHe(object sender, YtClient.data.events.LoadEvent e)  //审核成功返回触发
        {
            WJs.alert(e.Msg.Msg);
            this.comboBox1.Value ="2";
            toolStripButton4_Click(null, null);
        }
        void ac_ServiceLoad_CancelShenHe(object sender, YtClient.data.events.LoadEvent e)  //取消审核成功返回触发
        {
            WJs.alert(e.Msg.Msg);
            this.comboBox1.Value = "1";
            toolStripButton4_Click(null, null);
        }
        void ac_ServiceLoad_JieZhuang(object sender, YtClient.data.events.LoadEvent e)  //结转成功返回触发
        {
            WJs.alert(e.Msg.Msg);
            this.comboBox1.Value = "6";
            toolStripButton4_Click(null, null);
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)  //保存成功返回触发
        {
            WJs.alert(e.Msg.Msg);
            toolStripButton4_Click(null, null);
        }
        void ac_ServiceDetailDeleteLoad(object sender, YtClient.data.events.LoadEvent e)//删除成功返回触发
        {
            WJs.alert(e.Msg.Msg);
             toolStripButton4_Click(null, null);
        
        }
        void ac_ServiceFaiLoad(object sender, YtClient.data.events.LoadFaiEvent e) //提交失败返回后触发
        {
            WJs.alert(e.Msg.Msg);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)//工具栏查看按钮点击事件
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 == null)

            {
                WJs.alert("请选择一行要查看的数据！");
                return;
            }
            WZPanDian_DetailScan ks = new WZPanDian_DetailScan(r1);
            ks.Main = this;
            ks.ShowDialog();
        }

        private void WZPanDian_Load(object sender, EventArgs e)//窗体加载事件
        {

            // toolStripButton4_Click(null, null);

  
            TvList.newBind().add("全部", "9").add("已结转", "6").add("已审核", "2").add("等待审核", "1").add("作废", "0").Bind(this.comboBox1);
            TvList.newBind().add("全部", "9").add("已结转", "6").add("已审核", "2").add("等待审核", "1").add("作废", "0").Bind(this.Column4);
            //TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(ytComboBox_Status);
           
            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.Column2);
            this.dateTimeDuan2.InitCorl();
            this.dateTimeDuan2.SelectedIndex = -1;

            this.dateTimeDuan2.SelectChange += new EventHandler(dateTimeDuan2_SelectChange);   
            this.dateTimePicker3.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker4.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            this.comboBox1.SelectedIndex = 0;
            //this.ytComboBox_Status.SelectedIndex = -1;
            //ytComboBox_Status.Focus();
            this.selTextInpt_Ware.Sql = "LKWZ_GetWare_ye";
            this.selTextInpt_Ware.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
           
        }

        void dateTimeDuan2_SelectChange(object sender, EventArgs e) //dateTimeDuan2选项发生改变时触发该事件
        {
            
            this.dateTimePicker3= this.dateTimeDuan2.Start;
            this.dateTimePicker4 = this.dateTimeDuan2.End;
            button2_Click(null, null);

        }

        
      

        private void button2_Click(object sender, EventArgs e)//查询按钮点击事件
        {
            this.dataGView1.ClearData();//1127
            this.dataGView2.ClearData();
            SqlStr sql = SqlStr.newSql();//创建SqlStr对象
            if (this.comboBox1.Value != null)
            {
                if (this.comboBox1.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    if (this.comboBox1.Value != "9")
                        sql.Add("and STATUS = ?", this.comboBox1.Value.Trim());

                }
            }
            if (this.selTextInpt_Ware.Value != null)
            {
                if (selTextInpt_Ware.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    sql.Add("and WARECODE = ?", selTextInpt_Ware.Value.Trim());

                }
            }
            else
            {
                WJs.alert("请选择库房！");
                return;
            }
          

            //添加查询条件及其参数
            sql.Add("and PDDATE <= ?", this.dateTimePicker4.Value);

            sql.Add("and PDDATE >= ?", this.dateTimePicker3.Value);


            //加载查询数据
            this.dataGView1.Url = "FindWZPandianMainInfo";
            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sql);
            if (this.dataGView1.RowCount > 0)
            {
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {
                    if (this.dataGView1["Column1", i].Value != null)
                    {
                        string string1 = LData.Es("WZPanDianDetailInfo1", "LKWZ", new object[] { this.dataGView1["Column1", i].Value, His.his.Choscode });
                        this.dataGView1["Column28", i].Value = string1;
                    }

                    

                }
            }


            this.dataGView1.setFocus(0, 0);

            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔"; 
            

        }

       

        private void dataGView1CellClick() //单击表格触发事件（原功能）
        {


            DataRow r1 = this.dataGView1.GetRowData();
            this.dataGView2.Url = "WZPanDianDetailInfo";
            this.dataGView2.reLoad(new object[] { r1["PDID"], His.his.Choscode });

        }

        

        private void toolStripButton1_Click(object sender, EventArgs e)//审核
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (r1["STATUS"].ToString()=="1")
                {
                    if (WJs.confirmFb("确定审核吗？"))
                    {
                        //rowid = this.dataGView1.CurrentRow.Index;
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkwz.BusinessManag.WZPanDianSvr";
                        ac.Sql = "ShenHeWZPanDianInfo";
                        ac.Add("PDID", r1["PDID"]);                    
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.Add("STATUS", "2");
                        ac.Add("SHUSERID", His.his.UserId);
                        ac.Add("SHUSERNAME", His.his.UserName);


                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_ShenHe);
                        ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
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

        private void toolStripButton6_Click(object sender, EventArgs e)//取消审核
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (r1["STATUS"].ToString() == "2")
                {
                    if (WJs.confirmFb("确定取消审核吗？"))
                    {
                        //rowid = this.dataGView1.CurrentRow.Index;
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkwz.BusinessManag.WZPanDianSvr";
                        ac.Sql = "CancelShenHeWZPanDianInfo";
                        ac.Add("PDID", r1["PDID"]);
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.Add("STATUS", "1");


                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_CancelShenHe);
                        ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
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
                WJs.alert("请选择要取消审核的盘点信息");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
             DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (r1["STATUS"].ToString() == "2")
                {
                    if (WJs.confirmFb("确定结转吗？"))
                    {
                        dataGView1CellClick();//保证打taGview2 中有数据
                        if (this.dataGView2.GetData() != null)
                        {

                            foreach (Dictionary<string, ObjItem> dr in this.dataGView2.GetData())
                            {
                                if (dr["库存流水号"].ToString().Trim().Length > 0)
                                {

                                    if (dr["采购单价"].IsNull || dr["采购单价"].ToString().Trim() == "")
                                    {
                                        WJs.alert("流水号为" + dr["库存流水号"] + "的采购单价为空！");
                                        return;
                                    }
                                    if (dr["零售单价"].IsNull || dr["零售单价"].ToString().Trim() == "")
                                    {
                                        WJs.alert("流水号为" + dr["库存流水号"] + "的零售单价为空！");
                                        return;
                                    }
                                }
                                else
                                {
                                    break;
                                }

                            }


                            //rowid = this.dataGView1.CurrentRow.Index;
                            ActionLoad ac = ActionLoad.Conn();
                            ac.Action = "LKWZSVR.lkwz.BusinessManag.WZPanDianSvr";
                            ac.Sql = "JieZhuangWZPanDianInfo";
                            ac.Add("PDID", r1["PDID"]);
                            ac.Add("CHOSCODE", His.his.Choscode);
                            ac.Add("STATUS", "6");
                            ac.Add("JZUSERID", His.his.UserId);
                            ac.Add("JZUSERNAME", His.his.UserName);


                            ac.Add("WARECODE", r1["WARECODE"].ToString());
                            ac.Add("MEMO", r1["MEMO"].ToString());
                            ac.Add("CHOSCODE", His.his.Choscode);
                            ac.Add("USERID", His.his.UserId);
                            ac.Add("USERNAME", His.his.UserName);

                            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_JieZhuang);
                            ac.Post();
                        }
                        else 
                        {
                            WJs.alert("盘点细表中没有数据，不能结转！");
                        }

                    }
                }
                else
                {
                    WJs.alert("状态错误，不能结转！");
                }
            }
            else
            {
                WJs.alert("请选择要结转的盘点信息");
            }
       
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (WJs.confirmFb("确定要删除选择的盘点信息吗？"))
                {
                    if (r1["STATUS"].ToString() == "1")
                    {

                        rowid = this.dataGView1.CurrentRow.Index;
                       
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkwz.BusinessManag.WZPanDianSvr";
                        ac.Sql = "DelWZPanDianInfo";
                        ac.Add("PDID", r1["PDID"]);
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                    }
                    else
                    {
                        WJs.alert("状态错误，只能删除状态为等待审核的数据！");
                    }
                }
            }
            else
            {
                WJs.alert("请选择要删除的盘点信息");
            }
        }

        private void dataGView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)//双击表格触发事件
        {
           
       
            toolStripButton3_Click(null, null);//查看
        }


       

    }
}
