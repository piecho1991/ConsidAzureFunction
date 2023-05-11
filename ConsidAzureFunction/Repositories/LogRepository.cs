using System;
using System.Threading.Tasks;
using Azure.Data.Tables;
using ConsidAzureFunction.Models;
using ConsidAzureFunction.Repositories.Interfaces;

namespace ConsidAzureFunction.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly TableServiceClient _client;

        public LogRepository(TableServiceClient client)
        {
            _client = client;
        }

        public async Task<object> GetAllLogsPeriodAsync(DateTime from, DateTime to)
        {
            var tableClient = await GetTableClientAsync();
            return tableClient.QueryAsync<LogTableEntity>(x => x.Timestamp >= from && x.Timestamp <= to);
        }

        public async Task AddLogAsync(LogTableEntity log)
        {
            var tableClient = await GetTableClientAsync();
            _ = await tableClient.AddEntityAsync(log);
        }

        private async Task<TableClient> GetTableClientAsync()
        {
            var tableClient = _client.GetTableClient(Environment.GetEnvironmentVariable("LogsTableName"));
            await tableClient.CreateIfNotExistsAsync();

            return tableClient;
        }
    }
}
