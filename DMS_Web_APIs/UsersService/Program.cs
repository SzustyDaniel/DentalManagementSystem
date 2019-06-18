using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UsersService.Data;

namespace UsersService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args).Build();
            using (IServiceScope scope = webHost.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                DataGenerator.Initialize(serviceProvider.GetRequiredService<DbContextOptions<UsersContext>>());
            }
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
