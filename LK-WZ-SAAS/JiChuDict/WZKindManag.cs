using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using YtUtil.tool;
using ChSys;
using JiChuDict.form;
using YtClient;
using YtWinContrl.com.datagrid;

namespace JiChuDict
{
    public partial class WZKindManag : Form,IPlug
    {
        public WZKindManag()
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
        //TreeNode tr = new TreeNode();
        string tr="";
        string a;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkwz.JiChuDict.WZKind";
            ac.Sql = "CopyWZKindInfo";
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post(); 
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text= "";
            this.yTxtBox_Name.Text = "";
            this.yTxtBox_PY.Text = "";
            this.yTxtBox_WB.Text = "";
        }

        private void WZKindManag_Load(object sender, EventArgs e)
        {
            //His.his.Choscode = "01040323";
            // WJs.SetDictTimeOut();//什么意思？
            ytTreeView1.vFiled = "KINDCODE";
            ytTreeView1.tFiled = "KINDNAME";
            ytTreeView1.pFiled = "SUPERCODE";
            tr = null;
           
            ytTreeView1.sql = "ScanWZKind";
           // this.ytTreeView1.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(ytTreeView1_FormatText);
            
            //是否加载时就显示数据
            //toolStripButton4_Click(null, null);
        }

     

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //this.ytTreeView1.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(ytTreeView1_FormatText);
           
            ytTreeView1.sql = "ScanWZKind";
            this.ytTreeView1.reLoad(new object[] { His.his.Choscode });
        }

        public void ReLoadData()
        {
            toolStripButton4_Click(null, null);
            GetAllNodeText(this.ytTreeView1.Nodes);
        }
        public void ReLoadData( string str)
        {
            tr = str;
            toolStripButton4_Click(null, null);
            GetAllNodeText(this.ytTreeView1.Nodes);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataRow r = this.ytTreeView1.getSelectRow();
            //if (r != null)
            //{
                //tr = this.ytTreeView1.SelectedNode.Text;
                WZKind_Add ks = new WZKind_Add(r, true);
                ks.pr = this;
                ks.ShowDialog();

            //}
            //else
            //{
            //    WJs.alert("请选择物资类型！");
            //    return;
            //}
            //if (ks.isSc)
            //{
            //    toolStripButton4_Click(null, null);//刷新
            //}
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            
            DataRow r = this.ytTreeView1.getSelectRow();
            if (r != null)
            {

              
                tr = this.ytTreeView1.SelectedNode.Text;
                string [] strr1 = tr.Split('|');
                tr = strr1[0];
                tr = tr.Trim();
                WZKind_Add ks = new WZKind_Add(r, false);
                ks.pr = this;
                ks.ShowDialog();
                //if (ks.isSc)
                //{
                //    toolStripButton4_Click(null, null);
                //}
            }
            else
            {
                WJs.alert("请选择要编辑的物资信息");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            toolStripButton4_Click(null, null);
          GetAllNodeText(this.ytTreeView1.Nodes);
           
        }
       private string  strsplit(string x)
        {
            string[] strr1 = x.Split('|');
            x = strr1[0];
            x = x.Trim();
           return x;
        
        }
        void GetAllNodeText(TreeNodeCollection tnc)//遍历整棵数，如果Text相等则被选中node.Text
        {
            foreach (TreeNode node in tnc)
            {
                
                    
                   if (node.Nodes.Count != 0)
                       GetAllNodeText(node.Nodes);
                   if ( strsplit(node.Text) == tr)
                   {
                       this.ytTreeView1.SelectedNode = node;
                       break;

                   };
          
            }
           
        }
        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {

            DataRow r = this.ytTreeView1.getSelectRow();
            if (r != null)
            {


                if (WJs.confirmFb("确定要删除选择的物资类别吗？\n删除后不能恢复!"))
                {
                    //if (r["IFUSE"].ToString() == "1")
                    //{
                    //    WJs.alert("不能删除已启用的物资,请先停用再删除!");
                    //    return;
                    //}
                    string values = LData.Es("WZKind_DelScan", "LKWZ", new object[] { r["KINDCODE"], r["CHOSCODE"] });
                   if (values !=null )
                    {
                        WJs.alert("不能删除已使用的物资，只能停用!");
                        return;
                    }

                    //tr = this.ytTreeView1.SelectedNode.Parent.Text;//获得其父节点为选中节点
                   tr = this.ytTreeView1.SelectedNode.Parent.Text;
                   string[] str = tr.Split('|');
                   tr = str[0];
                   tr = tr.Trim();
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkwz.JiChuDict.WZKind";
                    ac.Sql = "DelWZKindInfo";
                    ac.Add("KINDCODE", r["KINDCODE"]);
                    ac.Add("SUPERCODE", r["SUPERCODE"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的物资类别！");
                return;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlStr sql = SqlStr.newSql();//创建SqlStr对象
            ytTreeView1.sql = "ScanWZKind";
            this.ytTreeView1.reLoad(new object[] { His.his.Choscode });
            //if (this.textBox1.Text.Trim().Length > 0)
            //{
            //    //添加查询条件及其参数
            //   sql.Add("and KINDCODE like ?", "%" + this.textBox1.Text.Trim() + "%");

            //}
            //if (this.yTxtBox_Name.Text.Trim().Length > 0)
            //{
            //    //添加查询条件及其参数
            //    sql.Add("and KINDNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
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

            string text = LData.Es("FindWZKind", null, new object[] { His.his.Choscode,this.textBox1.Text.Trim(), this.yTxtBox_Name.Text.Trim(), this.yTxtBox_PY.Text.Trim(), this.yTxtBox_WB.Text.Trim() });

            FindNode(this.ytTreeView1.Nodes, text);
         
            

          
          
            
        }
        void FindNode(TreeNodeCollection tnc,string str)//遍历整棵数，如果Text相等则被选中node.Text
        {
            foreach (TreeNode node in tnc)
            {


                if (node.Nodes.Count != 0)
                    FindNode(node.Nodes,str);
                if (strsplit(node.Text) == str)
                {
                    this.ytTreeView1.SelectedNode = node;
                    break;

                };

            }

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DataRow r = this.ytTreeView1.getSelectRow();
            if (r != null)
            {
                if (WJs.confirmFb("是否启用？"))
                {
                    tr = this.ytTreeView1.SelectedNode.Text;
                    string[] str = tr.Split('|');
                    tr = str[0];
                    tr = tr.Trim();
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkwz.JiChuDict.WZKind";
                    ac.Sql = "StartWZKindInfo";
                    ac.Add("KINDCODE", r["KINDCODE"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要启用的物资类别");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            DataRow r = this.ytTreeView1.getSelectRow();
            if (r != null)
            {
                if (WJs.confirmFb("是否停用？"))
                {
               
                    tr = this.ytTreeView1.SelectedNode.Text;
                    string[] str = tr.Split('|');
                    tr = str[0];
                    tr = tr.Trim();
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkwz.JiChuDict.WZKind";
                    ac.Sql = "StopWZKindInfo";
                    ac.Add("KINDCODE", r["KINDCODE"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要停用的物资类别");
            }
        }

        private void ytTreeView1_FormatText(YtWinContrl.com.events.TextFormatEvent e)
        {
               

            if (e.row["KINDNAME"] !=null)
            {
                toolStripButton1.Enabled = false;
            }

            //显示所有信息
            //e.FmtText = e.row["KINDNAME"].ToString() + "       |上级编码=" + e.row["SUPERCODE"] + "  |类别名称=" + e.row["KINDCODE"] + "  |拼音码=" + e.row["PYCODE"] + "  |五笔码=" + e.row["WBCODE"] + "  |是否末节点=" + e.row["IFEND"] + "  |是否使用=" + e.row["IFUSE"] + "  |操作员ID=" + e.row["USERID"] + "  |操作员姓名=" + e.row["USERNAME"] + "  |修改时间=" + e.row["RECDATE"] + "  |医疗机构编码=" + e.row["CHOSCODE"];
            
            if (e.row["IFUSE"].ToString() == "1")
            {
                a = "启用";
            }
            if (e.row["IFUSE"].ToString() == "0")
            {
                a = "停用";
            }
            e.FmtText = e.row["KINDNAME"].ToString() + "        |" + a;
          
       
        }

      
     

    }
}
