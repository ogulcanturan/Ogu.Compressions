using Ogu.Compressions.Abstractions;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public partial class NoneCompression : CompressionBase, INoneCompression
    {
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