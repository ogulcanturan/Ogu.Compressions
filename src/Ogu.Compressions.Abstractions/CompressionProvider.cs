using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Provides compression functionality by managing a collection of compression algorithms.
    /// Implements the <see cref="ICompressionProvider"/> interface to retrieve the appropriate <see cref="ICompression"/> implementation 
    /// based on the specified compression type or encoding name. Also supports performing asynchronous compression and decompression operations.
    /// </summary>
    public sealed class CompressionProvider : ICompressionProvider
    {
        private readonly Dictionary<CompressionType, ICompression> _compressions;
        private readonly ICompressionTypeResolver _compressionTypeResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionProvider"/> class with the specified collection of compressions.
        /// </summary>
        /// <param name="compressions">A collection of <see cref="ICompression"/> implementations that will be used by the provider./// </param>
        /// <param name="compressionTypeResolver">An instance of <see cref="ICompressionTypeResolver"/> used to resolve compression types from encoding names.</param>
        public CompressionProvider(IEnumerable<ICompression> compressions, ICompressionTypeResolver compressionTypeResolver = null)
        {
            _compressions = compressions
                .GroupBy(compressor => compressor.Type)
                .ToDictionary(
                    compressor => compressor.Key,
                    compressor => compressor.LastOrDefault());

            _compressionTypeResolver = compressionTypeResolver ?? new CompressionTypeResolver();

            Compressions = _compressions.Values;
        }

        public IEnumerable<ICompression> Compressions { get; }

        public ICompression GetCompression(CompressionType type)
        {
            return _compressions.GetValueOrDefault(type);
        }

        public ICompression GetCompression(string encodingName)
        {
            return _compressionTypeResolver.TryResolve(encodingName, out var type)
                ? GetCompression(type)
                : null;
        }

        public ICompression GetRequiredCompression(CompressionType type)
        {
            return GetCompression(type) ?? throw new CompressionNotAvailableException(type);
        }

        public ICompression GetRequiredCompression(string encodingName)
        {
            return GetCompression(encodingName) ?? throw new CompressionNotAvailableException(encodingName);
        }

        public Task<byte[]> CompressAsync(CompressionType type, string input, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressAsync(input, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, string input, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(input, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType type, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType type, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType type, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType type, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressAsync(input, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(input, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType type, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressAsync(bytes, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(bytes, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType type, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressAsync(stream, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(stream, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType type, Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressAsync(stream, level, leaveOpen, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(stream, level, leaveOpen, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType type, string input, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressToStreamAsync(input, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, string input, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(input, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType type, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType type, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType type, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressToStreamAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType type, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressToStreamAsync(input, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(input, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType type, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressToStreamAsync(bytes, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(bytes, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType type, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressToStreamAsync(stream, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(stream, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType type, Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).CompressToStreamAsync(stream, level, leaveOpen, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(stream, level, leaveOpen, cancellationToken);
        }

        public byte[] Compress(CompressionType type, string input)
        {
            return GetRequiredCompression(type).Compress(input);
        }

        public byte[] Compress(string encodingName, string input)
        {
            return GetRequiredCompression(encodingName).Compress(input);
        }

        public byte[] Compress(CompressionType type, byte[] bytes)
        {
            return GetRequiredCompression(type).Compress(bytes);
        }

        public byte[] Compress(string encodingName, byte[] bytes)
        {
            return GetRequiredCompression(encodingName).Compress(bytes);
        }

        public byte[] Compress(CompressionType type, Stream stream)
        {
            return GetRequiredCompression(type).Compress(stream);
        }

        public byte[] Compress(string encodingName, Stream stream)
        {
            return GetRequiredCompression(encodingName).Compress(stream);
        }

        public byte[] Compress(CompressionType type, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(type).Compress(stream, leaveOpen);
        }

        public byte[] Compress(string encodingName, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(encodingName).Compress(stream, leaveOpen);
        }

        public byte[] Compress(CompressionType type, string input, CompressionLevel level)
        {
            return GetRequiredCompression(type).Compress(input, level);
        }

        public byte[] Compress(string encodingName, string input, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).Compress(input, level);
        }

        public byte[] Compress(CompressionType type, byte[] bytes, CompressionLevel level)
        {
            return GetRequiredCompression(type).Compress(bytes, level);
        }

        public byte[] Compress(string encodingName, byte[] bytes, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).Compress(bytes, level);
        }

        public byte[] Compress(CompressionType type, Stream stream, CompressionLevel level)
        {
            return GetRequiredCompression(type).Compress(stream, level);
        }

        public byte[] Compress(string encodingName, Stream stream, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).Compress(stream, level);
        }

        public byte[] Compress(CompressionType type, Stream stream, CompressionLevel level, bool leaveOpen)
        {
            return GetRequiredCompression(type).Compress(stream, level, leaveOpen);
        }

        public byte[] Compress(string encodingName, Stream stream, CompressionLevel level, bool leaveOpen)
        {
            return GetRequiredCompression(encodingName).Compress(stream, level, leaveOpen);
        }

        public Stream CompressToStream(CompressionType type, string input)
        {
            return GetRequiredCompression(type).CompressToStream(input);
        }

        public Stream CompressToStream(string encodingName, string input)
        {
            return GetRequiredCompression(encodingName).CompressToStream(input);
        }

        public Stream CompressToStream(CompressionType type, byte[] bytes)
        {
            return GetRequiredCompression(type).CompressToStream(bytes);
        }

        public Stream CompressToStream(string encodingName, byte[] bytes)
        {
            return GetRequiredCompression(encodingName).CompressToStream(bytes);
        }

        public Stream CompressToStream(CompressionType type, Stream stream)
        {
            return GetRequiredCompression(type).CompressToStream(stream);
        }

        public Stream CompressToStream(string encodingName, Stream stream)
        {
            return GetRequiredCompression(encodingName).CompressToStream(stream);
        }

        public Stream CompressToStream(CompressionType type, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(type).CompressToStream(stream, leaveOpen);
        }

        public Stream CompressToStream(string encodingName, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(encodingName).CompressToStream(stream, leaveOpen);
        }

        public Stream CompressToStream(CompressionType type, string input, CompressionLevel level)
        {
            return GetRequiredCompression(type).CompressToStream(input, level);
        }

        public Stream CompressToStream(string encodingName, string input, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).CompressToStream(input, level);
        }

        public Stream CompressToStream(CompressionType type, byte[] bytes, CompressionLevel level)
        {
            return GetRequiredCompression(type).CompressToStream(bytes, level);
        }

        public Stream CompressToStream(string encodingName, byte[] bytes, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).CompressToStream(bytes, level);
        }

        public Stream CompressToStream(CompressionType type, Stream stream, CompressionLevel level)
        {
            return GetRequiredCompression(type).CompressToStream(stream, level);
        }

        public Stream CompressToStream(string encodingName, Stream stream, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).CompressToStream(stream, level);
        }

        public Stream CompressToStream(CompressionType type, Stream stream, CompressionLevel level, bool leaveOpen)
        {
            return GetRequiredCompression(type).CompressToStream(stream, level, leaveOpen);
        }

        public Stream CompressToStream(string encodingName, Stream stream, CompressionLevel level, bool leaveOpen)
        {
            return GetRequiredCompression(encodingName).CompressToStream(stream, level, leaveOpen);
        }

        public Task<byte[]> DecompressAsync(CompressionType type, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).DecompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(CompressionType type, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).DecompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(CompressionType type, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).DecompressAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType type, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).DecompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType type, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).DecompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType type, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).DecompressToStreamAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressToStreamAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType type, HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).DecompressToStreamAsync(httpContent, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressToStreamAsync(httpContent, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType type, HttpContent httpContent, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(type).DecompressToStreamAsync(httpContent, leaveOpen, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, HttpContent httpContent, bool leaveOpen,CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressToStreamAsync(httpContent, leaveOpen, cancellationToken);
        }

        public byte[] Decompress(CompressionType type, byte[] bytes)
        {
            return GetRequiredCompression(type).Decompress(bytes);
        }

        public byte[] Decompress(string encodingName, byte[] bytes)
        {
            return GetRequiredCompression(encodingName).Decompress(bytes);
        }

        public byte[] Decompress(CompressionType type, Stream stream)
        {
            return GetRequiredCompression(type).Decompress(stream);
        }

        public byte[] Decompress(string encodingName, Stream stream)
        {
            return GetRequiredCompression(encodingName).Decompress(stream);
        }

        public byte[] Decompress(CompressionType type, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(type).Decompress(stream, leaveOpen);
        }

        public byte[] Decompress(string encodingName, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(encodingName).Decompress(stream, leaveOpen);
        }
    }
}