using System.Collections.Generic;
using System.Threading.Tasks;
using DataDrivenSamples.Data.Shared.Models;

namespace DataDrivenSamples.Data.CosmosDB.Mongo
{
    public interface IMongoDb
    {
        Task<List<Item>> GetAll();

        Task<Item> Get(string id);

        Task<Item> Create(Item item);

        Task<string> Update(string id, Item item);

        Task<bool> Delete(string id);
    }
}
