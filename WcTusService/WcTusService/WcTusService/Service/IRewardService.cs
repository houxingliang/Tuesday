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

        [OperationContract]
        List<tb_reward_Template_imp> GetRewardImpList(int tmpID);
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
        [OperationContract]
        List<tb_share> GetHotShareList();
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

        #region 奖品发放相关
        //按任务分类查询列表信息
        [OperationContract]
        List<RewardUserGrantEntity> GetTaskExecuteByTaskName(string name, DateTime actionDate, DateTime endDate);

        //按任务分类的主键ID查询列表信息
        [OperationContract]
        List<RewardUserGrantEntity> GetTaskExecuteByTaskID(int id, DateTime actionDate, DateTime endDate);

        //按用户分类查询奖品发放信息
        [OperationContract]
        List<RewardUserGrantEntity> GetTaskExecuteByUser(string nickName, string name, string phoneNum);

        //根据用户ID发放奖品
        [OperationContract]
        int GrantRewardByUserID(int id);

        //根据登录用户ID申请奖品
        [OperationContract]
        int TaskApplication(int id);

        #endregion

        #region 统计报表
        //活动首次转发统计
        [OperationContract]
        List<Statistical_UserShare_Business> FirstShare(int taskId);

        //总转发次数统计
        [OperationContract]
        List<Statistical_UserShare_Business> TotalShare(int taskId);

        //活动用户首次转发排名统计
        [OperationContract]
        List<Statistical_Rank_business> FirstRank(List<int> taskId);

        //活动用户总转排名统计
        [OperationContract]
        List<Statistical_Rank_business> TotalRank(List<int> taskId);

        //用户奖品总数统计
        [OperationContract]
        List<Statistical_UserRank_Business> UserRewardSum(DateTime actionTime, DateTime endTime, int RewardId);


        #endregion

        #region 用户相关
        [OperationContract]
        int AddUser(tb_user user);
        [OperationContract]
        int EditUser(tb_user user);
        [OperationContract]
        tb_user GetUserById(int id);
        [OperationContract]
        bool IsUsedPhone(string PhoneNum);
        #endregion
    }
}
