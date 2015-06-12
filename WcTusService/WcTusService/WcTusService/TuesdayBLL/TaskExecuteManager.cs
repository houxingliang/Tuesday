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
        public List<RewardUserGrantEntity> GetTaskExecuteByTaskId(int id, DateTime actionDate, DateTime endDate)
        {
            taskExecuteData=new TaskExecuteData();
            taskItemData = new TaskItemData();
            userData=new UserData();
            List<tb_task> taskList = taskExecuteData.GetTaskById(id,actionDate,endDate);
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
                            }
                            if (taskExecuteList != null && taskExecuteList.Count > 0)
                            {
                                foreach (tb_taskExecute taskExecute in taskExecuteList)
                                {
                                    RewardUserGrantEntity grant = new RewardUserGrantEntity();
                                    grant.Task = task;
                                    grant.TaskExecute = taskExecute;
                                    grant.User = userData.GetUserByID(taskExecute.fk_user_id);
                                    grant.EntityType = 1;
                                    //根据任务项获取奖励模板的奖品信息
                                    RewardTmpImpData rewardTmpData=new RewardTmpImpData();
                                    RewardData rewardData=new RewardData();
                                    List< tb_reward_Template_imp> impList=rewardTmpData.GetRewardImpList(taskItem.fk_rewardTemplate_id);
                                    List<tb_reward> rewardList=new List<tb_reward>();
                                    grant.Reward = new List<tb_reward>();
                                    foreach(tb_reward_Template_imp imp in impList)
                                    {
                                        tb_reward reward=new tb_reward();
                                        reward.dbl_count=imp.dbl_count;
                                        reward.nvr_rewardName=rewardData.GetRewardByID(imp.fk_reward_id).nvr_rewardName;
                                        reward.dbl_count = imp.dbl_count;
                                        grant.Reward.Add(reward);

                                    }
                                    grant.TmpName = new RewardTemplateData().GetRewardTmpById(taskItem.fk_rewardTemplate_id).nvr_tmpName;
                                    grantList.Add(grant);
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
            //获取用户列表信息
            List<tb_user> userList = new UserData().GetUserByNameOrPhone(nickName, name, phoneNum);
            if (userList != null)
            {
                List<RewardUserGrantEntity> grantList = new List<RewardUserGrantEntity>();
                //根据用户列表获取所有的任务执行信息
                foreach (tb_user user in userList)
                {
                    List<tb_taskExecute> taskExecuteList = new TaskExecuteData().GetTaskExecuteListByUserId(user.int_user_id);
                    //根据任务执行信息，获取对应的分享奖励
                    if (taskExecuteList != null)
                    {
                        RewardUserGrantEntity grant = new RewardUserGrantEntity();
                        grant.User = user;
                        foreach (tb_taskExecute taskExecute in taskExecuteList)
                        {
                            grant.TaskExecute = taskExecute;
                            tb_taskItem taskItem=new TaskItemData().GetItemByid(taskExecute.fk_taskItem_id);
                            grant.Task = new TaskData().GettaskByid(taskItem.fk_task_id);
                            grant.EntityType = 1;
                            tb_rewardTemplate rewardTemplate = new RewardTemplateData().GetRewardTmpById((int)taskItem.fk_rewardTemplate_id);
                            List<tb_reward> rewardList = new List<tb_reward>();
                            //根据奖励模板获取奖品信息
                            if (rewardTemplate != null)
                            {
                                List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rewardTemplate.pk_rewardTemplate_id);
                                foreach (tb_reward_Template_imp imp in impList)
                                {
                                    tb_reward reward = new RewardData().GetRewardByID(imp.fk_reward_id);
                                    reward.dbl_count = imp.dbl_count;
                                    rewardList.Add(reward);
                                }
                                grant.TmpName = rewardTemplate.nvr_tmpName;
                                grant.Reward = rewardList;
                                grant.Type = "任务奖励";
                            }
                        }
                        grantList.Add(grant);
                    }

                    //根据用户列表获取所有的用户分享信息
                    List<tb_userShare> userShareList = new UserShareData().GetUserShareListByUserId(user.int_user_id);
                    if (userShareList != null)
                    {
                        foreach (tb_userShare userShare in userShareList)
                        {
                            RewardUserGrantEntity grant = new RewardUserGrantEntity();
                            grant.User = user;
                            grant.UserShare = userShare;
                            grant.Task = new tb_task();
                            grant.Task.nvr_taskName = new ShareData().GetshareByid(userShare.fk_shareContents_id).nvr_shareName;
                            grant.Task.dtm_actionTime = DateTime.Now;
                            grant.Task.dtm_createTime = DateTime.Now;
                            grant.Task.dtm_endTime = DateTime.Now;
                            grant.EntityType = 0;
                            //首次分享
                            if (userShare.bit_firstShare)
                            {
                                grant.Type = "首次分享";
                                tb_share share = new ShareData().GetshareByid(userShare.fk_shareContents_id);
                                tb_rewardTemplate rewardTemplate = new RewardTemplateData().GetRewardTmpById(share.fk_rewardTemplate_id_f);
                                List<tb_reward> rewardList = new List<tb_reward>();
                                //根据奖励模板获取奖品信息
                                if (rewardTemplate != null)
                                {
                                    List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rewardTemplate.pk_rewardTemplate_id);
                                    foreach (tb_reward_Template_imp imp in impList)
                                    {
                                        tb_reward reward = new RewardData().GetRewardByID(imp.fk_reward_id);
                                        rewardList.Add(reward);
                                    }
                                    grant.TmpName = rewardTemplate.nvr_tmpName;
                                    grant.Reward = rewardList;
                                }
                            }
                            //二次分享
                            else if (!userShare.bit_firstShare && userShare.fk_user_id != null)
                            {
                                grant.Type = "二次分享";
                                tb_share share = new ShareData().GetshareByid(userShare.fk_shareContents_id);
                                tb_rewardTemplate rewardTemplate = new RewardTemplateData().GetRewardTmpById(share.fk_rewardTemplate_id_s);
                                List<tb_reward> rewardList = new List<tb_reward>();
                                //根据奖励模板获取奖品信息
                                if (rewardTemplate != null)
                                {
                                    List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rewardTemplate.pk_rewardTemplate_id);
                                    foreach (tb_reward_Template_imp imp in impList)
                                    {
                                        tb_reward reward = new RewardData().GetRewardByID(imp.fk_reward_id);
                                        rewardList.Add(reward);
                                    }
                                    grant.TmpName = rewardTemplate.nvr_tmpName;
                                    grant.Reward = rewardList;
                                }
                            }
                            //二次分享返还
                            else if (!userShare.bit_firstShare && userShare.fk_user_id == null && userShare.fk_superUser_id != null)
                            {
                                grant.Type = "二次分享返还";
                                tb_share share = new ShareData().GetshareByid(userShare.fk_shareContents_id);
                                tb_rewardTemplate rewardTemplate = new RewardTemplateData().GetRewardTmpById((int)share.fk_superUser_rewardTmp_id);
                                List<tb_reward> rewardList = new List<tb_reward>();
                                //根据奖励模板获取奖品信息
                                if (rewardTemplate != null)
                                {
                                    List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rewardTemplate.pk_rewardTemplate_id);
                                    foreach (tb_reward_Template_imp imp in impList)
                                    {
                                        tb_reward reward = new RewardData().GetRewardByID(imp.fk_reward_id);
                                        rewardList.Add(reward);
                                    }
                                    grant.TmpName = rewardTemplate.nvr_tmpName;
                                    grant.Reward = rewardList;
                                }
                            }
                            grantList.Add(grant);
                        }
                    }
                }
                return grantList;
            }
            return null;
        }
        /// <summary>
        /// 按任务分类(主键ID)查询奖品发放列表
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="actionDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        public List<RewardUserGrantEntity> GetTaskExecuteByTaskID(int id,bool isApply,bool isGrant)
        {
            taskExecuteData = new TaskExecuteData();
            taskItemData = new TaskItemData();
            userData = new UserData();
            List<tb_task> taskList = taskExecuteData.GetTaskByID(id);
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
                                        grant.Type = "任务奖励";
                                        grant.Task = task;
                                        grant.EntityType = 1;
                                        grant.TmpName = new RewardTemplateData().GetRewardTmpById(taskItem.fk_rewardTemplate_id).nvr_tmpName;
                                        grantList.Add(grant);
                                    }
                                }   
                            }
                        }
                    }
                    if (grantList != null)
                    {
                        var query = from p in grantList
                                    where p.TaskExecute.bit_isApply == isApply &&
                                    p.TaskExecute.bit_isGrant == isGrant
                                    select p;
                        return query.ToList();
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 根据任务执行情况发放奖励
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public int FafangTask(List<int> idList)
        {
            int returnNum = 0;
            if (idList != null)
            {
                taskExecuteData = new TaskExecuteData();
                foreach (int i in idList)
                {
                    tb_taskExecute taskExecute = taskExecuteData.GetTaskExecuteById(i);
                    taskExecute.bit_isGrant = true;
                    returnNum += taskExecuteData.EditTaskExecute(taskExecute);
                }
            }
            return returnNum;
        }
        /// <summary>
        /// 根据用户ID
        /// 查询任务执行信息集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<tb_taskExecute> GetTaskExecuteListByUserId(int id)
        {
            taskExecuteData = new TaskExecuteData();
            return taskExecuteData.GetTaskExecuteListByUserId(id);
        }
        /// <summary>
        /// 根据用户ID获取该用户连续签到次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetTimeByUsedId(int id)
        {
            //获取正在执行中的任务ID
            TaskManager taskManager=new TaskManager();
            int taskId = taskManager.GetActivityTask().pk_task_id;
            //获取正在执行中的任务ID的任务项集合
            List<int> taskItem = new List<int>();
            taskItemData = new TaskItemData();
            List<tb_taskItem> itemList = taskItemData.GetItemBytaskid(taskId);
            if (itemList != null)
            {
                foreach (tb_taskItem item in itemList)
                {
                    taskItem.Add(item.pk_taskItem_id);
                }
            }
            //获取该用户的任务执行信息列表
            List<tb_taskExecute> taskExecuteList = GetTaskExecuteListByUserId(id);
            if (taskExecuteList != null)
            {
                List<tb_taskExecute> executeList = new List<tb_taskExecute>();
                foreach (int i in taskItem)
                {
                    for (int j = 0; j < taskExecuteList.Count(); j++)
                    {
                        if (i == taskExecuteList[j].fk_taskItem_id)
                        {
                            executeList.Add(taskExecuteList[j]);
                            break;
                        }
                    }
                }
                int returnNum = 0;
                //用户执行的当前活动任务的任务执行集合
                if (executeList != null && executeList.Count() > 0)
                {
                    DateTime taday = DateTime.Now;
                    var query = from p in executeList
                                orderby p.dtm_executeTime
                                select p;
                    executeList = query.ToList();
                    if (executeList[0].dtm_executeTime.ToShortDateString().Equals(taday.ToShortDateString()))
                    {
                        for (int i = 0; i < executeList.Count(); i++)
                        {
                            if (executeList[i].dtm_executeTime.ToShortDateString().Equals(taday.AddDays(-i).ToShortDateString()))
                            {
                                returnNum += returnNum;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else if (executeList[0].dtm_executeTime.ToShortDateString().Equals(taday.AddDays(-1).ToShortDateString()))
                    {
                        for (int i = 0; i < executeList.Count(); i++)
                        {
                            if (executeList[i].dtm_executeTime.ToShortDateString().Equals(taday.AddDays(-i).ToShortDateString()))
                            {
                                returnNum += returnNum;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                return returnNum;
            }
            return 0;
        }
    }
}