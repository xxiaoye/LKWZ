using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.his.WZUnitManag
{
    class WZUnit : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql != null && data.Sql.Equals("Del"))
            {
                int ifuse = DaoTool.ExecuteScalar(dao, OptContent.get("WZUnitIsUse"), data).ToInt();
                if (1 == ifuse)
                    throw new Exception("物资单位已经被系统使用，不能删除！");
                int rw = DaoTool.ExecuteNonQuery(dao, OptContent.get("DelWZUnitInfo"), data);

                if (rw < 0)
                {
                    throw new Exception("删除物资单位信息失败！");
                }
                msg = "物资单位已删除！";
                return "ok";

            }
            if (data.Sql != null && data.Sql.Equals("Save"))
            {
                Opt op = OptContent.get("SaveWZUnitInfo");
                if (data.Param.ContainsValue(op.Key))
                {
                    int repeat = DaoTool.ExecuteScalar(dao, OptContent.get("ModifyWZPriceIsRepeat"), data).ToInt();
                    if (repeat > 0)
                    {
                        throw new Exception("已经存在该物资价格体系信息,不能修改成该名称！" + dao.ErrMsg);
                    }
                   
                   
                    int tr = DaoTool.ExecuteScalar(dao, OptContent.get("DefaultWZUnit"), data).ToInt();
                    
                    if ((data.Param["DEFVALUE"].ToString() == "1") && tr > 0)
                    {
                        data.Params = new object[] { data.Param["DICGRPID"],data.Param["DICID"]};
                        dao.ExecuteNonQuery(OptContent.get("SetDefaultWZUnit").Sql,data.Params);
                        //DaoTool.Update(dao, OptContent.get("SetDefaultWZUnit"), data);
                       // DaoTool.Save(dao, OptContent.get("SetDefaultWZUnit"), data);
                        //DaoTool.Update(dao, OptContent.get("SetDefaultWZUnit"), data);
                    }
                    
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = "保存物资单位信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存物资单位信息失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    int repeat = DaoTool.ExecuteScalar(dao, OptContent.get("AddWZPriceIsRepeat"), data).ToInt();
                    if (repeat > 0)
                    {
                        throw new Exception("已经存在该物资价格体系信息！" + dao.ErrMsg);
                    }
                    int dicid_int = DaoTool.ExecuteScalar(dao, OptContent.get("SaveWzUnitInfo_seq"), data).ToInt() + 1;
                  
                    if (dicid_int >= 0 && dicid_int < 10)
                    {
                        data.Param["DICID"] = "0" + dicid_int.ToString();
                    }
                    else
                    {
                        data.Param["DICID"] = dicid_int.ToString();
                    }
                    //int tr = DaoTool.ExecuteScalar(dao, OptContent.get("DefaultWZUnit"), data).ToInt();

                    if ((data.Param["DEFVALUE"].ToString() == "1") && DaoTool.ExecuteScalar(dao, OptContent.get("DefaultWZUnit"), data).ToInt() > 0)
                    {
                        data.Params = new object[] { data.Param["DICGRPID"], data.Param["DICID"] };
                        dao.ExecuteNonQuery(OptContent.get("SetDefaultWZUnit").Sql, data.Params);
                        //DaoTool.Update(dao, OptContent.get("SetDefaultWZUnit"), data);
                        // DaoTool.Save(dao, OptContent.get("SetDefaultWZUnit"), data);
                        //DaoTool.Update(dao, OptContent.get("SetDefaultWZUnit"), data);
                    }

                    if (DaoTool.Save(dao, op, data) > -1)
                    {
                        msg = "添加成功!";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("添加物资单位信息失败！" + dao.ErrMsg);
                    }
                }
                

            }
            else
            {
                throw new Exception("保存物资单位信息失败！" + dao.ErrMsg);
            }
            }
        }

      #endregion
}

