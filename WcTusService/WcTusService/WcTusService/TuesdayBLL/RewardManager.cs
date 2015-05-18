using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcTusService.Data;
using WcTusService.TuesdayModel;

namespace WcTusService.TuesdayBLL
{
    //奖品业务处理类
    public class RewardManager
    {
        RewardData rData = null;
        /// <summary>
        /// 新增奖品信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public int AddReward(tb_reward rd)
        { 
            rData=new RewardData();
            return rData.AddReward(rd);
        }
        /// <summary>
        /// 根据奖品名称查询奖品信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public tb_reward GetRewardByName(string name)
        {
            rData = new RewardData();
            return rData.GetRewardByName(name);
        }
        /// <summary>
        /// 根据奖品ID主键查询奖品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_reward GetRewardById(int id)
        {
            rData = new RewardData();
            return rData.GetRewardByID(id);
        }
        /// <summary>
        /// 更新奖品信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public int EditReward(tb_reward rd)
        {
            rData = new RewardData();
            return rData.EditReward(rd);
        }
        /// <summary>
        /// 查询奖品列表信息
        /// </summary>
        /// <returns></returns>
        public List<tb_reward> GetRewardList()
        {
            rData = new RewardData();
            return rData.GetRewardList();
        }

        public int DelRewardById(int id)
        {
            tb_reward reward = new RewardData().GetRewardByID(id);
            if (reward != null)
            {
                reward.bit_isDelete = true;
                return new RewardData().EditReward(reward);
            }
            else
                return 0;
        }
    }
}