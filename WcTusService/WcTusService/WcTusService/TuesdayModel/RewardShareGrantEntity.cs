using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.TuesdayModel
{
    /// <summary>
    /// 奖品分享发放实体
    /// </summary>
    public class RewardShareGrantEntity
    {
        //用户实体字段
        tb_user user;

        public tb_user User
        {
            get { return user; }
            set { user = value; }
        }
        //用户分享实体类
        tb_userShare userShare;

        public tb_userShare UserShare
        {
            get { return userShare; }
            set { userShare = value; }
        }
        //奖品集合
        List<tb_reward> reward;

        public List<tb_reward> Reward
        {
            get { return reward; }
            set { reward = value; }
        }
        //分享类型
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        //奖励模板名称
        private string tmpName;

        public string TmpName
        {
            get { return tmpName; }
            set { tmpName = value; }
        }
    }
}