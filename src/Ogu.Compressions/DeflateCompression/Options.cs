using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions
{
    public sealed class DeflateCompressionOptions : CompressionOptions, IOptions<DeflateCompressionOptions>
    {
        public DeflateCompressionOptions()
        {
        }

        public DeflateCompressionOptions(CompressionLevel level, int bufferSize) : base(level, bufferSize)
        {
        }

        public override string EncodingName => EncodingNames.Deflate;

        public override CompressionType Type => CompressionType.Deflate;

        DeflateCompressionOptions IOptions<DeflateCompressionOptions>.Value => this;
    }
}