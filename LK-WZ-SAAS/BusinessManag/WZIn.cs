using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using YtUtil.tool;
using YtWinContrl.com.datagrid;
using ChSys;
using BusinessManag.form2;
using YiTian.db;
using YtClient;
using YtWinContrl.com;

namespace BusinessManag
{
    public partial class WZIn : Form, IPlug
    {
       
        public WZIn()
        {
            InitializeComponent();
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
        //  private Panel[] plis = null;

        #endregion
        private void WZIn_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            this.dataGView1.Url = "WZInMainSearch";



            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            //this.dateTimeDuan1.SelectedIndex=3;
            TvList.newBind().add("等待审核", "1").add("作废", "0").add("已审核", "2").add("已入库", "6").Bind(this.Column7);
            TvList.newBind().add("等待审核", "1").add("作废", "0").add("已审核", "2").add("已入库", "6").Bind(this.InStatus_ytComboBox);

            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.Column4);
         
            //this.dataGView2.Url = "WZInDetailSearch";
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.InWare_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
            this.InWare_selTextInpt.Sql = "GetInWare";
            this.dateTimeDuan1.SelectChange += new EventHandler(dateTimeDuan1_SelectChange);
            this.dataGView1.DoubleClick += new EventHandler(dataGView1_DoubleClick);
          
        }

        void dataGView1_DoubleClick(object sender, EventArgs e)
        {
            scan_toolStripButton_Click(null, null);
        }
        void dateTimeDuan1_SelectChange(object sender, EventArgs e)
        {
            Search_button_Click(null, null);
        }
        

        private void add_toolStripButton_Click(object sender, EventArgs e)
        {
            if (this.InWare_selTextInpt.Value == null)
            {
                WJs.alert("请选择入库库房！");
                return;
            }
            InWare form = new InWare(this.InWare_selTextInpt.Value,this.InWare_selTextInpt.Text,app);
            form.ShowDialog();
            this.InStatus_ytComboBox.SelectedIndex = 0;
            //this.InWare_selTextInpt.Value=
            Search_button_Click(null, null);
        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            if (this.InWare_selTextInpt.Value == null)
            {
                WJs.alert("请选择入库库房！");
                return;
            }
            if (this.InStatus_ytComboBox.Text.Trim().Length == 0)
            {
                WJs.alert("请选择入库状态！");
                return;
            }
           

            this.dataGView1.reLoad(new object[] { His.his.Choscode, TvList.getValue(this.InStatus_ytComboBox).ToInt(),this.InWare_selTextInpt.Value, this.dateTimePicker1.Value, this.dateTimePicker2.Value });

            if (this.dataGView1.RowCount > 0)
            {
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {
                    if (this.dataGView1["Column1", i].Value != null)
                    {
                        string string1 = LData.Es("WZRuKuMixiBishu", "LKWZ", new object[] { this.dataGView1["Column1", i].Value, His.his.Choscode });
                        this.dataGView1["mxbs", i].Value = string1;
                    }



                }
                
            }
            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔";
            this.JinEHeJi.Text = this.dataGView1.Sum("总金额").ToString() + "元";
            this.RuKuJinEHeJi.Text = this.dataGView1.Sum("零售总金额").ToString() + "元";
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToInt() == 1)
                {
                   
                    InWare form = new InWare(dr,2,app);//编辑
                    form.ShowDialog();
                    this.InStatus_ytComboBox.SelectedIndex = 0;
                    Search_button_Click(null, null);
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该入库信息已作废，不能再修改！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该入库信息已审核，不能再修改！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该入库信息已入库，不能再修改！");
                }
            }
            else
            {
                WJs.alert("请选择要修改的入库信息！");
            }
        }

        private void scan_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                InWare form = new InWare(dr, 3,app);//浏览
                form.ShowDialog();
                //this.InStatus_ytComboBox.SelectedIndex = 0;
                Search_button_Click(null, null);
            }
            else
            {
                WJs.alert("请选择要浏览的入库信息！");
            }
        }

        private void DeleButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();

            if (dr != null)
            {

                if (dr["状态"].ToInt() == 1)
                {

                    if (WJs.confirmFb("您确定要删除入库信息id=" + dr["入库ID"].ToString() + "吗？"))
                    {
                        ActionLoad ac = new ActionLoad();
                        ac.Action = "LKWZSVR.lkwz.WZIn.WZInDan";
                        ac.Sql = "RuKuDanDelete";
                        ac.Add("INID", dr["入库ID"].ToString());
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                       // this.InStatus_ytComboBox.SelectedIndex = 1;
                        Search_button_Click(null, null);
                       // WJs.alert("删除入库信息id=" + dr["入库ID"].ToString() + "成功！");
                    }

                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该入库信息已作废，不能再删除！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该入库信息已审核，不能再删除！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该入库信息已入库，不能再删除！");
                }
            }
            else
            {
                WJs.alert("请选择要删除的入库信息！");
            }
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
        }
        void ac0_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
           // WJs.alert(e.Msg.Msg);
           
        }
        void ac2_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            
             //更新入库细表的库存流水号
            string flowno = e.Msg.Msg.Split('+')[0];
            string DETAILNO = e.Msg.Msg.Split('+')[1];
            ActionLoad ac2 = new ActionLoad();
            ac2.Action = "LKWZSVR.lkwz.WZIn.WZInDan";
            ac2.Sql = "UpdateWZInDetailInfo";
            ac2.Add("DETAILNO", DETAILNO);
            ac2.Add("STOCKFLOWNO", flowno);
            ac2.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac0_ServiceLoad);
            ac2.Post();
        }

        private void check_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
          //  List<Dictionary<string, ObjItem>> list = this.dataGView2.GetData();
            if (dr != null)
            {

                if (dr["状态"].ToInt() == 1)
                {
                   
                    ActionLoad ac = new ActionLoad();
                    ac.Action = "LKWZSVR.lkwz.WZIn.WZInDan";
                    ac.Sql = "RuKuDanUpdate";
                    ac.Add("SHDATE", DateTime.Now);
                    ac.Add("STATUS", 2);
                    ac.Add("INID", dr["入库ID"].ToString());
                    ac.Add("SHUSERID", His.his.UserId.ToString());
                    ac.Add("SHUSERNAME", His.his.UserName);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                   // this.InStatus_ytComboBox.SelectedIndex = 2;
                    //WJs.alert("入库信息id=" + dr["入库ID"].ToString() + "审核成功！");
                    Search_button_Click(null, null);
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该入库信息已作废，不能再审核！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该入库信息已审核，不能再次审核！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该入库信息已入库，不能再审核！");
                }
            }
            else
            {
                WJs.alert("请选择要审核的入库信息！");
            }
        }

        private void UnCheck_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {

                if (dr["状态"].ToInt() == 2)
                {
                    if (WJs.confirmFb("您确定要取消审核入库信息id=" + dr["入库ID"].ToString() + "吗？"))
                    {
                        ActionLoad ac = new ActionLoad();
                        ac.Action = "LKWZSVR.lkwz.WZIn.WZInDan";
                        ac.Sql = "RuKuDanUpdate";
                        ac.Add("SHDATE", null);
                        ac.Add("STATUS", 1);
                        ac.Add("INID", dr["入库ID"].ToString());
                        ac.Add("SHUSERID", null);
                        ac.Add("SHUSERNAME", null);
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        //this.InStatus_ytComboBox.SelectedIndex = 0;
                        // WJs.alert("取消审核入库信息id=" + dr["入库ID"].ToString() + "成功！");
                        Search_button_Click(null, null);
                    }
                    
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该入库信息已作废，不能取消审核！");
                }
                if (dr["状态"].ToInt() == 1)
                {
                    WJs.alert("该入库信息还未审核，不需要取消审核！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该入库信息已入库，不能再取消审核！");
                }
            }
            else
            {
                WJs.alert("请选择要取消审核的入库信息！");
            }
        }

        private void InWare_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToInt() == 2)
                { 
                    //通过入库ID获取该入库信息的明细数据
                    DataTable datatable =  LData.LoadDataTable("WZInDetailSearch", new object[] { dr["入库ID"].ToString(), His.his.Choscode });
                    if (datatable != null)
                    {
                        foreach (DataRow r in datatable.Rows)
                        {
                           // tv.add(r[0].ToString(), r[1].ToString());
                            if (r != null)
                            {
                                //更新库存总表信息
                                decimal before_num =0;
                                decimal num = Convert.ToDecimal(r["NUM"]);
                                decimal after_num = 0;
                                //入库时生成流水细表的时候将采购单价和零售单价转化为最小单位时的价格
                                string jiage = r["PRICE"].ToString();
                                string lsjiage = r["LSPRICE"].ToString();
                               // decimal c = Convert.ToDecimal(LData.Es("GetCHANGERATE", null, new object[] { His.his.Choscode, dic["物资ID"].ToString() }));
                                if (!r["UNITCODE"].Equals(r["LSUNITCODE"]))
                                {
                                    //不是最小单位，需将入库数量化为最小单位的数量
                                    num = num * Convert.ToDecimal(r["CHANGERATE"]);
                                    jiage = MathUtil.round((Convert.ToDecimal(jiage) / Convert.ToDecimal(r["CHANGERATE"])).ToString(), 4);
                                    lsjiage = MathUtil.round((Convert.ToDecimal(lsjiage) / Convert.ToDecimal(r["CHANGERATE"])).ToString(), 4);
                                    
                                }
                                
                                ActionLoad ac0 = new ActionLoad();
                                ac0.Action = "LKWZSVR.lkwz.WZIn.WZInDan";
                                ac0.Add("WZID", r["WZID"].ToString());
                                DataTable stock_table = LData.LoadDataTable("GetSTOCKTable", new object[] { His.his.Choscode, r["WZID"].ToString(), dr["入库库房编码"].ToString() });
                                if (stock_table != null)
                                {
                                    //库存总表中存在该库房对应的这种物资，只需更新库存量
                                    before_num = Convert.ToDecimal(stock_table.Rows[0]["NUM"]);

                                    after_num = before_num + num;
                                    ac0.Sql = "UpdateWZStock";
                                    
                                    ac0.Add("STOCKID", stock_table.Rows[0]["STOCKID"]);
                                    ac0.Add("NUM", after_num);


                                }
                                else
                                {
                                    //新增库存总表记录
                                    ac0.Sql = "SaveWZStock";
                                    ac0.Add("WARECODE", dr["入库库房编码"].ToString());
                                    ac0.Add("NUM", num);
                                    ac0.Add("LSUNITCODE", r["LSUNITCODE"]);

                                    ac0.Add("NUMSX", Convert.ToDecimal(r["HIGHNUM"]) * Convert.ToDecimal(r["CHANGERATE"]));
                                    ac0.Add("NUMXX", Convert.ToDecimal(r["LOWNUM"])*Convert.ToDecimal(r["CHANGERATE"]));
                                    //ac0.Add("MEMO", r["LSUNITCODE"]);
                                    ac0.Add("CHOSCODE", His.his.Choscode);
                                }
                                ac0.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac0_ServiceLoad);
                                ac0.Post();
                                //LData.Exe("GetSTOCKID", null, new object[] { His.his.Choscode, r["WZID"].ToString(), dr["入库库房编码"].ToString() });
                                //string stockid_1 = LData.Es("GetSTOCKID", null, new object[] { His.his.Choscode, r["WZID"].ToString(), dr["入库库房编码"].ToString() });
                               // string dep =  r["DETAILNO"].ToString();

                                //更新库存流水表
                                ActionLoad ac1 = new ActionLoad();
                                ac1.Action = "LKWZSVR.lkwz.WZIn.WZInDan";
                                ac1.Sql = "SaveWZStockDetail";
                                ac1.Add("WZID", r["WZID"].ToString());
                                ac1.Add("INID", r["INID"].ToString());
                                ac1.Add("WARECODE", dr["入库库房编码"].ToString());
                                string stockid_s;
                               // string befornum = "0";
                                if (stock_table != null)
                                {
                                    stockid_s = stock_table.Rows[0]["STOCKID"].ToString();
                                   
                                }
                                else
                                {
                                    stockid_s = LData.Es("GetSTOCKId", "LKWZ", new object[] { His.his.Choscode, r["WZID"].ToString(), dr["入库库房编码"].ToString() });
                                }
                                ac1.Add("STOCKID", stockid_s);
                                ac1.Add("BEFORENUM", before_num);
                                ac1.Add("NUM", num);

                                ac1.Add("LSUNITCODE", r["LSUNITCODE"]);

                                ac1.Add("CHANGERATE", r["CHANGERATE"]);
                                ac1.Add("OUTNUM", 0);
                                //ac1.Add("PRICE", r["PRICE"]);
                                //ac1.Add("LSPRICE", r["LSPRICE"]);
                                ac1.Add("PRICE", jiage);
                                ac1.Add("LSPRICE",lsjiage);
                                ac1.Add("PRODUCTDATE",Convert.ToDateTime(r["PRODUCTDATE"]));
                                ac1.Add("VALIDDATE", Convert.ToDateTime(r["VALIDDATE"]));
                                ac1.Add("PH", r["PH"]);
                                ac1.Add("PZWH", r["PZWH"]);

                                ac1.Add("SUPPLYID", r["SUPPLYID"]);
                                ac1.Add("SUPPLYNAME", r["SUPPLYNAME"]);
                                ac1.Add("WSXKZH", r["WSXKZH"]);
                                ac1.Add("MEMO", r["MEMO"]);
                                ac1.Add("TXM", r["TXM"]);

                                ac1.Add("RECIPECODE", dr["单据号"].ToString());
                                ac1.Add("SHDH", dr["随货单号"].ToString());
                                ac1.Add("SUPPLYID2", dr["供货商ID"].ToString());
                                ac1.Add("SUPPLYNAME2", dr["供货商名称"].ToString());
                                ac1.Add("INDATE", DateTime.Now);
                                ac1.Add("CHOSCODE", His.his.Choscode);
                                ac1.Add("DETAILNO", r["DETAILNO"]);//用于更新入库细表

                                ac1.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac2_ServiceLoad);
                                ac1.Post();
                                ////更新入库细表的库存流水号
                                //ActionLoad ac2 = new ActionLoad();
                                //ac2.Action = "LKWZSVR.lkwz.WZIn.WZInDan";
                                //ac2.Sql = "UpdateWZInDetailInfo";
                                //ac2.Add("DETAILNO", r["DETAILNO"].ToString());
                                //ac2.Add("STOCKFLOWNO", r["FLOWNO"].ToString());
                                //if (dr["采购计划流水号"] != null)
                                //{
                                //    ActionLoad ac3 = new ActionLoad();
                                //    ac3.Action = "LKWZSVR.lkwz.WZPlan.WZPlanMain";
                                //    ac3.Sql = "Save";
                                //    ac3.Add("PLANID", dr["采购计划流水号"].ToString());
                                //    ac3.Add("STATUS", 6);
                                //    ac3.Add("SHINDATE", DateTime.Now);
                                //    ac3.Add("SHINUSERID", His.his.UserId);
                                //    ac3.Add("SHINUSERNAME", His.his.UserName);
                                //    ac3.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                                //    ac3.Post();
                                //}
                            }
                        }
                    }
                    if (dr["采购计划流水号"] != null)
                    {
                        ActionLoad ac3 = new ActionLoad();
                        ac3.Action = "LKWZSVR.lkwz.WZPlan.WZPlanDan";
                        ac3.Sql = "UpdateWZPlanDan";
                        ac3.Add("PLANID", dr["采购计划流水号"].ToString());
                        ac3.Add("STATUS", 6);
                        ac3.Add("SHINDATE", DateTime.Now);
                        ac3.Add("SHINUSERID", His.his.UserId);
                        ac3.Add("SHINUSERNAME", His.his.UserName);
                        ac3.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac0_ServiceLoad);
                        ac3.Post();

                    }

                    ActionLoad ac = new ActionLoad();
                    ac.Action = "LKWZSVR.lkwz.WZIn.WZInDan";
                    ac.Sql = "RuKuDanUpdate";
                    ac.Add("SHINDATE", DateTime.Now);
                    ac.Add("STATUS", 6);
                    ac.Add("INID", dr["入库ID"].ToString());
                    ac.Add("SHINUSERID", His.his.UserId);
                    ac.Add("SHINUSERNAME", His.his.UserName);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                    this.InStatus_ytComboBox.SelectedIndex = 3;
                   // WJs.alert("入库信息id=" + dr["入库ID"].ToString() + "入库成功！");
                    Search_button_Click(null, null);
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该入库信息已作废，不能入库！");
                }
                if (dr["状态"].ToInt() == 1)
                {
                    WJs.alert("该入库信息还未审核，请等审核后再入库！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该入库信息已入库，不能再次入库！");
                }
            }
            else
            {
                WJs.alert("请选择要入库的入库信息！");
            }
        }

        
       
    }
}
