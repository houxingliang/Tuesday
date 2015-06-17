using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcTusService.Data;
using WcTusService.TuesdayModel;

namespace WcTusService.TuesdayBLL
{
    /// <summary>
    /// 任务业务逻辑类
    /// </summary>
    public class TaskManager
    {
        TaskData taskdata = null;//任务数据访问类
        TaskItemData taskItemdata = null;//任务项数据访问类
        /// <summary>
        /// 添加任务信息
        /// </summary>
        /// <param name="task">任务实体</param>
        /// <returns></returns>
        public int AddTask(tb_task task)
        {
            taskdata = new TaskData();
            return taskdata.Addtask(task);
        }
        /// <summary>
        /// 修改任务信息
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int EditTask(tb_task task)
        {
            taskdata = new TaskData();
            return taskdata.EditTask(task);
        }
        /// <summary>
        /// 任务列表
        /// </summary>
        /// <returns></returns>
        public List<tb_task> GetTaskList()
        {
            taskdata = new TaskData();
            List<tb_task> taskList = taskdata.GettaskList();
            if (taskList != null && taskList.Count > 0)
                return taskList;
            else
                return null;
        }
        /// <summary>
        /// 根据任务的主键ID
        /// 查询任务实体信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public tb_task GetTaskById(int id)
        {
            taskdata = new TaskData();
            tb_task tbtask = taskdata.GettaskByid(id);
            if (tbtask != null)
                return tbtask;
            else
                return null;
        }
        /// <summary>
        /// 删除任务信息
        /// 将任务的删除状态改为true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelTask(int id)
        {
            taskdata = new TaskData();
            tb_task task = taskdata.GettaskByid(id);
            if (task != null)
            {
                task.bit_isDelete = true;
                return taskdata.EditTask(task);
            }
            else
                return 0;
        }
        /// <summary>
        /// 根据任务主键ID
        /// 获取任务项列表
        /// </summary>
        /// <param name="id">任务主键ID</param>
        /// <returns></returns>
        public List<TaskItemReward> GetTaskItemByTaskId(int id)
        {
            taskItemdata = new TaskItemData();
            List<tb_taskItem> taskItemList = taskItemdata.GetItemBytaskid(id);
            if (taskItemList != null)
            {
                List<TaskItemReward> taskItemRewardList = new List<TaskItemReward>();
                foreach (tb_taskItem taskItem in taskItemList)
                {
                    TaskItemReward taskItemReward = new TaskItemReward();
                    taskItemReward.RewardTmp = new RewardTemplateData().GetRewardTmpById(taskItem.fk_rewardTemplate_id);
                    taskItemReward.RewardList = new List<tb_reward>();
                    taskItemReward.TaskItem = taskItem;
                    //该模板下的奖励集合
                    List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(taskItemReward.RewardTmp.pk_rewardTemplate_id);
                    foreach (tb_reward_Template_imp imp in impList)
                    {
                        tb_reward reward = new RewardData().GetRewardByID(imp.fk_reward_id);
                        taskItemReward.RewardList.Add(reward);
                    }
                    taskItemRewardList.Add(taskItemReward);
                }
                return taskItemRewardList;
            }
            else
                return null;
        }
        /// <summary>
        /// 添加任务项信息
        /// </summary>
        /// <param name="taskItem">任务项</param>
        /// <returns></returns>
        public int AddTaskItem(tb_taskItem taskItem)
        {
            int returnNum = 0;
            taskItemdata = new TaskItemData();
            if (taskItem != null)
            {
                returnNum = taskItemdata.AddtaskItem(taskItem);
            }
            return returnNum;
        }
        /// <summary>
        /// 修改任务项
        /// </summary>
        /// <param name="taskItem"></param>
        /// <returns></returns>
        public int EditTaskItem(tb_taskItem taskItem)
        {
            int returnNum = 0;
            taskItemdata = new TaskItemData();
            if (taskItem != null)
            {
                returnNum = taskItemdata.EdittaskItem(taskItem);
            }
            return returnNum;
        }
        /// <summary>
        /// 删除任务项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelTaskItem(int id)
        {
            int returnNum = 0;
            taskItemdata = new TaskItemData();
            returnNum = taskItemdata.DelTaskItem(id);
            return returnNum;
        }
        /// <summary>
        /// 根据任务项主键ID查询任务项信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_taskItem GetTaskItemById(int id)
        {
            taskItemdata = new TaskItemData();
            tb_taskItem ti = taskItemdata.GetItemByid(id);
            if (ti != null)
                return ti;
            else
                return null;
        }
        /// <summary>
        /// 根据任务得到奖品明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<tb_reward> GetRewardByTaskId(int id)
        {
            taskdata = new TaskData();
            return taskdata.GetRewardByTaskId(id);
        }
        /// <summary>
        /// 获取正在执行中的任务信息
        /// </summary>
        /// <returns></returns>
        public tb_task GetActivityTask()
        {
            taskdata = new TaskData();
            return taskdata.GetActivityTask();
        }
    }
}