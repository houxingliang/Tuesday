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

        private tb_userShare userShare;//用户分享情况

        public tb_userShare UserShare
        {
            get { return userShare; }
            set { userShare = value; }
        }

        private string type;//类型(首次分享、二次分享、二次返还、任务执行)

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private tb_task task;

        public tb_task Task
        {
            get { return task; }
            set { task = value; }
        }

        private tb_share share;

        public tb_share Share
        {
            get { return share; }
            set { share = value; }
        }
        private int entityType;//0代表分享，1代表任务

        public int EntityType
        {
            get { return entityType; }
            set { entityType = value; }
        }

        private string tmpName;//奖励模板名称

        public string TmpName
        {
            get { return tmpName; }
            set { tmpName = value; }
        }
    }
}