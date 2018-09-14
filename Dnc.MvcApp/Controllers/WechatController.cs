using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnc.MvcApp.Core.Common.Wechat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnc.MvcApp.Controllers
{
    public class WechatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 验证接口
        /// </summary>
        /// <param name="signature">签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        [HttpGet, Route("Message")]
        [AllowAnonymous]
        public ActionResult MessageGet(string signature, string timestamp, string nonce, string echostr)
        {
            return View();
        }

        /// <summary>
        /// 接收消息并处理和返回相应结果
        /// </summary>
        /// <param name="msg_signature">当加密模式时才会有该变量（消息签名）</param>
        /// <param name="signature">签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        [HttpPost, Route("Message")]
        [AllowAnonymous]
        public ActionResult MessagePost(string msg_signature, string signature, string timestamp, string nonce)
        {
            return View();
        }
    }
}