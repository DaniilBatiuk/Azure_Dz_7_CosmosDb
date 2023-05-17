using Newtonsoft.Json;

namespace Azure_Dz_7_CosmosDb.Models
{
    public class Blog
    {
        [JsonProperty("id")]
        public string Id { get; set; } = default!;
        [JsonProperty("name")]
        public string Name { get; set; } = default!;
        [JsonProperty("author")]
        public string Author { get; set; } = default!;
        [JsonProperty("text")]
        public string Text { get; set; } = default!;
    }
}
