using Ogu.Compressions.Abstractions;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public partial class NoneCompression : CompressionBase, INoneCompression
    {
        public NoneCompression() : base(CompressionLevel.Fastest, 0) { }

        public override string EncodingName { get; } = string.Empty;

        public override CompressionType CompressionType { get; } = CompressionType.None;

        protected override byte[] InternalCompress(byte[] bytes, CompressionLevel level)
        {
            return bytes;
        }

        protected override byte[] InternalCompress(Stream stream, CompressionLevel level, bool leaveOpen)
        {
            return stream.ReadAllBytes(leaveOpen);
        }

        protected override Task<byte[]> InternalCompressAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(bytes);
        }

        protected override Task<byte[]> InternalCompressAsync(Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return stream.ReadAllBytesAsync(leaveOpen, cancellationToken);
        }

        protected override async Task<Stream> InternalCompressToStreamAsync(byte[] bytes, CompressionLevel compressionLevel, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(new MemoryStream(bytes)).ConfigureAwait(false);
        }

        protected override Task<Stream> InternalCompressToStreamAsync(Stream stream, CompressionLevel compressionLevel, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(stream);
        }

        protected override Stream InternalCompressToStream(byte[] bytes, CompressionLevel compressionLevel)
        {
            return new MemoryStream(bytes);
        }

        protected override Stream InternalCompressToStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
        {
            return stream;
        }
    }
}