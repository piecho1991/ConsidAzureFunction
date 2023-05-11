using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConsidAzureFunction.ApiClient;
using ConsidAzureFunction.ApiClient.Interfaces;
using ConsidAzureFunction.Repositories;
using ConsidAzureFunction.Repositories.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(ConsidAzureFunction.Startup))]

namespace ConsidAzureFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.AddTableServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
                clientBuilder.AddBlobServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
            });

            builder.Services.AddLogging();
            builder.Services.AddScoped<ILogRepository, LogRepository>();
            builder.Services.AddScoped<IPayloadBlobRepository, PayloadBlobRepository>();
            builder.Services.AddScoped<IPublicApiClient, PublicApiClient>();
        }
    }
}
