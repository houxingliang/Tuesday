using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.Model
{
    /// <summary>
    /// 活动首次转发
    /// </summary>
    public class Ftime
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
    }
}