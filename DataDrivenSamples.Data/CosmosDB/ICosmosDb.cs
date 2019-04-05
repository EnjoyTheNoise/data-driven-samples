using System.Collections.Generic;
using System.Threading.Tasks;
using DataDrivenSamples.Data.Shared.Models;

namespace DataDrivenSamples.Data.CosmosDB
{
    public interface ICosmosDb
    {
        Task<List<Item>> GetAll();

        Task<Item> Get(string id);

        Task<string> Create(Item item);

        Task<string> Update(string id, Item item);

        Task Delete(string id);
    }
}
