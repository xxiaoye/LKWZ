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
using YtWinContrl.com.datagrid;

namespace JiChuDict
{
    public partial class WZInOutManag : Form, IPlug
    {
        String[] Type = new String[]{ "IOID", "IONAME", "PYCODE", "WBCODE" } ;
        public WZInOutManag()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
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


        private void WZInOutManag_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            //object[] param = new object[] { His.his.Choscode, His.his.WsjCode };

            //xmList = TvList.newBind().SetCacheKey("XmDw").Load("FindXmDwList", param);
            //xmList.Bind(this.Column6);

            //TvList.newBind().SetCacheKey("XmDw").Load("FindXmDwList", param).Bind(this.Column7);

            //TvList.newBind().SetCacheKey("LB").Load("FindDict", new object[] { "2" }).Bind(this.Column10);
            //TvList.newBind().SetCacheKey("LB").add("(全部)").Load("FindDict", new object[] { "2" }).Bind(this.cbFarekind);

            //jxList = TvList.newBind().SetCacheKey("JX").Load("FindDict", new object[] { "1" });
            //jxList.Bind(this.Column9);
            //TvList.newBind().SetCacheKey("FYLB").Load("FindFyLbList", param).Bind(this.Column11);

            TvList.newBind().add("否", "0").add("是", "1").Bind(this.Column4);
            TvList.newBind().add("不包含", "0").add("包含", "1").Bind(this.Column7);
            TvList.newBind().add("不包含", "0").add("包含", "1").Bind(this.Column8);
            TvList.newBind().add("入库", "0").add("出库", "1").Bind(this.Column9);
            TvList.newBind().add("不使用", "0").add("使用", "1").Bind(this.Column10);
            TvList.newBind().add("不使用", "0").add("使用", "1").Bind(this.Column11);
            TvList.newBind().add("不使用", "0").add("使用", "1").Bind(this.Column12);
            TvList.newBind().add("普通", "0").add("调拨", "1").add("申领", "2").add("盘点", "3").add("退回", "4").Bind(this.Column13);
           
            TvList.newBind().add("否", "0").add("是", "1").Bind(this.Column14);




            this.dataGView1.IsPage = true;

            this.dataGView1.Url = "FindInOutList";
            reLoad();

            //plbtn.Visible = His.Fixedflag;

            //cbFarekind.SelectedIndex = 0;
            //cbBnw.SelectedIndex = 0;
        }
        public void reLoad()
        {
            //this.dataGView1.Url = sql;
            SqlStr sqlc = SqlStr.newSql();
            
            if (this.textBox1.Text.Trim().Length > 0)
            {
                string strF = "%" + this.textBox1.Text.Trim() + "%";
                sqlc.Add(" and (" + Type[this.comboBox1.SelectedIndex] + " like ?)", strF, strF, strF);
            }
            if ( comboBox2.SelectedIndex>0)
            {
                sqlc.Add(" and IOFLAG=" + (comboBox2.SelectedIndex-1) .ToString()+ "");
            }
            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sqlc);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.comboBox2.SelectedIndex = 0;
            reLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reLoad();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)  //复制
        {
           

                if (WJs.confirmFb("确定要复制通用的出入库信息到本医疗机构？!"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.his.sys.SaveInOut";
                    ac.Sql = "Copy";
                    ac.Add("IOID",null);
                    ac.Add("IONAME", null);
                    ac.Add("PYCODE", null);
                    ac.Add("WBCODE", null);
                    ac.Add("IFUSE", null);
                    ac.Add("RECIPECODE", null);
                    ac.Add("RECIPELENGTH", null);
                    ac.Add("RECIPEYEAR", null);
                    ac.Add("RECIPEMONTH", null);
                    ac.Add("MEMO", null);
                    ac.Add("IOFLAG", null);
                    ac.Add("USEST", null);
                    ac.Add("USEND", null);
                    ac.Add("USERD", null);
                    ac.Add("OPFLAG", null);
                    ac.Add("IFDEFAULT", null);
                    ac.Add("RECDATE", null);
                    ac.Add("USERID", His.his.UserId);
                    ac.Add("USERNAME", His.his.UserName);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            reLoad();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)  //添加
        {
            //if (r != null)
            //{
            //    DataRow r = this.dataGView1.GetRowData().Table.NewRow();
            //}
            //DataRow r = new DataRow();
            //r = new DataRow();
            Dictionary<string, object> r = new Dictionary<string, object>();

            r.Add("IOID", "");
            r.Add("RECIPELENGTH", 10);
            r.Add("IONAME","");
            r.Add("PYCODE", "");
            r.Add("WBCODE", "");
            r.Add("MEMO","");
            r.Add("USERID", His.his.UserId);
            r.Add("USERNAME", His.his.UserName);
            r.Add("RECIPECODE","");
            r.Add("RECDATE",DateTime.Now);
            r.Add("CHOSCODE", His.his.Choscode);
            r.Add("IFUSE", 0);
            r.Add("RECIPEYEAR", 0);
            r.Add("RECIPEMONTH", 0);
            r.Add("IOFLAG",0);
            r.Add("USEST", 1);
            r.Add("USEND",0);
            r.Add("USERD", 0);
            r.Add("OPFLAG", 0);
            r.Add("IFDEFAULT", 0);


            //r["IOID"]="";
            //r["RECIPELENGTH"]=10;
            //r["IONAME"]="";
            //r["PYCODE"]="";
            //r["WBCODE"]="";
            //r["MEMO"]="";
            //r["USERID"]=His.his.UserId;
            //r["USERNAME"]=His.his.UserName;
            //r["RECDATE"]=DateTime.Now;
            //r["CHOSCODE"]=His.his.Choscode;
            //r["IFUSE"]=0;
            //r["RECIPEYEAR"]=0;
            //r["RECIPEMONTH"]=0;
            //r["IOFLAG"]=0;
            //r["USEST"]=1;
            //r["USEND"]=0;
            //r["USERD"]=0;
            //r["OPFLAG"]=0;
            //r["IFDEFAULT"]=0;
           
          
            JiChuDict.form.InOutForm form = new JiChuDict.form.InOutForm(r, true);
            form.ShowDialog();
            if (form.isSc)
            {
                reLoad();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e) //编辑
        {
            DataRow r1 = this.dataGView1.GetRowData();
           // Dictionary<string, object> r = this.dataGView1.getRowData().ToDictionary<>;
            Dictionary<string,object> r=new Dictionary<string,object>();;
            r.Add("IOID", r1["IOID"]);
            r.Add("RECIPELENGTH", r1["RECIPELENGTH"]);
            r.Add("IONAME", r1["IONAME"]);
            r.Add("PYCODE", r1["PYCODE"]);
            r.Add("WBCODE", r1["WBCODE"]);
            r.Add("MEMO", r1["MEMO"]);
            r.Add("USERID", r1["USERID"]);
            r.Add("USERNAME", r1["USERNAME"]);
            r.Add("RECIPECODE", r1["RECIPECODE"]);
            r.Add("RECDATE", r1["RECDATE"]);
            r.Add("CHOSCODE", r1["CHOSCODE"]);
            r.Add("IFUSE", r1["IFUSE"]);
            r.Add("RECIPEYEAR", r1["RECIPEYEAR"]);
            r.Add("RECIPEMONTH", r1["RECIPEMONTH"]);
            r.Add("IOFLAG", r1["IOFLAG"]);
            r.Add("USEST", r1["USEST"]);
            r.Add("USEND", r1["USEND"]);
            r.Add("USERD", r1["USERD"]);
            r.Add("OPFLAG", r1["OPFLAG"]);
            r.Add("IFDEFAULT", r1["IFDEFAULT"]);
            if (r != null)
            {
                JiChuDict.form.InOutForm form = new JiChuDict.form.InOutForm(r, false);
                form.ShowDialog();
                if (form.isSc)
                {
                    reLoad();
                }
            }
            else
            {
                WJs.alert("请选择要编辑的出入类型信息");
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)  //删除
        {
            DataRow r = this.dataGView1.GetRowData();
            if (r != null)
            {
                if (WJs.confirmFb("确定要删除选择的入出类型信息吗？!"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.his.sys.SaveInOut";
                    ac.Sql = "Del";
                    ac.Add("IOID", r["IOID"]);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的入出类型信息");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e) //停用
        {
            DataRow r = this.dataGView1.GetRowData();
            if (r != null)
            {
                if (WJs.confirmFb("确定要停用选择的入出类型信息吗？!"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.his.sys.SaveInOut";
                    ac.Sql = "TingYong";
                    ac.Add("IOID", r["IOID"]);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要停用的入出类型信息");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)  //启用
        {
            DataRow r = this.dataGView1.GetRowData();
            if (r != null)
            {
                if (WJs.confirmFb("确定要启用选择的入出类型信息吗？!"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.his.sys.SaveInOut";
                    ac.Sql = "QiYong";
                    ac.Add("IOID", r["IOID"]);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要启用的入出类型信息");
            }

        }
        
    }
}
