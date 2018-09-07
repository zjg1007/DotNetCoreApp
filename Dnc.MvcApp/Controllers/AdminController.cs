using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dnc.MvcApp.Filters;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Article;
using Dnc.Entities.Application;
using Dnc.MvcApp.ViewInjections;
using Newtonsoft.Json;

namespace Dnc.MvcApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEntityRepository _context;

        public AdminController(IEntityRepository context)
        {
            _context = context;
        }

        [HttpGet]
        //[AppAuthorityFilter(new string[] { "授权用户组", "系统管理员组" })]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Right()
        {
            return View();
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
    }
}
