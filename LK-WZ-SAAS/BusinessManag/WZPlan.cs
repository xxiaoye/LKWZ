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
    public partial class WZPlan : Form, IPlug
    {
        public WZPlan()
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
        private void WZPlan_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            this.dataGView1.Url = "WZPlanMainSearch";

            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            TvList.newBind().add("待审核", "1").add("已删除", "0").add("已审核", "2").add("已入库", "6").Bind(this.Column7);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column8);
           
            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.Column2);
            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.Column3);
           // this.dataGView2.Url = "WZPlanDetailSearch";

            //this.dataGView1.reLoad(new object[] { His.his.Choscode });
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
        private void Search_button_Click(object sender, EventArgs e)
        {
            if (this.InWare_selTextInpt.Value == null)
            {
                WJs.alert("请选择库房！");
                return;
            }

            this.dataGView1.reLoad(new object[] { His.his.Choscode, this.InWare_selTextInpt.Value, this.dateTimePicker1.Value, this.dateTimePicker2.Value });
           
            if (this.dataGView1.RowCount > 0)
            {
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {
                    if (this.dataGView1["Column1", i].Value != null)
                    {
                        string string1 = LData.Es("WZPlanMixiBishu", "LKWZ", new object[] { this.dataGView1["Column1", i].Value, His.his.Choscode });
                        this.dataGView1["mxbs", i].Value = string1;
                    }



                }
            }
            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔";
            this.JinEHeJi.Text = this.dataGView1.Sum("零售总金额").ToString() + "元";
            this.RuKuJinEHeJi.Text = this.dataGView1.Sum("总金额").ToString() + "元";
        }

      
        private void Search_button_Click_1(object sender, EventArgs e)
        {
            Search_button_Click(null, null);
        }

        private void add_toolStripButton_Click(object sender, EventArgs e)
        {
            if (this.InWare_selTextInpt.Value == null)
            {
                WJs.alert("请选择库房！");
                return;
            }
            PlanForm form = new PlanForm(this.InWare_selTextInpt.Value, this.InWare_selTextInpt.Text,app);
            form.ShowDialog();
            //this.InStatus_ytComboBox.SelectedIndex = 0;
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

                    PlanForm form = new PlanForm(dr, 2, this.InWare_selTextInpt.Value, this.InWare_selTextInpt.Text,app);//编辑
                    form.ShowDialog();
                    //this.InStatus_ytComboBox.SelectedIndex = 0;
                    Search_button_Click(null, null);
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该该采购计划已作废，不能再修改！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该该采购计划已审核，不能再修改！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该该采购计划已入库，不能再修改！");
                }
            }
            else
            {
                WJs.alert("请选择要修改的采购计划信息！");
            }
        }

        private void scan_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                PlanForm form = new PlanForm(dr, 3, this.InWare_selTextInpt.Value, this.InWare_selTextInpt.Text,app);//浏览
                form.ShowDialog();
                Search_button_Click(null, null);
            }
            else
            {
                WJs.alert("请选择要浏览的采购计划信息！");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
        }
        private void DeleButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();

            if (dr != null)
            {

                if (dr["状态"].ToInt() == 1)
                {
                    ActionLoad ac = new ActionLoad();
                    ac.Action = "LKWZSVR.lkwz.WZPlan.WZPlanDan";
                    ac.Sql = "PlanDanDelete";
                    ac.Add("PLANID", dr["采购计划id"].ToString());
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                    Search_button_Click(null, null);
                    // WJs.alert("删除采购计划id=" + dr["采购计划id"].ToString() + "成功！");
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该计划已删除，不能再次删除！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该计划已审核，不能再删除！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该计划已入库，不能再删除！");
                }
            }
            else
            {
                WJs.alert("请选择要删除的采购计划信息！");
            }
        }

        private void check_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToInt() == 1)
                {
                    ActionLoad ac = new ActionLoad();
                    ac.Action = "LKWZSVR.lkwz.WZPlan.WZPlanDan";
                    ac.Sql = "PlanDanUpdate";
                    ac.Add("SHDATE", DateTime.Now);
                    ac.Add("STATUS", 2);
                    ac.Add("PLANID", dr["采购计划id"].ToString());
                    ac.Add("SHUSERID", His.his.UserId.ToString());
                    ac.Add("SHUSERNAME", His.his.UserName);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                    // WJs.alert("采购计划id=" + dr["采购计划id"].ToString() + "审核成功！");
                    Search_button_Click(null, null);
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该计划已删除，不能审核！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该计划已审核，不需要再次审核！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该计划已入库，不需要再审核！");
                }
            }
            else
            {
                WJs.alert("请选择要审核的采购计划信息！");
            }
        }
    }
}
