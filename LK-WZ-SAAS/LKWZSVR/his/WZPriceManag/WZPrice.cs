using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.his.WZPriceManag
{
    class WZPrice : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql != null && data.Sql.Equals("Del"))
            {
               // int ifuse = DaoTool.ExecuteScalar(dao, OptContent.get("WZPriceIsUse"), data).ToInt();
                //if (1 == ifuse)
                //{
                //    //throw new Exception("计价体系已经被系统使用，不能删除，只能停用！");
                //    msg = "计价体系已经被系统使用，不能删除，只能停用！";
                //    return "ok";
                //}
                    
                int rw = DaoTool.ExecuteNonQuery(dao, OptContent.get("DelWZPriceInfo"), data);

                if (rw < 0)
                {
                    //throw new Exception("计价体系已经被系统使用，不能删除，只能停用！");
                    msg = "计价体系已经被系统使用，不能删除，只能停用！";
                    return "ok";
                }
                else
                {
                    msg = "计价体系已删除！";
                    return "ok";
                }
               

            }
            if (data.Sql != null && data.Sql.Equals("Disable"))
            {
                Opt op = OptContent.get("SaveWZPriceInfo");
                // data.Param.ContainsKey(op.Key);
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = "停用计价体系信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("停用计价体系信息失败！" + dao.ErrMsg);
                    }
                }
            }
            if (data.Sql != null && data.Sql.Equals("Enable"))
            {
                Opt op = OptContent.get("SaveWZPriceInfo");
                //if (data.Param.ContainsKey(op.Key))
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = "启用计价体系信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("启用计价体系信息失败！" + dao.ErrMsg);
                    }
                }
            }
            if (data.Sql != null && data.Sql.Equals("Save"))
            {
                Opt op = OptContent.get("SaveWZPriceInfo");
              
                
                if (data.Param.ContainsKey(op.Key))
                {
                    int repeat = DaoTool.ExecuteScalar(dao, OptContent.get("WZModifyIsRepeat"), data).ToInt();
                    if (repeat > 0)
                    {
                        //throw new Exception("已经存在该计价体系信息，不能修改成该名称！" + dao.ErrMsg);
                        msg = "已经存在该计价体系信息，不能修改成该名称！";
                        return "ok";
                    }


                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = "保存计价体系信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存计价体系信息失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    int repeat=DaoTool.ExecuteScalar(dao,OptContent.get("WZAddIsRepeat"),data).ToInt();
                    if(repeat>0)
                    {
                        //throw new Exception("已经存该计价体系信息！" + dao.ErrMsg);
                        msg = "已经存该计价体系信息！";
                        return "ok";
                    }

                    data.Param["PRICEID"] = DaoTool.Seq(dao, "LKWZ.SEQWZPRICE");
                    if (DaoTool.Save(dao, op, data) > -1)
                    {
                        msg = "添加成功!";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("添加计价体系信息失败！" + dao.ErrMsg);
                    }
                }
                

            }
            else
            {
                throw new Exception("保存计价体系信息失败！" + dao.ErrMsg);
            }
            
        }

        #endregion
    }
}
