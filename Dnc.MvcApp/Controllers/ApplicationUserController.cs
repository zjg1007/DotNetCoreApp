using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Application;
using Dnc.MvcApp.ViewInjections;
using DNC.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dnc.MvcApp.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly IEntityRepository _Service;

        public ApplicationUserController(IEntityRepository service)
        {
            _Service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取用户全部信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IActionResult> UserInfoAll(int page = 1, int limit = 10)
        {
            string strTrpe = Request.Query["key[name]"] + "";
            LayerPage<ApplicationUserMV> lp = new LayerPage<ApplicationUserMV>();
            var data = new List<ApplicationUser>();
            await Task.Run(() =>
            {
                lp.code = 0;
                lp.msg = "";
                data = _Service.GetAll<ApplicationUser>(m => m.ApplicationGroup).Where(m => m.Name.Contains(strTrpe)).OrderByDescending(b => b.Name).Skip((page - 1) * limit).Take(limit).ToList();
                lp.count = _Service.GetAll<ApplicationUser>().ToList().Count();
            });
            List<ApplicationUserMV> Info = new List<ApplicationUserMV>();
            foreach (var item in data)
            {
                Info.Add(new ApplicationUserMV(item));
            }
            lp.data = Info;
            return Json(lp);
        }

        /// <summary>
        /// 商品管理-修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var data = _Service.GetSingle<ApplicationUser>(Guid.Parse(id),m=>m.ApplicationGroup);
            var dataVM = new ApplicationUserMV(data);
            ViewData["group"] = _Service.GetAll<ApplicationGroup>().ToList();
            return View(dataVM);
        }
        [HttpPost]
        public IActionResult Edit(ApplicationUserMV model)
        {
            if (ModelState.IsValid)
            {
                var and  = HttpContext.Request.Form["sex"];
                var and1 = HttpContext.Request.Form["sex1"];
                var mm = _Service.GetSingleBy<ApplicationUser>(m => m.ID == model.ID);
                if (and1.ToString()=="0")
                    mm.Locked = true;
                else
                    mm.Locked = false;
                var apgruop =_Service.GetSingleBy<ApplicationGroup>(m => m.ID == and);
                mm.ApplicationGroup = apgruop;
                _Service.EditAndSave<ApplicationUser>(mm);
                return Redirect("Index");
            }
            else
            {
                return View(model);
            }
            #region 到要经过执行controller里方法后 显示出页面。
            //return RedirectToAction("Index");//可跳出本controller
            //return RedirectToRoute(new {controller="Home",action="Index"});//可跳出本controller
           // return RedirectToRoute(new { controller = "ApplicationUser", action = "Index" });
            
            //Response.Redirect("Index");//只能使用本controller下的方法名称。返回值为void
            //return Redirect("Index");//只能使用本controller下的方法名称。
            //return Content("<script>alert('操作成功。';window.location.href='/home/index')");
            #endregion
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
                    var data = _Service.GetSingleBy<ApplicationUser>(m => m.ID == id);
                    _Service.DeleteAndSave<ApplicationUser>(data);
                }
                else
                {
                    // 将遵循 json 规格定义的对象数据字符串转换为C#对象
                    var labelInfo = JsonConvert.DeserializeObject<List<ApplicationUser>>(batchid);
                    foreach (var item in labelInfo)
                    {
                        var data = _Service.GetSingleBy<ApplicationUser>(m => m.ID == item.ID);
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
    }
}