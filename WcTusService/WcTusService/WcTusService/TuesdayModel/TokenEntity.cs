using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcTusService.TuesdayModel
{
    public class TokenEntity
    {
        string token;

        public string Token
        {
            get { return token; }
            set { token = value; }
        }
        DateTime tokenTime;

        public DateTime TokenTime
        {
            get { return tokenTime; }
            set { tokenTime = value; }
        }

    }
}