using System.Threading.Tasks;
using DataDrivenSamples.Data.CosmosDB.Mongo;
using DataDrivenSamples.Data.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataDrivenSamples.Controllers
{
    [Route("api/mongo")]
    public class MongoController : Controller
    {
        private readonly IMongoDb _repo;

        public MongoController(IMongoDb repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repo.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _repo.Get(id);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Item item)
        {
            var result = await _repo.Create(item);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Item item)
        {
            var result = await _repo.Update(id, item);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _repo.Delete(id);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
