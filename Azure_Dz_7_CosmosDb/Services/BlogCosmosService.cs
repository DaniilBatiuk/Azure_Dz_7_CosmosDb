using Azure_Dz_7_CosmosDb.DTOs;
using Azure_Dz_7_CosmosDb.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Azure_Dz_7_CosmosDb.Services
{
    public class BlogCosmosService : IBlogDbService
    {
        private readonly Container container;
        public BlogCosmosService(CosmosClient client, string databeseId, string containerId)
        {
            container = client.GetContainer(databeseId, containerId);
        }

        public async Task<Blog> AddAsync(CreateBlogDTO blogDTO)
        {
            Blog newBlog = new Blog
            {
                Id = Guid.NewGuid().ToString(),
                Author = blogDTO.Author,
                Name = blogDTO.Name,
                Text = blogDTO.Text

            };
            ItemResponse<Blog> response = await container.CreateItemAsync(newBlog, new PartitionKey(newBlog.Name));
            return response.Resource;
        }

        public async Task<HttpStatusCode> DeleteAsync(string id, string key)
        {
            ItemResponse<Blog> resp = await container.DeleteItemAsync<Blog>(id, new PartitionKey(key));
            return resp.StatusCode;
        }

        public async Task<IEnumerable<Blog>> GetCatsAsync(string query)
        {
            QueryDefinition queryDefinition = new QueryDefinition(query);
            FeedIterator<Blog> feedIterator = container.GetItemQueryIterator<Blog>(queryDefinition);
            List<Blog> blogs = new List<Blog>();
            while (feedIterator.HasMoreResults)
            {
                FeedResponse<Blog> resp = await feedIterator.ReadNextAsync();
                foreach (var blog in resp)
                {
                    blogs.Add(blog);
                }
            }
            return blogs;
        }

        public async Task<Blog> UpdateAsync(Blog blogToUpdate)
        {
            ItemResponse<Blog> resp = await container.UpsertItemAsync(blogToUpdate, new PartitionKey(blogToUpdate.Name));
            return resp.Resource;
        }
    }
}
