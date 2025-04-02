using Ogu.Compressions.Abstractions;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public abstract partial class CompressionBase : ICompression
    {
        protected CompressionBase(CompressionLevel level, int bufferSize)
        {
            Level = level;
            BufferSize = bufferSize;
        }

        public CompressionLevel Level { get; }

        public int BufferSize { get; }

        public abstract CompressionType CompressionType { get; }

        public abstract string EncodingName { get; }

        public Task<byte[]> CompressAsync(string input, CancellationToken cancellationToken = default)
        {
            return CompressAsync(Encoding.UTF8.GetBytes(input), cancellationToken);
        }

        public Task<byte[]> CompressAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            return CompressAsync(bytes, Level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            return CompressAsync(stream, Level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return CompressAsync(stream, leaveOpen, Level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return CompressAsync(Encoding.UTF8.GetBytes(input), level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return InternalCompressAsync(bytes, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(Stream stream, bool leaveOpen, CompressionLevel compressionLevel, CancellationToken cancellationToken = default)
        {
            return InternalCompressAsync(stream, compressionLevel, leaveOpen, cancellationToken);
        }

        public Task<byte[]> CompressAsync(Stream stream, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return InternalCompressAsync(stream, level, leaveOpen: false, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string input, CancellationToken cancellationToken = default)
        {
            return CompressToStreamAsync(input, Level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            return CompressToStreamAsync(bytes, Level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            return CompressToStreamAsync(stream, Level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return CompressToStreamAsync(stream, leaveOpen, Level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return CompressToStreamAsync(Encoding.UTF8.GetBytes(input), Level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return InternalCompressToStreamAsync(bytes, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(Stream stream, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return InternalCompressToStreamAsync(stream, level, leaveOpen: false, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(Stream stream, bool leaveOpen, CompressionLevel compressionLevel, CancellationToken cancellationToken = default)
        {
            return InternalCompressToStreamAsync(stream, compressionLevel, leaveOpen, cancellationToken);
        }

        public byte[] Compress(string input)
        {
            return Compress(Encoding.UTF8.GetBytes(input));
        }

        public byte[] Compress(byte[] bytes)
        {
            return Compress(bytes, Level);
        }

        public byte[] Compress(Stream stream)
        {
            return Compress(stream, Level);
        }

        public byte[] Compress(Stream stream, bool leaveOpen)
        {
            return Compress(stream, leaveOpen, Level);
        }

        public byte[] Compress(string input, CompressionLevel level)
        {
            return Compress(Encoding.UTF8.GetBytes(input), level);
        }

        public byte[] Compress(byte[] bytes, CompressionLevel level)
        {
            return InternalCompress(bytes, level);
        }

        public byte[] Compress(Stream stream, CompressionLevel level)
        {
            return InternalCompress(stream, level, leaveOpen: false);
        }

        public byte[] Compress(Stream stream, bool leaveOpen, CompressionLevel level)
        {
            return InternalCompress(stream, level, leaveOpen);
        }

        public Stream CompressToStream(string input)
        {
            return CompressToStream(input, Level);
        }

        public Stream CompressToStream(byte[] bytes)
        {
            return CompressToStream(bytes, Level);
        }

        public Stream CompressToStream(Stream stream)
        {
            return CompressToStream(stream, Level);
        }

        public Stream CompressToStream(Stream stream, bool leaveOpen)
        {
            return CompressToStream(stream, leaveOpen, Level);
        }

        public Stream CompressToStream(string input, CompressionLevel level)
        {
            return CompressToStream(Encoding.UTF8.GetBytes(input), Level);
        }

        public Stream CompressToStream(byte[] bytes, CompressionLevel level)
        {
            return InternalCompressToStream(bytes, level);
        }

        public Stream CompressToStream(Stream stream, CompressionLevel level)
        {
            return InternalCompressToStream(stream, level, leaveOpen: false);
        }

        public Stream CompressToStream(Stream stream, bool leaveOpen, CompressionLevel compressionLevel)
        {
            return InternalCompressToStream(stream, compressionLevel, leaveOpen);
        }

        protected abstract Task<byte[]> InternalCompressAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);

        protected abstract Task<byte[]> InternalCompressAsync(Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default);

        protected abstract Task<Stream> InternalCompressToStreamAsync(byte[] bytes, CompressionLevel compressionLevel, CancellationToken cancellationToken = default);

        protected abstract Task<Stream> InternalCompressToStreamAsync(Stream stream, CompressionLevel compressionLevel, bool leaveOpen, CancellationToken cancellationToken = default);

        protected abstract byte[] InternalCompress(byte[] bytes, CompressionLevel level);

        protected abstract byte[] InternalCompress(Stream stream, CompressionLevel level, bool leaveOpen);

        protected abstract Stream InternalCompressToStream(byte[] bytes, CompressionLevel compressionLevel);

        protected abstract Stream InternalCompressToStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen);
    }
}