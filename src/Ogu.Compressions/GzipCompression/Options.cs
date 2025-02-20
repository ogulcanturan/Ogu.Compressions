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

        /// <inheritdoc />
        GzipCompressionOptions IOptions<GzipCompressionOptions>.Value => this;
    }
}