using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ServiceDepartamentosRDSMSql.Data;
using ServiceDepartamentosRDSMSql.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ServiceDepartamentosRDSMSql
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
            String cadenaaws =
                this.Configuration.GetConnectionString("awsmysqlhospital");

            services.AddTransient<RepositoryDepartamentos>();
            services.AddDbContextPool<DepartamentoContext>(
                options => options.UseMySql(
                    cadenaaws, ServerVersion.AutoDetect(cadenaaws)));

            //HABILITAMOS CORS EN EL SERVICIO
            services.AddCors(options =>
            options.AddPolicy("AllowOrigin", c => c.AllowAnyOrigin()));
            
            services.AddSwaggerGen( options =>
                {
                    options.SwaggerDoc(name: "v1", new OpenApiInfo
                    {
                        Title = "Api MySql AWS",
                        Version = "v1",
                        Description = "Api MySql AWS"
                    });
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);
                });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json", name: "Api v1");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
