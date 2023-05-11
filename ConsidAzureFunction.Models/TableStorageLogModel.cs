using System;
using Azure;
using Azure.Data.Tables;

namespace ConsidAzureFunction.Models
{
    public class TableStorageLogModel : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string Text { get; set; }
        public TableStorageStatusEnum Status { get; set; }
    }

    public enum TableStorageStatusEnum
    {
        Success,
        Failure
    }
}
