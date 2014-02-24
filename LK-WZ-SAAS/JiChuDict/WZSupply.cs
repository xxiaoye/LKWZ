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
using JiChuDict.form;
using YiTian.db;
using YtUtil.tool;
using YtClient;
using YtWinContrl.com.datagrid;

namespace JiChuDict
{
    public partial class WZSupply : Form, IPlug 
    {
        int rowid = 0;
        public WZSupply()
        {
            InitializeComponent();
        }

        private void WZSupply_Load(object sender, EventArgs e)
        {
            //this.dataGView1.Url="WZSupply_Scan";
            //this.dataGView1.reLoad(new object[] { His.his.Choscode });
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column6);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column7);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column5);
            TvList.newBind().add("厂商ID", "1").add("名称", "2").add("拼音码", "3").add("五笔码", "4").add("模糊查找", "5").Bind(this.Search_ytComboBox);
            TvList.newBind().add("全部", "1").add("生产厂商", "2").add("供应商", "3").Bind(this.ytComboBox1);
            this.Search_ytComboBox.SelectedIndex = 4;
            this.ytComboBox1.SelectedIndex = 0;
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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            WZSupply_Add form = new WZSupply_Add();
            form.WZSupplyMain = this;
            form.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.dataGView1.Url = "WZSupply_Scan";
            this.dataGView1.reLoad(new object[] { His.his.Choscode });
        }
        public void ReLoadData()
        {
            this.dataGView1.Url = "WZSupply_Scan";
            this.dataGView1.reLoad(new object[] { His.his.Choscode });
            //this.dataGView1.setFocus(rowid, 0);
            if (this.dataGView1.RowCount <= rowid)
            {
                this.dataGView1.setFocus(0, 0);

            }
            else
            {
                this.dataGView1.setFocus(rowid, 0);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)//编辑
        {
         
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                //string str= LData.Es("IfChoscodeIsZero", "LKWZ", new object[] { dr["SUPPLYID"] });
          if (dr["医疗机构编码"].ToString() == His.his.Choscode)
          
                {
                    rowid = this.dataGView1.CurrentRow.Index;
                    WZSupply_Add form = new WZSupply_Add(dr);
                    form.WZSupplyMain = this;
                    form.ShowDialog();
                }
                else 
                {
                    WJs.alert("只能修改本医疗机构的厂商信息！");
                }
            }
            else
            {
                WJs.alert("请选择要编辑的厂商信息！");
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//删除
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["医疗机构编码"].ToString() != His.his.Choscode)
                {
                    WJs.alert("不能删除不属于本医疗机构的厂商信息！");
                    return;
                }
                else if (WJs.confirmFb("确定要删除选择的厂商信息吗？\n删除后不能恢复!"))
                {


                    rowid = this.dataGView1.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkwz.JiChuDict.WZSupplySvr";
                    ac.Sql = "DelChangShangInfo";
                    ac.Add("SUPPLYID", dr["厂商ID"].ToInt());
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.Add("IFUSE", dr["是否使用"].ToString());
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的厂商信息！");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            ReLoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlStr sql = SqlStr.newSql();//创建SqlStr对象
            //if (this.textBox1.Text.Trim().Length > 0)
            //{
            //    //添加查询条件及其参数
            //    sql.Add("and SUPPLYID like ?", "%" + this.textBox1.Text.Trim() + "%");
            //}
            //if (this.yTxtBox_Name.Text.Trim().Length > 0)
            //{
            //    //添加查询条件及其参数
            //    sql.Add("and SUPPLYNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
            //}
            //if (this.yTxtBox_PY.Text.Trim().Length > 0)
            //{
            //    //添加查询条件及其参数
            //    sql.Add("and PYCODE like ?", "%" + this.yTxtBox_PY.Text.Trim() + "%");
            //}
            //if (this.yTxtBox_WB.Text.Trim().Length > 0)
            //{
            //    //添加查询条件及其参数
            //    sql.Add("and WBCODE like ?", "%" + this.yTxtBox_WB.Text.Trim() + "%");
            //}


            if (this.textBox1.Text.Trim().Length > 0)
            {
                string strF = null;
                if (this.Search_ytComboBox.SelectedIndex > -1)
                {
                    strF = this.textBox1.Text.Trim();
                    if (this.Search_ytComboBox.SelectedIndex == 0)
                    {
                        sql.Add("and (SUPPLYID =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 1)
                    {
                        sql.Add("and (SUPPLYNAME =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 2)
                    {
                        sql.Add("and (pycode =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 3)
                    {
                        sql.Add("and (wbcode =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 4)
                    {
                        strF = "%" + this.textBox1.Text.Trim() + "%";
                        sql.Add(" and (SUPPLYID like ? or SUPPLYNAME like ? or pycode like ? or wbcode like ?)", strF, strF, strF, strF);
                    }

                }
                else
                {
                    WJs.alert("请选择查询条件！");
                }

            }
            if (this.ytComboBox1.SelectedIndex != 0)
            {
                if (this.ytComboBox1.SelectedIndex == 1)
                {
                    sql.Add("and IFFACTORY=1 ");
                }
                else
                {
                    sql.Add("and  IFSUPPLY=1");
                }
            
            }


            //加载查询数据
            this.dataGView1.Url = "FindChangShangInfo";
            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sql);
            this.dataGView1.setFocus(rowid, 1);
            rowid = 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)//停用
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["医疗机构编码"].ToString() != His.his.Choscode)
                {
                    WJs.alert("只能停用属于本医疗机构的厂商信息！");
                }
                else if (WJs.confirmFb("是否停用？"))
                {
                    rowid = this.dataGView1.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkwz.JiChuDict.WZSupplySvr";
                    ac.Sql = "StopWZSupplyInfo";
                    ac.Add("SUPPLYID", dr["厂商ID"].ToInt());
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要停用的厂商信息");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)//启用
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["医疗机构编码"].ToString() != His.his.Choscode)
                {
                    WJs.alert("只能启用属于本医疗机构的厂商信息！");
                }
                else if (WJs.confirmFb("是否启用？"))
                {
                    rowid = this.dataGView1.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkwz.JiChuDict.WZSupplySvr";
                    ac.Sql = "StartWZSupplyInfo";
                    ac.Add("SUPPLYID", dr["厂商ID"].ToInt());
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要停用的厂商信息");
            }
        }

        //private void button2_Click(object sender, EventArgs e)//清空131212
        //{
        //    this.textBox1.Text = "";
        //    this.yTxtBox_Name.Text = "";
        //    this.yTxtBox_PY.Text = "";
        //    this.yTxtBox_WB.Text = "";
        //}

        //private void button4_Click(object sender, EventArgs e)//过滤查询131212
        //{
        //    decimal afford,make;
        //    if (checkBox_Afford.Checked)
        //    { 
        //     afford=1;
        //    }else
        //    {
        //        afford=0;
        //    }
        //    if(checkBox_Make.Checked)
        //    {
        //    make=1;
        //    }else
        //    {
        //     make=0;
        //    }
        //    this.dataGView1.Url = "FilterWZSupplyInfo";
        //    this.dataGView1.reLoad(new object[] { His.his.Choscode, Convert.ToDecimal(make),Convert.ToDecimal(afford) });
        //}
    }
}
