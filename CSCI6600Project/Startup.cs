using CSCI6600Project.DataGeneration;
using CSCI6600Project.Models;
using CSCI6600Project.Models.Context;
using CSCI6600Project.Models.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSCI6600Project.Cache;

namespace CSCI6600Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<csci6600Context>(options =>
                options.UseSqlServer(Configuration["ConnectionStringNonIndexed"]));
            services.AddDbContext<csci6600_indexedContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStringIndexed"]));
            var config = Configuration["RedisConnectionString"];
            services.AddDistributedRedisCache(options =>  
            {
                options.Configuration = config;
                options.InstanceName = "master";
            });

            // Services
            services.AddTransient<IDataService,DataService>();
            services.AddTransient<ICacheManager,CacheManager>();
            services.AddTransient<IGeneratorService,GeneratorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
