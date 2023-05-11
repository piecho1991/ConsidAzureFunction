using System.Text.Json.Serialization;

namespace ConsidAzureFunction.Models
{
    
    public class RandomModel
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("entries")]
        public EntryModel[] Entries { get; set; }
    }
}
