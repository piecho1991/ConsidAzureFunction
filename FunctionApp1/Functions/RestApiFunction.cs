using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Azure;
using ConsidAzureFunction.Other;
using ConsidAzureFunction.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ConsidAzureFunction.Functions
{
    public class RestApiFunction
    {
        private readonly ILogger<RestApiFunction> _logger;
        private readonly ILogRepository _logRepository;
        private readonly IPayloadBlobRepository _payloadBlobRepository;

        public RestApiFunction(ILogger<RestApiFunction> log, ILogRepository logRepository, IPayloadBlobRepository payloadBlobRepository)
        {
            _logger = log;
            _logRepository = logRepository;
            _payloadBlobRepository = payloadBlobRepository;
        }

        [FunctionName("RestApiFunction_GetLogs")]
        [OpenApiOperation(operationId: "GetLogs")]
        [OpenApiParameter(name: "from", In = ParameterLocation.Query, Required = true, Type = typeof(string))]
        [OpenApiParameter(name: "to", In = ParameterLocation.Query, Required = true, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        public async Task<IActionResult> GetLogs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getlogs")] HttpRequest req)
        {
            try
            {
                DateTime from = DateTime.Parse(req.Query["from"]);
                DateTime to = DateTime.Parse(req.Query["to"]);

                var items = await _logRepository.GetAllLogsPeriodAsync(from, to);

                return new OkObjectResult(items);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, "");

                return new BadRequestErrorMessageResult("DateTime format is incorrect");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "");

                return new InternalServerErrorMessageResult("Something went wrong :(");
            }
        }

        [FunctionName("RestApiFunction_GetLogPayload")]
        [OpenApiOperation(operationId: "GetLogPayload")]
        [OpenApiParameter(name: "key", In = ParameterLocation.Query, Required = true, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        public async Task<IActionResult> GetLogPayload(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getpayload")] HttpRequest req)
        {
            try
            {
                string key = req.Query["key"];

                var item = await _payloadBlobRepository.GetPayloadByKey(key);

                return new OkObjectResult(item);
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(ex, "");

                return new BadRequestErrorMessageResult("The specified payload does not exists");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "");

                return new InternalServerErrorMessageResult("Something went wrong :(");
            }
        }

    }
}

