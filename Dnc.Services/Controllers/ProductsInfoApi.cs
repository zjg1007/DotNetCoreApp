using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Article;
using DNC.ApiModel.Article;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Services.Controllers
{
    /// <summary>
    /// 商品信息表
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("DncDemo")]
    public class ProductsInfoApi: Controller
    {
        private readonly IEntityRepository _DbService;
        /// <summary>
        /// 商品信息表
        /// </summary>
        /// <param name="service"></param>
        public ProductsInfoApi(IEntityRepository service)
        {
            this._DbService = service;
        }
        /// <summary>
        /// 获取全部商品信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        /// <param name="name">商品名称</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProductsAllByName")]
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
        /// <param name="id">商品ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProductsAllByID")]
        public IActionResult GetProductsAllByID(string id)
        {
            var data = _DbService.GetSingleBy<ProductsInfo>(m => m.ID == id, m => m.ProductsCategory);
            return Json(data);
        }
        /// <summary>
        /// 根据商品类型名称查所有商品信息
        /// </summary>
        /// <param name="name">商品类型名称</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProductsAllByCName")]
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
    }
}
