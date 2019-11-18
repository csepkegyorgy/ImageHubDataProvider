namespace LogMeOut.ImageHub.DataProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LogMeOut.ImageHub.BusinessLogic.Logic;
    using LogMeOut.ImageHub.BusinessLogic.Logic.Base;
    using LogMeOut.ImageHub.BusinessLogic.Query;
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Repository.Interfaces;
    using LogMeOut.ImageHub.Repository.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            AddDependencies(services);

            services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", builder => builder
                    .WithOrigins("http://localhost:3000", "http://192.168.0.100:3000")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithHeaders("Accept", "Content-Type", "Origin", "X-My-Header"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void AddDependencies(IServiceCollection services)
        {
            services.AddScoped<IImageHubRepository, ImageHubRepository>();
            services.AddScoped<IBaseLogicDependency, BaseLogicDependency>();

            services.AddScoped<IFtpDownloaderLogic, FtpDownloaderLogic>();
            services.AddScoped<IFtpUploaderLogic, FtpUploaderLogic>();
            services.AddScoped<IPostQueryLogic, PostQueryLogic>();
            services.AddScoped<IAuthenticationLogic, AuthenticationLogic>();
            services.AddScoped<IPostSubmitterLogic, PostSubmitterLogic>();
            services.AddScoped<IUserRelationHandlerLogic, UserRelationHandlerLogic>();
        }
    }
}
