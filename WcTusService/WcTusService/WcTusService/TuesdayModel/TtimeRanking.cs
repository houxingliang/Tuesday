using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.Model
{
    /// <summary>
    /// 几次活动总转发排名
    /// </summary>
    public class TtimeRanking
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int userid;
        /// <summary>
        /// 电话号码
        /// </summary>
        public string phone;
        /// <summary>
        /// 姓名
        /// </summary>
        public string name;
        /// <summary>
        /// 名次
        /// </summary>
        public int ranking;
        /// <summary>
        /// 总转次数
        /// </summary>
        public int ttime;
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string wname;
    }
}