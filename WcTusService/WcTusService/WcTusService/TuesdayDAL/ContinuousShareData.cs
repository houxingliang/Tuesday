using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WcTusService.TuesdayModel;

namespace WcTusService.TuesdayDAL
{
    /// <summary>
    /// 任务的连续分享 数据访问类
    /// </summary>
    public class ContinuousShareData
    {
        //数据库模型
        ShareWeiEntities share = new ShareWeiEntities();
        /// <summary>
        /// 添加任务连续分享
        /// </summary>
        /// <param name="cs"> 连续分享实体类</param>
        /// <returns>受影响行数</returns>
        public int AddContinuousShare(tb_continuousShare cs)
        {
            if (cs != null)
            {
                share.tb_continuousShare.Add(cs);
                return share.SaveChanges();
            }
            return 0;
        }
        /// <summary>
        /// 更改连续执行任务的状态
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public int EditContinuousShare(tb_continuousShare cs)
        {
            if (cs != null)
            {
                share.Entry(cs).State = EntityState.Modified;
                return share.SaveChanges();
            }
            return 0;
        }
        /// <summary>
        /// 根据主键获取连续执行实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_continuousShare GetContinuousShareById(int id)
        {
            var query = from p in share.tb_continuousShare
                        where p.pk_continuousShare_Id == id
                        select p;
            return query.FirstOrDefault();
        }
        /// <summary>
        /// 根据任务ID和用户ID
        /// 查询连续执行任务实体集合
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public List<tb_continuousShare> GetContinuousShareByTaskId(int taskId, int userid)
        {
            var query = from p in share.tb_continuousShare
                        where p.fk_task_Id == taskId && p.fk_user_Id == userid
                        select p;
            return query.ToList();
        }
    }
}