using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;

namespace Ogu.Compressions
{
    public sealed class ZstdCompressionOptions : CompressionOptions, IOptions<ZstdCompressionOptions>
    {
        public ZstdCompressionOptions() { }

        public ZstdCompressionOptions(CompressionOptions options)
        {
            Level = options.Level;
            BufferSize = options.BufferSize;
        }

        public override string EncodingName => EncodingNames.Zstd;

        public override CompressionType Type => CompressionType.Zstd;

        /// <inheritdoc />
        ZstdCompressionOptions IOptions<ZstdCompressionOptions>.Value => this;
    }
}