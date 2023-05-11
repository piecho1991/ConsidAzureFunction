using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using ConsidAzureFunction.Models;
using ConsidAzureFunction.Repositories.Interfaces;

namespace ConsidAzureFunction.Repositories
{
    public class PayloadBlobRepository : IPayloadBlobRepository
    {
        private readonly BlobServiceClient _client;

        public PayloadBlobRepository(BlobServiceClient client)
        {
            _client = client;
        }

        public async Task AddPayloadAsync(string key, Stream payload)
        {
            var blobClient = await GetBlobClientAsync(key);
            _ = await blobClient.UploadAsync(payload);
        }

        public async Task<object> GetPayloadByKey(string key)
        {
            var blobClient = await GetBlobClientAsync(key);
            var items = await blobClient.DownloadContentAsync();

            if (items.HasValue)
            {
                return items.Value.Content.ToString();
            }

            return null;
        }

        private async Task<BlobClient> GetBlobClientAsync(string key)
        {
            var blobContainerClient = _client.GetBlobContainerClient(Environment.GetEnvironmentVariable("PayloadsContainerName"));
            await blobContainerClient.CreateIfNotExistsAsync();

            return blobContainerClient.GetBlobClient(key);
        }
    }
}
