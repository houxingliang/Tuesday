﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcTusService.Data;
using WcTusService.TuesdayModel;

namespace WcTusService.TuesdayBLL
{
    /// <summary>
    /// 统计报表业务逻辑类
    /// </summary>
    public class StatisticalManager
    {
        UserShareData userShareData = null;//用户分享数据访问类
        UserData userData = null;//用户数据访问类
        /// <summary>
        /// 活动首次转发统计
        /// </summary>
        /// <param name="shareId">分享内容ID</param>
        /// <returns></returns>
        public List<Statistical_UserShare_Business> FirstShare(int shareId)
        {
            //根据活动ID获取分享后的活动列表
            List<tb_userShare> userShareList = new UserShareData().GetUserShareListByShareID(shareId);

            List<Statistical_UserShare_Business> busList = new List<Statistical_UserShare_Business>();
            foreach (tb_userShare userShare in userShareList)
            {
                Statistical_UserShare_Business b = new Statistical_UserShare_Business();
                if (userShare.bit_firstShare)
                {
                    b.User = new UserData().GetUserByID((int)userShare.fk_user_id);
                }               
                b.UserShare = userShare;
                busList.Add(b);
            }
            return busList;
        }
        /// <summary>
        /// 总转发次数统计
        /// </summary>
        /// <param name="shareId">分享活动主键ID</param>
        /// <returns>分享转发 业务实体</returns>
        public List<Statistical_UserShare_Business> TotalShare(int shareId)
        {
            //根据活动ID获取分享后的活动列表
            List<tb_userShare> userShareList = new UserShareData().GetUserShareListByShareID(shareId);

            List<Statistical_UserShare_Business> busList = new List<Statistical_UserShare_Business>();
            foreach (tb_userShare userShare in userShareList)
            {
                Statistical_UserShare_Business b = new Statistical_UserShare_Business();
                if (userShare.bit_firstShare)
                {
                    b.User = new UserData().GetUserByID((int)userShare.fk_user_id);
                }
                else if (!userShare.bit_firstShare && userShare.fk_user_id != null)
                {
                    b.User = new UserData().GetUserByID((int)userShare.fk_user_id);
                }
                 //二次分享返还
                else if (!userShare.bit_firstShare && userShare.fk_user_id == null && userShare.fk_superUser_id != null)
                {
                    b.User = new UserData().GetUserByID((int)userShare.fk_user_id);
                }
                b.UserShare = userShare;
                busList.Add(b);
            }
            return busList;
        }
        /// <summary>
        /// 活动用户首次转发排名统计
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<Statistical_Rank_business> FirstRank(List<int> taskId)
        {


            List<Statistical_Rank_business> rankList = new List<Statistical_Rank_business>();
            foreach (int tid in taskId)
            {
                tb_task task = new TaskData().GettaskByid(tid);
                if (task != null)
                {
                    //获取任务的所有任务项信息
                    List<tb_taskItem> taskItemList = new TaskItemData().GetItemBytaskid(task.pk_task_id);
                    List<int> shareIdList = new List<int>();//分享内容ID集合
                    if (taskItemList != null)
                    {
                        foreach (tb_taskItem taskItem in taskItemList)
                        {
                            shareIdList.Add(taskItem.fk_share_id);
                        }
                        //首次分享的业务实体类
                        List<Statistical_UserShare_Business> businessList = new List<Statistical_UserShare_Business>();
                        shareIdList.Add(task.fk_share_id);
                        shareIdList = shareIdList.Distinct().ToList();//去除集合中的重复数据
                        userShareData = new UserShareData();
                        if (shareIdList != null && shareIdList.Count > 0)
                        {
                            List<tb_userShare> userShareList = new List<tb_userShare>();
                            foreach (int i in shareIdList)
                            {
                                tb_userShare userShare = userShareData.GetUserShareByID(i);
                                //是否是首次转发(首次转发将数据放入列表)
                                
                                if (userShare!=null&&userShare.bit_firstShare == true)
                                {
                                    userShareList.Add(userShare);
                                    Statistical_UserShare_Business business = new Statistical_UserShare_Business();
                                    business.UserShare = userShare;
                                    tb_user user = userData.GetUserByID((int)userShare.fk_user_id);
                                    if (user != null)
                                    {
                                        business.User = user;
                                    }
                                    businessList.Add(business);
                                }

                            }
                        }
                        //根据用户进行分组，获取每个用户的首次分享次数
                        Statistical_Rank_business rankBusiness = new Statistical_Rank_business();
                        var query = from p in businessList
                                    group p by p.User into g
                                    orderby g.Count()
                                    where g.Count() >= 1
                                    select new
                                    {
                                        Count = g.Count(),
                                        User=g.Key
                                    };
                        foreach (var item in query)
                        {
                            rankBusiness.Count = item.Count;
                            rankBusiness.User = item.User;
                            rankList.Add(rankBusiness);
                        }
                    }
                }
            }
            return rankList;
        }
        
         /// <summary>
        /// 活动用户总转排名统计
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<Statistical_Rank_business> TotalRank(List<int> taskId)
        {
            List<Statistical_Rank_business> rankList = new List<Statistical_Rank_business>();
            foreach (int tid in taskId)
            {
                tb_task task = new TaskData().GettaskByid(tid);
                if (task != null)
                {
                    //获取任务的所有任务项信息
                    List<tb_taskItem> taskItemList = new TaskItemData().GetItemBytaskid(task.pk_task_id);
                    List<int> shareIdList = new List<int>();//分享内容ID集合
                    if (taskItemList != null)
                    {
                        foreach (tb_taskItem taskItem in taskItemList)
                        {
                            shareIdList.Add(taskItem.fk_share_id);
                        }
                        //首次分享的业务实体类
                        List<Statistical_UserShare_Business> businessList = new List<Statistical_UserShare_Business>();
                        shareIdList.Add(task.fk_share_id);
                        shareIdList = shareIdList.Distinct().ToList();//去除集合中的重复数据
                        userShareData = new UserShareData();
                        if (shareIdList != null && shareIdList.Count > 0)
                        {
                            List<tb_userShare> userShareList = new List<tb_userShare>();
                            foreach (int i in shareIdList)
                            {
                                tb_userShare userShare = userShareData.GetUserShareByID(i);
                                userShareList.Add(userShare);
                                Statistical_UserShare_Business business = new Statistical_UserShare_Business();
                                business.UserShare = userShare;
                                if (userShare != null)
                                {
                                    tb_user user = userData.GetUserByID((int)userShare.fk_user_id);
                                    if (user != null)
                                    {
                                        business.User = user;
                                    }
                                    businessList.Add(business);
                                }
                            }
                        }
                        //根据用户进行分组，获取每个用户的首次分享次数
                        Statistical_Rank_business rankBusiness = new Statistical_Rank_business();
                        var query = from p in businessList
                                    group p by p.User into g
                                    orderby g.Count()
                                    where g.Count() >= 1
                                    select new
                                    {
                                        Count = g.Count(),
                                        User = g.Key
                                    };
                        foreach (var item in query)
                        {
                            rankBusiness.Count = item.Count;
                            rankBusiness.User = item.User;
                            rankList.Add(rankBusiness);
                        }
                    }
                }
            }
            return rankList;
        }
        //用户奖品总数统计
        public List<Statistical_UserRank_Business> UserRewardSum(DateTime actionTime, DateTime endTime, int RewardId)
        {
            List<Statistical_UserRank_Business> rankList = new List<Statistical_UserRank_Business>();
            string rewardName="";
            //获取指定时间段内，用户分享的数据集
            userShareData = new UserShareData();
            List<tb_userShare> userShareList = userShareData.GetUserShareListByTime(actionTime, endTime);
            if (userShareList != null)
            {
                foreach (tb_userShare userShare in userShareList)
                {
                    Statistical_UserRank_Business grant = new Statistical_UserRank_Business();
                    //首次分享
                    if (userShare.bit_firstShare)
                    {
                        tb_share share = new ShareData().GetshareByid(userShare.fk_shareContents_id);
                        tb_rewardTemplate rewardTemplate = new RewardTemplateData().GetRewardTmpById(share.fk_rewardTemplate_id_f);
                        //根据奖励模板获取奖品信息
                        if (rewardTemplate != null)
                        {
                            List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rewardTemplate.pk_rewardTemplate_id);
                            foreach (tb_reward_Template_imp imp in impList)
                            {
                                if (imp.fk_reward_id == RewardId)
                                {
                                    grant.He += imp.dbl_count;
                                }
                            }
                        }
                        if (grant.He> 0)
                        {
                            grant.UserShare = userShare;
                            grant.User = new UserData().GetUserByID((int)userShare.fk_user_id);
                            rankList.Add(grant);
                        }
                    }
                    //二次分享
                    else if (!userShare.bit_firstShare && userShare.fk_user_id != null)
                    {
                        tb_share share = new ShareData().GetshareByid(userShare.fk_shareContents_id);
                        tb_rewardTemplate rewardTemplate = new RewardTemplateData().GetRewardTmpById(share.fk_rewardTemplate_id_s);
                        //根据奖励模板获取奖品信息
                        if (rewardTemplate != null)
                        {
                            List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rewardTemplate.pk_rewardTemplate_id);
                            foreach (tb_reward_Template_imp imp in impList)
                            {
                                if (imp.fk_reward_id == RewardId)
                                {
                                    grant.He += imp.dbl_count;
                                }
                            }
                        }
                        if (grant.He > 0)
                        {
                            grant.UserShare = userShare;
                            grant.User = new UserData().GetUserByID((int)userShare.fk_user_id);
                            rankList.Add(grant);
                        }
                    }
                    //二次分享返还
                    else if (!userShare.bit_firstShare && userShare.fk_user_id == null && userShare.fk_superUser_id != null)
                    {
                        tb_share share = new ShareData().GetshareByid(userShare.fk_shareContents_id);
                        tb_rewardTemplate rewardTemplate = new RewardTemplateData().GetRewardTmpById((int)share.fk_superUser_rewardTmp_id);
                        //根据奖励模板获取奖品信息
                        if (rewardTemplate != null)
                        {
                            List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rewardTemplate.pk_rewardTemplate_id);
                            foreach (tb_reward_Template_imp imp in impList)
                            {
                                if (imp.fk_reward_id == RewardId)
                                {
                                    grant.He += imp.dbl_count;
                                }
                            }
                        }
                        if (grant.He > 0)
                        {
                            grant.UserShare = userShare;
                            grant.User = new UserData().GetUserByID((int)userShare.fk_user_id);
                            rankList.Add(grant);
                        }
                    }
                }
            }
            //获取指定时间段内，任务执行的数据集
            List<tb_taskExecute> taskExecuteList = new TaskExecuteData().GetTaskExecuteByTime(actionTime, endTime);
            if (taskExecuteList != null)
            {
                foreach (tb_taskExecute taskExecute in taskExecuteList)
                {
                    Statistical_UserRank_Business grant = new Statistical_UserRank_Business();
                    tb_taskItem taskItem = new TaskItemData().GetItemByid(taskExecute.fk_taskItem_id);
                    //通过奖励模板ID获取奖励模板信息
                    tb_rewardTemplate rewardTemplate = new RewardTemplateData().GetRewardTmpById(taskItem.fk_rewardTemplate_id);
                    //根据奖励模板获取奖品信息
                    if (rewardTemplate != null)
                    {
                        List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rewardTemplate.pk_rewardTemplate_id);
                        foreach (tb_reward_Template_imp imp in impList)
                        {
                            if (imp.fk_reward_id == RewardId)
                            {
                                grant.He += imp.dbl_count;
                            }
                        }
                    }
                    if (grant.He > 0)
                    {
                        grant.User = new UserData().GetUserByID((int)taskExecute.fk_user_id);
                        rankList.Add(grant);
                    }
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------------------

            //根据奖励模板，查询所有的奖励信息（名称和数量）
            var returnRank = from p in rankList
                             group p by p.User into g
                             orderby g.Count()
                             where g.Count() >= 1
                             select new
                             {
                                 sum =(from g1 in g
                                           select g1.He).Sum(),
                                 message = g.Key
                             };
            List<Statistical_UserRank_Business> returnRankList = new List<Statistical_UserRank_Business>();
            foreach (var temp in returnRank)
            {
                Statistical_UserRank_Business b = new Statistical_UserRank_Business();
                b.User = temp.message;
                b.He = temp.sum;
                returnRankList.Add(b);
            }
            return returnRankList;
        }
        /// <summary>
        /// 获取糖币明细信息
        /// </summary>
        /// <param name="actionTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<TangbiDetail> GetTangbiDetal(DateTime actionTime,DateTime endTime)
        {
            //建立糖币明细集合
            List<TangbiDetail> tangbiList = new List<TangbiDetail>();
            //获取用户分享表的所有有效数据
            userShareData = new UserShareData();
            userData=new UserData();
            List<tb_userShare> userShareList = userShareData.GetUserShareList();
            if (userShareList != null)
            {
                var temp = from p in userShareList
                           where p.dtm_shareTime >= actionTime &&
                           p.dtm_shareTime <= endTime
                           select p;
                foreach (var userShare in temp)
                {
                    TangbiDetail tangbiDetail = new TangbiDetail();
                    //糖币明细中的用户信息
                    tangbiDetail.User = userData.GetUserByID((int)userShare.fk_user_id);
                    //糖币明细中的分享时间
                    tangbiDetail.ShareTime = userShare.dtm_shareTime;
                    //糖币明细中的活动名称
                    tb_share share = new ShareData().GetshareByid(userShare.fk_shareContents_id);
                    if (share != null)
                    {
                        tangbiDetail.Name = share.nvr_shareName;
                    }
                    //如果用户是首次分享
                    if (userShare.bit_firstShare)
                    {
                        tangbiDetail.ShareType = "首次分享";
                        tb_rewardTemplate rt = new tb_rewardTemplate();
                        rt = new RewardTemplateData().GetRewardTmpById(share.fk_rewardTemplate_id_f);
                        if (rt != null)
                        {
                            List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rt.pk_rewardTemplate_id);
                            if (impList != null)
                            {
                                foreach (var imp in impList)
                                {
                                    tb_reward reward = new RewardData().GetRewardByID(imp.fk_reward_id);
                                    if (reward.nvr_rewardName.Equals("糖币"))
                                    {
                                        tangbiDetail.Sum = imp.dbl_count;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        tangbiDetail.ShareType = "二次分享";
                        tb_rewardTemplate rt = new tb_rewardTemplate();
                        rt = new RewardTemplateData().GetRewardTmpById(share.fk_rewardTemplate_id_s);
                        if (rt != null)
                        {
                            List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rt.pk_rewardTemplate_id);
                            if (impList != null)
                            {
                                foreach (var imp in impList)
                                {
                                    tb_reward reward = new RewardData().GetRewardByID(imp.fk_reward_id);
                                    if (reward.nvr_rewardName.Equals("糖币"))
                                    {
                                        tangbiDetail.Sum = imp.dbl_count;
                                    }
                                }
                            }
                        }
                    }
                    if (userShare.fk_superUser_id != 0&&userShare.fk_superUser_id!=null
                        && share.fk_superUser_rewardTmp_id != null)
                    {
                        TangbiDetail tangbiDetailSuper = new TangbiDetail();
                        tangbiDetailSuper.User =userData.GetUserByID((int)userShare.fk_superUser_id);
                        tangbiDetailSuper.ShareType = "二次返还";
                        tangbiDetailSuper.ShareTime = userShare.dtm_shareTime;
                        tangbiDetailSuper.Name = tangbiDetail.Name;

                        tb_rewardTemplate rt = new tb_rewardTemplate();
                        rt = new RewardTemplateData().GetRewardTmpById((int)share.fk_superUser_rewardTmp_id);
                        if (rt != null)
                        {
                            List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rt.pk_rewardTemplate_id);
                            if (impList != null)
                            {
                                foreach (var imp in impList)
                                {
                                    tb_reward reward = new RewardData().GetRewardByID(imp.fk_reward_id);
                                    if (reward.nvr_rewardName.Equals("糖币"))
                                    {
                                        tangbiDetailSuper.Sum = imp.dbl_count;
                                    }
                                }
                            }
                        }
                        tangbiList.Add(tangbiDetailSuper);
                    }
                    tangbiList.Add(tangbiDetail);
                }
            }
            //获取任务执行表的所有有效数据
            List<tb_taskExecute> taskExecuteList = new TaskExecuteData().GetRewardTmpList();
            if (taskExecuteList != null)
            {
                var temp = from p in taskExecuteList
                           where p.dtm_executeTime >= actionTime &&
                           p.dtm_executeTime <= endTime
                           select p;
                foreach (var taskExecute in temp)
                {
                    TangbiDetail tangbiDetail = new TangbiDetail();
                    tangbiDetail.ShareType = "任务奖励";
                    tangbiDetail.ShareTime = taskExecute.dtm_executeTime;
                    tangbiDetail.User = new UserData().GetUserByID(taskExecute.fk_user_id);
                    //任务执行名称
                    tb_taskItem taskItem = new TaskItemData().GetItemByid(taskExecute.fk_taskItem_id);
                    if (taskItem != null)
                    {
                        tb_rewardTemplate rt = new tb_rewardTemplate();
                        rt = new RewardTemplateData().GetRewardTmpById((int)taskItem.fk_rewardTemplate_id);
                        if (rt != null)
                        {
                            List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(rt.pk_rewardTemplate_id);
                            if (impList != null)
                            {
                                foreach (var imp in impList)
                                {
                                    tb_reward reward = new RewardData().GetRewardByID(imp.fk_reward_id);
                                    if (reward.nvr_rewardName.Equals("糖币"))
                                    {
                                        tangbiDetail.Sum = imp.dbl_count;
                                    }
                                }
                            }
                        }
                        tangbiDetail.Name = new TaskData().GettaskByid(taskItem.fk_task_id).nvr_taskName;
                    }
                    tangbiList.Add(tangbiDetail);
                }
            }
            return tangbiList;
        }
    }
}