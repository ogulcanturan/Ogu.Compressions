using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    /// <summary>
    /// Implements the <see cref="IBrotliCompression"/> interface for compressing and decompressing data 
    /// using the built-in 'System.IO.Compression.Brotli' assembly for target frameworks above .NET Standard 2.1+; 
    /// otherwise, it uses 'Brotli.NET' for .NET Standard 2.0.
    /// </summary>
    public class BrotliCompression : CompressionBase, IBrotliCompression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrotliCompression"/> class using the specified options.
        /// </summary>
        /// <param name="opts">The options configuration for Brotli (br) compression.</param>
        public BrotliCompression(IOptions<BrotliCompressionOptions> opts) : base(opts.Value) { }

        protected override byte[] InternalCompress(byte[] bytes, CompressionLevel level)
        {
            using (var memoryStream = new MemoryStream())
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(memoryStream, CompressionMode.Compress, leaveOpen: false))
#else
                using (var brotliStream = new BrotliStream(memoryStream, level, leaveOpen: false))
#endif
                {
#if NETSTANDARD2_0
                    brotliStream.Write(bytes, 0, bytes.Length);
#else
                    brotliStream.Write(bytes);
#endif
                }

                return memoryStream.ToArray();
            }
        }

        protected override byte[] InternalCompress(Stream stream, CompressionLevel level, bool leaveOpen, int bufferSize)
        {
            using (var memoryStream = new MemoryStream())
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(memoryStream, CompressionMode.Compress, leaveOpen: false))
#else
                using (var brotliStream = new BrotliStream(memoryStream, level, leaveOpen: false))
#endif
                {
                    stream.CopyTo(brotliStream, bufferSize);
                }

                if (leaveOpen)
                {
                    stream.Position = 0;
                }
                else
                {
                    stream.Dispose();
                }

                return memoryStream.ToArray();
            }
        }

        protected override async Task<byte[]> InternalCompressAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream())
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(memoryStream, CompressionMode.Compress, leaveOpen: false))
#else
                using (var brotliStream = new BrotliStream(memoryStream, level, leaveOpen: false))
#endif
                {
#if NETSTANDARD2_0
                    await brotliStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
#else
                    await brotliStream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
#endif
                }

                return memoryStream.ToArray();
            }
        }

        protected override async Task<byte[]> InternalCompressAsync(Stream stream, CompressionLevel level, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream())
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(memoryStream, CompressionMode.Compress, leaveOpen: false))
#else
                using (var brotliStream = new BrotliStream(memoryStream, level, leaveOpen: false))
#endif
                {
                    await stream.CopyToAsync(brotliStream, bufferSize, cancellationToken).ConfigureAwait(false);
                }

                if (leaveOpen)
                {
                    stream.Position = 0;
                }
                else
                {
#if NETSTANDARD2_0
                    stream.Dispose();
#else
                    await stream.DisposeAsync();
#endif
                }

                return memoryStream.ToArray();
            }
        }

        protected override async Task<Stream> InternalCompressToStreamAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default)
        {
            var outputStream = new MemoryStream();

            try
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(outputStream, CompressionMode.Compress, leaveOpen: true))
#else
                using (var brotliStream = new BrotliStream(outputStream, level, leaveOpen: true))
#endif
                {
#if NETSTANDARD2_0
                    await brotliStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
#else
                    await brotliStream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
#endif
                }

                outputStream.Position = 0;

                return outputStream;
            }
            catch
            {
#if NETSTANDARD2_0
                outputStream.Dispose();
#else
                await outputStream.DisposeAsync().ConfigureAwait(false);
#endif
                throw;
            }
        }

        protected override async Task<Stream> InternalCompressToStreamAsync(Stream stream, CompressionLevel level, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            var outputStream = new MemoryStream();

            try
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(outputStream, CompressionMode.Compress, leaveOpen: true))
#else
                using (var brotliStream = new BrotliStream(outputStream, level, leaveOpen: true))
#endif
                {
                    await stream.CopyToAsync(brotliStream, bufferSize, cancellationToken).ConfigureAwait(false);
                }

                if (leaveOpen)
                {
                    stream.Position = 0;
                }
                else
                {
#if NETSTANDARD2_0
                    stream.Dispose();
#else
                    await stream.DisposeAsync();
#endif
                }

                outputStream.Position = 0;

                return outputStream;
            }
            catch
            {
#if NETSTANDARD2_0
                outputStream.Dispose();
#else
                await outputStream.DisposeAsync().ConfigureAwait(false);
#endif
                throw;
            }
        }

        protected override Stream InternalCompressToStream(byte[] bytes, CompressionLevel level)
        {
            var outputStream = new MemoryStream();

            try
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(outputStream, CompressionMode.Compress, leaveOpen: true))
#else
                using (var brotliStream = new BrotliStream(outputStream, level, leaveOpen: true))
#endif
                {
#if NETSTANDARD2_0
                    brotliStream.Write(bytes, 0, bytes.Length);
#else
                    brotliStream.Write(bytes);
#endif
                }

                outputStream.Position = 0;

                return outputStream;
            }
            catch
            {
                outputStream.Dispose();

                throw;
            }
        }

        protected override Stream InternalCompressToStream(Stream stream, CompressionLevel level, bool leaveOpen, int bufferSize)
        {
            var outputStream = new MemoryStream();

            try
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(outputStream, CompressionMode.Compress, leaveOpen: true))
#else
                using (var brotliStream = new BrotliStream(outputStream, level, leaveOpen: true))
#endif
                {
                    stream.CopyTo(brotliStream, bufferSize);
                }

                if (leaveOpen)
                {
                    stream.Position = 0;
                }
                else
                {
                    stream.Dispose();
                }

                outputStream.Position = 0;

                return outputStream;
            }
            catch
            {
                outputStream.Dispose();

                throw;
            }
        }

        protected override async Task<byte[]> InternalDecompressAsync(byte[] bytes, int bufferSize, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
#if NETSTANDARD2_0
                    using (var brotliStream = new Brotli.BrotliStream(memoryStream, CompressionMode.Decompress, leaveOpen: false))
#else
                    using (var brotliStream = new BrotliStream(memoryStream, CompressionMode.Decompress, leaveOpen: false))
#endif
                    {
                        await brotliStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
                    }

                    return outputStream.ToArray();
                }
            }
        }

        protected override async Task<byte[]> InternalDecompressAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            using (var outputStream = new MemoryStream())
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(stream, CompressionMode.Decompress, leaveOpen))
#else
                using (var brotliStream = new BrotliStream(stream, CompressionMode.Decompress, leaveOpen))
#endif
                {
                    await brotliStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
                }

                if (leaveOpen)
                {
                    stream.Position = 0;
                }

                return outputStream.ToArray();
            }
        }

        protected override async Task<Stream> InternalDecompressToStreamAsync(byte[] bytes, int bufferSize, CancellationToken cancellationToken = default)
        {
            var outputStream = new MemoryStream();

            try
            {
                using (var memoryStream = new MemoryStream(bytes))
                {
#if NETSTANDARD2_0
                    using (var brotliStream = new Brotli.BrotliStream(memoryStream, CompressionMode.Decompress, leaveOpen: false))
#else
                    using (var brotliStream = new BrotliStream(memoryStream, CompressionMode.Decompress, leaveOpen: false))
#endif
                    {
                        await brotliStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
                    }

                    outputStream.Position = 0;

                    return outputStream;
                }
            }
            catch
            {
#if NETSTANDARD2_0
                outputStream.Dispose();
#else
                await outputStream.DisposeAsync().ConfigureAwait(false);
#endif
                throw;
            }
        }

        protected override async Task<Stream> InternalDecompressToStreamAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            var outputStream = new MemoryStream();

            try
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(stream, CompressionMode.Decompress, leaveOpen))
#else
                using (var brotliStream = new BrotliStream(stream, CompressionMode.Decompress, leaveOpen))
#endif
                {
                    await brotliStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
                }

                if (leaveOpen)
                {
                    stream.Position = 0;
                }

                outputStream.Position = 0;

                return outputStream;
            }
            catch
            {
#if NETSTANDARD2_0
                outputStream.Dispose();
#else
                await outputStream.DisposeAsync().ConfigureAwait(false);
#endif
                throw;
            }
        }

        protected override async Task<Stream> InternalDecompressToStreamAsync(HttpContent httpContent, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            var streamContent =
#if NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP3_1
                await httpContent.ReadAsStreamAsync().ConfigureAwait(false);
#else
                await httpContent.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
#endif
            return await InternalDecompressToStreamAsync(streamContent, leaveOpen: leaveOpen, bufferSize, cancellationToken).ConfigureAwait(false);
        }

        protected override byte[] InternalDecompress(byte[] bytes, int bufferSize)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
#if NETSTANDARD2_0
                    using (var brotliStream = new Brotli.BrotliStream(memoryStream, CompressionMode.Decompress, leaveOpen: false))
#else
                    using (var brotliStream = new BrotliStream(memoryStream, CompressionMode.Decompress, leaveOpen: false))
#endif
                    {
                        brotliStream.CopyTo(outputStream, bufferSize);
                    }

                    return outputStream.ToArray();
                }
            }
        }

        protected override byte[] InternalDecompress(Stream stream, bool leaveOpen, int bufferSize)
        {
            using (var outputStream = new MemoryStream())
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(stream, CompressionMode.Decompress, leaveOpen))
#else
                using (var brotliStream = new BrotliStream(stream, CompressionMode.Decompress, leaveOpen))
#endif
                {
                    brotliStream.CopyTo(outputStream, bufferSize);
                }

                if (leaveOpen)
                {
                    stream.Position = 0;
                }

                return outputStream.ToArray();
            }
        }
    }
}