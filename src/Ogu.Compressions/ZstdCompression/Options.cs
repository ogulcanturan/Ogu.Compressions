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

        /// <inheritdoc />
        ZstdCompressionOptions IOptions<ZstdCompressionOptions>.Value => this;
    }
}