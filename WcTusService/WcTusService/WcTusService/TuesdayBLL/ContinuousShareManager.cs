using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcTusService.TuesdayDAL;
using WcTusService.TuesdayModel;

namespace WcTusService.TuesdayBLL
{
    /// <summary>
    /// 任务的连续分享 数据访问类
    /// </summary>
    public class ContinuousShareManager
    {
        ContinuousShareData csData = null;
        /// <summary>
        /// 根据任务ID和用户ID
        /// 新增任务连续执行情况
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public int AddContinuousShareByUserIdAndTaskId(int userid,int taskId)
        {
            int returnNum = 0;
            TaskManager tm = new TaskManager();
            List<TaskItemReward> taskItemRewardList= tm.GetTaskItemByTaskId(taskId);
            List<tb_taskItem> taskItemList = new List<tb_taskItem>();
            if(taskItemRewardList!=null)
            {
                foreach(TaskItemReward r in taskItemRewardList)
                {
                    taskItemList.Add(r.TaskItem);
                }
            }
            if (taskItemList != null)
            {
                csData = new ContinuousShareData();
                foreach (tb_taskItem taskItem in taskItemList)
                {
                    tb_continuousShare cs = new tb_continuousShare();
                    cs.fk_user_Id = userid;
                    cs.fk_task_Id = taskId;
                    cs.fk_taskItem_Id = taskItem.pk_taskItem_id;
                    cs.dtm_action = taskItem.dtm_actionTime;
                    cs.dtm_end = taskItem.dtm_endTime;
                    cs.int_interval = (taskItem.dtm_endTime - taskItem.dtm_actionTime).Days;//相隔天数
                    cs.int_order = taskItem.int_order;
                    returnNum+=csData.AddContinuousShare(cs);
                }
                return returnNum;
            }
            return returnNum;
        }
        /// <summary>
        /// 将用户当前执行的任务重置为未执行状态
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public int ResetContinuousShare(int userid,int taskId)
        {
            int returnNum = 0;
            csData=new ContinuousShareData();
            List<tb_continuousShare> csList = csData.GetContinuousShareByTaskId(taskId, userid);
            if (csList != null && csList.Count > 0)
            {
                TaskManager tm = new TaskManager();
                List<TaskItemReward> taskItemRewardList = tm.GetTaskItemByTaskId(taskId);
                List<tb_taskItem> taskItemList = new List<tb_taskItem>();
                if (taskItemRewardList != null)
                {
                    foreach (TaskItemReward r in taskItemRewardList)
                    {
                        taskItemList.Add(r.TaskItem);
                    }
                }
                //获取当前日期存在于那个item当中
                int nowOrder = 0;//当前日期所处的排序顺序
                foreach (tb_taskItem taskItem in taskItemList)
                {
                    DateTime dtNow = DateTime.Now;
                    if (dtNow > taskItem.dtm_actionTime && dtNow < taskItem.dtm_endTime)
                    {
                        nowOrder = taskItem.int_order;//当前日期所处的排序顺序
                        csList[0].dtm_action = dtNow;
                        csList[0].dtm_end = taskItem.dtm_endTime;
                        csList[0].dtm_execute = null;
                        csList[0].bit_execute = false;
                        break;
                    }
                }
                for (int i = 0; i < csList.Count(); i++)
                {
                    if (i == 0)
                    {
                        csList[i].int_interval = (csList[i].dtm_end - csList[0].dtm_action).Days;//相隔天数
                    }
                    else
                    {
                        csList[i].dtm_action = csList[i].dtm_end.AddDays(1);
                        csList[i].dtm_end = csList[i].dtm_action.AddDays(csList[i].int_interval);
                        csList[i].bit_execute = false;
                        csList[i].dtm_execute = null;
                    }
                    returnNum += csData.EditContinuousShare(csList[i]);
                }
            }
            else
            {
                returnNum = AddContinuousShareByUserIdAndTaskId(userid, taskId);
            }
            return returnNum;
        }
        /// <summary>
        /// 返回连续执行次数
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public int ContinuousShareTime(int taskId,int userId)
        {
            int returnNum = 0;
            bool isLianxu = IsContinuous(taskId, userId);
            if (isLianxu)
            {
                csData = new ContinuousShareData();
                List<tb_continuousShare> csList = csData.GetContinuousShareByTaskId(taskId, userId);
                if (csList == null)
                {
                    AddContinuousShareByUserIdAndTaskId(userId, taskId);
                    return 0;
                }
                var query = from p in csList
                            where p.bit_execute == true
                            select p;
                returnNum = query.ToList().Count();
                return returnNum;
            }
            else
            {
                ResetContinuousShare(userId, taskId);
                return 0;
            }
        }
        /// <summary>
        /// 当前用户是否已经执行过任务
        /// </summary>
        /// <returns></returns>
        public bool IsExecuteTask(int taskId,int userId)
        {
            csData = new ContinuousShareData();
            List<tb_continuousShare> csList = csData.GetContinuousShareByTaskId(taskId, userId);
            DateTime dtNow = DateTime.Now;
            foreach (tb_continuousShare cs in csList)
            {
                if ((dtNow-cs.dtm_end).Days<=0 && (dtNow - cs.dtm_action).Days>0)
                {
                    if (cs.bit_execute)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        /// <summary>
        /// 检测是否为连续签到
        /// 如果不是连续签到，返回false
        /// 连续签到返回true
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsContinuous(int taskId,int userId)
        {
            csData = new ContinuousShareData();
            List<tb_continuousShare> csList = csData.GetContinuousShareByTaskId(taskId, userId);
            DateTime dtNow = DateTime.Now;
            if (csList != null)
            {
                var query = from p in csList
                            orderby p.int_order
                            select p;
                csList = query.ToList();
                foreach (tb_continuousShare cs in csList)
                {
                    if (cs.bit_execute == false)
                    {
                        if ((dtNow - cs.dtm_end).Days <= 0 && (dtNow - cs.dtm_action).Days > 0)
                            return true;
                        else
                            return false;
                    }
                }
            }
            return false;
        }
    }
}