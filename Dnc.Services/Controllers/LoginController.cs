using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Application;
using Dnc.Services.AuthHelper.OverWrite;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Services.Controllers
{
    /// <summary>
    /// 登陆获取Token
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("DncDemo")]
    public class LoginController : Controller
    {
        private readonly IEntityRepository _DbService;
        /// <summary>
        /// 收藏信息表
        /// </summary>
        /// <param name="service"></param>
        public LoginController(IEntityRepository service)
        {
            this._DbService = service;
        }
        #region 获取token的第二种方法
        /// <summary>
        /// 获取JWT的方法
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="sub">角色</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token")]
        public async Task<object> GetJWTStr(string name, string pass)
        {
            string jwtStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            //这里直接写死了

            // var user = await sysUserInfoServices.GetUserRoleNameStr(name, pass);
            var user = _DbService.GetSingleBy<ApplicationUser>(m => m.Phone == name && m.Password == pass,m=>m.ApplicationGroup);

            if (user != null)
            {

                TokenModelJWT tokenModel = new TokenModelJWT();
                tokenModel.Uid = user.ApplicationGroup.RoleID;
                tokenModel.Role = user.ApplicationGroup.Role;

                jwtStr = JwtHelper.IssueJWT(tokenModel);
                suc = true;
            }
            else
            {
                jwtStr = "login fail!!!";
            }

            return Ok(new
            {
                success = suc,
                token = jwtStr
            });
        }
        [HttpGet]
        [Route("GetTokenNuxt")]
        public async Task<object> GetJWTStrForNuxt(string name, string pass)
        {
            string jwtStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            //这里直接写死了
            if (name == "admins" && pass == "admins")
            {
                TokenModelJWT tokenModel = new TokenModelJWT();
                tokenModel.Uid = 1;
                tokenModel.Role = "Admin";

                jwtStr = JwtHelper.IssueJWT(tokenModel);
                suc = true;
            }
            else
            {
                jwtStr = "login fail!!!";
            }
            var result = new
            {
                data = new { success = suc, token = jwtStr }
            };

            return Ok(new
            {
                success = suc,
                data = new { success = suc, token = jwtStr }
            });
        }
    #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sub"></param>
        /// <param name="expiresSliding"></param>
        /// <param name="expiresAbsoulute"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("jsonp")]
        public void Getjsonp(string callBack, long id = 1, string sub = "Admin", int expiresSliding = 30, int expiresAbsoulute = 30)
        {
            TokenModelJWT tokenModel = new TokenModelJWT();
            tokenModel.Uid = id;
            tokenModel.Role = sub;

            string jwtStr = JwtHelper.IssueJWT(tokenModel);

            string response = string.Format("\"value\":\"{0}\"", jwtStr);
            string call = callBack + "({" + response + "})";
            Response.WriteAsync(call);
        }
    }
}
