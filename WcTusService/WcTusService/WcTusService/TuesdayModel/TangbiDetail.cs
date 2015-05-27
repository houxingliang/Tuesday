using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.TuesdayModel
{
    public class TangbiDetail
    {
        tb_user user;
        /// <summary>
        /// 用户信息
        /// </summary>
        public tb_user User
        {
            get { return user; }
            set { user = value; }
        }
        double sum;
        /// <summary>
        /// 糖币数量
        /// </summary>
        public double Sum
        {
            get { return sum; }
            set { sum = value; }
        }
        string name;
        /// <summary>
        /// 活动标题
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        DateTime shareTime;
        /// <summary>
        /// 分享时间
        /// </summary>
        public DateTime ShareTime
        {
            get { return shareTime; }
            set { shareTime = value; }
        }
        string shareType;
        /// <summary>
        /// 分享类型
        /// </summary>
        public string ShareType
        {
            get { return shareType; }
            set { shareType = value; }
        }
    }
}