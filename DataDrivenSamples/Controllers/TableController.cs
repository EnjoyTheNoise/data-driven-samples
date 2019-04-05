using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataDrivenSamples.Data.Shared.Dtos.Create;
using DataDrivenSamples.Data.Shared.Dtos.Delete;
using DataDrivenSamples.Data.Shared.Dtos.Get;
using DataDrivenSamples.Data.Shared.Dtos.Update;
using DataDrivenSamples.Data.TableStorage;
using Microsoft.AspNetCore.Mvc;


namespace DataDrivenSamples.Controllers
{
    [Route("api/table")]
    public class TableController : Controller
    {
        private readonly ITableStorage _table;
        private readonly IMapper _mapper;

        public TableController(ITableStorage table, IMapper mapper)
        {
            _table = table;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _table.GetAllAsync();
            return Ok(_mapper.Map<List<GetItemResponseDto>>(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromBody] GetItemRequestDto dto)
        {
            var result = await _table.GetItemAsync(dto.Id);
            return Ok(_mapper.Map<GetItemResponseDto>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateItemRequestDto item)
        {
            var result = await _table.CreateItemAsync(item);

            return Ok(_mapper.Map<CreateItemResponseDto>(result));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateItemRequestDto dto)
        {
            var result = await _table.UpdateItemAsync(dto);

            return Ok(_mapper.Map<UpdateItemResponseDto>(result));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteItemRequestDto dto)
        {
            await _table.DeleteItemAsync(dto.Id);

            return NoContent();
        }
    }
}
