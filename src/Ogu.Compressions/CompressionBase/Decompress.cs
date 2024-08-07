using Ogu.Compressions.Abstractions;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public abstract partial class CompressionBase : ICompression
    {
        public Task<byte[]> DecompressAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            return InternalDecompressAsync(bytes, BufferSize, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            return InternalDecompressAsync(stream, false, BufferSize, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return InternalDecompressAsync(stream, leaveOpen, BufferSize, cancellationToken);
        }

        public Task<Stream> DecompressAsync(HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            return InternalDecompressAsync(httpContent, BufferSize, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            return InternalDecompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            return InternalDecompressToAsync(stream, false, BufferSize, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return InternalDecompressToAsync(stream, leaveOpen, BufferSize, cancellationToken);
        }

        public byte[] Decompress(byte[] bytes)
        {
            return InternalDecompress(bytes);
        }

        public byte[] Decompress(Stream stream)
        {
            return InternalDecompress(stream, false);
        }

        public byte[] Decompress(Stream stream, bool leaveOpen)
        {
            return InternalDecompress(stream, leaveOpen);
        }

        protected abstract Task<byte[]> InternalDecompressAsync(byte[] bytes, int bufferSize, CancellationToken cancellationToken = default);
        protected abstract Task<byte[]> InternalDecompressAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default);
        protected abstract Task<Stream> InternalDecompressAsync(HttpContent httpContent, int bufferSize, CancellationToken cancellationToken = default);
        protected abstract Task<Stream> InternalDecompressToStreamAsync(byte[] bytes, CancellationToken cancellationToken = default);
        protected abstract Task<Stream> InternalDecompressToAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default);
        protected abstract byte[] InternalDecompress(byte[] bytes);
        protected abstract byte[] InternalDecompress(Stream stream, bool leaveOpen);
    }
}