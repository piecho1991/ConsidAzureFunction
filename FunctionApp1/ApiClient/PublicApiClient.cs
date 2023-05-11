using System;
using System.Net.Http;
using System.Threading.Tasks;
using ConsidAzureFunction.ApiClient.Interfaces;
using ConsidAzureFunction.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ConsidAzureFunction.ApiClient
{
    public class PublicApiClient : HttpClient, IPublicApiClient
    {
        public PublicApiClient()
        {
            BaseAddress = new Uri("https://api.publicapis.org/");
        }

        public async Task<HttpContent> GetRandom()
        {
            var response = await GetAsync("/random?auth=null");
            return response.Content;
        }

    }
}
