using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using YtClient;
using ChSys;
using YtUtil.tool;
using YtWinContrl.com.datagrid;
//using JiChuDict.form;

namespace BusinessManag
{
    public partial class WZShenLing : Form, IPlug
    {
        private form.ShenLingEdit editForm = null;
        private int xBRowIndex=-1;
        private int xBColumnIndex = -1;
        private int IsEdit = 0;
        
        private DataTable WzWare;
        private DataTable Wz;
        private DataTable Unit;
        private DataTable StockDetail;
        private DataGridViewRow editRow;
        private DataTable InOutKind;

        private int IsUserChange = 0;

        public WZShenLing()
        {
            InitializeComponent();
            TvList.newBind().add("作废", "0").add("等待审核", "1").add("已审核", "2").add("已发放","5").add( "已调拨","6").Bind(this.comboBox2);
            this.selTextInpt1.SelParam = His.his.Choscode + "|{key}|{key}|{key}"; ;
            //this.selTextInpt1.OneRowAutoSelect = true;
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 1;
            this.dataGView1.ReadOnly = false;
            this.dataGView1.AllowUserToAddRows = false;//true;
            this.groupBox2.Visible = false;
            this.toolStripButton7.Enabled = false;
            this.dtbegin.Value = DateTime.Now.AddMonths(-1);
            //this.dtend.
            this.dtend.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);


        }
        #region IPlug 成员
        IAppContent app;
        public Form getMainForm()
        {
            return this;
        }
        private void init()
        {

        }
        public void initPlug(IAppContent app, object[] param)
        {
            this.app = app;
            if (param != null && param.Length > 0)
            {
                // ty = (param[0].ToString());
            }
        }

        public bool unLoad()
        {
            return true;
        }

        #endregion
        private void WZShenLing_Load(object sender, EventArgs e)
        {
           
            TvList.newBind().add("作废", "0").add("等待审核", "1").add("已审核", "2").add("已发放","5").add("已确认入库", "6").Bind(this.Column9);
            TvList.newBind().add("否", "0").add("是", "1").Bind(this.Column2);
            ActionLoad ld1 = ActionLoad.Conn();  //库房绑定
            ld1.Action = "Find";
            ld1.Sql = "DbFindYeWuWare";
            ld1.SetParams(new object[] { His.his.Choscode });
            ld1.ServiceLoad += new YtClient.data.events.LoadEventHandle(ld1_ServiceLoad);
            ld1.Post();

            UnitBind();

       
            this.dataGView1.IsPage = false;
            this.dataGView1.Url = "SlFindPlanMainList";


            this.dataGView1.AllowUserToAddRows = false;

            //for (int i = 0; i < this.dataGView1.Rows.Count; i++)
            //{
            //    this.dataGView1.Rows[i].Visible = true;
            //}
            //for (int i = 0; i < this.dataGView1.Columns.Count; i++)
            //{
            //    this.dataGView1.Columns[i].Visible = true;
            //}
            this.dataGView1.ReadOnly = true;
            //for (int i = 0; i < this.dataGView2.Columns.Count; i++)
            //{
            //    this.dataGView2.Columns[i].Visible = true;
            //    this.dataGView2.Columns[i].ReadOnly = true;
            //}
            //this.dataGView2.Columns[this.dataGView2.Columns.Count - 1].Visible = false;


            this.dataGView2.IsPage = true;
            this.dataGView2.Url = "SlFindPlanDetailList";

            this.groupBox2.Size = new Size(this.Size.Width, this.dataGView1.Size.Height - 120);


            this.toolStripButton6.Enabled = true;
            this.toolStripButton5.Enabled = true;
            this.toolStripButton7.Enabled = false;

            this.toolStripButton9.Enabled = true;
            this.toolStripButton8.Enabled = true;
            this.toolStripButton4.Enabled = true;
            this.toolStripButton3.Enabled = true;
            this.toolStripButton2.Enabled = true;
            this.toolStripButton1.Enabled = false;
            button1.Enabled = true;
        }
        public void reLoad()  //根据查询条件，重新加载结果
        {
            if (this.selTextInpt1.Value == null || this.selTextInpt1.Value.Trim() == "")
            {
                WJs.alert("请选择一个库房");
                return;
            }
            SqlStr sqlc = SqlStr.newSql();

            if (this.comboBox1.SelectedIndex == 0)
            {
                sqlc.Add(" and WARECODE=" + this.selTextInpt1.Value.Trim());
            }
            else
            {
                sqlc.Add(" and TARGETWARECODE=" + this.selTextInpt1.Value.Trim());
            }
           // this.dataGView1.reLoad(new object[] { His.his.Choscode }, sqlc);


            this.dataGView1.reLoad(new object[] { dtbegin.Value, dtend.Value, ((YtWinContrl.com.datagrid.TextValue)this.comboBox2.SelectedItem).Value }, sqlc);
            if (this.dataGView1.RowCount > 0)
            {
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {
                    if (this.dataGView1["Column1", i].Value != null)
                    {
                        string string1 = LData.Es("SlMixiBishu", "LKWZ", new object[] { this.dataGView1["Column1", i].Value, His.his.Choscode });
                        this.dataGView1["mxbs", i].Value = string1;
                    }



                }
            }
            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔";
            this.JinEHeJi.Text = this.dataGView1.Sum("总金额").ToString() + "元";
            this.RuKuJinEHeJi.Text = this.dataGView1.Sum("零售总金额").ToString() + "元";

        }


        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)  //弹出服务端返回的信息
        {
            WJs.alert(e.Msg.Msg);
        }
        //void ld_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)  //出库方式绑定
        //{
        //    InOutKind = e.Msg.GetDataTable();
        //    TvList tv = TvList.newBind();
        //    ((DataGridViewComboBoxColumn)this.Column2).Items.Clear();
        //    if (InOutKind != null)
        //    {
        //        foreach (DataRow r in InOutKind.Rows)
        //        {
        //            tv.add(r[1].ToString(), r[0].ToString());

        //        }
        //    }
        //    tv.Bind(this.Column2);
        //}

        void UnitBind()  //细表的单位绑定
        {
            ActionLoad ld3 = ActionLoad.Conn();
            ld3.Action = "Find";
            ld3.Sql = "DbFindWzUnit";
            ld3.SetParams(new object[] { });
            ld3.ServiceLoad += new YtClient.data.events.LoadEventHandle(ld3_ServiceLoad);
            ld3.Post();
        }
        void StockDetailBind(Object WARECODE)//根据库房ID，绑定细表库存流水号
        {
            if (WARECODE==null)
                return;
            ActionLoad ld4 = ActionLoad.Conn();
            ld4.Action = "Find";
            ld4.Sql = "DbFindStockDetail";
            ld4.SetParams(new object[] { WARECODE });
            ld4.ServiceLoad += new YtClient.data.events.LoadEventHandle(ld4_ServiceLoad);
            ld4.Post();
        }

        void ld1_ServiceLoad(object sender, YtClient.data.events.LoadEvent e) //物资库房绑定
        {
            WzWare = e.Msg.GetDataTable();

            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.Column4).Items.Clear();
            ((DataGridViewComboBoxColumn)this.Column5).Items.Clear();
            if (WzWare != null)
            {
                foreach (DataRow r in WzWare.Rows)
                {
                    tv.add(r[0].ToString(), r[1].ToString());
                }
            }
            tv.Bind(this.Column4);
            tv.Bind(this.Column5);
        }
        void ld2_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)  //根据出库库房ID，绑定物资
        {
            Wz = e.Msg.GetDataTable();
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.Column27).Items.Clear();
            if (Wz != null)
            {
                foreach (DataRow r in Wz.Rows)
                {
                    tv.add(r[1].ToString(), r[0].ToString());
                }
            }
            tv.Bind(this.Column27);
        }
        void ld3_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            Unit = e.Msg.GetDataTable();
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.Column29).Items.Clear();
            if (Unit != null)
            {
                foreach (DataRow r in Unit.Rows)
                {
                    tv.add(r[1].ToString(), r[0].ToString());
                }
            }
            tv.Bind(Column29);

        }
        void ld4_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            StockDetail = e.Msg.GetDataTable();
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.Column37).Items.Clear();
            if (StockDetail != null)
            {
                foreach (DataRow r in StockDetail.Rows)
                {
                    tv.add(r[1].ToString(), r[0].ToString());
                }
            }
            tv.Bind(this.Column37);
            //if (xBRowIndex != -1)
            //    tv.Bind((DataGridViewComboBoxCell)this.dataGView2.Rows[xBRowIndex].Cells["Column37"]);
        }

        void reBindStock(object WzId)  //根据物资ID，重新绑定库存流水号列
        {
            if (Wz == null || StockDetail == null || WzId==null)
            {
                return;
            }
            DataRow[] r1 = Wz.Select("WZID=" + WzId.ToString());
            DataRow[] r = StockDetail.Select("WZID=" + WzId.ToString());
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxCell)this.dataGView2.Rows[xBRowIndex].Cells["Column37"]).Items.Clear();
            for (int i = 0; i < r.Length; i++)
            {
                tv.add(r[i]["FLOWNO"].ToString(), r[i]["FLOWNO"].ToString());
                
            }
            tv.Bind((DataGridViewComboBoxCell)this.dataGView2.Rows[xBRowIndex].Cells["Column37"]);
            if (r.Length > 0)
            {
                if (this.dataGView2.Rows[xBRowIndex].Cells["Column37"].Value == null)
                {
                    this.dataGView2.Rows[xBRowIndex].Cells["Column37"].Value = r[0]["FLOWNO"].ToString();
                    updateStockRelateInfo(r[0]["FLOWNO"], xBRowIndex);
                }
               
            }
            
        }
        void reBindUnit(Object WzId)  //根据物资ID，从新绑定细表的单位列
        {
            if (Wz == null || Unit == null  || WzId==null)
            {
                return;
            }
            DataRow[] r1 = Wz.Select("WZID=" + WzId.ToString());
            DataRow[] r = Unit.Select("DICID='" + r1[0][11].ToString() + "' or DICID='" + r1[0][13].ToString() + "'");
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxCell)this.dataGView2.Rows[xBRowIndex].Cells["Column29"]).Items.Clear();
            for (int i = 0; i < r.Length; i++)
            {
                tv.add(r[i][1].ToString(), r[i][0].ToString());
            }
            tv.Bind((DataGridViewComboBoxCell)this.dataGView2.Rows[xBRowIndex].Cells["Column29"]);
            if (r.Length > 0 && this.dataGView2.Rows[xBRowIndex].Cells["Column29"].Value==null)
                this.dataGView2.Rows[xBRowIndex].Cells["Column29"].Value = r[0][0].ToString();
            this.dataGView2.Rows[xBRowIndex].Cells["Column29"].Tag = r1[0][11].ToString(); //Column29.Tag代表最小单位，Column29.ToolTipText代表换算率
            this.dataGView2.Rows[xBRowIndex].Cells["Column29"].ToolTipText = r1[0][14].ToString();
  

        }
        void reBindXbWz(object WareId)   //根据库房ID，重新绑定细表物资列
        {
            if (WareId == null)
                return;
            ActionLoad ld2 = ActionLoad.Conn();
            ld2.Action = "Find";
            ld2.Sql = "DbFindMyWz";
            ld2.SetParams(new object[] { WareId });
            ld2.ServiceLoad += new YtClient.data.events.LoadEventHandle(ld2_ServiceLoad);
            ld2.Post();
        }

        private void button1_Click(object sender, EventArgs e) //查询按钮，根据查询条件重新加载数据
        {
            this.reLoad();
        }

        private void dataGView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//主表--查看详细按钮--的功能
        {

        }
        private void reLoadDetail(object PlanID,object WareID)  //根据选择的主表ID，重新加载细表内容
        {
            this.reBindXbWz(WareID);
            StockDetailBind(WareID);
            this.dataGView2.reLoad(new object[] { PlanID.ToString() });

        }

        private void toolStripButton6_Click(object sender, EventArgs e)   //新增
        {
            this.editForm = new form.ShenLingEdit(null,2,app);
            this.editForm.ShowDialog();
            //IsEdit = 1;//1代表处于新增模式
            //reBindEveryRow();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)  //取消
        {
            if (IsEdit == 1)
            {
                this.dataGView1.Rows.Remove(editRow);
            }
            this.dataGView1.AllowUserToAddRows = false;

            for (int i = 0; i < this.dataGView1.Rows.Count; i++)
            {
                this.dataGView1.Rows[i].Visible = true;
            }
            for (int i = 0; i < this.dataGView1.Columns.Count; i++)
            {
                this.dataGView1.Columns[i].Visible =true;
            }
            this.dataGView1.ReadOnly = true;

            for (int i = 0; i < this.dataGView2.Columns.Count; i++)
            {
                this.dataGView2.Columns[i].Visible = true;
            }
            
            this.dataGView2.ReadOnly = true;
            this.groupBox2.Visible = false;
            this.toolStripButton6.Enabled = true;
            this.toolStripButton5.Enabled = true;
            this.toolStripButton7.Enabled = false;

            this.toolStripButton9.Enabled = true;
            this.toolStripButton8.Enabled = true;
            this.toolStripButton4.Enabled = true;
            this.toolStripButton3.Enabled = true;
            this.toolStripButton2.Enabled = true;
            this.toolStripButton1.Enabled = false;
            button1.Enabled = true;


            //this.dtend.Value = DateTime.Now;

            IsUserChange = 0;
            IsEdit = 0;
            this.reLoad();

        }

        private void toolStripButton7_Click(object sender, EventArgs e)  //保存
        {
            if (CheckInputInfo())
            {
                SavePlanMainRow();
                toolStripButton1_Click(null, null);
            }

        }
        private bool CheckInputInfo()
        {
            String errMsg=null;
            if (editRow.Cells["Column4"].Value == null)
            {
                errMsg += "主表：请选择采购库房！\n";
                //WJs.alert("请选择出库库房！");
                //return;
            }
            if (editRow.Cells["Column5"].Value == null)
            {
                errMsg += "主表：请选择申领目的库房！\n";
                //WJs.alert("请选择目的库房！");
                //return;
            }
            for (int i = 0; i < dataGView2.Rows.Count-1; i++)
            {
                if (dataGView2.Rows[i].Cells["Column27"].Value == null)
                {
                    errMsg += "细表:第"+(i+1).ToString()+"项，请选择物质种类！\n";
                }
                if (dataGView2.Rows[i].Cells["Column28"].Value == null)
                {
                    errMsg += "细表:第" + (i + 1).ToString() + "项，请填写物质数量！\n";
                }
                if (dataGView2.Rows[i].Cells["Column29"].Value == null)
                {
                    errMsg += "细表:第" + (i + 1).ToString() + "项，请选择物质单位！\n";
                }
                if (dataGView2.Rows[i].Cells["Column37"].Value == null)
                {
                    errMsg += "细表:第" + (i + 1).ToString() + "项，请选择某个入库流水号！\n";
                }
            }
            if (errMsg != null)
            {
                WJs.alert(errMsg);
                return false;
            }
            else
                return true;
        }
        private string getSerializationXb()
        {
            string xbNeiRong = "";
            for (int i = 0; i < this.dataGView2.Rows.Count-1; i++)
            {
                DataGridViewCellCollection r = this.dataGView2.Rows[i].Cells;
                if (r != null)
                {
                    if (r["Column45"].Value == null || r["Column45"].Value.ToString() != "Delete")
                    {
                        //if (r["Column25"].Value != null)
                        //{
                        //    xbNeiRong += "1|";
                        //    xbNeiRong += r["Column25"].Value.ToString() + "|";
                        //}
                        //else
                        //{
                        //    xbNeiRong += "2|";
                        //}
                        xbNeiRong += r["Column26"].Value.ToString() + "|";
                        xbNeiRong += r["Column27"].Value.ToString() + "|";
                        xbNeiRong += r["Column28"].Value.ToString() + "|";
                        xbNeiRong += r["Column29"].Value.ToString() + "|";
                        xbNeiRong += r["Column30"].Value.ToString() + "|";
                        xbNeiRong += r["Column31"].Value.ToString() + "|";
                        xbNeiRong += r["Column32"].Value.ToString() + "|";
                        xbNeiRong += r["Column33"].Value.ToString() + "|";
                        xbNeiRong += r["Column34"].Value.ToString() + "|";
                        xbNeiRong += r["Column35"].Value.ToString() + "|";
                        if (r["Column36"].Value == null)
                            xbNeiRong += " |";
                        else
                        xbNeiRong += r["Column36"].Value.ToString() + "|";
                        xbNeiRong += r["Column37"].Value.ToString() + "|";
                        xbNeiRong += r["Column38"].Value.ToString() + "|";
                        xbNeiRong += r["Column39"].Value.ToString() + "~";

                    }
                    //else if (r["Column25"].Value != null)
                    //{
                    //    xbNeiRong += "3|";
                    //    xbNeiRong += xbNeiRong += r["Column25"].Value.ToString() + "~";
                    //}
                }
            }
            if (xbNeiRong != "")
            {
                xbNeiRong = xbNeiRong.Remove(xbNeiRong.Length - 1);
            }

                return xbNeiRong;
        }
        private void SavePlanMainRow()
        {
            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkwz.WZShenLing.SaveShenLing";
            if (IsEdit == 1)
            {
                ac.Sql = "AddPlanMain";
                ac.Add("PLANID", null);
                ac.Add("PLANDATE", null);//幅度段更改
            }
            else
            {
                ac.Sql = "UpdatePlanMain";
                ac.Add("PLANID", Convert.ToInt32(editRow.Cells["Column1"].Value.ToString()));
                ac.Add("PLANDATE", Convert.ToDateTime(editRow.Cells["Column8"].Value.ToString()));//幅度段更改
            }

            
            //ac.Add("IOID", Convert.ToInt32(editRow.Cells["Column2"].Value.ToString()));//字符串
            //ac.Add("RECIPECODE", editRow.Cells["Column3"].Value.ToString());  //服务端更改 //字符串
            ac.Add("WARECODE", editRow.Cells["Column4"].Value.ToString());  //字符串
            ac.Add("TARGETWARECODE", editRow.Cells["Column5"].Value.ToString()); //字符串
            ac.Add("TOTALMONEY", editRow.Cells["Column6"].Value);//数字
            ac.Add("LSTOTALMONEY", editRow.Cells["Column7"].Value); //数字
            ac.Add("STATUS", 1);
            ac.Add("IFPLAN", 0);
            if (editRow.Cells["Column10"].Value==null)
            ac.Add("MEMO", "");
            else
            ac.Add("MEMO",editRow.Cells["Column10"].Value.ToString());
            ac.Add("USERID", His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("RECDATE", DateTime.Now);  //服务端更改
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("XiBiaoData",getSerializationXb());//WJs.ListToXML(this.dataGView2.GetData()));//this.dataGView2.GetData());//);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac5_ServiceLoad);
            ac.Post();
           
        }
        void ac5_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);

        }
        private void reBindEveryRow()
        {

            this.toolStripButton6.Enabled = false;
            this.toolStripButton5.Enabled = false;
            this.toolStripButton7.Enabled = true;
            this.toolStripButton9.Enabled = false;
            this.toolStripButton8.Enabled = false;
            this.toolStripButton4.Enabled = false;
            this.toolStripButton3.Enabled = false;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton1.Enabled = true;
            button1.Enabled = false;
//Datagrid1对应关系
//采购计划ID     PLANID         Column1
//采购库房       WARECODE       Column4
//申领目的库房   TARGETWARECODE Column5
//总金额         TOTALMONEY     Column6
//零售总金额     LSTOTALMONEY   Column7
//制定日期       PLANDATE       Column8
//状态           STATUS         Column9
//是否采购计划   IFPLAN         Column2
//备注           MEMO           Column10
//操作员ID       USERID         Column12
//操作员名称     USERNAME       Column13
//修改日期       RECDATE        Column14
//审核日期       SHDATE         Column15
//审核操作员ID   SHUSERID       Column16
//审核操作员名称 SHUSERNAME     Column17
//医疗机构编码   CHOSCODE       Column21
//入库日期       SHINDATE       Column22
//入库操作员ID   SHINUSERID     Column23
//入库操作员名称 SHINUSERNAME   Column3
//出库日期       SHOUTDATE      Column18
//出库操作员ID   SHOUTUSERID    Column19
//出库操作员名称 SHOUTUSERNAME  Column20
                Dictionary<string, YiTian.db.ObjItem> rData = this.dataGView1.getRowData();
                this.dataGView1.DataSource = null;
                this.dataGView1.ClearData();

                editRow = this.dataGView1.Rows[this.dataGView1.Rows.Add()];
            if (IsEdit==2)
            {
                editRow.Cells["Column1"].Value = rData["采购计划ID"];
                editRow.Cells["Column2"].Value = rData["是否采购计划"].ToString();
                editRow.Cells["Column3"].Value = rData["入库操作员名称"];
                editRow.Cells["Column4"].Value = rData["采购库房"].ToString(); ;
                editRow.Cells["Column5"].Value = rData["申领目的库房"].ToString();
                editRow.Cells["Column6"].Value = rData["总金额"];
                editRow.Cells["Column7"].Value = rData["零售总金额"];
                editRow.Cells["Column8"].Value = rData["制定日期"];
                editRow.Cells["Column9"].Value = rData["状态"].ToString();
                editRow.Cells["Column10"].Value = rData["备注"];
                //editRow.Cells["Column11"].Value = rData["操作标志"].ToString();
                editRow.Cells["Column12"].Value = rData["操作员ID"];
                editRow.Cells["Column13"].Value = rData["操作员名称"];
                editRow.Cells["Column14"].Value = rData["修改日期"];
                editRow.Cells["Column15"].Value = rData["审核日期"];
                editRow.Cells["Column16"].Value = rData["审核操作员ID"];
                editRow.Cells["Column17"].Value = rData["审核操作员名称"];
                editRow.Cells["Column18"].Value = rData["出库日期"];
                editRow.Cells["Column19"].Value = rData["出库操作员ID"];
                editRow.Cells["Column20"].Value = rData["出库操作员名称"];
                editRow.Cells["Column21"].Value = rData["医疗机构编码"];
                editRow.Cells["Column22"].Value = rData["入库日期"];
                editRow.Cells["Column23"].Value = rData["入库操作员ID"];
                this.dataGView2.reLoad(new object[] { editRow.Cells["Column1"].Value.ToString() });
                
            }


            List<Dictionary<string, YiTian.db.ObjItem>> dataTable = this.dataGView2.GetData();
            
//Datagrid2对应关系    
//采购计划ID      PLANID       Column25
//行号            ROWNO        Column26
//物质            WZID         Column27
//数量            NUM          Column28
//单位            UNITCODE     Column29
//出库单价        PRICE        Column30
//出库金额        MONEY        Column31
//零售单价        LSPRICE      Column32
//零售金额        LSMONEY      Column33
//生产商          SCS          Column34
//供应商          GYS          Column35
//备注            MEMO         Column36
//条形码          TXM          Column38
//医疗机构编码    CHOSCODE     Column39
//库存流水号      STOCKFLOWNO  Column37


            this.dataGView2.DataSource = null;
            this.dataGView2.Rows.Clear();

            if(IsEdit==2)
            {
                foreach (Dictionary<string, YiTian.db.ObjItem> r in dataTable)
                {
                    if (r != null)
                    {
                        int index = this.dataGView2.Rows.Add();
                        this.dataGView2.Rows[index].Cells["Column25"].Value = r["采购计划ID"];
                        this.dataGView2.Rows[index].Cells["Column26"].Value = r["行号"];
                        this.dataGView2.Rows[index].Cells["Column27"].Value = r["物质"].ToString();
                        this.dataGView2.Rows[index].Cells["Column37"].Value = r["库存流水号"].ToString();

                        this.dataGView2.Rows[index].Cells["Column30"].Value = r["出库单价"];
                        this.dataGView2.Rows[index].Cells["Column31"].Value = r["出库金额"];
                        this.dataGView2.Rows[index].Cells["Column32"].Value = r["零售单价"];
                        this.dataGView2.Rows[index].Cells["Column33"].Value = r["零售金额"];

                        this.dataGView2.Rows[index].Cells["Column28"].Value = r["数量"];
                        this.dataGView2.Rows[index].Cells["Column29"].Value = r["单位"].ToString();
                        this.dataGView2.Rows[index].Cells["Column34"].Value = r["生产商"];
                        this.dataGView2.Rows[index].Cells["Column35"].Value = r["供应商"];
                        this.dataGView2.Rows[index].Cells["Column36"].Value = r["备注"];
                        
                        this.dataGView2.Rows[index].Cells["Column38"].Value = r["条形码"];
                        this.dataGView2.Rows[index].Cells["Column39"].Value = r["医疗机构编码"];
                        //this.dataGView2.Rows[index].Cells["Column40"].Value = r["生产厂家ID"];
                        //this.dataGView2.Rows[index].Cells["Column41"].Value = r["生产厂家名称"];
                        //this.dataGView2.Rows[index].Cells["Column42"].Value = r["生产日期"];
                        //this.dataGView2.Rows[index].Cells["Column43"].Value = r["有效期"];
                        //this.dataGView2.Rows[index].Cells["Column44"].Value = r["卫生许可证号"];
                        this.dataGView2.Rows[index].Cells["Column28"].ReadOnly = false;
                        this.dataGView2.Rows[index].Cells["Column29"].ReadOnly = false;
                        this.dataGView2.Rows[index].Cells["Column37"].ReadOnly = false;
                    }

                }
            }
            

            for (int i = 0; i < this.dataGView1.Columns.Count; i++)
            {
                this.dataGView1.Columns[i].Visible = false;
            }
            this.dataGView1.ReadOnly = false;
            this.dataGView1.Columns["Column4"].Visible = true;
            this.dataGView1.Columns["Column5"].Visible = true;
            this.dataGView1.Columns["Column6"].Visible = true;
            this.dataGView1.Columns["Column7"].Visible = true;
            this.dataGView1.Columns["Column6"].ReadOnly = true;
            this.dataGView1.Columns["Column7"].ReadOnly = true;

            this.groupBox2.Visible = true;
            this.dataGView2.ReadOnly = false;
            this.dataGView2.Columns["Column27"].ReadOnly = false;
            this.dataGView2.Columns["Column30"].ReadOnly = true;
            this.dataGView2.Columns["Column32"].ReadOnly = true;
            this.dataGView2.Columns["Column31"].ReadOnly = true;
            this.dataGView2.Columns["Column33"].ReadOnly = true;

            this.dataGView2.Columns["Column25"].Visible = false;
            this.dataGView2.Columns["Column34"].Visible = false;
            this.dataGView2.Columns["Column35"].Visible = false;
            this.dataGView2.Columns["Column38"].Visible = false;
            this.dataGView2.Columns["Column39"].Visible = false;
            this.dataGView2.Columns["Column45"].Visible = true;
            //for (int i = 0; i < this.dataGView2.Columns.Count; i++)
            //{
            //    this.dataGView2.Columns[i].Visible = false;
            //}
            //this.dataGView2.Columns[2].Visible = true;
            //this.dataGView2.Columns[3].Visible = true;
            //this.dataGView2.Columns[4].Visible = true;
            //this.dataGView2.Columns[5].Visible = true;
            //this.dataGView2.Columns[6].Visible = true;
            //this.dataGView2.Columns[7].Visible = true;
            //this.dataGView2.Columns[8].Visible = true;
            //this.dataGView2.Columns[9].Visible = true;
            //this.dataGView2.Columns[12].Visible = true;
            //this.dataGView2.Columns["Column45"].Visible = true;

        }
        private void toolStripButton5_Click(object sender, EventArgs e)  //编辑
        {

            if (this.dataGView1.CurrentRow==null)
            {
                WJs.alert("请选择要编辑的行");
                return;
            }
            if (this.dataGView1.GetRowData()["STATUS"].ToString() != "1")
            {
                WJs.alert("只能对处于等待审核状态的数据进行编辑，请选择等待审核的行！");
                return;
            }
            Dictionary<string, string> info = new Dictionary<string, string>() { };
            info.Add("MuDiKuFangCode", this.dataGView1.CurrentRow.Cells["Column5"].Value.ToString());
            info.Add("MuDiKuFangName", ((DataGridViewComboBoxCell)this.dataGView1.CurrentRow.Cells["Column5"]).FormattedValue.ToString());
            info.Add("ChuKuKuFangCode", this.dataGView1.CurrentRow.Cells["Column4"].Value.ToString());
            info.Add("ChuKuKuFangName", ((DataGridViewComboBoxCell)this.dataGView1.CurrentRow.Cells["Column4"]).FormattedValue.ToString());
            //info.Add("ChuKuFangShiCode", this.dataGView1.CurrentRow.Cells["Column2"].Value.ToString());
            //info.Add("ChuKuFangShiName", ((DataGridViewComboBoxCell)this.dataGView1.CurrentRow.Cells["Column2"]).FormattedValue.ToString());
            info.Add("BeiZhu", this.dataGView1.CurrentRow.Cells["Column10"].Value.ToString());
            info.Add("ChuKuZongJinE", this.dataGView1.CurrentRow.Cells["Column6"].Value.ToString());
            info.Add("RuKuZongJinE", this.dataGView1.CurrentRow.Cells["Column7"].Value.ToString());
            //info.Add("DanJuHao", this.dataGView1.CurrentRow.Cells["Column3"].Value.ToString());
            info.Add("outID", this.dataGView1.CurrentRow.Cells["Column1"].Value.ToString());
            info.Add("outDate", this.dataGView1.CurrentRow.Cells["Column8"].Value.ToString());
            this.editForm = new form.ShenLingEdit(info,1,app);
            this.editForm.ShowDialog();
            //if (this.dataGView1.SelectedRows == null || this.dataGView1.SelectedRows.Count < 1)
            //{
            //    WJs.alert("请选择要编辑的行");
            //    return;
            //}
            //if (this.dataGView1.GetRowData()["STATUS"].ToString() != "1")
            //{
            //    WJs.alert("只能对处于等待审核的数据进行编辑，请选择等待审核的行！");
            //    return;
            //}

            //IsEdit = 2;//2代表处于编辑模式
            //reBindEveryRow();

        }

        #region //当改变主表里面的某些单元格时，更改关联单元格的绑定
        void reBindMdWare(object CKWareId)
        {

            if (CKWareId == null)
            {
                return;
            }
            DataRow[] r = WzWare.Select("WARECODE='" + this.dataGView1.Rows[xBRowIndex].Cells["Column4"].Value.ToString() + "'");
            r = WzWare.Select("IFSTWARE=" + r[0]["IFNDWARE"].ToString() + " and IFNDWARE=" + r[0]["IFRDWARE"].ToString()+" and IFRDWARE=0");
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxCell)this.dataGView1.Rows[xBRowIndex].Cells["Column5"]).Items.Clear();
            for (int i = 0; i < r.Length; i++)
            {
                tv.add(r[i][0].ToString(), r[i][1].ToString());
            }
            if (xBRowIndex != -1)
            {
                tv.Bind((DataGridViewComboBoxCell)this.dataGView1.Rows[xBRowIndex].Cells["Column5"]);
                this.dataGView1.Rows[xBRowIndex].Cells["Column5"].Value = null;

            }

        }
        void reBindOpflag(int rowIndex)
        {
            
            DataRow[] r = InOutKind.Select("IOID=" + this.dataGView1.Rows[rowIndex].Cells["Column2"].Value);
            this.dataGView1.Rows[rowIndex].Cells["Column11"].Value = r[0]["OPFLAG"];
            string recipe = r[0]["RECIPECODE"].ToString();
            int rL = Convert.ToInt32(r[0]["RECIPELENGTH"]);
            if (r[0]["RECIPEYEAR"].ToString() == "1")
            {
                recipe += DateTime.Now.Year.ToString("D4");
            }
            if (r[0]["RECIPEMONTH"].ToString() == "1")
            {
                recipe += DateTime.Now.Month.ToString("D2");
            }
            rL=rL-recipe.Length;
            recipe = "D" + rL.ToString()+"_" + recipe;
            this.dataGView1.Rows[rowIndex].Cells["Column3"].Value = recipe;
            
        }
        private void deleteAllDetail() //改变出库库房时，删除细表数据
        {
            for (int i = 0; i < this.dataGView2.Rows.Count-1; i++)
            {
                this.dataGView2.Rows[i].Cells["Column28"].Value = 0;
                this.dataGView2.Rows[i].Cells["Column45"].Value = "Delete";
                this.dataGView2.Rows[i].Visible = false;
            }


        }
        private void dataGView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (IsEdit==0)
                return;
            
            xBRowIndex = e.RowIndex;
            switch (e.ColumnIndex)
            {
                case 2:  //当出库库房改变时，重新绑定目的库房，并清空细表
                    reBindMdWare(this.dataGView1.Rows[e.RowIndex].Cells["Column4"].Value);
                    break;
                case 3:
                    reBindXbWz(this.dataGView1.Rows[e.RowIndex].Cells["Column5"].Value);
                    StockDetailBind(this.dataGView1.Rows[e.RowIndex].Cells["Column5"].Value);
                    if (IsUserChange == 1)
                    {
                        deleteAllDetail();
                    }
                    break;
            }
        }

        private void dataGView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (IsEdit==0)
                return;

            this.dataGView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        #endregion


        #region 细表的某些cell改变时，更改关联Cell的绑定

        private void updateMoney(int rowIndex)
        {
            double money = 0.0;
            
            if (this.dataGView2.Rows[rowIndex].Cells["Column29"].Value == null || this.dataGView2.Rows[rowIndex].Cells["Column28"].Value == null)
            {
                return;
            }
            if (this.dataGView2.Rows[rowIndex].Cells["Column29"].Value.ToString() == this.dataGView2.Rows[rowIndex].Cells["Column29"].Tag.ToString())
            {
                money = Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column29"].ToolTipText) * Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column28"].Value.ToString()) * Convert.ToDouble(this.dataGView2.Rows[rowIndex].Cells["Column30"].Value.ToString());
                this.dataGView2.Rows[rowIndex].Cells["Column31"].Value = money.ToString("f4");
                money = Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column29"].ToolTipText) * Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column28"].Value.ToString()) * Convert.ToDouble(this.dataGView2.Rows[rowIndex].Cells["Column32"].Value.ToString());
                this.dataGView2.Rows[rowIndex].Cells["Column33"].Value = money.ToString("f4");
            }
            else
            {
                money =  Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column28"].Value.ToString()) * Convert.ToDouble(this.dataGView2.Rows[rowIndex].Cells["Column30"].Value.ToString());
                this.dataGView2.Rows[rowIndex].Cells["Column31"].Value = money.ToString("f4");
                money =  Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column28"].Value.ToString()) * Convert.ToDouble(this.dataGView2.Rows[rowIndex].Cells["Column32"].Value.ToString());
                this.dataGView2.Rows[rowIndex].Cells["Column33"].Value = money.ToString("f4");
            }
            
            editRow.Cells["Column6"].Value = this.dataGView2.Sum("出库金额");
            editRow.Cells["Column7"].Value = this.dataGView2.Sum("零售金额");
        
        }
        private void updateStockRelateInfo(object stockId,int rowIndex)
        {
            DataRow[] r = StockDetail.Select("FLOWNO='" + stockId.ToString()+"'");
            if (r == null || r.Length == 0)
            {
                return;
            }
            this.dataGView2.Rows[rowIndex].Cells["Column28"].Tag = (Convert.ToInt32(r[0]["NUM"]) - Convert.ToInt32(r[0]["OUTNUM"]));
            this.dataGView2.Rows[rowIndex].Cells["Column30"].Value = r[0]["PRICE"];
            this.dataGView2.Rows[rowIndex].Cells["Column32"].Value = r[0]["LSPRICE"]; 
            this.dataGView2.Rows[rowIndex].Cells["Column38"].Value = r[0]["TXM"];
            this.dataGView2.Rows[rowIndex].Cells["Column39"].Value = His.his.Choscode;
            this.dataGView2.Rows[rowIndex].Cells["Column34"].Value = r[0]["SUPPLYNAME"];
            this.dataGView2.Rows[rowIndex].Cells["Column35"].Value = r[0]["SUPPLYNAME2"];

        }
        public void checkIfOverFlow(int rowIndex)
        {
            if (this.dataGView2.Rows[rowIndex].Cells["Column28"].Value == null)
            {
                return;
            }
            int num = 0;
            if (WJs.IsZs(this.dataGView2.Rows[rowIndex].Cells["Column28"].Value.ToString()))
            {
                if (this.dataGView2.Rows[rowIndex].Cells["Column29"].Value.ToString() == this.dataGView2.Rows[rowIndex].Cells["Column29"].Tag.ToString())
                {
                    num = Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column29"].ToolTipText) * Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column28"].Value.ToString());

                    if (num > Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column28"].Tag.ToString()))
                    {
                        WJs.alert("超出此流水的剩余物资的最大数！最大数量是" + this.dataGView2.Rows[rowIndex].Cells["Column28"].Tag.ToString() + "(最小单位)");
                        this.dataGView2.Rows[rowIndex].Cells["Column28"].Value = (Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column28"].Tag) / Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column29"].ToolTipText)).ToString();

                    }
                }
                else
                {
                    num = Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column28"].Value.ToString());
                    if (num > Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column28"].Tag.ToString()))
                    {
                        WJs.alert("超出此流水的剩余物资的最大数！最大数量是" + this.dataGView2.Rows[rowIndex].Cells["Column28"].Tag.ToString() + "(最小单位)");
                        this.dataGView2.Rows[rowIndex].Cells["Column28"].Value = this.dataGView2.Rows[rowIndex].Cells["Column28"].Tag;

                    }
                }

            }
            else
            {
                WJs.alert("数量需输入整数！");
                this.dataGView2.Rows[rowIndex].Cells["Column28"].Value = 0;
            }
        }
        //public void checkIfOverFlow(int rowIndex)
        //{
        //    if (this.dataGView2.Rows[rowIndex].Cells["Column28"].Value == null)
        //    {
        //        return;
        //    }
        //    if (WJs.IsZs(this.dataGView2.Rows[rowIndex].Cells["Column28"].Value.ToString()))
        //    {
        //        if (Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column28"].Value.ToString()) > Convert.ToInt32(this.dataGView2.Rows[rowIndex].Cells["Column28"].Tag.ToString()))
        //        {
        //            this.dataGView2.Rows[rowIndex].Cells["Column28"].Value = this.dataGView2.Rows[rowIndex].Cells["Column28"].Tag;
        //        }
        //    }
        //    else
        //    {
        //        WJs.alert("数量需输入整数！");
        //    }
        //}
        private void updateHangHao()
        {
            if (this.groupBox2.Visible == true)
            {
                int k = 1;
                for (int i = 0; i < this.dataGView2.Rows.Count - 1; i++)
                {
                    if (this.dataGView2.Rows[i].Visible == true)
                    {
                        this.dataGView2.Rows[i].Cells["Column26"].Value = k;
                        k++;
                    }
                }
            }
        }
        private void dataGView2_CellValueChanged(object sender, DataGridViewCellEventArgs e) 
        {
            if (IsEdit == 0 )
                return;
            xBRowIndex = e.RowIndex;
            updateHangHao();

            if (this.dataGView2.Rows[e.RowIndex].Cells["Column27"].ColumnIndex == e.ColumnIndex)
            {
                
                this.dataGView2.Rows[e.RowIndex].Cells["Column28"].ReadOnly = false;
                this.dataGView2.Rows[e.RowIndex].Cells["Column29"].ReadOnly = false;
                this.dataGView2.Rows[e.RowIndex].Cells["Column37"].ReadOnly = false;
                this.dataGView2.Rows[e.RowIndex].Cells["Column36"].ReadOnly = true; ;
               
                reBindStock(this.dataGView2.Rows[e.RowIndex].Cells["Column27"].Value);
                reBindUnit(this.dataGView2.Rows[e.RowIndex].Cells["Column27"].Value);

                this.dataGView2.Rows[e.RowIndex].Cells["Column28"].Value = "0";
                
            }
            else if (this.dataGView2.Rows[e.RowIndex].Cells["Column28"].ColumnIndex == e.ColumnIndex) //当出库数量变化时，检查时候超出限定，并更新金额
            {
                checkIfOverFlow(e.RowIndex);
                if (this.dataGView2.Rows[e.RowIndex].Cells["Column28"].Value != null && WJs.IsZs(this.dataGView2.Rows[e.RowIndex].Cells["Column28"].Value.ToString()))
                updateMoney(e.RowIndex);
            }
            else if (this.dataGView2.Rows[e.RowIndex].Cells["Column29"].ColumnIndex == e.ColumnIndex) //当单位变化时，更新金额
            {

              //  if (this.dataGView2.Rows[e.RowIndex].Cells["Column28"].Value!=null && WJs.IsZs(this.dataGView2.Rows[e.RowIndex].Cells["Column28"].Value.ToString()))
                    updateMoney(e.RowIndex);
              //  else
               //     WJs.alert("请输入整数个数量");
            }
            else if (this.dataGView2.Rows[e.RowIndex].Cells["Column37"].ColumnIndex == e.ColumnIndex)
            {
                updateStockRelateInfo(this.dataGView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, e.RowIndex);
            }

        }

        private void dataGView2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (IsEdit==0)
                return;
            this.dataGView2.CommitEdit(DataGridViewDataErrorContexts.Commit);

        }
        #endregion

        private void dataGView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGView2.Columns[e.ColumnIndex].Name=="Column45" && this.dataGView2.Rows[e.RowIndex].IsNewRow==false)
            {
                this.dataGView2.Rows[e.RowIndex].Cells["Column28"].Value = 0;
                this.dataGView2.Rows[e.RowIndex].Visible = false;
                this.dataGView2.Rows[e.RowIndex].Cells["Column45"].Value = "Delete";
                

                
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)  //审核
        {
            //if (this.dataGView1.SelectedRows == null || this.dataGView1.SelectedRows.Count < 1)
            if (this.dataGView1.CurrentRow == null)
            {
                WJs.alert("请选择要取消审核的行");
                return;
            }
            if (this.dataGView1.GetRowData()["STATUS"].ToString() != "1")
            {
                WJs.alert("请选择等待审核的行！");
                return;
            }
            this.comboBox2.SelectedIndex = 2;
            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkwz.WZShenLing.SaveShenLing";
            ac.Sql = "ChangeShState";
            ac.Add("SHDATE", DateTime.Now);
            ac.Add("SHUSERID", His.his.UserId);
            ac.Add("SHUSERNAME", His.his.UserName);
            ac.Add("PLANID", this.dataGView1.GetRowData()["PLANID"]);
            ac.Add("STATUS", 2);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(Reload_ServiceLoad);
            ac.Post();
            this.groupBox2.Visible = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)  //取消审核
        {
            //if (this.dataGView1.SelectedRows == null || this.dataGView1.SelectedRows.Count < 1)
            if (this.dataGView1.CurrentRow == null)
            {
                WJs.alert("请选择要取消审核的行");
                return;
            }
            if (this.dataGView1.GetRowData()["STATUS"].ToString() != "2")
            {
                WJs.alert("请选择已审核的行！");
                return;
            }
            this.comboBox2.SelectedIndex = 1;
            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkwz.WZShenLing.SaveShenLing";

            ac.Sql = "ChangeShState";
            ac.Add("SHDATE", DateTime.Now);
            ac.Add("SHUSERID", His.his.UserId);
            ac.Add("SHUSERNAME", His.his.UserName);
            ac.Add("PLANID", this.dataGView1.GetRowData()["PLANID"]);
            ac.Add("STATUS", 1);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(Reload_ServiceLoad);
            ac.Post();
            this.groupBox2.Visible = false;

        }
        private void toolStripButton9_Click(object sender, EventArgs e)  //发放
        {
            //if (this.dataGView1.SelectedRows == null || this.dataGView1.SelectedRows.Count < 1)
            if (this.dataGView1.CurrentRow == null)
            {
                WJs.alert("请选择要发放的行");
                return;
            }
            if (this.dataGView1.GetRowData()["STATUS"].ToString() != "2")
            {
                WJs.alert("请选择已审核的行！");
                return;
            }
            this.comboBox2.SelectedIndex = 3;
            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkwz.WZShenLing.SaveShenLing";
            ac.Sql = "ChangeOutState";
            ac.Add("SHOUTDATE", DateTime.Now);
            ac.Add("SHOUTUSERID", His.his.UserId);
            ac.Add("SHOUTUSERNAME", His.his.UserName);
            ac.Add("PLANID", this.dataGView1.GetRowData()["PLANID"]);
            ac.Add("STATUS", 5);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(Reload_ServiceLoad);
            ac.Post();
            this.groupBox2.Visible = false;
        }
        private void toolStripButton2_Click(object sender, EventArgs e) //确认入库
        {
            //if (this.dataGView1.SelectedRows == null || this.dataGView1.SelectedRows.Count < 1)
            if (this.dataGView1.CurrentRow == null)
            {
                WJs.alert("请选择要确认入库的行");
                return;
            }
            if (this.dataGView1.GetRowData()["STATUS"].ToString() != "5")
            {
                WJs.alert("请选择已发放的行！");
                return;
            }
            this.comboBox2.SelectedIndex = 4;
            ActionLoad ac = ActionLoad.Conn();


            ac.Action = "LKWZSVR.lkwz.WZShenLing.SaveShenLing";
            ac.Sql = "QueRenRuKu";
            ac.Add("SHINDATE", DateTime.Now);
            ac.Add("SHINUSERID", His.his.UserId);
            ac.Add("SHINUSERNAME", His.his.UserName);
            ac.Add("PLANID", this.dataGView1.GetRowData()["PLANID"]);
            ac.Add("STATUS", 6);
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(Reload_ServiceLoad);
            ac.Post();
            this.groupBox2.Visible = false;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)  //删除
        {
            //if (this.dataGView1.SelectedRows == null || this.dataGView1.SelectedRows.Count < 1)
            if (this.dataGView1.CurrentRow == null)
            {
                WJs.alert("请选择要删除的行");
                return;
            }
            if (this.dataGView1.GetRowData()["STATUS"].ToString() != "1")
            {
                WJs.alert("请选择等待审核的行！");
                return;
            }
            this.comboBox2.SelectedIndex = 0;
            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkwz.WZShenLing.SaveShenLing";
            ac.Sql = "ChangeShState";
            ac.Add("SHDATE", DateTime.Now);
            ac.Add("SHUSERID", His.his.UserId);
            ac.Add("SHUSERNAME", His.his.UserName);
            ac.Add("PLANID", this.dataGView1.GetRowData()["PLANID"]);
            ac.Add("STATUS", 0);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(Reload_ServiceLoad);
            ac.Post();
            this.groupBox2.Visible = false;
        }
        void Reload_ServiceLoad(object sender, YtClient.data.events.LoadEvent e) //物资库房绑定
        {

            WJs.alert(e.Msg.Msg);
            this.reLoad();
        }

        private void dataGView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton5_Click(null, null);
        }

        private void scan_toolStripButton_Click(object sender, EventArgs e)
        {
            if (this.dataGView1.CurrentRow == null)
            {
                WJs.alert("请选择要浏览的行");
                return;
            }
           
            Dictionary<string, string> info = new Dictionary<string, string>() { };
            info.Add("MuDiKuFangCode", this.dataGView1.CurrentRow.Cells["Column5"].Value.ToString());
            info.Add("MuDiKuFangName", ((DataGridViewComboBoxCell)this.dataGView1.CurrentRow.Cells["Column5"]).FormattedValue.ToString());
            info.Add("ChuKuKuFangCode", this.dataGView1.CurrentRow.Cells["Column4"].Value.ToString());
            info.Add("ChuKuKuFangName", ((DataGridViewComboBoxCell)this.dataGView1.CurrentRow.Cells["Column4"]).FormattedValue.ToString());
            //info.Add("ChuKuFangShiCode", this.dataGView1.CurrentRow.Cells["Column2"].Value.ToString());
            //info.Add("ChuKuFangShiName", ((DataGridViewComboBoxCell)this.dataGView1.CurrentRow.Cells["Column2"]).FormattedValue.ToString());
            info.Add("BeiZhu", this.dataGView1.CurrentRow.Cells["Column10"].Value.ToString());
            info.Add("ChuKuZongJinE", this.dataGView1.CurrentRow.Cells["Column6"].Value.ToString());
            info.Add("RuKuZongJinE", this.dataGView1.CurrentRow.Cells["Column7"].Value.ToString());
            //info.Add("DanJuHao", this.dataGView1.CurrentRow.Cells["Column3"].Value.ToString());
            info.Add("outID", this.dataGView1.CurrentRow.Cells["Column1"].Value.ToString());
            info.Add("outDate", this.dataGView1.CurrentRow.Cells["Column8"].Value.ToString());
            this.editForm = new form.ShenLingEdit(info,0,app);
            this.editForm.ShowDialog();
        }

        



    }
}
