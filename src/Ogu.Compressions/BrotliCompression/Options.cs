using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;

namespace Ogu.Compressions
{
    public sealed class BrotliCompressionOptions : CompressionOptions, IOptions<BrotliCompressionOptions>
    {
        public BrotliCompressionOptions() { }

        public BrotliCompressionOptions(CompressionOptions options)
        {
            Level = options.Level;
            BufferSize = options.BufferSize;
        }

        /// <inheritdoc />
        BrotliCompressionOptions IOptions<BrotliCompressionOptions>.Value => this;
    }
}