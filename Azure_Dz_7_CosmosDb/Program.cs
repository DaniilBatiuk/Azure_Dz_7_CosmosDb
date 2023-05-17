using Azure_Dz_7_CosmosDb.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBlogDbService, BlogCosmosService>(options =>
{
    IConfigurationSection section = builder.Configuration.GetSection("AzureCosmosDbSettings");
    string uri = section.GetValue<string>("uri");
    string key = section.GetValue<string>("key");
    string databaseId = section.GetValue<string>("databaseId");
    string containerId = section.GetValue<string>("containerId");
    CosmosClient client = new CosmosClient(uri, key);
    return new BlogCosmosService(client, databaseId, containerId);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
