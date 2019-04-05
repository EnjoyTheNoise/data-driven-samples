using System.Threading.Tasks;
using DataDrivenSamples.Data.CosmosDB;
using DataDrivenSamples.Data.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataDrivenSamples.Controllers
{
    [Route("api/cosmos")]
    public class CosmosController : Controller
    {
        private readonly ICosmosDb _cosmos;
        public CosmosController(ICosmosDb cosmos)
        {
            _cosmos = cosmos;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _cosmos.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _cosmos.Get(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Item item)
        {
            var result = await _cosmos.Create(item);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Item item)
        {
            item.Id = id;
            var result = await _cosmos.Update(id, item);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cosmos.Delete(id);

            return NoContent();
        }
    }
}
