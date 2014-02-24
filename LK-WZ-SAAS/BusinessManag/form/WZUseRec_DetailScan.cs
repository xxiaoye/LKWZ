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
using YiTian.db;
using YtUtil.tool;
using YtClient;

namespace BusinessManag.form
{
    public partial class WZUseRec_DetailScan : Form
    {
        public WZUseRec_DetailScan()
        {
            InitializeComponent();
        }
        DataRow r=null;
        private bool isAdd;
        private bool isScan=false;
        int currentrownum=0;
        int cout=0;
        public WZUseRec_DetailScan(DataRow r,bool _isAdd)
        {
            isAdd = _isAdd;
            this.r = r;
     
            InitializeComponent();
        }
        public WZUseRec_DetailScan(DataRow r) //查看传值
        {
            isScan = true;
            this.r = r;
            InitializeComponent();
        }
        public bool isSc = false;
        public WZUseRec Main;

       // private TvList dwList;
        TvList dwList = new TvList();

        private void WZPanDian_DetailScan_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            TvList.newBind().Load("Chang_LSDanWeiBianMa", null).Bind(this.Column24);
            this.dateTimePicker3.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker4.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            this.selTextInpt_Ware.Enabled = true;
            this.selTextInpt_Ware.Sql = "WZUseRec2_Stock1129";
            this.selTextInpt_Ware.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";

            //TvList.newBind().Load("WZUseRec2_Stock", new object[] { His.his.Choscode }).Bind(this.comboBox1);
          
            //this.selTextInpt2.textBox1.ReadOnly = true; 
            this.yTextBox_Name.ReadOnly = true;
            //this.yTextBox_ZYNum.TextChanged += new EventHandler(yTextBox_ZYNum_TextChanged);
            this.yTextBox_ZYNum.Leave += new EventHandler(yTextBox_ZYNum_Leave);
            //this.dataGView1.Url = "WZUseRec_ScanStockDetail";
            //this.dataGView1.reLoad(new object[] { His.his.Choscode });
            dataGView1.addSql("WZUseRec_KDKS", "开单科室",null , "{key}|{key}|{key}|" + His.his.Choscode);
            //dataGView1.addSql("WZUseRec_KDKS", "开单科室ID", null, "{key}|{key}|{key}|" + His.his.Choscode);
            dataGView1.addSql("WZUseRec_JDKS", "接单科室", null, "{key}|{key}|{key}|" + His.his.Choscode);
            //dataGView1.addSql("WZUseRec_JDKS", "接单科室ID", null, "{key}|{key}|{key}|" + His.his.Choscode);
            if (!this.isAdd)
            {
                if (r != null)
                {
                    this.selTextInpt_Ware.Value = r["WARECODE"].ToString();

                    this.selTextInpt_Ware.Text = LData.Es("LKWZ_GetWareName_ye", null, new object[] { His.his.Choscode, r["WARECODE"].ToString() });


                    this.yTextBox_Name.ReadOnly = true;
                    this.yTextBox_ZYNum.ReadOnly = true;
                    //this.dateTimePicker1.Enabled = true;
                    this.yTextBox_ZYNum.Text = r["SICKCODE"].ToString();
                    this.yTextBox_Name.Text = r["SICKNAME"].ToString();
                    //this.comboBox1.Enabled = false; //也可以修改其他库房的使用记录
                    button1_Click_1(null, null);  //查询数据
                }

            }
            if (this.isScan) //这里应该不需要查看功能了（可删）
            {
                
                //this.toolStripButton5.Enabled = false;
                this.selTextInpt_Ware.Enabled = false;
                this.selTextInpt_Ware.Value = r["WARECODE"].ToString();
                this.selTextInpt_Ware.Text = LData.Es("LKWZ_GetWareName_ye", null, new object[] { His.his.Choscode, r["WARECODE"].ToString() });


                this.yTextBox_Name.ReadOnly = true;
                this.yTextBox_ZYNum.ReadOnly = true;
                this.yTextBox_ZYNum.Text = r["SICKCODE"].ToString();
                this.yTextBox_Name.Text = r["SICKNAME"].ToString();
                this.btn_Save.Enabled = false;
                button1_Click_1(null, null);  //查询数
               
            }

        }


        
          
        
        public void ReLoadData() //原来代码
        {
            Main.ReLoadData();

        }
      

        private void btn_Save_Click(object sender, EventArgs e) //确定按钮点击事件
        {
            if (this.yTextBox_Name.Text.Trim().Length == 0)
            {
                WJs.alert("请输入住院号！");
                this.yTextBox_ZYNum.Focus();
                return;
            }

            if (this.selTextInpt_Ware.Value ==null)
            {
                WJs.alert("请选择使用库房！");
                this.selTextInpt_Ware.Focus();
                return;
            }


            ActionLoad ac = ActionLoad.Conn();

            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("WARECODE",this.selTextInpt_Ware.Value);
            ac.Add("SICKCODE", this.yTextBox_ZYNum.Text.ToString());
            ac.Add("SICKNAME", this.yTextBox_Name.Text.ToString());
            ac.Add("USERID", His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("STATUS", "1");
            //if (!isAdd)
            //{
            //    //修改使用信息

            //    if (PDID1.Trim().Length > 0)
            //    {
            //        ac.Add("USENO", PDID1);
            //        r["USENO"] = PDID1;

            //    }
            //    else
            //    {
            //        ac.Add("USENO", r["USENO"].ToString());
            //    }

            //}
            //else
            //{
            //    //保存使用信息
            //    ac.Add("USENO", null);
            //}

                ac.Action = "LKWZSVR.lkwz.BusinessManag.WZUseRecSvr";
                ac.Sql = "SaveWZUseRecInfo";
           

      
            if (this.dataGView1.RowCount > 0)
            {


               cout = 1;
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {


                    if (this.dataGView1["Column9", i].Value != null && this.dataGView1["Column9", i].Value.ToString().Trim().Length > 0 )
                    {
                        string IfCanOut = LData.Es("findWZUse_IfCanOut", "LKWZ", new object[] { His.his.Choscode });

                        string StockNum = LData.Es("findWZUseInNumInfo", "LKWZ", new object[] { His.his.Choscode, this.dataGView1["Column22", i].Value });

                      
                        int NowNum= Convert.ToInt32(this.dataGView1["Column9", i].Value);

                        if (this.dataGView1["Column21", i].Value != null && this.dataGView1["Column21", i].Value.ToString().Trim().Length>0)
                      {
                          string OldUseNum = LData.Es("updataInfo_Use_SeachNum", "LKWZ", new object[] { His.his.Choscode, this.dataGView1["Column21", i].Value });
                     
                          NowNum = Convert.ToInt32(this.dataGView1["Column9", i].Value) - Convert.ToInt32(OldUseNum); 
                      }
                      
                        if (IfCanOut != null && IfCanOut.Trim() == "0")
                      {



                          if (StockNum != null && Convert.ToInt32(StockNum) - NowNum < 0)
                          {
                              WJs.alert("使用数量不能大于库存数量！系统设置了不允许负库存出库，如果允许，请在系统参数管理中进行设置！");
                              this.dataGView1.setFocus(i, "使用数量");
                              return;
                          }
                          //else
                          //{
                          //    WJs.alert("系统没有设置是否允许负库存出库，请在系统参数管理中进行设置！");
                          //    return;
                          //}

                      }

                        if (this.dataGView1["Column17", i].Value != null && this.dataGView1["Column17", i].Value.ToString().Trim().Length > 0)
                        {
                            if (this.dataGView1["Column18", i].Value != null && this.dataGView1["Column18", i].Value.ToString().Trim().Length > 0)
                            {
                                if (this.dataGView1["Column2", i].Value != null && this.dataGView1["Column2", i].Value.ToString().Trim().Length > 0)
                                {
                                    //ac.Add("WZID" + cout, this.dataGView1["Column16", i].Value);
                                    ac.Add("STOCKFLOWNO" + cout, this.dataGView1["Column15", i].Value);
                                    ac.Add("SENDDEPTID" + cout, this.dataGView1["Column20", i].Value);
                                    ac.Add("RECVDEPTID" + cout, this.dataGView1["Column19", i].Value);
                                    ac.Add("USEDATE" + cout, this.dataGView1["Column2", i].Value);
                                    ac.Add("USENUM" + cout, this.dataGView1["Column9", i].Value);
                                    if (this.dataGView1["Column21", i].Value != null)
                                    {
                                        ac.Add("USENO" + cout, this.dataGView1["Column21", i].Value);
                                    }
                                    else
                                    {
                                        ac.Add("USENO" + cout, null);
                                    }
                                    cout++;
                                }
                                else
                                {
                                    WJs.alert("请选择使用时间！");
                                    this.dataGView1.setFocus(i, "使 用 时 间");
                                    return;
                                }
                            }
                            else
                            {
                                WJs.alert("请选择接单科室！");
                                this.dataGView1.setFocus(i, "接单科室");
                                return;
                            }
                        }
                        else
                        {
                            WJs.alert("请选择开单科室！");
                            this.dataGView1.setFocus(i, "开单科室");
                            return;
                        }
                    }
                }
                ac.Add("MyCount", cout);
               

                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
                ac.Post();
            }
            else
            {
                WJs.alert("详细盘点数据不能为空！");
                return;
            }




                 //if (cout <= 1)
            //{
            //    WJs.alert("请先录入使用信息！");
            //    return;
            //}
           

            if (WJs.confirmFb("是否继续录入使用信息？"))
            {
                //this.isAdd = false;
                this.yTextBox_ZYNum.Enabled = false;
                button1_Click_1(null, null);
            }
            else
            {
                this.Close();
            }
            
           
        }
        void ac_ServiceFaiLoad(object sender, YtClient.data.events.LoadFaiEvent e) //提交失败返回后触发（保存）
        {


            WJs.alert(e.Msg.Msg);
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)  //提交成功返回后触发
        {


            //string[] t = e.Msg.Msg.Split('|');
            //PDID1 = t[1];
            //WJs.alert(t[0]);
            //WZPDZ.ReLoadData();
            //Main.toolStripButton4_Click(null, null);
             WJs.alert(e.Msg.Msg);
             Main.callback(this.yTextBox_ZYNum.Text, this.selTextInpt_Ware.Value);
             isSc = true;
            //this.Close();

        }
        private void button1_Click_1(object sender, EventArgs e) //窗体查询按钮点击事件
        {

            SqlStr sql = SqlStr.newSql();

            if (this.yTextBox_ZYNum.Text.Trim().Length == 0)
            {
                WJs.alert("请输入住院号！");
                this.yTextBox_ZYNum.Focus();
                return;
            }
            if (this.selTextInpt_Ware.Value == null)
            {
                WJs.alert("请选择库房！");
                this.selTextInpt_Ware.Focus();
                return;
            }

            if (this.checkBox2.Checked)
            {
                sql.Add(" and e.NUM > 0 ");
            }
            if (this.dateTimePicker3.Value < this.dateTimePicker4.Value)
            {

                sql.Add("and a.INDATE >= ?", this.dateTimePicker3.Value);

                sql.Add("and a.INDATE <= ?", this.dateTimePicker4.Value);
            }
            else if (this.dateTimePicker3.Value != this.dateTimePicker4.Value)
            {
                WJs.alert("查询时间段有误！");

                return;
            }
            this.dataGView1.ClearData();

            
            if (!isAdd)
            {
                this.dataGView1.Url = "WZUseRec_EditStockDetail";
                this.dataGView1.reLoad(new object[] { this.yTextBox_ZYNum.Text, His.his.Choscode, this.selTextInpt_Ware.Value, His.his.Choscode, this.selTextInpt_Ware.Value, His.his.Choscode, this.selTextInpt_Ware.Value, His.his.Choscode, this.selTextInpt_Ware.Value }, sql);

            }
            else
            {

                this.dataGView1.Url = "WZUseRec_SearchStockDetail";
                this.dataGView1.reLoad(new object[] { His.his.Choscode, this.selTextInpt_Ware.Value }, sql);


            }


            if (this.dataGView1.RowCount > 0)
            {
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {
                   
                    if (this.dataGView1["Column30", i].Value != null && this.dataGView1["Column25", i].Value != null && this.dataGView1["Column24", i].Value != null)
                    {

                        string string1 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView1["Column30", i].Value });
                       string string2 = LData.Es("Chang_LSDanWeiBianMa1", "LKWZ", new object[] { this.dataGView1["Column24", i].Value });
                       this.dataGView1["Column25", i].Value = "1" + string1 + "=" + this.dataGView1["Column25", i].Value + string2;
                        //this.dataGView1["Column25", i].Value = "1" + this.dataGView1["Column30", i].EditedFormattedValue + "=" + this.dataGView1["Column25", i].Value + this.dataGView1["Column24", i].EditedFormattedValue;
                        
                    }

                }
            }


          
        } //查询按钮

        private void dataGView1_CellEndEdit(object sender, DataGridViewCellEventArgs e) //判断输入是否正确
        {
           if(this.dataGView1.CurrentCell.Value !=null)
            {
            
            
            if( this.dataGView1.CurrentCell.Value.ToString().Trim().Length>0)
                {
                int a=0;
                    if (this.dataGView1.CurrentCell.ColumnIndex == this.dataGView1.CurrentRow.Cells["Column9"].ColumnIndex)
                    {
                        if (int.TryParse(this.dataGView1.CurrentCell.Value.ToString(), out a))
                        {
                            if (a <= 0)
                            {
                                WJs.alert("使用数量必须是大于零的整数！");
                                this.dataGView1.CurrentCell.Value = null;
                                this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "使用数量");
                                return;
                            }
                            this.dataGView1.CurrentRow.Cells["Column2"].Value = DateTime.Now;

                            DateTime a11 = DateTime.Parse(this.dataGView1.CurrentRow.Cells["Column2"].Value.ToString());
                            DateTime a21 = DateTime.Parse(this.dataGView1.CurrentRow.Cells["Column13"].Value.ToString());

                            if (a11 > a21)
                            {
                                WJs.alert("使用日期不能超过有效日期！");
                                this.dataGView1.CurrentRow.Cells["Column2"].Value = null;
                                this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "使 用 时 间");
                                return;

                            }


                        }

                        else
                        {
                            WJs.alert("使用数量必须是大于零的整数！");
                            this.dataGView1.CurrentCell.Value = null;
                            this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "使用数量");
                            return;
                        }
                    }
                    else if (this.dataGView1.CurrentCell.ColumnIndex == this.dataGView1.CurrentRow.Cells["Column17"].ColumnIndex)
                    {



                        if (this.dataGView1.CurrentRow.Cells["Column20"].Value == null)
                        {
                            
                            WJs.alert("请选择开单科室！");
                            this.dataGView1.CurrentCell.Value = null;
                            this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "开单科室");
                            return;

                        }
                        
                         else if (this.dataGView1.CurrentRow.Cells["Column17"].Value != null)
                            {
                                string valid = LData.Es("WZUseRec_KDKS_Valid1", "LKWZ", new object[] { this.dataGView1.CurrentRow.Cells["Column17"].Value.ToString(), His.his.Choscode });
                                if (valid == null)
                                {
                                    WJs.alert("请选择开单科室！");
                                    this.dataGView1.CurrentCell.Value = null;
                                    this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "开单科室");
                                    return;
                                }
                            }
                            else
                            {
                                WJs.alert("请选择开单科室！");
                                this.dataGView1.CurrentCell.Value = null;
                                this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "开单科室");
                                return;
                            }
                        
                    
                    }
                    else if (this.dataGView1.CurrentCell.ColumnIndex == this.dataGView1.CurrentRow.Cells["Column18"].ColumnIndex)
                    {

                        if (this.dataGView1.CurrentRow.Cells["Column19"].Value == null)
                        {

                            WJs.alert("请选择接单科室！");
                            this.dataGView1.CurrentCell.Value = null;
                            this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "接单科室");
                            return;

                        }
                        else if (this.dataGView1.CurrentRow.Cells["Column18"].Value != null)
                        {
                            string valid = LData.Es("WZUseRec_KDKS_Valid1", "LKWZ", new object[] { this.dataGView1.CurrentRow.Cells["Column18"].Value.ToString(), His.his.Choscode });
                            if (valid == null)
                            {
                                WJs.alert("请选择接单科室！");
                                this.dataGView1.CurrentCell.Value = null;
                                this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "接单科室");
                                return;
                            }

                        }
                        else
                        {

                            WJs.alert("请选择接单科室！");
                            this.dataGView1.CurrentCell.Value = null;
                            this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "接单科室");
                            return;
                        }
                    }
                    else if (this.dataGView1.CurrentCell.ColumnIndex == this.dataGView1.CurrentRow.Cells["Column2"].ColumnIndex)
                    {
                       
                      DateTime a1=DateTime.Parse(this.dataGView1.CurrentRow.Cells["Column2"].Value.ToString()); 
                        DateTime a2= DateTime.Parse(this.dataGView1.CurrentRow.Cells["Column13"].Value.ToString());
                        
                                if(a1>a2)
                                {
                                  WJs.alert("使用日期不能超过有效日期！");
                                  this.dataGView1.CurrentCell.Value = null;
                                  this.dataGView1.setFocus(this.dataGView1.CurrentRow.Index, "使 用 时 间");
                                   return;

                                }
                           
                    }
                 
                }
       
            
            }
          }

        private void yTextBox_ZYNum_Leave(object sender, EventArgs e) //当结束编辑离开焦点时触发
        {
            if(isAdd || r==null)
            {
                if (this.yTextBox_ZYNum.Text.Length > 0)
                {

                    //if (this.yTextBox_Name.Text.Length <=0)
                    //{
                    decimal zynum;
                    string n = this.yTextBox_ZYNum.Text.Trim();

                    if (n.Length > 0 && decimal.TryParse(n, out zynum))
                    {
                        n = LData.Exe("WZUseRec_ZYNum", "LKWZ", new object[] { His.his.Choscode, Convert.ToDecimal(n) });
                        if (n != null)
                        {
                            this.yTextBox_Name.Text = n.ToString();

                        }
                        else
                        {
                            WJs.alert("病号输入错误");
                            this.yTextBox_ZYNum.Focus();

                        }

                    }
                    else
                    {
                        WJs.alert("病号输入错误");
                        this.yTextBox_ZYNum.Focus();

                    }
                }
                //}
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e) //取消按钮点击事件
        {

           
                if (WJs.confirmFb("是否确定不保存退出？"))
                {
                    this.Close();
                }
           
        }

        }
    }

