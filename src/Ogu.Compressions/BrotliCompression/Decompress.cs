using Ogu.Compressions.Abstractions;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public partial class BrotliCompression : CompressionBase, ICompression
    {
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
#if NETSTANDARD2_0
                    using (var brotliStream = new Brotli.BrotliStream(memoryStream, CompressionMode.Decompress, leaveOpen: false))
#else
                    using (var brotliStream = new BrotliStream(memoryStream, CompressionMode.Decompress, leaveOpen: false))
#endif
                    {
                        await brotliStream.CopyToAsync(outputStream, BufferSize, cancellationToken).ConfigureAwait(false);
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

        protected override byte[] InternalDecompress(byte[] bytes)
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
                        brotliStream.CopyTo(outputStream);
                    }

                    return outputStream.ToArray();
                }
            }
        }

        protected override byte[] InternalDecompress(Stream stream, bool leaveOpen)
        {
            using (var outputStream = new MemoryStream())
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(stream, CompressionMode.Decompress, leaveOpen))
#else
                using (var brotliStream = new BrotliStream(stream, CompressionMode.Decompress, leaveOpen))
#endif
                {
                    brotliStream.CopyTo(outputStream);
                }

                return outputStream.ToArray();
            }
        }
    }
}