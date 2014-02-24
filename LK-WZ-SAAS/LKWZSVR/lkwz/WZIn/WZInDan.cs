using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;
using System.Data;

namespace LKWZSVR.lkwz.WZIn
{
    class WZInDan : IEx
    {
        #region IEx 成员
        bool saveRunDetail(YiTian.db.Dao dao, YtService.data.OptData data)
        {
            List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(data.Param["DanJuMx"].ToString());
            Opt opt2 = OptContent.get("SaveWZInDetailInfo");
            foreach (Dictionary<string, object> d in mxli)
            {
                //d["DETAILNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZInDetail");
                d["INID"] = data.Param["INID"];
                d["WZID"] = d["物资ID"];
                d["UNITCODE"] = d["单位编码"];
                d["NUM"] = d["数量"];
                d["PRICE"] = d["采购单价"];
                d["MONEY"] = d["采购金额"];
                d["LSPRICE"] = d["零售单价"];

                d["LSMONEY"] = d["零售金额"];
                d["PH"] = d["生产批号"];
                d["PZWH"] = d["批准文号"];
               
                d["SUPPLYNAME"] = d["生产厂家"];
                if (d["生产厂家ID"].ToString().Equals(""))
                {
                    //生产厂家为手动输入
                    long supply_id = DaoTool.Seq(dao, "LKWZ.SEQWZSUPPLY");
                    d["SUPPLYID"] = supply_id;
                }
                else
                {
                    d["SUPPLYID"] = d["生产厂家ID"];
                }
                //if (!d["生产厂家ID"].ToString().Equals(""))
                //{
                //    //d["SUPPLYNAME"] = d["生产厂家_Text"];
                //    d["SUPPLYNAME"] = d["生产厂家"];
                //}
               
                d["PRODUCTDATE"] = Convert.ToDateTime(d["生产日期"]);

                d["VALIDDATE"] = Convert.ToDateTime(d["有效期"]);
                d["WSXKZH"] = d["卫生许可证号"];
                d["MEMO"] = d["备注"];
                d["TXM"] = d["条形码"];
                d["CHOSCODE"] = data.Param["CHOSCODE"];
                if (!d["流水号"].ToString().Equals(""))
                {
                    d["DETAILNO"] = d["流水号"].ToString();
                    if (DaoTool.Update(dao, opt2, d) < 0)
                    {
                        throw new Exception("保存单据明细失败！");
                    }

                }
                else
                {
                    d["DETAILNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZInDetail");
                    if (DaoTool.Save(dao, opt2, d) < 0)
                    {
                        throw new Exception("保存单据明细失败！");
                    }
                }

            }
            return true;
        }
        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql != null && data.Sql.Equals("UpdateWZStock"))
            {
                if (DaoTool.Update(dao, OptContent.get("SaveWZStockInfo"), data) > -1)
                {
                    msg = "保存库存信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("保存库存信息失败！" + dao.ErrMsg);
                }
            }
            if (data.Sql != null && data.Sql.Equals("SaveWZStock"))
            {
                data.Param["STOCKID"] = DaoTool.Seq(dao, "LKWZ.SEQWZStock"); ;
                if (DaoTool.Save(dao, OptContent.get("SaveWZStockInfo"), data) < 0)
                {
                    // msg = flowno+'+'+DETAILNO;
                    throw new Exception("添加物资库存总表信息失败！" + dao.ErrMsg);


                }
                
            }
            if (data.Sql != null && data.Sql.Equals("RuKuDanWZdelete"))
            {

                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DeleteWZInDetailInfo"), data) < 0)
                {
                    throw new Exception("删除入库物资信息失败！");

                }
                if (DaoTool.Update(dao, OptContent.get("SaveWZInMainInfo"), data) < 0)
                {
                    throw new Exception("保存入库信息失败！" + dao.ErrMsg);
                }
                
                msg = "删除入库物资信息成功！";
                return "ok";
            }
            if (data.Sql != null && data.Sql.Equals("RuKuDanDelete"))
            {
                data.Param["STATUS"] = 0;
                if (DaoTool.Update(dao, OptContent.get("SaveWZInMainInfo"), data) > -1)
                {
                    msg = "删除入库信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("删除入库信息失败！" + dao.ErrMsg);
                }
            }
            if (data.Sql != null && data.Sql.Equals("RuKuDanUpdate"))
            {
                Opt op = OptContent.get("SaveWZInMainInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                       // saveRunDetail(dao, data);
                        if (data.Param["STATUS"].ToString() == "2")
                        {
                            msg = "审核入库信息成功！";
                        }
                        else if (data.Param["STATUS"].ToString() == "1")
                        {
                            msg = "取消审核入库信息成功！";
                        }
                        else 
                        {
                            msg = "入库成功！";
                        }
                        
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存入库信息失败！" + dao.ErrMsg);
                    }
                }
            }
             if (data.Sql != null && data.Sql.Equals("SaveWZStockDetail"))
            {
                Opt op = OptContent.get("SaveWZStockDetailInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = "保存物资库存流水信息成功！";

                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存物资库存流水信息失败！" + dao.ErrMsg);
                    }
                }
                else 
                {
                    data.Param["FLOWNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZStockDetail");
                   string flowno = data.Param["FLOWNO"].ToString();
                   string DETAILNO = data.Param["DETAILNO"].ToString();
                    if (DaoTool.Save(dao, op, data) <0 )
                    {
                       // msg = flowno+'+'+DETAILNO;
                       throw new Exception("保存物资库存流水信息失败！" + dao.ErrMsg);
                        
                       
                    }
                    //更新入库细表中的对应库存流水号

                    //YtService.data.OptData d = new YtService.data.OptData();
                    //d.Param["STOCKFLOWNO"] = data.Param["FLOWNO"];
                    //d.Param["DETAILNO"] = data.Param["DETAILNO"];
                    //if (DaoTool.Update(dao, OptContent.get("SaveWZInDetailInfo"), d) < 0)
                    //{
                    //    throw new Exception("更新入库细表信息失败！" + dao.ErrMsg);
                    //}
                    return msg = flowno + '+' + DETAILNO;

                }

            }
             if (data.Sql != null && data.Sql.Equals("UpdateWZInDetailInfo"))
             {
                 if (DaoTool.Update(dao, OptContent.get("SaveWZInDetailInfo"), data) < 0)
                 {
                     throw new Exception("更新入库细表信息失败！" + dao.ErrMsg);
                 }
                 msg = "保存成功！";
                 return "ok";
             }
            if (data.Sql != null && data.Sql.Equals("RuKuDanSave"))
            {
                Opt op = OptContent.get("SaveWZInMainInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    //if (data.Param["SUPPLYID"].Equals(""))
                    //{
                    //    //手动输入供货商
                    //    data.Param["SUPPLYID"] = DaoTool.Seq(dao, "LKWZ.SEQWZSUPPLY");
                    //}
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        saveRunDetail(dao,data);
                        
                        msg = "保存入库信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存入库信息失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    DataTable dt = dao.Fd(OptContent.get("SearchDicWZInOut").Sql, new object[] { data.Param["IOID"].ToString() });
                    data.Param["OPFLAG"] = dt.Rows[0]["OPFLAG"].ToString();
                    data.Param["INID"] = DaoTool.Seq(dao, "LKWZ.SEQWZIn");
                    //if (data.Param["SUPPLYID"].Equals(""))
                    //{
                    //    //手动输入供货商
                    //    data.Param["SUPPLYID"] = DaoTool.Seq(dao, "LKWZ.SEQWZSUPPLY");
                    //}
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

                    data.Param["RECIPECODE"] = recipe;
                    data.Param["INDATE"] = DateTime.Now;
                    data.Param["STATUS"] = 1;

                    if (DaoTool.Save(dao, OptContent.get("SaveWZInMainInfo"), data) > -1)
                    {
                      
                        saveRunDetail(dao,data);
                       
                    }
                    else
                    {
                        throw new Exception("保存入库单据失败！");
                    }
                }
            }
            msg = "保存成功！";
            return "ok";
        }

        #endregion
    }
}
