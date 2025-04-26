using Ogu.Compressions.Abstractions;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Ogu.Compressions
{
    /// <summary>
    /// Represents a placeholder compression implementation that performs no compression.
    /// This class implements the <see cref="INoneCompression"/> interface and is used internally to indicate no compression.
    /// </summary>
    public class NoneCompression : CompressionBase, INoneCompression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoneCompression"/> class.
        /// </summary>
        public NoneCompression(IOptions<NoneCompressionOptions> opts) : base(opts.Value) { }

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

        protected override Task<byte[]> InternalDecompressAsync(byte[] bytes, int bufferSize, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(bytes);
        }

        protected override Task<byte[]> InternalDecompressAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            return stream.ReadAllBytesAsync(leaveOpen, cancellationToken);
        }

        protected override async Task<Stream> InternalDecompressToStreamAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(new MemoryStream(bytes)).ConfigureAwait(false);
        }

        protected override Task<Stream> InternalDecompressToStreamAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(stream);
        }

        protected override Task<Stream> InternalDecompressToStreamAsync(HttpContent httpContent, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            return
#if NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP3_1
                httpContent.ReadAsStreamAsync();
#else
                httpContent.ReadAsStreamAsync(cancellationToken);
#endif
        }

        protected override byte[] InternalDecompress(byte[] bytes)
        {
            return bytes;
        }

        protected override byte[] InternalDecompress(Stream stream, bool leaveOpen)
        {
            return stream.ReadAllBytes(leaveOpen);
        }
    }
}