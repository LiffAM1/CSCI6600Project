using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CSCI6600Project.DB;
using CSCI6600Project.Models;
using CSCI6600Project.Models.Base;
using CSCI6600Project.Models.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<csci6600Context>();
                    context.Database.EnsureCreated();

                    var indexedContext = services.GetRequiredService<csci6600_indexedContext>();
                    indexedContext.Database.EnsureCreated();

                    // One-time initialization - already complete
                    // DBInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        var builtConfig = config.Build();
                        var secretClient = new SecretClient(
                            new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                            new DefaultAzureCredential());
                        config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
}
