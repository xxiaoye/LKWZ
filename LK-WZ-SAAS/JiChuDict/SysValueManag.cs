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
    public partial class SysValueManag : Form, IPlug
    {
        DataTable mzd=null;
        public SysValueManag()
        {
            InitializeComponent();
            this.selTextInpt1.OneRowAutoSelect = true;
            this.selTextInpt1.textBox1.ReadOnly = true;
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

        private void reLoad()
        {
            ActionLoad ld = ActionLoad.Conn();
            ld.Action = "Find";
            ld.Sql = "FindSysValue";
            ld.SetParams(new object[] { His.his.Choscode });
            
            ld.ServiceLoad += new YtClient.data.events.LoadEventHandle(ld_ServiceLoadcSF);

            ld.Post();

        }
        private void SysValueManag_Load(object sender, EventArgs e)
        {
            this.selTextInpt1.SelParam = His.his.Choscode + "|0|0|{key}|{key}";
            reLoad();
            this.selTextInpt1.OneRowAutoSelect = false;
           
        }

        private void ld_ServiceLoadcSF(object sender, YtClient.data.events.LoadEvent e)
        {
            mzd = e.Msg.GetDataTable();
            if (mzd != null)
            {
                if (mzd.Rows.Count >= 1)
                {
                    
                    //Cfh = mzd.Rows[0]["处方号"].ToString();
                    reSetValue();
                    //reLoad();
                }
            }
        }
        void reSetValue()
        {
           DataRow[] r= mzd.Select("id=2100");//
           if (r != null && r.Length==1)
           {
               this.comboBox1.SelectedIndex=Convert.ToInt32(r[0]["SYSVALUE"]);
           }
           r= mzd.Select("id=2101");//
           if (r != null && r.Length==1)
           {
               this.comboBox2.SelectedIndex=Convert.ToInt32(r[0]["SYSVALUE"]);
           }
           r= mzd.Select("id=2102");//
           if (r != null && r.Length==1)
           {
               this.comboBox3.SelectedIndex=Convert.ToInt32(r[0]["SYSVALUE"]);
           }
           r= mzd.Select("id=2103");//
           if (r != null && r.Length==1)
           {
               this.comboBox4.SelectedIndex=Convert.ToInt32(r[0]["SYSVALUE"]);
           }
           r= mzd.Select("id=2104");//
           this.selTextInpt1.Value = null;
           this.selTextInpt1.OneRowAutoSelect = true;
           if (r != null && r.Length==1)
           {
               this.selTextInpt1.SelParam = His.his.Choscode + "|" + r[0]["SYSVALUE"].ToString() + "|" + r[0]["SYSVALUE"].ToString() + "|{key}|{key}";
               this.selTextInpt1.LoadText();
               this.selTextInpt1.OpenWin();
               
               this.selTextInpt1.SelParam = His.his.Choscode + "|0|0|{key}|{key}";
               //this.selTextInpt1.Text = r[0]["SYSVALUE"].ToString();
               //this.selTextInpt1.Value = r[0]["SYSVALUE"].ToString();
               
               //this.selTextInpt1.tex
               
           }
           this.selTextInpt1.OneRowAutoSelect = false;
         //  this.selTextInpt1.LoadText(); ;
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
        }

        private void button1_Click(object sender, EventArgs e)  //修改
        {
            ActionLoad ld = ActionLoad.Conn();

            ld.Action = "LKWZSVR.his.sys.SaveSysValue";
            ld.Sql = "Add";

            if (this.selTextInpt1.Value == null)
            {
                
                this.selTextInpt1.Value = "0";
                
            }
            if (mzd==null || mzd.Rows.Count == 0)
            {
                
                ld.SetParams(new object[] { 2100, "调拨出库时，是否需要目标库房的确认", this.comboBox1.SelectedIndex.ToString(), "0：直接入库,1：确认入库", His.his.Choscode, 
                2101, "申领出库时，是否需要申领库房的确认", this.comboBox2.SelectedIndex.ToString(), "0：直接入库,1：确认入库", His.his.Choscode, 
                2102, "退回出库时，是否需要上级库房的确认", this.comboBox3.SelectedIndex.ToString(), "0：直接入库,1：确认入库", His.his.Choscode, 
                2103, "是否允许负库存出库", this.comboBox4.SelectedIndex.ToString(), "0：不允许；1：允许", His.his.Choscode, 
                2104, "三级库房使用登记时使用的出库方式",this.selTextInpt1.Value, "设置DictWZInOut里属于三级库房的入出标志=1的IOID", His.his.Choscode });

                ld.Sql = "Add";
            }
            else
            {
                ld.SetParams(new object[] { this.comboBox1.SelectedIndex.ToString(), His.his.Choscode, this.comboBox2.SelectedIndex.ToString(), His.his.Choscode, this.comboBox3.SelectedIndex.ToString(), His.his.Choscode, this.comboBox4.SelectedIndex.ToString(), His.his.Choscode, this.selTextInpt1.Value,His.his.Choscode });
                ld.Sql = "Update";
            }
            if (this.selTextInpt1.Value == "0")
            {
                this.selTextInpt1.Value = null;
            }

            ld.ServiceLoad += new YtClient.data.events.LoadEventHandle(ld_ServiceLoad);

                ld.Post();




        }

        private void ld_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {


            
            //this.SysValueManag_Load(null, null);
           
                    reLoad();
                   // this.selTextInpt1.OneRowAutoSelect = false;
        }

        private void button2_Click(object sender, EventArgs e) //取消
        {
            reSetValue();
        }

        private void selTextInpt1_Enter(object sender, EventArgs e)
        {
            //this.selTextInpt1.SelParam = His.his.Choscode + "|0|0";
            //this.selTextInpt1.LoadText();
            //this.selTextInpt1.ShowWin();

            //this.selTextInpt1.Params;
            
        }

        private void selTextInpt1_MouseClick(object sender, MouseEventArgs e)
        {
            //this.selTextInpt1.SelParam = His.his.Choscode + "|0|0";
        
            //this.selTextInpt1.LoadText();
            //this.selTextInpt1.OpenWin();
        }
        
    }
}
