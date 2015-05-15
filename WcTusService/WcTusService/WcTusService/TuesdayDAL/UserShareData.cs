using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WcTusService.Model;
using WcTusService.TuesdayModel;

namespace WcTusService.Data
{
    /// <summary>
    /// 用户分享数据访问类
    /// </summary>
    public class UserShareData
    {
        //数据库模型
        ShareWeiEntities share = new ShareWeiEntities();
        /// <summary>
        /// 用户分享信息
        /// </summary>
        /// <param name="userShare">用户分享实体</param>
        /// <returns>受影响行数</returns>
        public int AddUserShare(tb_userShare userShare)
        {
            if (userShare != null)
            {
                share.tb_userShare.Add(userShare);
                int returnNum = share.SaveChanges();
                return returnNum;
            }
            else
            {
                return 0;
            }

        }
        /// <summary>
        /// 更新用户分享信息
        /// </summary>
        /// <param name="userShare"></param>
        /// <returns></returns>
        public int EditUserShare(tb_userShare userShare)
        {
            if (userShare != null)
            {
                share.Entry(userShare).State = EntityState.Modified;
                return share.SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据主键ID查询用户分享信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_userShare GetUserShareByID(int id)
        {
            var userShare = from r in share.tb_userShare
                       where r.pk_userShare_ID == id &&
                       r.bit_isDelete==false
                       select r;
            if (userShare.ToList().Count > 0)
            {
                return userShare.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据分享内容主键ID
        /// 查询用户分享信息集合
        /// </summary>
        /// <param name="shareId"></param>
        /// <returns></returns>
        public List<tb_userShare> GetUserShareListByShareID(int shareId)
        {
            var userShare = from r in share.tb_userShare
                            where r.fk_shareContents_id == shareId
                            select r;
            if (userShare.ToList().Count > 0)
            {
                return userShare.ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户分享集合
        /// </summary>
        /// <returns></returns>
        public List<tb_userShare> GetUserShareList()
        {
            var userShares = from r in share.tb_userShare
                        select r;
            if (userShares.ToList().Count > 0)
                return userShares.ToList();
            else
                return null;
        }
        /// <summary>
        /// 获取指定时间段的
        /// 用户分享信息集合
        /// </summary>
        /// <param name="actionTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<tb_userShare> GetUserShareListByTime(DateTime actionTime,DateTime endTime)
        {
            var query = from r in share.tb_userShare
                        where r.dtm_shareTime >= actionTime &&
                        r.dtm_shareTime <= endTime
                        select r;
            if (query.ToList().Count > 0)
                return query.ToList();
            else
                return null;
        }
    }
}