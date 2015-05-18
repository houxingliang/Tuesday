using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcTusService.TuesdayModel;

namespace WcTusService.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IRewardService”。
    [ServiceContract]
    public interface IRewardService
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        List<string> GetList();
        #region 奖品相关
        [OperationContract]
        int AddReward(tb_reward reward);
        [OperationContract]
        int EditReward(tb_reward reward);
        [OperationContract]
        List<tb_reward> GetRewardList();
        [OperationContract]
        int DelRewardList(int id);
        #endregion

        #region 奖品模板相关
        [OperationContract]
        int AddRewardTmp(RewardTemplate rt);

        [OperationContract]
        int EditRewardTmp(RewardTemplate rt);

        [OperationContract]
        int DelRewardTmp(int rewardTmpId);

        [OperationContract]
        List<RewardTemplate> GetRewrdTmpList();
        #endregion

        #region 分享相关
        [OperationContract]
        int AddShare(tb_share share);
        [OperationContract]
        int EditShare(tb_share share);
        [OperationContract]
        int DelShare(int id);
        [OperationContract]
        List<tb_share> GetShareList();
        #endregion

        #region  任务相关
        [OperationContract]
        int AddTask(tb_task task);
        [OperationContract]
        int EditTask(tb_task task);
        [OperationContract]
        int DelTask(int id);
        [OperationContract]
        List<tb_task> GetTaskList();
        #endregion

        #region 任务项相关
        [OperationContract]
        int AddTaskItem(tb_taskItem taskItem);
        [OperationContract]
        int EditTaskItem(tb_taskItem taskItem);
        [OperationContract]
        tb_taskItem GetTaskItem(int id);
        [OperationContract]
        int DelTaskItem(int id);
        [OperationContract]
        List<tb_taskItem> GetTaskItemList(int id);
        #endregion
    }
}
