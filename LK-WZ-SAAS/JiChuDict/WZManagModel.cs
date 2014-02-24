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
    public partial class WZManagModel : Form, IPlug
    {

       // private string CurrentFindContent="";
       // private int CurrentFindType = 0;
       // private TreeNode CurrentPnode = null;

        public WZManagModel()
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


        private void WZManagModel_Load(object sender, EventArgs e)
        {       
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

 

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
        }
        
    }
}
