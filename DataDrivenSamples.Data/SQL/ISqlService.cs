using System.Collections.Generic;
using System.Threading.Tasks;
using DataDrivenSamples.Data.Shared.Dtos.Create;
using DataDrivenSamples.Data.Shared.Dtos.Delete;
using DataDrivenSamples.Data.Shared.Dtos.Get;
using DataDrivenSamples.Data.Shared.Dtos.Update;
using DataDrivenSamples.Data.Shared.Models;

namespace DataDrivenSamples.Data.SQL
{
    public interface ISqlService
    {
        Task<IList<Item>> GetAll();

        Task<Item> GetItem(GetItemRequestDto dto);

        Task<Item> GetItem(string id);

        Task<Item> CreateItem(CreateItemRequestDto dto);

        Task<Item> UpdateItem(UpdateItemRequestDto dto);

        Task DeleteItem(DeleteItemRequestDto dto);

        Task DeleteItem(string id);
    }
}
