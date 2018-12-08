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
using Microsoft.Extensions.Caching.Memory;
using Dnc.Services.AuthHelper.OverWrite;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Dnc.Services.SwaggerHelper.CustomApiVersion;

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
        private const string ApiName = "Dnc.Services";
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
                //c.SwaggerDoc("v1", new Info
                //{
                //    Version = "v0.1.0",
                //    Title = "Shopping.Core API",
                //    Description = "商城接口说明",
                //    TermsOfService = "None",
                //    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Dnc.Services",
                //        Email = "xx@xx.com",
                //        Url = "http://www.baidu.com" }
                //});

                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new Info
                    {
                        // {ApiName} 定义成全局变量，方便修改
                        Version = version,
                        Title = $"{ApiName} 接口文档",
                        Description = $"{ApiName} HTTP API " + version,
                        TermsOfService = "None",
                        Contact = new Contact { Name = "Blog.Core", Email = "Blog.Core@xxx.com", Url = "https://www.jianshu.com/u/94102b59cc2a" }
                    });
                });


                //就是这里

                //这里是配置好xml文件之后，去读取这个文件
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Dnc.Services.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                //下面是添加实体显示
                //注意，不能再HttpGet中，用实体类做参数，会报错
                var xmlModelPath = Path.Combine(basePath, "Dnc.Entities.xml");//这个就是Model层的xml文件名

                c.IncludeXmlComments(xmlModelPath);


                #region Token绑定到ConfigureServices
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                var security = new Dictionary<string, IEnumerable<string>> { { "Dnc.Services", new string[] { } }, };
                c.AddSecurityRequirement(security);
                //方案名称“Dnc.Services.xml”可自定义，上下一致即可
                c.AddSecurityDefinition("Dnc.Services", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion
            });

            #endregion

            #region Token服务注册
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                options.AddPolicy("Client1", policy => policy.RequireRole("Client1").Build());
                options.AddPolicy("Client2", policy => policy.RequireRole("Client2").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
            });
            #endregion
            //认证
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
          .AddJwtBearer(o =>
          {
              o.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,//是否验证Issuer
                  ValidateAudience = true,//是否验证Audience 
                  ValidateIssuerSigningKey = true,//是否验证IssuerSigningKey 
                  ValidIssuer = "Dnc.Services",
                  ValidAudience = "wr",
                  ValidateLifetime = true,//是否验证超时  当设置exp和nbf时有效 同时启用ClockSkew 
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtHelper.secretKey)),
                  //注意这是缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间
                  ClockSkew = TimeSpan.FromSeconds(30)

              };
          });

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

            //将TokenAuth注册中间件
            //app.UseMiddleware<JwtTokenAuth>();//注意此授权方法已经放弃，请使用下边的官方验证方法。但是如果你还想传User的全局变量，还是可以继续使用中间件
            app.UseAuthentication();

            app.UseMvc();
            app.UseCors("DncDemo");

            
        }
    }
}
