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

namespace StatQuery
{
    public partial class WZInQuery : Form, IPlug
    {
        public WZInQuery()
        {
            InitializeComponent();
        }
        #region IPlug 成员

        public Form getMainForm()
        {
            return this;
        }
        private void init()
        {

        }
        public void initPlug(IAppContent app, object[] param)
        {

        }

        public bool unLoad()
        {
            return true;
        }
      //  private Panel[] plis = null;

        #endregion
        private void WZInQuery_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.Column26);
            this.selTextInpt_WZKind.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;//物质类别
            this.selTextInpt_WZKind.Sql = "FindWZKindWZXiaoQi";//与效期的数据库查询语句相同
            
            this.selTextInpt_WZName.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;//物质名称
            this.selTextInpt_WZName.Sql = "FindWZDictWZXiaoQi";//与效期的数据库查询语句相同


            this.selTextInpt_Ware.Sql = "StatQuery_GetInWareaschanger";//库房
            this.selTextInpt_Ware.SelParam = His.his.Choscode+ "|{key}|{key}|{key}|{key}" ;

            this.selTextInpt_Factor.Sql = "StatQuery_GetFactor";//生产商
            this.selTextInpt_Factor.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;
            
            this.selTextInpt_supply.Sql = "StatQuery_GetSupplyer";//供应商
            this.selTextInpt_supply.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;

            this.selTextInpt_Maker.Sql = "StatQuery_GetMaker";//制单人
            this.selTextInpt_Maker.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;

            this.selTextInpt_SHer.Sql = "StatQuery_GetSHer";//审核人
            this.selTextInpt_SHer.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;

            this.selTextInpt_Surer.Sql = "StatQuery_GetSurer";//确认人
            this.selTextInpt_Surer.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;



            //this.dataGView1.Url = "StatQuery_WZInMainSearchaschanger";
            this.dataGView2.Url = "StatQuery_WZInDetailSearchaschanger";
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
        
        private void Search_button_Click(object sender, EventArgs e)
        {
            SqlStr sql = new SqlStr();
            TimeSpan ts=new TimeSpan(183,0,0,0);
            if (this.dateTimePicker2.Value.Date - this.dateTimePicker1.Value.Date > ts)
            {
                this.dateTimePicker1.Focus();
                WJs.alert("查询的日期相隔不能超过6个月！");
            
            }
            if (this.selTextInpt_Ware.Value == null)
            {
                sql.Add("and c.IFSTWARE=1");
            }
            else
            {
                sql.Add("and b. WARECODE=?", this.selTextInpt_Ware.Value);
            }

            if (this.selTextInpt_WZKind.Value != null)
            {
                sql.Add("and d.KINDCODE=?",this.selTextInpt_WZKind.Value);
            }
           

            if (this.selTextInpt_WZName.Value != null)
            {
                sql.Add("and a.WZID=?",this.selTextInpt_WZName.Value);
            }

            if (!this.selTextInpt_Factor.Text.Equals(""))
            {
                sql.Add("and  a.SUPPLYNAME like ?", "%" + this.selTextInpt_Factor.Text.Trim().ToString() + "%");
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
                sql.Add("and b.SHINUSERID=?", this.selTextInpt_Surer.Value);
            }
          
           
            this.dataGView2.reLoad(new object[] { His.his.Choscode, this.dateTimePicker1.Value, this.dateTimePicker2.Value }, sql);
            this.TiaoSu.Text = this.dataGView2.RowCount.ToString() + "笔";
            this.JinEHeJi.Text = this.dataGView2.Sum("金额").ToString() + "元";
            this.RuKuJinEHeJi.Text = this.dataGView2.Sum("零售金额").ToString() + "元";


               
        }
        

    }
}
