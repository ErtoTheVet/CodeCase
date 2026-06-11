using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Configuration.Models;

namespace Config.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParametersController : ControllerBase
    {
        private readonly IMongoCollection<Parameters> _collection;

        public ParametersController()
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("ConfigDB");

            _collection =
                database.GetCollection<Parameters>("Parameters");
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(string? name, string? applicationName)
        {
            var filter = Builders<Parameters>.Filter.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                filter &= Builders<Parameters>.Filter.Regex(
                    x => x.Name,
                    new MongoDB.Bson.BsonRegularExpression(name, "i"));
            }

            if (!string.IsNullOrEmpty(applicationName))
            {
                filter &= Builders<Parameters>.Filter.Eq(
                    x => x.ApplicationName,
                    applicationName);
            }

            var result = await _collection.Find(filter).ToListAsync();

            return Ok(result);
        }

        [HttpGet("ALL")]
        public async Task<IActionResult> GetAll()
        {
            var parameters =
                await _collection.Find(_ => true).ToListAsync();

            return Ok(parameters);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] Parameters parameter)
        {
            await _collection.InsertOneAsync(parameter);

            return Ok(parameter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            string id,
            [FromBody] Parameters parameter)
        {
            await _collection.ReplaceOneAsync(
                x => x.Id == id,
                parameter);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _collection.DeleteOneAsync(
                x => x.Id == id);

            return Ok();
        }
    }
}
