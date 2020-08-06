using AnimaDigital.Api.Swagger;
using AnimaDigital.Application;
using AnimaDigital.Application.Application;
using AnimaDigital.Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AnimaDigital.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
         
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAnimaApplication();

            var xmlName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            services.AddSwagger(xmlName);

            services.AddApiBehavior();
        }
         
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwaggerApp();
            app.UseCorsApp();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
