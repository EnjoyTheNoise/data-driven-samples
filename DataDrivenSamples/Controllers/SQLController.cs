using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataDrivenSamples.Data.Shared.Dtos.Create;
using DataDrivenSamples.Data.Shared.Dtos.Delete;
using DataDrivenSamples.Data.Shared.Dtos.Get;
using DataDrivenSamples.Data.Shared.Dtos.Update;
using DataDrivenSamples.Data.SQL;
using Microsoft.AspNetCore.Mvc;

namespace DataDrivenSamples.Controllers
{
    [Route("api/sql")]
    public class SqlController : Controller
    {
        private readonly ISqlService _sql;
        private readonly IMapper _mapper;

        public SqlController(ISqlService sql, IMapper mapper)
        {
            _sql = sql;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _sql.GetAll();

            return Ok(_mapper.Map<List<GetItemResponseDto>>(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _sql.GetItem(id);

            return Ok(_mapper.Map<GetItemResponseDto>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateItemRequestDto dto)
        {
            var result = await _sql.CreateItem(dto);

            return Ok(_mapper.Map<CreateItemResponseDto>(result));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateItemRequestDto dto)
        {
            var result = await _sql.UpdateItem(dto);

            return Ok(_mapper.Map<UpdateItemResponseDto>(result));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteItemRequestDto dto)
        {
            await _sql.DeleteItem(dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _sql.DeleteItem(id);

            return NoContent();
        }
    }
}
