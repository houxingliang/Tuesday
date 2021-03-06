﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Security;
using WcTusService.TuesdayBLL;
using WcTusService.TuesdayModel;

namespace WcTusService.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“RewardService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 RewardService.svc 或 RewardService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)] 
    public class RewardService : IRewardService
    {
        #region 废代码
        public void DoWork()
        {
        }
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<string> GetList(string token)
        {
            List<string> list = new List<string>();
            list.Add("s");
            list.Add("s");
            list.Add("'");
            return list;
        }
        #endregion

        #region 奖品相关
        /// <summary>
        /// 添加奖品信息
        /// </summary>
        /// <param name="reward"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddReward(tb_reward reward,string token)
        {
            new TokenManager().IsToken(token);
            RewardManager rm = new RewardManager();
            return rm.AddReward(reward);
        }
        /// <summary>
        /// 修改奖品信息
        /// </summary>
        /// <param name="reward"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int EditReward(tb_reward reward, string token)
        {
            new TokenManager().IsToken(token);
            RewardManager manager = new RewardManager();
            return manager.EditReward(reward);
        }
        /// <summary>
        /// 获取奖品信息列表
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_reward> GetRewardList(string token)
        {
            new TokenManager().IsToken(token);
            RewardManager rm = new RewardManager();
            return rm.GetRewardList();
        }
        /// <summary>
        /// 删除奖品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int DelRewardList(int id, string token)
        {
            new TokenManager().IsToken(token);
            RewardManager rm = new RewardManager();
            return rm.DelRewardById(id);
        }
        #endregion
       
        #region 奖品模板相关
        /// <summary>
        /// 添加奖品模板信息
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddRewardTmp(RewardTemplate rt, string token)
        {
            new TokenManager().IsToken(token);
            //rt = new RewardTemplate();
            //rt.RewardTemplateImp = new List<tb_reward_Template_imp>();
            //tb_reward_Template_imp imp = new tb_reward_Template_imp();
            //imp.bit_isDelete = false;
            //imp.dbl_count = 0.01;
            //imp.fk_reward_id = 1;
            //imp.fk_rewardTemplate_id = 1;
            //rt.RewardTemplateImp.Add(imp);
            //rt.RewardTmp = new tb_rewardTemplate();
            //rt.RewardTmp.bit_isDelete = false;
            //rt.RewardTmp.nvr_tmpName = "糖币";
            //rt.RewardTmp.pk_rewardTemplate_id = 1;
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(RewardTemplate));
            byte[] byteArr;
            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, rt);

                byteArr = ms.ToArray();
            }
            string temp = Encoding.UTF8.GetString(byteArr);
            Console.WriteLine(temp); 


            RewardTemplateManager rtm = new RewardTemplateManager();
            return rtm.AddRewardTemplate(rt);
        }
        /// <summary>
        /// 修改奖励模板
        /// </summary>
        /// <param name="rt">奖励模板实体</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int EditRewardTmp(RewardTemplate rt, string token)
        {
            new TokenManager().IsToken(token);
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(RewardTemplate));
            byte[] byteArr;
            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, rt);

                byteArr = ms.ToArray();
            }
            string temp = Encoding.UTF8.GetString(byteArr);
            Console.WriteLine(temp);
            RewardTemplateManager rtm = new RewardTemplateManager();
            return rtm.EditRewardTemplate(rt);
        }
        /// <summary>
        /// 删除奖励模板
        /// </summary>
        /// <param name="rewardTmpId"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int DelRewardTmp(int rewardTmpId, string token)
        {
            new TokenManager().IsToken(token);
            RewardTemplateManager rewardTmp = new RewardTemplateManager();
            return rewardTmp.DelRewardTemplate(rewardTmpId);
        }
        /// <summary>
        /// 奖品模板集合
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<RewardTemplate> GetRewrdTmpList(string token)
        {
            new TokenManager().IsToken(token);
            RewardTemplateManager rtm = new RewardTemplateManager();
            return rtm.GetRewardTemplateList();
        }
        /// <summary>
        /// 根据奖励模板ID获取奖励模板信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="token"></param>
        /// <returns>模板信息</returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public RewardTemplate GetRewardTmpById(int id, string token)
        {
            new TokenManager().IsToken(token);
            RewardTemplateManager m = new RewardTemplateManager();
            return m.GetRewardTemplateById(id);
        }
        /// <summary>
        /// 根据奖励模板ID获取糖币信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public tb_reward GetTangbiByTmpId(int id, string token)
        {
            new TokenManager().IsToken(token);
            RewardTemplateManager rtm = new RewardTemplateManager();
            return rtm.GetTangbiByTmpId(id);
        }
        /// <summary>
        /// 根据模板主键ID查询奖励模板的所有奖品信息
        /// </summary>
        /// <param name="tmpID">模板主键ID</param>
        /// <returns>模板下的奖品列表</returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_reward_Template_imp> GetRewardImpList(int tmpID, string token)
        {
            new TokenManager().IsToken(token);
            RewardTemplateManager rtm = new RewardTemplateManager();
            return rtm.GetRewardImpList(tmpID);
        }
        #endregion

        #region 分享相关
        ShareManager shareManager = null;    
        /// <summary>
        /// 添加分享信息
        /// </summary>
        /// <param name="share"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddShare(tb_share share, string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.AddShare(share);
        }
        /// <summary>
        /// 修改分享信息
        /// </summary>
        /// <param name="share"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int EditShare(tb_share share, string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.EditShare(share);
        }
        /// <summary>
        /// 删除分享信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int DelShare(int id, string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.DelShare(id);
        }
        /// <summary>
        /// 获取分享列表信息
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_share> GetShareList(bool status,string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.GetShareList(status);
        }

        /// <summary>
        /// 根据分享主键ID获取分享信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>分享信息</returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public tb_share GetShareById(int id, string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.GetShareById(id);
        }
        /// <summary>
        /// 获取热门分享信息列表
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_share> GetHotShareList(string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.GetHotShare();
        }
        /// <summary>
        /// 获取最新分享信息列表
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_share> GetNewShareList(string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.GetNewShare();
        }
        /// <summary>
        /// 根据分享主键ID获取奖品明细
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns>奖品明细</returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_reward> GetRewardByShareId(int id,string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.GetRewardByShareId(id);
        }
        /// <summary>
        /// 根据用户ID
        /// 查看分享内容是否被用户分享
        /// </summary>
        /// <param name="userid">用户主键ID</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<UserShareReward> GetUserShareList(int userid,string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.GetUserShareList(userid);
        }
        /// <summary>
        /// 根据分享主键获取
        /// 该分享下的所有奖励信息
        /// </summary>
        /// <param name="shareId">分享主键ID</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<RewardUserGrantEntity> GetRewardMessageByShareId(int shareId, string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.GetRewardMessageByShareId(shareId);
        }
        #endregion

        #region 任务相关
        TaskManager taskManager = null;
        /// <summary>
        /// 发布任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddTask(tb_task task, string token)
        {
            new TokenManager().IsToken(token);
            taskManager = new TaskManager();
            return taskManager.AddTask(task);
        }
        /// <summary>
        /// 修改任务信息
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int EditTask(tb_task task, string token)
        {
            new TokenManager().IsToken(token);
            taskManager = new TaskManager();
            return taskManager.EditTask(task);
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int DelTask(int id, string token)
        {
            new TokenManager().IsToken(token);
            taskManager = new TaskManager();
            return taskManager.DelTask(id);
        }
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_task> GetTaskList(string token)
        {
            new TokenManager().IsToken(token);
            taskManager = new TaskManager();
            return taskManager.GetTaskList();
        }

        /// <summary>
        /// 根据任务主键ID获取任务信息
        /// </summary>
        /// <param name="id">主键ID、</param>
        /// <returns>任务实体信息</returns>
        public tb_task GetTaskById(int id, string token)
        {
            new TokenManager().IsToken(token);
            taskManager = new TaskManager();
            return taskManager.GetTaskById(id);
        }
        /// <summary>
        /// 根据任务ID获取奖品明细
        /// </summary>
        /// <param name="id">任务主键ID</param>
        /// <param name="token"></param>
        /// <returns>奖品明细</returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_reward> GetRewardByTaskId(int id,string token)
        {
            new TokenManager().IsToken(token);
            taskManager = new TaskManager();
            return taskManager.GetRewardByTaskId(id);
        }
        #endregion

        #region  任务项相关
        
        /// <summary>
        /// 添加任务项
        /// </summary>
        /// <param name="taskItem"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddTaskItem(tb_taskItem taskItem, string token)
        {
            new TokenManager().IsToken(token);
            //{"bit_isDelete":false,"bit_isInherit":true,"bit_status":true,"dtm_actionTime":"\\/Date(1431931532208+0800)\\/","dtm_endTime":"\\/Date(1431931532208+0800)\\/","fk_rewardTemplate_id":1,"fk_share_id":1,"fk_task_id":1,"int_forward":1,"int_order":1,"pk_taskItem_id":0}
            //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(tb_taskItem));
            //byte[] byteArr;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    json.WriteObject(ms, taskItem);

            //    byteArr = ms.ToArray();
            //}
            //string temp = Encoding.UTF8.GetString(byteArr);
            //Console.WriteLine(temp);
            taskManager = new TaskManager();
            
            return taskManager.AddTaskItem(taskItem);;
        }
        /// <summary>
        /// 修改任务项信息
        /// </summary>
        /// <param name="taskItem"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int EditTaskItem(tb_taskItem taskItem, string token)
        {
            new TokenManager().IsToken(token);
            taskManager = new TaskManager();
            return taskManager.EditTaskItem(taskItem);
        }
        /// <summary>
        /// 根据主键ID
        /// 获取任务项详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public tb_taskItem GetTaskItem(int id, string token)
        {
            new TokenManager().IsToken(token);
            taskManager = new TaskManager();
            return taskManager.GetTaskItemById(id);
        }
        /// <summary>
        /// 根据主键ID
        /// 删除任务项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int DelTaskItem(int id, string token)
        {
            new TokenManager().IsToken(token);
            taskManager = new TaskManager();
            return taskManager.DelTaskItem(id) ;
        }
        /// <summary>
        /// 根据任务ID（外键）
        /// 获取任务下的所有任务项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<TaskItemReward> GetTaskItemList(int id, string token)
        {
            new TokenManager().IsToken(token);
            taskManager = new TaskManager();
            return taskManager.GetTaskItemByTaskId(id);
        }
        #endregion

        #region 用户任务执行情况
        /// <summary>
        /// 根据用户ID获取该用户连续签到次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int GetTimeByUserId(int userId,string token)
        {
            new TokenManager().IsToken(token);
            taskExecuteManager = new TaskExecuteManager();
            return taskExecuteManager.GetTimeByUsedId(userId);
        }
        #endregion

        #region 奖品发放相关
        TaskExecuteManager taskExecuteManager = null;
        /// <summary>
        /// 按任务分类查询列表信息
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <param name="actionDate">任务开始时间</param>
        /// <param name="endDate">任务结束时间</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<RewardUserGrantEntity> GetTaskExecuteByTaskName(int id, DateTime actionDate, DateTime endDate, string token)
        {
            new TokenManager().IsToken(token);
            taskExecuteManager = new TaskExecuteManager();
            return taskExecuteManager.GetTaskExecuteByTaskId(id,actionDate,endDate);
        }
        /// <summary>
        /// 根据活动内容分类查询奖品发放列表信息
        /// </summary>
        /// <param name="name">活动内容名称</param>
        /// <param name="actionDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="token">验证token</param>
        /// <returns>活动列表</returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_share> GetShareList_Grant(string name, DateTime actionDate, DateTime endDate, string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.GetShareList(name,actionDate,endDate);
        }
        /// <summary>
        /// 根据分享主键ID获取用户的奖品信息
        /// </summary>
        /// <param name="id">分享主键</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<RewardUserGrantEntity> GetShareGrantListById(int id, bool isApply, bool isGrant, string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.GetShareGrantListById(id,isApply,isGrant);
        }

        /// <summary>
        /// 按任务分类主键ID查询列表信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="actionDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<RewardUserGrantEntity> GetTaskExecuteByTaskID(int id, bool isApply, bool isGrant, string token)
        {
            new TokenManager().IsToken(token);
            taskExecuteManager = new TaskExecuteManager();
            return taskExecuteManager.GetTaskExecuteByTaskID(id,isApply,isGrant);
        }
        /// <summary>
        /// 根据用户信息查询奖品发放信息
        /// </summary>
        /// <param name="nickName">微信昵称</param>
        /// <param name="name">用户名</param>
        /// <param name="phoneNum">电话号码</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<RewardUserGrantEntity> GetTaskExecuteByUser(string nickName, string name, string phoneNum, string token)
        {
            new TokenManager().IsToken(token);
            taskExecuteManager = new TaskExecuteManager();
            return taskExecuteManager.GetTaskExecuteByUser(nickName,name,phoneNum);
        }
        /// <summary>
        /// 根据用户ID发放奖品
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int GrantRewardByUserID(int id, string token)
        {
            new TokenManager().IsToken(token);
            taskExecuteManager = new TaskExecuteManager();
            return taskExecuteManager.GrantRewardByUserID(id);
        }
        /// <summary>
        /// 根据登录用户ID申请奖品
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int TaskApplication(int id, string token)
        {
            new TokenManager().IsToken(token);
            taskExecuteManager = new TaskExecuteManager();
            return taskExecuteManager.TaskApplication(id);
        }
        /// <summary>
        /// 根据任务执行表主键ID发放奖励
        /// </summary>
        /// <param name="idList">任务执行表主键集合</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int FafangTask(List<int> idList,string token)
        {
            new TokenManager().IsToken(token);
            taskExecuteManager = new TaskExecuteManager();
            return taskExecuteManager.FafangTask(idList);
        }
        /// <summary>
        /// 根据用户分项表主键ID发放奖励
        /// </summary>
        /// <param name="idList">用户分享表主键集合</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int FafangShare(List<int> idList,string token)
        {
            new TokenManager().IsToken(token);
            shareManager=new ShareManager();
            return shareManager.FafangShare(idList);
        }
        /// <summary>
        /// 用户申请任务奖励
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int ShenQingTask(List<int> idList, string token)
        {
            new TokenManager().IsToken(token);
            taskExecuteManager = new TaskExecuteManager();
            return taskExecuteManager.FafangTask(idList);
        }
        /// <summary>
        /// 用户申请分享奖励
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int ShenQingShare(List<int> idList, string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.ShenQingShare(idList);
        }
        /// <summary>
        /// 根据用户主键ID
        /// 获取用户的所有应得奖励信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<RewardUserGrantEntity> GetUserRewardByUserId(int userId, string token)
        {
            new TokenManager().IsToken(token);
            shareManager = new ShareManager();
            return shareManager.GetUserRewardByUserId(userId);
        }
        #endregion

        #region 统计报表
        StatisticalManager statisticalManager = null;
        /// <summary>
        /// 活动首次转发统计
        /// </summary>
        /// <param name="shareId">任务ID</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<Statistical_UserShare_Business> FirstShare(int shareId, string token)
        {
            new TokenManager().IsToken(token);
            statisticalManager = new StatisticalManager();
            return statisticalManager.FirstShare(shareId);
        }

        /// <summary>
        /// 总转发次数统计
        /// </summary>
        /// <param name="shareId">任务ID</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<Statistical_UserShare_Business> TotalShare(int shareId, string token)
        {
            new TokenManager().IsToken(token);
            statisticalManager = new StatisticalManager();
            return statisticalManager.TotalShare(shareId);
        }

        /// <summary>
        /// 活动用户首次转发排名统计
        /// </summary>
        /// <param name="taskId">活动ID集合</param>
        /// <returns></returns>
        public List<Statistical_Rank_business> FirstRank(List<int> taskId, string token)
        {
            new TokenManager().IsToken(token);
            statisticalManager = new StatisticalManager();
            return statisticalManager.FirstRank(taskId);
        }
        /// <summary>
        /// 活动用户首次转发排名统计前十
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="top">前几条数据</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<Statistical_Rank_business> FirstRankTop10(List<int> taskId,int top, string token)
        {
            new TokenManager().IsToken(token);
            statisticalManager = new StatisticalManager();
            return statisticalManager.FirstRank(taskId);
        }
        /// <summary>
        /// 活动用户总转排名统计
        /// </summary>
        /// <param name="taskId">活动ID集合</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<Statistical_Rank_business> TotalRank(List<int> taskId, string token)
        {
            new TokenManager().IsToken(token);
            statisticalManager = new StatisticalManager();
            return statisticalManager.TotalRank(taskId);
        }

        /// <summary>
        /// 活动用户总转排名统计前10
        /// </summary>
        /// <param name="taskId">活动ID集合</param>
        /// <param name="top">前几条数据</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<Statistical_Rank_business> TotalRankTop10(List<int> taskId,int top,string token)
        {
            new TokenManager().IsToken(token);
            statisticalManager = new StatisticalManager();
            return statisticalManager.TotalRank(taskId);
        }
        /// <summary>
        /// 用户奖品总数统计
        /// </summary>
        /// <param name="actionTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="RewardId">任务ID</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<Statistical_UserRank_Business> UserRewardSum(DateTime actionTime, DateTime endTime, int RewardId, string token)
        {
            new TokenManager().IsToken(token);
            statisticalManager = new StatisticalManager();
            return statisticalManager.UserRewardSum(actionTime,endTime,RewardId);
        }
        /// <summary>
        /// 获取糖币明细
        /// </summary>
        /// <param name="actionTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>糖币明细集合</returns>
        public List<TangbiDetail> GetTangBiDetail(DateTime actionTime, DateTime endTime, string token)
        {
            new TokenManager().IsToken(token);
            statisticalManager = new StatisticalManager();
            return statisticalManager.GetTangbiDetal(actionTime,endTime);
        }
        /// <summary>
        /// 统计报表
        /// 获取所有奖品的发放总数
        /// </summary>
        /// <param name="actionTime"></param>
        /// <param name="endTime"></param>
        /// <param name="token"></param>
        /// <returns></returns>
         public List<tb_reward> GetTotalRewardByTime(DateTime actionTime, DateTime endTime,string token)
        {
            new TokenManager().IsToken(token);
            statisticalManager = new StatisticalManager();
            return statisticalManager.GetTotalRewardByTime(actionTime, endTime);
        }
        #endregion

        #region 用户相关
        UserManager userManager = new UserManager();
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddUser(tb_user user, string token)
        {
            new TokenManager().IsToken(token);
            //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(tb_user));
            //byte[] byteArr;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    json.WriteObject(ms, user);

            //    byteArr = ms.ToArray();
            //}
            //string temp = Encoding.UTF8.GetString(byteArr);
            //Console.WriteLine(temp);

            return userManager.AddUser(user);
        }
        /// <summary>
        /// 更改用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int EditUser(tb_user user, string token)
        {
            new TokenManager().IsToken(token);
            return userManager.EditUser(user);
        }
        /// <summary>
        /// 根据用户ID查询用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public tb_user GetUserById(int id, string token)
        {
            new TokenManager().IsToken(token);
            return userManager.GetUserById(id);
        }
        /// <summary>
        /// 手机号是否被占用
        /// </summary>
        /// <param name="phoneNum">手机号</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public bool IsUsedPhone(string phoneNum, string token)
        {
            new TokenManager().IsToken(token);
            return userManager.IsUsedPhone(phoneNum);
        }
        /// <summary>
        /// 根据用户电话ID
        /// 获取用户信息
        /// </summary>
        /// <param name="num"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public tb_user GetUserByPhoneNum(string num,string token)
        {
            new TokenManager().IsToken(token);
            return userManager.GetUserByPhoneNum(num);
        }
        /// <summary>
        /// 根据星期二用户ID
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public tb_user GetUserByTuesdayId(string id,string token)
        {
            new TokenManager().IsToken(token);
            return userManager.GetUserByTuesdayId(id);
        }
        /// <summary>
        /// 根据微信OpenId
        /// 查询用户信息
        /// </summary>
        /// <param name="openId">微信OpenId</param>
        /// <param name="token">token随机值</param>
        /// <returns>user实体</returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public tb_user GetUserByOpenId(string openId, string token)
        {
            new TokenManager().IsToken(token);
            userManager = new UserManager();
            return userManager.GetUserByOpenId(openId);
        }
        #endregion

        #region token相关
        //数据库模型
        ShareWeiEntities share = new ShareWeiEntities();
        /// <summary>
        /// 更新token令牌
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public TokenEntity EditToken(string appid)
        {
            string timestamp = DateTime.Now.ToBinary().ToString();
            var q = from p in share.tb_token
                        where p.vr_appid == appid
                        select p;
            if (q.FirstOrDefault() == null)
            {
                throw new Exception("无效的AppId");
            }
            else
            {
                var query = from p in share.tb_token
                            where p.vr_appid == appid
                            select p;
                tb_token token = query.FirstOrDefault();
                token.vr_token =GenerateToken(appid,timestamp);
                token.dtm_tokenTime = DateTime.Now.AddHours(2);
                //更新数据库实体
                share.Entry(token).State = EntityState.Modified;
                share.SaveChanges();
                TokenEntity returnToken = new TokenEntity();
                returnToken.Token = token.vr_token;
                returnToken.TokenTime = token.dtm_tokenTime;
                return returnToken;
            }
        }

        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public TokenEntity GetToken(string appid)
        {
            var query = from p in share.tb_token
                        where p.vr_appid == appid
                        select p;
            tb_token token = query.FirstOrDefault();
            if (token != null)
            {
                TokenEntity returnToken = new TokenEntity();
                returnToken.Token = token.vr_token;
                returnToken.TokenTime = token.dtm_tokenTime;
                return returnToken;
            }
            else
                throw new Exception("无效的AppId");
           
        }
        //生成token
        public string GenerateToken(string appid, string timeStamp)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile((appid+timeStamp), "SHA1").ToLower();
        }
        #endregion

        #region 连续签到
        ContinuousShareManager csManager = null;
        /// <summary>
        /// 根据任务ID和用户ID
        /// 新增任务连续执行情况
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddContinuousShareByUserIdAndTaskId(int userid,int taskId,string token)
        {
            new TokenManager().IsToken(token);
            csManager = new ContinuousShareManager();
            return csManager.AddContinuousShareByUserIdAndTaskId(userid, taskId);
        }
         /// <summary>
        /// 将用户当前执行的任务重置为未执行状态
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int ResetContinuousShare(int userid,int taskId,string token)
        {
            new TokenManager().IsToken(token);
            csManager = new ContinuousShareManager();
            return csManager.ResetContinuousShare(userid, taskId);
        }
         /// <summary>
        /// 返回连续执行次数
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int ContinuousShareTime(int taskId,int userId,string token)
        {
            new TokenManager().IsToken(token);
            csManager = new ContinuousShareManager();
            return csManager.ContinuousShareTime(userId, taskId);
        }
        /// <summary>
        /// 当前用户是否已经执行过任务
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public bool IsExecuteTask(int taskId,int userId,string token)
        {
            new TokenManager().IsToken(token);
            csManager = new ContinuousShareManager();
            return csManager.IsExecuteTask(userId, taskId);
        }
        /// <summary>
        /// 检测是否为连续签到
        /// 如果不是连续签到，返回false
        /// 连续签到返回true
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public bool IsContinuous(int taskId,int userId,string token)
        {
            new TokenManager().IsToken(token);
            csManager = new ContinuousShareManager();
            return csManager.IsContinuous(userId, taskId);
        }
        #endregion
    }
}
