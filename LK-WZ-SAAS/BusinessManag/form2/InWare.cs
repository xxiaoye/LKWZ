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
    public partial class InWare : Form
    {
        Dictionary<string, ObjItem> dr;
        private int isAdd = 0;
        private string inware_id;
        private string inware_name;
        IAppContent app;
        public InWare(string inware_id,string inware_name,IAppContent app)
        {
            this.app = app;
            this.isAdd = 1;
            this.inware_id = inware_id;
            this.inware_name = inware_name;
            InitializeComponent();
        }
        public InWare(Dictionary<string, ObjItem> dr,int isAdd,IAppContent app)
        {
            this.app = app;
            this.isAdd = isAdd;
            this.dr = dr;
            InitializeComponent();
        }
        private TvList dwList;
        private void InWare_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            if (isAdd == 1)
            {
                this.InWare_selTextInpt.Text = inware_name;
                this.InWare_selTextInpt.Value = inware_id;
                this.InWare_selTextInpt.Enabled = false;
            }
            this.InWay_selTextInpt.Sql = "GetInWareWay";
            this.InWay_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
           
            this.gys_selTextInpt.Sql = "GetGYS";
            this.gys_selTextInpt.SelParam = "{key}|{key}|{key}|{key}";
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.lsunitcode);
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.unit);


            this.dataGView1.Url = "WZInDetailSearch";
 // TvList.newBind().SetCacheKey("XmDw").Load("GetSupply", new object[] { His.his.Choscode }).Bind(this.supply);
            dwList = TvList.newBind().SetCacheKey("XmDw").Load("GetWZUnit2", new object[] { His.his.Choscode });
            dwList.Bind(this.unit);
            if (isAdd !=1 )
            {
                this.InWay_selTextInpt.Text = LData.Exe("GetInWareWayName", "LKWZ", new object[] { His.his.Choscode, dr["入库方式ID"].ToString() });
                this.InWare_selTextInpt.Text = LData.Exe("GetInWareName", "LKWZ", new object[] { His.his.Choscode, dr["入库库房编码"].ToString() });
                this.InWay_selTextInpt.Value = dr["入库方式ID"].ToString();
                this.InWare_selTextInpt.Value = dr["入库库房编码"].ToString();
                this.InWare_selTextInpt.Enabled=false;
                this.SHDH_yTextBox.Text = dr["随货单号"].ToString();
                this.fpcode_yTextBox.Text = dr["发票号码"].ToString();
                this.totalmoney_yTextBox.Text = dr["总金额"].ToString();
                this.lstotalmoney_yTextBox.Text = dr["零售总金额"].ToString();
                if (!dr["发票日期"].IsNull)
                {
                    this.dateTimePicker1.Value = dr["发票日期"].ToDateTime();
                }
                //dwList = TvList.newBind().SetCacheKey("XmDw").Load("GetWZUnit2", new object[] { His.his.Choscode });
                //dwList.Bind(this.unit);
                this.gys_selTextInpt.Text = dr["供货商名称"].ToString();
                this.gys_selTextInpt.Value = dr["供货商ID"].ToString();
                this.memo_yTextBox.Text = dr["备注"].ToString();
                this.dataGView1.Columns[wz.Index].ReadOnly = true;
                this.dataGView1.reLoad(new object[] { dr["入库ID"].ToString(), His.his.Choscode });
                this.cancel_toolStripButton.Enabled = false;
                this.fromplan_toolStripButton.Enabled = false;
                if (isAdd == 3)
                {
                    //this.toolStrip1.Enabled = false;
                    for(int i=0;i<this.toolStrip1.Items.Count;i++)
                    {
                        this.toolStrip1.Items[i].Enabled = false;
                    }
                    this.toolStripButton1.Enabled = true;

                    this.dataGView1.ReadOnly = true;
                    this.InWare_selTextInpt.Enabled = false;
                    this.InWay_selTextInpt.Enabled = false;
                    this.gys_selTextInpt.Enabled = false;
                    this.fpcode_yTextBox.ReadOnly = true;
                    this.dateTimePicker1.Enabled = false;
                    this.SHDH_yTextBox.ReadOnly = true;
                    this.memo_yTextBox.ReadOnly = true;
                }
               
              
                
                 this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
               
                 BindUnit();
               
            }

           // this.dataGView1.addRowSel += new AddRowHandle(dataGView1_addRowSel);
            dataGView1.RowToXml += new RowToXmlHandle(dataGView1_RowToXml);
           
        }

        //void dataGView1_addRowSel(AddRowEvent e)
        //{
        //    Dictionary<string, ObjItem> data = this.dataGView1.getRowData();
        //    if (data != null)
        //    {
        //        string t = "1" + dwList.GetText(data["单位"].ToString()) + "=" + data["换算系数"].ToString() + dwList.GetText(data["最小单位编码"].ToString());
        //        this.dataGView1.CurrentRow.Cells[bzxs.Index].Value = t;
        //    }

        //}
        void BindUnit()
        {
            foreach (DataGridViewRow r in this.dataGView1.Rows)
            {
                Dictionary<string, ObjItem> data = this.dataGView1.getRowData(r);
                string dw;
                string wzdw = "";
                if (data != null)
                {
                    if (isAdd != 1)
                    {
                        wzdw = data["单位编码0"].ToString();
                    }
                   // wzdw = data["单位编码0"].ToString();
                    dw = data["单位编码"].ToString();
                    DataGridViewComboBoxCell d = r.Cells[unit.Index] as DataGridViewComboBoxCell;
                    dwList.setFilter(data["最小单位编码"].ToString(), dw, wzdw).Bind(d);

                    string t = "1" + dwList.GetText(data["单位"].ToString()) + "=" + data["换算系数"].ToString() + dwList.GetText(data["最小单位编码"].ToString());
                    r.Cells[bzxs.Index].Value = t;
                    
                    

                }
            }
        }
        void BindUnit2()
        {
            foreach (DataGridViewRow r in this.dataGView1.Rows)
            {
                Dictionary<string, ObjItem> data = this.dataGView1.getRowData(r);
                string dw;
                string wzdw = "";
                if (data != null)
                {
                    
                    wzdw = data["单位编码0"].ToString();
                    dw = data["单位编码"].ToString();
                    DataGridViewComboBoxCell d = r.Cells[unit.Index] as DataGridViewComboBoxCell;
                    dwList.setFilter(data["最小单位编码"].ToString(), dw, wzdw).Bind(d);

                }
            }
        }
        void dataGView1_RowToXml(RowToXmlEvent e)
        {
            if (e.Data["物资ID"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【物资】！");
                this.dataGView1.setFocus(e.Row.Index, "物资");
                return;
            }
            if (e.Data["单位"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行没有单位，请到物资字典中对【" + e.Data["物资"].ToString() + "】加入单位！");
                this.dataGView1.setFocus(e.Row.Index, "物资");
                return;
            }
            if (!WJs.IsZs(e.Data["数量"].ToString()) || e.Data["数量"].ToDouble() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【数量】只能输入整数，并且必须大于0！");
                this.dataGView1.setFocus(e.Row.Index, "数量");
                return;
            }
            if (!WJs.MaxNumOver(e.Data["数量"].ToString(), "第" + (e.Row.Index + 1) + "行【数量】"))
            {
                e.IsValid = false;
                this.dataGView1.setFocus(e.Row.Index, "数量");
                return;
            }
            if (!WJs.IsNum(e.Data["采购单价"].ToString()) || e.Data["采购单价"].ToFloat() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【采购单价】只能输入数字，并且必须大于0！");
                this.dataGView1.setFocus(e.Row.Index, "采购单价");
                return;
            }
            if (!WJs.IsNum(e.Data["零售单价"].ToString()) || e.Data["零售单价"].ToFloat() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【零售单价】只能输入数字，并且必须大于0！");
                this.dataGView1.setFocus(e.Row.Index, "零售单价");
                return;
            }
            if (e.Data["生产日期"].ToDateTime() > DateTime.Now)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【生产日期】不能大于当前日期！");
                this.dataGView1.setFocus(e.Row.Index, "生产日期");
                return;
            }
            if (e.Data["生产日期"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("请设置第" + (e.Row.Index + 1) + "行【生产日期】，且不能大于当前日期！");
                this.dataGView1.setFocus(e.Row.Index, "生产日期");
                return;
            }
            if (e.Data["有效期"].ToDateTime() < DateTime.Now)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【有效期】不能小于当前日期！");
                this.dataGView1.setFocus(e.Row.Index, "有效期");
                return;
            }
            if (!e.Data["生产批号"].IsNull && e.Data["生产批号"].ToString().Length > 50)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行输入的【生产批号】最多只允许50个字符！");
                this.dataGView1.setFocus(e.Row.Index, "生产批号");
                return;
            }
            if (!e.Data["批准文号"].IsNull && e.Data["批准文号"].ToString().Length > 50)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行输入的【批准文号】最多只允许50个字符！");
                this.dataGView1.setFocus(e.Row.Index, "批准文号");
                return;
            }
            if (!e.Data["卫生许可证号"].IsNull && e.Data["卫生许可证号"].ToString().Length > 50)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行输入的【卫生许可证号】最多只允许50个字符！");
                this.dataGView1.setFocus(e.Row.Index, "卫生许可证号");
                return;
            }
            if (!e.Data["备注"].IsNull && e.Data["备注"].ToString().Length > 50)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行输入的【备注】最多只允许50个字符！");
                this.dataGView1.setFocus(e.Row.Index, "备注");
                return;
            }
            if (!e.Data["条形码"].IsNull && e.Data["条形码"].ToString().Length > 50)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行输入的【条形码】最多只允许50个字符！");
                this.dataGView1.setFocus(e.Row.Index, "条形码");
                return;
            }
            if (!e.Data["生产厂家"].IsNull && e.Data["生产厂家"].ToString().Length > 50)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行输入的【生产厂家】最多只允许50个字符！");
                this.dataGView1.setFocus(e.Row.Index, "生产厂家");
                return;
            }
        }

      
        private void add_toolStripButton_Click(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            //判断是否选择库房
            if (this.InWare_selTextInpt.Value ==null)
            {
                WJs.alert("请选择入库库房！");
                InWare_selTextInpt.Focus();
                return;
            }
            //判断是否选择入库方式
            if (this.InWay_selTextInpt.Value == null)
            {
                WJs.alert("请选择入库方式！");
                InWay_selTextInpt.Focus();
                return;
            }

            Dictionary<string, object> de = new Dictionary<string, object>();
            de["生产日期"] = WJs.getDate(His.his.WebDate);
            de["有效期"] = WJs.getDate(His.his.WebDate).AddYears(1);
            de["数量"] = 0;
            de["采购单价"] = "0.0000";
            de["零售单价"] = "0.0000";
            de["采购金额"] = "0.0000";
            de["零售金额"] = "0.0000";
            
            string wareid = this.InWare_selTextInpt.Value;
            string ifall = LData.Es("WZWareIfall", "LKWZ", new object[] { wareid });
            //string wz_s = "物资";
           // string wzid_s="";
            if (ifall.Equals("1"))
            {
               // this.InWZ_selTextInpt.Sql = "GetInWZ0";
                dataGView1.addSql("GetInWZ0", "物资", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");
                //this.InWZ_selTextInpt.SelParam = His.his.Choscode + "|" + wareid + "|{key}|{key}";
               // dataGView1.addSql("",wz_s,wzid_s,
            }
            else
            {
               // this.InWZ_selTextInpt.Sql = "GetInWZ";
                dataGView1.addSql("GetInWZ", "物资", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");
              
            }
          

          this.dataGView1.addSql("GetSupply", "生产厂家", "", His.his.Choscode + "|{key}|{key}|{key}|{key}");
           // TvList.newBind().SetCacheKey("XmDw").Load("GetSupply", new object[] { His.his.Choscode }).Bind(this.supply);
            this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);

          // this.dataGView1.CellEndEdit += new DataGridViewCellEventHandler(dataGView1_CellEndEdit);
           
            this.dataGView1.AddRow(de, 0);
            this.dataGView1.CurrentRow.Cells[wz.Index].ReadOnly = false;


        }

        //void dataGView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    Dictionary<string, ObjItem> data = this.dataGView1.getRowData(this.dataGView1.Rows[e.RowIndex]);
        //    if (!data["物资ID"].IsNull)
        //    {
        //        if (e.ColumnIndex == wz.Index)
        //        {


        //            DataGridViewComboBoxCell d = this.dataGView1.CurrentRow.Cells[unit.Index] as DataGridViewComboBoxCell;
        //            string lsunit_s = LData.Es("GetWZUNITCODEName", null, new object[] { data["最小单位编码"].ToString() });


        //            if (!data["最小单位编码"].ToString().Equals("") && !data["最小单位编码"].ToString().Equals(data["单位编码"].ToString()))
        //            {
        //                //d.Items.Add(lsunit_s, data["最小单位编码"].ToString());
        //                TvList.newBind().add(data["单位0"].ToString(), data["单位编码"].ToString()).add(lsunit_s, data["最小单位编码"].ToString()).Bind(d);
        //            }
        //            else
        //            {
        //                TvList.newBind().add(data["单位0"].ToString(), data["单位编码"].ToString()).Bind(d);
        //            }
        //            this.dataGView1.CurrentRow.Cells[unit.Index].Value = data["单位编码"].ToString();
        //            this.dataGView1.CurrentRow.Cells[price.Index].Value = data["采购单价0"].ToString();

        //        }
        //    }
            
        //}

        
        void dataGView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGView1.ReadOnly) return;
           
            Dictionary<string, ObjItem> data = this.dataGView1.getRowData(this.dataGView1.Rows[e.RowIndex]);
            string unit_o;
            if (e.ColumnIndex == txm.Index || e.ColumnIndex == unit.Index)
            {
                if (e.ColumnIndex == txm.Index)
                {
                    BindUnit();
                    this.dataGView1.CurrentRow.Cells[unit.Index].Value = data["单位编码"].ToString();
                    this.dataGView1.CurrentRow.Cells[price.Index].Value = data["采购单价0"].ToString();
                    unit_o = data["单位编码"].ToString();
                }
                else
                {
                    unit_o = this.dataGView1.CurrentRow.Cells[unit.Index].Value.ToString();
                    this.dataGView1.CurrentRow.Cells[unitcode.Index].Value = data["单位"].ToString();
                }

                string lsunitcode_o = data["最小单位编码"].ToString();


                //判断该库房价格的计价体系是否统一浮动
                string ifall_s = LData.Es("PriceIfAll", "LKWZ", new object[] { His.his.Choscode, this.InWare_selTextInpt.Value });
                this.dataGView1.CurrentRow.Cells[price.Index].Value = data["采购单价0"].ToDecimal();
                string priceid_s = LData.Es("Ware_GetPriceID", null, new object[] { His.his.Choscode, this.InWare_selTextInpt.Value });
                string rate_s = LData.Es("GetPriceRate", "LKWZ", new object[] { priceid_s });
                if (ifall_s.Equals("1"))
                {
                    //该库房的计价体系是否统一浮动
                    //获取计价体系的加价率
                    //string priceid_s = LData.Es("Ware_GetPriceID", null, new object[] { His.his.Choscode, this.InWare_selTextInpt.Value });
                   // string rate_s = LData.Es("GetPriceRate", null, new object[] { priceid_s });
                    this.dataGView1.CurrentRow.Cells[lsprice.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() * (1 + Convert.ToDecimal(rate_s))) + "", 4);


                }
                else
                {
                    //根据采购单价*（1+各个物资字典里的加价率）获得零售单价
                    this.dataGView1.CurrentRow.Cells[lsprice.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() * (1 + data["加价率"].ToDecimal())) + "", 4);
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
                        //string rate_s = LData.Es("GetPriceRate", null, new object[] { priceid_s });
                        this.dataGView1.CurrentRow.Cells[lsprice.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() * (1 + Convert.ToDecimal(rate_s)) / i) + "", 4);
                    }
                    else
                    {
                        //根据采购单价*（1+各个物资字典里的加价率）获得零售单价
                        this.dataGView1.CurrentRow.Cells[lsprice.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() * (1 + data["加价率"].ToDecimal()) / i) + "", 4);
                    }
                    this.dataGView1.CurrentRow.Cells[price.Index].Value = MathUtil.round((data["采购单价0"].ToDecimal() / i) + "", 4);
                    // this.dataGView1.CurrentRow.Cells[lsprice.Index].Value = MathUtil.round((data["零售单价"].ToDecimal() / i) + "", 4);
                }
                        

               
            }
          
            if (e.ColumnIndex == validate_temp.Index || e.ColumnIndex == productdate.Index)
            {
                try
                {
                    if (!this.dataGView1.CurrentRow.Cells[validate_temp.Index].Value.ToString().Equals(""))
                    {
                        this.dataGView1.CurrentRow.Cells[validate.Index].Value = Convert.ToDateTime(this.dataGView1.CurrentRow
                            .Cells[productdate.Index].Value).AddMonths(Convert.ToInt32(this.dataGView1.CurrentRow.Cells[validate_temp.Index].Value));
                    }
                    else
                    {
                        this.dataGView1.CurrentRow.Cells[validate.Index].Value = Convert.ToDateTime(this.dataGView1.CurrentRow
                            .Cells[productdate.Index].Value).AddMonths(12);
                    }
                }
                catch
                { 
                }
                
            }
            //if (e.ColumnIndex == supply.Index)
            //{
            //    try
            //    {
            //        if (this.dataGView1.CurrentRow.Cells[supply.Index].Value != null)
            //        {
            //            this.dataGView1.CurrentRow.Cells[supplyid.Index].Value = data["生产厂家"].ToString();
            //        }
            //    }
            //    catch
            //    { 
            //    }
            //}

            if ((e.ColumnIndex == unit.Index || e.ColumnIndex == num.Index || e.ColumnIndex == price.Index || e.ColumnIndex == lsprice.Index) && e.RowIndex > -1)
            {
                try
                {

                    this.dataGView1.jsBds("零售金额=数量*零售单价");
                    this.dataGView1.jsBds("采购金额=数量*采购单价");
                    ////统计金额
                    this.totalmoney_yTextBox.Text = dataGView1.Sum("采购金额").ToString("0.0000");
                    this.lstotalmoney_yTextBox.Text = dataGView1.Sum("零售金额").ToString("0.0000");

                }
                catch
                {

                }
            
            }
           
            
        }
        private bool valid()
        {
            if (this.InWay_selTextInpt.Value==null)
            {
                WJs.alert("请选择入库方式！");
                this.InWay_selTextInpt.Focus();
                return false;
            }
            if (this.InWare_selTextInpt.Value==null)
            {
                WJs.alert("请选择入库库房！");
                this.InWare_selTextInpt.Focus();
                return false;
            }
           
            return true;
        }
        private void save_toolStripButton_Click(object sender, EventArgs e)
        {
            if (this.valid())
            {
                if (this.dataGView1.RowCount == 0)
                {
                    WJs.alert("请添加入库物资");
                    return;
                }
                if (this.totalmoney_yTextBox.Text.Trim().Length > 11)
                {
                    WJs.alert("总金额太大(不能超过100000.0000)！请减少该批次物资");
                    return;
                }
                if (this.lstotalmoney_yTextBox.Text.Trim().Length > 11)
                {
                    WJs.alert("零售金额太大(不能超过100000.0000)！请减少该批次物资");
                    return;
                }
                string str = this.dataGView1.GetDataToXml();
                if (str != null)
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkwz.WZIn.WZInDan";
                    ac.Sql = "RuKuDanSave";
                    //ac.Add("过单日期", this.dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    ac.Add("IOID", this.InWay_selTextInpt.Value);
                    ac.Add("WARECODE", this.InWare_selTextInpt.Value);
                    ac.Add("SUPPLYNAME", this.gys_selTextInpt.Text);
                    if (this.gys_selTextInpt.Value!=null)
                    {
                        ac.Add("SUPPLYID", this.gys_selTextInpt.Value);

                    }
                    
                    
                    ac.Add("INDATE", DateTime.Now);
                    ac.Add("INVOICEDATE", this.dateTimePicker1.Value);
                    ac.Add("INVOICECODE", this.fpcode_yTextBox.Text);
                    ac.Add("MEMO", this.memo_yTextBox.Text);
                    ac.Add("SHDH", this.SHDH_yTextBox.Text);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.Add("USERID", His.his.UserId);
                    ac.Add("USERNAME", His.his.UserName);
                    ac.Add("RECDATE", DateTime.Now);
                    ac.Add("TOTALMONEY", this.totalmoney_yTextBox.Text);
                    ac.Add("LSTOTALMONEY",this.lstotalmoney_yTextBox.Text);
                    if (this.plan_selTextInpt.Value != null)
                    {
                        //该入库单是从采购计划中获取
                        ac.Add("PLANNO", this.plan_selTextInpt.Text);
                    }
                    ac.Add("DanJuMx", str);
                    if (isAdd == 2)
                    {
                        ac.Add("INID", dr["入库ID"].ToString());
                    }
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    
                    ac.Post();

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
        void InitInWare()
        {
            this.dataGView1.Rows.Clear();
            this.InWay_selTextInpt.Text = "";
            this.InWay_selTextInpt.Value = "";
            //this.InWare_selTextInpt.Text = "";
           // this.InWare_selTextInpt.Value = "";
            this.fpcode_yTextBox.Text = "";
            this.SHDH_yTextBox.Text = "";
            this.memo_yTextBox.Text = "";
            this.gys_selTextInpt.Text = "";
            this.gys_selTextInpt.Value = "";
            this.totalmoney_yTextBox.Text = "0.0000";
            this.lstotalmoney_yTextBox.Text = "0.0000";

        }

        private void cancel_toolStripButton_Click(object sender, EventArgs e)
        {
            if(WJs.confirm("是否放弃保存，暂存数据将清空！"))
            {
                InitInWare();
                
            }
        }

        private void DeleButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> doc = this.dataGView1.getRowData();
            if (doc != null)
            {
                if (doc["物资"].IsNull)
                {
                    this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                }
                else
                {
                    if (WJs.confirmFb("您确定要删除选择的入库物资信息吗？"))
                    {

                        this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                        this.dataGView1.jsBds("零售金额=数量*零售单价");
                        this.dataGView1.jsBds("采购金额=数量*采购单价");
                        ////统计金额
                        this.totalmoney_yTextBox.Text = dataGView1.Sum("采购金额").ToString("0.0000");
                        this.lstotalmoney_yTextBox.Text = dataGView1.Sum("零售金额").ToString("0.0000");
                        if (!doc["流水号"].IsNull && !doc["流水号"].ToString().Equals(""))
                        {
                            //数据库中已经存在该记录，需要删除数据库中的记录

                            ActionLoad ac = new ActionLoad();
                            ac.Action = "LKWZSVR.lkwz.WZIn.WZInDan";
                            ac.Sql = "RuKuDanWZdelete";
                            ac.Add("DETAILNO", doc["流水号"].ToString());
                            //ac.Add("WZID", doc["物资ID"].ToString());
                            //获取入库ID
                            string inwareid = LData.Es("GetInWareId", "LKWZ", new object[] { doc["流水号"].ToString() });

                            ac.Add("INID", inwareid);


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

        private void fromplan_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr == null)
            {
               

                this.KeyPreview = true;
                //判断是否选择库房
                if (this.InWare_selTextInpt.Value == null)
                {
                    WJs.alert("请选择入库库房！");
                    InWare_selTextInpt.Focus();
                    return;
                }
                //判断是否选择入库方式
                if (this.InWay_selTextInpt.Value == null)
                {
                    WJs.alert("请选择入库方式！");
                    InWay_selTextInpt.Focus();
                    return;
                }
                
                this.label11.Visible = true;
                this.plan_selTextInpt.Visible = true;
                this.button1.Visible = true;
                this.plan_selTextInpt.TextChanged += new EventHandler(plan_selTextInpt_TextChanged);
                this.plan_selTextInpt.TextChanged += new EventHandler(plan_selTextInpt_TextChanged);
                this.plan_selTextInpt.Sql = "GetWZPlan";
                this.plan_selTextInpt.SelParam = His.his.Choscode + "|" + inware_id;
                //判断是否选择采购计划
                if (this.plan_selTextInpt.Value == null)
                {
                    WJs.alert("请选择采购计划！");
                    plan_selTextInpt.Focus();
                    return;
                }
               
                
            }
            else
            {
                WJs.alert("该入库单已经有明细数据，不能从采购计划中生成明细信息！");
            }
        }

        void plan_selTextInpt_TextChanged(object sender, EventArgs e)
        {
            this.button1.Enabled = true;
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            //add_toolStripButton_Click(null, null);
            this.dataGView1.CellValueChanged +=new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
          //  this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
            this.productdate.DataPropertyName = null;
            //this.validate_temp.DataPropertyName = null;
            this.validate.DataPropertyName = null;
            this.dataGView1.Url = "WZPlanDetailSearch";
            this.dataGView1.reLoad(new object[] { this.plan_selTextInpt.Text.Trim().ToString(), His.his.Choscode });
            BindUnit2();
            if (this.dataGView1.RowCount == 0)
            {
                WJs.alert("该采购计划没有明细数据，不能生成入库单");
                return;
            }
            else
            {
                this.button1.Enabled = true;
                this.cancel_toolStripButton.Enabled = false;
            }
            this.dataGView1.jsBds("零售金额=数量*零售单价");
            this.dataGView1.jsBds("采购金额=数量*采购单价");
            ////统计金额
            this.totalmoney_yTextBox.Text = dataGView1.Sum("采购金额").ToString("0.0000");
            this.lstotalmoney_yTextBox.Text = dataGView1.Sum("零售金额").ToString("0.0000");
            //this.plan_selTextInpt.Enabled = false;
            this.dataGView1.Columns[wz.Index].ReadOnly = true;
            this.fromplan_toolStripButton.Enabled = false;
            //this.cancel_toolStripButton.Enabled = false;
            this.button1.Enabled = false;
            this.add_toolStripButton.Enabled = false;
        
            foreach (DataGridViewRow r in this.dataGView1.Rows)
            {
                //Dictionary<string, ObjItem> data = this.dataGView1.getRowData(r);
                if (r != null)
                {
                    r.Cells[productdate.Index].Value = DateTime.Now;
                    r.Cells[validate.Index].Value = WJs.getDate(His.his.WebDate).AddYears(1);
                    if (!r.Cells[validate_temp.Index].Value.ToString().Equals(""))
                    {
                        r.Cells[validate.Index].Value = Convert.ToDateTime(r.Cells[productdate.Index].Value).AddMonths(Convert.ToInt32(r.Cells[validate_temp.Index].Value));
                    }
                    else
                    {
                        Convert.ToDateTime(r.Cells[productdate.Index].Value).AddMonths(12);
                    }

                }
            }
        
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (inware_id == null)
            {
                ActionLoad ld = ActionLoad.Conn();
                DataTable tb = ld.Find("InMainDaYinQuery", new object[] { dr["入库ID"].ToString() });
               
                
                if (tb != null && tb.Rows.Count > 0)
                {
                    // setDefalutStock(tb.Rows[0], r);
                     string KuFangName = LData.Exe("GetInWareName", "LKWZ", new object[] { His.his.Choscode, tb.Rows[0]["WARECODE"].ToString() });
                    string RuKuFangShi=LData.Exe("GetInWareWayName", "LKWZ", new object[] { His.his.Choscode, tb.Rows[0]["IOID"].ToString() });
                    Dictionary<string, object> pp = new Dictionary<string, object>();
                    pp.Add("BiaoTi", tb.Rows[0][0].ToString() + "  【" + KuFangName + "】  " + "入库单");
                    pp.Add("FangXiang", "入库到  【" + KuFangName + "】");
                    pp.Add("Time", "时间：" + tb.Rows[0]["RECDATE"].ToString());
                    pp.Add("LeiBie", "类别：" + RuKuFangShi);
                    pp.Add("BianHao", "单据号：" + tb.Rows[0]["RECIPECODE"].ToString());
                    pp.Add("HeJi", "供应商：" + tb.Rows[0]["SUPPLYNAME"].ToString());
                    pp.Add("JinE1", tb.Rows[0]["TOTALMONEY"].ToString());
                    pp.Add("JinE2", tb.Rows[0]["LSTOTALMONEY"].ToString());
                    pp.Add("Name1", "操作员：" + tb.Rows[0]["USERNAME"].ToString());
                    pp.Add("Name2", "审核员：" + tb.Rows[0]["SHUSERNAME"].ToString());
                    pp.Add("Name3", "备注：" + tb.Rows[0]["MEMO"].ToString());
                    pp.Add("Name4", "入库员：" + tb.Rows[0]["SHINUSERNAME"].ToString());
                    pp.Add("ID", dr["入库ID"].ToString());
                    app.LoadPlug("RepEdit.RepView", new object[] { "CLWZRKDYCS001", pp, false }, false);
                }
            }
            else
            {
                MessageBox.Show("请先保存数据！");
            }
        }
        private void dataGView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.label12.Text = "共:" + this.dataGView1.Rows.GetRowCount(DataGridViewElementStates.Visible) + "条"; ;
        }
        

       
    }
}
