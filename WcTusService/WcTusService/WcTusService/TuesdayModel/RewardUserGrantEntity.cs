using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.TuesdayModel
{
    /// <summary>
    /// 用户的奖品发放业务实体
    /// </summary>
    public class RewardUserGrantEntity
    {
        private tb_user user;//用户

        public tb_user User
        {
            get { return user; }
            set { user = value; }
        }
        private List<tb_reward> reward;//奖品

        public List<tb_reward> Reward
        {
            get { return reward; }
            set { reward = value; }
        }
        private tb_taskExecute taskExecute;//任务执行情况

        public tb_taskExecute TaskExecute
        {
            get { return taskExecute; }
            set { taskExecute = value; }
        }
    }
}