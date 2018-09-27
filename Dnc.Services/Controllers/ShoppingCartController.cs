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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("DncDemo")]
    public class ShoppingCartController: Controller
    {
        private readonly IEntityRepository _DbService;

        public ShoppingCartController(IEntityRepository service)
        {
            this._DbService = service;
        }
        /// <summary>
        /// 获取用户购物车信息
        /// /api/ShoppingCart/GetProductInfoAllByID?Uid=
        /// </summary>
        /// <param name="Uid">用户ID</param>
        /// <returns></returns>
        [HttpGet("{Uid}")]
        [HttpGet("GetProductInfoAllByID")]
        public IEnumerable<ShoppingCart> GetProductInfoAllByID(string Uid)
        {
            var data = _DbService.GetAll<ShoppingCart>(m => m.ApplicationUser).Where(m => m.ApplicationUser.Phone == Uid);
            return data;
        }
        /// <summary>
        /// 添加购物车
        /// /api/ShoppingCart/InsertShoppingCart?Uid=**&&Pid=
        /// </summary>
        /// <param name="Uid">用户ID</param>
        /// <param name="Pid">商品ID</param>
        /// <returns></returns>
        [HttpGet("InsertShoppingCart")]
        public LogonUserStatus InsertShoppingCart(string Uid,string Pid)
        {
            var LogonUserStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "添加购物车失败"
            };
            try
            {
                if (!string.IsNullOrEmpty(Uid)&& !string.IsNullOrEmpty(Pid))
                {
                    //用户信息
                    var user = _DbService.GetSingleBy<ApplicationUser>(m => m.Phone == Uid);
                    //商品信息
                    var pdinfo = _DbService.GetSingleBy<ProductsInfo>(m => m.ID == Pid);
                    var spC = new ShoppingCart()
                    {
                        ApplicationUser = user,
                        ProductsInfo = pdinfo
                    };
                    /*
                     优惠券绑定（为完成）

                     */
                    _DbService.AddAndSave<ShoppingCart>(spC);
                    LogonUserStatus.IsLogon = true;
                    LogonUserStatus.Message = "添加成功";
                }
               
            }
            catch (Exception)
            {
                LogonUserStatus.IsLogon = false;
                LogonUserStatus.Message = "添加失败";
            }
           
            return LogonUserStatus;
        }
        /// <summary>
        /// 增加商品购买数量
        /// /api/ShoppingCart/ShoppingCartAddNubmer?id=
        /// </summary>
        /// <param name="id">购物车表ID</param>
        /// <returns></returns>
        [HttpGet("ShoppingCartAddNubmer")]
        public LogonUserStatus ShoppingCartAddNubmer(string id)
        {
            var LogonUserStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "操作失败"
            };
            try
            {
                var data = _DbService.GetSingle<ShoppingCart>(Guid.Parse(id), m => m.ApplicationUser, m => m.ProductsInfo);
                if (data.Number <= 99)
                {
                    data.Number++;
                    _DbService.AddOrEditAndSave<ShoppingCart>(data);
                    LogonUserStatus.IsLogon = true;
                    LogonUserStatus.Message = "操作成功";
                }
                else
                {
                    LogonUserStatus.IsLogon = false;
                    LogonUserStatus.Message = "操作失败,数量不能大于99";
                }
               
            }
            catch (Exception)
            {
                LogonUserStatus.IsLogon = false;
                LogonUserStatus.Message = "操作失败";
            }

            return LogonUserStatus;
        }
        /// <summary>
        /// 减去购物车数量
        /// /api/ShoppingCart/ShoppingCartReduceNubmer?id=
        /// </summary>
        /// <param name="id">购物车ID</param>
        /// <returns></returns>
        [HttpGet("ShoppingCartReduceNubmer")]
        public LogonUserStatus ShoppingCartReduceNubmer(string id)
        {
            var LogonUserStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "操作失败"
            };
            try
            {
                var data = _DbService.GetSingle<ShoppingCart>(Guid.Parse(id), m => m.ApplicationUser, m => m.ProductsInfo);
                if (data.Number > 1)
                {
                    data.Number--;
                    _DbService.AddOrEditAndSave<ShoppingCart>(data);
                    LogonUserStatus.IsLogon = true;
                    LogonUserStatus.Message = "操作成功";
                }
                else
                {
                    LogonUserStatus.IsLogon = false;
                    LogonUserStatus.Message = "商品数量必须大于1";
                }
            }
            catch (Exception)
            {
                LogonUserStatus.IsLogon = false;
                LogonUserStatus.Message = "操作失败";
            }

            return LogonUserStatus;
        }
        /// <summary>
        /// 删除商品信息-购物车
        /// /api/ShoppingCart/ShoppingCartDel?id=
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ShoppingCartDel")]
        public LogonUserStatus ShoppingCartDel(string id)
        {
            var LogonUserStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "操作失败"
            };
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var data = _DbService.GetSingle<ShoppingCart>(Guid.Parse(id), m => m.ApplicationUser, m => m.ProductsInfo);
                    _DbService.DeleteAndSave<ShoppingCart>(data);
                    LogonUserStatus.IsLogon = true;
                    LogonUserStatus.Message = "操作成功";
                }
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
