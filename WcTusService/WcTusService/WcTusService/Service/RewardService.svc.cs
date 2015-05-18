using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using WcTusService.TuesdayBLL;
using WcTusService.TuesdayModel;

namespace WcTusService.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“RewardService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 RewardService.svc 或 RewardService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)] 
    public class RewardService : IRewardService
    {
        #region 废代码
        public void DoWork()
        {
        }
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<string> GetList()
        {
            List<string> list = new List<string>();
            list.Add("s");
            list.Add("s");
            list.Add("'");
            return list;
        }
        #endregion

        #region 奖品相关
        /// <summary>
        /// 添加奖品信息
        /// </summary>
        /// <param name="reward"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddReward(tb_reward reward)
        {
            RewardManager rm = new RewardManager();
            return rm.AddReward(reward);
        }
        /// <summary>
        /// 修改奖品信息
        /// </summary>
        /// <param name="reward"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int EditReward(tb_reward reward)
        {
            RewardManager manager = new RewardManager();
            return manager.EditReward(reward);
        }
        /// <summary>
        /// 获取奖品信息列表
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_reward> GetRewardList()
        {
            RewardManager rm = new RewardManager();
            return rm.GetRewardList();
        }
        /// <summary>
        /// 删除奖品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int DelRewardList(int id)
        {
            RewardManager rm = new RewardManager();
            return rm.DelRewardById(id);
        }
        #endregion
       
        #region 奖品模板相关
        /// <summary>
        /// 添加奖品模板信息
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddRewardTmp(RewardTemplate rt)
        {
            //rt = new RewardTemplate();
            //rt.RewardTemplateImp = new List<tb_reward_Template_imp>();
            //tb_reward_Template_imp imp = new tb_reward_Template_imp();
            //imp.bit_isDelete = false;
            //imp.dbl_count = 0.01;
            //imp.fk_reward_id = 1;
            //imp.fk_rewardTemplate_id = 1;
            //rt.RewardTemplateImp.Add(imp);
            //rt.RewardTmp = new tb_rewardTemplate();
            //rt.RewardTmp.bit_isDelete = false;
            //rt.RewardTmp.nvr_tmpName = "糖币";
            //rt.RewardTmp.pk_rewardTemplate_id = 1;
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(RewardTemplate));
            byte[] byteArr;
            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, rt);

                byteArr = ms.ToArray();
            }
            string temp = Encoding.UTF8.GetString(byteArr);
            Console.WriteLine(temp); 


            RewardTemplateManager rtm = new RewardTemplateManager();
            return rtm.AddRewardTemplate(rt);
        }
        /// <summary>
        /// 修改奖励模板
        /// </summary>
        /// <param name="rt">奖励模板实体</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int EditRewardTmp(RewardTemplate rt)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(RewardTemplate));
            byte[] byteArr;
            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, rt);

                byteArr = ms.ToArray();
            }
            string temp = Encoding.UTF8.GetString(byteArr);
            Console.WriteLine(temp);
            RewardTemplateManager rtm = new RewardTemplateManager();
            return rtm.EditRewardTemplate(rt);
        }
        /// <summary>
        /// 删除奖励模板
        /// </summary>
        /// <param name="rewardTmpId"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int DelRewardTmp(int rewardTmpId)
        {
            RewardTemplateManager rewardTmp = new RewardTemplateManager();
            return rewardTmp.DelRewardTemplate(rewardTmpId);
        }
        /// <summary>
        /// 奖品模板集合
        /// </summary>
        /// <returns></returns>
        public List<RewardTemplate> GetRewrdTmpList()
        {
            RewardTemplateManager rtm = new RewardTemplateManager();
            return rtm.GetRewardTemplateList();
        }
        #endregion

        #region 分享相关
        ShareManager shareManager = null;    
        /// <summary>
        /// 添加分享信息
        /// </summary>
        /// <param name="share"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddShare(tb_share share)
        {
            shareManager = new ShareManager();
            return shareManager.AddShare(share);
        }
        /// <summary>
        /// 修改分享信息
        /// </summary>
        /// <param name="share"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int EditShare(tb_share share)
        {
            shareManager = new ShareManager();
            return shareManager.EditShare(share);
        }
        /// <summary>
        /// 删除分享信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int DelShare(int id)
        {
            shareManager = new ShareManager();
            return shareManager.DelShare(id);
        }
        /// <summary>
        /// 获取分享列表信息
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_share> GetShareList()
        {
            shareManager = new ShareManager();
            return shareManager.GetShareList();
        }
        #endregion

        #region 任务相关
        TaskManager taskManager = null;
        /// <summary>
        /// 发布任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddTask(tb_task task)
        {
            taskManager = new TaskManager();
            return taskManager.AddTask(task);
        }
        /// <summary>
        /// 修改任务信息
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int EditTask(tb_task task)
        {
            taskManager = new TaskManager();
            return taskManager.EditTask(task);
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int DelTask(int id)
        {
            taskManager = new TaskManager();
            return taskManager.DelTask(id);
        }
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_task> GetTaskList()
        {
            taskManager = new TaskManager();
            return taskManager.GetTaskList();
        }
        #endregion

        #region  任务项相关
        
        /// <summary>
        /// 添加任务项
        /// </summary>
        /// <param name="taskItem"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int AddTaskItem(tb_taskItem taskItem)
        {
            //{"bit_isDelete":false,"bit_isInherit":true,"bit_status":true,"dtm_actionTime":"\\/Date(1431931532208+0800)\\/","dtm_endTime":"\\/Date(1431931532208+0800)\\/","fk_rewardTemplate_id":1,"fk_share_id":1,"fk_task_id":1,"int_forward":1,"int_order":1,"pk_taskItem_id":0}
            //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(tb_taskItem));
            //byte[] byteArr;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    json.WriteObject(ms, taskItem);

            //    byteArr = ms.ToArray();
            //}
            //string temp = Encoding.UTF8.GetString(byteArr);
            //Console.WriteLine(temp);
            taskManager = new TaskManager();
            
            return taskManager.AddTaskItem(taskItem);;
        }
        /// <summary>
        /// 修改任务项信息
        /// </summary>
        /// <param name="taskItem"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int EditTaskItem(tb_taskItem taskItem)
        {
            return 0;
        }
        /// <summary>
        /// 根据主键ID
        /// 获取任务项详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public tb_taskItem GetTaskItem(int id)
        {
            return null;
        }
        /// <summary>
        /// 根据主键ID
        /// 删除任务项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public int DelTaskItem(int id)
        {
            return 0;
        }
        /// <summary>
        /// 根据任务ID（外键）
        /// 获取任务下的所有任务项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<tb_taskItem> GetTaskItemList(int id)
        {
            return null;
        }
        #endregion
    }
}
