using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtUtil.tool;
using ChSys;
using YtPlugin;
using YtWinContrl.com.datagrid;
using JiChuDict.form;
using YiTian.db;
using YtClient;

namespace JiChuDict
{
    public partial class WZPriceManag : Form, IPlug
    {
        bool isAdd;
        TvList xmList;
        TvList jxList;
        public WZPriceManag()
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


        private void WZPriceManag_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            TvList.newBind().add("计价体系ID", "1").add("名称", "2").add("拼音码", "3").add("五笔码", "4").add("模糊查找","5").Bind(this.Search_ytComboBox);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column6);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column7);
            this.Search_ytComboBox.SelectedIndex = 4;
            this.dataGView1.Url = "FindWZPrice";
            this.dataGView1.IsPage = true;
            this.Search_ytComboBox.SelectedIndexChanged +=new EventHandler(Search_ytComboBox_SelectedIndexChanged);
           // this.Search_ytComboBox.TextChanged +=new EventHandler(Search_ytComboBox_TextChanged);
            //this.dataGView1.reLoad(new object[] { His.his.Choscode });
            //this.dataGView1.reLoad(null);
        }
        void Search_ytComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Search_yTextBox.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            rfresh_toolStripButton_Click(null, null);

        }

        private void Add_toolStripButton_Click(object sender, EventArgs e)
        {
            isAdd = true;
            AddWZPrice form = new AddWZPrice();
            form.Main = this;
            form.ShowDialog();
            button1_Click(null, null);
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            isAdd = false;
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                AddWZPrice form = new AddWZPrice(null, dr, xmList, jxList);
                form.Main = this;
                form.ShowDialog();
                button1_Click(null, null);
            }
            else
            {
                WJs.alert("请选择要修改的计价体系信息！");
            }
        }

        private void DeleButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (WJs.confirmFb("您确定要删除选择的计价体系吗？"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.his.WZPriceManag.WZPrice";
                    // ac.Action = "Save";
                    ac.Sql = "Del";
                    //ac.Sql = "DelWZInfo";

                   // ac.Add("choscode", His.his.Choscode.ToString());
                    ac.Add("PRICEID", dr["计价体系id"].ToString());
                    //ac.SetKeyValue("choscode,warecode");

                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                    reLoad();
                }
            }
            else
            {
                WJs.alert("请选择要删除的计价体系信息！");
            }

        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            
            //refData(-1);
            
        }


        private void DisableButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["是否使用"].ToString() == "1")
                {
                    if (WJs.confirmFb("您确定要停用选择的计价体系吗？"))
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.his.WZPriceManag.WZPrice";
                        ac.Sql = "Disable";

                        //ac.Add("choscode", His.his.Choscode.ToString());
                        ac.Add("PRICEID", dr["计价体系id"].ToString());
                        ac.Add("IFUSE", "0");
                        //ac.SetKeyValue("choscode,warecode");

                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        reLoad();
                    }
                }
                else
                {
                    WJs.alert("该计价体系已经停用了！");
                }

            }
            else
            {
                WJs.alert("请选择要停用的物资信息！");
            }

        }

        private void EnableButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["是否使用"].ToString() == "0")
                {
                    if (WJs.confirmFb("您确定要启用选择的计价体系吗？"))
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.his.WZPriceManag.WZPrice";
                        ac.Sql = "Enable";

                       // ac.Add("choscode", His.his.Choscode.ToString());
                        ac.Add("PRICEID", dr["计价体系id"].ToString());
                       // ac.SetKeyValue("choscode,warecode");
                        ac.Add("IFUSE", "1");

                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        reLoad();
                    }
                }
                else
                {
                    WJs.alert("该计价体系已经启用了！");
                }

            }
            else
            {
                WJs.alert("请选择要启用的计价体系信息！");
            }

        }

        private void rfresh_toolStripButton_Click(object sender, EventArgs e)
        {
            reLoad();
           // refData(-1);
        }

        //private void refData(int localID)
        //{
        //    try
        //    {
        //        if (this.dataGView1.CurrentCell != null && localID < 0)
        //            localID = this.dataGView1.CurrentCell.RowIndex;
        //        reLoad();
        //        if (localID >= 0)
        //        {
        //            this.dataGView1.ClearSelection();
        //            Application.DoEvents();
        //            if (this.dataGView1.CurrentCell != null)
        //                this.dataGView1.CurrentCell = this.dataGView1[1, localID];
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WJs.alert(ex.Message);
        //    }
        //}

        public void reLoad()
        {
            SqlStr sqlc = SqlStr.newSql();
            this.dataGView1.Url = "FindWZPrice";

            if (this.Search_yTextBox.Text.Trim().Length > 0)
            {
                string strF = null;
                if (this.Search_ytComboBox.SelectedIndex > -1)
                {
                    strF = this.Search_yTextBox.Text.Trim();
                    if (this.Search_ytComboBox.SelectedIndex == 0)
                    {
                        sqlc.Add("and (PRICEID =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 1)
                    {
                        sqlc.Add("and (PRICENAME =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 2)
                    {
                        sqlc.Add("and (PYCODE =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 3)
                    {
                        sqlc.Add("and (WBCODE =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 4)
                    {
                        strF = "%" + this.Search_yTextBox.Text.Trim() + "%";
                        sqlc.Add(" and (PRICEID like ? or PRICENAME like ? or PYCODE like ? or WBCODE like ?)", strF, strF, strF, strF);
                    }
                   // this.dataGView1.reLoad(new object[] { His.his.Choscode }, sqlc);
                }
                else
                {
                    WJs.alert("请选择查询条件！");
                }

            }
           

            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sqlc);

           

        }

        private void scan_toolStripButton_Click(object sender, EventArgs e)
        {
            this.dataGView1.reLoad(new object[] { His.his.Choscode });
        }

       
    }
}
