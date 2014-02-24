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
            if (data.Sql != null && data.Sql.Equals("DeleteWZKind"))
            {
               // Opt op = OptContent.get("SaveSetWZDetail");
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DelWZKind"), data) < 0)
                {
                    throw new Exception("删除物资类别信息失败！" + dao.ErrMsg);
                }
                msg = "物资类别已删除！";
                return "ok";
               
            }

            if (data.Sql != null && data.Sql.Equals("AllSave"))
            {
                 List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(data.Param["DanJuMx"].ToString());
                 Opt opt2 = OptContent.get("SaveSetWZDetail");
                 if (mxli != null)
                 {
                     if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DelAllWZKind"), data) < 0)
                     {
                         throw new Exception("保存物资类别信息处理失败！" + dao.ErrMsg);
                     }
                 }
                foreach (Dictionary<string, object> d in mxli)
                {
                    
                    d["WARECODE"] = data.Param["WARECODE"];
                    d["CHOSCODE"] = data.Param["CHOSCODE"];
                    d["KINDCODE"] = d["类别编码"];
                    if (DaoTool.Save(dao, opt2, d) < 0)
                    {
                        throw new Exception("保存管理的物资类别失败！");
                    }
                   
                }
                msg = "保存成功！";
                return "ok";
                //Opt op = OptContent.get("SaveSetWZDetail");
                //if (DaoTool.Save(dao, op, data) > -1)
                //{
                //    msg = "设置管理的物资类别成功!";
                //    return "ok";
                //}
                //else
                //{
                //    throw new Exception("设置管理的物资类别失败！" + dao.ErrMsg);
                //    //return "ok";
                //}
            }
            if (data.Sql != null && data.Sql.Equals("SelectSave"))
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
