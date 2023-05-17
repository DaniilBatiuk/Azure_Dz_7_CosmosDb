using Azure_Dz_7_CosmosDb.DTOs;
using Azure_Dz_7_CosmosDb.Models;
using Azure_Dz_7_CosmosDb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Azure_Dz_7_CosmosDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        public readonly IBlogDbService cosmosService;
        public BlogsController(IBlogDbService cosmosService)
        {
            this.cosmosService = cosmosService;
        }



        [HttpGet]
        public async Task<IEnumerable<Blog>> Get()
        {
            string query = "SELECT * FROM c\r\nORDER BY c._ts DESC";
            return await cosmosService.GetCatsAsync(query);
        }

        [HttpPost]
        public async Task<Blog> Create(CreateBlogDTO blogDTO)
        {
            Blog addedBlog = await cosmosService.AddAsync(blogDTO);
            return addedBlog;
        }

        [HttpPut]
        public async Task<Blog> Update(Blog blogToUpdate)
        {
            Blog updatedBlog = await cosmosService.UpdateAsync(blogToUpdate);
            return updatedBlog;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id, string name)
        {
            HttpStatusCode statusCode = await cosmosService.DeleteAsync(id, name);
            return Ok(new { message = $"Blog with id: {id} deleted" });
        }
    }
}
