using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SRS.Infrastructure.Options;

namespace SRS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<DatastoreOptions>(Configuration.GetSection("DataStore"));
            //public static class ServiceCollectionExtensions
            //{
            //    public static TOptions ConfigureOptions<TOptions>(this IServiceCollection services, IConfigurationRoot configuration, string sectionName) where TOptions : class, new()
            //    {
            //        IConfigurationSection section = configuration.GetSection(sectionName);
            //        TOptions instance = Activator.CreateInstance<TOptions>();
            //        section.Bind((object)instance);
            //        services.Configure<TOptions>((IConfiguration)section);
            //        return instance;
            //    }
            //}


            services
                .AddMvcCore()
                .AddJsonFormatters();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
