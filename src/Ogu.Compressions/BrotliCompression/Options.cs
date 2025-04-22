using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions
{
    public sealed class BrotliCompressionOptions : CompressionOptions, IOptions<BrotliCompressionOptions>
    {
        public BrotliCompressionOptions()
        {
        }

        public BrotliCompressionOptions(CompressionLevel level, int bufferSize) : base(level, bufferSize)
        {
        }

        public override string EncodingName => EncodingNames.Brotli;

        public override CompressionType Type => CompressionType.Brotli;

        BrotliCompressionOptions IOptions<BrotliCompressionOptions>.Value => this;
    }
}