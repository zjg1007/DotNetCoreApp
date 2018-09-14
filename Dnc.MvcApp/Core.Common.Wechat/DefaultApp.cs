using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.Core.Common.Wechat
{
    public class DefaultApp : IApp
    {
        private static  String file_access_token = "access_token.properties";
        private static  String access_token_key = "access_token";
        private static  String expiration_time_key = "expiration_time";

        private static String accessToken = "";
        private static long expirationTime = 0L;

        private String appId = "wx056b1427145ca83c";
        private String appSecret = "3189cc34d5d3100942315b654c54673d";
        private String token = "lyj1";
        private String appGroupId = null;
        private String mchId = null;
        private String mchKey = null;
        public DefaultApp(String appGroupId)
        {
            this.appGroupId = appGroupId;
        }

        public DefaultApp(String appId, String appSecret)
        {
            this.appId = appId;
            this.appSecret = appSecret;
        }

        public DefaultApp(String appId, String appSecret, String token, String appGroupId)
        {
            this.appId = appId;
            this.appSecret = appSecret;
            this.token = token;
            this.appGroupId = appGroupId;
        }
        public string getAccessToken()
        {
            return null;
        }

        public string GetAppGroupId()
        {
            throw new NotImplementedException();
        }

        public string getAppId()
        {
            throw new NotImplementedException();
        }

        public string getAppSecret()
        {
            throw new NotImplementedException();
        }

        public string getJsapiTicket()
        {
            throw new NotImplementedException();
        }

        public string getMchId()
        {
            throw new NotImplementedException();
        }

        public string getMchKey()
        {
            throw new NotImplementedException();
        }

        public string getToken()
        {
            throw new NotImplementedException();
        }
    }
}
