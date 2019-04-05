using System.Collections.Generic;
using System.Threading.Tasks;
using DataDrivenSamples.Data.Shared.Dtos.Create;
using DataDrivenSamples.Data.Shared.Dtos.Update;
using DataDrivenSamples.Data.Shared.Models;

namespace DataDrivenSamples.Data.TableStorage
{
    public interface ITableStorage
    {
        Task<IEnumerable<Item>> GetAllAsync();

        Task<Item> GetItemAsync(string id);

        Task<Item> CreateItemAsync(CreateItemRequestDto dto);

        Task<Item> UpdateItemAsync(UpdateItemRequestDto dto);

        Task DeleteItemAsync(string id);
    }
}
