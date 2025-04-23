using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public class CompressionFactory : ICompressionFactory
    {
        private readonly IBrotliCompression _brotliCompression;
        private readonly IDeflateCompression _deflateCompression;
        private readonly ISnappyCompression _snappyCompression;
        private readonly IZstdCompression _zstdCompression;
        private readonly IGzipCompression _gzipCompression;
        private readonly INoneCompression _noneCompression;

        public CompressionFactory() : this(new CompressionFactoryOptions()) { }

        public CompressionFactory(IOptions<CompressionFactoryOptions> opts)
        {
            var compressionFactoryOptions = opts.Value;

            _brotliCompression = new BrotliCompression(new BrotliCompressionOptions(compressionFactoryOptions.Level, compressionFactoryOptions.BufferSize));
            _deflateCompression = new DeflateCompression(new DeflateCompressionOptions(compressionFactoryOptions.Level, compressionFactoryOptions.BufferSize));
            _snappyCompression = new SnappyCompression(new SnappyCompressionOptions(compressionFactoryOptions.Level, compressionFactoryOptions.BufferSize));
            _zstdCompression = new ZstdCompression(new ZstdCompressionOptions(compressionFactoryOptions.Level, compressionFactoryOptions.BufferSize));
            _gzipCompression = new GzipCompression(new GzipCompressionOptions(compressionFactoryOptions.Level, compressionFactoryOptions.BufferSize));
            _noneCompression = new NoneCompression();
        }

        public CompressionFactory(
            IBrotliCompression brotliCompression, 
            IDeflateCompression deflateCompression, 
            ISnappyCompression snappyCompression, 
            IZstdCompression zstdCompression, 
            IGzipCompression gzipCompression, 
            INoneCompression noneCompression)
        {
            _brotliCompression = brotliCompression;
            _deflateCompression = deflateCompression;
            _snappyCompression = snappyCompression;
            _zstdCompression = zstdCompression;
            _gzipCompression = gzipCompression;
            _noneCompression = noneCompression;
        }

        public ICompression Get(CompressionType compressionType)
        {
            switch (compressionType)
            {
                case CompressionType.Brotli:
                    return _brotliCompression;
                case CompressionType.Deflate:
                    return _deflateCompression;
                case CompressionType.Snappy:
                    return _snappyCompression;
                case CompressionType.Zstd:
                    return _zstdCompression;
                case CompressionType.Gzip:
                    return _gzipCompression;
                case CompressionType.None:
                default:
                    return _noneCompression;
            }
        }

        public ICompression Get(string encodingName)
        {
            _ = CompressionHelper.TryConvertEncodingNameToCompressionType(encodingName, out var compressionType);

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

        public byte[] Compress(CompressionType compressionType, string input)
        {
            return Get(compressionType).Compress(input);
        }

        public byte[] Compress(string encodingName, string input)
        {
            return Get(encodingName).Compress(input);
        }

        public byte[] Compress(CompressionType compressionType, byte[] bytes)
        {
            return Get(compressionType).Compress(bytes);
        }

        public byte[] Compress(string encodingName, byte[] bytes)
        {
            return Get(encodingName).Compress(bytes);
        }

        public byte[] Compress(CompressionType compressionType, Stream stream)
        {
            return Get(compressionType).Compress(stream);
        }

        public byte[] Compress(string encodingName, Stream stream)
        {
            return Get(encodingName).Compress(stream);
        }

        public byte[] Compress(CompressionType compressionType, Stream stream, bool leaveOpen)
        {
            return Get(compressionType).Compress(stream);
        }

        public byte[] Compress(string encodingName, Stream stream, bool leaveOpen)
        {
            return Get(encodingName).Compress(stream);
        }

        public byte[] Compress(CompressionType compressionType, string input, CompressionLevel level)
        {
            return Get(compressionType).Compress(input, level);
        }

        public byte[] Compress(string encodingName, string input, CompressionLevel level)
        {
            return Get(encodingName).Compress(input, level);
        }

        public byte[] Compress(CompressionType compressionType, byte[] bytes, CompressionLevel level)
        {
            return Get(compressionType).Compress(bytes, level);
        }

        public byte[] Compress(string encodingName, byte[] bytes, CompressionLevel level)
        {
            return Get(encodingName).Compress(bytes, level);
        }

        public byte[] Compress(CompressionType compressionType, Stream stream, CompressionLevel level)
        {
            return Get(compressionType).Compress(stream, level);
        }

        public byte[] Compress(string encodingName, Stream stream, bool leaveOpen, CompressionLevel level)
        {
            return Get(encodingName).Compress(stream, level);
        }

        public Stream CompressToStream(CompressionType compressionType, string input)
        {
            return Get(compressionType).CompressToStream(input);
        }

        public Stream CompressToStream(string encodingName, string input)
        {
            return Get(encodingName).CompressToStream(input);
        }

        public Stream CompressToStream(CompressionType compressionType, byte[] bytes)
        {
            return Get(compressionType).CompressToStream(bytes);
        }

        public Stream CompressToStream(string encodingName, byte[] bytes)
        {
            return Get(encodingName).CompressToStream(bytes);
        }

        public Stream CompressToStream(CompressionType compressionType, Stream stream)
        {
            return Get(compressionType).CompressToStream(stream);
        }

        public Stream CompressToStream(string encodingName, Stream stream)
        {
            return Get(encodingName).CompressToStream(stream);
        }

        public Stream CompressToStream(CompressionType compressionType, Stream stream, bool leaveOpen)
        {
            return Get(compressionType).CompressToStream(stream);
        }

        public Stream CompressToStream(string encodingName, Stream stream, bool leaveOpen)
        {
            return Get(encodingName).CompressToStream(stream);
        }

        public Stream CompressToStream(CompressionType compressionType, string input, CompressionLevel level)
        {
            return Get(compressionType).CompressToStream(input, level);
        }

        public Stream CompressToStream(string encodingName, string input, CompressionLevel level)
        {
            return Get(encodingName).CompressToStream(input, level);
        }

        public Stream CompressToStream(CompressionType compressionType, byte[] bytes, CompressionLevel level)
        {
            return Get(compressionType).CompressToStream(bytes, level);
        }

        public Stream CompressToStream(string encodingName, byte[] bytes, CompressionLevel level)
        {
            return Get(encodingName).CompressToStream(bytes, level);
        }

        public Stream CompressToStream(CompressionType compressionType, Stream stream, CompressionLevel level)
        {
            return Get(compressionType).CompressToStream(stream, level);
        }

        public Stream CompressToStream(string encodingName, Stream stream, bool leaveOpen, CompressionLevel level)
        {
            return Get(encodingName).CompressToStream(stream, level);
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

        public Task<Stream> DecompressToStreamAsync(CompressionType compressionType, HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            return Get(compressionType).DecompressToStreamAsync(httpContent, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            return Get(encodingName).DecompressToStreamAsync(httpContent, cancellationToken);
        }

        public byte[] Decompress(CompressionType compressionType, byte[] bytes)
        {
            return Get(compressionType).Decompress(bytes);
        }

        public byte[] Decompress(string encodingName, byte[] bytes)
        {
            return Get(encodingName).Decompress(bytes);
        }

        public byte[] Decompress(CompressionType compressionType, Stream stream)
        {
            return Get(compressionType).Decompress(stream);
        }

        public byte[] Decompress(string encodingName, Stream stream)
        {
            return Get(encodingName).Decompress(stream);
        }

        public byte[] Decompress(CompressionType compressionType, Stream stream, bool leaveOpen)
        {
            return Get(compressionType).Decompress(stream, leaveOpen);
        }

        public byte[] Decompress(string encodingName, Stream stream, bool leaveOpen)
        {
            return Get(encodingName).Decompress(stream, leaveOpen);
        }
    }
}