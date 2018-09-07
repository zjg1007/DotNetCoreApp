using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using Dnc.Entities.Application;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Article;
using System.Collections.Generic;
using Dnc.MvcApp.Filters;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DNC.ViewModels.Article;
using System;
using Dnc.MvcApp.ViewInjections;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace Dnc.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEntityRepository _Service;
        private IHostingEnvironment hostingEnv;

        public HomeController(IEntityRepository service, IHostingEnvironment env)
        {
            _Service = service;
            this.hostingEnv = env;
        }

        //[Route("")]
        //[Route("首页")]
        //[Route("首页/Index")]
        //[HttpGet("{id}")]
        
        public IActionResult Index()
        {
            ViewData["Title"] = "首页";
            return View();
        }
        public IActionResult ProductsInfo()
        {
            return View();
        }
        /// <summary>
        /// 商品管理-获取全部信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProductsInfoAll(int page = 1, int limit = 10)
        {
            string strTrpe = Request.Query["key[name]"] + "";
            LayerPage<ProductsInfoMV> lp = new LayerPage<ProductsInfoMV>();
            var data = new List<ProductsInfo>();
            await Task.Run(() =>
            {
                lp.code = 0;
                lp.msg = "";
                 data = _Service.GetAll<ProductsInfo>(m=>m.ProductsCategory).Where(m => m.Name.Contains(strTrpe)).OrderByDescending(b => b.Name).Skip((page - 1) * limit).Take(limit).ToList();
                lp.count = _Service.GetAll<ProductsInfo>().ToList().Count();
            });
            List<ProductsInfoMV> Info = new List<ProductsInfoMV>();
            foreach (var item in data)
            {
                Info.Add(new ProductsInfoMV(item));
            }
            lp.data = Info;
            return Json(lp);
        }
        /// <summary>
        /// 商品管理-商品发布
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Right()
        {
            HttpContext.Session.SetString("img", "");
            //文章类别
            ViewBag.TypeList = _Service.GetAll<ProductsCategory>().Distinct(p => p.Name).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Right(ProductsInfoMV model,string htmlInfo)
        {
            string str = Request.Form["like[isBest]"];
            string str1 = Request.Form["like[isHot]"];
            string str2 = Request.Form["like[isNew]"];
            string str3 = Request.Form["like[isFree]"];
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "商品发布失败"
            };
            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() =>
                    {
                            ProductsInfo ps = new ProductsInfo();
                            var pc = _Service.GetSingleBy<ProductsCategory>(m => m.ID == model.ProductsCategoryName);
                            model.MapBo(ps);
                            ps.Abstract = htmlInfo;
                            ps.ProductsCategory = pc;
                        if (str == "on" || str == "true") ps.IsBest = true; else ps.IsBest = false;
                        if (str1 == "on" || str1 == "true") ps.IsHot = true; else ps.IsHot = false;
                        if (str2 == "on" || str2 == "true") ps.IsNew = true; else ps.IsNew = false;
                        if (str3 == "on" || str3 == "true") ps.IsFree = true; else ps.IsFree = false;
                        string imgUrl = HttpContext.Session.GetString("img");
                            ps.ImagesUrl = imgUrl;
                            _Service.AddAndSave<ProductsInfo>(ps);
                            logonStatus.IsLogon = true;
                            logonStatus.Message = "商品添加成功";
                            HttpContext.Session.SetString("img", "");
                    });
                }
                catch (System.Exception)
                {
                    logonStatus.IsLogon = false;
                    logonStatus.Message = "商品添加失败";
                }
               
            }
            return Json(logonStatus);
        }
        /// <summary>
        /// 商品管理-修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(string id)
        {
            HttpContext.Session.SetString("img","");
            var data = _Service.GetSingle<ProductsInfo>(Guid.Parse(id), m => m.ProductsCategory);
            //文章类别 傻瓜式排序 按字段值拍在列的首位
            var TypeList_q = _Service.GetAll<ProductsCategory>().Distinct(p => p.Name).ToList();
            TypeList_q.Remove(data.ProductsCategory);
            List<ProductsCategory> filerType = new List<ProductsCategory>();
            filerType.Add(data.ProductsCategory);
            foreach (var item in TypeList_q)
            {
                filerType.Add(item);
            }
            ViewBag.TypeList = filerType;

            ProductsInfoMV proMV = new ProductsInfoMV(data);
            return View(proMV);
        }
        [HttpPost]
        public IActionResult Edit(ProductsInfoMV model, string htmlInfo)
        {
            string str = Request.Form["like[isBest]"];
            string str1 = Request.Form["like[isHot]"];
            string str2 = Request.Form["like[isNew]"];
            string str3 = Request.Form["like[isFree]"];
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "商品编辑失败"
            };
            if (ModelState.IsValid)
            {
                try
                {
                    var data = _Service.GetSingleBy<ProductsInfo>(m=>m.ID==model.ID,m=>m.ProductsCategory);
                    var pc = _Service.GetSingleBy<ProductsCategory>(m => m.ID == model.ProductsCategoryID);
                    model.MapBo(data);
                    data.ProductsCategory = pc;
                    data.Abstract = htmlInfo;
                    if (str == "on"|| str == "true") data.IsBest = true; else data.IsBest = false;
                    if (str1 == "on" || str1 == "true") data.IsHot = true; else data.IsHot = false;
                    if (str2 == "on" || str2 == "true") data.IsNew = true; else data.IsNew = false;
                    if (str3 == "on" || str3 == "true") data.IsFree = true; else data.IsFree = false;
                    string imgUrl = HttpContext.Session.GetString("img");
                    if (model.ImagesUrl != "" && model.ImagesUrl != null && imgUrl != "")
                    {
                        imgUrl = model.ImagesUrl + "," + imgUrl;
                        
                    }
                    else if (model.ImagesUrl != "" && model.ImagesUrl != null && imgUrl == "")
                    {
                        imgUrl = model.ImagesUrl;
                    }
                    data.ImagesUrl = imgUrl;
                    _Service.EditAndSave<ProductsInfo>(data);
                    logonStatus.IsLogon = true;
                    logonStatus.Message = "商品编辑成功";
                }
                catch (System.Exception)
                {
                    logonStatus.IsLogon = false;
                    logonStatus.Message = "商品编辑失败";
                }

            }
            return Json(logonStatus);
        }
        [HttpPost]
        public IActionResult Del(String id, [FromForm] string batchid)
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
                    var data = _Service.GetSingleBy<ProductsInfo>(m => m.ID == id);
                    _Service.DeleteAndSave<ProductsInfo>(data);
                }
                else
                {
                    // 将遵循 json 规格定义的对象数据字符串转换为C#对象
                    var labelInfo = JsonConvert.DeserializeObject<List<ProductsInfo>>(batchid);
                    foreach (var item in labelInfo)
                    {
                        var data = _Service.GetSingleBy<ProductsInfo>(m => m.ID == item.ID);
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

        [HttpGet]
        public IActionResult Left()
        {
            return View();
        }
        [HttpGet]
        public IActionResult top()
        {
            return View();
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "这是 联系方式 的内容。";

            return View();
        }

        public IActionResult Error(string errorMeg)
        {
            ViewBag.ErrorMessage = errorMeg;
            return View();
        }
        public IActionResult test()
        {
            return View();
        }
        [HttpPost]
        public IActionResult upload(string errorMeg, IList<IFormFile> file)
        {
            
            var UploadFiles = new UploadFiles
            {
                code = 1,
                msg ="上传图片失败",
                data=new data { src=null}
            };
            try
            {

                string img = SubImg(file);
                string imgUrl = HttpContext.Session.GetString("img");
                if (imgUrl != null&& imgUrl != "")
                {
                    HttpContext.Session.SetString("img", imgUrl + "," + img);
                }
                else
                {
                    HttpContext.Session.SetString("img", img);
                }
                UploadFiles.code = 0;
                UploadFiles.msg = "上传图片成功";
                UploadFiles.data.src = imgUrl;
            }
            catch (Exception)
            {
                UploadFiles.code = 1;
                UploadFiles.msg = "上传图片失败";
                UploadFiles.data.src = null;
            }
            
            return Json(UploadFiles);
        }
        /// <summary>
        /// 多文件上传
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private string SubImg(IList<IFormFile> files)
        {
            long size = 0;
            string imgUrl = "";
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                               .Parse(file.ContentDisposition)
                                .FileName
                               .Trim('"');
                var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                filename = strDateTime + strRan+ filename;
                imgUrl = "/" + filename;
                filename = hostingEnv.WebRootPath + $@"\{filename}";
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            string img = $"{files.Count} file(s) / { size} bytes uploaded successfully!";
            ViewBag.Message = img;
            return imgUrl;
        }

    }
}