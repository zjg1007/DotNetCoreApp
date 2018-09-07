using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Article;
using DNC.ApiModel.Article;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class ProductsInfoApi: Controller
    {
        private readonly IEntityRepository _DbService;

        public ProductsInfoApi(IEntityRepository service)
        {
            this._DbService = service;
        }
        /// <summary>
        /// 获取全部商品信息
        /// </summary>
        /// /api/ProductsInfoApi
        /// <returns></returns>
        public IEnumerable<ProductsInfoMV> Get()
        {
            var data = _DbService.GetAll<ProductsInfo>(m => m.ProductsCategory).ToList();
            var dataList = new List<ProductsInfoMV>();
            foreach (var item in data)
            {
                dataList.Add(new ProductsInfoMV(item));
            }
            return dataList;
        }
        /// <summary>
        /// 搜索商品
        /// </summary>
        /// /api/ProductsInfoApi/GetProductsAllByName?name=
        /// <param name="name"></param>
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
        /// 根据商品ID获取商品详细信息
        /// </summary>
        /// <param name="name"></param>
        /// /api/ProductsInfoApi/GetProductsAllByID?name=
        /// <returns></returns>
        [HttpGet("{id}")]
        [HttpGet("GetProductsAllByID")]
        public IActionResult GetProductsAllByID(string id)
        {
            var data = _DbService.GetSingleBy<ProductsInfo>(m => m.ID == id, m => m.ProductsCategory);
            return Json(data);
        }
        /// <summary>
        /// 根据商品类型名称查所有商品信息
        /// </summary>
        /// <param name="name"></param>
        /// /api/ProductsInfoApi/GetProductsAllByCName?name=
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
