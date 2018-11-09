using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Article;
using DNC.ApiModel.Article;
using DNC.ViewModels.Article;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Services.Controllers
{
    /// <summary>
    /// 商品类别表
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("DncDemo")]
    public class ProductsCategoryApiController : Controller
    {
        private readonly IEntityRepository _DbService;
        /// <summary>
        /// 获取全部商品类别信息
        /// </summary>
        /// <param name="service"></param>
        public ProductsCategoryApiController(IEntityRepository service)
        {
            this._DbService = service;
        }
        /// <summary>
        /// 获取全部商品类别信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ProductsCategoryMV> Get()
        {
            var data = _DbService.GetAll<ProductsCategory>().ToList();
            var dataList = new List<ProductsCategoryMV>();
            foreach (var item in data)
            {
                dataList.Add(new ProductsCategoryMV(item));
            }
            return dataList;
        }
      
    }
}
