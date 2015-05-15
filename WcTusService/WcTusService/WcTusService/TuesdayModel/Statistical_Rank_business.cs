using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.TuesdayModel
{
    /// <summary>
    /// 统计报表
    /// 活动用户转发排名业务实体类
    /// </summary>
    public class Statistical_Rank_business
    {
        private tb_user user;

        public tb_user User
        {
            get { return user; }
            set { user = value; }
        }
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
    }
}