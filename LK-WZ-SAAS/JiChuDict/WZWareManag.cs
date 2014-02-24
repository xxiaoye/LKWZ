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
using YtClient;
using YtUtil.tool;
using JiChuDict.form;
using YiTian.db;

namespace JiChuDict
{
    public partial class WZWareManag : Form, IPlug
    {
        //private Thread mainThread;
        //private int rowid = 0;
        bool isAdd;
       TvList xmList;
        TvList jxList;
        public WZWareManag()
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
       // private Panel[] plis = null;

        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
          
            rfresh_toolStripButton_Click(null, null);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            isAdd = true;
            AddWZ form = new AddWZ();
            form.Main = this;
            form.ShowDialog();
            button1_Click(null, null);
        }

        private void WZWareManag_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            this.dataGView1.Url = "FindWZWares";
            TvList.newBind().add("库房编码", "1").add("库房名称", "2").add("拼音码", "3").add("五笔码", "4").add("模糊查找", "5").Bind(this.Search_ytComboBox);
            this.Search_ytComboBox.SelectedIndex = 4;
            TvList.newBind().add("先进先出", "1").add("先产先出", "2").add("先变先出", "3").add("近期先出", "4").Bind(this.Column11);
           // TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column13);
            this.dataGView1.IsPage = true;
            this.Search_ytComboBox.TextChanged +=new EventHandler(Search_ytComboBox_TextChanged);
           // this.dataGView1.reLoad(new object[] { His.his.Choscode });

        }
        void Search_ytComboBox_TextChanged(object sender, EventArgs e)
        {
            this.yTextBox1.Text = "";
        }
        private void ModifyButton_Click(object sender, EventArgs e)
        {
            isAdd = false;
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                AddWZ form = new AddWZ(null, dr, xmList, jxList);
                form.Main = this;
                form.ShowDialog();
                button1_Click(null, null);
            }
            else
            {
                WJs.alert("请选择要修改的物资库房信息！");
            }
        }

        private void DeleButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (WJs.confirmFb("您确定要删除选择的物资库房吗？"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.his.WZWareManag.WZWare";
                   // ac.Action = "Save";
                    ac.Sql = "Del";
                    //ac.Sql = "DelWZInfo";

                    ac.Add("choscode",His.his.Choscode.ToString());
                    ac.Add("warecode", dr["库房编码"].ToString());
                    ac.SetKeyValue("choscode,warecode");
                   
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                    reLoad();
                }
            }
            else
            {
                WJs.alert("请选择要删除的物资库房信息！");
            }
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            //refData(-1);
            //  this.gridPage1.Refresh();
            WJs.alert(e.Msg.Msg);
        }

        private void DisableButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["是否使用"].ToString() == "1")
                {
                    if (WJs.confirmFb("您确定要停用选择的物资库房吗？"))
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.his.WZWareManag.WZWare";
                        ac.Sql = "Disable";

                        ac.Add("choscode", His.his.Choscode.ToString());
                        ac.Add("warecode", dr["库房编码"].ToString());
                        ac.Add("ifuse", "0");
                       ac.SetKeyValue("choscode,warecode");

                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        reLoad();
                    }
                }
                else
                {
                    WJs.alert("该物资库房已经停用了！");
                }
                
            }
            else
            {
                WJs.alert("请选择要停用的物资库房信息！");
            }
        }

        private void EnableButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["是否使用"].ToString() == "0")
                {
                    if (WJs.confirmFb("您确定要启用选择的物资库房吗？"))
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.his.WZWareManag.WZWare";
                        ac.Sql = "Enable";

                        ac.Add("choscode", His.his.Choscode.ToString());
                        ac.Add("warecode", dr["库房编码"].ToString());
                        ac.SetKeyValue("choscode,warecode");
                        ac.Add("ifuse", "1");

                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        reLoad();
                    }
                }
                else
                {
                    WJs.alert("该物资库房已经启用了！");
                }

            }
            else
            {
                WJs.alert("请选择要启用的物资库房信息！");
            }

        }

       

        
        private void rfresh_toolStripButton_Click(object sender, EventArgs e)
        {
            reLoad();
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
            this.dataGView1.Url = "FindWZWares";
            
            if (this.yTextBox1.Text.Trim().Length > 0)
            {
                string strF = null;
                if (this.Search_ytComboBox.SelectedIndex > -1)
                {
                    strF = this.yTextBox1.Text.Trim();
                    if (this.Search_ytComboBox.SelectedIndex == 0)
                    {
                        sqlc.Add("and (a.warecode =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 1)
                    {
                        sqlc.Add("and (a.warename =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 2)
                    {
                        sqlc.Add("and (a.pycode =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 3)
                    {
                        sqlc.Add("and (a.wbcode =?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 4)
                    {
                        strF = "%" + this.yTextBox1.Text.Trim() + "%";
                        sqlc.Add(" and (a.warecode like ? or a.warename like ? or a.pycode like ? or a.wbcode like ?)", strF, strF, strF, strF);
                    }

                }
                else
                {
                    WJs.alert("请选择查询条件！");
                }
                
            }
            
            //this.dataGView1.reLoad(new object[] { His.his.Choscode }, sqlc);
            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sqlc);
           

        }

        private void scan_toolStripButton_Click(object sender, EventArgs e)
        {
            this.dataGView1.reLoad(new object[] { His.his.Choscode });
        }

     


      
        


    }
}
