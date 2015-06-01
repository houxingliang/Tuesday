using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcTusService.Data;
using WcTusService.TuesdayModel;

namespace WcTusService.TuesdayBLL
{
    /// <summary>
    /// 任务执行情况业务逻辑类
    /// </summary>
    public class TaskExecuteManager
    {
        TaskExecuteData taskExecuteData = null;
        TaskItemData taskItemData = null;
        UserData userData = null;
        /// <summary>
        /// 新增任务执行情况
        /// </summary>
        /// <param name="taskexecute"></param>
        /// <returns></returns>
        public int AddTaskExecute(tb_taskExecute taskexecute)
        {
            int returnNum = 0;
            taskExecuteData = new TaskExecuteData();
            returnNum = taskExecuteData.AddTaskExecute(taskexecute);
            return returnNum;
        }
        /// <summary>
        /// 修改任务执行情况
        /// </summary>
        /// <param name="taskexecute"></param>
        /// <returns></returns>
        public int EditTaskExecute(tb_taskExecute taskexecute)
        {
            int returnNum = 0;
            taskExecuteData = new TaskExecuteData();
            returnNum = taskExecuteData.EditTaskExecute(taskexecute);
            return returnNum;
        }
        /// <summary>
        /// 根据登录用户的ID
        /// 申请奖品
        /// </summary>
        /// <param name="taskexecute">申请任务执行的申请</param>
        /// <returns></returns>
        public int TaskApplication(int id)
        {
            int returnNum = 0;
            taskExecuteData = new TaskExecuteData();
            returnNum = taskExecuteData.EditTaskExecuteByUserId(id);
            return returnNum;
        }
        /// <summary>
        /// 根据用户ID发放奖品
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public int GrantRewardByUserID(int id)
        {
            int returnNum = 0;
            taskExecuteData = new TaskExecuteData();
            returnNum = taskExecuteData.GrantRewardByUserID(id);
            return returnNum;
        }
        /// <summary>
        /// 根据任务项发放奖品
        /// </summary>
        /// <param name="taskItemId">任务项ID</param>
        /// <returns></returns>
        public int GrantRewardByTaskId(int taskItemId)
        {
            int returnNum = 0;
            taskExecuteData = new TaskExecuteData();
            returnNum = taskExecuteData.GrantRewardByTaskId(taskItemId);
            return returnNum;
        }

        /// <summary>
        /// 按任务分类(名称)查询奖品发放列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="actionDate"></param>
        /// <param name="endDate"></param>
        public List<RewardUserGrantEntity> GetTaskExecuteByTaskName(string name, DateTime actionDate, DateTime endDate)
        {
            taskExecuteData=new TaskExecuteData();
            taskItemData = new TaskItemData();
            userData=new UserData();
            List<tb_task> taskList = taskExecuteData.GetTaskByName(name,actionDate,endDate);
            //需要拼接业务实体，将任务表和任务执行表的信息拼接到一起。
            if (taskList != null)
            {
                foreach (tb_task task in taskList)
                {
                    //获取任务的所有任务项
                    List<tb_taskItem> taskItemList = taskItemData.GetItemBytaskid(task.pk_task_id);
                    //根据任务项查询符合条件的任务执行情况
                    List<RewardUserGrantEntity> grantList = new List<RewardUserGrantEntity>();
                    if (taskItemList != null)
                    {
                        foreach (tb_taskItem taskItem in taskItemList)
                        {
                            List<tb_taskExecute> taskExecuteList = new List<tb_taskExecute>();
                            taskExecuteList.AddRange(taskExecuteData.GetRewardTmpListByItemId(taskItem.pk_taskItem_id));
                            if (taskExecuteList != null && taskExecuteList.Count > 0)
                            {
                                foreach (tb_taskExecute taskExecute in taskExecuteList)
                                {
                                    RewardUserGrantEntity grant = new RewardUserGrantEntity();
                                    grant.TaskExecute = taskExecute;
                                    grant.User = userData.GetUserByID(taskExecute.fk_user_id);
                                    //根据任务项获取奖励模板的奖品信息
                                    RewardTmpImpData rewardTmpData=new RewardTmpImpData();
                                    RewardData rewardData=new RewardData();
                                    List< tb_reward_Template_imp> impList=rewardTmpData.GetRewardImpList(taskItem.fk_rewardTemplate_id);
                                    List<tb_reward> rewardList=new List<tb_reward>();
                                    foreach(tb_reward_Template_imp imp in impList)
                                    {
                                        tb_reward reward=new tb_reward();
                                        reward.dbl_count=imp.dbl_count;
                                        reward.nvr_rewardName=rewardData.GetRewardByID(imp.fk_reward_id).nvr_rewardName;
                                        grant.Reward.Add(reward);

                                    }
                                }
                            }
                        }
                    }
                    return grantList;
                }
            }
            return null;
        }
        /// <summary>
        /// 根据用户信息查询奖励发放情况
        /// </summary>
        /// <param name="nickName">微信昵称</param>
        /// <param name="name">用户名</param>
        /// <param name="phoneNum">电话号码</param>
        /// <returns></returns>
        public List<RewardUserGrantEntity> GetTaskExecuteByUser(string nickName,string name,string phoneNum)
        {
            UserManager userManager = new UserManager();
            taskExecuteData = new TaskExecuteData();
            userData = new UserData();
            List < tb_user > userList= userManager.GetUserByNameOrPhone(nickName,name,phoneNum);
            if (userList != null)
            {
                List<RewardUserGrantEntity> returnList = new List<RewardUserGrantEntity>();
                foreach (tb_user user in userList)
                {
                    List<tb_taskExecute> taskExecuteList = new List<tb_taskExecute>();
                    taskExecuteList.AddRange(taskExecuteData.GetRewardTmpList(user.int_user_id));
                    if (taskExecuteList != null && taskExecuteList.Count > 0)
                    {
                        foreach (tb_taskExecute taskExecute in taskExecuteList)
                        {
                            RewardUserGrantEntity grant = new RewardUserGrantEntity();
                            grant.TaskExecute = taskExecute;
                            grant.User = userData.GetUserByID(taskExecute.fk_user_id);
                            grant.Reward=new List<tb_reward>();
                            //根据任务项获取奖励模板的奖品信息
                            RewardTmpImpData rewardTmpData = new RewardTmpImpData();
                            RewardData rewardData = new RewardData();
                            List<tb_reward_Template_imp> impList = rewardTmpData.GetRewardImpList(1);
                            List<tb_reward> rewardList = new List<tb_reward>();
                            foreach (tb_reward_Template_imp imp in impList)
                            {
                                tb_reward reward = new tb_reward();
                                reward = rewardData.GetRewardByID(imp.fk_reward_id);
                                reward.dbl_count = imp.dbl_count;
                                grant.Reward.Add(reward);
                            }
                            returnList.Add(grant);
                        }
                    }
                }
                return returnList;
            }
            return null;
        }
        /// <summary>
        /// 按任务分类(主键ID)查询奖品发放列表
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="actionDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        public List<RewardUserGrantEntity> GetTaskExecuteByTaskID(int id, DateTime actionDate, DateTime endDate)
        {
            taskExecuteData = new TaskExecuteData();
            taskItemData = new TaskItemData();
            userData = new UserData();
            List<tb_task> taskList = taskExecuteData.GetTaskByID(id, actionDate, endDate);
            //需要拼接业务实体，将任务表和任务执行表的信息拼接到一起。
            if (taskList != null)
            {
                foreach (tb_task task in taskList)
                {
                    //获取任务的所有任务项
                    List<tb_taskItem> taskItemList = taskItemData.GetItemBytaskid(task.pk_task_id);
                    //根据任务项查询符合条件的任务执行情况
                    List<RewardUserGrantEntity> grantList = new List<RewardUserGrantEntity>();
                    if (taskItemList != null)
                    {
                        foreach (tb_taskItem taskItem in taskItemList)
                        {
                            List<tb_taskExecute> taskExecuteList = new List<tb_taskExecute>();
                            List<tb_taskExecute> temp = taskExecuteData.GetRewardTmpListByItemId(taskItem.pk_taskItem_id);
                            if (temp != null)
                            {
                                taskExecuteList.AddRange(temp);
                                if (taskExecuteList != null && taskExecuteList.Count > 0)
                                {
                                    foreach (tb_taskExecute taskExecute in taskExecuteList)
                                    {
                                        RewardUserGrantEntity grant = new RewardUserGrantEntity();
                                        grant.TaskExecute = taskExecute;
                                        grant.User = userData.GetUserByID(taskExecute.fk_user_id);
                                        //根据任务项获取奖励模板的奖品信息
                                        RewardTmpImpData rewardTmpData = new RewardTmpImpData();
                                        RewardData rewardData = new RewardData();
                                        List<tb_reward_Template_imp> impList = rewardTmpData.GetRewardImpList(taskItem.fk_rewardTemplate_id);
                                        List<tb_reward> rewardList = new List<tb_reward>();
                                        grant.Reward = new List<tb_reward>();
                                        foreach (tb_reward_Template_imp imp in impList)
                                        {
                                            tb_reward reward = new tb_reward();
                                            reward.dbl_count = imp.dbl_count;
                                            reward.nvr_rewardName = rewardData.GetRewardByID(imp.fk_reward_id).nvr_rewardName;
                                            grant.Reward.Add(reward);
                                        }
                                        grantList.Add(grant);
                                    }
                                }   
                            }
                        }
                    }
                    return grantList;
                }
            }
            return null;
        }
    }
}