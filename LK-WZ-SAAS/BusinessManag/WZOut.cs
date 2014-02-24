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

namespace BusinessManag
{
    public partial class WZOut : Form, IPlug
    {
        public WZOut()
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
        private void WZOut_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            this.dataGView1.Url = "WZOutMainSearch";



            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            //this.dateTimeDuan1.SelectedIndex=3;
            TvList.newBind().add("等待审核", "1").add("作废", "0").add("已审核", "2").add("已出库", "6").Bind(this.Column7);
            TvList.newBind().add("等待审核", "1").add("作废", "0").add("已审核", "2").add("已出库", "6").Bind(this.InStatus_ytComboBox);
          
            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.Column4);//1202
            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.Column47);
         
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.InWare_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
            this.InWare_selTextInpt.Sql = "GetOutWare";
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

        private void Search_button_Click(object sender, EventArgs e)
        {
            if (this.InWare_selTextInpt.Value == null)
            {
                WJs.alert("请选择出库库房！");
                return;
            }
            if (this.InStatus_ytComboBox.Text.Trim().Length == 0)
            {
                WJs.alert("请选择出库状态！");
                return;
            }


            this.dataGView1.reLoad(new object[] { His.his.Choscode, TvList.getValue(this.InStatus_ytComboBox).ToInt(), this.InWare_selTextInpt.Value, this.dateTimePicker1.Value, this.dateTimePicker2.Value });
            if (this.dataGView1.RowCount > 0)
            {
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {
                    if (this.dataGView1["Column1", i].Value != null)
                    {
                        string string1 = LData.Es("WZChuKuMixiBishu", "LKWZ", new object[] { this.dataGView1["Column1", i].Value, His.his.Choscode });
                        this.dataGView1["mxbs", i].Value = string1;
                    }



                }
               
            }
            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔";
            this.JinEHeJi.Text = this.dataGView1.Sum("总金额").ToString() + "元";
            this.RuKuJinEHeJi.Text = this.dataGView1.Sum("零售总金额").ToString() + "元";

        }

        private void add_toolStripButton_Click(object sender, EventArgs e)
        {
            if (this.InWare_selTextInpt.Value == null)
            {
                WJs.alert("请选择出库库房！");
                return;
            }
            OutWare form = new OutWare(this.InWare_selTextInpt.Value, this.InWare_selTextInpt.Text,app);
            form.ShowDialog();
            this.InStatus_ytComboBox.SelectedIndex = 0;
            //this.InWare_selTextInpt.Value=
            Search_button_Click(null, null);
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToInt() == 1)
                {

                    OutWare form = new OutWare(this.InWare_selTextInpt.Value, this.InWare_selTextInpt.Text,dr, 2,app);//编辑
                    form.ShowDialog();
                    this.InStatus_ytComboBox.SelectedIndex = 0;
                    Search_button_Click(null, null);
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该出库信息已作废，不能再修改！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该出库信息已审核，不能再修改！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该出库信息已出库，不能再修改！");
                }
            }
            else
            {
                WJs.alert("请选择要修改的出库信息！");
            }
        }

        private void scan_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                OutWare form = new OutWare(this.InWare_selTextInpt.Value, this.InWare_selTextInpt.Text, dr, 3,app);//浏览
                form.ShowDialog();
                //this.InStatus_ytComboBox.SelectedIndex = 0;
                Search_button_Click(null, null);
            }
            else
            {
                WJs.alert("请选择要浏览的出库信息！");
            }
        }

        private void DeleButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();

            if (dr != null)
            {

                if (dr["状态"].ToInt() == 1)
                {

                    if (WJs.confirmFb("您确定要删除出库信息id=" + dr["出库ID"].ToString() + "吗？"))
                    {
                        ActionLoad ac = new ActionLoad();
                        ac.Action = "LKWZSVR.lkwz.WZOut.WZOutDan";
                        ac.Sql = "ChuKuDanDelete";
                        ac.Add("OUTID", dr["出库ID"].ToString());
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                       // this.InStatus_ytComboBox.SelectedIndex = 1;
                        Search_button_Click(null, null);
                       // WJs.alert("删除出库信息id=" + dr["出库ID"].ToString() + "成功！");
                    }

                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该出库信息已作废，不能再删除！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该出库信息已审核，不能再删除！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该出库信息已出库，不能再删除！");
                }
            }
            else
            {
                WJs.alert("请选择要删除的出库信息！");
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
        private void check_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
           
            if (dr != null)
            {

                if (dr["状态"].ToInt() == 1)
                {
                    

                    ActionLoad ac = new ActionLoad();
                    ac.Action = "LKWZSVR.lkwz.WZOut.WZOutDan";
                    ac.Sql = "ChuKuDanUpdate";
                    ac.Add("SHDATE", DateTime.Now);
                    ac.Add("STATUS", 2);
                    ac.Add("OUTID", dr["出库ID"].ToString());
                    ac.Add("SHUSERID", His.his.UserId.ToString());
                    ac.Add("SHUSERNAME", His.his.UserName);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                    //this.InStatus_ytComboBox.SelectedIndex = 2;
                   // WJs.alert("出库信息id=" + dr["出库ID"].ToString() + "审核成功！");
                    Search_button_Click(null, null);
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该出库信息已作废，不能再审核！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该出库信息已审核，不能再次审核！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该出库信息已入库，不能再审核！");
                }
            }
            else
            {
                WJs.alert("请选择要审核的出库信息！");
            }
        }

        private void UnCheck_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();

            if (dr != null)
            {

                if (dr["状态"].ToInt() == 2)
                {


                    ActionLoad ac = new ActionLoad();
                    ac.Action = "LKWZSVR.lkwz.WZOut.WZOutDan";
                    ac.Sql = "ChuKuDanUpdate";
                    ac.Add("SHDATE", DateTime.Now);
                    ac.Add("STATUS", 1);
                    ac.Add("OUTID", dr["出库ID"].ToString());
                    ac.Add("SHUSERID", His.his.UserId.ToString());
                    ac.Add("SHUSERNAME", His.his.UserName);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                    //this.InStatus_ytComboBox.SelectedIndex = 2;
                    // WJs.alert("出库信息id=" + dr["出库ID"].ToString() + "审核成功！");
                    Search_button_Click(null, null);
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该出库信息已作废，不能再取消审核！");
                }
                if (dr["状态"].ToInt() == 1)
                {
                    WJs.alert("该出库信息未审核，不需要取消审核！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该出库信息已入库，不能再取消审核！");
                }
            }
            else
            {
                WJs.alert("请选择要取消审核的出库信息！");
            }
        }

        private void OutWare_toolStripButton_Click(object sender, EventArgs e)
        {
             Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
             if (dr != null)
             {
                 if (dr["状态"].ToInt() == 0)
                 {
                     WJs.alert("该出库信息已作废，不能出库！");
                 }
                 if (dr["状态"].ToInt() == 1)
                 {
                     WJs.alert("该出库信息还未审核，请等审核后再出库！");
                 }
                 if (dr["状态"].ToInt() == 6)
                 {
                     WJs.alert("该出库信息已出库，不能再次出库！");
                 }
                 if (dr["状态"].ToInt() == 2)
                 {
                     //通过出库ID获取该出库信息的明细数据
                    DataTable datatable =  LData.LoadDataTable("WZOutDetailSearch", new object[] { dr["出库ID"].ToString(), His.his.Choscode });
                    if (datatable != null)
                    {
                        foreach (DataRow r in datatable.Rows)
                        {
                            if (r != null)
                            {
                                //更新库存总表信息
                                decimal before_num = 0;
                                decimal num = Convert.ToDecimal(r["NUM"]);
                                decimal after_num = 0;
                                if (!r["UNITCODE"].Equals(r["LSUNITCODE"]))
                                {
                                    //不是最小单位，需将出库数量化为最小单位的数量
                                    num = num * Convert.ToDecimal(r["CHANGERATE"]);
                                    //jiage = MathUtil.round((Convert.ToDecimal(jiage) / Convert.ToDecimal(r["CHANGERATE"])).ToString(), 4);
                                    //lsjiage = MathUtil.round((Convert.ToDecimal(lsjiage) / Convert.ToDecimal(r["CHANGERATE"])).ToString(), 4);

                                }
                                ActionLoad ac0 = new ActionLoad();
                                ac0.Action = "LKWZSVR.lkwz.WZOut.WZOutDan";
                                ac0.Add("WZID", r["WZID"].ToString());
                                DataTable stock_table = LData.LoadDataTable("GetSTOCKTable", new object[] { His.his.Choscode, r["WZID"].ToString(), dr["出库库房编码"].ToString() });
                                before_num = Convert.ToDecimal(stock_table.Rows[0]["NUM"]);

                                after_num = before_num - num;
                                ac0.Sql = "UpdateWZStock";

                                ac0.Add("STOCKID", stock_table.Rows[0]["STOCKID"]);
                                ac0.Add("NUM", after_num);
                                ac0.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac0_ServiceLoad);
                                ac0.Post();

                                //更新库存流水表
                                ActionLoad ac1 = new ActionLoad();
                                ac1.Action = "LKWZSVR.lkwz.WZOut.WZOutDan";
                                ac1.Sql = "UpdateWZStockDetail";
                                ac1.Add("FLOWNO", r["STOCKFLOWNO"].ToString());
                                string before_outnum = LData.Es("GetOutNUM", "LKWZ", new object[] { r["STOCKFLOWNO"].ToString() });
                                decimal after_outnum = num + Convert.ToDecimal(before_outnum);
                                ac1.Add("OUTNUM", after_outnum);
                                ac1.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac0_ServiceLoad);
                                ac1.Post();
                            }
                        }
                        ActionLoad ac = new ActionLoad();
                        ac.Action = "LKWZSVR.lkwz.WZOut.WZOutDan";
                        ac.Sql = "ChuKuDanUpdate";
                        ac.Add("SHOUTDATE", DateTime.Now);
                        ac.Add("STATUS", 6);
                        ac.Add("OUTID", dr["出库ID"].ToString());
                        ac.Add("SHOUTUSERID", His.his.UserId);
                        ac.Add("SHOUTUSERNAME", His.his.UserName);
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        //this.InStatus_ytComboBox.SelectedIndex = 3;
                        //WJs.alert("出库信息id=" + dr["出库ID"].ToString() + "出库成功！");
                        Search_button_Click(null, null);
                    }
                 }
             }
             else
             {
                 WJs.alert("请选择要出库的出库信息！");
             }
        }
       
    }
}
