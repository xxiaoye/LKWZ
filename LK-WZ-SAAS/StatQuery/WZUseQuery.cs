using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using YtWinContrl.com.datagrid;
using ChSys;
using YtUtil.tool;

namespace StatQuery
{
    public partial class WZUseQuery : Form,IPlug
    {
        public WZUseQuery()
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

        private void WZUseQuery_Load(object sender, EventArgs e)
        {
            this.selTextInpt_Ware.Sql = "WZUseRec2_Stockaschanger";//库房
            this.selTextInpt_Ware.SelParam = His.his.Choscode+"|{key}|{key}|{key}|{key}" ;

            this.selTextInpt_WZKind.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;//物质类别
            this.selTextInpt_WZKind.Sql = "FindWZKindWZXiaoQi";//与效期的数据库查询语句相同

            this.selTextInpt_WZName.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;//物质名称
            this.selTextInpt_WZName.Sql = "FindWZDictWZXiaoQi";//与效期的数据库查询语句相同

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


            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = 3;

            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.Column12);
            TvList.newBind().Load("Chang_LSDanWeiBianMa", new object[] { His.his.Choscode }).Bind(this.Column24);
            TvList.newBind().add("全部", "2").add("有效", "1").add("作废", "0").Bind(this.Column15);
            TvList.newBind().Load("Chang_KaiJiedankeshi", new object[] { His.his.Choscode }).Bind(this.Column26);
            TvList.newBind().Load("Chang_KaiJiedankeshi", new object[] { His.his.Choscode }).Bind(this.Column19);
       
        }
        void dateTimeDuan2_SelectChange(object sender, EventArgs e)
        {
            //button2_Click(null, null);
            this.dateTimePicker1 = this.dateTimeDuan1.Start;
            this.dateTimePicker2 = this.dateTimeDuan1.End;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGView1.ClearData();//1127
            SqlStr sql = new SqlStr();
            TimeSpan ts = new TimeSpan(183, 0, 0, 0);
            if (this.dateTimePicker2.Value.Date - this.dateTimePicker1.Value.Date > ts)
            {
                this.dateTimePicker1.Focus();
                WJs.alert("查询的日期相隔不能超过6个月！");
                return;
            }
            if (this.selTextInpt_Ware.Value == null)
            {
                sql.Add("and c.IFRDWARE=1");
            }
            else
            {
                sql.Add("and a.WARECODE=?", this.selTextInpt_Ware.Value);
            }

            if (this.selTextInpt_WZKind.Value != null)
            {
                sql.Add("and d.KINDCODE=?", this.selTextInpt_WZKind.Value);
            }


            if (this.selTextInpt_WZName.Value != null)
            {
                sql.Add("and d.WZID=?", this.selTextInpt_WZName.Value);
            }

            if (this.selTextInpt_supply.Value != null)
            {
                sql.Add("and a.SUPPLYID2=?", this.selTextInpt_supply.Value);
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
                sql.Add("and a.USERID=?", this.selTextInpt_Maker.Value);
            }
            //if (this.selTextInpt_SHer.Value != null)
            //{
            //    sql.Add("and d.SHUSERID=?", this.selTextInpt_SHer.Value);
            //}
            //if (this.selTextInpt_Surer.Value != null)
            //{
            //    sql.Add("and d.SHINUSERID=?", this.selTextInpt_Surer.Value);
            //}
          
           

            //添加查询条件及其参数
            sql.Add("and USEDATE >= ?", this.dateTimePicker1.Value);

            sql.Add("and USEDATE <= ?", this.dateTimePicker2.Value);


            //加载查询数据
            this.dataGView1.Url = "FindWZUseRecInfoaschanger";
            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sql);
            this.dataGView1.setFocus(0, 1);
            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "条";
           // this.JinEHeJi.Text = this.dataGView1.Sum("零售金额").ToString() + "元";
            if (this.dataGView1.RowCount != 0)
            {
                decimal sum=0;
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {
                    if (this.dataGView1["Column15", i].Value.ToString() =="1")
                    {
                        sum +=Convert.ToDecimal(this.dataGView1["Column28", i].Value);
                    }
                   
                }
                this.JinEHeJi.Text = sum.ToString() + "元";
            }
       
        }
    }
}
