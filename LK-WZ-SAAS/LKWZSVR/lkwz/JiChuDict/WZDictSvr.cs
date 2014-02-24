using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YiTian.db;
using YtService.util;
using YtService.config;

namespace LKWZSVR.lkwz.JiChuDict
{
    class WZDictSvr:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {

            msg = "物资信息";
            Dictionary<string, object> pa = new Dictionary<string, object>();
            string ac = data.Sql;
            ObjItem Obj,Obj1,Obj2,Obj3,Obj4,Obj5,Obj6,Obj7,Obj8;
            if ("SaveDictWZInfo".Equals(ac))
            {
                pa["WZNAME"] = data.Param["WZNAME"].ToString();
                pa["KINDCODE"] = data.Param["KINDCODE"].ToString();
                pa["COUNTCODE"] = data.Param["COUNTCODE"].ToString();
                pa["UNITCODE"] = data.Param["UNITCODE"].ToString();
                pa["LSUNITCODE"] = data.Param["LSUNITCODE"].ToString();

                pa["PRICE"] =Math.Round( Convert.ToDecimal(data.Param["PRICE"]),4);
                pa["CHANGERATE"] = Convert.ToDecimal(data.Param["CHANGERATE"]);
                pa["LOWNUM"] =Math.Round( Convert.ToDecimal(data.Param["LOWNUM"]),2);
                pa["HIGHNUM"] =Math.Round( Convert.ToDecimal(data.Param["HIGHNUM"]),2);
                pa["RATE"] =Math.Round( Convert.ToDecimal(data.Param["RATE"]),4);

                pa["IFNY"] = Convert.ToDecimal(data.Param["IFNY"]);
                pa["IFUSE"] = Convert.ToDecimal(data.Param["IFUSE"]);
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
               
                pa["RECDATE"] = DateTime.Now;
                
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                if (data.Param["USERNAME"] != null)
                {
                    pa["USERNAME"] = data.Param["USERNAME"].ToString();
                }
                else
                {
                    pa["USERNAME"] = null;
                }
                if (data.Param["FARECODE"] != null)
                {
                    pa["FARECODE"] = data.Param["FARECODE"].ToString();
                }
                else
                {
                    pa["FARECODE"] = null;
                }
              
                if (data.Param["PYCODE"] != null)
                {
                    pa["PYCODE"] = data.Param["PYCODE"].ToString();
                }
                else
                {
                    pa["PYCODE"] = null;
                }
                if (data.Param["WBCODE"] != null)
                {
                    pa["WBCODE"] = data.Param["WBCODE"].ToString();
                }
                else
                {
                    pa["WBCODE"] = null;
                }
                if (data.Param["SHORTNAME"] != null)
                {
                    pa["SHORTNAME"] = data.Param["SHORTNAME"].ToString();
                }
                else
                {
                    pa["SHORTNAME"] = null;
                }
                if (data.Param["SHORTCODE"] != null)
                {
                    pa["SHORTCODE"] = data.Param["SHORTCODE"].ToString();
                }
                else
                {
                    pa["SHORTCODE"] = null;
                }
                if (data.Param["ALIASNAME"] != null)
                {
                    pa["ALIASNAME"] = data.Param["ALIASNAME"].ToString();
                }
                else
                {
                    pa["ALIASNAME"] = null;
                }
                if (data.Param["ALIASCODE"] != null)
                {
                    pa["ALIASCODE"] = data.Param["ALIASCODE"].ToString();
                }
                else
                {
                    pa["ALIASCODE"] = null;
                }
                if (data.Param["GG"] != null)
                {
                    pa["GG"] = data.Param["GG"].ToString();
                }
                else
                {
                    pa["GG"] = null;
                }
                if (data.Param["VALIDE"] != null && data.Param["VALIDE"].ToString() !="")
                {
                    pa["VALIDE"] = Convert.ToDecimal(data.Param["VALIDE"]);
                }
                else
                {
                    pa["VALIDE"] = null;
                }
                if (data.Param["TXM"] != null)
                {
                    pa["TXM"] = data.Param["TXM"].ToString();
                }
                else
                {
                    pa["TXM"] = null;
                }
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }
                if (data.Param["WZID"] == null)
                {
                   
                    Opt findwzidInfo = OptContent.get("findwzidInfo");
                    ObjItem objj = DaoTool.ExecuteScalar(dao, findwzidInfo, pa);
                   
                    long a =DaoTool.Seq(dao, "LKWZ.SEQWZ");
                
                     for(long i=a;i<= objj.ToLong();i++)
                    {
                          a=DaoTool.Seq(dao, "LKWZ.SEQWZ");
                       
                    }
                     pa["WZID"] = a;
                  
                    Opt saveInfo = OptContent.get("SaveDictWzInfo");
                    Opt name = OptContent.get("SaveDictWzInfo_Name");

                    Obj = DaoTool.ExecuteScalar(dao, name, pa);
                    if (!Obj.IsNull)
                    {
                        msg = "名称重复！";
                        return "ok";
                    }
                    if (DaoTool.Save(dao, saveInfo, pa) < 0)
                        throw new Exception("添加物资信息失败！");
                    msg = "添加成功！";
                    return "ok";
                }
                else
                {
                    pa["WZID"] = Convert.ToDecimal(data.Param["WZID"]);
                    Opt updataInfo = OptContent.get("UpdataDictWzInfoInfo");
                    Opt updataname = OptContent.get("UpdataDictWzInfoInfo_Name");


                    Obj = DaoTool.ExecuteScalar(dao, updataname, pa);
                    if (!Obj.IsNull)
                    {
                        msg = "名称重复！";
                        return "ok";
                    }
                    if (DaoTool.ExecuteNonQuery(dao, updataInfo, pa) < 0)
                        throw new Exception("修改物资信息失败！");
                    msg = "修改成功！";
                    return "ok";
                }

             
            }
            if ("DelWZDictInfo".Equals(ac))
            {
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                //pa["IFUSE"] = Convert.ToDecimal(data.Param["IFUSE"]);
                pa["WZID"] = Convert.ToDecimal(data.Param["WZID"]);
             
                    Opt Del = OptContent.get("DelWZDictInfo");
                    Opt DelDetail = OptContent.get("DelWZDictDetailInfo");
                    //Opt IfUse = OptContent.get("DelWZDictInfo_Use");
                    //Obj = DaoTool.ExecuteScalar(dao, IfUse, pa);
                    ////DaoTool.
                    //if (Obj.ToString() == "1")
                    //{
                    //    msg = "已使用过,不能删除只能停用！";
                    //    return "ok";
                    //}

                    Opt values1 = OptContent.get("DelWZDictInfo_Use1");
                    Opt values2 = OptContent.get("DelWZDictInfo_Use2");
                    Opt values3 = OptContent.get("DelWZDictInfo_Use3");
                    Opt values4 = OptContent.get("DelWZDictInfo_Use4");
                    Opt values5 = OptContent.get("DelWZDictInfo_Use5");
                    Opt values6 = OptContent.get("DelWZDictInfo_Use6");
                    Opt values7 = OptContent.get("DelWZDictInfo_Use7");
                    Opt values8 = OptContent.get("DelWZDictInfo_Use8");
                    Obj1 = DaoTool.ExecuteScalar(dao, values1, pa);
                    Obj2 = DaoTool.ExecuteScalar(dao, values2, pa);
                    Obj3 = DaoTool.ExecuteScalar(dao, values3, pa);
                    Obj4 = DaoTool.ExecuteScalar(dao, values4, pa);
                    Obj5 = DaoTool.ExecuteScalar(dao, values5, pa);
                    Obj6 = DaoTool.ExecuteScalar(dao, values6, pa);
                    Obj7 = DaoTool.ExecuteScalar(dao, values7, pa);
                    Obj8 = DaoTool.ExecuteScalar(dao, values8, pa);
                    if (!Obj1.IsNull || !Obj2.IsNull || !Obj3.IsNull || !Obj4.IsNull || !Obj5.IsNull || !Obj6.IsNull || !Obj7.IsNull || !Obj8.IsNull)
                    {
                        msg = "不能删除已使用的物资，只能停用！";
                        return "ok";
                    }




                    if (DaoTool.ExecuteNonQuery(dao, DelDetail, pa) < 0)
                        throw new Exception("删除物资详细信息失败！");

                    if (DaoTool.ExecuteNonQuery(dao, Del, pa) < 0)
                        throw new Exception("删除物资信息失败！");

                   
                    msg = "删除成功！";
                    return "ok";
               
          
                }

             if ("DelWZDictDetailInfo".Equals(ac))
            {
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
               
                pa["WZID"] = Convert.ToDecimal(data.Param["WZID"]);
                
                   
                   
               
                    Opt Del = OptContent.get("DelWZDictDetailInfo");
                    if (DaoTool.ExecuteNonQuery(dao, Del, pa) < 0)
                        throw new Exception("删除详细物资信息失败！");
                    msg = "删除成功！";
                    return "ok";


            }
            if ("StopWZDictInfo".Equals(ac))
            {
                Opt stop = OptContent.get("StartOrStopWZDictInfo");

                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["WZID"] = data.Param["WZID"].ToString();
                pa["IFUSE"] = 0;
                if (DaoTool.ExecuteNonQuery(dao, stop, pa) < 0)
                    throw new Exception("停用厂商信息失败！");
                msg = "停用成功！";
                return "ok";

            }
            if ("StartWZDictInfo".Equals(ac))
            {
                Opt start = OptContent.get("StartOrStopWZDictInfo");

                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["WZID"] = data.Param["WZID"].ToString();
                pa["IFUSE"] = 1;
                if (DaoTool.ExecuteNonQuery(dao, start, pa) < 0)
                    throw new Exception("启用厂商信息失败！");
                msg = "启用成功！";
                return "ok";

            }
            return "ok";

        }
        #endregion
    }
}
