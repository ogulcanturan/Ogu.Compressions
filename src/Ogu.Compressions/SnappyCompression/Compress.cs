using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using Snappier;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public partial class SnappyCompression : CompressionBase, ISnappyCompression
    {
        public SnappyCompression(IOptions<SnappyCompressionOptions> opts) : base(opts.Value.Level, opts.Value.BufferSize) { }

        public override string EncodingName { get; } = EncodingNames.Snappy;

        public override CompressionType CompressionType { get; } = CompressionType.Snappy;

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
    }
}