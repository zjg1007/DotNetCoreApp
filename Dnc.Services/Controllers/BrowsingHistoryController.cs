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
    /// 浏览信息API
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("DncDemo")]
    public class BrowsingHistoryController: Controller
    {
        private readonly IEntityRepository _DbService;
        /// <summary>
        /// 浏览信息API
        /// </summary>
        /// <param name="service"></param>
        public BrowsingHistoryController(IEntityRepository service)
        {
            this._DbService = service;
        }
        /// <summary>
        /// 获取浏览记录
        /// </summary>
        /// <param name="Uid">用户ID</param>
        /// <returns></returns>
        [HttpGet("{Uid}")]
        public IEnumerable<BrowsingHistory> GetBrowsingAll(string Uid)
        {
            var data = _DbService.GetAll<BrowsingHistory>(m => m.ApplicationUser,m=>m.ProductsInfo).Where(m => m.ApplicationUser.Phone == Uid).ToList();
            return data;
        }
        /// <summary>
        /// 录入浏览记录
        /// </summary>
        /// <param name="Uid">用户ID</param>
        /// <param name="Pid">商品ID</param>
        /// <returns></returns>
        [HttpGet]
        public LogonUserStatus InsertBrowsingInfo(string Uid,string Pid)
        {
            var LogonUserStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "操作失败"
            };
            if (!string.IsNullOrEmpty(Uid) && !string.IsNullOrEmpty(Pid))
            {
                var frired = _DbService.GetSingleBy<BrowsingHistory>(m => m.ProductsInfo.ID == Pid && m.ApplicationUser.Phone == Uid);
                if (frired == null)
                {
                    var user = _DbService.GetSingleBy<ApplicationUser>(m => m.Phone == Uid);
                    var pdmodel = _DbService.GetSingleBy<ProductsInfo>(m => m.ID == Pid);
                    BrowsingHistory end = new BrowsingHistory()
                    {
                        ApplicationUser = user,
                        ProductsInfo = pdmodel
                        //优惠信息
                    };
                    _DbService.AddAndSave<BrowsingHistory>(end);
                    LogonUserStatus.IsLogon = true;
                    LogonUserStatus.Message = "操作成功";
                }
                
            }
            return LogonUserStatus;
        }
    }
}
