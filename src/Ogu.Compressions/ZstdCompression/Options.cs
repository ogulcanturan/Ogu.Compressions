using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions
{
    public class ZstdCompressionOptions : CompressionOptions, IOptions<ZstdCompressionOptions>
    {
        public ZstdCompressionOptions()
        {
        }

        public ZstdCompressionOptions(CompressionLevel level, int bufferSize) : base(level, bufferSize)
        {
        }

        public override string EncodingName => EncodingNames.Zstd;

        public override CompressionType Type => CompressionType.Zstd;

        ZstdCompressionOptions IOptions<ZstdCompressionOptions>.Value => this;
    }
}