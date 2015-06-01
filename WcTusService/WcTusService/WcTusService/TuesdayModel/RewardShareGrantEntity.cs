using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.TuesdayModel
{
    /// <summary>
    /// 
    /// </summary>
    public class RewardShareGrantEntity
    {
        tb_user user;

        public tb_user User
        {
            get { return user; }
            set { user = value; }
        }
        tb_userShare userShare;

        public tb_userShare UserShare
        {
            get { return userShare; }
            set { userShare = value; }
        }
        List<tb_reward> reward;

        public List<tb_reward> Reward
        {
            get { return reward; }
            set { reward = value; }
        }
    }
}