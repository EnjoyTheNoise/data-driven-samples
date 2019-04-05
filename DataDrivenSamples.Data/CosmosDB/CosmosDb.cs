using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataDrivenSamples.Data.Shared.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataDrivenSamples.Data.CosmosDB
{
    public class CosmosDb : ICosmosDb
    {
        private static string DbName => "DB";
        private static string Collection => "items";
        private readonly DocumentClient _client;

        public CosmosDb(IConfiguration configuration)
        {
            var uri = configuration["Cosmos:Uri"];
            var key = configuration["Cosmos:Key"];

            _client = new DocumentClient(new Uri(uri), key);

            Init().Wait();
        }

        private async Task Init()
        {
            await _client.CreateDatabaseIfNotExistsAsync(new Database {Id = DbName});
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DbName),
                new DocumentCollection {Id = Collection});
        }

        private static Uri GetCollection()
        {
            return UriFactory.CreateDocumentCollectionUri(DbName, Collection);
        }

        private static Uri GetDocument(string id)
        {
            return UriFactory.CreateDocumentUri(DbName, Collection, id);
        }

        public async Task<List<Item>> GetAll()
        {
            var query = _client.CreateDocumentQuery<Item>(GetCollection()).Where(_ => true).AsDocumentQuery();

            var result = new List<Item>();

            while (query.HasMoreResults)
            {
                result.AddRange(await query.ExecuteNextAsync<Item>());
            }

            return result;
        }

        public async Task<Item> Get(string id)
        {
            var result = await _client.ReadDocumentAsync<Item>(GetDocument(id));

            return result;
        }

        public async Task<string> Create(Item item)
        {
            var result = await _client.CreateDocumentAsync(GetCollection(), item);

            return result.Resource.Id;
        }

        public async Task<string> Update(string id, Item item)
        {
            var result = await _client.ReplaceDocumentAsync(GetDocument(id), item);

            return result.Resource.Id;
        }

        public async Task Delete(string id)
        {
            await _client.DeleteDocumentAsync(GetDocument(id));
        }
    }
}
