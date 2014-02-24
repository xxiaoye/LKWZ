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
using YtWinContrl.com.datagrid;

namespace BusinessManag.form
{
    public partial class DiaoBoEdit : Form
    {
        private int isEdit = 0;
        private Dictionary<string, string> outInfo;
        
        TvList dwList=null;
        int isAllowFKC = 0;
        int InitSuccess = 0;
        DataTable DW = null;
        string ChuKuOrder="";
        string KuFangJiBie = "";
        IAppContent app;
        string preValue = null;
        string preText = null;

        YtWinContrl.com.contrl.SelTextInpt Wz =null;
        public DiaoBoEdit(Dictionary<string, string> info, int isEdit,IAppContent app)
        {
            this.app=app;
            this.isEdit = isEdit;
            outInfo = info;
            InitializeComponent();
            this.selTextInpt1.SelParam = His.his.Choscode + "|0|2|2|2" + "|{key}|{key}|{key}|{key}|{key}|{key}";  //0表示选择不选择本级库房，1表示选择本级库房，2表示对本级库房不做限制
            this.selTextInpt2.SelParam = His.his.Choscode + "|0|2|2|2" + "|{key}|{key}|{key}|{key}|{key}|{key}";
            this.selTextInpt3.SelParam = "1|1|1|" + His.his.Choscode+"|{key}|{key}|{key}|{key}";
            this.dataGView1.EntHh = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e) //新增物质
        {
 
            if (this.selTextInpt1.Value == null || this.selTextInpt1.Value == "")
            {
                WJs.alert("请选择出库库房！");
                return;
            }
            //改变查询条件 这里是因为查询Opt中需要a.cHosCode=? and 仓库编号 = ? 参数进行查询
            this.dataGView1.GetSql("物资").Ps = His.his.Choscode + "|" + this.selTextInpt1.Value + "|" + isAllowFKC.ToString() + "|{key}|{key}|{key}|{key}|{key}|{key}";
           
            //添加新行默认值
            Dictionary<string, object> de = new Dictionary<string, object>();
            de["数量"] = 0;
            this.dataGView1.AddRow(de, 0);


        }
        private void toolStripButton2_Click(object sender, EventArgs e) //删除物质
        {
            if (this.dataGView1.CurrentRow!=null)
            {
                //this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                this.dataGView1.CurrentRow.Visible = false;
                this.textBox1.Text = this.dataGView1.Sum("出库金额").ToString();
                this.textBox2.Text = this.dataGView1.Sum("入库金额").ToString();
            }
        }


        private bool checkNum(string wzId,string liuShuiId)
        {
            int num = 0;
            for (int i = 0; i < this.dataGView1.Rows.Count; i++)
            {
                if (this.dataGView1.Rows[i].Visible == true)
                {
                    DataGridViewCellCollection r = this.dataGView1.Rows[i].Cells;
                    if (r["WzId"].Value != null && r["STOCKFLOWID"].Value != null && r["WzId"].Value.ToString() == wzId && r["STOCKFLOWID"].Value.ToString() == liuShuiId)
                    {
                        //num += Convert.ToInt32(r["ShuLiang"].Value);
                        if (r["DanWei"].Value == r["DanWeiC2"])
                        {
                            num += Convert.ToInt32(r["ShuLiang"].Value) * Convert.ToInt32(r["ChangRate"].Value);
                        }
                        else
                        {
                            num += Convert.ToInt32(r["ShuLiang"].Value);
                        }
                        if (num > Convert.ToInt32(r["YuLiang"].Value))
                        {
                            WJs.alert("从第" + (i + 1).ToString() + "行起，与库存流水" + liuShuiId + "中的" + r["WuZi"].Value.ToString() + "已经超出此流水的最大余量，请为第" + (i + 1).ToString() + "行后的此物质，选择其他流水");
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private string getSerializationXb()
        {
            string xbNeiRong = "";
            for (int i = 0; i < this.dataGView1.Rows.Count ; i++)
            {
                DataGridViewCellCollection r = this.dataGView1.Rows[i].Cells;
                if (r != null)
                {
                    //if (r["Column45"].Value == null || r["Column45"].Value.ToString() != "Delete")
                    if(this.dataGView1.Rows[i].Visible==true)
                    {
                        int num = 0;
                        if (r["LiuShuiNum"].Value != null)
                        {
                            xbNeiRong += "1|";
                            xbNeiRong += r["LiuShuiNum"].Value.ToString() + "|";
                            xbNeiRong += r["ChuKuId"].Value.ToString() + "|";
                        }
                        else
                        {
                            xbNeiRong += "2|";
                        }
                        if (r["WzId"].Value == null || r["WzId"].Value.ToString().Trim() == "")
                        {
                            WJs.alert("第"+(i+1)+"项，请输入物资！");
                            return null;
                        }
                        
                       

                        if (r["STOCKFLOWID"].Value == null || r["STOCKFLOWID"].Value.ToString().Trim() == "")
                        {
                            WJs.alert("第" + (i + 1) + "项，请选择库存流水！");
                            return null;
                        }

                        if (r["ShuLiang"].Value == null || !WJs.IsZs(r["ShuLiang"].Value.ToString()) || Convert.ToInt32(r["ShuLiang"].Value.ToString())<1)
                        {
                            WJs.alert("第" + (i + 1) + "项，数量请输入整数！");
                            return null;
                        }

                        if (!checkNum(r["WzId"].Value.ToString(), r["STOCKFLOWID"].Value.ToString()))
                            return null;
                        //num = Convert.ToInt32(r["ShuLiang"].Value);
                        //if (r["DanWei"].Value == r["DanWeiC2"])
                        //{
                        //    num *= Convert.ToInt32(r["ChangRate"].Value);
                        //}
                        //if (num > Convert.ToInt32(r["YuLiang"].Value))
                        //{
                        //    WJs.alert("超出流水的最大剩余数量，请修改");
                        //    return null;
                        //}


                        xbNeiRong += r["WzId"].Value.ToString() + "|";

                        xbNeiRong += r["ShuLiang"].Value.ToString() + "|";

                        xbNeiRong += r["DanWei"].Value.ToString() + "|";


                        xbNeiRong += r["ChuKuDanJia"].Value.ToString() + "|";

                        xbNeiRong += r["ChuKuJinE"].Value.ToString() + "|";

                        xbNeiRong += r["RuKuDanJia"].Value.ToString() + "|";

                        xbNeiRong += r["RuKuJinE"].Value.ToString() + "|";
                        if (r["BeiZhu"].Value == null)
                            xbNeiRong += " |";
                        else
                            xbNeiRong += r["BeiZhu"].Value.ToString().Replace('~',' ').Replace('|',' ') + "|";

                        xbNeiRong += r["TiaoXingMa"].Value.ToString() + "|";
                        xbNeiRong += r["Choscode"].Value.ToString() + "|";


                        xbNeiRong += r["STOCKFLOWID"].Value.ToString() + "|";

                        xbNeiRong += r["ShengChanPiHao"].Value.ToString() + "|";
                        xbNeiRong += r["PiZhunWenHao"].Value.ToString() + "|";
                        xbNeiRong += r["ShengChanChangJiaId"].Value.ToString() + "|";
                        xbNeiRong += r["ShengChanChangJiaName"].Value.ToString() + "|";
                        xbNeiRong += r["ShengChanRiQi"].Value.ToString() + "|";
                        xbNeiRong += r["YouXiaoQi"].Value.ToString() + "|";
                        xbNeiRong += r["WeiShengXuKeZhengHao"].Value.ToString() + "~";

                    }
                    else if (r["LiuShuiNum"].Value != null)
                    {
                        xbNeiRong += "3|";
                        xbNeiRong += r["LiuShuiNum"].Value.ToString() + "~";
                    }
                }
            }
            if (xbNeiRong != "")
            {
                xbNeiRong = xbNeiRong.Remove(xbNeiRong.Length - 1);
            }

            return xbNeiRong;
        }
        private void SaveData()
        {
            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkwz.WZDiaoBo.SaveDiaoBo";
            if (outInfo==null)
            {
                ac.Sql = "AddOutMain";
                ac.Add("OUTID", null);
                ac.Add("OUTDATE", null);

            }
            else
            {
                ac.Sql = "UpdateOutMain";
                ac.Add("OUTID",  Convert.ToInt32(outInfo["outID"]));
                ac.Add("OUTDATE", Convert.ToDateTime(outInfo["outDate"]));//幅度段更改

            }
            if (this.selTextInpt3.Value == null)
            {
                WJs.alert("请输入出入库方式");
                return;
            }
            else
                ac.Add("IOID", Convert.ToInt32(this.selTextInpt3.Value));//字符串


            if (outInfo == null)
            {
                ActionLoad ld = ActionLoad.Conn();
                DataTable tb = ld.Find("DbEditFindInOutKindById", new object[] { this.selTextInpt3.Value.ToString() });

                DataRow r = tb.Rows[0];
                //this.dataGView1.Rows[rowIndex].Cells["Column11"].Value = r[0]["OPFLAG"];
                string recipe = r["RECIPECODE"].ToString();
                int rL = Convert.ToInt32(r["RECIPELENGTH"]);
                if (r["RECIPEYEAR"].ToString() == "1")
                {
                    recipe += DateTime.Now.Year.ToString("D4");
                }
                if (r["RECIPEMONTH"].ToString() == "1")
                {
                    recipe += DateTime.Now.Month.ToString("D2");
                }
                rL = rL - recipe.Length;
                recipe = "D" + rL.ToString() + "_" + recipe;
                this.textBox3.Text = recipe;
                //this.dataGView1.Rows[rowIndex].Cells["Column3"].Value = recipe;
            }
            ac.Add("RECIPECODE", this.textBox3.Text);  //服务端更改 //字符串


            if (this.selTextInpt1.Value == null)
            {
                WJs.alert("请输入出库库房！");
                return;
            }
            else
            ac.Add("WARECODE", this.selTextInpt1.Value.ToString());  //字符串

            if (this.selTextInpt2.Value == null)
            {
                WJs.alert("请输入目的库房！");
                return;
            }
            else
            ac.Add("TARGETWARECODE", this.selTextInpt2.Value.ToString()); //字符串


            
            ac.Add("TOTALMONEY", this.textBox1.Text);//数字
            ac.Add("LSTOTALMONEY", this.textBox2.Text); //数字
            ac.Add("STATUS", 1);
            ac.Add("MEMO", this.textBox4.Text.Replace('~', ' ').Replace('|', ' '));
            ac.Add("OPFLAG", 1);
            ac.Add("USERID", His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("RECDATE", DateTime.Now);  //服务端更改
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("XiBiaoData", getSerializationXb());
            if (ac.Param["XiBiaoData"] != null)
            {
                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac5_ServiceLoad);
                ac.Post();
                this.Close();
            }
        }

        void ac5_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {


            string[] r = e.Msg.Msg.Split('|');
            WJs.alert(r[0]);
            if (r.Length == 4)
            {
                this.textBox3.Text = r[2];
                if (outInfo == null)
                {
                    outInfo = new Dictionary<string, string>();
                    outInfo.Add("outID", r[1]);
                    outInfo.Add("outDate", r[3]);
                }
                this.dataGView1.ClearData();
                this.loadXbData();
                

            }
          

        }
        private void toolStripButton1_Click(object sender, EventArgs e)  //保存
        {
            if (WJs.confirm("确认保存？"))
            {
                SaveData();
                
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e) //取消
        {
            if (WJs.confirm("取消后，修改后的信息将不会保存，确认取消？"))
            {
                this.Close();
            }
        }



        # region//当为编辑时，加载数据
        private void loadData()
        {
            this.selTextInpt2.Value = outInfo["MuDiKuFangCode"];
            this.selTextInpt2.Text = outInfo["MuDiKuFangName"];
            this.selTextInpt1.Value = outInfo["ChuKuKuFangCode"];
            this.selTextInpt1.Text = outInfo["ChuKuKuFangName"];
            this.selTextInpt3.Value = outInfo["ChuKuFangShiCode"];
            this.selTextInpt3.Text = outInfo["ChuKuFangShiName"];
            this.textBox4.Text = outInfo["BeiZhu"];
            this.textBox1.Text = outInfo["ChuKuZongJinE"];
            this.textBox1.Text = outInfo["RuKuZongJinE"];
            this.textBox3.Text=outInfo["DanJuHao"];
            ActionLoad ld = ActionLoad.Conn();
            DataTable tb = ld.Find("DbEditFindWareById", new object[] { this.selTextInpt1.Value.ToString() });
            if (tb != null && tb.Rows.Count > 0)
            {
                if (tb.Rows[0]["OUTORDER"].ToString() == "1")
                {
                    ChuKuOrder = "DbEditFindStockDetailOrderByP";
                }
                else if (tb.Rows[0]["OUTORDER"].ToString() == "2")
                {
                    ChuKuOrder = "DbEditFindStockDetailOrderByI";
                }
                else if (tb.Rows[0]["OUTORDER"].ToString() == "3")
                {
                    ChuKuOrder = "DbEditFindStockDetailOrderByG";
                }
                else if (tb.Rows[0]["OUTORDER"].ToString() == "4")
                {
                    ChuKuOrder = "DbEditFindStockDetailOrderByV";
                }
                KuFangJiBie="|" + tb.Rows[0]["IFSTWARE"].ToString() + "|" + tb.Rows[0]["IFNDWARE"].ToString() + "|" + tb.Rows[0]["IFRDWARE"].ToString();
            }
            this.selTextInpt2.SelParam = His.his.Choscode + "|" + this.selTextInpt1.Value.ToString() + KuFangJiBie + "|{key}|{key}|{key}|{key}";
            
            loadXbData();
        }
        private void loadXbData()
        {
            ActionLoad ld = ActionLoad.Conn();
            ld.Action = "Find";
            ld.Sql = "DbEditFindOutDetailList";
            ld.SetParams(new object[] {outInfo["outID"] });
            ld.ServiceLoad += new YtClient.data.events.LoadEventHandle(fillXbData);
            ld.Post();
        }
        private void fillXbData(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.GetDataTable() != null)
            {
                if (e.Msg.GetDataTable().Rows.Count > 0)
                {
                    //DataRowCollection rInfo = e.Msg.GetDataTable().Rows;
                    //for (int i = 0; i < rInfo.Count; i++)
                    foreach(DataRow v in e.Msg.GetDataTable().Rows)
                    {
                        if (v != null)
                        {
                            Dictionary<string, object> d = new Dictionary<string, object>();
                            DataGridViewRow r = this.dataGView1.AddRow(d, 0);

                            r.Cells["WuZi"].Value = v["WZNAME"];
                            r.Cells["WzId"].Value = v["WZID"];
                            r.Cells["DanWeiC1"].Value = v["DanWeiC1"];
                            
                            r.Cells["KuCunLiuShui"].Value = v["STOCKFLOWNO"];
                            r.Cells["ShuLiang"].Value = v["NUM"];
                            r.Cells["STOCKFLOWID"].Value = v["STOCKFLOWNO"];
                            fillStockRelated(r);
                            r.Cells["DanWeiC2"].Value = v["DanWeiC2"];
                            //r.Cells["DanWei1"].Value = v["DanWei1"];
                            //r.Cells["DanWei2"].Value = v["DanWei1"];

                            r.Cells["DanWei"].Value = v["UNITCODE"];

                            r.Cells["ChuKuDanJia"].Value = v["PRICE"];
                            r.Cells["ChuKuJinE"].Value = v["MONEY"];
                            r.Cells["RuKuDanJia"].Value = v["INPRICE"];
                            r.Cells["RuKuJinE"].Value = v["INMONEY"];
                            r.Cells["TiaoXingMa"].Value = v["TXM"];
                            r.Cells["Choscode"].Value = v["CHOSCODE"];
                            r.Cells["BeiZhu"].Value = v["MEMO"];
                            r.Cells["ShengChanPiHao"].Value = v["PH"];
                            r.Cells["PiZhunWenHao"].Value = v["PZWH"];
                            r.Cells["ShengChanChangJiaId"].Value = v["SUPPLYID"];
                            r.Cells["ShengChanChangJiaName"].Value = v["SUPPLYNAME"];
                            r.Cells["ShengChanRiQi"].Value = v["PRODUCTDATE"];
                            r.Cells["YouXiaoQi"].Value = v["VALIDDATE"];
                            r.Cells["WeiShengXuKeZhengHao"].Value = v["WSXKZH"];
                           
                            
                            r.Cells["LiuShuiNum"].Value = v["DETAILNO"];
                            r.Cells["ChuKuId"].Value = v["OUTID"];
                            //r.Cells["ChangeRate"] = v[""];

   

                            
                            //r.Cells["YuLiang"].Value = v[""];
                            //r.Cells["XbStatus"].Value = v[""];
                            
                        }
                    }
                }
            }
        }
        private void setDefalutStock(DataRow v,DataGridViewRow r)
        {
            if (v != null)
            {
                //          if (r.Cells["STOCKFLOWID"].Value == null)
                {
                    //r.Cells["WuZi"] =v["STOCKFLOWNO"];
                    r.Cells["KuCunLiuShui"].Value = v["库存流水"];
                    //r.Cells["ShuLiang"] = v["UNITCODE"];
                    //r.Cells["DanWei"] = v["PRICE"];
                    r.Cells["ChuKuDanJia"].Value = v["出库单价"];
                    //r.Cells["ChuKuJinE"] = v["INPRICE"];
                    r.Cells["RuKuDanJia"].Value = v["入库单价"];
                    //r.Cells["RuKuJinE"].Value = v["TXM"];
                    r.Cells["TiaoXingMa"].Value = v["条形码"];
                    //r.Cells["Choscode"].Value = v["MEMO"];
                    r.Cells["BeiZhu"].Value = v["备注"];
                    r.Cells["ShengChanPiHao"].Value = v["生产批号"];
                    r.Cells["PiZhunWenHao"].Value = v["批准文号"];
                    r.Cells["ShengChanChangJiaId"].Value = v["生产厂家ID"];
                    r.Cells["ShengChanChangJiaName"].Value = v["生产厂家名称"];
                    r.Cells["ShengChanRiQi"].Value = v["生产日期"];
                    r.Cells["YouXiaoQi"].Value = v["有效期"];
                    r.Cells["WeiShengXuKeZhengHao"].Value = v["卫生许可证号"];
                    //r.Cells["WzId"] = v["STOCKFLOWNO"];
                    r.Cells["STOCKFLOWID"].Value = v["库存流水号"];
                    //r.Cells["LiuShuiNum"] = v["OUTID"];
                    //r.Cells["ChuKuId"] = v[""];
                    //r.Cells["ChangeRate"] = v["换算系数"];
                    //r.Cells["DanWei1"] = v["DanWeiC2"];
                    //r.Cells["DanWei2"].Value = v["余量"];
                    //r.Cells["DanWeiC1"].Value = v["余量"];
                    //r.Cells["DanWeiC2"].Value = v["余量"];
                    //r.Cells["YuLiang"].Value = v["余量"];
                    //r.Cells["XbStatus"].Value = v["余量"];
//                    select NUM,UNITCODE,STOCKFLOWNO from WZOUTDETAIL a1,(select outid from WZOUTMAIN where status>0 and status <6 and WARECODE='04') a2 where a1.outid=a2.outid 
//union all 
//select NUM,UNITCODE,STOCKFLOWNO from WZPLANDETAIL b1,(select planid  from WZPLANMAIN where status>0 and status <6 and IFPLAN=0 and TARGETWARECODE='04' ) b2 where b1.planid=b2.planid
                }
                r.Cells["ChangeRate"].Value = v["换算系数"];
                r.Cells["YuLiang"].Value = v["余量"];
            }
            else
            {
                r.Cells["KuCunLiuShui"].Value = null;
                r.Cells["ChuKuDanJia"].Value = "0.0000";
                r.Cells["RuKuDanJia"].Value = "0.0000";
                r.Cells["TiaoXingMa"].Value = null;
                r.Cells["BeiZhu"].Value = null;
                r.Cells["ShengChanPiHao"].Value = null;
                r.Cells["PiZhunWenHao"].Value = null;
                r.Cells["ShengChanChangJiaId"].Value = null;
                r.Cells["ShengChanChangJiaName"].Value = null;
                r.Cells["ShengChanRiQi"].Value = null;
                r.Cells["YouXiaoQi"].Value = null;
                r.Cells["WeiShengXuKeZhengHao"].Value = null;
                r.Cells["STOCKFLOWID"].Value = null;
                r.Cells["ChangeRate"].Value = "1";
                r.Cells["YuLiang"].Value ="0";
            }
        }
        private void fillStockRelated(DataGridViewRow r)
        {
            ActionLoad ld = ActionLoad.Conn();
            DataTable tb = ld.Find("DbEditFindStockDetailById", new object[] { r.Cells["STOCKFLOWID"].Value });
            if (tb != null && tb.Rows.Count > 0)
            {
                setDefalutStock(tb.Rows[0], r);
            }
        }
        #endregion
       
        private void DiaoBoEdit_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            string setinput = "物资";
            this.dataGView1.addSql("DbEditFindWZ", setinput, "");
            this.dataGView1.addSql("DbEditFindStockDetail", "库存流水", "");
            dwList = TvList.newBind().SetCacheKey("WzDw").Load("DbEditFindWzUnitList", new object[] { });
            dwList.Bind();

            ActionLoad ld = ActionLoad.Conn();
            DW = ld.Find("DbEditFindWzUnitList", new object[] {});

            ActionLoad ld4 = ActionLoad.Conn();
            ld4.Action = "Find";
            ld4.Sql = "DbEditFindSysValue";
            ld4.SetParams(new object[] { "2103", His.his.Choscode });
            ld4.ServiceLoad += new YtClient.data.events.LoadEventHandle(ld1_ServiceLoad);
            ld4.Post();

            if (outInfo != null)
            {
                loadData();
            }
            if (isEdit == 0)
            {
                //浏览
                //this.toolStrip1.Enabled = false;
                for (int i = 0; i < this.toolStrip1.Items.Count; i++)
                {
                    this.toolStrip1.Items[i].Enabled = false;
                }
                this.toolStripButton5.Enabled = true;

                this.selTextInpt1.Enabled = false;
                this.selTextInpt2.Enabled = false;
                this.selTextInpt3.Enabled = false;
                this.textBox4.Enabled = false;
                //this.dataGView1.Enabled = false;
                this.dataGView1.ReadOnly = true;
            }
            this.selTextInpt1.TextChanged += new EventHandler(this.selTextInpt1_Leave);

            InitSuccess = 1;
            
        }
        void ld1_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.GetDataTable() != null)
            {
                if (e.Msg.GetDataTable().Rows.Count > 0)
                {
                    isAllowFKC = Convert.ToInt32(e.Msg.GetDataTable().Rows[0]["SYSVALUE"]);
                }
            }
        }

        private void selTextInpt1_Leave(object sender, EventArgs e)
        {

            if (this.selTextInpt1.Value != null && this.selTextInpt1.Value != "")
            {
                if ( this.selTextInpt1.Value.ToString() != preValue)
                {
                    if (preValue != null)
                    {
                        if (WJs.confirm("更改出库库房将会删除所有细表信息，确认更改出库库房？"))
                        {
                            this.selTextInpt2.Value = null;
                            this.selTextInpt2.Text = null;

                            for (int i = 0; i < this.dataGView1.Rows.Count; i++)
                            {
                                this.dataGView1.Rows[i].Visible = false;
                            }
                        }
                        else
                        {
                            this.selTextInpt1.Value = preValue;
                            this.selTextInpt1.Text = preText;
                            
                            return;
                        }
                    }
                    preText = this.selTextInpt1.Text.ToString();
                    preValue = this.selTextInpt1.Value.ToString();
                    

                }
                if(this.selTextInpt1.SelectedDataRow!=null)
                {
                    if (this.selTextInpt1.SelectedDataRow["OUTORDER"].ToString() == "1")
                    {
                        ChuKuOrder = "DbEditFindStockDetailOrderByP";
                    }
                    else if (this.selTextInpt1.SelectedDataRow["OUTORDER"].ToString() == "2")
                    {
                        ChuKuOrder = "DbEditFindStockDetailOrderByI";
                    }
                    else if (this.selTextInpt1.SelectedDataRow["OUTORDER"].ToString() == "3")
                    {
                        ChuKuOrder = "DbEditFindStockDetailOrderByG";
                    }
                    else if (this.selTextInpt1.SelectedDataRow["OUTORDER"].ToString() == "4")
                    {
                        ChuKuOrder = "DbEditFindStockDetailOrderByV";
                    }
                    KuFangJiBie = "|" + this.selTextInpt1.SelectedDataRow["IFSTWARE"].ToString() + "|" + this.selTextInpt1.SelectedDataRow["IFNDWARE"].ToString() + "|" + this.selTextInpt1.SelectedDataRow["IFRDWARE"].ToString();
                }
                this.selTextInpt2.SelParam = His.his.Choscode + "|" + this.selTextInpt1.Value.ToString() + KuFangJiBie + "|{key}|{key}|{key}|{key}";//"|" + this.selTextInpt1.SelectedDataRow["IFSTWARE"].ToString() + "|" + this.selTextInpt1.SelectedDataRow["IFRDWARE"].ToString() + "|" + this.selTextInpt1.SelectedDataRow["IFNDWARE"].ToString() + "|{key}|{key}|{key}|{key}";
                this.dataGView1.GetSql("物资").Ps = His.his.Choscode + "|" + this.selTextInpt1.Value + "|" + isAllowFKC.ToString() + "|{key}|{key}|{key}|{key}|{key}|{key}";

            
            }

        }

        private void updateMoney(int row)
        {
            DataGridViewRow r=this.dataGView1.Rows[row];
            if (r.Cells["ShuLiang"].Value != null && WJs.IsZs(r.Cells["ShuLiang"].Value.ToString()))
            {
                r.Cells["ChuKuJinE"].Value = (Convert.ToDouble(r.Cells["ChuKuDanJia"].Value) * Convert.ToDouble(r.Cells["ShuLiang"].Value)).ToString("f4");
                r.Cells["RuKuJinE"].Value = (Convert.ToDouble(r.Cells["RuKuDanJia"].Value) * Convert.ToDouble(r.Cells["ShuLiang"].Value)).ToString("f4");
                this.textBox1.Text = this.dataGView1.Sum("出库金额").ToString("f4");; ;
                this.textBox2.Text = this.dataGView1.Sum("入库金额").ToString("f4");
            }
            else
            {
                r.Cells["ChuKuJinE"].Value =0.ToString("f4");
                r.Cells["RuKuJinE"].Value = 0.ToString("f4");
                this.textBox1.Text = 0.ToString("f4");
                this.textBox2.Text = 0.ToString("f4");
            }
        }
        private void updateJiaGe(int row)
        {
            DataGridViewRow r = this.dataGView1.Rows[row];
            if (r.Cells["DanWei"].Value.ToString() != r.Cells["DanWeiC1"].Value.ToString())
            {
                if (r.Cells["DanWei"].Value.ToString() != r.Cells["DanWeiC2"].Value.ToString())
                {
                    r.Cells["ChuKuDanJia"].Value = (Convert.ToDouble(r.Cells["ChuKuDanJia"].Value) / Convert.ToDouble(r.Cells["ChangeRate"].Value)).ToString("f4");
                    r.Cells["RuKuDanJia"].Value = (Convert.ToDouble(r.Cells["RuKuDanJia"].Value) / Convert.ToDouble(r.Cells["ChangeRate"].Value)).ToString("f4");
                }
                else
                {
                    r.Cells["ChuKuDanJia"].Value = (Convert.ToDouble(r.Cells["ChuKuDanJia"].Value) *Convert.ToDouble(r.Cells["ChangeRate"].Value)).ToString("f4");
                    r.Cells["RuKuDanJia"].Value = (Convert.ToDouble(r.Cells["RuKuDanJia"].Value) * Convert.ToDouble(r.Cells["ChangeRate"].Value)).ToString("f4");
                }
                r.Cells["DanWeiC1"].Value = r.Cells["DanWei"].Value.ToString();
            }
           
        }
        private void dataGView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == WzId.Index)  //更改物质
            {
                ActionLoad ld = ActionLoad.Conn();
                DataTable tb = null;
                tb = ld.Find(ChuKuOrder, new object[] { this.selTextInpt1.Value, this.dataGView1.Rows[e.RowIndex].Cells["WzId"].Value });
                if (tb != null && tb.Rows.Count > 0)
                {
                    setDefalutStock(tb.Rows[0], this.dataGView1.CurrentRow);
                }
            }
            else if (e.ColumnIndex == ShuLiang.Index || e.ColumnIndex==RuKuDanJia.Index)
            {
                updateMoney(e.RowIndex);
            }
            else if (e.ColumnIndex == DanWei.Index)
            {
                updateJiaGe(e.RowIndex);
            }
            else if (e.ColumnIndex == DanWeiC2.Index)
            {
                string hsxs = "";
                TvList tv = TvList.newBind();
                ((DataGridViewComboBoxCell)this.dataGView1.Rows[e.RowIndex].Cells["DanWei"]).Items.Clear();
                DataRow[] dw=DW.Select("DICID='" + this.dataGView1.Rows[e.RowIndex].Cells["DanWeiC1"].Value.ToString()+"'");
                if (dw != null)
                {
                    tv.add(dw[0]["DICDESC"].ToString(), this.dataGView1.Rows[e.RowIndex].Cells["DanWeiC1"].Value.ToString());
                    hsxs += "=" + this.dataGView1.Rows[e.RowIndex].Cells["ChangeRate"].Value.ToString() + dw[0]["DICDESC"].ToString();
                }
                //tv.add(this.dataGView1.Rows[e.RowIndex].Cells["DanWei1"].Value.ToString(), this.dataGView1.Rows[e.RowIndex].Cells["DanWeiC1"].Value.ToString());
                dw = DW.Select("DICID='" + this.dataGView1.Rows[e.RowIndex].Cells["DanWeiC2"].Value.ToString()+"'");
                if (dw != null)
                {
                    tv.add(dw[0]["DICDESC"].ToString(), this.dataGView1.Rows[e.RowIndex].Cells["DanWeiC2"].Value.ToString());
                    hsxs = "1" + dw[0]["DICDESC"].ToString() + hsxs;
                }
                
                //tv.add(this.dataGView1.Rows[e.RowIndex].Cells["DanWei2"].Value.ToString(), this.dataGView1.Rows[e.RowIndex].Cells["DanWeiC2"].Value.ToString());
                tv.Bind((DataGridViewComboBoxCell)this.dataGView1.Rows[e.RowIndex].Cells["DanWei"]);
                this.dataGView1.Rows[e.RowIndex].Cells["DanWei"].Value = this.dataGView1.Rows[e.RowIndex].Cells["DanWeiC1"].Value;
            //}
            //else if (e.ColumnIndex == ChangeRate.Index)
            //{
                this.dataGView1.Rows[e.RowIndex].Cells["BZXS"].Value = hsxs;//"1" + dw[0]["DICDESC"].ToString() + "=" + this.dataGView1.Rows[e.RowIndex].Cells["ChangeRate"].Value.ToString() + this.dataGView1.Rows[e.RowIndex].Cells["DanWei1"].Value.ToString();
            }
        }
        private void dataGView1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs ee)
        {
            
        }
        private void dataGView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (InitSuccess == 0)
                return;
            int CMI=this.dataGView1.CurrentCell.ColumnIndex;
            if (CMI == ShuLiang.Index || CMI == DanWei.Index)
            {
                this.dataGView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dataGView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (InitSuccess == 0)
                return;
            if (e.ColumnIndex == KuCunLiuShui.Index)
            {
                this.dataGView1.GetSql("库存流水").Ps = this.selTextInpt1.Value + "|" + this.dataGView1.Rows[e.RowIndex].Cells["WzId"].Value;
            }
            else if (e.ColumnIndex == WuZi.Index)
            {
                this.dataGView1.GetSql("物资").Ps = His.his.Choscode + "|" + this.selTextInpt1.Value + "|" + isAllowFKC.ToString() + "|{key}|{key}|{key}|{key}|{key}|{key}";
            }
        }
        
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (outInfo != null)
            {
                ActionLoad ld = ActionLoad.Conn();
                DataTable tb = ld.Find("OutMainDaYinQuery", new object[] { outInfo["outID"] });
                if (tb != null && tb.Rows.Count > 0)
                {
                   // setDefalutStock(tb.Rows[0], r);
                
                    Dictionary<string, object> pp = new Dictionary<string, object>();
                    pp.Add("BiaoTi", tb.Rows[0][0].ToString() + "  【" + outInfo["ChuKuKuFangName"] + "】  " + "调拨单");
                    pp.Add("FangXiang", "由  【" + outInfo["ChuKuKuFangName"] + "】  调往  【" + outInfo["MuDiKuFangName"] + "】");
                    pp.Add("Time", "时间："+tb.Rows[0][5].ToString());
                    pp.Add("LeiBie", "类别：调拨单");
                    pp.Add("BianHao", "单据号："+tb.Rows[0][8].ToString());
                    //pp.Add("HeJi", "合计");
                    pp.Add("JinE1", tb.Rows[0][9].ToString());
                    pp.Add("JinE2", tb.Rows[0][10].ToString());
                    pp.Add("Name1", "操作员："+tb.Rows[0][1].ToString());
                    pp.Add("Name2", "审核员：" + tb.Rows[0][2].ToString());
                    pp.Add("Name3", "");
                    pp.Add("Name4", "出库员：" + tb.Rows[0][3].ToString());
                    pp.Add("HeJi","备注："+tb.Rows[0]["MEMO"].ToString());
                    pp.Add("ID", outInfo["outID"]);

                    app.LoadPlug("RepEdit.RepView", new object[] { "CLWZDBDYCS001", pp, false }, false);
               }
            }
            else
            {
                MessageBox.Show("请先保存数据！");
            }
           
        }

        private void dataGView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           
        }

        private void dataGView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            label8.Text = "共:" + this.dataGView1.Rows.GetRowCount(DataGridViewElementStates.Visible) + "条";
        }


    }
}
