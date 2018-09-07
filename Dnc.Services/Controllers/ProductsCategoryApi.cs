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
        /// <summary>
        /// 根据商品名称获取商品详细信息
        /// </summary>
        /// <param name="name"></param>
        /// /api/ProductsCategory/GetProductsAllByName?name=
        /// <returns></returns>
        [HttpGet("{name}")]
        [HttpGet("GetProductsAllByName")]
        public IEnumerable<ProductsInfoMV> GetProductsAllByName(string name)
        {
            var data = _DbService.GetAll<ProductsInfo>(m => m.ProductsCategory).Where(m => m.Name.Contains(name)).ToList();
            var dataList = new List<ProductsInfoMV>();
            foreach (var item in data)
            {
                dataList.Add(new ProductsInfoMV(item));
            }
            return dataList;
        }
        /// <summary>
        /// 根据商品类型名称查所有商品信息
        /// </summary>
        /// <param name="name"></param>
        /// /api/ProductsCategory/GetProductsAllByCName?name=
        /// <returns></returns>
        [HttpGet("{name}")]
        [HttpGet("GetProductsAllByCName")]
        public IEnumerable<ProductsInfoMV> GetProductsAllByCName(string name)
        {
            var data = _DbService.GetAll<ProductsInfo>(m => m.ProductsCategory).Where(m => m.ProductsCategory.Name.Contains(name)).ToList();
            var dataList = new List<ProductsInfoMV>();
            foreach (var item in data)
            {
                dataList.Add(new ProductsInfoMV(item));
            }
            return dataList;
        }
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
