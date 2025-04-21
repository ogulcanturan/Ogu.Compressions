using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;

namespace Ogu.Compressions
{
    public sealed class GzipCompressionOptions : CompressionOptions, IOptions<GzipCompressionOptions>
    {
        public GzipCompressionOptions() { }

        public GzipCompressionOptions(CompressionOptions options)
        {
            Level = options.Level;
            BufferSize = options.BufferSize;
        }

        public override string EncodingName => EncodingNames.Gzip;

        public override CompressionType Type => CompressionType.Gzip;

        /// <inheritdoc />
        GzipCompressionOptions IOptions<GzipCompressionOptions>.Value => this;
    }
}