using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.TuesdayModel
{
    /// <summary>
    /// 任务项和奖励信息的业务实体
    /// </summary>
    public class TaskItemReward
    {
        tb_taskItem taskItem;

        public tb_taskItem TaskItem
        {
            get { return taskItem; }
            set { taskItem = value; }
        }

        List<tb_reward> rewardList;

        public List<tb_reward> RewardList
        {
            get { return rewardList; }
            set { rewardList = value; }
        }

        tb_rewardTemplate rewardTmp;

        public tb_rewardTemplate RewardTmp
        {
            get { return rewardTmp; }
            set { rewardTmp = value; }
        }
    }
}