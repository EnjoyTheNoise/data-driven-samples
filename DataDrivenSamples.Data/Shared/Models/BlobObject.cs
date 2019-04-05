using System;
using System.IO;

namespace DataDrivenSamples.Data.Shared.Models
{
    public class BlobObject
    {
        public string Container { get; set; }
        public Uri Uri { get; set; }
        public Uri StorageUri { get; set; }
        public Uri SasUri { get; set; }
        public Stream Stream { get; set; }
        public string Name { get; set; }
    }
}
