using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using YtService.action;
using YtService.util;
using YtService.config;

namespace LKWZSVR.lkwz.WZShenLing
{
    public class SaveShenLing : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            
            if (data.Sql == null)
            {
                throw new Exception("Sql内容为空！");
            }
            else if (data.Sql.Equals("AddPlanMain"))
            {
                data.Param["PLANID"] = DaoTool.Seq(dao, "LKWZ.SEQWZPlan");
                data.Param["PLANDATE"] = DateTime.Now;
                data.Param["RECDATE"] = DateTime.Now;

                string[] XbTable = data.Param["XiBiaoData"].ToString().Split('~'); ;

                if (DaoTool.Save(dao, OptContent.get("SlSaveWzPlanMainInfo"), data) < 0)
                {
                    throw new Exception("主表保存失败！");
                }
                foreach (string r in XbTable)
                {
                    YtService.data.OptData data1=new YtService.data.OptData();
                    data1.Param = new Dictionary<string, object>();
                    string[] xbValue = r.Split('|');
                    //if (xbValue[0] == "2")
                    {
                        data1.Param.Add("PLANID", data.Param["PLANID"]);
                        data1.Param.Add("ROWNO", Convert.ToInt32(xbValue[0]));
                        data1.Param.Add("WZID", Convert.ToInt32(xbValue[1]));//r["物质ID"]);
                        data1.Param.Add("NUM", Convert.ToInt32(xbValue[2]));//r["数量"]);
                        data1.Param.Add("UNITCODE", Convert.ToInt32(xbValue[3]));//r["单位"]);
                        data1.Param.Add("PRICE", Convert.ToDouble(xbValue[4]));//r["出库单价"]);
                        data1.Param.Add("MONEY", Convert.ToDouble(xbValue[5]));//r["出库金额"]);
                        data1.Param.Add("LSPRICE", Convert.ToDouble(xbValue[6]));//r["入库单价"]);
                        data1.Param.Add("LSMONEY", Convert.ToDouble(xbValue[7]));//r["入库金额"]);
                        data1.Param.Add("SCS", xbValue[8]);//r["备注"]);
                        data1.Param.Add("GYS", xbValue[9]);//r["条形码"]);
                        data1.Param.Add("MEMO", xbValue[10]);//; r["医疗机构编码"]);
                        data1.Param.Add("STOCKFLOWNO", Convert.ToInt32(xbValue[11]));// r["库存流水号"]);
                        data1.Param.Add("TXM", xbValue[12]);// r["生产批号"]);
                        data1.Param.Add("CHOSCODE", xbValue[13]);//r["批准文号"]);
                        if (DaoTool.Save(dao, OptContent.get("SlSaveWzPlanDetailInfo"), data1) < 0)
                        {
                            throw new Exception("细表保存失败！");
                        }
                    }
                }
                    //msg = "保存成功！";
                msg = "保存成功！" + "|" + data.Param["PLANID"].ToString() + "|" + data.Param["PLANDATE"].ToString();
                return "ok";
            }
            else if (data.Sql.Equals("UpdatePlanMain"))
            {
                //data.Param["PLANID"] = DaoTool.Seq(dao, "LKWZ.SEQWZPlan");
                //data.Param["PLANDATE"] = DateTime.Now;
                data.Param["RECDATE"] = DateTime.Now;

                string[] XbTable = data.Param["XiBiaoData"].ToString().Split('~'); ;

                if (DaoTool.Update(dao, OptContent.get("SlSaveWzPlanMainInfo"), data) < 0)
                {
                    throw new Exception("主表保存失败！");
                }
                dao.ExecuteNonQuery("delete from LKWZ.WZPLANDETAIL where PLANID=" + data.Param["PLANID"].ToString());
                foreach (string r in XbTable)
                {
                    YtService.data.OptData data1 = new YtService.data.OptData();
                    data1.Param = new Dictionary<string, object>();
                    string[] xbValue = r.Split('|');
                    //if (xbValue[0] == "2")
                    {
                        data1.Param.Add("PLANID", data.Param["PLANID"]);
                        data1.Param.Add("ROWNO", Convert.ToInt32(xbValue[0]));
                        data1.Param.Add("WZID", Convert.ToInt32(xbValue[1]));
                        data1.Param.Add("NUM", Convert.ToInt32(xbValue[2]));
                        data1.Param.Add("UNITCODE", Convert.ToInt32(xbValue[3]));
                        data1.Param.Add("PRICE", Convert.ToDouble(xbValue[4]));
                        data1.Param.Add("MONEY", Convert.ToDouble(xbValue[5]));
                        data1.Param.Add("LSPRICE", Convert.ToDouble(xbValue[6]));
                        data1.Param.Add("LSMONEY", Convert.ToDouble(xbValue[7]));
                        data1.Param.Add("SCS", xbValue[8]);
                        data1.Param.Add("GYS", xbValue[9]);
                        data1.Param.Add("MEMO", xbValue[10]);
                        data1.Param.Add("STOCKFLOWNO", Convert.ToInt32(xbValue[11]));
                        data1.Param.Add("TXM", xbValue[12]);
                        data1.Param.Add("CHOSCODE", xbValue[13]);
                        if (DaoTool.Save(dao, OptContent.get("SlSaveWzPlanDetailInfo"), data1) < 0)
                        {
                            throw new Exception("细表保存失败！");
                        }
                    }
                }
                //msg = "保存成功！";
                msg = "保存成功！" + "|" + data.Param["PLANID"].ToString() + "|" + data.Param["PLANDATE"].ToString();
                return "ok";
            }
            else if (data.Sql.Equals("ChangeShState"))
            {

                if (DaoTool.Update(dao, OptContent.get("SlUpdateShStatus"), data) < 0)
                {
                    throw new Exception("执行失败！");
                }
                msg = "执行成功！";
                return "ok";
            }
            else if (data.Sql.Equals("ChangeOutState"))
            {

                if (DaoTool.Update(dao, OptContent.get("SlUpdateOutStatus"), data) < 0)
                {
                    throw new Exception("执行失败！");
                }
                msg = "执行成功！";
                return "ok";
            }
            else if (data.Sql.Equals("QueRenRuKu"))
            {
                if (DaoTool.Update(dao, OptContent.get("SlUpdateInStatus"), data) < 0)
                {
                    throw new Exception("执行失败！");
                }

                DataRow PlanMian = dao.Fd("select * from LKWZ.WZPLANMAIN where PLANID=" + data.Param["PLANID"].ToString()).Rows[0];

                #region  添加入库主表
                DataRow InKind = dao.Fd("select * from LKWZ.DICTWZINOUT where IfUse=1 and IOFlag=0 and OPFlag=2 and CHOSCODE=" + data.Param["CHOSCODE"].ToString()).Rows[0];
                string recipe = InKind["RECIPECODE"].ToString();
                if (InKind["RECIPEYEAR"].ToString() == "1")
                {
                    recipe += DateTime.Now.Year.ToString("D4");
                }
                if (InKind["RECIPEMONTH"].ToString() == "1")
                {
                    recipe += DateTime.Now.Month.ToString("D2");
                }
                int recipeno = Convert.ToInt32(dao.ExecuteScalar(OptContent.get("DbGetInRecipeNo").Sql, new object[] { recipe + '%' })) + 1;
                recipe += recipeno.ToString("D" + (Convert.ToInt32(InKind["RECIPELENGTH"]) - recipe.Length).ToString());

                YtService.data.OptData data1 = new YtService.data.OptData();
                data1.Param = new Dictionary<string, object>();
                data1.Param.Add("INID", DaoTool.Seq(dao, "LKWZ.SEQWZIn"));
                data1.Param.Add("IOID", InKind["IOID"]);
                data1.Param.Add("WARECODE", PlanMian["WARECODE"]);
                data1.Param.Add("RECIPECODE", recipe);
                data1.Param.Add("TOTALMONEY", PlanMian["TOTALMONEY"]);
                data1.Param.Add("LSTOTALMONEY", PlanMian["LSTOTALMONEY"]);
                data1.Param.Add("INDATE", DateTime.Now);
                data1.Param.Add("STATUS", 6);
                data1.Param.Add("OPFLAG", 2);
                data1.Param.Add("MEMO", PlanMian["MEMO"]);
                data1.Param.Add("USERID", data.Param["SHINUSERID"]);
                data1.Param.Add("USERNAME", data.Param["SHINUSERNAME"]);
                data1.Param.Add("RECDATE", DateTime.Now);

                data1.Param.Add("SHDH", null);// 随货单号
                data1.Param.Add("INVOICECODE", null);//r["批准文号"]);
                data1.Param.Add("INVOICEDATE", null);// r["生产厂家ID"]);
                data1.Param.Add("SUPPLYNAME", null);// r["生产厂家名称"]);
                data1.Param.Add("SUPPLYID", null);// r["生产日期"]);

                data1.Param.Add("SHDATE", DateTime.Now);// r["有效期"]);
                data1.Param.Add("SHUSERID", data.Param["SHINUSERID"]);// r["卫生许可证号"]);
                data1.Param.Add("SHUSERNAME", data.Param["SHINUSERNAME"]);// r["卫生许可证号"]);
                data1.Param.Add("SHINDATE", DateTime.Now);// r["卫生许可证号"]);
                data1.Param.Add("SHINUSERID", data.Param["SHINUSERID"]);// r["卫生许可证号"]);
                data1.Param.Add("SHINUSERNAME", data.Param["SHINUSERNAME"]);// r["卫生许可证号"]);
                data1.Param.Add("PLANNO", PlanMian["PLANID"]);// r["卫生许可证号"]);
                data1.Param.Add("CHOSCODE", data.Param["CHOSCODE"]);// r["卫生许可证号"]);
                if (DaoTool.Save(dao, OptContent.get("SlSaveWZInMainInfo"), data1) < 0)
                {
                    throw new Exception("执行失败！");
                }
                #endregion

                #region 添加出库主表

                DataRow OutKind = dao.Fd("select * from LKWZ.DICTWZINOUT where IfUse=1 and IOFlag=1 and OPFlag=2 and CHOSCODE=" + data.Param["CHOSCODE"].ToString()).Rows[0];
                string recipe1 = InKind["RECIPECODE"].ToString();
                if (InKind["RECIPEYEAR"].ToString() == "1")
                {
                    recipe1 += DateTime.Now.Year.ToString("D4");
                }
                if (InKind["RECIPEMONTH"].ToString() == "1")
                {
                    recipe1 += DateTime.Now.Month.ToString("D2");
                }
                int recipeno1 = Convert.ToInt32(dao.ExecuteScalar(OptContent.get("DbGetOutRecipeNo").Sql, new object[] { recipe1 + '%' })) + 1;
                recipe1 += recipeno1.ToString("D" + (Convert.ToInt32(InKind["RECIPELENGTH"]) - recipe1.Length).ToString());

                YtService.data.OptData data0 = new YtService.data.OptData();
                data0.Param = new Dictionary<string, object>();
                data0.Param.Add("OUTID", DaoTool.Seq(dao, "LKWZ.SEQWZOut"));
                data0.Param.Add("IOID", OutKind["IOID"]);
                data0.Param.Add("RECIPECODE", recipe1);
                data0.Param.Add("WARECODE", PlanMian["TARGETWARECODE"]);
                data0.Param.Add("TARGETWARECODE", PlanMian["WARECODE"]);
                data0.Param.Add("TOTALMONEY", PlanMian["TOTALMONEY"]);
                data0.Param.Add("LSTOTALMONEY", PlanMian["LSTOTALMONEY"]);
                data0.Param.Add("OUTDATE", DateTime.Now);
                data0.Param.Add("STATUS", 6);
                data0.Param.Add("MEMO", PlanMian["MEMO"]);
                data0.Param.Add("OPFLAG", 2);
                data0.Param.Add("USERID", data.Param["SHINUSERID"]);// r["卫生许可证号"]);
                data0.Param.Add("USERNAME", data.Param["SHINUSERNAME"]);// r["卫生许可证号"]);
                data0.Param.Add("RECDATE", DateTime.Now);// r["有效期"]);
                data0.Param.Add("SHDATE", DateTime.Now);// r["有效期"]);
                data0.Param.Add("SHUSERID", data.Param["SHINUSERID"]);// r["卫生许可证号"]);
                data0.Param.Add("SHUSERNAME", data.Param["SHINUSERNAME"]);// r["卫生许可证号"]);
                data0.Param.Add("SHOUTDATE", DateTime.Now);
                data0.Param.Add("SHOUTUSERID", data.Param["SHINUSERID"]);
                data0.Param.Add("SHOUTUSERNAME", data.Param["SHINUSERNAME"]);
                data0.Param.Add("CHOSCODE", data.Param["CHOSCODE"]);// r["卫生许可证号"]);
                data0.Param.Add("PLANNO", PlanMian["PLANID"]);
                data0.Param.Add("INID", data1.Param["INID"]);// 随货单号

                if (DaoTool.Save(dao, OptContent.get("SlSaveWzOutMainInfo"), data0) < 0)
                {
                    throw new Exception("执行失败！");
                }
                #endregion


                #region  添加出库细表、流水表，更新库存
                //添加细表
                DataTable xB = dao.Fd(OptContent.get("SlFindPlanDetailList").Sql, new object[] { PlanMian["PLANID"] });//DaoTool.FindDT(dao, OptContent.get("DbFindOutDetailList"), new object[]{ data.Param["OUTID"]});
                foreach (DataRow r in xB.Rows)
                {
                    int Num = 0;
                    DataRow Wz = dao.Fd("select * from LKWZ.DICTWZ where WZID=" + r["WZID"].ToString()).Rows[0];
                    DataRow Sd = dao.Fd("select * from LKWZ.WZSTOCKDETAIL where FLOWNO=" + r["STOCKFLOWNO"]).Rows[0];
                    
                    if (Convert.ToInt32(Wz["LSUNITCODE"]) != Convert.ToInt32(r["UNITCODE"]))
                    //if (Wz["LSUNITCODE"] != r["UNITCODE"])
                    {
                        Num = Convert.ToInt32(r["NUM"]) * Convert.ToInt32(Wz["CHANGERATE"]);
                    }
                    else
                        Num = Convert.ToInt32(r["NUM"]);

                    //更新出库库房库存
                    //dao.ExecuteNonQuery("update LKWZ.WZSTOCKDETAIL set NUM=NUM-" + Num.ToString() + " where FLOWNO=" + r["STOCKFLOWNO"].ToString());
                    dao.ExecuteNonQuery("update LKWZ.WZSTOCKDETAIL set OUTNUM=OUTNUM+" + Num.ToString() + " where FLOWNO=" + r["STOCKFLOWNO"].ToString());
                    dao.ExecuteNonQuery("update LKWZ.WZSTOCK set NUM=NUM - " + Num.ToString() + " where STOCKID=" + Sd["STOCKID"].ToString());

                    //dao.ExecuteNonQuery("update LKWZ.WZSTOCKDETAIL set NUM=NUM-" + Num.ToString() + " where FLOWNO=" + r["STOCKFLOWNO"].ToString());
                    //dao.ExecuteNonQuery("update LKWZ.WZSTOCK set NUM=NUM - " + Num.ToString() + " where STOCKID=" + Sd["STOCKID"].ToString());

                    //更新目的库房库存
                    object StockID = dao.ExecuteScalar("select STOCKID from LKWZ.WZSTOCK where WZID=" + r["WZID"].ToString() + " and  WARECODE=" + PlanMian["WARECODE"].ToString() + " and CHOSCODE=" + data.Param["CHOSCODE"].ToString());
                    int Pnum = 0;

                    YtService.data.OptData data3 = new YtService.data.OptData();
                    data3.Param = new Dictionary<string, object>();
                    data3.Param.Add("WARECODE", PlanMian["WARECODE"]);
                    data3.Param.Add("WZID", r["WZID"]);
                    //data3.Param.Add("NUM", r["NUM"]);
                    data3.Param.Add("LSUNITCODE", Wz["LSUNITCODE"]);
                    data3.Param.Add("CHOSCODE", data.Param["CHOSCODE"]);
                    if (StockID == null)
                    {
                        data3.Param.Add("NUM",Num);
                        data3.Param.Add("STOCKID", DaoTool.Seq(dao, "LKWZ.SEQWZStock"));
                        DaoTool.Save(dao, OptContent.get("DbSaveWZStockInfo"), data3);
                        Pnum = 0;
                    }
                    else
                    {
                        Pnum = Convert.ToInt32(dao.ExecuteScalar("select NUM from LKWZ.WZSTOCK where STOCKID=" + StockID.ToString()));
                        data3.Param.Add("NUM", Num+Pnum);
                        data3.Param.Add("STOCKID", StockID);
                        DaoTool.Update(dao, OptContent.get("DbSaveWZStockInfo"), data3);
                    }

                    //创建库存流水表
                    YtService.data.OptData data4 = new YtService.data.OptData();
                    data4.Param = new Dictionary<string, object>();
                    data4.Param.Add("FLOWNO", DaoTool.Seq(dao, "LKWZ.SEQWZStockDetail"));
                    data4.Param.Add("INID", data1.Param["INID"]);
                    data4.Param.Add("WZID", r["WZID"]);
                    data4.Param.Add("WARECODE", PlanMian["WARECODE"]);
                    data4.Param.Add("NUM", Num);
                    data4.Param.Add("PRICE", Sd["PRICE"]);
                    data4.Param.Add("STOCKID", data3.Param["STOCKID"]);
                    data4.Param.Add("LSPRICE", Sd["LSPRICE"]);
                    data4.Param.Add("BEFORENUM", Pnum);
                    data4.Param.Add("OUTNUM", 0);
                    data4.Param.Add("CHANGERATE", Wz["CHANGERATE"]);
                    data4.Param.Add("LSUNITCODE", Wz["LSUNITCODE"]);
                    data4.Param.Add("PH", Sd["PH"]);
                    data4.Param.Add("PZWH", Sd["PZWH"]);
                    data4.Param.Add("MEMO", r["MEMO"]);
                    data4.Param.Add("TXM", Sd["TXM"]);
                    data4.Param.Add("SUPPLYNAME", Sd["SUPPLYNAME"]);
                    data4.Param.Add("SUPPLYID", Sd["SUPPLYID"]);
                    data4.Param.Add("PRODUCTDATE", Sd["PRODUCTDATE"]);
                    data4.Param.Add("VALIDDATE", Sd["VALIDDATE"]);
                    data4.Param.Add("WSXKZH", Sd["WSXKZH"]);
                    data4.Param.Add("RECIPECODE", Sd["RECIPECODE"]);
                    data4.Param.Add("SHDH", Sd["SHDH"]);
                    data4.Param.Add("SUPPLYID2", Sd["SUPPLYID2"]);
                    data4.Param.Add("SUPPLYNAME2", Sd["SUPPLYNAME2"]);
                    data4.Param.Add("CHOSCODE", data.Param["CHOSCODE"]);
                    data4.Param.Add("INDATE", DateTime.Now);

                    if (DaoTool.Save(dao, OptContent.get("DbSaveWZStockDetailInfo"), data4) < 0)
                    {
                        throw new Exception("执行失败！");
                    }

                    //创建入库细表
                    YtService.data.OptData data2 = new YtService.data.OptData();
                    data2.Param = new Dictionary<string, object>();
                    data2.Param.Add("DETAILNO", DaoTool.Seq(dao, "LKWZ.SEQWZInDetail"));
                    data2.Param.Add("INID", data1.Param["INID"]);
                    data2.Param.Add("WZID", r["WZID"]);
                    data2.Param.Add("UNITCODE", r["UNITCODE"]);
                    data2.Param.Add("NUM", r["NUM"]);
                    data2.Param.Add("PRICE", r["PRICE"]);
                    data2.Param.Add("MONEY", r["MONEY"]);
                    data2.Param.Add("LSPRICE", r["LSPRICE"]);
                    data2.Param.Add("LSMONEY", r["LSMONEY"]);
                    data2.Param.Add("PH", Sd["PH"]);
                    data2.Param.Add("PZWH", Sd["PZWH"]);
                    data2.Param.Add("MEMO", r["MEMO"]);
                    data2.Param.Add("TXM", Sd["TXM"]);
                    data2.Param.Add("SUPPLYNAME", Sd["SUPPLYNAME"]);
                    data2.Param.Add("SUPPLYID", Sd["SUPPLYID"]);
                    data2.Param.Add("PRODUCTDATE", Sd["PRODUCTDATE"]);
                    data2.Param.Add("VALIDDATE", Sd["VALIDDATE"]);
                    data2.Param.Add("WSXKZH", Sd["WSXKZH"]);
                    data2.Param.Add("CHOSCODE", data.Param["CHOSCODE"]);
                    data2.Param.Add("STOCKFLOWNO", data4.Param["FLOWNO"]);//库存流水号

                    if (DaoTool.Save(dao, OptContent.get("DbSaveWZInDetailInfo"), data2) < 0)
                    {
                        throw new Exception("执行失败！");
                    }


                    //创建出库细表
                    YtService.data.OptData data5 = new YtService.data.OptData();
                    data5.Param = new Dictionary<string, object>();

                    data5.Param.Add("OUTID", data0.Param["OUTID"]);
                    data5.Param.Add("DETAILNO", DaoTool.Seq(dao,"LKWZ.SEQWZOutDetail"));
                    data5.Param.Add("WZID", r["WZID"]);
                    data5.Param.Add("NUM", r["NUM"]);
                    data5.Param.Add("UNITCODE", r["UNITCODE"]);
                    data5.Param.Add("PRICE", r["LSPRICE"]);
                    data5.Param.Add("MONEY", r["LSMONEY"]);
                    data5.Param.Add("INPRICE", r["PRICE"]);
                    data5.Param.Add("INMONEY", r["MONEY"]);
                    data5.Param.Add("MEMO", r["MEMO"]);
                    data5.Param.Add("TXM", Sd["TXM"]);
                    data5.Param.Add("CHOSCODE", data.Param["CHOSCODE"]);
                    data5.Param.Add("STOCKFLOWNO", r["STOCKFLOWNO"]);
                    data5.Param.Add("PH", Sd["PH"]);
                    data5.Param.Add("PZWH", Sd["PZWH"]);
                    data5.Param.Add("SUPPLYID", Sd["SUPPLYID"]);
                    data5.Param.Add("SUPPLYNAME", Sd["SUPPLYNAME"]);
                    data5.Param.Add("PRODUCTDATE", Sd["PRODUCTDATE"]);
                    data5.Param.Add("VALIDDATE", Sd["VALIDDATE"]);
                    data5.Param.Add("WSXKZH", Sd["WSXKZH"]);
                    
                    if (DaoTool.Save(dao, OptContent.get("SlSaveWzOutDetailInfo"), data5) < 0)
                    {
                        throw new Exception("执行失败！");
                    }



                }
#endregion




                msg = "操作成功！";
                return "ok";
            }

            msg = "成功！";
            return "ok";
        }

        #endregion

    }
}
