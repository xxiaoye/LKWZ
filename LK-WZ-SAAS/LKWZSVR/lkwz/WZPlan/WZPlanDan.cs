using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.lkwz.WZPlan
{
    class WZPlanDan : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql != null && data.Sql.Equals("PlanDanDelete"))
            {
                data.Param["STATUS"] = 0;
                if (DaoTool.Update(dao, OptContent.get("SaveWZPlanMainInfo"), data) > -1)
                {
                    msg = "删除采购计划信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("删除采购计划信息失败！" + dao.ErrMsg);
                }
            }
            if (data.Sql != null && data.Sql.Equals("PlanDanUpdate"))
            {
                Opt op = OptContent.get("SaveWZPlanMainInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        // saveRunDetail(dao, data);
                        //if (data.Param["STATUS"].ToString() == "2")
                        //{
                            
                        //}

                        msg = "审核采购计划信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("审核采购计划信息失败！" + dao.ErrMsg);
                    }
                }
            }
            if (data.Sql != null && data.Sql.Equals("PlanDanWZdelete"))
            {
                //删除采购计划细表记录
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DeleteWZPlanDetailInfo"), data) < 0)
                {
                    throw new Exception("删除采购物资信息失败！");

                }
                //更新采购计划主表信息
                if (DaoTool.Update(dao, OptContent.get("SaveWZPlanMainInfo"), data) < 0)
                {
                    throw new Exception("保存采购计划单失败！" + dao.ErrMsg);
                }
                msg = "删除采购物资信息成功！";
                return "ok";
            }
            if (data.Sql != null && data.Sql.Equals("UpdateWZPlanDan"))
            {
                if (DaoTool.Update(dao, OptContent.get("SaveWZPlanMainInfo"), data) > -1)
                {
                    //savePlanDetail(dao, data);
                    msg = "保存采购计划信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("保存采购计划信息失败！" + dao.ErrMsg);
                }
            }
            if (data.Sql != null && data.Sql.Equals("PlanDanSave"))
            {
                Opt op = OptContent.get("SaveWZPlanMainInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        savePlanDetail(dao, data);
                        msg = "保存采购计划信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存采购计划信息失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    data.Param["PLANID"] = DaoTool.Seq(dao, "LKWZ.SEQWZPLAN");
                    if (DaoTool.Save(dao, op, data) > -1)
                    {
                        savePlanDetail(dao, data);
                        msg = "添加采购计划成功!";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("添加采购计划失败！" + dao.ErrMsg);
                    }
                }
            }
            msg = "保存成功！";
            return "ok";
        }


        bool savePlanDetail(YiTian.db.Dao dao, YtService.data.OptData data)
        {
            List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(data.Param["DanJuMx"].ToString());
            Opt opt2 = OptContent.get("SaveWZPlanDetailInfo");
            foreach (Dictionary<string, object> d in mxli)
            {
                //d["DETAILNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZInDetail");
                d["PLANID"] = data.Param["PLANID"];
                d["WZID"] = d["物资ID"];
                d["UNITCODE"] = d["单位编码"];
                d["NUM"] = d["采购数量"];
                d["PRICE"] = d["采购单价"];
                d["MONEY"] = d["采购金额"];
                d["LSPRICE"] = d["零售单价"];
                d["LSMONEY"] = d["零售金额"];
                d["MEMO"] = d["备注"];
                d["TXM"] = d["条形码"];
                d["CHOSCODE"] = data.Param["CHOSCODE"];
                if (!d["行号"].ToString().Equals(""))
                {
                    d["ROWNO"] = d["行号"].ToString();   
                    if (DaoTool.Update(dao, opt2, d) < 0)
                    {
                        throw new Exception("保存单据明细失败！");
                    }

                }
                else
                {
                    object rw = dao.Es(OptContent.get("GetROWNO").Sql, new object[] { data.Param["PLANID"] });
                    //data.Param["ROWNO"] = Convert.ToDecimal(rw) + 1;
                    d["ROWNO"] = Convert.ToDecimal(rw) + 1;
                    if (DaoTool.Save(dao, opt2, d) < 0)
                    {
                        throw new Exception("添加单据明细失败！");
                    }
                }

            }
            return true;
        }
        #endregion
    }
}
