using System;
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
        TaskData taskData = null;//任务数据访问类
        TaskItemData taskItemData = null;//任务项数据访问类
        ShareData shareData = null;//分享数据库访问类
        UserShareData userShareData = null;//用户分享数据访问类
        UserData userData = null;//用户数据访问类
        //活动首次转发统计
        public List<Statistical_UserShare_Business> FirstShare(int taskId)
        {
            tb_task task = new TaskData().GettaskByid(taskId);
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
                            tb_userShare userShare=userShareData.GetUserShareByID(i);
                            //是否是首次转发(首次转发将数据放入列表)
                            if (userShare.bit_firstShare == true)
                            {
                                userShareList.Add(userShare);
                                Statistical_UserShare_Business business = new Statistical_UserShare_Business();
                                business.UserShare = userShare;
                                tb_user user = userData.GetUserByID(userShare.fk_user_id);
                                if (user != null)
                                {
                                    business.User = user;
                                }
                                businessList.Add(business);
                            }
                            
                        }
                    }
                    return businessList;
                }
            }
            return null;
        }
        //总转发次数统计
        public List<Statistical_UserShare_Business> TotalShare(int taskId)
        {
            tb_task task = new TaskData().GettaskByid(taskId);
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
                                userShareList.Add(userShare);
                                Statistical_UserShare_Business business = new Statistical_UserShare_Business();
                                business.UserShare = userShare;
                                tb_user user = userData.GetUserByID(userShare.fk_user_id);
                                if (user != null)
                                {
                                    business.User = user;
                                }
                                businessList.Add(business);

                        }
                    }
                    return businessList;
                }
            }
            return null;
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
                                if (userShare.bit_firstShare == true)
                                {
                                    userShareList.Add(userShare);
                                    Statistical_UserShare_Business business = new Statistical_UserShare_Business();
                                    business.UserShare = userShare;
                                    tb_user user = userData.GetUserByID(userShare.fk_user_id);
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
                                    tb_user user = userData.GetUserByID(userShare.fk_user_id);
                                    if (user != null)
                                    {
                                        business.User = user;
                                    }
                                    businessList.Add(business);
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
            //根据用户分享表中的分享内容ID，查询所有的奖励模板ID
            if (userShareList != null)
            {
                foreach (tb_userShare u in userShareList)
                {
                    Statistical_UserRank_Business userRank = new Statistical_UserRank_Business();
                    userRank.UserShare = u;
                    userRank.User = new UserData().GetUserByID(u.fk_user_id);
                    //赋值奖品类型
                    tb_reward_Template_imp imp = new RewardTmpImpData().GetRewardImpByIdAndRewardId((int)u.fk_shareReward_id, RewardId);
                    userRank.RewardType = new RewardData().GetRewardByID(RewardId).nvr_rewardName;
                    rewardName=userRank.RewardType;
                    //赋值奖品数量
                    userRank.He = imp.dbl_count;
                    rankList.Add(userRank);

                }
            }
            //根据奖励模板，查询所有的奖励信息（名称和数量）
            var returnRank = from p in rankList
                             group p by p.User into g
                             orderby g.Count()
                             where g.Count() >= 1
                             select new
                             {
                                 sum =(from g1 in g
                                           select g1.He).Sum(),
                                 typeName=(from g1 in g
                                               select g1.RewardType),
                                 message = g.Key
                             };
            List<Statistical_UserRank_Business> returnRankList = new List<Statistical_UserRank_Business>();
            foreach (var temp in returnRank)
            {
                Statistical_UserRank_Business b = new Statistical_UserRank_Business();
                b.User = temp.message;
                b.He = temp.sum;
                b.RewardType = rewardName;
                returnRankList.Add(b);
            }
            return returnRankList;
        }

    }
}