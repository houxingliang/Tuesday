using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WcTusService.Model;
using WcTusService.TuesdayModel;

namespace WcTusService.Data
{
    /// <summary>
    /// 任务执行数据访问类
    /// </summary>
    public class TaskExecuteData
    {
        //数据库模型
        ShareWeiEntities db = new ShareWeiEntities();
        /// <summary>
        /// 添加任务执行信息
        /// </summary>
        public int AddTaskExecute(tb_taskExecute tte)
        {
            if (tte != null)
            {
                db.tb_taskExecute.Add(tte);
                int returnNum = db.SaveChanges();
                return returnNum;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 更新任务执行信息
        /// </summary>
        public int EditTaskExecute(tb_taskExecute tte)
        {
            if (tte != null)
            {
                db.Entry(tte).State = EntityState.Modified;
                return db.SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据用户ID查询任务执行情况
        /// 将是否申请发放设置为 true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int EditTaskExecuteByUserId(int id)
        {
            var query = from p in db.tb_taskExecute
                        where p.fk_user_id == id && p.bit_isGrant==false
                         && p.bit_isApply==false
                        select p;
            foreach (var row in query)
            {
                row.bit_isApply = true;
            }
            return db.SaveChanges();
        }
        /// <summary>
        /// 根据用户ID发放奖励
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GrantRewardByUserID(int userid)
        {
            var query = from p in db.tb_taskExecute
                        where p.fk_user_id == userid && p.bit_isGrant == false
                         && p.bit_isApply == true
                        select p;
            foreach (var row in query)
            {
                row.bit_isGrant = true;
            }
            return db.SaveChanges();
        }
        /// <summary>
        /// 根据任务项ID发放奖励
        /// </summary>
        /// <param name="taskItemId">任务项ID</param>
        /// <returns></returns>
        public int GrantRewardByTaskId(int taskItemId)
        {
            var query = from p in db.tb_taskExecute
                        where p.fk_taskItem_id == taskItemId && p.bit_isGrant == false
                         && p.bit_isApply == true
                        select p;
            foreach (var row in query)
            {
                row.bit_isGrant = true;
            }
            return db.SaveChanges();
        }
        /// <summary>
        /// 根据主键ID查询任务执行信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public tb_taskExecute GetRewardTmpById(int id)
        {
            var rt = from r in db.tb_taskExecute
                     where r.pk_taskExecute_id == id
                     select r;
            if (rt.ToList().Count > 0)
                return rt.FirstOrDefault();
            else
                return null;
        }
        /// <summary>
        /// 查询任务执行信息集合
        /// </summary>
        /// <returns></returns>
        public List<tb_taskExecute> GetRewardTmpList()
        {
            var rt = from r in db.tb_taskExecute
                     select r;
            return rt.ToList();
        }
        /// <summary>
        /// 根据用户ID
        /// 查询任务执行信息集合
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public List<tb_taskExecute> GetTaskExecuteListByUserId(int id)
        {
            var rt = from r in db.tb_taskExecute
                     where r.fk_user_id==id
                     select r;
            if (rt.ToList().Count > 0)
            {
                return rt.ToList();
            }
            return null;
        }
        /// <summary>
        /// 根据任务项ID
        /// 查询任务执行信息集合
        /// </summary>
        /// <param name="itemid">任务项ID</param>
        /// <returns></returns>
        public List<tb_taskExecute> GetRewardTmpListByItemId(int itemId)
        {
            var rt = from r in db.tb_taskExecute
                     where r.fk_taskItem_id == itemId
                     select r;
            if (rt.ToList().Count > 0)
            {
                return rt.ToList();
            }
            return null;
        }
        /// <summary>
        /// 根据任务名称和时间
        /// 查询符合条件的任务列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<tb_task> GetTaskById(int id,DateTime actionDate,DateTime endDate)
        {
            if (id != 0)
            {
                var query = from p in db.tb_task
                            where p.pk_task_id == id &&
                            p.dtm_actionTime >= actionDate &&
                            p.dtm_endTime <= endDate && p.bit_isDelete == false
                            select p;
                if (query != null)
                    return query.ToList();
                else
                    return null;
            }
            else
            {
                var query = from p in db.tb_task
                            where p.dtm_actionTime >= actionDate &&
                            p.dtm_endTime <= endDate && p.bit_isDelete == false
                            select p;
                if (query != null)
                    return query.ToList();
                else
                    return null;
            }
        }
        /// <summary>
        /// 根据时间
        /// 查询任务执行信息
        /// </summary>
        /// <param name="actionDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<tb_taskExecute> GetTaskExecuteByTime(DateTime actionDate,DateTime endDate)
        {
            var query = from p in db.tb_taskExecute
                        where p.dtm_executeTime >= actionDate && p.dtm_executeTime <= endDate
                        select p;
            return query.ToList();
        }
        /// <summary>
        /// 根据任务名称和时间
        /// 查询符合条件的任务列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<tb_task> GetTaskByID(int id)
        {
            var query = from p in db.tb_task
                        where p.pk_task_id==id  && p.bit_isDelete == false
                        select p;
            if (query != null)
                return query.ToList();
            else
                return null;
        }
        /// <summary>
        /// 根据主键ID获取任务执行信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_taskExecute GetTaskExecuteById(int id)
        {
            var query = from p in db.tb_taskExecute
                        where p.pk_taskExecute_id == id
                        select p;
            if (query != null)
                return query.FirstOrDefault();
            else
                return null;
        }
    }
}