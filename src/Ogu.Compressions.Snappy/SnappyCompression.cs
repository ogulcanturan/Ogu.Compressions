using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using Snappier;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    /// <summary>
    /// Implements the <see cref="ISnappyCompression"/> interface for compressing and decompressing data using the 'Snappier' library.
    /// </summary>
    public class SnappyCompression : CompressionBase, ISnappyCompression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnappyCompression"/> class using the specified options.
        /// </summary>
        /// <param name="opts">The options configuration for Snappy (snappy) compression.</param>
        public SnappyCompression(IOptions<SnappyCompressionOptions> opts) : base(opts.Value) { }

        protected override byte[] InternalCompress(byte[] bytes, CompressionLevel level)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var snappyStream = new SnappyStream(memoryStream, CompressionMode.Compress, leaveOpen: false))
                {
#if NETSTANDARD2_0
                    snappyStream.Write(bytes, 0, bytes.Length);
#else
                    snappyStream.Write(bytes);
#endif
                }

                return memoryStream.ToArray();
            }
        }

        protected override byte[] InternalCompress(Stream stream, CompressionLevel level, bool leaveOpen)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var snappyStream = new SnappyStream(memoryStream, CompressionMode.Compress, leaveOpen: false))
                {
                    stream.CopyTo(snappyStream);
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
                using (var snappyStream = new SnappyStream(memoryStream, CompressionMode.Compress, leaveOpen: false))
                {
#if NETSTANDARD2_0
                    await snappyStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
#else
                    await snappyStream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
#endif
                }

                return memoryStream.ToArray();
            }
        }

        protected override async Task<byte[]> InternalCompressAsync(Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var snappyStream = new SnappyStream(memoryStream, CompressionMode.Compress, leaveOpen: false))
                {
#if NETSTANDARD2_0
                    await stream.CopyToAsync(snappyStream).ConfigureAwait(false);
#else
                    await stream.CopyToAsync(snappyStream, cancellationToken).ConfigureAwait(false);
#endif
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
            var memoryStream = new MemoryStream();

            try
            {
                using (var snappyStream = new SnappyStream(memoryStream, CompressionMode.Compress, leaveOpen: true))
                {
#if NETSTANDARD2_0
                    await snappyStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
#else
                    await snappyStream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
#endif
                }

                memoryStream.Position = 0;

                return memoryStream;
            }
            catch
            {
#if NETSTANDARD2_0
                memoryStream.Dispose();
#else
                await memoryStream.DisposeAsync().ConfigureAwait(false);
#endif
                throw;
            }
        }

        protected override async Task<Stream> InternalCompressToStreamAsync(Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            var memoryStream = new MemoryStream();

            try
            {
                using (var snappyStream = new SnappyStream(memoryStream, CompressionMode.Compress, leaveOpen: true))
                {
#if NETSTANDARD2_0
                    await stream.CopyToAsync(snappyStream).ConfigureAwait(false);
#else
                    await stream.CopyToAsync(snappyStream, cancellationToken).ConfigureAwait(false);
#endif
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

                memoryStream.Position = 0;

                return memoryStream;
            }
            catch
            {
#if NETSTANDARD2_0
                memoryStream.Dispose();
#else
                await memoryStream.DisposeAsync().ConfigureAwait(false);
#endif
                throw;
            }
        }

        protected override Stream InternalCompressToStream(byte[] bytes, CompressionLevel level)
        {
            var memoryStream = new MemoryStream();

            try
            {
                using (var snappyStream = new SnappyStream(memoryStream, CompressionMode.Compress, leaveOpen: true))
                {
#if NETSTANDARD2_0
                    snappyStream.Write(bytes, 0, bytes.Length);
#else
                    snappyStream.Write(bytes);
#endif
                }

                memoryStream.Position = 0;

                return memoryStream;
            }
            catch
            {
                memoryStream.Dispose();

                throw;
            }
        }

        protected override Stream InternalCompressToStream(Stream stream, CompressionLevel level, bool leaveOpen)
        {
            var memoryStream = new MemoryStream();

            try
            {
                using (var snappyStream = new SnappyStream(memoryStream, CompressionMode.Compress, leaveOpen: true))
                {
                    stream.CopyTo(snappyStream);
                }

                if (leaveOpen)
                {
                    stream.Position = 0;
                }
                else
                {
                    stream.Dispose();
                }

                memoryStream.Position = 0;

                return memoryStream;
            }
            catch
            {
                memoryStream.Dispose();

                throw;
            }
        }

        protected override async Task<byte[]> InternalDecompressAsync(byte[] bytes, int bufferSize, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var snappyStream = new SnappyStream(memoryStream, CompressionMode.Decompress))
                    {
                        await snappyStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
                    }

                    return outputStream.ToArray();
                }
            }
        }

        protected override async Task<byte[]> InternalDecompressAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var snappyStream = new SnappyStream(stream, CompressionMode.Decompress, leaveOpen))
                {
                    await snappyStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
                }

                if (leaveOpen)
                {
                    stream.Position = 0;
                }

                return outputStream.ToArray();
            }
        }

        protected override async Task<Stream> InternalDecompressToStreamAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            var outputStream = new MemoryStream();
            try
            {
                using (var memoryStream = new MemoryStream(bytes))
                {

                    using (var snappyStream = new SnappyStream(memoryStream, CompressionMode.Decompress))
                    {
                        await snappyStream.CopyToAsync(outputStream, BufferSize, cancellationToken).ConfigureAwait(false);
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
                using (var snappyStream = new SnappyStream(stream, CompressionMode.Decompress, leaveOpen))
                {
                    await snappyStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
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

            return await InternalDecompressToStreamAsync(streamContent, leaveOpen, bufferSize, cancellationToken).ConfigureAwait(false);
        }

        protected override byte[] InternalDecompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var snappyStream = new SnappyStream(memoryStream, CompressionMode.Decompress))
                    {
                        snappyStream.CopyTo(outputStream);
                    }

                    return outputStream.ToArray();
                }
            }
        }

        protected override byte[] InternalDecompress(Stream stream, bool leaveOpen)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var snappyStream = new SnappyStream(stream, CompressionMode.Decompress, leaveOpen))
                {
                    snappyStream.CopyTo(outputStream);
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