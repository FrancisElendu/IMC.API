using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMC.API.Services;
using IMC.API.Settings;
using IMC.API.TaxJarHttpClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IMC.API
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

            services.Configure<TaxJarSettings>(Configuration.GetSection(nameof(TaxJarSettings)));
            services.AddScoped<ITaxJarService, TaxJarService>();
            //services.AddHttpClient();
            //services.AddHttpClient<ITaxJarService, TaxJarService>();
            services.AddHttpClient<IHttpClientService, HttpClientService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
