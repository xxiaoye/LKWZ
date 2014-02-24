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
using StatQuery.form;
using YtUtil.tool;
using YtPlugin;
using YtClient;

namespace StatQuery
{
    public partial class WZStockAlarm : Form,IPlug
    {
        public WZStockAlarm()
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
        int uomp;
        int domp;
        private void WZStockAlarm_Load(object sender, EventArgs e)
        {
            //TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.comboBox1);
            //this.comboBox1.SelectedIndexChanged += new EventHandler(dataGView1_SelectionChanged);
            this.selTextInpt_Ware.Sql = "LKWZ_GetWare_ye";
            this.selTextInpt_Ware.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.main_lsunicode);
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.xi_unicode);
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.xi_lsunicode);
            TvList.newBind().Load("WZUseRec_Stock", new object[] { His.his.Choscode }).Bind(this.warename);
           
        }


        private void dataGView1_SelectionChanged(object sender, EventArgs e)//被引掉了，没有实现该功能
        {
            this.yTextBox_Down.Clear();
            this.yTextBox_Up.Clear();
        }



        //public void ReLoadData()
        //{

          //    DataRow r1 = this.dataGView_Main.GetRowData();
          //    this.dataGView_Main.Url = "WZStockAlarmStockDetailInfo";
          //    this.dataGView_Main.reLoad(new object[] { this.comboBox1.Value , His.his.Choscode });
          //}


        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            this.dataGView_xi.ClearData();//清理细表的数据
            if (this.selTextInpt_Ware.Value != null)
            {
                if (this.selTextInpt_Ware.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    this.dataGView_Main.Url = "ScanWZStockAlarmStockInfo";
                    this.dataGView_Main.reLoad(new object[] { this.selTextInpt_Ware.Value, His.his.Choscode });
                    this.TiaoSu.Text = this.dataGView_Main.RowCount.ToString() + "笔"; 
                }
            }
            else
            {
                WJs.alert("请选择库房！");
                return;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataRow r1 = this.dataGView_Main.GetRowData();


            WZStockAlarm_Set ksd = new WZStockAlarm_Set(r1);
            ksd.Main = this;
            ksd.ShowDialog();
            if (ksd.isSc)
            {
                toolStripButton8_Click(null, null);
            }
        }

       


        private void button1_Click_1(object sender, EventArgs e)
        {
            this.dataGView_Main.ClearData();//1127
            this.dataGView_xi.ClearData();
          
            if (this.selTextInpt_Ware.Value != null)
            {
                if (this.selTextInpt_Ware.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                      SqlStr sql = SqlStr.newSql();
           if (this.yTextBox_Down.Text.Trim().Length>0 )
            {
                if (int.TryParse(this.yTextBox_Down.Text.Trim().ToString(), out  uomp))
                {
                    sql.Add(" ( a.NUM <= ?  ", this.yTextBox_Down.Text.ToString());
                }
                else
                {
                    WJs.alert("输入库存下限值错误，请输入整数！");
                    this.yTextBox_Down.Focus();
                    return;
                }
           }
             else
           {
             sql.Add("(( a.NUM <= a.NUMXX)" );
           }
               
            if (this.yTextBox_Up.Text.Trim().Length>0)
            {
                if (int.TryParse(this.yTextBox_Up.Text.Trim().ToString(), out  domp))
                {
                    sql.Add(" or a.NUM >= ? )", this.yTextBox_Up.Text.ToString());
                }
                else
                {
                    WJs.alert("输入库存上限值错误，请输入整数！");
                    this.yTextBox_Down.Focus();
                    return;
                }
            }
             else
            {
             sql.Add(" or (a.NUM >= a.NUMSX and a.NUMSX !=0) )" );
            }
         
                    this.dataGView_Main.Url = "FindWZStockAlarmStockInfo";

                 
                    
            this.dataGView_Main.reLoad(new object[] {this.selTextInpt_Ware.Value, His.his.Choscode }, sql);
                }
            }
            else
            {
                WJs.alert("请选择库房！");
                return;
            }
            this.TiaoSu.Text = this.dataGView_Main.RowCount.ToString() + "笔"; 
        }

        private void dataGView_Main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            DataRow r1 = this.dataGView_Main.GetRowData();
            if (r1 != null)
            {
                this.dataGView_xi.Url = "WZStockAlarmStockDetailInfo";
                this.dataGView_xi.reLoad(new object[] { r1["STOCKID"], His.his.Choscode });


                if (this.dataGView_xi.RowCount > 0)
                {
                    for (int i = 0; i < this.dataGView_xi.RowCount; i++)
                    {

                        if (this.dataGView_xi["bzxs", i].Value != null && this.dataGView_xi["xi_lsunicode", i].Value != null && this.dataGView_xi["xi_unicode", i].Value != null)
                        {

                              //string string1 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView_xi["xi_unicode", i].Value });
                             // string string2 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView_xi["xi_lsunicode", i].Value });
                            // this.dataGView_xi["bzxs", i].Value = "1" + string1 + "=" + this.dataGView_xi["bzxs", i].Value + string2;
                            this.dataGView_xi["bzxs", i].Value = "1" + this.dataGView_xi["xi_unicode", i].EditedFormattedValue + "=" + this.dataGView_xi["bzxs", i].Value + this.dataGView_xi["xi_lsunicode", i].EditedFormattedValue;
                            
                        }
                    }
                }

                this.label3.Text = this.dataGView_xi.RowCount.ToString() + "条"; 
            }
            else
            {
                WJs.alert("请选择库存主表信息!");
            }
       
        }



    }
}
