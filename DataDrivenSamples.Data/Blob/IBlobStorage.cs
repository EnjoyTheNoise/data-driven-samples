using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataDrivenSamples.Data.Shared.Models;

namespace DataDrivenSamples.Data.Blob
{
    public interface IBlobStorage
    {
        Task<List<BlobObject>> GetBlob();
        Task<BlobObject> Get(string id);
        Task<BlobObject> Create(BlobObject obj);
        Task<Uri> Update(BlobObject obj);
        Task Remove(string id);
    }
}
