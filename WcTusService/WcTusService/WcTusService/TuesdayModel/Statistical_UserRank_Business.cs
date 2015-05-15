using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.TuesdayModel
{
    /// <summary>
    /// 用户奖品总数统计
    /// </summary>
    public class Statistical_UserRank_Business
    {
        //用户分享
        private tb_userShare userShare;

        public tb_userShare UserShare
        {
            get { return userShare; }
            set { userShare = value; }
        }
        //用户
        private tb_user user;

        public tb_user User
        {
            get { return user; }
            set { user = value; }
        }
        //奖品总数
        private double he;

        public double He
        {
            get { return he; }
            set { he = value; }
        }
        //奖品类型
        private string rewardType;

        public string RewardType
        {
            get { return rewardType; }
            set { rewardType = value; }
        }
    }
}