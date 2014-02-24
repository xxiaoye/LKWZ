using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.lkwz.StatQuery
{
    class WZStockAlarmSvr:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
           msg = "使用信息";
            Dictionary<string, object> pa = new Dictionary<string, object>();
            string ac = data.Sql;


            if ("SetWZStockAlarmStockInfo".Equals(ac))
            {
     
                pa["STOCKID"] = Convert.ToDecimal(data.Param["STOCKID"]);
                pa["NUMSX"] = Convert.ToDecimal(data.Param["NUMSX"]);
                pa["NUMXX"] = Convert.ToDecimal(data.Param["NUMXX"]); 
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                Opt setInfo = OptContent.get("SetWZStockAlarmStockInfo");


                if (DaoTool.ExecuteNonQuery(dao, setInfo, pa) < 0)
                    throw new Exception("设置库存上下限失败！");
                msg = "设置库存上下限成功！";
                return "ok";
            }
            return "ok";
        }

        #endregion
    }
}
