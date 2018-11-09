using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dnc.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Dnc.DataAccessRepository.Context;
using Dnc.DataAccessRepository.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace Dnc.Services
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

           // BusinessDataAccess.Set();
        }
        
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //添加跨域访问授权处理
            //services.AddCors(option => option.AddPolicy("DncDemo", p => p.WithOrigins("http://813zjg.top:5002",
            //    "http://localhost:8166").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            services.AddCors(option => option.AddPolicy("DncDemo", builder =>
            {
                builder.AllowAnyOrigin() //允许任何来源的主机访问
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();//指定处理cookie
            }));
            //注入 DbContext 对应的数据库连接实例
            services.AddDbContext<EntityDbContext>();

            // 注入 数据服务接口实例;
            services.AddTransient<IEntityRepository, EntityRepository>();

            // 注入Session组件
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.Name = ".MyCoreApp";
            });
            //注入接口文档
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = "Shopping.Core API",
                    Description = "商城接口说明",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Shopping.Core",
                        Email = "xx@xx.com",
                        Url = "http://www.baidu.com" }
                });
                //这里是配置好xml文件之后，去读取这个文件
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Dnc.Services.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                //下面是添加实体显示
                //注意，不能再HttpGet中，用实体类做参数，会报错
                var xmlModelPath = Path.Combine(basePath, "Dnc.Entities.xml");//这个就是Model层的xml文件名

                c.IncludeXmlComments(xmlModelPath);
            });
           
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                //位置Swagger
                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                    //c.RoutePrefix = "";//路径配置，设置为空，表示直接访问该文件
                });
                #endregion
            }
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // 启用 Session，添加 Session 的引用
            app.UseSession();

            app.UseMvc();
            app.UseCors("DncDemo");
        }
    }
}
