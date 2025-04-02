using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    public interface ICompression
    {
        string EncodingName { get; }
        int BufferSize { get; }
        CompressionType CompressionType { get; }
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