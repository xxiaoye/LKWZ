using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YiTian.db;
using YtService.config;
using YtService.util;

namespace LKWZSVR.lkwz.JiChuDict
{
    class WZDictDetailSvr:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            msg = "物资信息";
            Dictionary<string, object> pa = new Dictionary<string, object>();
            string ac = data.Sql;
            int a;
            if ("SaveDictDetailWZInfo".Equals(ac))
            {
                a = Convert.ToInt32(data.Param["ISADD"]);
                pa["WZID"] = Convert.ToDecimal(data.Param["WZID"]);
                pa["SUPPLYID"] = Convert.ToDecimal(data.Param["SUPPLYID"]);
                pa["SUPPLYNAME"] = data.Param["SUPPLYNAME"].ToString();
                pa["IFFACTORY"] = Convert.ToDecimal(data.Param["IFFACTORY"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["IFSUPPLY"] = Convert.ToDecimal(data.Param["IFSUPPLY"]);
                if (a > 0)
                {
                    Opt save = OptContent.get("SaveWZDictDetailInfo");


                    if (DaoTool.Save(dao, save, pa) < 0)
                        throw new Exception("新建物资信息失败！");
                    msg = "保存成功！";
                    return "ok";
                }
                else
                {
                    Opt updata = OptContent.get("UpdataWZDictDetailInfo");
                    if (DaoTool.ExecuteNonQuery(dao, updata, pa) < 0)
                        throw new Exception("修改物资信息失败！");
                    msg = "修改成功！";
                    return "ok";
                }

            }
            return "ok";
        }

        #endregion
    }
}
