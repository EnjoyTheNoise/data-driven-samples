using System.Linq;
using DataDrivenSamples.Data.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace DataDrivenSamples.Infrastructure.ExtensionMethods
{
    public static class BlobExtensions
    {
        public static void ReadFileStream(this BlobObject blob, HttpRequest request, bool isUpdate = false)
        {
            if (request.Form.Files.Count <= 0) return;
            if (blob.Stream != null) return;

            var file = request.Form.Files.FirstOrDefault();
            blob.Stream = file?.OpenReadStream();
            if (!isUpdate)
            {
                blob.Name = file?.FileName;
            }
        }
    }
}
