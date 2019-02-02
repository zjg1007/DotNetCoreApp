using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dnc.DataAccessRepository.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dnc.Services.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("DncDemo")]
    public class TestController : Controller
    {
        private readonly IEntityRepository _DbService;
        /// <summary>
        /// 购物车表
        /// </summary>
        /// <param name="service"></param>
        public TestController(IEntityRepository service)
        {
            this._DbService = service;
        }
        /// <summary>
        /// 测试数据
        /// </summary>
        /// <returns></returns>
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
/// <param name="value"></param>
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
