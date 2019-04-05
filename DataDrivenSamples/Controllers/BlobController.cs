using System.Threading.Tasks;
using DataDrivenSamples.Data.Blob;
using DataDrivenSamples.Data.Shared.Models;
using DataDrivenSamples.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace DataDrivenSamples.Controllers
{
    [Route("api/blob")]
    public class BlobController : Controller
    {
        private readonly IBlobStorage _storage;

        public BlobController(IBlobStorage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _storage.GetBlob();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _storage.Get(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var blob = new BlobObject();
            blob.ReadFileStream(Request);

            var result = await _storage.Create(blob);

            return Ok(result.SasUri);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id)
        {
            var blob = new BlobObject {Name = id};
            blob.ReadFileStream(Request, true);

            var result = await _storage.Update(blob);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _storage.Remove(id);

            return NoContent();
        }
    }
}
