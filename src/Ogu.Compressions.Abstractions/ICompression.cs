using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Defines the interface for compression algorithms to performing asynchronous compression and decompression operations.
    /// </summary>
    public interface ICompression
    {
        /// <summary>
        /// Gets the name of the encoding used for underlying compression.
        /// </summary>
        string EncodingName { get; }

        /// <summary>
        /// Gets the size, in bytes, of the buffer. The default size is 81920.
        /// </summary>
        /// <remarks>
        /// Value must be greater than zero.
        /// </remarks>
        int BufferSize { get; }

        /// <summary>
        /// Gets the type of compression. This value identifies which compression algorithm is used.
        /// </summary>
        CompressionType Type { get; }

        /// <summary>
        /// Gets the compression level to use for the underlying stream.
        /// </summary>
        CompressionLevel Level { get; }

        Task<byte[]> CompressAsync(string input, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(byte[] bytes, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(Stream stream, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(string input, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(Stream stream, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(Stream stream, bool leaveOpen, CompressionLevel compressionLevel, CancellationToken cancellationToken = default);

        Task<Stream> CompressToStreamAsync(string input, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(byte[] bytes, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(Stream stream, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(string input, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(Stream stream, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(Stream stream, bool leaveOpen, CompressionLevel compressionLevel, CancellationToken cancellationToken = default);

        byte[] Compress(string input);
        byte[] Compress(byte[] bytes);
        byte[] Compress(Stream stream);
        byte[] Compress(Stream stream, bool leaveOpen);
        byte[] Compress(string input, CompressionLevel level);
        byte[] Compress(byte[] bytes, CompressionLevel level);
        byte[] Compress(Stream stream, CompressionLevel level);
        byte[] Compress(Stream stream, bool leaveOpen, CompressionLevel level);

        Stream CompressToStream(string input);
        Stream CompressToStream(byte[] bytes);
        Stream CompressToStream(Stream stream);
        Stream CompressToStream(Stream stream, bool leaveOpen);
        Stream CompressToStream(string input, CompressionLevel level);
        Stream CompressToStream(byte[] bytes, CompressionLevel level);
        Stream CompressToStream(Stream stream, CompressionLevel level);
        Stream CompressToStream(Stream stream, bool leaveOpen, CompressionLevel compressionLevel);

        Task<byte[]> DecompressAsync(byte[] bytes, CancellationToken cancellationToken = default);
        Task<byte[]> DecompressAsync(Stream stream, CancellationToken cancellationToken = default);
        Task<byte[]> DecompressAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        
        Task<Stream> DecompressToStreamAsync(byte[] bytes, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(Stream stream, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(HttpContent httpContent, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(HttpContent httpContent, bool leaveOpen, CancellationToken cancellationToken = default);

        byte[] Decompress(byte[] bytes);
        byte[] Decompress(Stream stream);
        byte[] Decompress(Stream stream, bool leaveOpen);
    }
}