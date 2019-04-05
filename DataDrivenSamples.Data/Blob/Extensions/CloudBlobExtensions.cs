using System;
using DataDrivenSamples.Data.Blob.Enums;
using DataDrivenSamples.Data.Shared.Models;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DataDrivenSamples.Data.Blob.Extensions
{
    public static class CloudBlobExtensions
    {
        public static BlobObject ToBlobObject(this CloudBlockBlob blockBlob)
        {
            return new BlobObject
            {
                Name = blockBlob.Name,
                Container = blockBlob.Container.Name,
                Uri = blockBlob.Uri,
                StorageUri = blockBlob.Container.StorageUri.PrimaryUri
            };
        }

        public static Uri GenerateSas(this IListBlobItem item, CloudBlobContainer container, SasPermissions permissions)
        {
            var constraints = new SharedAccessBlobPolicy
            {
                Permissions = (SharedAccessBlobPermissions)permissions,
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(2),
                SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5)
            };

            var token = container.GetSharedAccessSignature(constraints);
            var uri = item.Uri + token;

            return new Uri(uri);
        }
    }
}
