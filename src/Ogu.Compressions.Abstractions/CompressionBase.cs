using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Represents the base class for compression algorithms.
    /// </summary>
    public abstract class Compression : ICompression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Compression"/> class using the specified compression options.
        /// </summary>
        /// <param name="options">The <see cref="CompressionOptions"/> that contains the encoding name, buffer size, compression type, and compression level.</param>
        protected Compression(CompressionOptions options) : this(options.EncodingName, options.BufferSize, options.Type, options.Level)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Compression"/> class with the specified encoding name, buffer size, compression type, and compression level.
        /// </summary>
        /// <param name="encodingName">The name of the compression encoding (e.g., "gzip", "deflate", etc.).</param>
        /// <param name="bufferSize">The size of the buffer used for compression operations.</param>
        /// <param name="type">The type of compression algorithm to be used.</param>
        /// <param name="level">The compression level to be applied during compression.</param>
        protected Compression(string encodingName, int bufferSize, CompressionType type, CompressionLevel level)
        {
            EncodingName = encodingName;
            BufferSize = bufferSize;
            Type = type;
            Level = level;
        }

        public string EncodingName { get; }

        public int BufferSize { get; }

        public CompressionType Type { get; }

        public CompressionLevel Level { get; }

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
            return CompressAsync(stream, Level, leaveOpen, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return CompressAsync(Encoding.UTF8.GetBytes(input), level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return InternalCompressAsync(bytes, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return InternalCompressAsync(stream, level, leaveOpen, BufferSize, cancellationToken);
        }

        public Task<byte[]> CompressAsync(Stream stream, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return InternalCompressAsync(stream, level, leaveOpen: false, BufferSize, cancellationToken);
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
            return CompressToStreamAsync(stream, Level, leaveOpen, cancellationToken);
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
            return InternalCompressToStreamAsync(stream, level, leaveOpen: false, BufferSize, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return InternalCompressToStreamAsync(stream, level, leaveOpen, BufferSize, cancellationToken);
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
            return Compress(stream, Level, leaveOpen);
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
            return InternalCompress(stream, level, leaveOpen: false, BufferSize);
        }

        public byte[] Compress(Stream stream, CompressionLevel level, bool leaveOpen)
        {
            return InternalCompress(stream, level, leaveOpen, BufferSize);
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
            return CompressToStream(stream, Level, leaveOpen);
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
            return InternalCompressToStream(stream, level, leaveOpen: false, BufferSize);
        }

        public Stream CompressToStream(Stream stream, CompressionLevel level, bool leaveOpen)
        {
            return InternalCompressToStream(stream, level, leaveOpen, BufferSize);
        }

        protected abstract Task<byte[]> InternalCompressAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);

        protected abstract Task<byte[]> InternalCompressAsync(Stream stream, CompressionLevel level, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default);

        protected abstract Task<Stream> InternalCompressToStreamAsync(byte[] bytes, CompressionLevel compressionLevel, CancellationToken cancellationToken = default);

        protected abstract Task<Stream> InternalCompressToStreamAsync(Stream stream, CompressionLevel compressionLevel, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default);

        protected abstract byte[] InternalCompress(byte[] bytes, CompressionLevel level);

        protected abstract byte[] InternalCompress(Stream stream, CompressionLevel level, bool leaveOpen, int bufferSize);

        protected abstract Stream InternalCompressToStream(byte[] bytes, CompressionLevel compressionLevel);

        protected abstract Stream InternalCompressToStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen, int bufferSize);

        public Task<byte[]> DecompressAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            return InternalDecompressAsync(bytes, BufferSize, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            return InternalDecompressAsync(stream, leaveOpen: false, BufferSize, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return InternalDecompressAsync(stream, leaveOpen, BufferSize, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            return InternalDecompressToStreamAsync(bytes, BufferSize, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            return InternalDecompressToStreamAsync(stream, leaveOpen: false, BufferSize, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return InternalDecompressToStreamAsync(stream, leaveOpen, BufferSize, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            return InternalDecompressToStreamAsync(httpContent, leaveOpen: false, BufferSize, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(HttpContent httpContent, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return InternalDecompressToStreamAsync(httpContent, leaveOpen, BufferSize, cancellationToken);
        }

        public byte[] Decompress(byte[] bytes)
        {
            return InternalDecompress(bytes, BufferSize);
        }

        public byte[] Decompress(Stream stream)
        {
            return InternalDecompress(stream, leaveOpen: false, BufferSize);
        }

        public byte[] Decompress(Stream stream, bool leaveOpen)
        {
            return InternalDecompress(stream, leaveOpen, BufferSize);
        }

        protected abstract Task<byte[]> InternalDecompressAsync(byte[] bytes, int bufferSize, CancellationToken cancellationToken = default);
        protected abstract Task<byte[]> InternalDecompressAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default);
        protected abstract Task<Stream> InternalDecompressToStreamAsync(byte[] bytes, int bufferSize, CancellationToken cancellationToken = default);
        protected abstract Task<Stream> InternalDecompressToStreamAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default);
        protected abstract Task<Stream> InternalDecompressToStreamAsync(HttpContent httpContent, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default);
        protected abstract byte[] InternalDecompress(byte[] bytes, int bufferSize);
        protected abstract byte[] InternalDecompress(Stream stream, bool leaveOpen, int bufferSize);
    }
}