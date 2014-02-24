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
using YtUtil.tool;
using JiChuDict.form;
using YiTian.db;
using YtClient;
using ChSys;

namespace JiChuDict
{
    public partial class ProjectUnitManag : Form, IPlug
    {
        bool isAdd;
        TvList xmList;
        TvList jxList;
        public ProjectUnitManag()
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

        private void Add_toolStripButton_Click(object sender, EventArgs e)
        {
            isAdd = true;
            AddWZUnit form = new AddWZUnit();
            form.Main = this;
            form.ShowDialog();
            Search_button_Click(null, null);
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            isAdd = false;
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                AddWZUnit form = new AddWZUnit(null, dr, xmList, jxList);
                form.Main = this;
                form.ShowDialog();
                Search_button_Click(null, null);
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
                if (WJs.confirmFb("您确定要删除选择的物资单位信息吗？"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.his.WZUnitManag.WZUnit";
                    // ac.Action = "Save";
                    ac.Sql = "Del";
                    //ac.Sql = "DelWZInfo";

                    // ac.Add("choscode", His.his.Choscode.ToString());
                    ac.Add("UNITCODE", dr["DICID"].ToString());
                    ac.Add("LSUNITCODE", dr["DICID"].ToString());
                    ac.Add("DICID", dr["DICID"].ToString());
                    ac.Add("DICGRPID", dr["字典组类别"].ToString());
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
            //refData(-1);
            WJs.alert(e.Msg.Msg);

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
            this.dataGView1.Url = "FindWZUnit";
           
            if (this.Search_yTextBox.Text.Trim().Length > 0)
            {
                string strF = null;
                if (this.Search_ytComboBox.SelectedIndex > -1)
                {
                    strF = this.Search_yTextBox.Text.Trim();
                    if (this.Search_ytComboBox.SelectedIndex == 0)
                    {
                        sqlc.Add("and (DICID = ?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 1)
                    {
                        sqlc.Add("and (DICDESC = ?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 2)
                    {
                        sqlc.Add("and (PYCODE = ?)", strF);
                    }
                    if (this.Search_ytComboBox.SelectedIndex == 3)
                    {
                        strF = "%" + this.Search_yTextBox.Text.Trim() + "%";
                        sqlc.Add(" and ( DICID like ? or DICDESC like ? or PYCODE like ?) ", strF, strF, strF);
                        
                    }
                }
                else
                {
                    WJs.alert("请选择查询条件！");
                }
                
            }

             // this.dataGView1.reLoad(null , sqlc);
           
            
            this.dataGView1.reLoad(null, sqlc);

        }



        private void ProjectUnitManag_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            TvList.newBind().add("ID", "1").add("名称", "2").add("拼音码", "3").add("模糊查询","4").Bind(this.Search_ytComboBox);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column6);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column5);
            this.Search_ytComboBox.SelectedIndex = 3;
            this.dataGView1.Url = "FindWZUnit";
            this.dataGView1.IsPage = true;
            this.Search_ytComboBox.SelectedIndexChanged +=new EventHandler(Search_ytComboBox_SelectedIndexChanged);
            //this.Search_ytComboBox.TextChanged +=new EventHandler(Search_ytComboBox_TextChanged);
            //this.Search_ytComboBox.TextChanged += new EventHandler(Search_ytComboBox_TextChanged);
        }
        void Search_ytComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Search_yTextBox.Text = "";
        }
        private void Search_button_Click(object sender, EventArgs e)
        {
            rfresh_toolStripButton_Click(null, null);
        }

        private void rfresh_toolStripButton_Click(object sender, EventArgs e)
        {
           // refData(-1);
            reLoad();
        }

        private void scan_toolStripButton_Click(object sender, EventArgs e)
        {
            //this.dataGView1.Url = "FindWZUnit";
            this.dataGView1.reLoad(null);
        }

       

       
    }
}
