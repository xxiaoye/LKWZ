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
    public partial class InOutForm : Form
    {

        private Dictionary<string, object> r;
        private bool isAdd;
        public bool isSc;

        public InOutForm(Dictionary<string, object> r, bool _isAdd)
        {
            isAdd = _isAdd;
            this.r = r;
            InitializeComponent();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox3.Text.Trim() == "")
            {
                WJs.alert("请填写出入类型名称！");
                return;
            }
            if (this.textBox11.Text.Trim() == "")
            {
                WJs.alert("请填写单据前缀");
                return;
            }
            if (!WJs.IsZs(this.textBox2.Text.Trim()) || (Convert.ToInt32(this.textBox2.Text) <=0))
            {
                WJs.alert("单据长度格式错误！请输入一个大于0整数");
                return;
            }
            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.his.sys.SaveInOut";
            if (isAdd)
                ac.Sql = "Add";
            else
                ac.Sql = "Update";
            ac.Add("IOID", this.textBox1.Text.Trim());
            ac.Add("IONAME", this.textBox3.Text.Trim());
            ac.Add("PYCODE", this.textBox4.Text.Trim());
            ac.Add("WBCODE", this.textBox5.Text.Trim());
            ac.Add("IFUSE", this.comboBox1.SelectedIndex);
            ac.Add("RECIPECODE", this.textBox11.Text.Trim());
            ac.Add("RECIPELENGTH", Convert.ToInt32(this.textBox2.Text));
            ac.Add("RECIPEYEAR", this.comboBox2.SelectedIndex);
            ac.Add("RECIPEMONTH", this.comboBox3.SelectedIndex);
            ac.Add("MEMO", this.textBox6.Text.Trim());
            ac.Add("IOFLAG", this.comboBox4.SelectedIndex);
            ac.Add("USEST", this.comboBox5.SelectedIndex);
            ac.Add("USEND", this.comboBox6.SelectedIndex);
            ac.Add("USERD", this.comboBox7.SelectedIndex);
            ac.Add("OPFLAG", this.comboBox8.SelectedIndex);
            ac.Add("IFDEFAULT", this.comboBox9.SelectedIndex);
            ac.Add("RECDATE", null);
            ac.Add("USERID", this.textBox7.Text);
            ac.Add("USERNAME", this.textBox8.Text);
            ac.Add("CHOSCODE", this.textBox10.Text);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac1_ServiceLoad);
            ac.Post();

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
                this.textBox1.Text = r["IOID"].ToString();
                this.textBox2.Text = r["RECIPELENGTH"].ToString();
                this.textBox3.Text = r["IONAME"].ToString();
                this.textBox4.Text = r["PYCODE"].ToString();
                this.textBox5.Text = r["WBCODE"].ToString();
                this.textBox6.Text = r["MEMO"].ToString();
                this.textBox7.Text = r["USERID"].ToString();
                this.textBox8.Text = r["USERNAME"].ToString();
                this.textBox9.Text = r["RECDATE"].ToString();
                this.textBox10.Text = r["CHOSCODE"].ToString();
                this.textBox11.Text = r["RECIPECODE"].ToString();
                this.comboBox1.SelectedIndex = Convert.ToInt32(r["IFUSE"]);
                this.comboBox2.SelectedIndex = Convert.ToInt32(r["RECIPEYEAR"]);
                this.comboBox3.SelectedIndex = Convert.ToInt32(r["RECIPEMONTH"]);
                this.comboBox4.SelectedIndex = Convert.ToInt32(r["IOFLAG"]);
                this.comboBox5.SelectedIndex = Convert.ToInt32(r["USEST"]);
                this.comboBox6.SelectedIndex = Convert.ToInt32(r["USEND"]);
                this.comboBox7.SelectedIndex = Convert.ToInt32(r["USERD"]);
                this.comboBox8.SelectedIndex = Convert.ToInt32(r["OPFLAG"]);
                this.comboBox9.SelectedIndex = Convert.ToInt32(r["IFDEFAULT"]);
            }
        }
        void InOutName_TextChanged(object sender, EventArgs e)
        {
            string n = this.textBox3.Text.Trim();
            if (n.Length > 0)
            {

                this.textBox4.Text = PyWbCode.getPyCode(n).ToLower();
                this.textBox5.Text = PyWbCode.getWbCode(n).ToLower();
            }
        }
        private void InOutForm_Load(object sender, EventArgs e)
        {
            this.textBox3.TextChanged += new EventHandler(InOutName_TextChanged);

                this.textBox1.Text = r["IOID"].ToString();
                this.textBox2.Text = r["RECIPELENGTH"].ToString();
                this.textBox3.Text = r["IONAME"].ToString();
                this.textBox4.Text = r["PYCODE"].ToString();
                this.textBox5.Text = r["WBCODE"].ToString();
                this.textBox6.Text = r["MEMO"].ToString();
                this.textBox7.Text = r["USERID"].ToString();
                this.textBox8.Text = r["USERNAME"].ToString();
                this.textBox9.Text = r["RECDATE"].ToString();
                this.textBox10.Text = r["CHOSCODE"].ToString();
                this.textBox11.Text = r["RECIPECODE"].ToString();
                this.comboBox1.SelectedIndex = Convert.ToInt32(r["IFUSE"]);
                this.comboBox2.SelectedIndex = Convert.ToInt32(r["RECIPEYEAR"]);
                this.comboBox3.SelectedIndex = Convert.ToInt32(r["RECIPEMONTH"]);
                this.comboBox4.SelectedIndex = Convert.ToInt32(r["IOFLAG"]);
                this.comboBox5.SelectedIndex = Convert.ToInt32(r["USEST"]);
                this.comboBox6.SelectedIndex = Convert.ToInt32(r["USEND"]);
                this.comboBox7.SelectedIndex = Convert.ToInt32(r["USERD"]);
                this.comboBox8.SelectedIndex = Convert.ToInt32(r["OPFLAG"]);
                this.comboBox9.SelectedIndex = Convert.ToInt32(r["IFDEFAULT"]);


        }
    }
}
