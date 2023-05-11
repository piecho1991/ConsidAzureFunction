using System.Text.Json.Serialization;

namespace ConsidAzureFunction.Models
{
    public class EntryModel
    {
        [JsonPropertyName("API")]
        public string Api { get; set; }
        public string Description { get; set; }
        public string Auth { get; set; }
        [JsonPropertyName("HTTPS")]
        public bool Https { get; set; }
        public string Cors { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
    }
}
