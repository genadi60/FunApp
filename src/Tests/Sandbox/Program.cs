using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CommandLine;
using FunApp.Data;
using FunApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sandbox
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            // Seed data on application startup
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<FunAppContext>();
                dbContext.Database.Migrate();
            ////    FunAppContextSeeder.Seed(dbContext, serviceScope.ServiceProvider);
           }
            
            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;

                Parser.Default.ParseArguments<SandboxOptions>(args).MapResult(
                    (SandboxOptions opts) => SandboxCode(opts, serviceProvider),
                    _ => 255);
            }
        }

        private static int SandboxCode(SandboxOptions options, IServiceProvider serviceProvider)
        {
            var sw = Stopwatch.StartNew();
            ////var settingsService = serviceProvider.GetService<ISettingsService>();
            var settingsService = serviceProvider.GetService <FunAppContext>();
            Console.WriteLine($"Count of users: {settingsService.Users.Count()}");
            Console.WriteLine(sw.Elapsed);
            return 0;
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddDbContext<FunAppContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseLoggerFactory(new LoggerFactory());
            });
                
                    

            services
                .AddIdentity<FunAppUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<FunAppContext>();
        ////        .AddUserStore<FunAppUserStore>()
        ////        .AddRoleStore<FunAppRoleStore>()
        ////        .AddDefaultTokenProviders();
        ////
        ////    services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
        ////    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        ////    services.AddScoped<IDbQueryRunner, DbQueryRunner>();
        ////
        ////    // Application services
        ////    services.AddTransient<IEmailSender, NullMessageSender>();
        ////    services.AddTransient<ISmsSender, NullMessageSender>();
        ////    services.AddTransient<ISettingsService, SettingsService>();
        }
    }
}
