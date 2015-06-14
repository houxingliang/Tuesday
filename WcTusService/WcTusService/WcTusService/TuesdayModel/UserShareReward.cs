using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.TuesdayModel
{
    /// <summary>
    /// 用户分享活动和奖励信息的业务实体
    /// </summary>
    public class UserShareReward
    {
        tb_user user;

        public tb_user User
        {
            get { return user; }
            set { user = value; }
        }
        tb_share share;

        public tb_share Share
        {
            get { return share; }
            set { share = value; }
        }
        List<tb_reward> rewardList;

        public List<tb_reward> RewardList
        {
            get { return rewardList; }
            set { rewardList = value; }
        }
        /// <summary>
        /// 是否分享了该活动
        /// </summary>
        int isShare;

        public int IsShare
        {
            get { return isShare; }
            set { isShare = value; }
        }
    }
}