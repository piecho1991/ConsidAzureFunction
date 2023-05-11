using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Data.Tables;
using ConsidAzureFunction.Storage.Abstraction;

namespace ConsidAzureFunction.Storage.Storage
{
    internal class TableStorageService<T> : ITableStorageService<T>
    {

        public Task<T[]> GetListOfLogsAsync(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        //private async Task<TableClient> GetTableClient()
        //{
        //    var serviceClient = new TableServiceClient(_configuration["StorageConnectionString"]);
        //    var tableClient = serviceClient.GetTableClient(TableName);
        //    await tableClient.CreateIfNotExistsAsync();
        //    return tableClient;
        //}
    }
}
