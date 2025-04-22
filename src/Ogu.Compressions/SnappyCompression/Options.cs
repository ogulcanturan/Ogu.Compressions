using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions
{
    public sealed class SnappyCompressionOptions : CompressionOptions, IOptions<SnappyCompressionOptions>
    {
        public SnappyCompressionOptions()
        {
        }

        public SnappyCompressionOptions(CompressionLevel level, int bufferSize) : base(level, bufferSize)
        {
        }

        public override string EncodingName => EncodingNames.Snappy;

        public override CompressionType Type => CompressionType.Snappy;

        SnappyCompressionOptions IOptions<SnappyCompressionOptions>.Value => this;
    }
}