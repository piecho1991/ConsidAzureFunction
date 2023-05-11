using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsidAzureFunction.ApiClient.Interfaces
{
    public interface IPublicApiClient
    {
        Task<HttpContent> GetRandom();
    }
}
