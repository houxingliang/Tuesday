using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WcTusService.TuesdayModel;

namespace WcTusService.Data
{
    /// <summary>
    /// 分享内容
    /// </summary>
    public class ShareData
    {
        ShareWeiEntities share = new ShareWeiEntities();
        /// <summary>
        /// 添加分享内容
        /// </summary>
        /// <param name="task"></param>
        public int Addshare(tb_share task)
        {
            share.tb_share.Add(task);
            return share.SaveChanges();
        }
        /// <summary>
        /// 更新分享内容
        /// </summary>
        /// <param name="task"></param>
        public int Editshare(tb_share task)
        {
            var ta = share.tb_share.Where(p => p.pk_share_id == task.pk_share_id);
            if (ta.Count() != 0)
            {
                share.Entry(task).State = EntityState.Modified;
                return share.SaveChanges();
            }
            else
                return 0;

        }
        /// <summary>
        /// 根据主键得到分享内容
        /// </summary>
        /// <param name="task"></param>
        public tb_share GetshareByid(int taskid)
        {
            var ta = from p in share.tb_share
                     where p.pk_share_id == taskid
                     select p;
            if (ta.Count() != 0)
            {
                return ta.FirstOrDefault();

            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到全部分享内容
        /// </summary>
        /// <param name="task"></param>
        public List<tb_share> GetshareAll(bool status)
        {
            if (status)
            {
                var ta = from p in share.tb_share
                         where p.bit_isDelete == false && p.bit_status==true
                             select p;
                if (ta.Count() != 0)
                {
                    return ta.ToList();

                }
                else
                {
                    return null;
                }
            }
            else
            {
                var ta = from p in share.tb_share
                         where p.bit_isDelete == false
                         select p;
                if (ta.Count() != 0)
                {
                    return ta.ToList();

                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取热门分享内容
        /// </summary>
        /// <returns></returns>
        public List<tb_share> GetHotShare()
        {
            var query = from p in share.tb_share
                        where p.bit_isDelete == false
                        orderby (p.int_firstShareTime + p.int_secondShareTime)
                        select p;
            if (query != null)
                return query.ToList();
            else
                return null;
        }
        /// <summary>
        /// 获取最新分享内容
        /// </summary>
        /// <returns></returns>
        public List<tb_share> GetNewShare()
        {
            var query = (from p in share.tb_share
                         where p.bit_isDelete==false
                        orderby p.dtm_createTime descending
                        select p).Take(10);
            if (query != null)
                return query.ToList();
            else
                return null;
        }
        /// <summary>
        /// 根据活动得到奖品明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<tb_reward> GetRewardByShareId(int id)
        {
            var query = from p in share.tb_share
                        where p.pk_share_id == id
                        select p;
            tb_share shareEntity = query.FirstOrDefault();
            if (shareEntity != null)
            {
                var rewardTmp = from p in share.tb_rewardTemplate
                                where p.pk_rewardTemplate_id == shareEntity.fk_rewardTemplate_id_f ||
                                p.pk_rewardTemplate_id == shareEntity.fk_rewardTemplate_id_s ||
                                p.pk_rewardTemplate_id == shareEntity.fk_superUser_rewardTmp_id
                                select p;
                List<tb_reward> rewardList = new List<tb_reward>();
                if (rewardTmp != null)
                {
                    List<tb_rewardTemplate> tmpList = new List<tb_rewardTemplate>();
                    tmpList = rewardTmp.ToList(); ;
                    if (tmpList != null)
                    {
                        foreach (var tmp in tmpList)
                        {
                            var query_imp = from p in share.tb_reward_Template_imp
                                            where p.fk_rewardTemplate_id == tmp.pk_rewardTemplate_id
                                            select p;
                            List<tb_reward_Template_imp> impList = query_imp.ToList();
                            if (impList != null)
                            {
                                foreach (tb_reward_Template_imp i in impList)
                                {
                                    var re = from p in share.tb_reward
                                             where p.pk_reward_id == i.fk_reward_id
                                             select p;
                                    tb_reward reward = re.FirstOrDefault();
                                    rewardList.Add(reward);
                                }
                            }

                        }
                    }
                }
                return rewardList;
            }
            else
            {
                return null;
            }
            
        }
    }
}