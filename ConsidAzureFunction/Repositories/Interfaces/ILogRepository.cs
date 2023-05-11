using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsidAzureFunction.Models;

namespace ConsidAzureFunction.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<object> GetAllLogsPeriodAsync(DateTime from, DateTime to);
        Task AddLogAsync(LogTableEntity log);
    }
}
