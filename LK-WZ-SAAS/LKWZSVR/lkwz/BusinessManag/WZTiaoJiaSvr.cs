using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YiTian.db;
using YtService.config;
using YtService.util;
using System.Data;

namespace LKWZSVR.lkwz.BusinessManag
{
    class WZTiaoJiaSvr:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            msg = "盘点信息";
            Dictionary<string, object> pa = new Dictionary<string, object>();
            Dictionary<string, object> pa1 = new Dictionary<string, object>();
            List<Dictionary<string, object>> gettjdetail = new List<Dictionary<string, object>>();
            string ac = data.Sql;
            //ObjItem Obj;

            if ("SaveWZTiaoJiaInfo".Equals(ac))
            {

                pa["TJDATE"] = DateTime.Now;
                pa["WARECODE"] = data.Param["WARECODE"].ToString();
                pa["TJREASON"] = data.Param["TJREASON"].ToString();
                
                pa["STATUS"] = Convert.ToDecimal(data.Param["STATUS"]);
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);

                pa["USERNAME"] = data.Param["USERNAME"].ToString();
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

                pa["TJID"] = DaoTool.Seq(dao, "LKWZ.SEQWZPD");
                Opt saveInfo = OptContent.get("SaveWZTiaoJiaMainInfo");


                if (DaoTool.Save(dao, saveInfo, pa) < 0)
                    throw new Exception("新建调价主表失败！");

                if (data.Param["MyCount"] != null && data.Param["STOCKFLOWNO" + 1] != null)
                {
                    Opt savedetailInfo = OptContent.get("SaveWZTiaoJiaDetailInfo");

                    for (int i = 1; i < Convert.ToInt32(data.Param["MyCount"]); i++)
                    {
                        pa1["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                        pa1["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO" + i]);
                        DataTable dt = DaoTool.FindDT(dao, "WZPZ_FindWZStockDetailInfo", pa1);

                        DataRow r = dt.Rows[0];
                        pa["NEWPRICE"] = Convert.ToDecimal(data.Param["NEWPRICE" + i]);
                        if (r["OUTNUM"] != null || r["OUTNUM"].ToString().Trim().Length > 0)
                        {
                            pa["NUM"] = Convert.ToDecimal(r["NUM"]) - Convert.ToDecimal(r["OUTNUM"]);//获得调价数量
                        }
                        else
                        {
                            pa["NUM"] = Convert.ToDecimal(r["NUM"]);//获得调价数量
                        }
                        pa["STOCKFLOWNO"] = Convert.ToDecimal(r["FLOWNO"]);
                        pa["STOCKID"] = Convert.ToDecimal(r["STOCKID"]);
                        pa["WZID"] = Convert.ToDecimal(r["WZID"]);
                        pa["PRODUCTDATE"] = Convert.ToDateTime(r["PRODUCTDATE"]);
                        pa["INDATE"] = Convert.ToDateTime(r["INDATE"]);
                        pa["RECIPECODE"] = r["RECIPECODE"].ToString();
                        pa["CHOSCODE"] = r["CHOSCODE"].ToString();
                        #region paras
                        if (r["LSUNITCODE"] != null)
                        {
                            if (r["LSUNITCODE"].ToString() != "")
                            {
                                pa["UNITCODE"] = Convert.ToDecimal(r["LSUNITCODE"]);
                            }
                        }
                        else
                        {
                            pa["UNITCODE"] = null;
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

                                pa["OLDPRICE"] = Convert.ToSingle(r["LSPRICE"]);
                            }
                        }
                        else
                        {
                            pa["OLDPRICE"] = null;
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
                      

                        if (DaoTool.Save(dao, savedetailInfo, pa) < 0)
                            throw new Exception("添加调价细表" + r["WZNAME"] + "信息失败！");
                    }
                }
                msg = "添加调价信息成功！|" + pa["TJID"];
                return "ok";
            }
            if ("UpdataWZTiaoJiaInfo".Equals(ac))
            {


                pa["WARECODE"] = data.Param["WARECODE"].ToString();
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["TJREASON"] = data.Param["TJREASON"].ToString();
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }



                pa["TJID"] = Convert.ToDecimal(data.Param["TJID"]);
                Opt updataInfo = OptContent.get("UpdataWZTiaoJiaInfo");


                if (DaoTool.ExecuteNonQuery(dao, updataInfo, pa) < 0)
                    throw new Exception("修改调价主表信息失败！");

                if (data.Param["MyCount"] != null)
                {
                    Opt ifsaveorupdata = OptContent.get("IfSaveOrUpdataWZUseDetailInfo");

                    Opt saveInfodedetail = OptContent.get("SaveWZTiaoJiaDetailInfo");


                    for (int i = 1; i < Convert.ToInt32(data.Param["MyCount"]); i++)
                    {
                        pa1["WZID"] = Convert.ToDecimal(data.Param["WZID" + i]);
                        pa1["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO" + i]);
                        pa1["TJID"] = Convert.ToDecimal(data.Param["TJID"]);
                        pa1["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                        DataTable dt = DaoTool.FindDT(dao, "WZPZ_FindWZStockDetailInfo", pa1);

                        DataRow r = dt.Rows[0];

                        ObjItem num = DaoTool.ExecuteScalar(dao, ifsaveorupdata, pa1);

                        if (!num.IsNull)
                        {


                            pa["NEWPRICE"] = Convert.ToDecimal(data.Param["NEWPRICE" + i]);
                            pa["CHOSCODE"] = r["CHOSCODE"].ToString();
                            pa["STOCKFLOWNO"] = Convert.ToDecimal(r["FLOWNO"]);
                            pa["TJID"] = Convert.ToDecimal(data.Param["TJID"]);
                            Opt updatadetailInfo = OptContent.get("UpdataeWZTiaoJiaDetailInfo");

                            if (DaoTool.ExecuteNonQuery(dao, updatadetailInfo, pa) < 0)
                                throw new Exception("修改调价细表" + r["WZNAME"] + "信息失败！！");
                        }
                        else //修改带新增调价信息
                        {


                            pa["NEWPRICE"] = Convert.ToDecimal(data.Param["NEWPRICE" + i]);

                            if (r["OUTNUM"] != null || r["OUTNUM"].ToString().Trim().Length > 0)
                            {
                                pa["NUM"] = Convert.ToDecimal(r["NUM"]) - Convert.ToDecimal(r["OUTNUM"]);//获得调价数量
                            }
                            else
                            {
                                pa["NUM"] = Convert.ToDecimal(r["NUM"]);//获得调价数量
                            }
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
                                throw new Exception("在修改中添加调价细表" + r["WZNAME"] + "信息失败！");

                        }

                    }




                    msg = "录入成功！|" + pa["TJID"];
                    return "ok";
                }

            }
            if ("SaveWZTiaoJiaDetailInfo".Equals(ac))
            {
                pa["TJID"] = Convert.ToDecimal(data.Param["TJID"]);
                pa["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO"]);
                pa["WZID"] = Convert.ToDecimal(data.Param["WZID"]);
                pa["STOCKID"] = Convert.ToDecimal(data.Param["STOCKID"]);
                pa["NUM"] = Convert.ToDecimal(data.Param["NUM"]);
                if (data.Param["UNITCODE"] != null)
                {
                    if (data.Param["UNITCODE"].ToString() != "")
                    {
                        pa["UNITCODE"] = Convert.ToDecimal(data.Param["UNITCODE"]);
                    }
                }
                else
                {
                    pa["UNITCODE"] = null;
                }
                if (data.Param["NEWPRICE"] != null)
                {
                    if (data.Param["NEWPRICE"].ToString() != "")
                    {

                        pa["NEWPRICE"] = Convert.ToSingle(data.Param["NEWPRICE"]);
                    }
                }
                else
                {
                    pa["NEWPRICE"] = null;
                }
                if (data.Param["OLDPRICE"] != null)
                {
                    if (data.Param["OLDPRICE"].ToString() != "")
                    {

                        pa["OLDPRICE"] = Convert.ToSingle(data.Param["OLDPRICE"]);
                    }
                }
                else
                {
                    pa["OLDPRICE"] = null;
                }
                if (data.Param["OLDPRICE"] != null)
                {
                    if (data.Param["OLDPRICE"].ToString() != "")
                    {

                        pa["OLDPRICE"] = Convert.ToSingle(data.Param["OLDPRICE"]);
                    }
                }
                else
                {
                    pa["OLDPRICE"] = null;
                }

                if (data.Param["PH"] != null)
                {
                    pa["PH"] = data.Param["PH"].ToString();
                }
                else
                {
                    pa["PH"] = null;
                }
                if (data.Param["PZWH"] != null)
                {
                    pa["PZWH"] = data.Param["PZWH"].ToString();
                }
                else
                {
                    pa["PZWH"] = null;
                }
                if (data.Param["SUPPLYID"] != null)
                {
                    if (data.Param["SUPPLYID"].ToString() != "")
                    {

                        pa["SUPPLYID"] = Convert.ToDecimal(data.Param["SUPPLYID"]);
                    }
                }
                else
                {
                    pa["SUPPLYID"] = null;
                }
                if (data.Param["SUPPLYNAME"] != null)
                {
                    pa["SUPPLYNAME"] = data.Param["SUPPLYNAME"].ToString();
                }
                else
                {
                    pa["SUPPLYNAME"] = null;
                }
                if (data.Param["VALIDDATE"] != null)
                {
                    if (data.Param["VALIDDATE"].ToString() != "")
                    {

                        pa["VALIDDATE"] = Convert.ToDateTime(data.Param["VALIDDATE"]);
                    }
                }
                else
                {
                    pa["VALIDDATE"] = null;
                }
                if (data.Param["WSXKZH"] != null)
                {
                    pa["WSXKZH"] = data.Param["WSXKZH"].ToString();
                }
                else
                {
                    pa["WSXKZH"] = null;
                }
                if (data.Param["SHDH"] != null)
                {
                    pa["SHDH"] = data.Param["SHDH"].ToString();
                }
                else
                {
                    pa["SHDH"] = null;
                }
                if (data.Param["SUPPLYID2"] != null)
                {

                    if (data.Param["SUPPLYID2"].ToString() != "")
                    {
                        pa["SUPPLYID2"] = Convert.ToDecimal(data.Param["SUPPLYID2"]);
                    }
                }
                else
                {
                    pa["SUPPLYID2"] = null;
                }
                if (data.Param["SUPPLYNAME2"] != null)
                {
                    pa["SUPPLYNAME2"] = data.Param["SUPPLYNAME2"].ToString();
                }
                else
                {
                    pa["SUPPLYNAME2"] = null;

                }
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;

                }
                if (data.Param["TXM"] != null)
                {
                    pa["TXM"] = data.Param["TXM"].ToString();
                }
                else
                {
                    pa["TXM"] = null;

                }

                pa["PRODUCTDATE"] = Convert.ToDateTime(data.Param["PRODUCTDATE"]);
                pa["INDATE"] = Convert.ToDateTime(data.Param["INDATE"]);
                pa["RECIPECODE"] = data.Param["RECIPECODE"].ToString();
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
  
                Opt saveInfo = OptContent.get("SaveWZTiaoJiaDetailInfo");


                if (DaoTool.Save(dao, saveInfo, pa) < 0)
                    throw new Exception("新建调价细表失败!");
                msg = "新建细表成功！";
                return "ok";
            }
            if ("UpdataeWZTiaoJiaDetailInfo".Equals(ac))
            {


        
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["NEWPRICE"] = data.Param["NEWPRICE"].ToString();
                pa["TJID"] = Convert.ToDecimal(data.Param["TJID"]);
                pa["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO"]);

                Opt updataInfo = OptContent.get("UpdataeWZTiaoJiaDetailInfo");



                if (DaoTool.ExecuteNonQuery(dao, updataInfo, pa) < 0)
                    throw new Exception("修改调价主表信息失败！");
                msg = "修改成功！";
                return "ok";


            }

            if ("DelWZTiaoJiaInfo".Equals(ac))
            {


                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["STATUS"] = data.Param["STATUS"].ToString();
                pa["TJID"] = Convert.ToDecimal(data.Param["TJID"]);
                Opt delInfo = OptContent.get("DelWZTiaoJiaInfo");



                if (DaoTool.ExecuteNonQuery(dao, delInfo, pa) < 0)
                    throw new Exception("删除调价信息失败！");
                msg = "删除成功！";
                return "ok";

            }
            if ("DelWZTiaoJiaDetailInfo".Equals(ac))
            {


                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["TJID"] = Convert.ToDecimal(data.Param["TJID"]);
                pa["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO"]);
                Opt delInfo = OptContent.get("DelWZTiaoJiaDetailInfo");



                if (DaoTool.ExecuteNonQuery(dao, delInfo, pa) < 0)
                    throw new Exception("删除调价信息失败！");
                msg = "删除成功！";
                return "ok";

            }

                   
            if ("ShenHeWZTiaoJiaInfo".Equals(ac))
            {
                pa["TJID"] = Convert.ToDecimal(data.Param["TJID"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["STATUS"] = data.Param["STATUS"].ToString();
                pa["SHUSERID"] = Convert.ToDecimal(data.Param["SHUSERID"]);
                pa["SHUSERNAME"] = data.Param["SHUSERNAME"].ToString();
                pa["SHDATE"] = DateTime.Now;

                Opt saveInfo = OptContent.get("ShenHeWZTiaoJiaInfo");        
                    if (DaoTool.ExecuteNonQuery(dao, saveInfo, pa) < 0)
                        throw new Exception("审核失败！");
                    msg = "审核成功！";
                    return "ok";
                }
            if ("CancelShenHeWZTiaoJianInfo".Equals(ac))
            {
                pa["TJID"] = Convert.ToDecimal(data.Param["TJID"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["STATUS"] = data.Param["STATUS"].ToString();
                pa["SHUSERID"] =null;
                pa["SHUSERNAME"] = null;
                pa["SHDATE"] = null;

                Opt saveInfo = OptContent.get("CancelShenHeWZTiaoJianInfo");
                if (DaoTool.ExecuteNonQuery(dao, saveInfo, pa) < 0)
                    throw new Exception("取消审核失败！");
                msg = "取消审核成功！";
                return "ok";
            }
            if ("SHInWZPanDianInfo".Equals(ac))
            {
                pa["TJID"] = Convert.ToDecimal(data.Param["TJID"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["STATUS"] = data.Param["STATUS"].ToString();
                pa["SHINUSERID"] = Convert.ToDecimal(data.Param["SHINUSERID"]);
                pa["SHINUSERNAME"] = data.Param["SHINUSERNAME"].ToString();
                pa["SHINDATE"] = DateTime.Now;

                Opt saveInfo = OptContent.get("SHInWZPanDianInfo");
                if (DaoTool.ExecuteNonQuery(dao, saveInfo, pa) < 0)
                    throw new Exception("确认失败！");
                //更新对应库存流水记录里的零售单价
                Opt getpdin = OptContent.get("ScanWZTiaoJiaDetailInfo");
                 gettjdetail = DaoTool.Find(dao, getpdin, pa);
                 if (gettjdetail!=null && gettjdetail.Count > 0)
                 {
                     foreach (Dictionary<string, object> outdata in gettjdetail)
                     {

                         pa["STOCKFLOWNO"] = Convert.ToDecimal(outdata["STOCKFLOWNO"]);
                         pa["NEWPRICE"] = Convert.ToDecimal(outdata["NEWPRICE"]);
                         Opt chgprice = OptContent.get("ChangPriceInWZStockDetailInfo");
                         if (DaoTool.ExecuteNonQuery(dao, chgprice, pa) < 0)
                            throw new Exception("调价库存流水表失败！");
                     }

                 }
                 msg = "确认成功！";
                 return "ok";  
            }
            return "ok";
        }

        #endregion
    }
}
