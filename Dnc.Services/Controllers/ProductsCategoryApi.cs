using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Article;
using DNC.ApiModel.Article;
using DNC.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class ProductsCategoryApi : Controller
    {
        private readonly IEntityRepository _DbService;

        public ProductsCategoryApi(IEntityRepository service)
        {
            this._DbService = service;
        }
        /// <summary>
        /// 获取全部商品类别信息
        /// </summary>
        /// /api/ProductsCategoryApi
        /// <returns></returns>
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
      
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
