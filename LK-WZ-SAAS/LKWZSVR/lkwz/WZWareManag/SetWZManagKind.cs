using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.his.WZWareManag
{
     public class SetWZManagKind:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {


            if (data.Sql != null && data.Sql.Equals("Save"))
            {
                Opt op = OptContent.get("SaveSetWZDetail");
                if (DaoTool.Save(dao, op, data) > -1)
                {
                    msg = "设置管理的物资类别成功!";
                    return "ok";
                }
                else
                {
                    throw new Exception("设置管理的物资类别失败！" + dao.ErrMsg);
                    //return "ok";
                }
            }
            else
            {
                throw new Exception("设置管理的物资类别失败！" + dao.ErrMsg);
                //return "ok";
            }
        }

        #endregion
    }
}
