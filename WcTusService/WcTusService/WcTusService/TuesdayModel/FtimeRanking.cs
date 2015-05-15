using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.Model
{
    /// <summary>
    /// 几次活动首次转发排名
    /// </summary>
    public class FtimeRanking
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
        /// 首转次数
        /// </summary>
        public int ftime;
    }
}