using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YiTian.db;
using YtPlugin;
using YtClient;
using ChSys;
using YtUtil.tool;
using YtWinContrl.com.datagrid;

namespace BusinessManag.form2
{
    public partial class OutWare : Form
    {
       Dictionary<string, ObjItem> dr;
        private int isAdd = 0;
        private string inware_id;
        private string inware_name;
        IAppContent app;
        public OutWare(string inware_id, string inware_name, IAppContent app)
        {
            this.app = app;
            this.isAdd = 1;
            this.inware_id = inware_id;
            this.inware_name = inware_name;
            InitializeComponent();
        }
        public OutWare(string inware_id, string inware_name, Dictionary<string, ObjItem> dr, int isAdd, IAppContent app)
        {
            this.app = app;
            this.isAdd = isAdd;
            this.dr = dr;
            this.inware_id = inware_id;
            this.inware_name = inware_name;
            InitializeComponent();
        }
        private TvList dwList;
        private void OutWare_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.InWare_selTextInpt.Text = inware_name;
            this.InWare_selTextInpt.Value = inware_id;
            this.InWay_selTextInpt.Sql = "GetOutWareWay";
            this.InWay_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";

            
            this.dataGView1.Url = "WZOutDetailSearch";
           // TvList.newBind().SetCacheKey("XmDw").Load("GetSupply", new object[] { His.his.Choscode }).Bind(this.supply);
            dwList = TvList.newBind().SetCacheKey("XmDw").Load("GetWZUnit2", new object[] { His.his.Choscode });
            dwList.Bind(this.unit);
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.lsunit);
           
            //this.dataGView1.jsBds("入库金额=数量*入库单价");
            //this.dataGView1.jsBds("出库金额=数量*出库单价");

            if (isAdd != 1)
            {
                this.InWay_selTextInpt.Text = LData.Exe("GetOutWareWayName", "LKWZ", new object[] { His.his.Choscode, dr["出库方式ID"].ToString() });
                //this.InWare_selTextInpt.Text = LData.Exe("GetInWareName", null, new object[] { His.his.Choscode, dr["入库库房编码"].ToString() });
                this.InWay_selTextInpt.Value = dr["出库方式ID"].ToString();
               // this.InWare_selTextInpt.Value = dr["出库库房编码"].ToString();
                this.InWare_selTextInpt.Enabled = false;
                
                this.totalmoney_yTextBox.Text = dr["总金额"].ToString();
                this.lstotalmoney_yTextBox.Text = dr["零售总金额"].ToString();
               
                this.memo_yTextBox.Text = dr["备注"].ToString();
                this.dataGView1.Columns[wz.Index].ReadOnly = true;
                this.dataGView1.Columns[flowno.Index].ReadOnly = true;
                this.dataGView1.reLoad(new object[] { dr["出库ID"].ToString(), His.his.Choscode });
                this.cancel_toolStripButton.Enabled = false;
               
                if (isAdd == 3)
                {
                    //this.toolStrip1.Enabled = false;
                    for (int i = 0; i < this.toolStrip1.Items.Count; i++)
                    {
                        this.toolStrip1.Items[i].Enabled = false;
                    }
                    this.toolStripButton1.Enabled = true;

                    this.dataGView1.ReadOnly = true;
                    this.InWare_selTextInpt.Enabled = false;
                    this.InWay_selTextInpt.Enabled = false;
                    
                    this.memo_yTextBox.ReadOnly = true;
                }



                this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
                BindUnit();

            }
            dataGView1.RowToXml += new RowToXmlHandle(dataGView1_RowToXml);
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
                this.dataGView1.setFocus(e.Row.Index, "数量");
                WJs.alert("第" + (e.Row.Index + 1) + "行【数量】只能输入整数，并且必须大于0！");
               
                return;
            }
            if (!WJs.MaxNumOver(e.Data["数量"].ToString(), "第" + (e.Row.Index + 1) + "行【数量】"))
            {
                e.IsValid = false;
                this.dataGView1.setFocus(e.Row.Index, "数量");
               
                return;
            }
            if (e.Data["单位"].ToString().Equals(e.Data["最小单位"].ToString()) && e.Data["数量"].ToDecimal() > e.Data["库存剩余量"].ToDecimal())
            {
                e.IsValid = false;

                WJs.alert("第" + (e.Row.Index + 1) + "行【数量】不能大于" + e.Data["库存剩余量"].ToDecimal());
                this.dataGView1.setFocus(e.Row.Index, "数量");
                
                return;
            }
            if (!e.Data["单位"].ToString().Equals(e.Data["最小单位"].ToString()) && e.Data["数量"].ToDecimal() > e.Data["库存剩余量"].ToDecimal() / e.Data["换算系数"].ToDecimal())
            {
                e.IsValid = false;

                WJs.alert("第" + (e.Row.Index + 1) + "行【数量】不能大于" + e.Data["库存剩余量"].ToDecimal() / e.Data["换算系数"].ToDecimal());
                this.dataGView1.setFocus(e.Row.Index, "数量");
               
                return;
            }
            
            //List<Dictionary<string, ObjItem>> listdata = this.dataGView1.GetData();

            //if (listdata != null)
            //{
            //    decimal count = 0;
                
            //    foreach (var ro in listdata)
            //    {
            //        if (ro != null)
                    
            //            if (this.dataGView1.CurrentRow.Cells[flowno.Index].Value.Equals(ro["库存流水号"].ToString()))
            //            {
            //                count++;
            //                if (count > 1)
            //                {
            //                    WJs.alert("您已经选择了该流水记录，不能再次选择！");
            //                    this.dataGView1.CurrentRow.Cells[flowno.Index].Value = "";
            //                    this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "库存流水号");
            //                    return;
            //                }
            //            }
            //     }
            // }
         }
        
        void BindUnit()
        {
            foreach (DataGridViewRow r in this.dataGView1.Rows)
            {
                Dictionary<string, ObjItem> data = this.dataGView1.getRowData(r);
                //string dw;
                //string wzdw = "";
                if (data != null)
                {
                    //if (isAdd != 1)
                    //{
                    //    wzdw = data["单位编码0"].ToString();
                    //}
                    //// wzdw = data["单位编码0"].ToString();
                    //dw = data["单位"].ToString();
                    DataGridViewComboBoxCell d = r.Cells[unit.Index] as DataGridViewComboBoxCell;
                    dwList.setFilter(data["最小单位"].ToString(), data["物资单位"].ToString()).Bind(d);
                    string t = "1" + dwList.GetText(data["物资单位"].ToString()) + "=" + data["换算系数"].ToString() + dwList.GetText(data["最小单位"].ToString());
                    r.Cells[bzxs.Index].Value = t;
                }
            }
        }
        private void add_toolStripButton_Click(object sender, EventArgs e)
        {
            this.KeyPreview = true;
           
            //判断是否选择入库方式
            if (this.InWay_selTextInpt.Value == null)
            {
                WJs.alert("请选择出库方式！");
                InWay_selTextInpt.Focus();
                return;
            }

            Dictionary<string, object> de = new Dictionary<string, object>();
            // de["生产日期"] = WJs.getDate(His.his.WebDate);
            //de["有效期"] = WJs.getDate(His.his.WebDate).AddYears(1);
            //de["数量"] = 0;
            //de["采购单价"] = "0.0000";
            //de["零售单价"] = "0.0000";
            //de["金额"] = "0.0000";
            //de["零售金额"] = "0.0000";
            de["医疗机构编码"] = His.his.Choscode;
            string wareid = this.InWare_selTextInpt.Value;
           // string hispra = LData.Es("GetHisPra", null, null);
            //string hispra = LData.Es("GetHisPra", null, new object[] { His.his.Choscode });
            //string wz_s = "物资";
            // string wzid_s="";
            //if (hispra == "0")
            //{
            //    //this.OutWZ_selTextInpt.Sql = "GetOutWZ1";
            //    dataGView1.addSql("GetOutWZ1", "物资", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");
            //}
            //else
            //{
            //    //this.OutWZ_selTextInpt.Sql = "GetOutWZ2";
            //    dataGView1.addSql("GetOutWZ2", "物资", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");

            //}
            dataGView1.addSql("GetWZSTOCKS", "物资", "", this.inware_id + "|" + His.his.Choscode +"|" + this.inware_id + "|" + His.his.Choscode);

           // dataGView1.addSql("GetWZSTOCKS", "库存流水号", "",this.inware_id + "|"+ this.inware_id);

            BindUnit();
           // 
            this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
           
            //this.dataGView1.AddRow(de, 0);
            this.dataGView1.AddRow(de, 0);
            this.dataGView1.CurrentRow.Cells[wz.Index].ReadOnly = false;
            //this.dataGView1.CurrentRow.Cells[flowno.Index].ReadOnly = false;
        }

       
        void dataGView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Dictionary<string, ObjItem> data = this.dataGView1.getRowData(this.dataGView1.Rows[e.RowIndex]);

            if (e.ColumnIndex == txm.Index)
            {
                BindUnit();
            }
            //if(e.ColumnIndex==wzid.Index)
            //{
            //    dataGView1.addSql("GetWZSTOCK", "库存流水号", "", this.dataGView1.CurrentRow.Cells[wzid.Index].Value.ToString() +
            //        "|" + this.inware_id + "|" + this.dataGView1.CurrentRow.Cells[wzid.Index].Value.ToString() + "|" + this.inware_id);
               
            //    BindUnit();
            //    this.dataGView1.CurrentRow.Cells[wz.Index].ReadOnly = true;
                
            //}
            //if (e.ColumnIndex == flowno.Index)
            //{
            //    List<Dictionary<string, ObjItem>> listdata = this.dataGView1.GetData();
            //    if (listdata != null)
            //    {
            //        listdata.RemoveAt(listdata.Count - 1);
            //    }
                
                
            //    if (listdata != null)
            //    {
            //        //decimal count = 0;
            //        foreach (var ro in listdata)
            //        {
            //            if (ro != null)
            //            {
            //                if (this.dataGView1.CurrentRow.Cells[flowno.Index].Value.Equals(ro["库存流水号"].ToString()))
            //                {
            //                    WJs.alert("您已经选择了该流水记录，不能再次选择！");
            //                    this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                                
            //                    //this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "库存流水号");
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //}
            if (e.ColumnIndex == unit.Index)
            {
                //单位变化的时候，价格也要变
                decimal num_d = data["库存剩余量"].ToDecimal();
                decimal hsxs_d = data["换算系数"].ToDecimal();
                //decimal inmoney_d = data["入库金额"].ToDecimal();
                //decimal outmoney_d = data["出库金额"].ToDecimal();
                decimal inprice_d = data["入库单价0"].ToDecimal();
                decimal outprice_d = data["出库单价0"].ToDecimal();
                if (!this.dataGView1.CurrentRow.Cells[unit.Index].Value.Equals(this.dataGView1.CurrentRow.Cells[lsunit.Index].Value))
                {
                    
                    this.dataGView1.CurrentRow.Cells[inprice.Index].Value = inprice_d * hsxs_d;
                    this.dataGView1.CurrentRow.Cells[outprice.Index].Value = outprice_d * hsxs_d;
                    this.dataGView1.CurrentRow.Cells[num.Index].Value = num_d / hsxs_d;

                }
                else
                {
                    
                    this.dataGView1.CurrentRow.Cells[inprice.Index].Value = data["入库单价0"].ToDecimal();
                    this.dataGView1.CurrentRow.Cells[outprice.Index].Value = data["出库单价0"].ToDecimal();
                    this.dataGView1.CurrentRow.Cells[num.Index].Value = data["库存剩余量"].ToDecimal();
                }

              

            }
            if (e.ColumnIndex == num.Index)
            {
                try
                {

                    this.dataGView1.jsBds("入库金额=数量*入库单价");
                    this.dataGView1.jsBds("出库金额=数量*出库单价");
                    ////统计金额
                    this.totalmoney_yTextBox.Text = dataGView1.Sum("入库金额").ToString("0.0000");
                    this.lstotalmoney_yTextBox.Text = dataGView1.Sum("出库金额").ToString("0.0000");

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
        //private bool checkNum(string wzId, string liuShuiId)
        //{
        //    int num = 0;
        //    for (int i = 0; i < this.dataGView1.Rows.Count; i++)
        //    {
        //        if (this.dataGView1.Rows[i].Visible == true)
        //        {
        //            DataGridViewCellCollection r = this.dataGView1.Rows[i].Cells;
        //            if (r["WzId"].Value.ToString() == wzId && r["STOCKFLOWID"].Value.ToString() == liuShuiId)
        //            {
        //                //num += Convert.ToInt32(r["ShuLiang"].Value);
        //                if (r["DanWei"].Value == r["DanWeiC2"])
        //                {
        //                    num += Convert.ToInt32(r["ShuLiang"].Value) * Convert.ToInt32(r["ChangRate"].Value);
        //                }
        //                else
        //                {
        //                    num += Convert.ToInt32(r["ShuLiang"].Value);
        //                }
        //                if (num > Convert.ToInt32(r["YuLiang"].Value))
        //                {
        //                    WJs.alert("从第" + (i + 1).ToString() + "行起，与库存流水" + liuShuiId + "中的" + r["WuZi"].Value.ToString() + "已经超出此流水的最大余量，请为第" + (i + 1).ToString() + "行后的此物质，选择其他流水");
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //    return true;
        //}
        private bool IsFlowNoRepeat()
        {
            //当每行数据正常时，判断流水号是否重复
           // List<Dictionary<string,ObjItem>> dc = this.dataGView1.GetData();
            int rc = this.dataGView1.RowCount;
            if (rc >0)
            {
               // int len = dc.Count();
                //int[] countflno = new int[len];
                string fl1 ;
                string fl2;
                for (int i = 0; i < rc; i++)
                {
                    //fl1 = dc[i]["库存流水号"].ToString();
                   fl1= this.dataGView1["flowno", i].Value.ToString();
                    for (int j = i + 1; j <rc; j++)
                    {
                        //fl2 = dc[j]["库存流水号"].ToString();
                        fl2 = this.dataGView1["flowno", j].Value.ToString();
                        if (fl1.Equals(fl2))
                        {
                            //dc[i]["库存流水号"].getValue == dc[i]["库存流水号"].getValue;
                            ++i;
                            ++j;
                            WJs.alert("第" + j + "行的库存流水号与第" + i + "行的库存流水号重复,请更改第" + j + "行的库存流水号");
                            this.dataGView1.setFocus(j-1,"库存流水号");
                            return false;
                        }
                    }
                }
            }
            
            return true;
        }
        private void save_toolStripButton_Click(object sender, EventArgs e)
        {
            if (this.dataGView1.RowCount == 0)
            {
                WJs.alert("请添加入库物资");
                return;
            }
            if (this.totalmoney_yTextBox.Text.Trim().Length > 11)
            {
                WJs.alert("入库总金额太大(不能超过100000.0000)！请减少该批次物资");
                return;
            }
            if (this.lstotalmoney_yTextBox.Text.Trim().Length > 11)
            {
                WJs.alert("出库金额太大(不能超过100000.0000)！请减少该批次物资");
                return;
            }


            //流水号重复就返回
            //if (!IsFlowNoRepeat())
            //{
            //    return;
            //}
            //if (!IsFlowNoRepeat())
            //{
            //    return;
            //}
           
            //if (IsFlowNoRepeat())
            //{
            //    str = this.dataGView1.GetDataToXml();
            //}
            //else
            //{
            //    return;
            //}
            //dataGView1.RowToXml += new RowToXmlHandle(dataGView1_RowToXml);
            string str = this.dataGView1.GetDataToXml();

            // 
            if (str != null && IsFlowNoRepeat())
            {
                ActionLoad ac = ActionLoad.Conn();
                ac.Action = "LKWZSVR.lkwz.WZOut.WZOutDan";
                ac.Sql = "ChuKuDanSave";

                ac.Add("IOID", this.InWay_selTextInpt.Value);
                ac.Add("WARECODE", this.InWare_selTextInpt.Value);
                //ac.Add("SUPPLYNAME", this.gys_selTextInpt.Text);
                //ac.Add("SUPPLYID", this.gys_selTextInpt.Value);
                ac.Add("OUTDATE", DateTime.Now);
                //ac.Add("INVOICEDATE", this.dateTimePicker1.Value);
                //ac.Add("INVOICECODE", this.fpcode_yTextBox.Text);
                ac.Add("MEMO", this.memo_yTextBox.Text);
                //ac.Add("SHDH", this.SHDH_yTextBox.Text);
                ac.Add("CHOSCODE", His.his.Choscode);
                ac.Add("USERID", His.his.UserId);
                ac.Add("USERNAME", His.his.UserName);
                //ac.Add("RECDATE", DateTime.Now);
                ac.Add("TOTALMONEY", this.totalmoney_yTextBox.Text);
                ac.Add("LSTOTALMONEY", this.lstotalmoney_yTextBox.Text);

                ac.Add("DanJuMx", str);
                if (isAdd == 2)
                {
                    ac.Add("OUTID", dr["出库ID"].ToString());
                }
                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);

                ac.Post();
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
                    if (WJs.confirmFb("您确定要删除选择的出库物资信息吗？"))
                    {

                        this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                        this.dataGView1.jsBds("入库金额=数量*入库单价");
                        this.dataGView1.jsBds("出库金额=数量*出库单价");
                        ////统计金额
                        this.totalmoney_yTextBox.Text = dataGView1.Sum("入库金额").ToString("0.0000");
                        this.lstotalmoney_yTextBox.Text = dataGView1.Sum("出库金额").ToString("0.0000");

                        if (!doc["流水号"].IsNull && !doc["流水号"].ToString().Equals(""))
                        {
                            //数据库中已经存在该记录，需要删除数据库中的记录

                            ActionLoad ac = new ActionLoad();
                            ac.Action = "LKWZSVR.lkwz.WZOut.WZOutDan";
                            ac.Sql = "ChuKuDanWZdelete";
                            ac.Add("DETAILNO", doc["流水号"].ToString());
                            //ac.Add("WZID", doc["物资ID"].ToString());
                            //获取入库ID
                            string outwareid = LData.Es("GetOutWareId", "LKWZ", new object[] { doc["流水号"].ToString() });

                            ac.Add("OUTID", outwareid);


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
        void InitOutWare()
        {
            this.dataGView1.Rows.Clear();
            this.InWay_selTextInpt.Text = "";
            this.InWay_selTextInpt.Value = "";
           
           
            this.memo_yTextBox.Text = "";
           
            this.totalmoney_yTextBox.Text = "0.0000";
            this.lstotalmoney_yTextBox.Text = "0.0000";

        }
        private void cancel_toolStripButton_Click(object sender, EventArgs e)
        {
            if (WJs.confirm("是否放弃保存，暂存数据将清空！"))
            {
                InitOutWare();

            }
        }
        private void Print_Click(object sender, EventArgs e)
        {
            if (isAdd == 3)
            {
                ActionLoad ld = ActionLoad.Conn();
                DataTable tb = ld.Find("OutMainDaYinQuery", new object[] { dr["出库ID"].ToString() });
                if (tb != null && tb.Rows.Count > 0)
                {
                    // setDefalutStock(tb.Rows[0], r);

                    Dictionary<string, object> pp = new Dictionary<string, object>();
                    pp.Add("BiaoTi", tb.Rows[0][0].ToString() + "  【" + inware_name + "】  " + "出库单");
                    pp.Add("FangXiang", "出库库房：  【" + inware_name + "】");
                    pp.Add("Time", "时间：" + tb.Rows[0][5].ToString());
                    pp.Add("LeiBie", "类别：出库单");
                    pp.Add("BianHao", "单据号：" + tb.Rows[0][8].ToString());
                    //pp.Add("HeJi", "合计");
                    pp.Add("JinE1", tb.Rows[0][9].ToString());
                    pp.Add("JinE2", tb.Rows[0][10].ToString());
                    pp.Add("Name1", "操作员：" + tb.Rows[0][1].ToString());
                    pp.Add("Name2", "审核员：" + tb.Rows[0][2].ToString());
                    pp.Add("Name3", "");
                    pp.Add("Name4", "出库员：" + tb.Rows[0][3].ToString());
                    pp.Add("HeJi", "备注：" + tb.Rows[0]["MEMO"].ToString());
                    pp.Add("ID", dr["出库ID"].ToString());
                    app.LoadPlug("RepEdit.RepView", new object[] { "CLWZDBDYCS001", pp, false }, false);
                }
            }
            else
            {
                MessageBox.Show("请先保存数据！");
            }

        }
        private void dataGView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.label3.Text = "共:" + this.dataGView1.Rows.GetRowCount(DataGridViewElementStates.Visible) + "条"; ;
        }
    }
}
