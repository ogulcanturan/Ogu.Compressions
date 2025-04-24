using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    public class CompressionProvider : ICompressionFactory
    {
        private readonly Dictionary<CompressionType, ICompression> _compressions;

        public CompressionProvider(IEnumerable<ICompression> compressions)
        {
            _compressions = compressions
                .GroupBy(compressor => compressor.Type)
                .ToDictionary(
                    compressor => compressor.Key,
                    compressor => compressor.LastOrDefault());
        }

        public IEnumerable<ICompression> GetCompressions()
        {
            return _compressions.Values;
        }

        public ICompression GetCompression(CompressionType compressionType)
        {
            switch (compressionType)
            {
                case CompressionType.Brotli:
                    {
                        return _compressions.GetValueOrDefault(CompressionType.Brotli);
                    }
                case CompressionType.Deflate:
                    {
                        return _compressions.GetValueOrDefault(CompressionType.Deflate);
                    }
                case CompressionType.Snappy:
                    {
                        return _compressions.GetValueOrDefault(CompressionType.Snappy);

                    }
                case CompressionType.Zstd:
                    {
                        return _compressions.GetValueOrDefault(CompressionType.Zstd);

                    }
                case CompressionType.Gzip:
                    {
                        return _compressions.GetValueOrDefault(CompressionType.Gzip);

                    }
                case CompressionType.None:
                    {
                        return _compressions.GetValueOrDefault(CompressionType.None);
                    }
                default:
                    return null;
            }
        }

        public ICompression GetCompression(string encodingName)
        {
            return CompressionHelper.TryConvertEncodingNameToCompressionType(encodingName, out var compressionType)
                ? GetCompression(compressionType)
                : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="CompressorNotAvailableException"></exception>
        public ICompression GetRequiredCompression(CompressionType type)
        {
            return GetCompression(type) ?? throw new CompressorNotAvailableException(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encodingName"></param>
        /// <returns></returns>
        /// <exception cref="CompressorNotAvailableException"></exception>
        public ICompression GetRequiredCompression(string encodingName)
        {
            return GetCompression(encodingName) ?? throw new CompressorNotAvailableException(encodingName);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, string input, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressAsync(input, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, string input, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(input, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressAsync(input, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(input, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressAsync(bytes, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(bytes, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(CompressionType compressionType, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressAsync(stream, level, cancellationToken);
        }

        public Task<byte[]> CompressAsync(string encodingName, Stream stream, bool leaveOpen, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressAsync(stream, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, string input, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressToStreamAsync(input, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, string input, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(input, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressToStreamAsync(input, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, string input, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(input, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressToStreamAsync(bytes, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(bytes, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(CompressionType compressionType, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).CompressToStreamAsync(stream, level, cancellationToken);
        }

        public Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).CompressToStreamAsync(stream, level, cancellationToken);
        }

        public byte[] Compress(CompressionType compressionType, string input)
        {
            return GetRequiredCompression(compressionType).Compress(input);
        }

        public byte[] Compress(string encodingName, string input)
        {
            return GetRequiredCompression(encodingName).Compress(input);
        }

        public byte[] Compress(CompressionType compressionType, byte[] bytes)
        {
            return GetRequiredCompression(compressionType).Compress(bytes);
        }

        public byte[] Compress(string encodingName, byte[] bytes)
        {
            return GetRequiredCompression(encodingName).Compress(bytes);
        }

        public byte[] Compress(CompressionType compressionType, Stream stream)
        {
            return GetRequiredCompression(compressionType).Compress(stream);
        }

        public byte[] Compress(string encodingName, Stream stream)
        {
            return GetRequiredCompression(encodingName).Compress(stream);
        }

        public byte[] Compress(CompressionType compressionType, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(compressionType).Compress(stream);
        }

        public byte[] Compress(string encodingName, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(encodingName).Compress(stream);
        }

        public byte[] Compress(CompressionType compressionType, string input, CompressionLevel level)
        {
            return GetRequiredCompression(compressionType).Compress(input, level);
        }

        public byte[] Compress(string encodingName, string input, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).Compress(input, level);
        }

        public byte[] Compress(CompressionType compressionType, byte[] bytes, CompressionLevel level)
        {
            return GetRequiredCompression(compressionType).Compress(bytes, level);
        }

        public byte[] Compress(string encodingName, byte[] bytes, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).Compress(bytes, level);
        }

        public byte[] Compress(CompressionType compressionType, Stream stream, CompressionLevel level)
        {
            return GetRequiredCompression(compressionType).Compress(stream, level);
        }

        public byte[] Compress(string encodingName, Stream stream, bool leaveOpen, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).Compress(stream, level);
        }

        public Stream CompressToStream(CompressionType compressionType, string input)
        {
            return GetRequiredCompression(compressionType).CompressToStream(input);
        }

        public Stream CompressToStream(string encodingName, string input)
        {
            return GetRequiredCompression(encodingName).CompressToStream(input);
        }

        public Stream CompressToStream(CompressionType compressionType, byte[] bytes)
        {
            return GetRequiredCompression(compressionType).CompressToStream(bytes);
        }

        public Stream CompressToStream(string encodingName, byte[] bytes)
        {
            return GetRequiredCompression(encodingName).CompressToStream(bytes);
        }

        public Stream CompressToStream(CompressionType compressionType, Stream stream)
        {
            return GetRequiredCompression(compressionType).CompressToStream(stream);
        }

        public Stream CompressToStream(string encodingName, Stream stream)
        {
            return GetRequiredCompression(encodingName).CompressToStream(stream);
        }

        public Stream CompressToStream(CompressionType compressionType, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(compressionType).CompressToStream(stream);
        }

        public Stream CompressToStream(string encodingName, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(encodingName).CompressToStream(stream);
        }

        public Stream CompressToStream(CompressionType compressionType, string input, CompressionLevel level)
        {
            return GetRequiredCompression(compressionType).CompressToStream(input, level);
        }

        public Stream CompressToStream(string encodingName, string input, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).CompressToStream(input, level);
        }

        public Stream CompressToStream(CompressionType compressionType, byte[] bytes, CompressionLevel level)
        {
            return GetRequiredCompression(compressionType).CompressToStream(bytes, level);
        }

        public Stream CompressToStream(string encodingName, byte[] bytes, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).CompressToStream(bytes, level);
        }

        public Stream CompressToStream(CompressionType compressionType, Stream stream, CompressionLevel level)
        {
            return GetRequiredCompression(compressionType).CompressToStream(stream, level);
        }

        public Stream CompressToStream(string encodingName, Stream stream, bool leaveOpen, CompressionLevel level)
        {
            return GetRequiredCompression(encodingName).CompressToStream(stream, level);
        }

        public Task<byte[]> DecompressAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).DecompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressAsync(bytes, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).DecompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressAsync(stream, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).DecompressAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<byte[]> DecompressAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType compressionType, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).DecompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressToStreamAsync(bytes, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType compressionType, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).DecompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressToStreamAsync(stream, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType compressionType, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).DecompressToStreamAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressToStreamAsync(stream, leaveOpen, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(CompressionType compressionType, HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(compressionType).DecompressToStreamAsync(httpContent, cancellationToken);
        }

        public Task<Stream> DecompressToStreamAsync(string encodingName, HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            return GetRequiredCompression(encodingName).DecompressToStreamAsync(httpContent, cancellationToken);
        }

        public byte[] Decompress(CompressionType compressionType, byte[] bytes)
        {
            return GetRequiredCompression(compressionType).Decompress(bytes);
        }

        public byte[] Decompress(string encodingName, byte[] bytes)
        {
            return GetRequiredCompression(encodingName).Decompress(bytes);
        }

        public byte[] Decompress(CompressionType compressionType, Stream stream)
        {
            return GetRequiredCompression(compressionType).Decompress(stream);
        }

        public byte[] Decompress(string encodingName, Stream stream)
        {
            return GetRequiredCompression(encodingName).Decompress(stream);
        }

        public byte[] Decompress(CompressionType compressionType, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(compressionType).Decompress(stream, leaveOpen);
        }

        public byte[] Decompress(string encodingName, Stream stream, bool leaveOpen)
        {
            return GetRequiredCompression(encodingName).Decompress(stream, leaveOpen);
        }
    }
}