using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using YtService.action;
using YtService.util;
using YtService.config;

namespace LKWZSVR.lkwz.WZDiaoBo
{
    public class SaveDiaoBo : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            
            if (data.Sql == null)
            {
                throw new Exception("Sql内容为空！");
            }
            else if (data.Sql.Equals("AddOutMain"))
            {
                data.Param["OUTID"] = DaoTool.Seq(dao,"LKWZ.SEQWZOut");
                data.Param["OUTDATE"] = DateTime.Now;
                data.Param["RECDATE"] = DateTime.Now;
                string recipe = data.Param["RECIPECODE"].ToString();
                recipe.IndexOf('_');
                int recipeno = Convert.ToInt32(dao.ExecuteScalar(OptContent.get("DbGetOutRecipeNo").Sql, new object[] { recipe.Substring(recipe.IndexOf('_') + 1) + '%' })) + 1;
                data.Param["RECIPECODE"] = recipe.Substring(recipe.IndexOf('_') + 1) + recipeno.ToString(recipe.Substring(0,recipe.IndexOf('_')));
                string[] XbTable = data.Param["XiBiaoData"].ToString().Split('~'); ;
                
                if (DaoTool.Save(dao, OptContent.get("DbSaveWzOutMainInfo"), data) < 0)
                {
                    throw new Exception("主表保存失败！");
                }
                foreach (string r in XbTable)
                {
                    YtService.data.OptData data1=new YtService.data.OptData();
                    data1.Param = new Dictionary<string, object>();
                    string[] xbValue = r.Split('|');
                    if (xbValue[0] == "2")
                    {
                        data1.Param.Add("DETAILNO", DaoTool.Seq(dao, "LKWZ.SEQWZOutDetail"));
                        data1.Param.Add("OUTID", data.Param["OUTID"]);
                        data1.Param.Add("WZID", Convert.ToInt32(xbValue[1]));//r["物质ID"]);
                        data1.Param.Add("NUM", Convert.ToInt32(xbValue[2]));//r["数量"]);
                        data1.Param.Add("UNITCODE", Convert.ToInt32(xbValue[3]));//r["单位"]);
                        data1.Param.Add("PRICE", Convert.ToDouble(xbValue[4]));//r["出库单价"]);
                        data1.Param.Add("MONEY", Convert.ToDouble(xbValue[5]));//r["出库金额"]);
                        data1.Param.Add("INPRICE", Convert.ToDouble(xbValue[6]));//r["入库单价"]);
                        data1.Param.Add("INMONEY", Convert.ToDouble(xbValue[7]));//r["入库金额"]);
                        data1.Param.Add("MEMO", xbValue[8]);//r["备注"]);
                        data1.Param.Add("TXM", xbValue[9]);//r["条形码"]);
                        data1.Param.Add("CHOSCODE", xbValue[10]);//; r["医疗机构编码"]);
                        data1.Param.Add("STOCKFLOWNO", Convert.ToInt32(xbValue[11]));// r["库存流水号"]);
                        data1.Param.Add("PH", xbValue[12]);// r["生产批号"]);
                        data1.Param.Add("PZWH", xbValue[13]);//r["批准文号"]);
                        if(xbValue[14]!="")
                        data1.Param.Add("SUPPLYID", Convert.ToInt32(xbValue[14]));// r["生产厂家ID"]);
                        else
                        data1.Param.Add("SUPPLYID", null);// r["生产厂家ID"]);
                        data1.Param.Add("SUPPLYNAME", xbValue[15]);// r["生产厂家名称"]);
                        data1.Param.Add("PRODUCTDATE", Convert.ToDateTime(xbValue[16]));// r["生产日期"]);
                        data1.Param.Add("VALIDDATE", Convert.ToDateTime(xbValue[17]));// r["有效期"]);
                        data1.Param.Add("WSXKZH", xbValue[18]);// r["卫生许可证号"]);

                        if (DaoTool.Save(dao, OptContent.get("DbSaveWzOutDetailInfo"), data1) < 0)
                        {
                            throw new Exception("细表保存失败！");
                        }
                    }
                }
                msg = "保存成功！" + "|" + data.Param["OUTID"].ToString() + "|" + data.Param["RECIPECODE"].ToString() + "|" + data.Param["OUTDATE"].ToString();
                return "ok";
            }
            else if (data.Sql.Equals("UpdateOutMain"))
            {
                //data.Param["OUTID"] = DaoTool.Seq(dao, "LKWZ.SEQWZOut");
                //data.Param["OUTDATE"] = DateTime.Now;
                data.Param["RECDATE"] = DateTime.Now;
               // string recipe = data.Param["RECIPECODE"].ToString();
               // recipe.IndexOf('_');
                //int recipeno = Convert.ToInt32(dao.ExecuteScalar(OptContent.get("DbGetRecipeNo").Sql, new object[] { recipe.Substring(recipe.IndexOf('_') + 1) + '%' })) + 1;
                //data.Param["RECIPECODE"] = recipe.Substring(recipe.IndexOf('_') + 1) + recipeno.ToString(recipe.Substring(0, recipe.IndexOf('_')));
                string[] XbTable = data.Param["XiBiaoData"].ToString().Split('~'); ;

                if (DaoTool.Update(dao, OptContent.get("DbSaveWzOutMainInfo"), data) < 0)
                {
                    throw new Exception("主表保存失败！");
                }
                foreach (string r in XbTable)
                {
                    YtService.data.OptData data1 = new YtService.data.OptData();
                    data1.Param = new Dictionary<string, object>();
                    string[] xbValue = r.Split('|');

 
                    if (xbValue[0] == "1")
                    {
                        data1.Param.Add("DETAILNO", Convert.ToInt32(xbValue[1]));
                        data1.Param.Add("OUTID", Convert.ToInt32(xbValue[2]));
                        data1.Param.Add("WZID", Convert.ToInt32(xbValue[3]));//r["物质ID"]);
                        data1.Param.Add("NUM", Convert.ToInt32(xbValue[4]));//r["数量"]);
                        data1.Param.Add("UNITCODE", Convert.ToInt32(xbValue[5]));//r["单位"]);
                        data1.Param.Add("PRICE", Convert.ToDouble(xbValue[6]));//r["出库单价"]);
                        data1.Param.Add("MONEY", Convert.ToDouble(xbValue[7]));//r["出库金额"]);
                        data1.Param.Add("INPRICE", Convert.ToDouble(xbValue[8]));//r["入库单价"]);
                        data1.Param.Add("INMONEY", Convert.ToDouble(xbValue[9]));//r["入库金额"]);
                        data1.Param.Add("MEMO", xbValue[10]);//r["备注"]);
                        data1.Param.Add("TXM", xbValue[11]);//r["条形码"]);
                        data1.Param.Add("CHOSCODE", xbValue[12]);//; r["医疗机构编码"]);
                        data1.Param.Add("STOCKFLOWNO", Convert.ToInt32(xbValue[13]));// r["库存流水号"]);
                        data1.Param.Add("PH", xbValue[14]);// r["生产批号"]);
                        data1.Param.Add("PZWH", xbValue[15]);//r["批准文号"]);
                        if(xbValue[16]!="")
                        data1.Param.Add("SUPPLYID", Convert.ToInt32(xbValue[16]));// r["生产厂家ID"]);
                        else
                        data1.Param.Add("SUPPLYID", null);// r["生产厂家ID"]);
                        data1.Param.Add("SUPPLYNAME", xbValue[17]);// r["生产厂家名称"]);
                        data1.Param.Add("PRODUCTDATE", Convert.ToDateTime(xbValue[18]));// r["生产日期"]);
                        data1.Param.Add("VALIDDATE", Convert.ToDateTime(xbValue[19]));// r["有效期"]);
                        data1.Param.Add("WSXKZH", xbValue[20]);// r["卫生许可证号"]);

                        if (DaoTool.Update(dao, OptContent.get("DbSaveWzOutDetailInfo"), data1) < 0)
                        {
                            throw new Exception("细表保存失败！");
                        }
                    }
                    else if (xbValue[0] == "2")
                    {
                        data1.Param.Add("DETAILNO", DaoTool.Seq(dao, "LKWZ.SEQWZOutDetail"));
                        data1.Param.Add("OUTID", data.Param["OUTID"]);
                        data1.Param.Add("WZID", Convert.ToInt32(xbValue[1]));//r["物质ID"]);
                        data1.Param.Add("NUM", Convert.ToInt32(xbValue[2]));//r["数量"]);
                        data1.Param.Add("UNITCODE", Convert.ToInt32(xbValue[3]));//r["单位"]);
                        data1.Param.Add("PRICE", Convert.ToDouble(xbValue[4]));//r["出库单价"]);
                        data1.Param.Add("MONEY", Convert.ToDouble(xbValue[5]));//r["出库金额"]);
                        data1.Param.Add("INPRICE", Convert.ToDouble(xbValue[6]));//r["入库单价"]);
                        data1.Param.Add("INMONEY", Convert.ToDouble(xbValue[7]));//r["入库金额"]);
                        data1.Param.Add("MEMO", xbValue[8]);//r["备注"]);
                        data1.Param.Add("TXM", xbValue[9]);//r["条形码"]);
                        data1.Param.Add("CHOSCODE", xbValue[10]);//; r["医疗机构编码"]);
                        data1.Param.Add("STOCKFLOWNO", Convert.ToInt32(xbValue[11]));// r["库存流水号"]);
                        data1.Param.Add("PH", xbValue[12]);// r["生产批号"]);
                        data1.Param.Add("PZWH", xbValue[13]);//r["批准文号"]);
                        if(xbValue[14]!="")
                        data1.Param.Add("SUPPLYID", Convert.ToInt32(xbValue[14]));// r["生产厂家ID"]);
                        else
                        data1.Param.Add("SUPPLYID", null);// r["生产厂家ID"]);
                        data1.Param.Add("SUPPLYNAME", xbValue[15]);// r["生产厂家名称"]);
                        data1.Param.Add("PRODUCTDATE", Convert.ToDateTime(xbValue[16]));// r["生产日期"]);
                        data1.Param.Add("VALIDDATE", Convert.ToDateTime(xbValue[17]));// r["有效期"]);
                        data1.Param.Add("WSXKZH", xbValue[18]);// r["卫生许可证号"]);

                        if (DaoTool.Save(dao, OptContent.get("DbSaveWzOutDetailInfo"), data1) < 0)
                        {
                            throw new Exception("细表保存失败！");
                        }
                    }
                    else if (xbValue[0] == "3")  //根据ID删除细表中的项
                    {
                        //data1.Param.Add("DETAILNO", xbValue[1]);

                        //if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DbDelWzOutDetailInfo"), data1) >0)
                        //{
                        //   // throw new Exception("细表保存失败！");
                        //}
                        if (dao.ExecuteNonQuery(" DELETE FROM LKWZ.WZOUTDETAIL where DETAILNO=" + xbValue[1].ToString()) < 0)//DaoTool.ExecuteNonQuery(dao, OptContent.get("DbDelWzOutDetailInfo"), data1) >0)
                        {
                            throw new Exception("细表保存失败！");
                        }
                    }
                }

               // msg = "保存成功！" + data.Param["RECIPECODE"].ToString(); ;
                msg = "保存成功！" + "|" + data.Param["OUTID"].ToString() + "|" + data.Param["RECIPECODE"].ToString() +"|"+ data.Param["OUTDATE"].ToString();
                return "ok";
            }
            else if (data.Sql.Equals("ChangeOutMainState"))
            {

                if (DaoTool.Update(dao, OptContent.get("DbShOutMainInfo"), data) < 0)
                {
                    throw new Exception("执行失败！");
                }
                msg = "执行成功！";
                return "ok";
            }
            else if (data.Sql.Equals("QueRenDiaoBo"))
            {
                if (DaoTool.Update(dao, OptContent.get("DbQrOutMainInfo"), data) < 0)
                {
                    throw new Exception("执行失败！");
                }

                DataRow InKind=dao.Fd("select * from LKWZ.DICTWZINOUT where IfUse=1 and IOFlag=0 and OPFlag=1 and CHOSCode="+data.Param["CHOSCODE"].ToString()).Rows[0];
                DataRow OutMian= dao.Fd("select * from LKWZ.WZOUTMAIN where OUTID="+data.Param["OUTID"].ToString()).Rows[0];
                string recipe=InKind["RECIPECODE"].ToString();
                if(InKind["RECIPEYEAR"].ToString()=="1")
                {
                    recipe+=DateTime.Now.Year.ToString("D4");
                }
                if(InKind["RECIPEMONTH"].ToString()=="1")
                {
                    recipe+=DateTime.Now.Month.ToString("D2");
                }
                int recipeno = Convert.ToInt32(dao.ExecuteScalar(OptContent.get("DbGetInRecipeNo").Sql, new object[] { recipe + '%' })) + 1;
                recipe += recipeno.ToString("D" + (Convert.ToInt32(InKind["RECIPELENGTH"]) - recipe.Length).ToString());
                //添加入库主表
                YtService.data.OptData data1 = new YtService.data.OptData();
                data1.Param = new Dictionary<string, object>();
                data1.Param.Add("INID", DaoTool.Seq(dao,"LKWZ.SEQWZIn"));
                
                data1.Param.Add("IOID", InKind["IOID"]);
                data1.Param.Add("WARECODE", OutMian["TARGETWARECODE"]);
                
                data1.Param.Add("RECIPECODE",recipe);
                data1.Param.Add("TOTALMONEY", OutMian["TOTALMONEY"]);
                data1.Param.Add("LSTOTALMONEY", OutMian["LSTOTALMONEY"]);
                data1.Param.Add("INDATE", DateTime.Now);
                data1.Param.Add("STATUS", 6);
                data1.Param.Add("OPFLAG", InKind["OPFLAG"]);
                data1.Param.Add("MEMO", OutMian["MEMO"]);
                data1.Param.Add("USERID", data.Param["SHOUTUSERID"]);
                data1.Param.Add("USERNAME", data.Param["SHOUTUSERNAME"]);
                data1.Param.Add("RECDATE", DateTime.Now);

                data1.Param.Add("SHDH",null);// 随货单号
                data1.Param.Add("INVOICECODE", null);//r["批准文号"]);
                data1.Param.Add("INVOICEDATE",null);// r["生产厂家ID"]);
                data1.Param.Add("SUPPLYNAME", null);// r["生产厂家名称"]);
                data1.Param.Add("SUPPLYID", null);// r["生产日期"]);
                data1.Param.Add("SHDATE", DateTime.Now);// r["有效期"]);
                data1.Param.Add("SHUSERID", data.Param["SHOUTUSERID"]);// r["卫生许可证号"]);
                data1.Param.Add("SHUSERNAME", data.Param["SHOUTUSERNAME"]);// r["卫生许可证号"]);
                data1.Param.Add("SHINDATE", DateTime.Now);// r["卫生许可证号"]);
                data1.Param.Add("SHINUSERID", data.Param["SHOUTUSERID"]);// r["卫生许可证号"]);
                data1.Param.Add("SHINUSERNAME", data.Param["SHOUTUSERNAME"]);// r["卫生许可证号"]);
                data1.Param.Add("PLANNO", null);// r["卫生许可证号"]);
                data1.Param.Add("CHOSCODE",  data.Param["CHOSCODE"]);// r["卫生许可证号"]);

                if (DaoTool.Save(dao, OptContent.get("DbSaveWZInMainInfo"), data1) < 0)
                {
                    throw new Exception("执行失败！");
                }

                //更新
                dao.ExecuteNonQuery("update LKWZ.WZOUTMAIN set INID="+data1.Param["INID"].ToString()+" where OUTID="+OutMian["OUTID"].ToString());

                //添加细表
                DataTable xB = dao.Fd(OptContent.get("DbFindOutDetailList").Sql, new object[] { OutMian["OUTID"] });//DaoTool.FindDT(dao, OptContent.get("DbFindOutDetailList"), new object[]{ data.Param["OUTID"]});
                foreach (DataRow r in xB.Rows)
                {
                    int Num = 0;
                    DataRow Wz = dao.Fd("select * from LKWZ.DICTWZ where WZID=" + r["WZID"].ToString()).Rows[0];
                    DataRow Sd = dao.Fd("select * from LKWZ.WZSTOCKDETAIL where FLOWNO=" + r["STOCKFLOWNO"]).Rows[0];
                    
                    if (Convert.ToInt32(Wz["LSUNITCODE"]) != Convert.ToInt32(r["UNITCODE"]))
                    {
                        Num =Convert.ToInt32( r["NUM"] )* Convert.ToInt32(Wz["CHANGERATE"]);
                    }
                    else
                        Num = Convert.ToInt32(r["NUM"]);

                    //更新出库库房库存
                    //OUTNUM dao.ExecuteNonQuery("update LKWZ.WZSTOCKDETAIL set NUM=NUM-" + Num.ToString() + " where FLOWNO=" + r["STOCKFLOWNO"].ToString());
                    dao.ExecuteNonQuery("update LKWZ.WZSTOCKDETAIL set OUTNUM=OUTNUM+" + Num.ToString() + " where FLOWNO=" + r["STOCKFLOWNO"].ToString());
                    dao.ExecuteNonQuery("update LKWZ.WZSTOCK set NUM=NUM - " + Num.ToString() + " where STOCKID=" + Sd["STOCKID"].ToString());

                    //更新目的库房库存
                    object StockID=dao.ExecuteScalar("select STOCKID from LKWZ.WZSTOCK where WZID="+r["WZID"].ToString()+" and  WARECODE="+ OutMian["TARGETWARECODE"].ToString() +" and CHOSCODE="+data.Param["CHOSCODE"].ToString());
                    int Pnum=0;
                      
                    YtService.data.OptData data3 = new YtService.data.OptData();
                    data3.Param = new Dictionary<string, object>();


                    
                    data3.Param.Add("WARECODE", OutMian["TARGETWARECODE"]);
                    data3.Param.Add("WZID", r["WZID"]);
                    
                    data3.Param.Add("LSUNITCODE", Wz["LSUNITCODE"]);
                    data3.Param.Add("CHOSCODE",data.Param["CHOSCODE"]);
                    if (StockID == null)
                    {
                        data3.Param.Add("NUM", Num);
                        data3.Param.Add("STOCKID", DaoTool.Seq(dao, "LKWZ.SEQWZStock"));
                        DaoTool.Save(dao, OptContent.get("DbSaveWZStockInfo"), data3);
                        Pnum=0;
                    }
                    else
                    {
                        Pnum=Convert.ToInt32(dao.ExecuteScalar("select NUM from LKWZ.WZSTOCK where STOCKID="+StockID.ToString()));
                        data3.Param.Add("NUM", Num+Pnum);
                        data3.Param.Add("STOCKID", StockID);
                        DaoTool.Update(dao, OptContent.get("DbSaveWZStockInfo"), data3);
                    }

                    //创建流水表
                    YtService.data.OptData data4 = new YtService.data.OptData();
                    data4.Param = new Dictionary<string, object>();
                    data4.Param.Add("FLOWNO", DaoTool.Seq(dao,"LKWZ.SEQWZStockDetail"));
                    data4.Param.Add("INID", data1.Param["INID"]);
                    data4.Param.Add("WZID", r["WZID"]);
                    data4.Param.Add("WARECODE", OutMian["TARGETWARECODE"]);
                    data4.Param.Add("NUM", Num);
                    //data4.Param.Add("NUM", r["NUM"]);
                    data4.Param.Add("PRICE", Sd["PRICE"]);
                    data4.Param.Add("STOCKID",  data3.Param["STOCKID"]);
                    data4.Param.Add("LSPRICE", Sd["LSPRICE"]);
                    data4.Param.Add("BEFORENUM", Pnum);
                    data4.Param.Add("OUTNUM", 0);
                    data4.Param.Add("CHANGERATE", Wz["CHANGERATE"]);
                    data4.Param.Add("LSUNITCODE", Wz["LSUNITCODE"]);
                    data4.Param.Add("PH", r["PH"]);
                    data4.Param.Add("PZWH", r["PZWH"]);
                    data4.Param.Add("MEMO", r["MEMO"]);
                    data4.Param.Add("TXM", r["TXM"]);
                    data4.Param.Add("SUPPLYNAME", r["SUPPLYNAME"]);
                    data4.Param.Add("SUPPLYID", r["SUPPLYID"]);
                    data4.Param.Add("PRODUCTDATE", r["PRODUCTDATE"]);
                    data4.Param.Add("VALIDDATE", r["VALIDDATE"]);
                    data4.Param.Add("WSXKZH", r["WSXKZH"]);
                    data4.Param.Add("RECIPECODE",Sd["RECIPECODE"]);
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
                    data2.Param.Add("INID",data1.Param["INID"]);
                    data2.Param.Add("WZID",r["WZID"]);
                    data2.Param.Add("UNITCODE",r["UNITCODE"]);
                    data2.Param.Add("NUM",r["NUM"]);
                    data2.Param.Add("PRICE",r["INPRICE"]);
                    data2.Param.Add("MONEY",r["INMONEY"]);
                    data2.Param.Add("LSPRICE",r["PRICE"]);
                    data2.Param.Add("LSMONEY",r["MONEY"]);
                    data2.Param.Add("PH",r["PH"]);
                    data2.Param.Add("PZWH",r["PZWH"]);
                    data2.Param.Add("MEMO",r["MEMO"]);
                    data2.Param.Add("TXM",r["TXM"]);
                    data2.Param.Add("SUPPLYNAME",r["SUPPLYNAME"]);
                    data2.Param.Add("SUPPLYID",r["SUPPLYID"]);
                    data2.Param.Add("PRODUCTDATE",r["PRODUCTDATE"]);
                    data2.Param.Add("VALIDDATE",r["VALIDDATE"]);
                    data2.Param.Add("WSXKZH",r["WSXKZH"]);
                    data2.Param.Add("CHOSCODE",data.Param["CHOSCODE"]);
                    data2.Param.Add("STOCKFLOWNO", data4.Param["FLOWNO"]);//库存流水号

                    if (DaoTool.Save(dao, OptContent.get("DbSaveWZInDetailInfo"), data2) < 0)
                    {
                        throw new Exception("执行失败！");
                    }

                    
                    
                }


                msg = "操作成功！";
                return "ok";
            }

            msg = "成功！";
            return "ok";
        }

        #endregion

    }
}
