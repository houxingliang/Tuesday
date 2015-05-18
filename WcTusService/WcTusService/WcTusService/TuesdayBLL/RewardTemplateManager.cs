using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcTusService.Data;
using WcTusService.TuesdayModel;

namespace WcTusService.TuesdayBLL
{
    /// <summary>
    /// 奖品模板业务处理类
    /// </summary>
    public class RewardTemplateManager
    {
        RewardTemplateData rtd = null;//奖品模板数据访问类
        RewardTmpImpData rtid = null;//奖品模板_奖品关联数据访问类
        /// <summary>
        /// 新增奖品模板信息
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        public int AddRewardTemplate(RewardTemplate rt)
        { 
            //定义返回值
            int returnNum = 0;
            rtd=new RewardTemplateData();
            rtid = new RewardTmpImpData();
            if (rt != null)
            {
                returnNum += rtd.AddRewardTmp(rt.RewardTmp);
                if (rt.RewardTemplateImp != null && rt.RewardTemplateImp.Count > 0)
                {
                    for (int i = 0; i < rt.RewardTemplateImp.Count; i++)
                    {
                        returnNum += rtid.AddRewardImp(rt.RewardTemplateImp[i]);
                    }
                }
                return returnNum;
            }
            return 0;
        }
        /// <summary>
        /// 修改奖励模板信息
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        public int EditRewardTemplate(RewardTemplate rt)
        { 
            //定义返回值
            int returnNum = 0;
            //保存奖品模板信息
            rtd = new RewardTemplateData();
            returnNum += rtd.EditRewardTmp(rt.RewardTmp);
            //对比关联表信息
            //获取原有信息
            rtid = new RewardTmpImpData();
            List<tb_reward_Template_imp> oldImp = rtid.GetRewardImpList(rt.RewardTmp.pk_rewardTemplate_id);
            List<int> oldStr = new List<int>();
            List<int> newStr=new List<int>();
            foreach (tb_reward_Template_imp i in oldImp)
            {
                oldStr.Add(i.fk_reward_id);
            }
            foreach (tb_reward_Template_imp i in rt.RewardTemplateImp)
            {
                newStr.Add(i.fk_reward_id);
            }
            //删除原有表中有而传入实体没有的关联表数据
            List<int> tempOld=new List<int>() ;
            if (oldStr != null)
            {
                foreach (int i in oldStr)
                {
                    tempOld.Add(i);
                }
            }
            foreach (int it in newStr)
            {
                tempOld.Remove(it);
            }
            if (tempOld != null && tempOld.Count > 0)
            {
                foreach (int i in tempOld)
                {
                    returnNum += rtid.DelRewardImp(i,rt.RewardTmp.pk_rewardTemplate_id);
                }
            }
            //新增传入实体中有而原有关联表中没有的数据
            List<int> newTemp = new List<int>();
            if (newStr != null)
            {
                foreach (int i in newStr)
                {
                    newTemp.Add(i);
                }
            }
            foreach (int it in oldStr)
            {
                newTemp.Remove(it);
            }
            if (newTemp != null && newTemp.Count > 0)
            {
                foreach (int i in newTemp)
                {
                    foreach (tb_reward_Template_imp trti in rt.RewardTemplateImp)
                    {
                        if (trti.fk_reward_id == i)
                        {
                            returnNum += rtid.AddRewardImp(trti);
                        }
                    }
                }
            }
            //更新传入实体和原有关联表中都有的数据
            var sameID = from l1 in oldStr
                         from l2 in newStr
                         where l1 == l2
                         select l1;
            foreach (var i in sameID)
            {
                foreach (tb_reward_Template_imp trti in rt.RewardTemplateImp)
                {
                    if (trti.fk_reward_id == i)
                    {
                        returnNum += rtid.EditReward(trti);
                    }
                }
            }
            //返回受影响行数
            return returnNum;
        }
        /// <summary>
        /// 根据模板ID查询奖品模板信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RewardTemplate GetRewardTemplateById(int id)
        {
            RewardTemplate rewardTemplate = new RewardTemplate();
            rtd = new RewardTemplateData();
            rtid = new RewardTmpImpData();
            rewardTemplate.RewardTmp = rtd.GetRewardTmpById(id);
            if (rewardTemplate.RewardTmp != null)
            {
                rewardTemplate.RewardTemplateImp = rtid.GetRewardImpList(rewardTemplate.RewardTmp.pk_rewardTemplate_id);
            }
            return rewardTemplate;
        }

        public List<RewardTemplate> GetRewardTemplateList()
        {
            //业务实体集合
            List<RewardTemplate> rtList = new List<RewardTemplate>();
            //奖励模板集合
            List<tb_rewardTemplate> rt = new List<tb_rewardTemplate>();

            rtd = new RewardTemplateData();
            rt = rtd.GetRewardTmpList();
            if (rt != null)
            {
                rtid = new RewardTmpImpData();
                foreach (tb_rewardTemplate t in rt)
                {
                    RewardTemplate rewardTemplate = new RewardTemplate();
                    rewardTemplate.RewardTemplateImp= rtid.GetRewardImpList(t.pk_rewardTemplate_id);
                    rewardTemplate.RewardTmp = t;
                    rtList.Add(rewardTemplate);
                }
            }
            return rtList; 
        }
        /// <summary>
        /// 删除奖品模板信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelRewardTemplate(int id)
        {
            rtd = new RewardTemplateData();
            rtid = new RewardTmpImpData();
            tb_rewardTemplate trt = rtd.GetRewardTmpById(id);
            if (trt != null)
            {
                trt.bit_isDelete = true;
            }
            return rtd.EditRewardTmp(trt);
        }
    }
}