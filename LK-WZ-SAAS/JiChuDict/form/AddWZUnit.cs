﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YiTian.db;
using YtWinContrl.com.datagrid;
using YtWinContrl.com;
using YtClient;
using YtUtil.tool;

namespace JiChuDict.form
{
    public partial class AddWZUnit : Form
    {
        public ProjectUnitManag Main;
        private YtWinContrl.com.datagrid.DataGView dataGViewPL;
        Dictionary<string, ObjItem> dr;
        bool isAdd;
        public bool isOk = false;
        TvList xmList;
        TvList jxList;
        public AddWZUnit()
        {
            isAdd = true;
            InitializeComponent();
        }

        public AddWZUnit(YtWinContrl.com.datagrid.DataGView gv,
                   Dictionary<string, ObjItem> dr, TvList xmList, TvList jxList)
        {
            isAdd = false;
            this.dr = dr;
            this.xmList = xmList;
            this.jxList = jxList;
            this.dataGViewPL = gv;
            InitializeComponent();
        }

        void dicdesc_yTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.dicdesc_yTextBox.Text.Trim().Length > 0)
            {

                pycode_yTextBox5.Text = PyWbCode.getPyCode(this.dicdesc_yTextBox.Text).ToLower();
                //wbcode_yTextBox5.Text = PyWbCode.getWbCode(this.pricename_yTextBox.Text).ToLower();
            }
        }


        private void save_button_Click(object sender, EventArgs e)
        {
            if (this.dicdesc_yTextBox.Text.Trim().Length == 0)
            {
                WJs.alert("请输入名称！");
                dicdesc_yTextBox.Focus();
                return;
            }
            if (this.defvalue_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请设置是否为默认值！");
                defvalue_ytComboBox.Focus();
                return;
            }

            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.his.WZUnitManag.WZUnit";
            ac.Sql = "Save";
            ac.Add("DICGRPID", this.dicgrpid_yTextBox.Text);
            ac.Add("DICDESC", this.dicdesc_yTextBox.Text);
            ac.Add("FIXED",1);
            ac.Add("PYCODE", this.pycode_yTextBox5.Text);
            ac.Add("DEFVALUE", TvList.getValue(this.defvalue_ytComboBox).ToInt());
           

            if (!isAdd)
            {
                ac.Add("DICID", dr["DICID"].ToString());
                ac.SetKeyValue("DICGRPID,DICID");

            }
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            //isOk = true;
           
           
            if (this.isAdd)
            {
                if (e.Msg.Msg.Equals("已经存在该物资价格体系信息!") || e.Msg.Msg.Equals("已经存在该物资价格体系信息,不能修改成该名称！"))
                {
                    WJs.alert(e.Msg.Msg);
                    //InitForm();
                }
                else if (!WJs.confirm("保存成功，是否继续添加？"))
                {
                    this.Close();
                }
                else
                {
                    InitForm();
                }
            }
            else
            {
                WJs.alert(e.Msg.Msg);
                this.Close();
            }

        }

        private void InitForm()
        {
            this.defvalue_ytComboBox.SelectedItem = "";
            this.defvalue_ytComboBox.SelectedIndex = -1;
            this.dicdesc_yTextBox.Text = "";
            this.pycode_yTextBox5.Text = "";
            this.defvalue_ytComboBox.SelectedIndex = 1;
            this.dicdesc_yTextBox.Focus();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddWZUnit_Load(object sender, EventArgs e)
        {
            ControlUtil.RegKeyEnter(this);
            this.dicgrpid_yTextBox.Text = "301";
            this.fixed_yTextBox.Text = "是";
            this.dicid_yTextBox.ReadOnly = true;
            dicdesc_yTextBox.TextChanged +=new EventHandler(dicdesc_yTextBox_TextChanged);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.defvalue_ytComboBox);
            
            this.dicdesc_yTextBox.Focus();

            if (!isAdd)
            {
                // this.dicgrpid_yTextBox = dr["DICGRPID"].ToString();
                this.dicid_yTextBox.Text = dr["DICID"].ToString();
                this.dicdesc_yTextBox.Text = dr["名称"].ToString();
                TvList.setSelectIndex(this.defvalue_ytComboBox, dr["是否默认值"].ToString());
                this.pycode_yTextBox5.Text = dr["拼音码"].ToString();


            }
            else
            {
                this.defvalue_ytComboBox.SelectedIndex = 1;
            }

           
        }
    }
}
