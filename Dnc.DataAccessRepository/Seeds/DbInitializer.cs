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

            var appGroup = new List<ApplicationGroup> {
                new ApplicationGroup {Name="一度社员",Decription="一度社员为EG嗨购邀请添加、购物积分=实付金额、并拥有专属邀请二维码.名下二度社员购物返佣比例为实付金额百分之一、积分返送为实付金额百分之十." }
                ,new ApplicationGroup { Name = "二度社员",Decription="二度社员为一度社员邀请添加.购物积分为等值实付金额.二度社员不具备邀请权限以及发展下线权限."}
                ,new ApplicationGroup { Name="普通会员",Decription="普通会员"}
                ,new ApplicationGroup { Name="管理员",Decription="系统管理员"}
            };
            foreach (var item in appGroup)
            {
                context.ApplicationGroup.Add(item);
            }
            context.SaveChanges();

           var userQx = context.ApplicationGroup.Where(m => m.Name == "管理员").FirstOrDefault();
            var appUser = new List<ApplicationUser> {
                new ApplicationUser {ApplicationGroup=userQx, Name="管理员", UserName="Admin",Password="admin123",Photo="",Phone="18877951565",RegisterTime=DateTime.Now,LastVisitTime=DateTime.Now,Safeques="你的名字？",SafeAnswer="Admin",Locked=false,Email="635096109@qq.com",BirthDate=DateTime.Now }
                ,new ApplicationUser {ApplicationGroup=userQx,Name="管理员2", UserName="Admin1",Password="admin123",Photo="",Phone="18877951565",RegisterTime=DateTime.Now,LastVisitTime=DateTime.Now,Safeques="你的名字？",SafeAnswer="Admin",Locked=false,Email="635096109@qq.com",BirthDate=DateTime.Now }
            };
            foreach (var item in appUser)
            {
                context.ApplicationUsers.Add(item);
            }
            context.SaveChanges();
            
           
            

        }
    }
}
