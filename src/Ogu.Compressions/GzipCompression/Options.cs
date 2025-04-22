using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions
{
    public sealed class GzipCompressionOptions : CompressionOptions, IOptions<GzipCompressionOptions>
    {
        public GzipCompressionOptions()
        {
        }

        public GzipCompressionOptions(CompressionLevel level, int bufferSize) : base(level, bufferSize)
        {
        }

        public override string EncodingName => EncodingNames.Gzip;

        public override CompressionType Type => CompressionType.Gzip;

        GzipCompressionOptions IOptions<GzipCompressionOptions>.Value => this;
    }
}