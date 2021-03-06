﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcTusService.Data;
using WcTusService.TuesdayModel;

namespace WcTusService.TuesdayBLL
{
    /// <summary>
    /// 分享业务逻辑类
    /// </summary>
    public class ShareManager
    {
        ShareData sharedata = null;
        /// <summary>
        /// 新增分享信息
        /// </summary>
        /// <param name="tbshare"></param>
        /// <returns></returns>
        public int AddShare(tb_share tbshare)
        {
            sharedata = new ShareData();
            return sharedata.Addshare(tbshare);
        }
        /// <summary>
        /// 根据分享信息主键ID获取分享内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_share GetShareById(int id)
        {
            sharedata = new ShareData();
            tb_share ts = sharedata.GetshareByid(id);
            if (ts != null)
                return ts;
            else
                return null;
        }
        /// <summary>
        /// 根据分享主键ID
        /// 获取该分享内容下首次、二次、二次返还的奖品信息
        /// </summary>
        /// <param name="shareId"></param>
        /// <returns></returns>
        public List<RewardUserGrantEntity> GetRewardMessageByShareId(int shareId)
        {
            tb_share share = GetShareById(shareId);
            List<RewardUserGrantEntity> grantList = new List<RewardUserGrantEntity>();
            //首次分享
            RewardUserGrantEntity grant = new RewardUserGrantEntity();
            tb_rewardTemplate rewardTemplate = new RewardTemplateData().GetRewardTmpById(share.fk_rewardTemplate_id_f);
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
                grant.Share = new tb_share();
                grant.Share.dtm_createTime = DateTime.Now;
                grant.Share.nvr_shareName= share.nvr_shareName;
                grant.Share.pk_share_id = share.pk_share_id;
                grant.Share.nvr_shareContents = share.nvr_shareContents;
                grant.EntityType = 0;
                grant.TmpName = rewardTemplate.nvr_tmpName;
                grant.Reward = rewardList;
                grant.Type = "首次分享";
            }
            grantList.Add(grant);
            //是否允许二次分享
            if (share.bit_secondShare)
            {
                RewardUserGrantEntity grant_s = new RewardUserGrantEntity();
                tb_rewardTemplate rewardTemplate_s = new RewardTemplateData().GetRewardTmpById(share.fk_rewardTemplate_id_s);
                List<tb_reward> rewardList_s = new List<tb_reward>();
                //根据奖励模板获取奖品信息
                if (rewardTemplate_s != null)
                {
                    List<tb_reward_Template_imp> impList_s = new RewardTmpImpData().GetRewardImpList(rewardTemplate_s.pk_rewardTemplate_id);
                    foreach (tb_reward_Template_imp imp in impList_s)
                    {
                        tb_reward reward_s = new RewardData().GetRewardByID(imp.fk_reward_id);
                        reward_s.dbl_count = imp.dbl_count;
                        rewardList.Add(reward_s);
                    }
                    grant_s.Share = new tb_share();
                    grant_s.Share.dtm_createTime = DateTime.Now;
                    grant_s.Share.nvr_shareName = share.nvr_shareName;
                    grant.Share.nvr_shareContents = share.nvr_shareContents;
                    grant_s.Share.pk_share_id = share.pk_share_id;
                    grant_s.EntityType = 0;
                    grant_s.TmpName = rewardTemplate.nvr_tmpName;
                    grant_s.Reward = rewardList;
                    grant_s.Type = "二次分享";
                    grantList.Add(grant_s);
                }
                if (share.fk_superUser_rewardTmp_id != null && share.fk_superUser_rewardTmp_id != 0)
                {
                    RewardUserGrantEntity grant_super = new RewardUserGrantEntity();
                    tb_rewardTemplate rewardTemplate_super = new RewardTemplateData().GetRewardTmpById((int)share.fk_superUser_rewardTmp_id);
                    List<tb_reward> rewardList_super = new List<tb_reward>();
                    //根据奖励模板获取奖品信息
                    if (rewardTemplate_super != null)
                    {
                        List<tb_reward_Template_imp> impList_super = new RewardTmpImpData().GetRewardImpList(rewardTemplate_super.pk_rewardTemplate_id);
                        foreach (tb_reward_Template_imp imp in impList_super)
                        {
                            tb_reward reward_s = new RewardData().GetRewardByID(imp.fk_reward_id);
                            reward_s.dbl_count = imp.dbl_count;
                            rewardList.Add(reward_s);
                        }
                        grant_super.Share = new tb_share();
                        grant_super.Share.dtm_createTime = DateTime.Now;
                        grant_super.Share.nvr_shareName = share.nvr_shareName;
                        grant.Share.nvr_shareContents = share.nvr_shareContents;
                        grant_super.Share.pk_share_id = share.pk_share_id;
                        grant_super.EntityType = 0;
                        grant_super.TmpName = rewardTemplate.nvr_tmpName;
                        grant_super.Reward = rewardList;
                        grant_super.Type = "二次返还";
                        grantList.Add(grant_super);
                    }
                }
            }
            return grantList;
        }
        /// <summary>
        /// 修改分享内容信息
        /// </summary>
        /// <param name="tbshare"></param>
        /// <returns></returns>
        public int EditShare(tb_share tbshare)
        {
            int returnNum = 0;
            sharedata=new ShareData();
            if (tbshare != null)
                return sharedata.Editshare(tbshare);
            else
                return returnNum;
        }
        /// <summary>
        /// 删除分享内容信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelShare(int id)
        {
            int returnNum = 0;
            sharedata = new ShareData();
            tb_share tbshare = sharedata.GetshareByid(id);
            if(tbshare!=null)
            {
                tbshare.bit_isDelete = true;
                return returnNum = sharedata.Editshare(tbshare);
            }
            else
                return returnNum;
        }
        /// <summary>
        /// 获取分享内容列表信息
        /// </summary>
        /// <returns></returns>
        public List<tb_share> GetShareList(bool status)
        {
            sharedata = new ShareData();
            List<tb_share> shareList = sharedata.GetshareAll(status);
            if (shareList != null)
                return shareList;
            else
                return null;
        }
        /// <summary>
        /// 根据活动名称和时间段查询
        /// 分享列表
        /// </summary>
        /// <param name="name">活动名称</param>
        /// <param name="actionDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public List<tb_share> GetShareList(string name, DateTime actionDate, DateTime endDate)
        {
            sharedata = new ShareData();
            return sharedata.getShareList(name, actionDate, endDate);
        }
        /// <summary>
        /// 获取热门分享内容
        /// </summary>
        /// <returns></returns>
        public List<tb_share> GetHotShare()
        {
            sharedata = new ShareData();
            return sharedata.GetHotShare();
        }
        /// <summary>
        /// 获取最新分享内容
        /// </summary>
        /// <returns></returns>
        public List<tb_share> GetNewShare()
        {
            sharedata = new ShareData();
            return sharedata.GetNewShare();
        }
        /// <summary>
        /// 根据活动得到奖品明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns>奖品明细</returns>
        public List<tb_reward> GetRewardByShareId(int id)
        {
            sharedata = new ShareData();
            return sharedata.GetRewardByShareId(id);
        }
        /// <summary>
        /// 根据分享主键ID获取用户的奖品信息
        /// </summary>
        /// <param name="id">分享表主键ID</param>
        /// <returns></returns>
        public List<RewardUserGrantEntity> GetShareGrantListById(int id,bool isApply,bool isGrant)
        {
            List<tb_userShare> userShareList = new UserShareData().GetUserShareListByShareID(id);

            if (userShareList != null)
            {
                List<RewardUserGrantEntity> grantList = new List<RewardUserGrantEntity>();
                foreach (tb_userShare userShare in userShareList)
                {
                    RewardUserGrantEntity grant = new RewardUserGrantEntity();
                    grant.UserShare = userShare;
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
                                reward.dbl_count = imp.dbl_count;
                                rewardList.Add(reward);
                            }
                            grant.Task = new tb_task();
                            grant.Task.dtm_createTime = DateTime.Now;
                            grant.Task.dtm_actionTime = DateTime.Now;
                            grant.Task.dtm_endTime = DateTime.Now;
                            grant.Task.nvr_taskName = share.nvr_shareName;
                            grant.Task.pk_task_id = share.pk_share_id;
                            grant.EntityType = 0;
                            grant.TmpName = rewardTemplate.nvr_tmpName;
                            grant.Reward = rewardList;
                        }
                        grant.UserShare = userShare;
                        grant.User = new UserData().GetUserByID((int)userShare.fk_user_id);
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
                                reward.dbl_count = imp.dbl_count;
                                rewardList.Add(reward);
                            }

                            grant.Task = new tb_task();
                            grant.Task.dtm_createTime = DateTime.Now;
                            grant.Task.dtm_actionTime = DateTime.Now;
                            grant.Task.dtm_endTime = DateTime.Now;
                            grant.Task.nvr_taskName = share.nvr_shareName;
                            grant.Task.pk_task_id = share.pk_share_id;
                            grant.EntityType = 0;
                            grant.TmpName = rewardTemplate.nvr_tmpName;
                            grant.Reward = rewardList;
                        }
                        grant.UserShare = userShare;
                        grant.User = new UserData().GetUserByID((int)userShare.fk_user_id);
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
                                reward.dbl_count = imp.dbl_count;
                                rewardList.Add(reward);
                            }
                            grant.Task = new tb_task();
                            grant.Task.dtm_createTime = DateTime.Now;
                            grant.Task.dtm_actionTime = DateTime.Now;
                            grant.Task.dtm_endTime = DateTime.Now;
                            grant.Task.nvr_taskName = share.nvr_shareName;
                            grant.Task.pk_task_id = share.pk_share_id;
                            grant.EntityType = 0;
                            grant.TmpName = rewardTemplate.nvr_tmpName;
                            grant.Reward = rewardList;
                        }
                        grant.UserShare = userShare;
                        grant.User = new UserData().GetUserByID((int)userShare.fk_superUser_id);
                    }
                    grantList.Add(grant);
                }
                if (grantList != null)
                {
                    var query = from p in grantList
                                where p.UserShare.bit_isApply == isApply &&
                                p.UserShare.bit_isGrant == isGrant
                                select p;
                    return query.ToList();
                }
                
            }
            return null;
        }
        /// <summary>
        /// 根据用户分享表主键，发放奖励
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public int FafangShare(List<int> idList)
        {
            int returnNum = 0;
            if (idList != null)
            {
                UserShareData userShareData = new UserShareData();
                foreach (int i in idList)
                {
                    tb_userShare userShare = userShareData.GetUserShareByID(i);
                    userShare.bit_isGrant = true;
                    returnNum += userShareData.EditUserShare(userShare);
                }
            }
            return returnNum;
        }
        /// <summary>
        /// 用户申请分享奖励
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public int ShenQingShare(List<int> idList)
        {
            int returnNum = 0;
            if (idList != null)
            {
                UserShareData userShareData = new UserShareData();
                foreach (int i in idList)
                {
                    tb_userShare userShare = userShareData.GetUserShareByID(i);
                    userShare.bit_isApply = true;
                    returnNum += userShareData.EditUserShare(userShare);
                }
            }
            return returnNum;
        }
        /// <summary>
        /// 根据用户ID获取该用户所有的分享信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<UserShareReward> GetUserShareList(int userid)
        {
            List<tb_share> shareList = new ShareData().GetshareAll(false);
            if (shareList != null)
            {
                List<UserShareReward> shareRewardList = new List<UserShareReward>();
                tb_user user = new UserData().GetUserByID(userid);
                List<tb_userShare> userShareList = new UserShareData().GetUserShareListByUserId(userid);
                foreach (tb_share share in shareList)
                {
                    UserShareReward r = new UserShareReward();
                    r.User = user;
                    r.Share = share;
                    List<tb_reward_Template_imp> impList = new RewardTmpImpData().GetRewardImpList(share.fk_rewardTemplate_id_f);
                    if (impList != null)
                    {
                        List<tb_reward> rewardList = new List<tb_reward>();
                        foreach (tb_reward_Template_imp imp in impList)
                        {
                            tb_reward reward = new RewardData().GetRewardByID(imp.fk_reward_id);
                            reward.dbl_count = imp.dbl_count;
                            rewardList.Add(reward);
                        }
                        r.RewardList = rewardList;
                        r.IsShare = 0;
                    }
                    r.RewardTmp = new RewardTemplateData().GetRewardTmpById(share.fk_rewardTemplate_id_f);
                    foreach (tb_userShare userShare in userShareList)
                    {
                        if (userShare.fk_shareContents_id == share.pk_share_id)
                        {
                            r.IsShare = 1;
                            break;
                        }
                    }
                    shareRewardList.Add(r);
                }
                return shareRewardList;
            }
            return null;
        }
        /// <summary>
        /// 根据用户主键ID
        /// 获取用户的奖励信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RewardUserGrantEntity> GetUserRewardByUserId(int userId)
        {
            tb_user user = new UserData().GetUserByID(userId);
            if (user != null)
                return new TaskExecuteManager().GetTaskExecuteByUser(user.nvr_wxName, user.nvr_userName, user.vr_phoneNum);
            else
                return null;
        }
    }
}