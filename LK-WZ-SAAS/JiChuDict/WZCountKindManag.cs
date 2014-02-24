using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using YtClient;
using ChSys;
using YtUtil.tool;
using JiChuDict.form;

namespace JiChuDict
{
    public partial class WZCountKindManag : Form, IPlug
    {

        private string CurrentFindContent="";
        private int CurrentFindType = 0;
        private TreeNode CurrentPnode = null;

        public WZCountKindManag()
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

        #endregion
        private void toolStripButton4_Click(object sender, EventArgs e)
        {   
            this.Close(); 
        } 

        private void KS_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            this.toolStripComboBox1.SelectedIndex = 1;
            ytTreeView1.vFiled = "COUNTCODE";  
            ytTreeView1.tFiled = "COUNTNAME";   
            ytTreeView1.pFiled = "SUPERCODE";    
            ytTreeView1.pValue = "";
            ytTreeView1.sql = "FindCountKindInfo";
            this.ytTreeView1.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(ytTreeView1_FormatText);
            ReLoadData();            
        }

        void ytTreeView1_FormatText(YtWinContrl.com.events.TextFormatEvent e)
        {
            string a = "";
            if (e.row["IFUSE"].ToString() == "1")
            {
                a = "启用";
            }
            if (e.row["IFUSE"].ToString() == "0")
            {
                a = "停用";
            }
            e.FmtText = e.row["COUNTNAME"].ToString() + "       |" + a;
        }
        public void ReLoadData() {
            //His.his.Choscode = "0";
            this.ytTreeView1.reLoad(new object[] { His.his.Choscode });            // His.his.Choscode  
            //ReLoadData(null, null);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //isAdd = true;
            object[] r = null;
            if (this.ytTreeView1.getSelectRow() != null )
            {
                r = this.ytTreeView1.getSelectRow().ItemArray;
                r[1] = r[0];
                r[0] = r[1].ToString() + (this.ytTreeView1.SelectedNode.GetNodeCount(false)+1).ToString("00");
            }
            else
            {
                r = new object[] { "", "0", "", "", "", 1,0,"",His.his.UserId,His.his.UserName,DateTime.Now.ToString(),His.his.Choscode };
                //COUNTCODE,SUPERCODE,COUNTNAME,PYCODE,WBCODE,IFEND,IFUSE,MEMO,USERID,USERNAME,RECDATE,CHOSCODE

                r[0] =(this.ytTreeView1.SelectedNode.GetNodeCount(false)+1).ToString("00");
            }

                r[2] = "";
                r[3] = "";
                r[4] = "";
                r[5] = 1;
                r[6] = 0;
                r[7] = "";
                r[8] = His.his.UserId;
                r[9] = His.his.UserName;
                r[10] = DateTime.Now.ToString();
                r[11] = His.his.Choscode;

                JiChuDict.form.CountKindForm form = new JiChuDict.form.CountKindForm(r, true);
                form.ShowDialog();
                //if (form.isSc)
                {
                    ReLoadData();
                }
            

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DataRow r = this.ytTreeView1.getSelectRow();
            if (r != null)
            {
                if (WJs.confirmFb("确定要启用选择的统计类别信息吗？!"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.his.sys.SaveCountKind";
                    ac.Sql = "QiYong";
                    ac.Add("COUNTCODE", r["COUNTCODE"]);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要启用的统计类别信息");
            }
        }
        void FindNextNode()
        {
            
            if (CurrentPnode.Nodes.Count > 0)
            {
                CurrentPnode =CurrentPnode.Nodes[0];
            }
            else
            {
                CurrentPnode = GetUnderNode(CurrentPnode);
                if (CurrentPnode == null)
                {
                    //WJs.alert("搜索完毕，是否从头开始搜索");
                    WJs.confirm("搜索完毕，没有发现新的符合项，将从头开始搜索");
                    CurrentPnode = ytTreeView1.TopNode;
                    return;
                }
            }
            DataRow r = this.ytTreeView1.GetRow(CurrentPnode);
            if (r[CurrentFindType].ToString().Contains(CurrentFindContent))
            {
                this.ytTreeView1.SelectedNode = CurrentPnode;

            }
            else
            {
                FindNextNode();
            }

           
        }

         TreeNode GetUnderNode(TreeNode node)
        {
            if (node == null)
            {
                return null;
            }

            if (node.IsExpanded&&node.NextNode != null)
            {
                return CurrentPnode.NextNode;
            }
            else
            {
                CurrentPnode = node.Parent;
                return GetUnderNode(node.Parent);
            }
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //if(this.ytTreeView1.)
            // COUNTCODE,COUNTNAME,PYCODE,WBCODE,SUPERCODE,IFEND,IFUSE,MEMO,USERID,USERNAME,RECDATE,CHOSCODE
            DataRow r1 = this.ytTreeView1.getSelectRow();
             //COUNTCODE,SUPERCODE,COUNTNAME,PYCODE,WBCODE,IFEND,IFUSE,MEMO,USERID,USERNAME,RECDATE,CHOSCODE
            // this.ytTreeView1.getSelectRow().ItemArray;
            if (r1 != null)
            {
                object[] r = new object[] { r1["COUNTCODE"], r1["SUPERCODE"], r1["COUNTNAME"], r1["PYCODE"], r1["WBCODE"], r1["IFEND"], r1["IFUSE"], r1["MEMO"], r1["USERID"], r1["USERNAME"], r1["RECDATE"], r1["CHOSCODE"] };
                JiChuDict.form.CountKindForm form = new JiChuDict.form.CountKindForm(r, false);
                //ks.pr = this;
                form.ShowDialog();
                //if (form.isSc)
                {
                    ReLoadData();
                }
            }
            else {
                WJs.alert("请选择要编辑的统计类别信息");
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DataRow r = this.ytTreeView1.getSelectRow();
            if (r != null)
            {
                if (r["IFEND"].ToString()=="0")
                {
                    WJs.alert("存在子节点，不能删除!");
                    return;
                }
                if (WJs.confirmFb("确定要删除选择的统计类别信息吗？\n删除后不能恢复!"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.his.sys.SaveCountKind";
                    ac.Sql = "Del";
                    ac.Add("COUNTCODE", r["COUNTCODE"]);
                    ac.Add("SUPERCODE", r["SUPERCODE"]);
                    ac.Add("CHOSCODE",His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else {
                WJs.alert("请选择要删除的统计类别信息");
            }
            
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            ReLoadData();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DataRow r = this.ytTreeView1.getSelectRow();
            if (r != null)
            {
                if (WJs.confirmFb("确定要停用选择的统计类别信息吗？!"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.his.sys.SaveCountKind";
                    ac.Sql = "TingYong";
                    ac.Add("COUNTCODE", r["COUNTCODE"]);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要停用的统计类别信息");
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            CurrentFindContent = this.toolStripTextBox1.Text.Trim();
            CurrentPnode = ytTreeView1.TopNode;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentFindType = this.toolStripComboBox1.SelectedIndex;
            CurrentPnode = ytTreeView1.TopNode;
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            FindNextNode();
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            if (this.toolStripTextBox1.Text.Trim().Equals("查找关键字"))
            {
                this.toolStripTextBox1.Text = "";
            }
        }

        private void toolStripTextBox1_Leave(object sender, EventArgs e)
        {
            if (this.toolStripTextBox1.Text.Trim().Equals(""))
            {
                this.toolStripTextBox1.Text = "查找关键字";
            }
        }

        
    }
}
