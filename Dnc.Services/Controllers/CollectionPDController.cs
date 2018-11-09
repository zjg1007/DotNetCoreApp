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
    /// 收藏信息表
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("DncDemo")]
    public class CollectionPDController: Controller
    {
        private readonly IEntityRepository _DbService;
        /// <summary>
        /// 收藏信息表
        /// </summary>
        /// <param name="service"></param>
        public CollectionPDController(IEntityRepository service)
        {
            this._DbService = service;
        }
        /// <summary>
        /// 获取收藏信息
        /// </summary>
        /// <param name="Uid">用户ID</param>
        /// <returns></returns>
        [HttpGet("{Uid}")]
        public IEnumerable<CollectionPD> GetCollectionPdAll(string Uid)
        {
            var data = _DbService.GetAll<CollectionPD>(m => m.ApplicationUser,m=>m.ProductsInfo).Where(m => m.ApplicationUser.Phone == Uid).ToList();
            return data;
        }
        /// <summary>
        /// 添加商品收藏信息
        /// </summary>
        /// <param name="Uid">用户ID</param>
        /// <param name="Pid">商品ID</param>
        /// <returns></returns>
        [HttpGet]
        public LogonUserStatus InsertCollection(string Uid,string Pid)
        {
            var LogonUserStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "操作失败"
            };
            if (!string.IsNullOrEmpty(Uid) && !string.IsNullOrEmpty(Pid))
            {
                var user = _DbService.GetSingleBy<ApplicationUser>(m => m.Phone == Uid);
                var pdmodel = _DbService.GetSingleBy<ProductsInfo>(m => m.ID == Pid);
                CollectionPD end = new CollectionPD()
                {
                    ApplicationUser = user,
                    ProductsInfo = pdmodel
                    //优惠信息
                };
                _DbService.AddAndSave<CollectionPD>(end);
                LogonUserStatus.IsLogon = true;
                LogonUserStatus.Message = "操作成功";
            }
            return LogonUserStatus;
        }
    }
}
