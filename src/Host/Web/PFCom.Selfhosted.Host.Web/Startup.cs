using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PFCom.Selfhosted.Application.ServiceRegistering;
using PFCom.Selfhosted.DataAccess;
using PFCom.Selfhosted.DataAccess.EFCore;
using PFCom.Selfhosted.DataAccess.EFCore.Providers;

namespace PFCom.Selfhosted.Host.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void configureServices_database(IServiceCollection services)
        {
            string sqlServerUse = this.Configuration["DbType"];
            string connStr = this.Configuration.GetConnectionString("DefaultConnection");

            if (sqlServerUse == "mssql")
            {
                services.AddScoped<DataContext>(x => new MssqlContext(connStr));
            }
            else if(sqlServerUse == "sqlite")
            {
                services.AddScoped<DataContext>(x => new SqliteContext(connStr));
            }
            else if (sqlServerUse == "mysql")
            {
                services.AddScoped<DataContext>(x => new MysqlContext(connStr));
            }
            else if (sqlServerUse == "postgre")
            {
                services.AddScoped<DataContext>(x => new PostgreContext(connStr));
            }
            
            services.AddScoped(typeof(IBaseRepository<>), typeof(EfCoreRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.configureServices_database(services);

            services.RegisterRepositories();

            services.RegisterApplicationServices();

            services.AddTransient(typeof(Lib.AutoMapping.IObjectMapper<,>), typeof(Lib.AutoMapping.Impl.ObjectMapper<,>));

            services.AddApiVersioning();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PFCom.Selfhosted.Host.Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PFCom.Selfhosted.Host.Web v1"));
            }

            app.UseApiVersioning();

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
