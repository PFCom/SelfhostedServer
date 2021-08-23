using System;
using System.Linq;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using PFCom.Selfhosted.DataAccess.EFCore;
using PFCom.Selfhosted.DataAccess.EFCore.Providers;

namespace PFCom.Selfhosted.Host.Web.Tests
{
    public class WebApiFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Remove(services.SingleOrDefault(x => x.ServiceType == typeof(DataContext)));

                Mock<IConfiguration> dbSettingsMock = new Mock<IConfiguration>();
                
                services.AddScoped<DataContext>(x => new SqliteContext("Filename=Test.db"));

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DataContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<WebApiFactory<TStartup>>>();

                    try
                    {
                        db.Database.Migrate();
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e, "An error occurred when migrating database. Error: {Message}", e.Message);
                    }
                }
            });
        }
    }
}
