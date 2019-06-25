using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueueService.AzureStorage;
using QueueService.AzureStorage.Repository;
using QueueService.AzureStorage.StorageManagement;
using QueueService.SignalR;

namespace QueueService
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AzureStorageSettings>(_configuration.GetSection("Data:Azure"));
            services.AddScoped<IQueueStorageService, QueueStorageService>();
            services.AddScoped<IQueueRepository, QueueRepository>();
            services.AddMvc();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseSignalR(routes =>
            {
                routes.MapHub<QueueNotificationsHub>("/QueueNotificationsHub");
            });
            app.UseMvc();
        }
    }
}
