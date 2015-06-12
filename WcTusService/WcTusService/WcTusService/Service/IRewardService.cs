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
        List<string> GetList(string token);
        #region 奖品相关
        [OperationContract]
        int AddReward(tb_reward reward,string token);
        [OperationContract]
        int EditReward(tb_reward reward,string token);
        [OperationContract]
        List<tb_reward> GetRewardList(string token);
        [OperationContract]
        int DelRewardList(int id, string token);
        #endregion

        #region 奖品模板相关
        [OperationContract]
        int AddRewardTmp(RewardTemplate rt, string token);

        [OperationContract]
        int EditRewardTmp(RewardTemplate rt, string token);

        [OperationContract]
        int DelRewardTmp(int rewardTmpId, string token);

        [OperationContract]
        List<RewardTemplate> GetRewrdTmpList(string token);

        [OperationContract]
        List<tb_reward_Template_imp> GetRewardImpList(int tmpID, string token);

        [OperationContract]
        RewardTemplate GetRewardTmpById(int id, string token);

        [OperationContract]
        tb_reward GetTangbiByTmpId(int id, string token);
        #endregion

        #region 分享相关
        [OperationContract]
        int AddShare(tb_share share, string token);
        [OperationContract]
        int EditShare(tb_share share, string token);
        [OperationContract]
        int DelShare(int id, string token);
        [OperationContract]
        List<tb_share> GetShareList(bool status,string token);
        [OperationContract]
        List<tb_share> GetHotShareList(string token);
        [OperationContract]
        tb_share GetShareById(int id, string token);
        [OperationContract]
        List<tb_reward> GetRewardByShareId(int id, string token);
        [OperationContract]
        List<RewardUserGrantEntity> GetShareGrantListById(int id, bool isApply, bool isGrant, string token);
        #endregion

        #region  任务相关
        [OperationContract]
        int AddTask(tb_task task, string token);
        [OperationContract]
        int EditTask(tb_task task, string token);
        [OperationContract]
        int DelTask(int id, string token);
        [OperationContract]
        List<tb_task> GetTaskList(string token);
        [OperationContract]
        tb_task GetTaskById(int id, string token);
        [OperationContract]
        List<tb_reward> GetRewardByTaskId(int id, string token);
        #endregion

        #region 任务项相关
        [OperationContract]
        int AddTaskItem(tb_taskItem taskItem, string token);
        [OperationContract]
        int EditTaskItem(tb_taskItem taskItem, string token);
        [OperationContract]
        tb_taskItem GetTaskItem(int id, string token);
        [OperationContract]
        int DelTaskItem(int id, string token);
        [OperationContract]
        List<tb_taskItem> GetTaskItemList(int id, string token);
        #endregion
        #region 用户任务执行情况
        [OperationContract]
        int GetTimeByUserId(int userId, string token);
        #endregion
        #region 奖品发放相关
        //按任务分类查询列表信息
        [OperationContract]
        List<RewardUserGrantEntity> GetTaskExecuteByTaskName(int id, DateTime actionDate, DateTime endDate, string token);
        //按活动内容分类查询列表
        [OperationContract]
        List<tb_share> GetShareList_Grant(string name, DateTime actionDate, DateTime endDate, string token);
        //按任务分类的主键ID查询列表信息
        [OperationContract]
        List<RewardUserGrantEntity> GetTaskExecuteByTaskID(int id, bool isApply, bool isGrant, string token);

        //按用户分类查询奖品发放信息
        [OperationContract]
        List<RewardUserGrantEntity> GetTaskExecuteByUser(string nickName, string name, string phoneNum, string token);

        //根据用户ID发放奖品
        [OperationContract]
        int GrantRewardByUserID(int id, string token);

        //根据登录用户ID申请奖品
        [OperationContract]
        int TaskApplication(int id, string token);
        //根据任务执行表主键发放奖励
        [OperationContract]
        int FafangTask(List<int> idList,string token);
        //根据用户分享表主键ID发放奖励
        [OperationContract]
        int FafangShare(List<int> idList, string token);
        #endregion

        #region 统计报表
        //活动首次转发统计
        [OperationContract]
        List<Statistical_UserShare_Business> FirstShare(int shareId, string token);

        //总转发次数统计
        [OperationContract]
        List<Statistical_UserShare_Business> TotalShare(int shareId, string token);

        //活动用户首次转发排名统计
        [OperationContract]
        List<Statistical_Rank_business> FirstRank(List<int> taskId, string token);
        [OperationContract]
        List<Statistical_Rank_business> FirstRankTop10(List<int> taskId, int top, string token);
        //活动用户总转排名统计
        [OperationContract]
        List<Statistical_Rank_business> TotalRank(List<int> taskId, string token);
        [OperationContract]
        List<Statistical_Rank_business> TotalRankTop10(List<int> taskId, int top, string token);
        //用户奖品总数统计
        [OperationContract]
        List<Statistical_UserRank_Business> UserRewardSum(DateTime actionTime, DateTime endTime, int RewardId, string token);
        //糖币明细
        [OperationContract]
        List<TangbiDetail> GetTangBiDetail(DateTime actionTime, DateTime endTime, string token);
        //获取最新分享统计信息
        [OperationContract]
        List<tb_share> GetNewShareList(string token);
        [OperationContract]
        List<tb_reward> GetTotalRewardByTime(DateTime actionTime, DateTime endTime, string token);
        #endregion

        #region 用户相关
        [OperationContract]
        int AddUser(tb_user user, string token);
        [OperationContract]
        int EditUser(tb_user user, string token);
        [OperationContract]
        tb_user GetUserById(int id, string token);
        [OperationContract]
        bool IsUsedPhone(string PhoneNum,string token);
        [OperationContract]
        tb_user GetUserByPhoneNum(string num, string token);
        [OperationContract]
        tb_user GetUserByTuesdayId(string id, string token);
        #endregion

        #region token相关

        [OperationContract]
        TokenEntity EditToken(string appid);
        [OperationContract]
        TokenEntity GetToken(string appid);
        #endregion
    }
}
