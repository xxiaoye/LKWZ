using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YiTian.db;
using ChSys;
using YtWinContrl.com.datagrid;
using YtClient;
using YtWinContrl.com;
using YtUtil.tool;
using YtPlugin;

namespace BusinessManag.form2
{
    public partial class PlanForm : Form
    {
        Dictionary<string, ObjItem> dr;
        private int isAdd = 0;
        private string inware_id;
        private string inware_name;
        IAppContent app;
        public PlanForm(string inware_id, string inware_name,IAppContent app)
        {
            this.app = app;
            this.isAdd = 1;
            this.inware_id = inware_id;
            this.inware_name = inware_name;
            InitializeComponent();
        }
        public PlanForm(Dictionary<string, ObjItem> dr, int isAdd, string inware_id, string inware_name, IAppContent app)
        {
            this.app = app;
            this.isAdd = isAdd;
            this.dr = dr;
            this.inware_id = inware_id;
            this.inware_name = inware_name;
            InitializeComponent();
        }
        private TvList dwList;
        private void PlanForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.InWare_selTextInpt.Text = inware_name;
            this.InWare_selTextInpt.Value = inware_id;
            this.InWare_selTextInpt.Enabled = false;
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.lsunitcode);

            this.dataGView2.Url = "WZPlanDetailSearch";
            dwList = TvList.newBind().SetCacheKey("XmDw").Load("GetWZUnit2", new object[] { His.his.Choscode });
            dwList.Bind(this.unit);
            if (isAdd != 1)
            {
                this.totalmoney_yTextBox.Text = dr["总金额"].ToString();
                this.lstotalmoney_yTextBox.Text = dr["零售总金额"].ToString();
                if (!dr["制定日期"].IsNull)
                {
                    this.dateTimePicker1.Value = dr["制定日期"].ToDateTime();
                }
                this.memo_yTextBox.Text = dr["备注"].ToString();
                this.dataGView2.Columns[wz.Index].ReadOnly = true;
                this.dataGView2.reLoad(new object[] { dr["采购计划id"].ToString(), His.his.Choscode });
                this.cancel_toolStripButton.Enabled = false;
                this.gain_toolStripButton.Enabled = false;
                this.getNUmXx_toolStripButton.Enabled = false;
                if (isAdd == 3)
                {
                   // this.toolStrip1.Enabled = false;
                    for (int i = 0; i < this.toolStrip1.Items.Count; i++)
                    {
                        this.toolStrip1.Items[i].Enabled = false;
                    }
                    this.toolStripButton1.Enabled = true;

                    this.dataGView2.ReadOnly = true;
                    this.dateTimePicker1.Enabled = false;
                    this.memo_yTextBox.ReadOnly = true;
                }
                this.dataGView2.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
                BindUnit();
            }
            dataGView2.RowToXml += new RowToXmlHandle(dataGView1_RowToXml);
        }

        void dataGView1_RowToXml(RowToXmlEvent e)
        {
            if (e.Data["物资ID"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【物资】！");
                this.dataGView2.setFocus(e.Row.Index-1, "物资");
                return;
            }
            if (e.Data["单位"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行没有单位，请到物资字典中对【" + e.Data["物资"].ToString() + "】加入单位！");
                this.dataGView2.setFocus(e.Row.Index, "物资");
                return;
            }
            if (!WJs.IsZs(e.Data["采购数量"].ToString()) || e.Data["采购数量"].ToDouble() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【采购数量】只能输入整数，并且必须大于0！");
                this.dataGView2.setFocus(e.Row.Index, "数量");
                return;
            }
            if (!WJs.MaxNumOver(e.Data["采购数量"].ToString(), "第" + (e.Row.Index + 1) + "行【采购数量】"))
            {
                e.IsValid = false;
                this.dataGView2.setFocus(e.Row.Index, "采购数量");
                return;
            }
            if (!WJs.IsNum(e.Data["采购单价"].ToString()) || e.Data["采购单价"].ToFloat() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【采购单价】只能输入数字，并且必须大于0！");
                this.dataGView2.setFocus(e.Row.Index, "采购单价");
                return;
            }
            if (!WJs.IsNum(e.Data["零售单价"].ToString()) || e.Data["零售单价"].ToFloat() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【零售单价】只能输入数字，并且必须大于0！");
                this.dataGView2.setFocus(e.Row.Index, "零售单价");
                return;
            }
            if (!e.Data["备注"].IsNull && e.Data["备注"].ToString().Length > 50)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行输入的【备注】最多只允许50个字符！");
                this.dataGView2.setFocus(e.Row.Index, "备注");
                return;
            }
        }


        private void add_toolStripButton_Click(object sender, EventArgs e)
        {
            this.KeyPreview = true;
           
            Dictionary<string, object> de = new Dictionary<string, object>();
          
            de["采购数量"] = 0;
            de["采购单价"] = "0.0000";
            de["零售单价"] = "0.0000";
            de["采购金额"] = "0.0000";
            de["零售金额"] = "0.0000";
            string wareid = this.InWare_selTextInpt.Value;
            string ifall = LData.Es("WZWareIfall", "LKWZ", new object[] { wareid });
           
            if (ifall.Equals("1"))
            {
              
                dataGView2.addSql("GetInWZ0", "物资", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");
               
            }
            else
            {
             
                dataGView2.addSql("GetInWZ", "物资", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");

            }
            this.dataGView2.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
            this.dataGView2.AddRow(de, 0);
            this.dataGView2.CurrentRow.Cells[wz.Index].ReadOnly = false;
            this.dataGView2.CurrentRow.Cells[unit.Index].ReadOnly = false;
           
        }
        void BindUnit()
        {
            foreach (DataGridViewRow r in this.dataGView2.Rows)
            {
                Dictionary<string, ObjItem> data = this.dataGView2.getRowData(r);
                string dw;
                string wzdw="";
                if (data != null)
                {
                    if (isAdd != 1)
                    {
                        wzdw = data["单位编码0"].ToString();
                    }
                    //wzdw = data["单位编码0"].ToString();
                    dw = data["单位编码"].ToString();
                    DataGridViewComboBoxCell d = r.Cells[unit.Index] as DataGridViewComboBoxCell;
                    dwList.setFilter(data["最小单位编码"].ToString(), dw,wzdw).Bind(d);
                    string t = "1" + dwList.GetText(data["单位"].ToString()) + "=" + data["换算系数"].ToString() + dwList.GetText(data["最小单位编码"].ToString());
                    r.Cells[bzxs.Index].Value = t;
                }
            }
        }
        void dataGView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGView2.ReadOnly) return;
            Dictionary<string, ObjItem> data = this.dataGView2.getRowData(this.dataGView2.Rows[e.RowIndex]);
            string unit_o;
            if (e.ColumnIndex == txm.Index || e.ColumnIndex == unit.Index)
            {
                if (e.ColumnIndex == txm.Index)
                {
                    BindUnit();
                    this.dataGView2.CurrentRow.Cells[unit.Index].Value = data["单位编码"].ToString();
                    this.dataGView2.CurrentRow.Cells[price.Index].Value = data["采购单价0"].ToString();
                    unit_o = data["单位编码"].ToString();
                }
                else
                {
                    unit_o = this.dataGView2.CurrentRow.Cells[unit.Index].Value.ToString();
                    this.dataGView2.CurrentRow.Cells[unitcode.Index].Value = data["单位"].ToString();
                }

                string lsunitcode_o = data["最小单位编码"].ToString();

                
                //判断该库房价格的计价体系是否统一浮动
                string ifall_s = LData.Es("PriceIfAll", "LKWZ", new object[] { His.his.Choscode, this.InWare_selTextInpt.Value });
                this.dataGView2.CurrentRow.Cells[price.Index].Value = data["采购单价0"].ToDecimal();
                //获取计价体系的加价率
                string priceid_s = LData.Es("Ware_GetPriceID", "LKWZ", new object[] { His.his.Choscode, this.InWare_selTextInpt.Value });
                string rate_s = LData.Es("GetPriceRate", "LKWZ", new object[] { priceid_s });
                if (ifall_s.Equals("1"))
                {
                    //该库房的计价体系是否统一浮动
                    
                   
                    this.dataGView2.CurrentRow.Cells[lsprice.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() * (1 + Convert.ToDecimal(rate_s))) + "", 4);


                }
                else
                {
                    //根据采购单价*（1+各个物资字典里的加价率）获得零售单价
                    this.dataGView2.CurrentRow.Cells[lsprice.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() * (1 + data["加价率"].ToDecimal())) + "", 4);
                }
                if (unit_o.Equals(lsunitcode_o))
                {
                    int i = data["换算系数"].ToInt();
                    // decimal r = data["采购单价0"].ToDecimal();
                    //decimal l = data["零售单价"].ToDecimal();
                    if (ifall_s.Equals("1"))
                    {
                        //该库房的计价体系是否统一浮动
                        //获取计价体系的加价率
                        //string priceid_s = LData.Es("Ware_GetPriceID", null, new object[] { His.his.Choscode, this.InWare_selTextInpt.Value });
                       // string rate_s = LData.Es("GetPriceRate", null, new object[] { priceid_s });
                        this.dataGView2.CurrentRow.Cells[lsprice.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() * (1 + Convert.ToDecimal(rate_s)) / i) + "", 4);
                    }
                    else
                    {
                        //根据采购单价*（1+各个物资字典里的加价率）获得零售单价
                        this.dataGView2.CurrentRow.Cells[lsprice.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() * (1 + data["加价率"].ToDecimal()) / i) + "", 4);
                    }
                    this.dataGView2.CurrentRow.Cells[price.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() / i) + "", 4);
                    // this.dataGView1.CurrentRow.Cells[lsprice.Index].Value = MathUtil.round((data["零售单价"].ToDecimal() / i) + "", 4);
                }


            }

            if ((e.ColumnIndex == unit.Index || e.ColumnIndex == num.Index || e.ColumnIndex == price.Index || e.ColumnIndex == lsprice.Index) && e.RowIndex > -1)
            {
                try
                {

                    this.dataGView2.jsBds("零售金额=采购数量*零售单价");
                    this.dataGView2.jsBds("采购金额=采购数量*采购单价");
                    ////统计金额
                    this.totalmoney_yTextBox.Text = dataGView2.Sum("采购金额").ToString("0.0000");
                    this.lstotalmoney_yTextBox.Text = dataGView2.Sum("零售金额").ToString("0.0000");

                }
                catch
                {

                }

            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            this.Close();
        }
        void ac2_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            // this.Close();
        }
        private void save_toolStripButton_Click(object sender, EventArgs e)
        {
            if (this.dataGView2.RowCount == 0)
            {
                WJs.alert("请添加采购物资");
                return;
            }
            if (this.totalmoney_yTextBox.Text.Trim().Length>11)
            {
                WJs.alert("总金额太大(不能超过100000.0000)！请减少该批次物资");
                return;
            }
            if (this.lstotalmoney_yTextBox.Text.Trim().Length > 11)
            {
                WJs.alert("零售金额太大(不能超过100000.0000)！请减少该批次物资");
                return;
            }
            string str = this.dataGView2.GetDataToXml();
            if (str != null)
            {
                ActionLoad ac = ActionLoad.Conn();
                ac.Action = "LKWZSVR.lkwz.WZPlan.WZPlanDan";
                ac.Sql = "PlanDanSave";
                
                ac.Add("WARECODE", this.InWare_selTextInpt.Value);
                ac.Add("PLANDATE", DateTime.Now);
              
                ac.Add("MEMO", this.memo_yTextBox.Text);
               
                ac.Add("CHOSCODE", His.his.Choscode);
                ac.Add("USERID", His.his.UserId);
                ac.Add("USERNAME", His.his.UserName);
                ac.Add("STATUS", "1");
                ac.Add("IFPLAN", 1);
               
                ac.Add("TOTALMONEY", this.totalmoney_yTextBox.Text);
                ac.Add("LSTOTALMONEY", this.lstotalmoney_yTextBox.Text);
                ac.Add("DanJuMx", str);
                if (isAdd == 2)
                {
                    ac.Add("PLANID", dr["采购计划id"].ToString());
                    //ac.Add("ROWNO", this.rowno_yTextBox.Text);
                   // ac.SetKeyValue("PLANID,ROWNO");
                }
                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);

                ac.Post();

            }
        }

        private void cancel_toolStripButton_Click(object sender, EventArgs e)
        {
            if (WJs.confirm("是否放弃保存，暂存数据将清空！"))
            {
                InitInWare();
                // this.Close();
            }
        }
        void InitInWare()
        {
            this.dataGView2.Rows.Clear();
            this.memo_yTextBox.Text = "";
            this.totalmoney_yTextBox.Text = "0.0000";
            this.lstotalmoney_yTextBox.Text = "0.0000";

        }

        private void DeleButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> doc = this.dataGView2.getRowData();
            if (doc != null)
            {
                if (doc["物资"].IsNull)
                {
                    this.dataGView2.Rows.Remove(this.dataGView2.CurrentRow);
                }
                else
                {
                    if (WJs.confirmFb("您确定要删除选择的采购物资信息吗？"))
                    {

                        this.dataGView2.Rows.Remove(this.dataGView2.CurrentRow);
                        this.dataGView2.jsBds("零售金额=采购数量*零售单价");
                        this.dataGView2.jsBds("采购金额=采购数量*采购单价");
                        ////统计金额
                        this.totalmoney_yTextBox.Text = dataGView2.Sum("采购金额").ToString("0.0000");
                        this.lstotalmoney_yTextBox.Text = dataGView2.Sum("零售金额").ToString("0.0000");
                        if (!doc["行号"].IsNull && !doc["行号"].ToString().Equals(""))
                        {
                            //数据库中已经存在该记录，需要删除数据库中的记录

                            ActionLoad ac = new ActionLoad();
                            ac.Action = "LKWZSVR.lkwz.WZPlan.WZPlanDan";
                            ac.Sql = "PlanDanWZdelete";
                            ac.Add("ROWNO", doc["行号"].ToString());
                            ac.Add("PLANID", doc["采购计划ID"].ToString());
                            ac.Add("TOTALMONEY", this.totalmoney_yTextBox.Text);
                            ac.Add("LSTOTALMONEY", this.lstotalmoney_yTextBox.Text);
                            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac2_ServiceLoad);
                            ac.Post();


                        }
                    }
                }

            }
            else
            {
                WJs.alert("请选择需要删除的行！");
            }
        }

        private void gain_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView2.getRowData();
            if (dr == null)
            {
                //add_toolStripButton_Click(null, null);
                //this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                this.dataGView2.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
                this.dataGView2.Columns[wz.Index].ReadOnly = true;
                this.cancel_toolStripButton.Enabled = false;
                //从本库房的历史采购计划中获取
                //DataTable dt = LData.LoadDataTable("GetHistroyPlan", new object[] { His.his.Choscode });
                this.dataGView2.Url = "GetHistroyPlan";
                this.dataGView2.reLoad(new object[] { His.his.Choscode });
                BindUnit();
                this.dataGView2.jsBds("零售金额=采购数量*零售单价");
                this.dataGView2.jsBds("采购金额=采购数量*采购单价");
                ////统计金额
                this.totalmoney_yTextBox.Text = dataGView2.Sum("采购金额").ToString("0.0000");
                this.lstotalmoney_yTextBox.Text = dataGView2.Sum("零售金额").ToString("0.0000");

            }
            else
            {
                WJs.alert("该采购计划已经有明细数据，不能从历史数据中复制明细信息！");
            }
        }

        private void getNUmXx_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView2.getRowData();
            if (dr == null)
            {
                //add_toolStripButton_Click(null, null);
                //this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                ////获取本库房中物资数量少于最低库存的记录
                ////DataTable dt = LData.LoadDataTable("GetHistroyPlan", new object[] { His.his.Choscode });
                this.dataGView2.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
                this.dataGView2.Columns[wz.Index].ReadOnly = true;
                this.dataGView2.Columns[unit.Index].ReadOnly = true;
                this.dataGView2.Url = "GetNumXxPlan";
                this.dataGView2.reLoad(new object[] { His.his.Choscode, this.inware_id });
                //BindUnit();
                foreach (DataGridViewRow r in this.dataGView2.Rows)
                {
                    Dictionary<string, ObjItem> data = this.dataGView2.getRowData(r);
                    string dw;
                    string wzdw = "";
                    if (data != null)
                    {
                        
                        wzdw = data["单位编码0"].ToString();
                        dw = data["单位编码"].ToString();
                        DataGridViewComboBoxCell d = r.Cells[unit.Index] as DataGridViewComboBoxCell;
                        dwList.setFilter(data["最小单位编码"].ToString(), dw, wzdw).Bind(d);
                        //r.Cells[lsprice.Index] = "";
                        int i = data["换算系数"].ToInt();
                        // decimal r = data["采购单价0"].ToDecimal();
                        //decimal l = data["零售单价"].ToDecimal();
                        string ifall_s = LData.Es("PriceIfAll", "LKWZ", new object[] { His.his.Choscode, this.InWare_selTextInpt.Value });
                        string priceid_s = LData.Es("Ware_GetPriceID", "LKWZ", new object[] { His.his.Choscode, this.InWare_selTextInpt.Value });
                        string rate_s = LData.Es("GetPriceRate", "LKWZ", new object[] { priceid_s });
                        if (ifall_s.Equals("1"))
                        {
                            //该库房的计价体系是否统一浮动
                            //获取计价体系的加价率
                            //string priceid_s = LData.Es("Ware_GetPriceID", null, new object[] { His.his.Choscode, this.InWare_selTextInpt.Value });
                            // string rate_s = LData.Es("GetPriceRate", null, new object[] { priceid_s });
                            r.Cells[lsprice.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() * (1 + Convert.ToDecimal(rate_s)) / i) + "", 4);
                        }
                        else
                        {
                            //根据采购单价*（1+各个物资字典里的加价率）获得零售单价
                            r.Cells[lsprice.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() * (1 + data["加价率"].ToDecimal()) / i) + "", 4);
                        }
                        r.Cells[price.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() / i) + "", 4);


                    }
                }
                if (this.dataGView2.RowCount == 0)
                {
                    WJs.alert("该库房物资充足，不能根据最低库存量生成采购计划单");
                    return;
                }
                else
                {
                    this.add_toolStripButton.Enabled = false;
                    this.cancel_toolStripButton.Enabled = false;
                    this.gain_toolStripButton.Enabled = false;
                    this.getNUmXx_toolStripButton.Enabled = false;
                }
                //this.dataGView1.jsBds(
                this.dataGView2.jsBds("零售金额=采购数量*零售单价", true);
                this.dataGView2.jsBds("采购金额=采购数量*采购单价", true);
                ////统计金额
                this.totalmoney_yTextBox.Text = dataGView2.Sum("采购金额").ToString("0.0000");
                this.lstotalmoney_yTextBox.Text = dataGView2.Sum("零售金额").ToString("0.0000");

            }
            else
            {
                WJs.alert("该采购计划已经有明细数据，不能从根据最低库存生成！");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (isAdd != 1)
            {
                ActionLoad ld = ActionLoad.Conn();
                DataTable tb = ld.Find("PlanMainDaYinQuery", new object[] { dr["采购计划id"].ToString() });
                // string KuFangName = LData.Exe("GetInWareName", null, new object[] { His.his.Choscode, dr["入库库房编码"].ToString() });
                if (tb != null && tb.Rows.Count > 0)
                {
                    // setDefalutStock(tb.Rows[0], r);

                    Dictionary<string, object> pp = new Dictionary<string, object>();
                    pp.Add("BiaoTi", tb.Rows[0][0].ToString() + "  【" + inware_name + "】  " + "采购单");
                    pp.Add("FangXiang", "采购库房  【" + inware_name + "】");
                    pp.Add("Time", "时间：" + tb.Rows[0][5].ToString());
                    pp.Add("LeiBie", "");
                    pp.Add("BianHao", "类别：采购单");
                    //pp.Add("HeJi", "合计");
                    pp.Add("JinE1", tb.Rows[0][9].ToString());
                    pp.Add("JinE2", tb.Rows[0][10].ToString());
                    pp.Add("Name1", "操作员：" + tb.Rows[0][1].ToString());
                    pp.Add("Name2", "审核员：" + tb.Rows[0][2].ToString());
                    pp.Add("Name3", "出库员：" + tb.Rows[0][3].ToString());
                    pp.Add("Name4", "入库员：" + tb.Rows[0][11].ToString());
                    pp.Add("HeJi", "备注：" + tb.Rows[0]["MEMO"].ToString());
                    pp.Add("ID", dr["采购计划id"].ToString());
                    app.LoadPlug("RepEdit.RepView", new object[] { "CLWZCGDYCS001", pp, false }, false);
                }
            }
            else
            {
                MessageBox.Show("请先保存数据！");
            }
        }
        private void dataGView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.label3.Text = "共:" + this.dataGView2.Rows.GetRowCount(DataGridViewElementStates.Visible) + "条"; ;
        }
    }
}
