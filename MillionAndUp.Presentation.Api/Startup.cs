using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MillionAndUp.Core.Aplication;
using MillionAndUp.Infraestructure.Identity;
using MillionAndUp.Infraestructure.Persistence;
using MillionAndUp.Infraestructure.Shared;
using MillionAndUp.Presentation.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MillionAndUp.Presentation.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string _MyCors = "MyCors";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddIdentityInfraestructure(Configuration);
            services.AddPersistenceInfraestructure(Configuration);
            services.AddSharedInfraestructure(Configuration);
            services.AddControllers();
            services.AddApiVersionExtension();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MillionAndUp.Presentation.Api", Version = "v1" });
                
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: _MyCors, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MillionAndUp.Presentation.Api v1"));

            }


            app.UseHttpsRedirection();
            app.UseCors(_MyCors);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseErrorHandlingMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
