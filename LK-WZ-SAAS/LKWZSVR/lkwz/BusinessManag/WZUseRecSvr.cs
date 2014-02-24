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
    class WZUseRecSvr:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
             msg = "使用信息";
            Dictionary<string, object> pa = new Dictionary<string, object>();
            Dictionary<string, object> pa1 = new Dictionary<string, object>();
            string ac = data.Sql;
           // ObjItem Obj;
            decimal num_update=0;
       
            if ("SaveWZUseRecInfo".Equals(ac))
            {
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();

                pa["WARECODE"] = data.Param["WARECODE"].ToString();
                pa["SICKCODE"] = data.Param["SICKCODE"].ToString();
                pa["SICKNAME"] = data.Param["SICKNAME"].ToString();

                pa["STATUS"] = Convert.ToDecimal(data.Param["STATUS"]);
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                pa["RECDATE"] = DateTime.Now;
                if (data.Param["USERNAME"] != null)
                {
                    pa["USERNAME"] = data.Param["USERNAME"].ToString();
                }
                else
                {
                    pa["USERNAME"] = null;
                }



                if (data.Param["MyCount"] != null && data.Param["STOCKFLOWNO" + 1] != null)
                {
                    //Opt saveInfodedetail = OptContent.get("SaveWZPanDianDetailInfo");
                    for (int i = 1; i < Convert.ToInt32(data.Param["MyCount"]); i++)
                    {
                        pa1["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                        pa1["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO" + i]);
                        DataTable dt = DaoTool.FindDT(dao, "WZPZ_FindWZStockDetailInfo", pa1);

                        DataRow r = dt.Rows[0];

                        pa["STOCKFLOWNO"] = Convert.ToDecimal(r["FLOWNO"]);
                        pa["STOCKID"] = Convert.ToDecimal(r["STOCKID"]);
                        pa["WZID"] = Convert.ToDecimal(r["WZID"]);
                        pa["WZNAME"] = r["WZNAME"].ToString();
                        pa["RECIPECODE"] = r["RECIPECODE"].ToString();
                        pa["PRODUCTDATE"] = Convert.ToDateTime(r["PRODUCTDATE"]);
                        pa["USENUM"] = Convert.ToDecimal(data.Param["USENUM" + i]);
                        pa["INDATE"] = Convert.ToDateTime(r["INDATE"]);
                        


                        pa["SENDDEPTID"] = Convert.ToDecimal(data.Param["SENDDEPTID" + i]);
                        pa["RECVDEPTID"] = Convert.ToDecimal(data.Param["RECVDEPTID" + i]);
                       
                        pa["USEDATE"] = Convert.ToDateTime(data.Param["USEDATE" + i]);

                        //pa["STOCKNUM"] = Convert.ToDecimal(r["NUM"]);
                        pa["PRODUCTDATE"] = Convert.ToDateTime(r["PRODUCTDATE"]);
          
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

                        if (data.Param["USENO" + i] == null || data.Param["USENO" + i].ToString().Trim().Length==0)
                        {
                            pa["USENO"] = DaoTool.Seq(dao, "LKWZ.SEQWZUSE");
                            Opt saveInfo = OptContent.get("SaveWZUseRecInfo");

                            //获得原库存数量
                            Opt findWZUseInNumInfo = OptContent.get("findWZUseInNumInfo");

                            ObjItem objitm = DaoTool.ExecuteScalar(dao, findWZUseInNumInfo, pa);

                            pa["NUM"] = Convert.ToDecimal(objitm.ToString()) - Convert.ToDecimal(data.Param["USENUM" + i]);

                          
                                if (DaoTool.Save(dao, saveInfo, pa) < 0)//保存使用记录表
                                    throw new Exception("登记使用" + r["WZNAME"] + "失败");


                                //修改库存总表中物资的数量

                                Opt WZStock_Updata_Ye = OptContent.get("WZStock_Updata_Ye");
                               
                                //获得现库存数量

                                if (DaoTool.ExecuteNonQuery(dao, WZStock_Updata_Ye, pa) < 0)
                                    throw new Exception("修改库存数量失败！");

                                //增加一条物资流水记录
                                pa["FLOWNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZSTOCKDETAIL");
                               
                               //获得出库数
                                pa["OUTNUM"] = Convert.ToDecimal(r["OUTNUM"]) + Convert.ToDecimal(data.Param["USENUM" + i]);
                                pa["INID"] = Convert.ToDecimal(r["INID"]);
                                pa["NUM"] = Convert.ToDecimal(r["NUM"]);
                                pa["BEFORENUM"] = Convert.ToDecimal(r["BEFORENUM"]);
                                pa["CHANGERATE"] = Convert.ToDecimal(r["CHANGERATE"]);
                                Opt savedetailInfo = OptContent.get("WZStockDetail_Add_Ye");

                                if (DaoTool.Save(dao, savedetailInfo, pa) < 0)
                                    throw new Exception("添加库存流水记录失败！");
                          
                        }
                        else
                        {
                            //获得原使用数量
                            pa["USENO"] = Convert.ToDecimal(data.Param["USENO" + i]);
                            Opt updataInfo_Use_SeachNum = OptContent.get("updataInfo_Use_SeachNum");
                            ObjItem objitem1 = DaoTool.ExecuteScalar(dao, updataInfo_Use_SeachNum, pa);

                            
                            Opt updataInfo_Use = OptContent.get("UpdataWZUseRecInfo");
                            if (DaoTool.ExecuteNonQuery(dao, updataInfo_Use, pa) < 0)
                                throw new Exception("修改使用信息失败！");

                          

                         if (!objitem1.IsNull)
                         {
                             num_update= Convert.ToDecimal(pa["USENUM"]) - objitem1.ToDecimal();
                             Opt findWZUseInNumInfo = OptContent.get("findWZUseInNumInfo");//获得原库存数量
                             ObjItem objitm = DaoTool.ExecuteScalar(dao, findWZUseInNumInfo, pa);
                             Opt WZStock_Updata_Ye = OptContent.get("WZStock_Updata_Ye");

                             if (num_update != 0)//其实是一样的
                             {
                                 pa["NUM"] = Convert.ToDecimal(objitm.ToString()) - num_update;
                                 //修改现库存数量
                                 if (DaoTool.ExecuteNonQuery(dao, WZStock_Updata_Ye, pa) < 0)
                                     throw new Exception("修改库存数量失败！");

                            
                               
                             }
                           
                             //增加一条物资流水记录
                          
                             //获得出库数

                              //获得原数量
                         //   Opt updataInfo_Use_SeachOutNum = OptContent.get("updataInfo_Use_SeachNum");
                         //ObjItem objitem2=DaoTool.ExecuteScalar(dao, updataInfo_Use_SeachOutNum, pa);
                         //if (!objitem2.IsNull)
                         //{
                         //    num_update= Convert.ToDecimal(pa["OUTNUM"]) - objitem1.ToDecimal();//修改的数量差距（修改的原数量-原来的库存数量）
                           
                            
                             Opt findWZUseOutNumInfo = OptContent.get("findWZUseOutNumInfo");//获得流水表中现出库数量(以该物资改库存ID的最大流水号获得）
                             ObjItem objitm3 = DaoTool.ExecuteScalar(dao, findWZUseOutNumInfo, pa);

                             //Opt WZStockFlo_Updata_Ye = OptContent.get("WZStock_Updata_Ye");
                             if (num_update != 0)//如果出库数不变则不修改
                             {
                                 pa["OUTNUM"] = Convert.ToDecimal(objitm3.ToString())+ num_update;
                                 pa["FLOWNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZSTOCKDETAIL");
                                 pa["INID"] = Convert.ToDecimal(r["INID"]);
                                 pa["NUM"] = Convert.ToDecimal(r["NUM"]);
                                 pa["BEFORENUM"] = Convert.ToDecimal(r["BEFORENUM"]);
                                 pa["CHANGERATE"] = Convert.ToDecimal(r["CHANGERATE"]);
                                 Opt savedetailInfo = OptContent.get("WZStockDetail_Add_Ye");

                                 if (DaoTool.Save(dao, savedetailInfo, pa) < 0)
                                     throw new Exception("添加库存流水记录失败！");



                             }

                          
                            
                         }


                      }
                     
               
                    }
                }
                msg = "录入使用登记成功！" ;
                return "ok";
            }
            #region 修改（原来功能）
            // if ("UpdataWZUseRecInfo".Equals(ac))
            // {

            //     pa["USENO"] =  Convert.ToDecimal(data.Param["USENO"]);

            //     Opt updataInfo_Use = OptContent.get("UpdataWZUseRecInfo");
            //     if (DaoTool.ExecuteNonQuery(dao, updataInfo_Use, pa) < 0)
            //         throw new Exception("修改使用信息失败！");

            //     //pa["WZNAME"] = data.Param["WZNAME"].ToString();                                                                                                                                                                                                                                                                                                                            
            //     //pa["LSUNITCODE"] = Convert.ToDecimal(data.Param["LSUNITCODE"]);
            //     pa["USENUM"] = Convert.ToDecimal(data.Param["USENUM"]);
            //     pa["USEDATE"] = DateTime.Now;
            //     pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();

            //     //if (data.Param["SICKNAME"] != null)
            //     //{
            //     //    pa["SICKNAME"] = data.Param["SICKNAME"].ToString();
            //     //}
            //     //else
            //     //{
            //     //    pa["SICKNAME"] = "";
            //     //}
            //     //if (data.Param["SICKCODE"] != null)
            //     //{
            //     //    pa["SICKCODE"] = data.Param["SICKCODE"].ToString();
            //     //}
            //     //else
            //     //{
            //     //    pa["SICKCODE"] = "";
            //     //}

            //     //if (data.Param["SENDDEPTID"] != null)
            //     //{
            //     //    if (data.Param["SENDDEPTID"] != "")
            //     //    {
            //     //        pa["SENDDEPTID"] = Convert.ToDecimal(data.Param["SENDDEPTID"]);
            //     //    }
            //     //    else
            //     //    {
            //     //        pa["SENDDEPTID"] = "";
            //     //    }
            //     //}

            //     //if (data.Param["RECVDEPTID"] != null)
            //     //{
            //     //    if (data.Param["RECVDEPTID"] != "")
            //     //    {
            //     //        pa["RECVDEPTID"] = Convert.ToDecimal(data.Param["RECVDEPTID"]);
            //     //    }
            //     //    else
            //     //    {
            //     //        pa["RECVDEPTID"] = "";
            //     //    }
            //     //}


            //     Opt updataInfo = OptContent.get("UpdataWZUseRecInfo");


            //if (DaoTool.ExecuteNonQuery(dao, updataInfo, pa) < 0)
            //         throw new Exception("修改使用信息失败！");
            //msg = "修改使用信息成功！";
            //     return "ok";

            // } 
            #endregion
            if ("DeleteWZUseRecInfo".Equals(ac))
            {

                pa["USENO"] =  Convert.ToDecimal(data.Param["USENO"]);
                pa["USENUM"] = Convert.ToDecimal(data.Param["USENUM"]);
                pa["STOCKID"] = Convert.ToDecimal(data.Param["STOCKID"]);
                pa["WZID"] = Convert.ToDecimal(data.Param["WZID"]);
                pa["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO"]);
                pa["WARECODE"] = data.Param["WARECODE"].ToString();
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
             
                Opt deleteInfo = OptContent.get("DeleteWZUseRecInfo");
                if (DaoTool.ExecuteNonQuery(dao, deleteInfo, pa) < 0)//删除WZUSEREC中的信息
                    throw new Exception("删除使用信息失败！"); 


                Opt findWZUseInNumInfo = OptContent.get("findWZUseInNumInfo");//获得原库存数量
                ObjItem objitm = DaoTool.ExecuteScalar(dao, findWZUseInNumInfo, pa);
                Opt WZStock_Updata_Ye2 = OptContent.get("WZStock_Updata_Ye");


                pa["NUM"] = Convert.ToDecimal(objitm.ToString()) + Convert.ToDecimal(data.Param["USENUM"]);
                    //修改现库存数量
                if (DaoTool.ExecuteNonQuery(dao, WZStock_Updata_Ye2, pa) < 0)//修改WZSTOCK中的信息
                        throw new Exception("修改库存数量失败！");

                   
                
                Opt findWZUseOutNumInfo = OptContent.get("findWZUseOutNumInfo");//获得流水表中现出库数量(以该物资改库存ID的最大流水号获得）
                    ObjItem objitm3 = DaoTool.ExecuteScalar(dao, findWZUseOutNumInfo, pa);

                    //Opt WZStockFlo_Updata_Ye = OptContent.get("WZStock_Updata_Ye");

                       
                        pa1["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                        pa1["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO"]);
                        DataTable dt = DaoTool.FindDT(dao, "WZPZ_FindWZStockDetailInfo", pa1);

                        DataRow r = dt.Rows[0];

                        pa["OUTNUM"] = Convert.ToDecimal(objitm3.ToString()) - Convert.ToDecimal(data.Param["USENUM"]);
                        pa["FLOWNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZSTOCKDETAIL");
                        pa["STOCKID"] = Convert.ToDecimal(r["STOCKID"]);
                        pa["WZID"] = Convert.ToDecimal(r["WZID"]);
                        pa["WZNAME"] = r["WZNAME"].ToString();
                        pa["WARECODE"] = r["WARECODE"].ToString();
                        pa["RECIPECODE"] = r["RECIPECODE"].ToString();
                        pa["PRODUCTDATE"] = Convert.ToDateTime(r["PRODUCTDATE"]);
                        pa["INDATE"] = Convert.ToDateTime(r["INDATE"]);
                        //pa["STOCKNUM"] = Convert.ToDecimal(r["NUM"]);
                        pa["PRODUCTDATE"] = Convert.ToDateTime(r["PRODUCTDATE"]);

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


                        pa["INID"] = Convert.ToDecimal(r["INID"]);
                        pa["NUM"] = Convert.ToDecimal(r["NUM"]);
                        pa["BEFORENUM"] = Convert.ToDecimal(r["BEFORENUM"]);
                        pa["CHANGERATE"] = Convert.ToDecimal(r["CHANGERATE"]);
                        Opt savedetailInfo2 = OptContent.get("WZStockDetail_Add_Ye");

                        if (DaoTool.Save(dao, savedetailInfo2, pa) < 0)
                            throw new Exception("添加库存流水记录失败！");


           msg = "删除使用信息成功！";
                return "ok";

            }
            //修改库存主表细表(以前的功能）

            if ("WZStockDetail_Add_Ye".Equals(ac))
            {
               
                pa["INID"] = Convert.ToDecimal(data.Param["INID"]);
                pa["WARECODE"] = data.Param["WARECODE"].ToString();
                pa["WZID"] = Convert.ToDecimal(data.Param["WZID"]);
                pa["STOCKID"] = Convert.ToDecimal(data.Param["STOCKID"]);
                //pa["NUM"] = Convert.ToDecimal(data.Param["NUM"]);
                pa["BEFORENUM"] = Convert.ToDecimal(data.Param["BEFORENUM"]);
               
                pa["LSUNITCODE"] = Convert.ToDecimal(data.Param["LSUNITCODE"]);
                pa["CHANGERATE"] = Convert.ToDecimal(data.Param["CHANGERATE"]);
                pa["PRODUCTDATE"] = Convert.ToDateTime(data.Param["PRODUCTDATE"]);
                pa["RECIPECODE"] = data.Param["RECIPECODE"].ToString();
                pa["INDATE"] = Convert.ToDateTime(data.Param["INDATE"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["OUTNUM"] = Convert.ToDecimal(data.Param["OUTNUM"]);
          

                pa["USENUM"] = Convert.ToDecimal(data.Param["USENUM"]);


                if (data.Param["PRICE"] != null)
                {

                    if (data.Param["PRICE"].ToString() != "")
                    {
                        pa["PRICE"] = Convert.ToSingle(data.Param["PRICE"]);
                    }
                    else
                    {
                        pa["PRICE"] = null;
                    }
                }

                if (data.Param["LSPRICE"] != null)
                {

                    if (data.Param["LSPRICE"].ToString() != "")
                    {
                        pa["LSPRICE"] = Convert.ToSingle(data.Param["LSPRICE"]);
                    }
                    else
                    {
                        pa["LSPRICE"] = null;
                    }
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
                    else
                    {
                        pa["SUPPLYID"] = null;
                    }

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
                    else
                    {
                        pa["VALIDDATE"] = null;
                    }

                }

                if (data.Param["WSXKZH"] != null)
                {
                    pa["WSXKZH"] = data.Param["WSXKZH"].ToString();
                }
                else
                {
                    pa["WSXKZH"] = null;
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
                    else
                    {
                        pa["SUPPLYID2"] = null;
                    }

                }

                if (data.Param["SUPPLYNAME2"] != null)
                {
                    pa["SUPPLYNAME2"] = data.Param["SUPPLYNAME2"].ToString();
                }
                else
                {
                    pa["SUPPLYNAME2"] = null;

                }
               // Opt findWZUseNumInfo = OptContent.get("findWZUseNumInfo");//获得入库流水号
                 
               //ObjItem objitm=  DaoTool.ExecuteScalar(dao, findWZUseNumInfo, pa);

               //pa["FLOWNO"] = Convert.ToDecimal(objitm.ToString());

                pa["FLOWNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZSTOCKDETAIL");

               Opt findWZUseInNumInfo = OptContent.get("findWZUseInNumInfo");//获得库存数量

               ObjItem objitm = DaoTool.ExecuteScalar(dao, findWZUseInNumInfo, pa);

               pa["NUM"] = Convert.ToDecimal(objitm.ToString()); 

                Opt savedetailInfo = OptContent.get("WZStockDetail_Add_Ye");

                Opt saveInfo = OptContent.get("WZStock_Updata_Ye");


                if (DaoTool.Save(dao, savedetailInfo, pa) < 0)
                    throw new Exception("添加库存流水记录失败！");

                if(DaoTool.ExecuteNonQuery(dao,saveInfo,pa)<0)
                      throw new Exception("修改库存数量失败！");
                //修改库存主表细表
                msg = "新建成功！";
                return "ok";
            }

            return "ok";

        }

        #endregion
    }
}
