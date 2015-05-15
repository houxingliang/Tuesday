using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.Model
{
    /// <summary>
    /// 活动总转发次数统计
    /// </summary>
    public class Ttime
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int userid;
        /// <summary>
        /// 手机号
        /// </summary>
        public string phone;
        /// <summary>
        /// 姓名
        /// </summary>
        public string name;
        /// <summary>
        /// 分享时间
        /// </summary>
        public DateTime date;
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string wname;
    }
}