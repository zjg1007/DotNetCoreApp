using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Article;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        private readonly IEntityRepository _DbService;

        public ArticleController(IEntityRepository service)
        {
            this._DbService = service;
        }

       

      

      

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
