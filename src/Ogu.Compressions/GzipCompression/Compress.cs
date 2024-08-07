using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public partial class GzipCompression : CompressionBase, IGzipCompression
    {
        public GzipCompression(IOptions<GzipCompressionOptions> opts) : base(opts.Value.Level, opts.Value.BufferSize) { }

        public override string EncodingName { get; } = EncodingNames.Gzip;

        public override CompressionType CompressionType { get; } = CompressionType.Gzip;

        protected override byte[] InternalCompress(byte[] bytes, CompressionLevel level)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(memoryStream, level))
                {
#if NETSTANDARD2_0
                    gZipStream.Write(bytes, 0, bytes.Length);
#else
                    gZipStream.Write(bytes);
#endif
                }

                return memoryStream.ToArray();
            }
        }

        protected override byte[] InternalCompress(Stream stream, CompressionLevel level, bool leaveOpen)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(memoryStream, level))
                {
                    stream.CopyTo(gZipStream);
                }

                if (!leaveOpen)
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
                using (var gZipStream = new GZipStream(memoryStream, level))
                {
#if NETSTANDARD2_0
                    await gZipStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
#else
                    await gZipStream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
#endif
                }

                return memoryStream.ToArray();
            }
        }

        protected override async Task<byte[]> InternalCompressAsync(Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(memoryStream, level))
                {
#if NETSTANDARD2_0
                    await stream.CopyToAsync(gZipStream).ConfigureAwait(false);
#else
                    await stream.CopyToAsync(gZipStream, cancellationToken).ConfigureAwait(false);
#endif
                }

                if (!leaveOpen)
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
                using (var gZipStream = new GZipStream(memoryStream, level))
                {
#if NETSTANDARD2_0
                    await gZipStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
#else
                    await gZipStream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
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
                using (var gZipStream = new GZipStream(memoryStream, level))
                {
#if NETSTANDARD2_0
                    await stream.CopyToAsync(gZipStream).ConfigureAwait(false);
#else
                    await stream.CopyToAsync(gZipStream, cancellationToken).ConfigureAwait(false);
#endif
                }

                if (!leaveOpen)
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
    }
}