using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Factory interface for retrieving compression implementations and performing asynchronous compression and decompression operations.
    /// </summary>
    public interface ICompressionFactory
    {
        /// <summary>
        /// Retrieves a compression implementation based on the specified <see cref="CompressionType"/>.
        /// Returns <c>null</c> if no matching service is found.
        /// </summary>
        /// <param name="compressionType">The type of compression to retrieve.</param>
        /// <returns>An <see cref="ICompression"/> instance if available; otherwise, <c>null</c>.</returns>
        ICompression Get(CompressionType compressionType);

        /// <summary>
        /// Retrieves a compression implementation based on the specified encoding name.
        /// Returns a "none" compression implementation if no match is found, or <c>null</c> if no applicable service is available.
        /// </summary>
        /// <param name="encodingName">The name of the encoding (e.g. "br", "deflate", "snappy", "zstd", "gzip" or "" for NoneCompression.)</param>
        /// <returns>An <see cref="ICompression"/> instance, a "none" compression if not matched, or <c>null</c> if unavailable.</returns>
        ICompression Get(string encodingName);

        Task<byte[]> CompressAsync(CompressionType compressionType, string input, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(string encodingName, string input, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(CompressionType compressionType, string input, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(string encodingName, string input, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(CompressionType compressionType, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(string encodingName, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(CompressionType compressionType, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<byte[]> CompressAsync(string encodingName, Stream stream, bool leaveOpen, CompressionLevel level, CancellationToken cancellationToken = default);

        Task<Stream> CompressToStreamAsync(CompressionType compressionType, string input, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(string encodingName, string input, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(CompressionType compressionType, string input, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(string encodingName, string input, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(CompressionType compressionType, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(string encodingName, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(CompressionType compressionType, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default);
        Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CompressionLevel level, CancellationToken cancellationToken = default);

        Task<byte[]> DecompressAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default);
        Task<byte[]> DecompressAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default);
        Task<byte[]> DecompressAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default);
        Task<byte[]> DecompressAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default);
        Task<byte[]> DecompressAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        Task<byte[]> DecompressAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
   
        Task<Stream> DecompressToStreamAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(CompressionType compressionType, HttpContent httpContent, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(string encodingName, HttpContent httpContent, CancellationToken cancellationToken = default);
    }
}