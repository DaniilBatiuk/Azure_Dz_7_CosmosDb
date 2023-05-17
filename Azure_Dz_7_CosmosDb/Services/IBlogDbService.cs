using Azure_Dz_7_CosmosDb.DTOs;
using Azure_Dz_7_CosmosDb.Models;
using System.Net;

namespace Azure_Dz_7_CosmosDb.Services
{
    public interface IBlogDbService
    {
        Task<IEnumerable<Blog>> GetCatsAsync(string query);

        Task<Blog> AddAsync(CreateBlogDTO newBlog);

        Task<Blog> UpdateAsync(Blog blogToUpdate);

        Task<HttpStatusCode> DeleteAsync(string id, string key);
    }
}
