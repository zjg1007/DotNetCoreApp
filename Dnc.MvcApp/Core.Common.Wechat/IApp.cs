using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.Core.Common.Wechat
{
    public interface IApp
    {
          String GetAppGroupId();

         String getAppId();

         String getAppSecret();

         String getToken();

         String getAccessToken();

         String getMchId();

         String getMchKey();

         String getJsapiTicket();
    }
}
