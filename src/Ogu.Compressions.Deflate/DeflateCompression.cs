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
    /// Implements the <see cref="IDeflateCompression"/> interface for compressing and decompressing data using the built-in 'System.IO.Compression' assembly.
    /// </summary>
    public class DeflateCompression : CompressionBase, IDeflateCompression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeflateCompression"/> class using the specified options.
        /// </summary>
        /// <param name="opts">The options configuration for Deflate (deflate) compression.</param>
        public DeflateCompression(IOptions<DeflateCompressionOptions> opts) : base(opts.Value) { }

        protected override byte[] InternalCompress(byte[] bytes, CompressionLevel level)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(memoryStream, level, leaveOpen: false))
                {
#if NETSTANDARD2_0
                    deflateStream.Write(bytes, 0, bytes.Length);
#else
                    deflateStream.Write(bytes);
#endif
                }

                return memoryStream.ToArray();
            }
        }

        protected override byte[] InternalCompress(Stream stream, CompressionLevel level, bool leaveOpen, int bufferSize)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(memoryStream, level, leaveOpen: false))
                {
                    stream.CopyTo(deflateStream, bufferSize);
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
                using (var deflateStream = new DeflateStream(memoryStream, level, leaveOpen: false))
                {
#if NETSTANDARD2_0
                    await deflateStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
#else
                    await deflateStream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
#endif
                }

                return memoryStream.ToArray();
            }
        }

        protected override async Task<byte[]> InternalCompressAsync(Stream stream, CompressionLevel level, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(memoryStream, level, leaveOpen: false))
                {
                    await stream.CopyToAsync(deflateStream, bufferSize, cancellationToken).ConfigureAwait(false);
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
                using (var deflateStream = new DeflateStream(memoryStream, level, leaveOpen: true))
                {
#if NETSTANDARD2_0
                    await deflateStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
#else
                    await deflateStream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
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

        protected override async Task<Stream> InternalCompressToStreamAsync(Stream stream, CompressionLevel level, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            var memoryStream = new MemoryStream();

            try
            {
                using (var deflateStream = new DeflateStream(memoryStream, level, leaveOpen: true))
                {
                    await stream.CopyToAsync(deflateStream, bufferSize, cancellationToken).ConfigureAwait(false);
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
                using (var deflateStream = new DeflateStream(memoryStream, level, leaveOpen: true))
                {
#if NETSTANDARD2_0
                    deflateStream.Write(bytes, 0, bytes.Length);
#else
                    deflateStream.Write(bytes);
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

        protected override Stream InternalCompressToStream(Stream stream, CompressionLevel level, bool leaveOpen, int bufferSize)
        {
            var memoryStream = new MemoryStream();

            try
            {
                using (var deflateStream = new DeflateStream(memoryStream, level, leaveOpen: true))
                {
                    stream.CopyTo(deflateStream, bufferSize);
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
                    using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, leaveOpen: true))
                    {
                        await deflateStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
                    }

                    return outputStream.ToArray();
                }
            }
        }

        protected override async Task<byte[]> InternalDecompressAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(stream, CompressionMode.Decompress, leaveOpen))
                {
                    await deflateStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
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
                    using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, leaveOpen: true))
                    {
                        await deflateStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
                    }
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

        protected override async Task<Stream> InternalDecompressToStreamAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            var outputStream = new MemoryStream();

            try
            {
                using (var deflateStream = new DeflateStream(stream, CompressionMode.Decompress, leaveOpen))
                {
                    await deflateStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
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
                    using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, leaveOpen: true))
                    {
                        deflateStream.CopyTo(outputStream, bufferSize);
                    }

                    return outputStream.ToArray();
                }
            }
        }

        protected override byte[] InternalDecompress(Stream stream, bool leaveOpen, int bufferSize)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(stream, CompressionMode.Decompress, leaveOpen))
                {
                    deflateStream.CopyTo(outputStream, bufferSize);
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