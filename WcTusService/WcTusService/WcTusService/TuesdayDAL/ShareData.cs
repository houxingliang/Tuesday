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
        public List<tb_share> GetshareAll()
        {
            var ta = from p in share.tb_share
                     where p.bit_isDelete==false
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
        /// <summary>
        /// 获取热门分享内容
        /// </summary>
        /// <returns></returns>
        public List<tb_share> GetHotShare()
        {
            var query = from p in share.tb_share
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
                        orderby p.dtm_createTime descending
                        select p).Take(10);
            if (query != null)
                return query.ToList();
            else
                return null;
        }
    }
}