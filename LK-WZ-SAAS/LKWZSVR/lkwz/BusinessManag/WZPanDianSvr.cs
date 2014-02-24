using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YiTian.db;
using YtService.util;
using YtService.config;
using System.Data;

namespace LKWZSVR.lkwz.BusinessManag
{
    class WZPanDianSvr:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            msg = "盘点信息";
            Dictionary<string, object> pa1 = new Dictionary<string, object>();
            Dictionary<string, object> pa = new Dictionary<string, object>();
            Dictionary<string, object> aa = new Dictionary<string, object>();
            Dictionary<string, object> bb = new Dictionary<string, object>();
            Dictionary<string, object> cc = new Dictionary<string, object>();
            Dictionary<string, object> dd = new Dictionary<string, object>();
            Dictionary<string, object> ee = new Dictionary<string, object>();
            Dictionary<string, object> ff = new Dictionary<string, object>();
            Dictionary<string, object> gg = new Dictionary<string, object>();
            Dictionary<string, object> hh = new Dictionary<string, object>();
            Dictionary<string, object> ii = new Dictionary<string, object>();
            Dictionary<string, object> jj = new Dictionary<string, object>();
             Dictionary<string, object> kk = new Dictionary<string, object>();
             Dictionary<string, object> ll = new Dictionary<string, object>();
            List<Dictionary<string, object>> listout = new List<Dictionary<string, object>>();
            List<Dictionary<string, object>> listin = new List<Dictionary<string, object>>();
            
            string ac = data.Sql;
          //  ObjItem Obj;
            if ("SaveWZPanDianInfo".Equals(ac))
            {
               
                pa["PDDATE"] = DateTime.Now;
                pa["WARECODE"] = data.Param["WARECODE"].ToString();

                pa["STATUS"] = Convert.ToDecimal(data.Param["STATUS"]);
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);


                pa["RECDATE"] = DateTime.Now;
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }
                if (data.Param["USERNAME"] != null)
                {
                    pa["USERNAME"] = data.Param["USERNAME"].ToString();
                }
                else
                {
                    pa["USERNAME"] = null;
                }
               
                    pa["PDID"] = DaoTool.Seq(dao, "LKWZ.SEQWZPD");
                    Opt saveInfo = OptContent.get("SaveWZPanDianInfo");
                   
                  
                    if (DaoTool.Save(dao, saveInfo, pa) < 0)
                        throw new Exception("新建盘点主表失败！");

                    //pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                    if (data.Param["MyCount"] != null && data.Param["STOCKFLOWNO" + 1] !=null)
                    {
                        Opt saveInfodedetail = OptContent.get("SaveWZPanDianDetailInfo");
                        for (int i = 1; i < Convert.ToInt32(data.Param["MyCount"]); i++)
                        {
                            pa1["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                            pa1["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO"+i]);
                            DataTable dt = DaoTool.FindDT(dao, "WZPZ_FindWZStockDetailInfo", pa1);
                        
                            DataRow r = dt.Rows[0];
                            pa["FACTNUM"] = Convert.ToDecimal(data.Param["FACTNUM" + i]);
                            pa["YKNUM"] = Convert.ToDecimal(data.Param["YKNUM" + i]);

                            pa["STOCKFLOWNO"] = Convert.ToDecimal(r["FLOWNO"]);
                            pa["STOCKID"] = Convert.ToDecimal(r["STOCKID"]);
                            pa["WZID"] = Convert.ToDecimal(r["WZID"]);
                            pa["STOCKNUM"] = Convert.ToDecimal(r["NUM"]);
                            pa["PRODUCTDATE"] = Convert.ToDateTime(r["PRODUCTDATE"]);
                            pa["INDATE"] = Convert.ToDateTime(r["INDATE"]);
                            pa["RECIPECODE"] = r["RECIPECODE"].ToString();
                            pa["CHOSCODE"] = r["CHOSCODE"].ToString();
                            #region paras
                            if (r["LSUNITCODE"] != null)
                            {
                                if (r["LSUNITCODE"].ToString() != "")
                                {
                                    pa["LSUNITCODE"] = Convert.ToDecimal(r["LSUNITCODE"]);
                                }
                            }
                            else
                            {
                                pa["LSUNITCODE"] = null;
                            }
                            if (r["PRICE"] != null)
                            {
                                if (r["PRICE"].ToString() != "")
                                {

                                    pa["PRICE"] = Convert.ToSingle(r["PRICE"]);
                                }
                            }
                            else
                            {
                                pa["PRICE"] = null;
                            }
                            if (r["LSPRICE"] != null)
                            {
                                if (r["LSPRICE"].ToString() != "")
                                {

                                    pa["LSPRICE"] = Convert.ToSingle(r["LSPRICE"]);
                                }
                            }
                            else
                            {
                                pa["LSPRICE"] = null;
                            }
                            if (r["PH"] != null)
                            {
                                pa["PH"] = r["PH"].ToString();
                            }
                            else
                            {
                                pa["PH"] = null;
                            }
                            if (r["PZWH"] != null)
                            {
                                pa["PZWH"] = r["PZWH"].ToString();
                            }
                            else
                            {
                                pa["PZWH"] = null;
                            }
                            if (r["SUPPLYID"] != null)
                            {
                                if (r["SUPPLYID"].ToString() != "")
                                {

                                    pa["SUPPLYID"] = Convert.ToDecimal(r["SUPPLYID"]);
                                }
                            }
                            else
                            {
                                pa["SUPPLYID"] = null;
                            }
                            if (r["SUPPLYNAME"] != null)
                            {
                                pa["SUPPLYNAME"] = r["SUPPLYNAME"].ToString();
                            }
                            else
                            {
                                pa["SUPPLYNAME"] = null;
                            }
                            if (r["VALIDDATE"] != null)
                            {
                                if (r["VALIDDATE"].ToString() != "")
                                {

                                    pa["VALIDDATE"] = Convert.ToDateTime(r["VALIDDATE"]);
                                }
                            }
                            else
                            {
                                pa["VALIDDATE"] = null;
                            }
                            if (r["WSXKZH"] != null)
                            {
                                pa["WSXKZH"] = r["WSXKZH"].ToString();
                            }
                            else
                            {
                                pa["WSXKZH"] = null;
                            }
                            if (r["SHDH"] != null)
                            {
                                pa["SHDH"] = r["SHDH"].ToString();
                            }
                            else
                            {
                                pa["SHDH"] = null;
                            }
                            if (r["SUPPLYID2"] != null)
                            {

                                if (r["SUPPLYID2"].ToString() != "")
                                {
                                    pa["SUPPLYID2"] = Convert.ToDecimal(r["SUPPLYID2"]);
                                }
                            }
                            else
                            {
                                pa["SUPPLYID2"] = null;
                            }
                            if (r["SUPPLYNAME2"] != null)
                            {
                                pa["SUPPLYNAME2"] = r["SUPPLYNAME2"].ToString();
                            }
                            else
                            {
                                pa["SUPPLYNAME2"] = null;

                            }
                            if (r["MEMO"] != null)
                            {
                                pa["MEMO"] = r["MEMO"].ToString();
                            }
                            else
                            {
                                pa["MEMO"] = null;

                            }
                            if (r["TXM"] != null)
                            {
                                pa["TXM"] = r["TXM"].ToString();
                            }
                            else
                            {
                                pa["TXM"] = null;

                            } 
                            #endregion
                            if (DaoTool.Save(dao, saveInfodedetail, pa) < 0)
                                throw new Exception("添加盘点细表"+r["WZNAME"]+"信息失败！");
                        }
                    }


                  

                    //Opt wyInfo = OptContent.get("WYWZPanDianDetailInfo");
                    //if (!DaoTool.ExecuteScalar(dao, wyInfo, pa).IsNull)
                    //{
                    //    msg = "表中已经有该物资的盘点数据，不能再新增，请在表中修改！";
                    //    return "ok";
                    //}


                    msg = "添加盘点信息成功！|" + pa["PDID"];
                    return "ok";
                }


            if ("UpdataWZPanDianInfo".Equals(ac))
            {

                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                pa["WARECODE"] = data.Param["WARECODE"].ToString();
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }
                if (data.Param["USERNAME"] != null)
                {
                    pa["USERNAME"] = data.Param["USERNAME"].ToString();
                }
                else
                {
                    pa["USERNAME"] = null;
                }


                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                Opt updataInfo = OptContent.get("UpdataWZPanDianInfo");



                if (DaoTool.ExecuteNonQuery(dao, updataInfo, pa) < 0)
                    throw new Exception("修改盘点信息失败！");
 

                //pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                if (data.Param["MyCount"] != null)
                {
                    Opt ifsaveorupdata = OptContent.get("IfSaveOrUpdataWZPanDianDetailInfo");

                    Opt saveInfodedetail = OptContent.get("SaveWZPanDianDetailInfo");


                    for (int i = 1; i < Convert.ToInt32(data.Param["MyCount"]); i++)
                    {
                        pa1["WZID"] = Convert.ToDecimal(data.Param["WZID" + i]);
                        pa1["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO" + i]);
                        pa1["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                        pa1["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                        DataTable dt = DaoTool.FindDT(dao, "WZPZ_FindWZStockDetailInfo", pa1);

                        DataRow r = dt.Rows[0];

                          ObjItem num = DaoTool.ExecuteScalar(dao, ifsaveorupdata, pa1);
                        
                        if (!num.IsNull)
                        {

                   
                            pa["FACTNUM"] = Convert.ToDecimal(data.Param["FACTNUM" + i]);
                            pa["YKNUM"] = Convert.ToDecimal(data.Param["YKNUM" + i]);
                            pa["CHOSCODE"] = r["CHOSCODE"].ToString();
                            pa["STOCKFLOWNO"] = Convert.ToDecimal(r["FLOWNO"]);
                            pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                            Opt updatadetailInfo = OptContent.get("UpdataeWZPanDicanDetailInfo");
                            if (DaoTool.ExecuteNonQuery(dao, updatadetailInfo, pa) < 0)
                                throw new Exception("修改盘点细表" + r["WZNAME"] + "信息失败！！");
                        }
                        else //修改带新增盘点信息
                        {

                         
                            pa["FACTNUM"] = Convert.ToDecimal(data.Param["FACTNUM" + i]);
                            pa["YKNUM"] = Convert.ToDecimal(data.Param["YKNUM" + i]);

                            pa["STOCKFLOWNO"] = Convert.ToDecimal(r["FLOWNO"]);
                            pa["STOCKID"] = Convert.ToDecimal(r["STOCKID"]);
                            pa["WZID"] = Convert.ToDecimal(r["WZID"]);
                            pa["STOCKNUM"] = Convert.ToDecimal(r["NUM"]);
                            pa["PRODUCTDATE"] = Convert.ToDateTime(r["PRODUCTDATE"]);
                            pa["INDATE"] = Convert.ToDateTime(r["INDATE"]);
                            pa["RECIPECODE"] = r["RECIPECODE"].ToString();
                            pa["CHOSCODE"] = r["CHOSCODE"].ToString();
                            #region paras
                            if (r["LSUNITCODE"] != null)
                            {
                                if (r["LSUNITCODE"].ToString() != "")
                                {
                                    pa["LSUNITCODE"] = Convert.ToDecimal(r["LSUNITCODE"]);
                                }
                            }
                            else
                            {
                                pa["LSUNITCODE"] = null;
                            }
                            if (r["PRICE"] != null)
                            {
                                if (r["PRICE"].ToString() != "")
                                {

                                    pa["PRICE"] = Convert.ToSingle(r["PRICE"]);
                                }
                            }
                            else
                            {
                                pa["PRICE"] = null;
                            }
                            if (r["LSPRICE"] != null)
                            {
                                if (r["LSPRICE"].ToString() != "")
                                {

                                    pa["LSPRICE"] = Convert.ToSingle(r["LSPRICE"]);
                                }
                            }
                            else
                            {
                                pa["LSPRICE"] = null;
                            }
                            if (r["PH"] != null)
                            {
                                pa["PH"] = r["PH"].ToString();
                            }
                            else
                            {
                                pa["PH"] = null;
                            }
                            if (r["PZWH"] != null)
                            {
                                pa["PZWH"] = r["PZWH"].ToString();
                            }
                            else
                            {
                                pa["PZWH"] = null;
                            }
                            if (r["SUPPLYID"] != null)
                            {
                                if (r["SUPPLYID"].ToString() != "")
                                {

                                    pa["SUPPLYID"] = Convert.ToDecimal(r["SUPPLYID"]);
                                }
                            }
                            else
                            {
                                pa["SUPPLYID"] = null;
                            }
                            if (r["SUPPLYNAME"] != null)
                            {
                                pa["SUPPLYNAME"] = r["SUPPLYNAME"].ToString();
                            }
                            else
                            {
                                pa["SUPPLYNAME"] = null;
                            }
                            if (r["VALIDDATE"] != null)
                            {
                                if (r["VALIDDATE"].ToString() != "")
                                {

                                    pa["VALIDDATE"] = Convert.ToDateTime(r["VALIDDATE"]);
                                }
                            }
                            else
                            {
                                pa["VALIDDATE"] = null;
                            }
                            if (r["WSXKZH"] != null)
                            {
                                pa["WSXKZH"] = r["WSXKZH"].ToString();
                            }
                            else
                            {
                                pa["WSXKZH"] = null;
                            }
                            if (r["SHDH"] != null)
                            {
                                pa["SHDH"] = r["SHDH"].ToString();
                            }
                            else
                            {
                                pa["SHDH"] = null;
                            }
                            if (r["SUPPLYID2"] != null)
                            {

                                if (r["SUPPLYID2"].ToString() != "")
                                {
                                    pa["SUPPLYID2"] = Convert.ToDecimal(r["SUPPLYID2"]);
                                }
                            }
                            else
                            {
                                pa["SUPPLYID2"] = null;
                            }
                            if (r["SUPPLYNAME2"] != null)
                            {
                                pa["SUPPLYNAME2"] = r["SUPPLYNAME2"].ToString();
                            }
                            else
                            {
                                pa["SUPPLYNAME2"] = null;

                            }
                            if (r["MEMO"] != null)
                            {
                                pa["MEMO"] = r["MEMO"].ToString();
                            }
                            else
                            {
                                pa["MEMO"] = null;

                            }
                            if (r["TXM"] != null)
                            {
                                pa["TXM"] = r["TXM"].ToString();
                            }
                            else
                            {
                                pa["TXM"] = null;

                            }
                            #endregion
                            if (DaoTool.Save(dao, saveInfodedetail, pa) < 0)
                                throw new Exception("在修改中添加盘点细表" + r["WZNAME"] + "信息失败！");
                        
                        }

                    }




                    msg = "录入成功！|" +pa["PDID"];
                    return "ok";

                }
            }
            if ("DelWZPanDianInfo".Equals(ac))
            {


                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();




                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                Opt delInfo = OptContent.get("DelWZPanDianInfo");



                if (DaoTool.ExecuteNonQuery(dao, delInfo, pa) < 0)
                    throw new Exception("删除盘点信息失败！");
                msg = "删除成功！";
                return "ok";
              
            }
            if ("DelWZPanDianDetailInfo".Equals(ac))
            {


                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();


                pa["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO"]);

                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                Opt delInfo = OptContent.get("DelWZPanDianDetailInfo");



                if (DaoTool.ExecuteNonQuery(dao, delInfo, pa) < 0)
                    throw new Exception("删除盘点详细信息失败！");
                msg = "删除成功！";
                return "ok";

            }

            if ("ShenHeWZPanDianInfo".Equals(ac))
            {
                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["STATUS"] = data.Param["STATUS"].ToString();
                pa["SHUSERID"] = Convert.ToDecimal(data.Param["SHUSERID"]);
                pa["SHUSERNAME"] = data.Param["SHUSERNAME"].ToString();
                pa["SHDATE"] = DateTime.Now;

                Opt saveInfo = OptContent.get("ShenHeWZPanDianInfo");        
                    if (DaoTool.ExecuteNonQuery(dao, saveInfo, pa) < 0)
                        throw new Exception("审核失败！");
                    msg = "审核成功！";
                    return "ok";
                }
            if ("CancelShenHeWZPanDianInfo".Equals(ac))
            {
                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["STATUS"] = data.Param["STATUS"].ToString();
                pa["SHUSERID"] =null;
                pa["SHUSERNAME"] = null;
                pa["SHDATE"] = null;

                Opt saveInfo = OptContent.get("CancelShenHeWZPanDianInfo");
                if (DaoTool.ExecuteNonQuery(dao, saveInfo, pa) < 0)
                    throw new Exception("取消审核失败！");
                msg = "取消审核成功！";
                return "ok";
            }
            if ("JieZhuangWZPanDianInfo".Equals(ac))
            {
                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["STATUS"] = data.Param["STATUS"].ToString();
                pa["JZUSERID"] = Convert.ToDecimal(data.Param["JZUSERID"]);
                pa["JZUSERNAME"] = data.Param["JZUSERNAME"].ToString();
                pa["JZDATE"] = DateTime.Now;

                Opt saveInfo = OptContent.get("JieZhuangWZPanDianInfo");        
                    if (DaoTool.ExecuteNonQuery(dao, saveInfo, pa) < 0)
                        throw new Exception("结转失败！");








                    Opt getpdin = OptContent.get("GetPDIDWZPanDianInfoStockIn");                
                 listin = DaoTool.Find(dao, getpdin, pa);
                if(listin!=null && listin.Count>0)
                {
                     //生成入库主表
                    aa["INID"] = DaoTool.Seq(dao, "LKWZ.SEQWZIN");
                    Opt getioid = OptContent.get("GetIOIDWZPanDianInfoStockIn");
                    ObjItem objitem = DaoTool.ExecuteScalar(dao, getioid,pa);
                    aa["IOID"] = Convert.ToDecimal(objitem.ToString());
                    //单据号
                    DataTable dt = dao.Fd(OptContent.get("SearchDicWZInOut").Sql, new object[] { aa["IOID"].ToString() });
                  string recipe = dt.Rows[0]["RECIPECODE"].ToString();
                  if (Convert.ToDecimal(dt.Rows[0]["RECIPEYEAR"]) == 1)
                  {
                      recipe += DateTime.Now.Year.ToString();
                  }
                  if (Convert.ToDecimal(dt.Rows[0]["RECIPEMONTH"]) == 1)
                  {
                      if (DateTime.Now.Month < 10)
                      {
                          recipe = recipe + "0" + DateTime.Now.Month.ToString();
                      }
                      else
                      {
                          recipe += DateTime.Now.Month.ToString();
                      }

                  }


                  decimal recipeno = Convert.ToDecimal(dao.ExecuteScalar(OptContent.get("GetRecipeNo").Sql, new object[] { recipe + '%' })) + 1;
                  if (recipeno > 0 && recipeno < 10)
                  {
                      recipe = recipe + "0" + recipeno.ToString();
                  }
                  else
                  {
                      recipe = recipe + recipeno.ToString();
                  }

                  aa["RECIPECODE"] = recipe;
                  aa["OPFLAG"] = 3;
                  aa["MEMO"] = data.Param["MEMO"].ToString();
                  aa["RECDATE"] =DateTime.Now;
                  aa["STATUS"] = 6;
                  aa["WARECODE"] = data.Param["WARECODE"].ToString();
                  aa["INDATE"] = DateTime.Now;
                  aa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                  aa["USERNAME"] = data.Param["USERNAME"].ToString();
                  aa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
               
                //得到总金额和零售总金额
                decimal total = 0;
                decimal lstotal = 0;
                foreach (var indata in listin)
                {
                    if (indata["PRICE"] == null || indata["PRICE"].ToString() == "")//保证有价格
                    { 
                        total = 0; 
                    }
                    else 
                    {
                        total += Convert.ToDecimal(indata["PRICE"]) * Convert.ToDecimal(indata["YKNUM"]);
                    }

                    if (indata["LSPRICE"] == null || indata["LSPRICE"].ToString()== "")
                    {
                        lstotal = 0;
                    }
                    else
                    {
                        lstotal += Convert.ToDecimal(indata["LSPRICE"]) * Convert.ToDecimal(indata["YKNUM"]);
                    }
               
                 
                }
                aa["TOTALMONEY"] = total;
                aa["LSTOTALMONEY"] = lstotal;

                Opt savewzinmain = OptContent.get("SaveWzInMainWZPanDianIn");              
                if (DaoTool.Save(dao, savewzinmain, aa) < 0)
                    throw new Exception("生成入库主表失败！");



                //向盘点主表中加入入库单序号

                kk["PDID"] = Convert.ToDecimal( Convert.ToDecimal(data.Param["PDID"]));
                kk["INID"] = aa["INID"];
                kk["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                Opt pdwz_updataed1 = OptContent.get("UpdataInWZPanDianInfo_In");
                if (DaoTool.ExecuteNonQuery(dao, pdwz_updataed1, kk) < 0)
                    throw new Exception("向盘点主表添加入库单序号失败！");

                //生成入库细表

                bb["INID"] = aa["INID"];
                foreach (Dictionary<string, object> indata in listin)
                {
                   
                    bb["DETAILNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZINDETAIL");
                    bb["WZID"] = Convert.ToDecimal(indata["WZID"]);
                
                    bb["UNITCODE"] = indata["LSUNITCODE"];//盘盈数据里的最小编码？无编码

                    bb["NUM"] = Convert.ToDecimal(indata["YKNUM"]);
                    bb["PRICE"] = Convert.ToDecimal(indata["PRICE"]);
                    bb["MONEY"] = Convert.ToDecimal(indata["PRICE"]) * Convert.ToDecimal(indata["YKNUM"]);
                    bb["LSPRICE"] = Convert.ToDecimal(indata["LSPRICE"]);
                    bb["LSMONEY"] = Convert.ToDecimal(indata["LSPRICE"]) * Convert.ToDecimal(indata["YKNUM"]);
                    bb["PRODUCTDATE"] = Convert.ToDateTime(indata["PRODUCTDATE"]);
                    bb["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                    bb["STOCKFLOWNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZSTOCKDETAIL");
                    bb["PH"] = indata["PH"];
                    bb["PZWH"] = indata["PZWH"];
                    bb["SUPPLYID"] = indata["SUPPLYID"];
                    bb["SUPPLYNAME"] = indata["SUPPLYNAME"];
                    bb["VALIDDATE"] = indata["VALIDDATE"];
                    bb["WSXKZH"] = indata["WSXKZH"];
                    bb["MEMO"] = indata["MEMO"];
                    bb["TXM"] = indata["TXM"];

                    Opt savewzindetail = OptContent.get("SaveWzInDetailWZPanDianIn");
                    if (DaoTool.Save(dao, savewzindetail, bb) < 0)
                        throw new Exception("生成入库细表失败！");

                    //想盘点细表中加入入库流水号
                    ii["STOCKFLOWNO"] = Convert.ToDecimal(indata["STOCKFLOWNO"]);
                    ii["PDID"] = Convert.ToDecimal(indata["PDID"]);
                    ii["DETAILNO"] = bb["DETAILNO"];
                    ii["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                    Opt pdwz_updatadetail = OptContent.get("UpdataInDetailWZPanDianInfo_In");
                    if (DaoTool.ExecuteNonQuery(dao, pdwz_updatadetail, ii) < 0)
                        throw new Exception("向盘点细表添加入库流水号失败！");

                    //生成库存总表
                    bb["WARECODE"] = Convert.ToDecimal(data.Param["WARECODE"]);
                    Opt stocknum = OptContent.get("iflsunitcodeInStockWZPanDianIn");//获得库存数量
                    ObjItem objtem4 = DaoTool.ExecuteScalar(dao, stocknum, bb);
                    Opt lsunitcode = OptContent.get("iflsunitcodeInWZDictWZPanDianIn");//获得最小单位编码在WZ中获得
                    ObjItem objtem5 = DaoTool.ExecuteScalar(dao, lsunitcode, bb);
                   

                    Opt hsxs = OptContent.get("HsxsInWZDictWZPanDianIn");//获得换算系数在WZ中获得
                    ObjItem objtem6 = DaoTool.ExecuteScalar(dao, hsxs, bb);

                    cc["WARECODE"] = aa["WARECODE"].ToString();
                    cc["WZID"] = Convert.ToDecimal(indata["WZID"]);
                    cc["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                    cc["LSUNITCODE"] = objtem5.ToInt();
                    Opt ifexist = OptContent.get("IfExistInStockWZPanDianIn");
                    ObjItem objtem2 = DaoTool.ExecuteScalar(dao, ifexist, bb);//是否在库存表中已存在

                    if (objtem2 != null)
                    {
                        //Opt iflsunitcode = OptContent.get("iflsunitcodeInStockWZPanDianIn");//是否为最小单位编码
                        //ObjItem objtem3 = DaoTool.ExecuteScalar(dao, iflsunitcode, aa);
                        cc["STOCKID"] = objtem2.ToInt();
                        if (bb["UNITCODE"].ToString() == objtem5.ToString())
                        {

                            cc["NUM"] = objtem4.ToInt() + Convert.ToInt32(indata["YKNUM"].ToString());
                            dd["CHANGERATE"] = 1;


                        }
                        else
                        {
                            cc["NUM"] = objtem4.ToInt() + objtem6.ToInt() * Convert.ToInt32(indata["YKNUM"].ToString());
                           
                            dd["CHANGERATE"] = objtem6.ToInt();
                        }
                        dd["BEFORENUM"] = objtem4.ToInt();
                        Opt updatastocktable = OptContent.get("UpdataInStockWZPanDianIn");
                        if (DaoTool.ExecuteNonQuery(dao, updatastocktable, cc) < 0)
                            throw new Exception("生成库存总表失败！");
                    }
                    else
                    {
                        cc["STOCKID"] = DaoTool.Seq(dao, "LKWZ.SEQWZSTOCK");
                        cc["NUM"] = Convert.ToInt32(indata["YKNUM"].ToString());
                        dd["BEFORENUM"] = 0;
                        Opt stocktable = OptContent.get("SaveInStockWZPanDianIn");
                        if (DaoTool.Save(dao, stocktable, cc) < 0)
                            throw new Exception("生成库存总表失败！");

                    }

                
                   
                    //生成库存细表
                    dd["FLOWNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZSTOCKDETAIL");
                    dd["INID"] = Convert.ToDecimal(aa["INID"].ToString());
                    dd["WARECODE"] = aa["WARECODE"].ToString();
                    dd["WZID"] = Convert.ToDecimal(indata["WZID"]);
                    dd["STOCKID"] = Convert.ToDecimal(indata["STOCKID"]);
                    dd["NUM"] = Convert.ToDecimal(indata["YKNUM"]);
                    dd["LSUNITCODE"] = objtem5.ToString();
                    dd["OUTNUM"] = 0;
                    dd["PRICE"] = bb["PRICE"];
                    dd["LSPRICE"] = bb["LSPRICE"];
                    dd["PH"] = bb["PH"];
                    dd["PZWH"] = bb["PZWH"];
                    dd["SUPPLYNAME"] = bb["SUPPLYNAME"];
                    dd["PRODUCTDATE"] = bb["PRODUCTDATE"];
                    dd["VALIDDATE"] = bb["VALIDDATE"];
                    dd["WSXKZH"] = bb["WSXKZH"];
                    dd["MEMO"] = bb["MEMO"];
                    dd["TXM"] = bb["TXM"];
                    dd["RECIPECODE"] = aa["RECIPECODE"];
                    dd["SUPPLYID2"] = indata["SUPPLYID2"];
                    dd["SHDH"] = indata["SHDH"];
                    dd["SUPPLYNAME2"] = indata["SUPPLYNAME2"];
                    dd["CHOSCODE"] = data.Param["CHOSCODE"].ToString();

                    dd["SUPPLYID"] = bb["SUPPLYID"];
                    dd["INDATE"] = DateTime.Now;
                    Opt stockdetailtable = OptContent.get("SaveInStockDetailWZPanDianIn");
                    if (DaoTool.Save(dao, stockdetailtable, dd) < 0)
                        throw new Exception("生成库存细表失败！");
                 }
  
                }





                Opt getpdout = OptContent.get("GetPDIDWZPanDianInfoOut");
                listout = DaoTool.Find(dao, getpdout, pa);
                if (listout!=null && listout.Count > 0)
                {
                  
                    //生成出库主表
                    aa["OUTID"] = DaoTool.Seq(dao, "LKWZ.SEQWZOUT");

                    Opt getioid = OptContent.get("GetIOIDWZPanDianInfoStockOut");
                    ObjItem objitem = DaoTool.ExecuteScalar(dao, getioid, pa);
                    aa["IOID"] = Convert.ToDecimal(objitem.ToString());
                    //单据号
                    DataTable dt = dao.Fd(OptContent.get("SearchDicWZInOut").Sql, new object[] { aa["IOID"].ToString() });
                    string recipe = dt.Rows[0]["RECIPECODE"].ToString();
                    if (Convert.ToDecimal(dt.Rows[0]["RECIPEYEAR"]) == 1)
                    {
                        recipe += DateTime.Now.Year.ToString();
                    }
                    if (Convert.ToDecimal(dt.Rows[0]["RECIPEMONTH"]) == 1)
                    {
                        if (DateTime.Now.Month < 10)
                        {
                            recipe = recipe + "0" + DateTime.Now.Month.ToString();
                        }
                        else
                        {
                            recipe += DateTime.Now.Month.ToString();
                        }

                    }


                    decimal recipeno = Convert.ToDecimal(dao.ExecuteScalar(OptContent.get("GetRecipeNo").Sql, new object[] { recipe + '%' })) + 1;
                    if (recipeno > 0 && recipeno < 10)
                    {
                        recipe = recipe + "0" + recipeno.ToString();
                    }
                    else
                    {
                        recipe = recipe + recipeno.ToString();
                    }

                    aa["RECIPECODE"] = recipe;
                    aa["OPFLAG"] = 3;
                    aa["MEMO"] = data.Param["MEMO"].ToString();
                    aa["RECDATE"] = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    aa["STATUS"] = 6;
                    aa["WARECODE"] =data.Param["WARECODE"].ToString();
                    aa["OUTDATE"] = DateTime.Now;
                    aa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                    aa["USERNAME"] = data.Param["USERNAME"].ToString();
                    aa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();

                    //得到总金额和零售总金额
                    decimal total = 0;
                    decimal lstotal = 0;
                    foreach (var outdata in listout)
                    {
                        total += Convert.ToDecimal(outdata["PRICE"]) *Math.Abs( Convert.ToDecimal(outdata["YKNUM"]));
                        lstotal += Convert.ToDecimal(outdata["LSPRICE"]) *Math.Abs(Convert.ToDecimal(outdata["YKNUM"]));
                    }
                    aa["TOTALMONEY"] = total;
                    aa["LSTOTALMONEY"] = lstotal;

                    Opt savewzoutmain = OptContent.get("SaveWzOutMainWZPanDianOut");
                    if (DaoTool.Save(dao, savewzoutmain, aa) < 0)
                        throw new Exception("生成出库主表失败！");



                    //向盘点主表中加入出库单序号

                    ll["PDID"] = Convert.ToDecimal(Convert.ToDecimal(data.Param["PDID"]));
                    ll["OUTID"] = aa["OUTID"];
                    ll["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                    Opt pdwz_updataed2 = OptContent.get("UpdataInWZPanDianInfo_Out");
                    if (DaoTool.ExecuteNonQuery(dao, pdwz_updataed2, ll) < 0)
                        throw new Exception("向盘点主表添加出库单序号失败！");




                    //生成出库细表

                    bb["OUTID"] = aa["OUTID"];
                    data.Param.Add("WZID", null);
                    data.Param.Add("STOCKID",null);
                    foreach (Dictionary<string,object> outdata in listout)
                    {
                        bb["DETAILNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZINDETAIL");
                        bb["WZID"] = Convert.ToDecimal(outdata["WZID"]);
                       
                        bb["UNITCODE"] = outdata["LSUNITCODE"];//盘盈数据里的最小编码？无编码

                        bb["NUM"] = Convert.ToDecimal(outdata["YKNUM"]) * (-1);
                        bb["INPRICE"] = Convert.ToDecimal(outdata["PRICE"]);
                        bb["INMONEY"] = Math.Round(Convert.ToDecimal(outdata["PRICE"]) * Convert.ToInt32(bb["NUM"]), 4);
                        bb["PRICE"] = Convert.ToDecimal(outdata["LSPRICE"]);
                        bb["MONEY"] = Math.Round(Convert.ToDecimal(outdata["LSPRICE"]) * Convert.ToInt32(bb["NUM"]), 4);
                        bb["PRODUCTDATE"] = Convert.ToDateTime(outdata["PRODUCTDATE"]);
                        bb["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                        bb["STOCKFLOWNO"] = outdata["STOCKFLOWNO"];
                        bb["PH"] = outdata["PH"];
                        bb["PZWH"] = outdata["PZWH"];
                        bb["SUPPLYID"] = outdata["SUPPLYID"];
                        bb["SUPPLYNAME"] = outdata["SUPPLYNAME"];
                        bb["VALIDDATE"] = outdata["VALIDDATE"];
                        bb["WSXKZH"] = outdata["WSXKZH"];
                        bb["MEMO"] = outdata["MEMO"];
                        bb["TXM"] = outdata["TXM"];


                        Opt savewzoutdetail = OptContent.get("SaveWzOutDetailWZPanDianOut");
                        if (DaoTool.Save(dao, savewzoutdetail, bb) < 0)
                            throw new Exception("生成出库细表失败！");

                        //向盘点细表中加入入库流水号
                        jj["STOCKFLOWNO"] = Convert.ToDecimal(outdata["STOCKFLOWNO"]);
                        jj["PDID"] = Convert.ToDecimal(outdata["PDID"]);
                        jj["DETAILNO"] = bb["DETAILNO"];
                        jj["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                        Opt pdwz_updatadetail2 = OptContent.get("UpdataInDetailWZPanDianInfo_Out");
                        if (DaoTool.ExecuteNonQuery(dao, pdwz_updatadetail2, jj) < 0)
                            throw new Exception("向盘点细表添加入库流水号失败！");




                        //生成库存总表
                        bb["WARECODE"] =data.Param["WARECODE"].ToString();
                        Opt stocknum = OptContent.get("iflsunitcodeInStockWZPanDianIn");//获得库存数量
                        ObjItem objtem4 = DaoTool.ExecuteScalar(dao, stocknum, bb);

                        Opt lsunitcode = OptContent.get("iflsunitcodeInWZDictWZPanDianIn");//获得最小单位编码在WZ中获得
                        ObjItem objtem5 = DaoTool.ExecuteScalar(dao, lsunitcode, bb);

                        Opt hsxs = OptContent.get("HsxsInWZDictWZPanDianIn");//获得换算系数在WZ中获得
                        ObjItem objtem6 = DaoTool.ExecuteScalar(dao, hsxs, bb);

                             Opt ifexist = OptContent.get("IfExistInStockWZPanDianIn");
                    ObjItem objtem2 = DaoTool.ExecuteScalar(dao, ifexist, bb);//是否在库存表中已存在

                        cc["STOCKID"] = objtem2.ToInt();
                        cc["WARECODE"] = aa["WARECODE"];
                        cc["WZID"] = Convert.ToDecimal(outdata["WZID"]);
                        cc["CHOSCODE"] = aa["CHOSCODE"];
                        cc["LSUNITCODE"] = objtem5.ToInt();
                            if (bb["UNITCODE"].ToString() == objtem5.ToString())
                            {

                                cc["NUM"] = objtem4.ToInt() - Convert.ToInt32(outdata["YKNUM"].ToString()) * (-1);
                             


                            }
                            else
                            {
                                cc["NUM"] = objtem4.ToInt() - objtem6.ToInt() * Convert.ToInt32(outdata["YKNUM"].ToString()) * (-1);
                
                               
                            }




                            Opt updatastocktable = OptContent.get("UpdataInStockWZPanDianIn");
                            if (DaoTool.ExecuteNonQuery(dao, updatastocktable, cc) < 0)
                                throw new Exception("生成库存总表失败！");

                        //生成库存细表
                        //YtService.data.OptData getdata=new YtService.data.OptData();
                       
                        data.Param["WZID"]= Convert.ToDecimal(outdata["WZID"]);
                        data.Param["STOCKID"]= objtem2.ToInt();
                      
                       
                        Opt stockdetailinfo = OptContent.get("StockDetailInfoWZPanDianOut");//获得库存细表信息
                        DataTable datatable = DaoTool.FindDT(dao, stockdetailinfo, data);
                        
                        DataRow dr = datatable.Rows[0];
                         if (bb["UNITCODE"].ToString() == objtem5.ToString())
                            {

                                dd["OUTNUM"] =Convert.ToInt32(dr["OUTNUM"]) + Convert.ToInt32(outdata["YKNUM"].ToString())*(-1);
                             


                            }
                            else
                            {
                                 dd["OUTNUM"] =Convert.ToInt32(dr["OUTNUM"]) + objtem6.ToInt() * Convert.ToInt32(outdata["YKNUM"].ToString())*(-1);
                
                               
                            }
                        dd["FLOWNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZSTOCKDETAIL");

                        dd["INID"] =dr["INID"];
                        dd["WARECODE"] = dr["WARECODE"];
                        dd["WZID"] =dr["WZID"];
                        dd["STOCKID"] = dr["STOCKID"];
                        dd["NUM"] = dr["NUM"];
                        dd["BEFORENUM"] = dr["BEFORENUM"];
                        dd["LSUNITCODE"] = dr["LSUNITCODE"];
                        dd["CHANGERATE"] = dr["CHANGERATE"];
                        dd["PRICE"] = dr["PRICE"];
                        dd["LSPRICE"] = dr["LSPRICE"];
                        dd["PH"] = dr["PH"];
                        dd["PZWH"] = dr["PZWH"];
                        dd["SUPPLYNAME"] = dr["SUPPLYNAME"];
                        dd["PRODUCTDATE"] = dr["PRODUCTDATE"];
                        dd["VALIDDATE"] = dr["VALIDDATE"];
                        dd["WSXKZH"] = dr["WSXKZH"];
                        dd["MEMO"] = dr["MEMO"];
                        dd["TXM"] = dr["TXM"];
                        dd["RECIPECODE"] = dr["RECIPECODE"];
                        dd["SUPPLYID2"] = dr["SUPPLYID2"];
                        dd["SHDH"] = dr["SHDH"];
                        dd["SUPPLYNAME2"] = dr["SUPPLYNAME2"];
                        dd["CHOSCODE"] = dr["CHOSCODE"];

                        dd["SUPPLYID"] = dr["SUPPLYID"];
                        dd["INDATE"] = dr["INDATE"];
                        Opt stockoutdetailtable = OptContent.get("SaveInStockDetailWZPanDianIn");
                        if (DaoTool.Save(dao, stockoutdetailtable, dd) < 0)
                            throw new Exception("生成库存细表失败！");
                    }

                }
        
            msg = "结转成功！";
                    return "ok";
                }
       
            
            return "ok";
        }
        #endregion
    }
}
