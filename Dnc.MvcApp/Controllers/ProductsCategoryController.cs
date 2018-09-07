using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Application;
using Dnc.Entities.Article;
using Dnc.MvcApp.ViewInjections;
using DNC.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dnc.MvcApp.Controllers
{
    public class ProductsCategoryController : Controller
    {
        private readonly IEntityRepository _Service;

        public ProductsCategoryController(IEntityRepository service)
        {
            _Service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取分类信息
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页数据</param>
        /// <returns></returns>
        public async Task<IActionResult> typeInfo(int page = 1, int limit = 10)
        {
            string strTrpe = Request.Query["key[name]"] + "";
            LayerPage<ProductsCategory> lp = new LayerPage<ProductsCategory>();
            await Task.Run(() =>
            {
                lp.code = 0;
                lp.msg = "";
                var data = _Service.GetAll<ProductsCategory>().Where(m => m.Name.Contains(strTrpe)).OrderByDescending(b => b.Name).Skip((page - 1) * limit).Take(limit).ToList();
                lp.count = _Service.GetAll<ProductsCategory>().ToList().Count();
                lp.data = data;
            });
            return Json(lp);
        }
        /// <summary>
        /// 分类管理-删除
        /// </summary>
        /// <param name="id">单个删除id</param>
        /// <param name="batchid">批量删除id</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult typeDel(String id, [FromForm] string batchid)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "删除失败！"
            };
            try
            {
                if (string.IsNullOrEmpty(batchid) && id != null)
                {
                    var data = _Service.GetSingleBy<ProductsCategory>(m => m.ID == id);
                    _Service.DeleteAndSave<ProductsCategory>(data);
                }
                else
                {
                    // 将遵循 json 规格定义的对象数据字符串转换为C#对象
                    var labelInfo = JsonConvert.DeserializeObject<List<ProductsCategory>>(batchid);
                    foreach (var item in labelInfo)
                    {
                        var data = _Service.GetSingleBy<ProductsCategory>(m => m.ID == item.ID);
                        _Service.Delete(data);
                    }
                    _Service.Save();
                }

                logonStatus.IsLogon = true;
                logonStatus.Message = "删除成功";
            }
            catch (Exception)
            {
                logonStatus.IsLogon = false;
                logonStatus.Message = "删除失败";
            }

            return Json(logonStatus);
        }
        /// <summary>
        /// 分类管理-编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult typeEdit(ProductsCategoryMV model)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "编辑失败！"
            };
            try
            {
                var data = _Service.GetSingleBy<ProductsCategory>(m => m.ID == model.ID);
                model.MapBo(data);
                _Service.EditAndSave<ProductsCategory>(data);
                logonStatus.IsLogon = true;
                logonStatus.Message = "编辑成功";
            }
            catch (Exception)
            {
                logonStatus.IsLogon = false;
                logonStatus.Message = "编辑失败";
            }

            return Json(logonStatus);
        }
        /// <summary>
        /// 分类管理-新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult typeInsert()
        {
            return View();
        }
        [HttpPost]
        public IActionResult typeInsert(ProductsCategory model)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "添加失败！"
            };
            try
            {
                ProductsCategory file = new ProductsCategory();
                file = model;
                _Service.AddAndSave<ProductsCategory>(file);
                logonStatus.IsLogon = true;
                logonStatus.Message = "添加成功！";
            }
            catch (Exception)
            {
                logonStatus.IsLogon = false;
                logonStatus.Message = "添加失败！";
            }

            return Json(logonStatus);
        }
    }
}