using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataDrivenSamples.Data.Blob.Enums;
using DataDrivenSamples.Data.Blob.Extensions;
using DataDrivenSamples.Data.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DataDrivenSamples.Data.Blob
{
    public class BlobStorage : IBlobStorage
    {
        private static CloudStorageAccount _storageAccount;

        private static CloudBlobClient Client => _storageAccount.CreateCloudBlobClient();

        private static string ContainerName => "blob";

        public BlobStorage(IConfiguration configuration)
        {
            _storageAccount = CloudStorageAccount.Parse(configuration["Storage"]);
        }
        
        public async Task<List<BlobObject>> GetBlob()
        {
            var container = await GetContainerReference();
            var blobs = await container.ListBlobsSegmentedAsync(null);

            var list = blobs.Results.Select(blob => new BlobObject
            {
                Container = blob.Container.Name,
                Uri = blob.Uri,
                StorageUri = blob.StorageUri.PrimaryUri,
                SasUri = blob.GenerateSas(container, SasPermissions.All)
            }).ToList();

            return list;
        }

        public async Task<BlobObject> Get(string id)
        {
            var container = await GetContainerReference();
            var blob = container.GetBlockBlobReference(id);

            var result = blob.ToBlobObject();
            result.SasUri = blob.GenerateSas(container, SasPermissions.ReadList);

            return result;
        }

        public async Task<BlobObject> Create(BlobObject obj)
        {
            var container = await GetContainerReference();
            var blob = container.GetBlockBlobReference(obj.Name);

            await blob.UploadFromStreamAsync(obj.Stream);
            obj.Uri = blob.Uri;
            obj.SasUri = blob.GenerateSas(container, SasPermissions.All);

            return obj;
        }

        public async Task<Uri> Update(BlobObject obj)
        {
            var container = await GetContainerReference();
            var blob = container.GetBlockBlobReference(obj.Name);

            await blob.UploadFromStreamAsync(obj.Stream);

            var uri = blob.GenerateSas(container, SasPermissions.ReadList);

            return uri;
        }

        public async Task Remove(string id)
        {
            var blob = await GetBlockBlob(id);

            await blob.DeleteAsync();
        }

        private static async Task<CloudBlobContainer> GetContainerReference()
        {
            var container = Client.GetContainerReference(ContainerName);
            await container.CreateIfNotExistsAsync();

            return container;
        }

        private static async Task<CloudBlockBlob> GetBlockBlob(string name)
        {
            var container = await GetContainerReference();

            return container.GetBlockBlobReference(name);
        }
    }
}
