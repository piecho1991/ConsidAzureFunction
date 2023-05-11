using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsidAzureFunction.Storage.Abstraction
{
    public interface ITableStorageService<T>
    {
        Task<T[]> GetListOfLogsAsync(DateTime from, DateTime to);
    }
}
