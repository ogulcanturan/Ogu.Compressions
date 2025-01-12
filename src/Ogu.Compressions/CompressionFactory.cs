using Microsoft.Extensions.DependencyInjection;
using Ogu.Compressions.Abstractions;
using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public class CompressionFactory : ICompressionFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CompressionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public ICompression Get(CompressionType compressionType)
        {
            switch (compressionType)
            {
                case CompressionType.Brotli:
                    return _serviceProvider.GetService<IBrotliCompression>();
                case CompressionType.Deflate:
                    return _serviceProvider.GetService<IDeflateCompression>();
                case CompressionType.Snappy:
                    return _serviceProvider.GetService<ISnappyCompression>();
                case CompressionType.Zstd:
                    return _serviceProvider.GetService<IZstdCompression>();
                case CompressionType.Gzip:
                    return _serviceProvider.GetService<IGzipCompression>();
                case CompressionType.None:
                default:
                    return _serviceProvider.GetService<INoneCompression>();
            }
        }

        /// <inheritdoc/>
        public ICompression Get(string encodingName)
        {
            _ = CompressionHelpers.TryConvertEncodingNameToCompressionType(encodingName, out var compressionType);

            return Get(compressionType);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, string input, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressAsync(input, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, string input, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressAsync(input, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressAsync(input, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressAsync(input, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressAsync(bytes, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressAsync(bytes, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressAsync(stream, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, Stream stream, bool leaveOpen, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressAsync(stream, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, string input, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressToStreamAsync(input, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, string input, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressToStreamAsync(input, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressToStreamAsync(input, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressToStreamAsync(input, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressToStreamAsync(bytes, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressToStreamAsync(bytes, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).CompressToStreamAsync(stream, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).CompressToStreamAsync(stream, level, cancellationToken);
        }
        
        public Task<byte[]> DecompressAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).DecompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).DecompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).DecompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).DecompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).DecompressAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).DecompressAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<Stream> DecompressAsync(CompressionType compressionType, HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).DecompressAsync(httpContent, cancellationToken);
        }

        public Task<Stream> DecompressAsync(string encodingName, HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).DecompressAsync(httpContent, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).DecompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).DecompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).DecompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).DecompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).DecompressToStreamAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).DecompressToStreamAsync(stream, leaveOpen, cancellationToken);
        }
    }
}