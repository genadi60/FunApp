using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using AngleSharp;
using AngleSharp.Parser.Html;
using CommandLine;
using FunApp.Data;
using FunApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

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
                serviceProvider = serviceScope.ServiceProvider;
                SandboxCode(serviceProvider);
                ////    FunAppContextSeeder.Seed(dbContext, serviceScope.ServiceProvider);
            }
            
            using (var serviceScope = serviceProvider.CreateScope())
            {
                
                //Parser.Default.ParseArguments<SandboxOptions>(args).MapResult(
                //    (SandboxOptions opts) => SandboxCode(opts, serviceProvider),
                //    _ => 255);
            }
        }

        private static void SandboxCode(IServiceProvider serviceProvider)
        {
            var webClient = new WebClient() {Encoding = Encoding.GetEncoding(1251)};
            var parser = new HtmlParser();
            
            for (int i = 6741; i < 10000; i++)
            {
                var context = serviceProvider.GetService<FunAppContext>();

                var address = "http://fun.dir.bg/vic_open.php?id=" + i;
                var html = webClient.DownloadString(address);
                var document = parser.Parse(html);

                if (document.QuerySelector("#newsbody") == null)
                {
                    
                    continue;
                }

                var content = document.QuerySelector("#newsbody")?.TextContent.Trim();
                var categoryName = document.QuerySelector(".tag-links-left a")?.TextContent.Trim();

                if (!string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(categoryName))
                {
                    var category = context.Categories.FirstOrDefault(c => c.Name.Equals(categoryName));
                    
                    if (category == null)
                    {
                       category = new Category
                       {
                           Name = categoryName
                       };
                    }
                    
                    var joke = new Joke
                    {
                        Category = category,
                        Content = content
                    };

                    context.Jokes.Add(joke);
                    context.SaveChanges();
                    Console.WriteLine($"{i} => {categoryName}");
                }
            }
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
