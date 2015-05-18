using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WcTusService.TuesdayModel;

namespace WcTusService.Data
{
    /// <summary>
    /// 奖品模板_奖品关联表
    /// </summary>
    public class RewardTmpImpData
    {
        //数据库模型
        ShareWeiEntities share = new ShareWeiEntities();
        /// <summary>
        /// 新增奖品模板关联信息
        /// </summary>
        /// <param name="rewardImp">奖品模板关联</param>
        /// <returns>受影响行数</returns>
        public int AddRewardImp(tb_reward_Template_imp rewardImp)
        {
            if (rewardImp != null)
            {
                share.tb_reward_Template_imp.Add(rewardImp);
                int returnNum = share.SaveChanges();
                return returnNum;
            }
            else
            {
                return 0;
            }

        }
        /// <summary>
        /// 更新奖品模板关联信息
        /// </summary>
        /// <param name="rewardImp"></param>
        /// <returns></returns>
        public int EditReward(tb_reward_Template_imp rewardImp)
        {
            if (rewardImp != null)
            {
                var query = from p in share.tb_reward_Template_imp
                            where p.pk_imp_id == rewardImp.pk_imp_id
                            select p;
                tb_reward_Template_imp temp = query.First();
                temp.bit_isDelete = rewardImp.bit_isDelete;
                temp.dbl_count = rewardImp.dbl_count;
                temp.fk_reward_id = rewardImp.fk_reward_id;
                temp.fk_rewardTemplate_id = rewardImp.fk_rewardTemplate_id;
                
                share.Entry(temp).State = EntityState.Modified;
                return share.SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据主键ID查询奖品模板关联信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_reward_Template_imp GetRewardImpByID(int id)
        {
            var rewardImp = from r in share.tb_reward_Template_imp
                         where r.pk_imp_id == id
                         select r;
            if (rewardImp.ToList().Count > 0)
            {
                return rewardImp.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据主键ID和奖品类型ID
        /// 查询奖品模板关联信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_reward_Template_imp GetRewardImpByIdAndRewardId(int id,int rewardId)
        {
            var rewardImp = from r in share.tb_reward_Template_imp
                            where r.pk_imp_id == id &&
                            r.fk_reward_id==rewardId
                            select r;
            if (rewardImp.ToList().Count > 0)
            {
                return rewardImp.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据奖品模板ID
        /// 获取奖品模板奖品集合
        /// </summary>
        /// <returns></returns>
        public List<tb_reward_Template_imp> GetRewardImpList(int tmpID)
        {
            var rewardImps = from r in share.tb_reward_Template_imp
                             where r.fk_rewardTemplate_id==tmpID
                          select r;
            return rewardImps.ToList();
        }
        /// <summary>
        /// 删除关联表信息
        /// </summary>
        /// <param name="rewardId">奖品ID</param>
        /// <param name="tmpId">模板ID</param>
        /// <returns></returns>
        public int DelRewardImp(int rewardId,int tmpId)
        {
            var rewardImps = from r in share.tb_reward_Template_imp
                             where r.fk_rewardTemplate_id == tmpId&&
                             r.fk_reward_id==rewardId
                             select r;
            share.tb_reward_Template_imp.Remove(rewardImps.FirstOrDefault());
            int returnNum=share.SaveChanges();
            return returnNum;

        }
    }
}