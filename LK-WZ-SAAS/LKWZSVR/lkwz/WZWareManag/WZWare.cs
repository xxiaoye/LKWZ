using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.his.WZWareManag
{
   public class WZWare:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql != null && data.Sql.Equals("Del"))
            {
                int ifuse = DaoTool.ExecuteScalar(dao, OptContent.get("WZIsUse"), data).ToInt();
                if (1==ifuse)
                    throw new Exception("物资已经被系统使用，不能删除，只能停用！");
                //int rw = DaoTool.ExecuteNonQuery(dao, OptContent.get("DelWZInfo"), data);

                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DelWZInfo"), data) < 0)
                {
                    throw new Exception("删除物资信息失败！");
                }
                msg = "物资已删除！";
                return "ok";

            }
            if (data.Sql != null && data.Sql.Equals("Disable"))
            {
                Opt op = OptContent.get("SaveWZInfo");
               // data.Param.ContainsKey(op.Key);
                if (data.Param.ContainsValue(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = "停用物资信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("停用物资信息失败！" + dao.ErrMsg);
                    }
                }
            }
            if (data.Sql != null && data.Sql.Equals("Enable"))
            {
                Opt op = OptContent.get("SaveWZInfo");
                //if (data.Param.ContainsKey(op.Key))
                if (data.Param.ContainsValue(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = "启用物资信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("启用物资信息失败！" + dao.ErrMsg);
                    }
                }
            }
            if (data.Sql != null && data.Sql.Equals("Save"))
            {
                //string str = "";
                Opt op = OptContent.get("SaveWZInfo");
                //data.Param.
                /*
                if (DaoTool.ExecuteScalar(dao, OptContent.get("WZIsManagall"), data).ToInt() == 0)
                {
                    str = "请稍后设置物资细表！";
                }*/
                //if (data.Param.ContainsKey(op.Key) )
                if (data.Param.ContainsValue(op.Key))
                {
                    string kd = "0";
                    int repeat = DaoTool.ExecuteScalar(dao, OptContent.get("ModifyWzInfoIsRepeat"), data).ToInt();
                    if (repeat > 0)
                    {
                        throw new Exception("已经存在该物资信息,不能修改成该名称！" + dao.ErrMsg);
                    }
                   
                  //  data.Params = new object[]{data.Param["warecode"],data.Param["choscode"]};
                    //data.Param.Add("kindcode", DaoTool.ExecuteScalar(dao, OptContent.get("ModifyWzInfoIsRepeat"), data));
                    int tr = DaoTool.ExecuteScalar(dao,OptContent.get("IsHavekindcode"), data).ToInt();
                    if (tr>0)
                    {
                        kd = "1";
                    }
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = kd+','+"保存物资信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存物资信息失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    string wd = null;
                    int repeat = DaoTool.ExecuteScalar(dao, OptContent.get("AddWzInfoIsRepeat"), data).ToInt();
                   
                   // int count_kindcode = DaoTool.ExecuteScalar(dao, OptContent.get("IsHavekindcode"), data).ToInt();
                    if (repeat > 0)
                    {
                        throw new Exception("已经存在该物资信息！" + dao.ErrMsg);
                    }
                    int warecode_int = DaoTool.ExecuteScalar(dao, OptContent.get("SaveWzInfo_seq"), data).ToInt() + 1;
                    if (warecode_int == 100)
                    {
                        throw new Exception("物资库存已满，不能继续添加！" + dao.ErrMsg);
    
                    }
                    if(warecode_int>=0&&warecode_int<10)
                    {
                        data.Param["warecode"] = "0"+warecode_int.ToString();
                        wd = "0" + warecode_int.ToString();
                    }
                    else
                    {
                        data.Param["warecode"] = warecode_int.ToString();
                        wd =  warecode_int.ToString();
                    }
                    //data.Param["warecode"] = DaoTool.ExecuteScalar(dao, OptContent.get("SaveWzInfo_seq"), data).ToInt() + 1;
                    if (DaoTool.Save(dao, op, data) > -1)
                    {
                       
                        msg = wd+','+"添加成功!";
                        
                        //msg = data.Param["warecode"];
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("添加物资信息失败！" + dao.ErrMsg);
                    }
                }
            }
            else
            {
                throw new Exception("保存物资信息失败！" + dao.ErrMsg);
            }
            
        }

        #endregion
    }
}
