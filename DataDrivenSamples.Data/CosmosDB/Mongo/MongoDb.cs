using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataDrivenSamples.Data.Shared.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DataDrivenSamples.Data.CosmosDB.Mongo
{
    public class MongoDb : IMongoDb
    {
        private static string DbName => "DB";
        private static string CollectionName => "items";
        private IMongoCollection<Item> Collection => GetCollection();
        private readonly MongoClient _client;

        public MongoDb(IConfiguration configuration)
        {
            _client = new MongoClient(configuration["Cosmos"]);
        }

        public async Task<List<Item>> GetAll()
        {
            return await Collection.Find(_ => true).ToListAsync();
        }

        public async Task<Item> Get(string id)
        {
            var result = await Collection.FindAsync(item => item.Id == id);

            return result.Current.SingleOrDefault();
        }

        public async Task<Item> Create(Item item)
        {
            await Collection.InsertOneAsync(item);

            return item;
        }

        public async Task<string> Update(string id, Item item)
        {
            var result = await Collection.ReplaceOneAsync(id, item);

            return result.ModifiedCount == 1 ? id : null;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await Collection.DeleteOneAsync(id);

            return result.DeletedCount == 1;
        }

        private IMongoCollection<Item> GetCollection()
        {
            return GetDatabase().GetCollection<Item>(CollectionName);
        }

        private IMongoDatabase GetDatabase()
        {
            return _client.GetDatabase(DbName);
        }
    }
}
