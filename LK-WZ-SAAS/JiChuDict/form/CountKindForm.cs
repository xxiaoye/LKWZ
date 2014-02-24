using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtUtil.tool;
using YtClient;
using YtWinContrl.com;
namespace JiChuDict.form
{
    public partial class CountKindForm : Form
    {

        private object[] r=null;
        private bool isAdd;
        public bool isSc;

        public CountKindForm(object[] r, bool _isAdd)
        {
            isAdd = _isAdd;
            this.r = r;
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 1;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox3.Text.Trim() == "")
            {
                WJs.alert("请输入统计类别名称");
                return;
            }
            ActionLoad ac1 = ActionLoad.Conn();
            ac1.Action = "LKWZSVR.his.sys.SaveCountKind";
            if (isAdd)
                ac1.Sql = "Add";
            else
                ac1.Sql = "Update";
            ac1.Add("COUNTCODE", this.textBox1.Text);
            ac1.Add("SUPERCODE", this.textBox2.Text);
            ac1.Add("COUNTNAME", this.textBox3.Text);
            ac1.Add("PYCODE", this.textBox4.Text);
            ac1.Add("WBCODE", this.textBox5.Text);
            ac1.Add("MEMO", this.textBox6.Text);
            ac1.Add("USERID", this.textBox7.Text);
            ac1.Add("USERNAME", this.textBox8.Text);
            ac1.Add("RECDATE", this.textBox9.Text);
            ac1.Add("CHOSCODE", this.textBox10.Text);
            ac1.Add("IFUSE", this.comboBox2.SelectedIndex);
            ac1.Add("IFEND", this.comboBox1.SelectedIndex);
            ac1.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac1_ServiceLoad);
            ac1.Post();
            

        }

        void ac1_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            //pr.ReLoadData();
            if (!isAdd || !WJs.confirm("是否继续添加统计类别？"))
            {
                isSc = true;
                this.Close();
            }
            if (isAdd)
            {
                this.textBox1.Text = "";//r[0].ToString();
                this.textBox2.Text = r[1].ToString();
                this.textBox3.Text = r[2].ToString();
                this.textBox4.Text = r[3].ToString();
                this.textBox5.Text = r[4].ToString();
                this.textBox6.Text = r[7].ToString();
                this.textBox7.Text = r[8].ToString();
                this.textBox8.Text = r[9].ToString();
                this.textBox9.Text = r[10].ToString();
                this.textBox10.Text = r[11].ToString();
                this.comboBox1.SelectedIndex = Convert.ToInt32(r[5]);
                this.comboBox2.SelectedIndex = Convert.ToInt32(r[6]);
            }
        }
        void countKindName_TextChanged(object sender, EventArgs e)
        {
            string n = this.textBox3.Text.Trim();
            if (n.Length > 0)
            {

                this.textBox4.Text = PyWbCode.getPyCode(n).ToLower();
                this.textBox5.Text = PyWbCode.getWbCode(n).ToLower();
            }
        }
        private void CountKindForm_Load(object sender, EventArgs e)
        {
            this.textBox3.TextChanged += new EventHandler(countKindName_TextChanged);

            //COUNTCODE,SUPERCODE,COUNTNAME,PYCODE,WBCODE,IFEND,IFUSE,MEMO,USERID,USERNAME,RECDATE,CHOSCODE
                this.textBox1.Text = r[0].ToString();
                this.textBox2.Text = r[1].ToString();
                this.textBox3.Text = r[2].ToString();
                this.textBox4.Text = r[3].ToString();
                this.textBox5.Text = r[4].ToString();
                this.textBox6.Text = r[7].ToString();
                this.textBox7.Text = r[8].ToString();
                this.textBox8.Text = r[9].ToString();
                this.textBox9.Text = r[10].ToString();
                this.textBox10.Text = r[11].ToString();
                this.comboBox1.SelectedIndex = Convert.ToInt32(r[5]);
                this.comboBox2.SelectedIndex = Convert.ToInt32(r[6]);
                //this.groupBox1.Controls[Convert.ToInt32(r["IFUSE"])].Select();
                //this.groupBox2.Controls[Convert.ToInt32(r["IFEND"])].Select();
                //ControlUtil.SetSelectRadioValue(this.groupBox1,r["IFUSE"]);
                //ControlUtil.SetSelectRadioValue(this.groupBox2, r["IFEND"]);
                // ControlUtil.SetSelectRadioValue(this.groupBox2, r["USEPLACE"]);

        }
    }
}
