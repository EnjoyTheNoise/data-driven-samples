using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace DataDrivenSamples.Data.Shared.Models
{
    public class Item : TableEntity
    {
        [JsonProperty(PropertyName = "id")]
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public Item()
        {
            PartitionKey = "item";
            RowKey = Guid.NewGuid().ToString("N");
            Id = RowKey;
        }
    }
}
