using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;

namespace Ogu.Compressions
{
    public sealed class DeflateCompressionOptions : CompressionOptions, IOptions<DeflateCompressionOptions>
    {
        public DeflateCompressionOptions() { }

        public DeflateCompressionOptions(CompressionOptions options)
        {
            Level = options.Level;
            BufferSize = options.BufferSize;
        }

        public override string EncodingName => EncodingNames.Deflate;

        public override CompressionType Type => CompressionType.Deflate;

        DeflateCompressionOptions IOptions<DeflateCompressionOptions>.Value => this;
    }
}