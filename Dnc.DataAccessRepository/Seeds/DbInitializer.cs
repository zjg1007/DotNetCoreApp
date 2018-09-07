using Dnc.DataAccessRepository.Context;
using Dnc.Entities.Application;
using Dnc.Entities.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.DataAccessRepository.Seeds
{
    public static class DbInitializer
    {
        public static void Initialze(EntityDbContext context)
        {
            //如果没有数据就创建数据库,数据存在就跳出执行
            context.Database.EnsureCreated();
            if (context.ApplicationUsers.Any())
                return;
            
           
            
            var appUser = new List<ApplicationUser> {
                new ApplicationUser {Name="管理员", UserName="Admin",Password="admin123",Photo="",Phone="18877951565",RegisterTime=DateTime.Now,LastVisitTime=DateTime.Now,Safeques="你的名字？",SafeAnswer="Admin",Locked=false,Email="635096109@qq.com",BirthDate=DateTime.Now }
            };
            foreach (var item in appUser)
            {
                context.ApplicationUsers.Add(item);
            }
            context.SaveChanges();
            
           
            

        }
    }
}
