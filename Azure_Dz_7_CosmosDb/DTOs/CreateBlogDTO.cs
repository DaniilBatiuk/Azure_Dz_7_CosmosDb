using Newtonsoft.Json;

namespace Azure_Dz_7_CosmosDb.DTOs
{
    public class CreateBlogDTO
    {
        public string Name { get; set; } = default!;

        public string Author { get; set; } = default!;
        public string Text { get; set; } = default!;
    }
}
