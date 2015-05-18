using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcTusService.Data;
using WcTusService.TuesdayModel;

namespace WcTusService.TuesdayBLL
{
    /// <summary>
    /// 分享业务逻辑类
    /// </summary>
    public class ShareManager
    {
        ShareData sharedata = null;
        /// <summary>
        /// 新增分享信息
        /// </summary>
        /// <param name="tbshare"></param>
        /// <returns></returns>
        public int AddShare(tb_share tbshare)
        {
            sharedata = new ShareData();
            return sharedata.Addshare(tbshare);
        }
        /// <summary>
        /// 根据分享信息主键ID获取分享内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_share GetShareById(int id)
        {
            sharedata = new ShareData();
            tb_share ts = sharedata.GetshareByid(id);
            if (ts != null)
                return ts;
            else
                return null;
        }
        /// <summary>
        /// 修改分享内容信息
        /// </summary>
        /// <param name="tbshare"></param>
        /// <returns></returns>
        public int EditShare(tb_share tbshare)
        {
            int returnNum = 0;
            sharedata=new ShareData();
            if (tbshare != null)
                return sharedata.Editshare(tbshare);
            else
                return returnNum;
        }
        /// <summary>
        /// 删除分享内容信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelShare(int id)
        {
            int returnNum = 0;
            sharedata = new ShareData();
            tb_share tbshare = sharedata.GetshareByid(id);
            if(tbshare!=null)
            {
                tbshare.bit_isDelete = true;
                return returnNum = sharedata.Editshare(tbshare);
            }
            else
                return returnNum;
        }
        /// <summary>
        /// 获取分享内容列表信息
        /// </summary>
        /// <returns></returns>
        public List<tb_share> GetShareList()
        {
            sharedata = new ShareData();
            List<tb_share> shareList = sharedata.GetshareAll();
            if (shareList != null)
                return shareList;
            else
                return null;
        }
    }
}