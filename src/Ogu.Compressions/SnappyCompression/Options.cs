using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;

namespace Ogu.Compressions
{
    public sealed class SnappyCompressionOptions : CompressionOptions, IOptions<SnappyCompressionOptions>
    {
        public SnappyCompressionOptions() { }

        public SnappyCompressionOptions(CompressionOptions options)
        {
            Level = options.Level;
            BufferSize = options.BufferSize;
        }

        public override string EncodingName => EncodingNames.Snappy;

        public override CompressionType Type => CompressionType.Snappy;

        /// <inheritdoc />
        SnappyCompressionOptions IOptions<SnappyCompressionOptions>.Value => this;
    }
}