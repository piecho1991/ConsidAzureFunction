using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsidAzureFunction.Repositories.Interfaces
{
    public interface IPayloadBlobRepository
    {
        Task AddPayloadAsync(string key, Stream payload);
        Task<object> GetPayloadByKey(string key);
    }
}
