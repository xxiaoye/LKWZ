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
using YiTian.db;
using YtUtil.tool;
using YtClient;
using BusinessManag.form;
namespace StatQuery
{
    public partial class WZDiaoBoQuery : Form, IPlug
    {
        private BusinessManag.form.DiaoBoEdit editForm = null;
        public WZDiaoBoQuery()
        {
            InitializeComponent();
        }
      //  #region IPlug 成员

      //  public Form getMainForm()
      //  {
      //      return this;
      //  }
      //  private void init()
      //  {

      //  }
      //  public void initPlug(IAppContent app, object[] param)
      //  {

      //  }

      //  public bool unLoad()
      //  {
      //      return true;
      //  }
      ////  private Panel[] plis = null;

      //  #endregion

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
        
        private void Search_button_Click(object sender, EventArgs e)
        {

              SqlStr sql = new SqlStr();
              TimeSpan ts = new TimeSpan(183, 0, 0, 0);
              if (this.dateTimePicker2.Value.Date - this.dateTimePicker1.Value.Date > ts)
              {
                  this.dateTimePicker1.Focus();
                  WJs.alert("查询的日期相隔不能超过6个月！");
                  return;
              }
              if (this.selTextInpt_Ware.Value != null)
              {

                  sql.Add("and b. WARECODE=?", this.selTextInpt_Ware.Value);
              }

              if (this.selTextInpt_WZKind.Value != null)
              {
                  sql.Add("and d.KINDCODE=?", this.selTextInpt_WZKind.Value);
              }


              if (this.selTextInpt_WZName.Value != null)
              {
                  sql.Add("and a.WZID=?", this.selTextInpt_WZName.Value);
              }

              if (this.selTextInpt_supply.Value != null)
              {
                  sql.Add("and sd.SUPPLYID2=?",this.selTextInpt_supply.Value);
              }

              //if (!this.selTextInpt_Factor.Text.Equals(""))
              //{
              //    sql.Add("and a.SUPPLYNAME=?", this.selTextInpt_Factor.Text.Trim().ToString());
              //}
              //else
              //{
              //    if (this.selTextInpt_Factor.Value != null)
              //    {
              //        sql.Add("and a.SUPPLYID=?", this.selTextInpt_Factor.Value);
              //    }
              //}

              if (!this.selTextInpt_Factor.Text.Equals(""))
              {
                  sql.Add("and  a.SUPPLYNAME like ?", "%" + this.selTextInpt_Factor.Text.Trim().ToString() + "%");
                  // sql.Add("and a.SUPPLYNAME=?", this.selTextInpt_Factor.Text.Trim().ToString());
              }
              else
              {
                  if (this.selTextInpt_Factor.Value != null)
                  {
                      sql.Add("and a.SUPPLYID=?", this.selTextInpt_Factor.Value);
                  }
              }
              if (this.selTextInpt_Maker.Value != null)
              {
                  sql.Add("and b.USERID=?", this.selTextInpt_Maker.Value);
              }
              if (this.selTextInpt_SHer.Value != null)
              {
                  sql.Add("and b.SHUSERID=?", this.selTextInpt_SHer.Value);
              }
              if (this.selTextInpt_Surer.Value != null)
              {
                  sql.Add("and b.SHOUTUSERID=?", this.selTextInpt_Surer.Value);
              }

              if (this.selTextInpt_GosWare.Value != null)
              {
                  sql.Add("and b.TARGETWARECODE=?", this.selTextInpt_GosWare.Value);
              }

              this.dataGView2.reLoad(new object[] { His.his.Choscode, this.dateTimePicker1.Value, this.dateTimePicker2.Value }, sql);
              this.TiaoSu.Text = this.dataGView2.RowCount.ToString()+"笔";
              this.JinEHeJi.Text = this.dataGView2.Sum("金额").ToString()+"元";
              this.RuKuJinEHeJi.Text = this.dataGView2.Sum("入库金额").ToString() + "元";
            //  this.dataGView1.reLoad(new object[] { His.his.Choscode, this.selTextInpt_Ware.Value, this.dateTimePicker1.Value, this.dateTimePicker2.Value }, sql);
                           
        }

        private void WZDiaoBoQuery_Load(object sender, EventArgs e)
        {
           
            WJs.SetDictTimeOut();
            this.selTextInpt_WZKind.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;//物质类别
            this.selTextInpt_WZKind.Sql = "Query_FindWZKindWZXiaoQi";//与效期的数据库查询语句相同

            this.selTextInpt_WZName.SelParam = "{key}|{key}|{key}|{key}|{key}|{key}|" + His.his.Choscode;//物质名称
            this.selTextInpt_WZName.Sql = "Query_FindWZDictWZXiaoQi";//与效期的数据库查询语句相同


            this.selTextInpt_Ware.Sql = "Query_GetOutWare"; //"StatQuery_GetInWareaschanger";//库房
            this.selTextInpt_Ware.SelParam = His.his.Choscode+"|{key}|{key}|{key}|{key}" ;

            this.selTextInpt_Factor.Sql = "Query_GetFactor";//生产商
            this.selTextInpt_Factor.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;
            
            this.selTextInpt_supply.Sql = "Query_GetSupplyer";//供应商
            this.selTextInpt_supply.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;

            this.selTextInpt_Maker.Sql = "Query_GetMaker";//制单人
            this.selTextInpt_Maker.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;

            this.selTextInpt_SHer.Sql = "Query_GetSHer";//审核人
            this.selTextInpt_SHer.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;

            this.selTextInpt_Surer.Sql = "Query_GetSurer";//确认人
            this.selTextInpt_Surer.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;


            this.selTextInpt_GosWare.Sql = "Query_GetOutWare";
            this.selTextInpt_GosWare.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";

            //this.dataGView1.Url = "StatQuery_WZInMainSearchaschanger";
            //this.dataGView1.Url =  "DbEditFindOutDetailListaschanger";
            this.dataGView2.Url = "Query_DbEditFindOutDetailListaschanger";
            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
           
            //TvList.newBind().add("等待审核", "1").add("作废", "0").add("已审核", "2").add("已入库", "6").Bind(this.Column7);


            this.dateTimeDuan1.SelectChange += new EventHandler(dateTimeDuan1_SelectChange);
            //this.dataGView1.SelectionChanged += new EventHandler(dataGView1_SelectionChanged);
          
        
        }

        void dateTimeDuan1_SelectChange(object sender, EventArgs e)
        {
            Search_button_Click(null, null);
        }

        private void dataGView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGView2.CurrentRow == null)
            {
                return;
            }
            ActionLoad ld = ActionLoad.Conn();
            DataTable tb = ld.Find("DbQueryFindWzOutMainById", new object[] { this.dataGView2.CurrentRow.Cells["Column24"].Value });

            if (tb != null && tb.Rows.Count > 0)
            {
                Dictionary<string, string> info = new Dictionary<string, string>() { };
                info.Add("MuDiKuFangCode", tb.Rows[0]["MuDiKuFangCode"].ToString());
                info.Add("MuDiKuFangName", tb.Rows[0]["MuDiKuFangName"].ToString());
                info.Add("ChuKuKuFangCode", tb.Rows[0]["ChuKuKuFangCode"].ToString());
                info.Add("ChuKuKuFangName", tb.Rows[0]["ChuKuKuFangName"].ToString());
                info.Add("ChuKuFangShiCode", tb.Rows[0]["ChuKuFangShiCode"].ToString());
                info.Add("ChuKuFangShiName", tb.Rows[0]["ChuKuFangShiName"].ToString());
                info.Add("BeiZhu", tb.Rows[0]["BeiZhu"].ToString());
                info.Add("ChuKuZongJinE", tb.Rows[0]["ChuKuZongJinE"].ToString());
                info.Add("RuKuZongJinE", tb.Rows[0]["RuKuZongJinE"].ToString());
                info.Add("DanJuHao", tb.Rows[0]["DanJuHao"].ToString());
                info.Add("outID", this.dataGView2.CurrentRow.Cells["Column24"].Value.ToString());
                info.Add("outDate", tb.Rows[0]["outDate"].ToString());
                this.editForm = new BusinessManag.form.DiaoBoEdit(info, 0, app);
                this.editForm.ShowDialog();
            }
        }

    }
}
