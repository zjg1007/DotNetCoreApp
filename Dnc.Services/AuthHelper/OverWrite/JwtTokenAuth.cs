using Dnc.Entities.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dnc.Services.AuthHelper.OverWrite
{
    /// <summary>
    /// 一个中间件，用来过滤每一个http请求，就是每当一个用户发送请求的时候，都先走这一步，然后再去访问http请求的接口
    /// </summary>
    public class JwtTokenAuth
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {
            //检测是否包含'Authorization'请求头
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }
            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString();

            TokenModelJWT tm = JwtHelper.SerializeJWT(tokenHeader);//序列化token，获取授权

            //授权 注意这个可以添加多个角色声明，请注意这是一个 list
            var claimList = new List<Claim>();
            var claim = new Claim(ClaimTypes.Role, tm.Role);
            claimList.Add(claim);
            var identity = new ClaimsIdentity(claimList);
            var principal = new ClaimsPrincipal(identity);
            httpContext.User = principal;

            return _next(httpContext);
        }
    }
}
