using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtWinContrl.com.datagrid;
using ChSys;
using YtUtil.tool;
using YtPlugin;

namespace StatQuery
{
    public partial class WZXiaoQi : Form,IPlug
    {
        public WZXiaoQi()
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

        private void WZXiaoQi_Load(object sender, EventArgs e)
        {

            this.selTextInpt1.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;//为什么要这样写？
            selTextInpt1.BxSr = false;//必须输入查询关键字
            this.selTextInpt1.Sql = "FindWZKindWZXiaoQi";
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt2.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;
            selTextInpt1.BxSr = false;//必须输入查询关键字
            this.selTextInpt2.Sql = "FindWZDictWZXiaoQi";
            //this.selTextInpt2.textBox1.ReadOnly = true;
            this.selTextInpt_Ware.Sql = "LKWZ_GetWare_ye";
            this.selTextInpt_Ware.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.Column24);
            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.Column16);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGView1.ClearData();//1127
            this.dataGView1.Url = "WZXiaoQi_SearchStockDetail";
            SqlStr sql = SqlStr.newSql();
            if (this.selTextInpt_Ware.Value== null)
            {
                WJs.alert("请选择库房！");
                this.selTextInpt_Ware.Focus();
                return;
            }
           
                sql.Add("and a.VALIDDATE-To_date('" + this.dateTimePicker3.Value + "','yyyy-mm-dd hh24-mi-ss')<=15");

            
          


            if (this.checkBox2.Checked)
            {
                sql.Add(" and c.NUM > 0 ");
            }
            if (this.selTextInpt1.Value!=null)
            {
                sql.Add(" and e.KINDCODE =? ", this.selTextInpt1.Value);
            }
              if (this.selTextInpt2.Value!=null)
            {
                sql.Add(" and a.WZID=? ",this.selTextInpt2.Value);
            }
         
           

            this.dataGView1.reLoad(new object[] {this.selTextInpt_Ware.Value, His.his.Choscode }, sql);

            //隐掉了包装系数，在效期中没什么意义。
            //if (this.dataGView1.RowCount > 0)
            //{
            //    for (int i = 0; i < this.dataGView1.RowCount; i++)
            //    {
                   
            //        if (this.dataGView1["Column17", i].Value != null && this.dataGView1["Column25", i].Value != null && this.dataGView1["Column24", i].Value != null)
            //        {

            //            string string1 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView1["Column17", i].Value });
            //            string string2 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView1["Column24", i].Value });
            //            this.dataGView1["Column25", i].Value = "1" + string1 + "=" + this.dataGView1["Column25", i].Value + string2;
            //        }
            //    }
            //}

            this.label3.Text = this.dataGView1.RowCount.ToString() + "条"; 
        }

    }
}
