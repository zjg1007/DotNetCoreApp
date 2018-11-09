using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Application;
using Dnc.Entities.Article;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Services.Controllers
{
    /// <summary>
    /// 签到表
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("DncDemo")]
    public class SignInController: Controller
    {
        private readonly IEntityRepository _DbService;
        /// <summary>
        /// 签到表
        /// </summary>
        /// <param name="service"></param>
        public SignInController(IEntityRepository service)
        {
            this._DbService = service;
        }
        /// <summary>
        /// 测试BUG
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        /// <summary>
        /// 获取用户签到信息
        /// </summary>
        /// <param name="Uid">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSignInInfoByUid")]
        public IActionResult GetSignInInfoByUid(string Uid)
        {
            var dataInfo = _DbService.GetAll<SignIn>(m => m.ApplicationUser).
                 Where(n => n.ApplicationUser.Phone == Uid && n.NewTime > DateTime.Now.AddDays(-Convert.ToInt32(DateTime.Now.Date.Day))).
                 Select(s => new { dataTime = s.NewTime.Day });
            return Json(dataInfo);
        }
        /// <summary>
        /// 用户签到
        /// </summary>
        /// <param name="Uid">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("SignInsert")]
        public LogonUserStatus SignInsert(string Uid)
        {
            var LogonUserStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "操作失败"
            };
            try
            {
                var and = _DbService.GetSingleBy<SignIn>(m => m.ApplicationUser.Phone == Uid && m.NewTime.Day==DateTime.Now.Day);
                if (and != null)
                {
                    LogonUserStatus.IsLogon = false;
                    LogonUserStatus.Message = "今日已经签到过了,无需重复操作.";
                    return LogonUserStatus;
                }
                else if(and == null)
                {
                    LogonUserStatus.IsLogon = false;
                    LogonUserStatus.Message = "没有此用户.";
                    return LogonUserStatus;
                }
                var end = _DbService.GetSingleBy<ApplicationUser>(m => m.Phone == Uid);
                SignIn model = new SignIn() { ApplicationUser = end };
                _DbService.AddAndSave<SignIn>(model);
                LogonUserStatus.IsLogon = true;
                LogonUserStatus.Message = "操作成功";

            }
            catch (Exception)
            {
                LogonUserStatus.IsLogon = false;
                LogonUserStatus.Message = "操作失败";
            }
            

            return LogonUserStatus;
        }
    }
}
