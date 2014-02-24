using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChSys;
using YtWinContrl.com.datagrid;
using YtUtil.tool;
using YtPlugin;
using YtClient;

namespace StatQuery
{
    public partial class WZStockQuery : Form,IPlug
    {
        public WZStockQuery()
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

        private void WZStockQuery_Load(object sender, EventArgs e)
        {

            this.selTextInpt1.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;//为什么要这样写？
            selTextInpt1.BxSr = false;//必须输入查询关键字
            this.selTextInpt1.Sql = "FindWZKindWZXiaoQi";//与效期的数据库查询语句相同
           // this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt2.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;
            selTextInpt1.BxSr = false;//必须输入查询关键字
            this.selTextInpt2.Sql = "FindWZDictWZXiaoQi";//与效期的数据库查询语句相同
            //this.selTextInpt2.textBox1.ReadOnly = true;

            this.selTextInpt_Ware.Sql = "LKWZ_GetWare_ye";
            this.selTextInpt_Ware.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.main_unicode);
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.xi_lsunicode);
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.xi_unicode);
            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.warename);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGView_Main.ClearData();//1127
            this.dataGView_xi.ClearData();
            this.dataGView_Main.Url = "WZStockQuery_SearchStock";
            SqlStr sql = SqlStr.newSql();
            if (this.selTextInpt_Ware.Value== null)
            {
                WJs.alert("请选择库房！");
                this.selTextInpt_Ware.Focus();
                return;
            }


       
            if (this.selTextInpt1.Value != null)
            {
                sql.Add(" and e.KINDCODE =? ", this.selTextInpt1.Value);
            }
            if (this.selTextInpt2.Value != null)
            {
                sql.Add(" and c.WZID=? ", this.selTextInpt2.Value);
            }



            this.dataGView_Main.reLoad(new object[] { this.selTextInpt_Ware.Value, His.his.Choscode }, sql);
            this.TiaoSu.Text = this.dataGView_Main.RowCount.ToString() + "笔"; 
        }

        private void dataGView_Main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            DataRow r1 = this.dataGView_Main.GetRowData();
            this.dataGView_xi.Url = "WZStockAlarmStockDetailInfo";
            this.dataGView_xi.reLoad(new object[] { r1["STOCKID"], His.his.Choscode });

            if (this.dataGView_xi.RowCount > 0)
            {
                for (int i = 0; i < this.dataGView_xi.RowCount; i++)
                {

                    if (this.dataGView_xi["bzxs", i].Value != null && this.dataGView_xi["xi_lsunicode", i].Value != null && this.dataGView_xi["xi_unicode", i].Value != null)
                    {

                       // string string1 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView_xi["xi_unicode", i].Value });
                      //  string string2 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView_xi["xi_lsunicode", i].Value });
                        //this.dataGView_xi["bzxs", i].Value = "1" + string1 + "=" + this.dataGView_xi["bzxs", i].Value + string2;
                        this.dataGView_xi["bzxs", i].Value = "1" + this.dataGView_xi["xi_unicode", i].EditedFormattedValue + "=" + this.dataGView_xi["bzxs", i].Value + this.dataGView_xi["xi_lsunicode", i].EditedFormattedValue;
                           
                    }
                }
            }
            this.label3.Text = this.dataGView_xi.RowCount.ToString() + "条"; 
       
        }

        

    }
}
