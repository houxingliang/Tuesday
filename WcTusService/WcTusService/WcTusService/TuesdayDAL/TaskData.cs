using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WcTusService.TuesdayModel;

namespace WcTusService.Data
{
    /// <summary>
    /// 任务表
    /// </summary>
    public class TaskData
    {
        ShareWeiEntities share = new ShareWeiEntities();
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="task"></param>
        public int Addtask(tb_task task)
        {
            share.tb_task.Add(task);
            return share.SaveChanges();
        }
        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="task"></param>
        public int EditTask(tb_task task)
        {
            share.Entry(task).State = EntityState.Modified;
            return share.SaveChanges();
           
        }
        /// <summary>
        /// 根据主键得到任务
        /// </summary>
        /// <param name="task"></param>
        public tb_task GettaskByid(int taskid)
        {
            var ta = from p in share.tb_task
                     where p.pk_task_id == taskid
                     select p;
            if (ta.Count() != 0)
            {
                return ta.FirstOrDefault();

            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到全部任务
        /// </summary>
        /// <param name="task"></param>
        public List<tb_task> GettaskList()
        {
            var ta = from p in share.tb_task
                    where p.bit_isDelete==false
                     select p;
            if (ta.Count() != 0)
            {
                return ta.ToList();

            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据任务得到奖品明细
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>奖品明细</returns>
        public List<tb_reward> GetRewardByTaskId(int id)
        {
            var query = from p in share.tb_task
                        where p.pk_task_id == id
                        select p;
            if (query != null)
            {
                tb_task task = query.FirstOrDefault();
                var queryItem = from p in share.tb_taskItem
                                where p.fk_task_id == task.pk_task_id
                                select p;
                List<tb_reward> rewardList = new List<tb_reward>();
                if (queryItem != null)
                {
                    List<tb_taskItem> itemList = queryItem.ToList();
                    foreach (tb_taskItem item in itemList)
                    {
                        var rewardTmp = from p in share.tb_rewardTemplate
                                        where p.pk_rewardTemplate_id == item.fk_rewardTemplate_id
                                        select p;
                        List<tb_rewardTemplate> tmpList = rewardTmp.ToList();
                        if (tmpList != null)
                        {
                            foreach (var tmp in tmpList)
                            {
                                var query_imp = from p in share.tb_reward_Template_imp
                                                where p.fk_rewardTemplate_id == tmp.pk_rewardTemplate_id
                                                select p;
                                List<tb_reward_Template_imp> impList = query_imp.ToList();
                                if (impList != null)
                                {
                                    foreach (tb_reward_Template_imp i in impList)
                                    {
                                        var re = from p in share.tb_reward
                                                 where p.pk_reward_id == i.fk_reward_id
                                                 select p;
                                        tb_reward reward = re.FirstOrDefault();
                                        rewardList.Add(reward);
                                    }
                                }

                            }
                        }
                    }
                }
                return rewardList;
            }
            else
            {
                return null;
            }
        }

    }
}