using System;
using Microsoft.Extensions.Configuration;
using RestSharp;
using TigerGraph;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using TigerGraph.Models;

namespace TigerGraphAzureLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            //the logger by adding a parameter (ILogger<MyClass>)
            var logger = serviceProvider.GetService<ILogger<Program>>();

            logger.LogInformation("TigerGraph Azure blob storage update");

            ;
            var tgLogger = new TigerGraph.ConsoleLogger();
            TigerGraph.Base.Runtime.SetLogger(tgLogger);
            var apiClient = new ApiClient();
            // SimpleEcho  check
            var response = apiClient.Echo().Result;

            // GSQL load

        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(configure => configure.AddConsole());

            if (configuration["LOG_LEVEL"] == "true")
            {
                services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace);
            }
            else
            {
                services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Error);
            }
        }

    }
}
