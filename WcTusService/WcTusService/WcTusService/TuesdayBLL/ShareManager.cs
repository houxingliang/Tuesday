using System;
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
        /// <param name="id">分项表主键ID</param>
        /// <returns></returns>
        public List<RewardShareGrantEntity> GetShareGrantListById(int id)
        {
            List<tb_userShare> userShareList = new UserShareData().GetUserShareListByShareID(id);

            if (userShareList != null)
            {
                List<RewardShareGrantEntity> grantList = new List<RewardShareGrantEntity>();
                foreach (tb_userShare userShare in userShareList)
                {
                    RewardShareGrantEntity grant = new RewardShareGrantEntity();
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
                            grant.TmpName = rewardTemplate.nvr_tmpName;
                            grant.Reward = rewardList;
                        }
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
                            grant.TmpName = rewardTemplate.nvr_tmpName;
                            grant.Reward = rewardList;
                        }
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
                            grant.TmpName = rewardTemplate.nvr_tmpName;
                            grant.Reward = rewardList;
                        }
                        grant.User = new UserData().GetUserByID((int)userShare.fk_superUser_id);
                    }
                    grantList.Add(grant);
                }
                return grantList;
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
                    userShare.bit_grantReward = true;
                    returnNum += userShareData.EditUserShare(userShare);
                }
            }
            return returnNum;
        }
    }
}