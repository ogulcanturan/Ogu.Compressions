using Ogu.Compressions.Abstractions;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public partial class DeflateCompression : CompressionBase, IDeflateCompression
    {
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

                return outputStream.ToArray();
            }
        }

        protected override async Task<Stream> InternalDecompressAsync(HttpContent httpContent, int bufferSize, CancellationToken cancellationToken = default)
        {
            var streamContent =
#if NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP3_1
                await httpContent.ReadAsStreamAsync().ConfigureAwait(false);
#else
                await httpContent.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
#endif

            return await InternalDecompressToAsync(streamContent, false, bufferSize, cancellationToken).ConfigureAwait(false);
        }

        protected override async Task<Stream> InternalDecompressToStreamAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            var outputStream = new MemoryStream();

            try
            {
                using (var memoryStream = new MemoryStream(bytes))
                {
                    using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, leaveOpen: true))
                    {
                        await deflateStream.CopyToAsync(outputStream, BufferSize, cancellationToken).ConfigureAwait(false);
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

        protected override async Task<Stream> InternalDecompressToAsync(Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken = default)
        {
            var outputStream = new MemoryStream();

            try
            {
                using (var deflateStream = new DeflateStream(stream, CompressionMode.Decompress, leaveOpen))
                {
                    await deflateStream.CopyToAsync(outputStream, bufferSize, cancellationToken).ConfigureAwait(false);
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

        protected override byte[] InternalDecompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, leaveOpen: true))
                    {
                        deflateStream.CopyTo(outputStream);
                    }

                    return outputStream.ToArray();
                }
            }
        }

        protected override byte[] InternalDecompress(Stream stream, bool leaveOpen)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(stream, CompressionMode.Decompress, leaveOpen))
                {
                    deflateStream.CopyTo(outputStream);
                }

                return outputStream.ToArray();
            }
        }
    }
}