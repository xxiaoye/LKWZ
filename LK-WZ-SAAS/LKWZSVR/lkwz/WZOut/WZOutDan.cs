using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;
using System.Data;

namespace LKWZSVR.lkwz.WZOut
{
    class WZOutDan : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql != null && data.Sql.Equals("ChuKuDanWZdelete"))
            {

                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DeleteWZOutDetailInfo"), data) < 0)
                {
                    throw new Exception("删除出库物资信息失败！");

                }
                if (DaoTool.Update(dao, OptContent.get("SaveWZOutMainInfo"), data) < 0)
                {
                    throw new Exception("保存出库信息失败！" + dao.ErrMsg);
                }

                msg = "删除出库物资信息成功！";
                return "ok";
            }
            if (data.Sql != null && data.Sql.Equals("ChuKuDanUpdate"))
            {
                Opt op = OptContent.get("SaveWZOutMainInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        // saveRunDetail(dao, data);
                        if (data.Param["STATUS"].ToString() == "2")
                        {
                            msg = "审核出库信息成功！";
                        }
                        else if (data.Param["STATUS"].ToString() == "1")
                        {
                            msg = "取消审核出库信息成功！";
                        }
                        else
                        {
                            msg = "出库成功！";
                        }

                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存出库信息失败！" + dao.ErrMsg);
                    }
                }
            }
            if (data.Sql != null && data.Sql.Equals("ChuKuDanDelete"))
            {
                data.Param["STATUS"] = 0;
                if (DaoTool.Update(dao, OptContent.get("SaveWZOutMainInfo"), data) > -1)
                {
                    msg = "删除出库信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("删除出库信息失败！" + dao.ErrMsg);
                }
            }
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
            if (data.Sql != null && data.Sql.Equals("UpdateWZStockDetail"))
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
            }
            if (data.Sql != null && data.Sql.Equals("ChuKuDanSave"))
            {
                Opt op = OptContent.get("SaveWZOutMainInfo");
                // data.Param.ContainsValue(op.Key);
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        //msg = "保存出库信息成功！";
                        //return "ok";
                        saveOutDetail(dao, data);
                    }
                    else
                    {
                        throw new Exception("保存出库信息失败！" + dao.ErrMsg);
                    }
                }
                else
                {

                    DataTable dt = dao.Fd(OptContent.get("SearchDicWZInOut").Sql, new object[] { data.Param["IOID"].ToString() });
                    data.Param["OPFLAG"] = dt.Rows[0]["OPFLAG"].ToString();
                    data.Param["OUTID"] = DaoTool.Seq(dao, "LKWZ.SEQWZOut");
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


                    decimal recipeno = Convert.ToDecimal(dao.ExecuteScalar(OptContent.get("GetOutRecipeNo").Sql, new object[] { recipe + '%' })) + 1;
                    if (recipeno > 0 && recipeno < 10)
                    {
                        recipe = recipe + "0" + recipeno.ToString();
                    }
                    else
                    {
                        recipe = recipe + recipeno.ToString();
                    }

                    data.Param["RECIPECODE"] = recipe;
                    data.Param["OUTDATE"] = DateTime.Now;
                    data.Param["STATUS"] = 1;

                    if (DaoTool.Save(dao, op, data) > -1)
                    {
                        saveOutDetail(dao, data);
                        //msg = "添加出库单成功!";
                        //return "ok";
                       
                    }
                    else
                    {
                        throw new Exception("添加出库信息失败！" + dao.ErrMsg);
                    }
                }
            }
            else
            {
                throw new Exception("保存出库信息失败！" + dao.ErrMsg);
            }
            msg = "保存成功！";
            return "ok";
        }

        //保存出库细表
        bool saveOutDetail(YiTian.db.Dao dao, YtService.data.OptData data)
        {
            List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(data.Param["DanJuMx"].ToString());
            Opt opt2 = OptContent.get("SaveWZOutDetailInfo");
            foreach (Dictionary<string, object> d in mxli)
            {
                //d["DETAILNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZInDetail");
                d["OUTID"] = data.Param["OUTID"];
                d["WZID"] = d["物资ID"];
                d["UNITCODE"] = d["单位"];
                d["NUM"] = d["数量"];
                d["PRICE"] = d["出库单价"];
                d["MONEY"] = d["出库金额"];
                d["INPRICE"] = d["入库单价"];
                d["STOCKFLOWNO"] = d["库存流水号"];
                d["INMONEY"] = d["入库金额"];
                d["PH"] = d["生产批号"];
                d["PZWH"] = d["批准文号"];
                d["SUPPLYID"] = d["生产厂家ID"];
                //if (!d["生产厂家ID"].ToString().Equals(""))
                //{
                //    d["SUPPLYNAME"] = d["生产厂家_Text"];
                //}
                d["SUPPLYNAME"] = d["生产厂家名称"];
                d["PRODUCTDATE"] = Convert.ToDateTime(d["生产日期"]);

                d["VALIDDATE"] = Convert.ToDateTime(d["有效期"]);
                d["WSXKZH"] = d["卫生许可证号"];
                d["MEMO"] = d["备注"];
                d["TXM"] = d["条形码"];
                d["CHOSCODE"] = d["医疗机构编码"];
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
                    d["DETAILNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZOutDetail");
                    if (DaoTool.Save(dao, opt2, d) < 0)
                    {
                        throw new Exception("保存单据明细失败！");
                    }
                }

            }
            return true;
        }
        #endregion
    }
}
