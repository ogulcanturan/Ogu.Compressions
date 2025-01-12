using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    public interface ICompressionFactory
    {
        /// <summary>
        /// Returns specified type compression or null if there is no such service
        /// </summary>
        /// <param name="compressionType"></param>
        /// <returns></returns>
        ICompression Get(CompressionType compressionType);

        /// <summary>
        /// Returns specified type compression or none compression if not matches or null if there is no such service
        /// </summary>
        /// <param name="encodingName"></param>
        /// <returns></returns>
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
        Task<Stream> DecompressAsync(CompressionType compressionType, HttpContent httpContent, CancellationToken cancellationToken = default);
        Task<Stream> DecompressAsync(string encodingName, HttpContent httpContent, CancellationToken cancellationToken = default);

        Task<Stream> DecompressToStreamAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
        Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);
       
    }
}