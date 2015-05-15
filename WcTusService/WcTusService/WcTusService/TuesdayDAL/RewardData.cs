using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WcTusService.TuesdayModel;

namespace WcTusService.Data
{
    
    /// <summary>
    /// 奖品数据访问类
    /// </summary>
    public class RewardData
    {
        //数据库模型
        ShareWeiEntities share = new ShareWeiEntities();
        /// <summary>
        /// 新增奖品信息
        /// </summary>
        /// <param name="reward">奖品实体</param>
        /// <returns>受影响行数</returns>
        public int AddReward(tb_reward reward)
        {
            if (reward != null)
            {
                share.tb_reward.Add(reward);
                int returnNum = share.SaveChanges();
                return returnNum;
            }
            else {
                return 0;
            }
            
        }
        /// <summary>
        /// 更新奖品信息
        /// </summary>
        /// <param name="reward"></param>
        /// <returns></returns>
        public int EditReward(tb_reward reward)
        {
            if (reward != null)
            {
                share.Entry(reward).State = EntityState.Modified;
                return share.SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据主键ID查询奖品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_reward GetRewardByID(int id)
        {
            var reward = from r in share.tb_reward
                         where r.pk_reward_id == id
                         select r;
            if (reward.ToList().Count > 0)
            {
                return reward.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据奖品名称查询奖品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_reward GetRewardByName(string name)
        {
            var reward = from r in share.tb_reward
                         where r.nvr_rewardName == name
                         select r;
            if (reward.ToList().Count > 0)
            {
                return reward.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取奖品集合
        /// </summary>
        /// <returns></returns>
        public List<tb_reward> GetRewardList()
        {
            var rewards = from r in share.tb_reward
                          select r;
            return rewards.ToList();
        }
    }
}