using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.Model
{
    /// <summary>
    /// 糖币明细
    /// </summary>
    public class SugarList
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
        /// 所获糖币
        /// </summary>
        public float sugar;
        /// <summary>
        /// 活动标题 
        /// </summary>
        public string title;
        /// <summary>
        /// 分享类型
        /// </summary>
        public string type;
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string wname;
    }
}