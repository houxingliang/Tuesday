using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcTusService.TuesdayModel;

namespace WcTusService.TuesdayBLL
{
    public class TokenManager
    {
        //数据库模型
        ShareWeiEntities share = new ShareWeiEntities();
        /// <summary>
        /// 查看token是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsToken(string token)
        {
            var query = from p in share.tb_token
                        where p.vr_token == token
                        select p;
            tb_token tokenEntity = query.FirstOrDefault();
            if (tokenEntity == null)
            {
                throw new Exception("无效的token值");
            }
            if (tokenEntity.dtm_tokenTime < DateTime.Now)
            {
                throw new Exception("token超时，请重新生成");
            }
            return true;
        }

    }
}