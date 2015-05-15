using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.TuesdayModel
{
    /// <summary>
    /// 统计报表-用户转发业务实体类
    /// </summary>
    public class Statistical_UserShare_Business
    {
        private tb_user user;

        public tb_user User
        {
            get { return user; }
            set { user = value; }
        }
        private tb_userShare userShare;

        public tb_userShare UserShare
        {
            get { return userShare; }
            set { userShare = value; }
        }
    }
}