using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataDrivenSamples.Data.Shared.Dtos.Create;
using DataDrivenSamples.Data.Shared.Dtos.Update;
using DataDrivenSamples.Data.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace DataDrivenSamples.Data.TableStorage
{
    public class TableStorage : ITableStorage
    {
        private readonly CloudTable _table;
        private readonly IMapper _mapper;
        private static string TableName => "items";
        private static string PartitionKey => "item";

        public TableStorage(IConfiguration configuration, IMapper mapper)
        {
            var storage = CloudStorageAccount.Parse(configuration["Storage"]);
            var client = storage.CreateCloudTableClient();
            _table = client.GetTableReference(TableName);
            _table.CreateIfNotExistsAsync().Wait();
            _mapper = mapper;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            var query = new TableQuery<Item>();
            var result = await _table.ExecuteQuerySegmentedAsync(query, null);

            return result.Results;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            var query = TableOperation.Retrieve<Item>(PartitionKey, id);
            var result = await _table.ExecuteAsync(query);

            return result.Result as Item;
        }

        public async Task<Item> CreateItemAsync(CreateItemRequestDto dto)
        {
            var item = _mapper.Map<Item>(dto);
            var query = TableOperation.Insert(item);
            var result = await _table.ExecuteAsync(query);

            return result.Result as Item;
        }

        public async Task<Item> UpdateItemAsync(UpdateItemRequestDto dto)
        {
            var item = await GetItemAsync(dto.Id);
            if (item.Value != dto.Value)
            {
                item.Value = dto.Value;
            }

            var query = TableOperation.Replace(item);
            var result = await _table.ExecuteAsync(query);

            return result.Result as Item;
        }

        public async Task DeleteItemAsync(string id)
        {
            var getItem = TableOperation.Retrieve<Item>(PartitionKey, id);
            var item = await _table.ExecuteAsync(getItem);
            var toDelete = item.Result as Item;

            var query = TableOperation.Delete(toDelete);
            await _table.ExecuteAsync(query);
        }
    }
}
