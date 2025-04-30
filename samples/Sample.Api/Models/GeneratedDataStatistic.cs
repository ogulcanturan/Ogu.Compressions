using Ogu.Compressions.Abstractions;
using System.IO.Compression;
using System.Text.Json.Serialization;

namespace Sample.Api.Models
{
    public class DataGeneratorStatistic
    {
        public string CompressionName { get; set; }

        public DataType DataType { get; set; }

        public CompressionType CompressionType { get; set; }

        public CompressionLevel CompressionLevel { get; set; }

        public long RawPayloadSize { get; set; }

        [JsonIgnore]
        public string RawPayloadFilePath { get; set; }

        public long CompressedPayloadSize { get; set; }

        [JsonIgnore]
        public string CompressedPayloadFilePath { get; set; }
    }
}