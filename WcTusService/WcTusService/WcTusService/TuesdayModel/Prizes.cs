using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.Model
{
    /// <summary>
    /// 奖品统计
    /// </summary>
    public class Prizes
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
        /// 奖品类型 
        /// </summary>
        public string typename;
        /// <summary>
        /// 所获数量
        /// </summary>
        public float Tnumber;
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string wname;
    }
}