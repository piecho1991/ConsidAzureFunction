using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ConsidAzureFunction.ApiClient;
using ConsidAzureFunction.ApiClient.Interfaces;
using ConsidAzureFunction.Models;
using ConsidAzureFunction.Repositories;
using ConsidAzureFunction.Repositories.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ConsidAzureFunction.Functions
{
    public class TimerFunction
    {
        private readonly ILogger<TimerFunction> _logger;
        private readonly IPublicApiClient _publicApiClient;
        private readonly ILogRepository _logRepository;
        private readonly IPayloadBlobRepository _payloadBlobRepository;

        public TimerFunction(IPublicApiClient publicApiClient, ILogRepository logRepository, IPayloadBlobRepository payloadBlobRepository, ILogger<TimerFunction> logger)
        {
            _publicApiClient = publicApiClient;
            _logRepository = logRepository;
            _payloadBlobRepository = payloadBlobRepository;
            _logger = logger;
        }

        [FunctionName("TimerFunction")]
        public async Task Run(
            [TimerTrigger("0 0 * * * *", RunOnStartup = true)] TimerInfo myTimer)
        {
            var id = Guid.NewGuid();
            var log = new LogTableEntity
            {
                PartitionKey = "TimerTrigger",
                RowKey = id.ToString(),
                Timestamp = DateTimeOffset.UtcNow,
                Status = TableStorageStatusEnum.Success,
                Text = "Operation successful"
            };

            try
            {
                var ret = await _publicApiClient.GetRandom();


                log.Status = TableStorageStatusEnum.Success;
                log.Text = "Operation successful";

                await _logRepository.AddLogAsync(log);
                await _payloadBlobRepository.AddPayloadAsync(log.RowKey, await ret.ReadAsStreamAsync());
            }
            catch (Exception ex)
            {
                // there is no sense to save data in blob because there is no payload

                _logger.LogCritical(ex, "");

                log.Status = TableStorageStatusEnum.Failure;
                log.Text = $"Operation failed. Message: {ex.Message}";

                await _logRepository.AddLogAsync(log);
            }
        }
    }
}
