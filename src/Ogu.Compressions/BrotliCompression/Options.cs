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

        public override string EncodingName => EncodingNames.Brotli;

        public override CompressionType Type => CompressionType.Brotli;

        /// <inheritdoc />
        BrotliCompressionOptions IOptions<BrotliCompressionOptions>.Value => this;
    }
}