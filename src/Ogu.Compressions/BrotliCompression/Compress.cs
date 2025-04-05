using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public partial class BrotliCompression : CompressionBase, IBrotliCompression
    {
        public BrotliCompression(IOptions<BrotliCompressionOptions> opts) : base(opts.Value.Level, opts.Value.BufferSize) { }

        public override string EncodingName { get; } = EncodingNames.Brotli;

        public override CompressionType CompressionType { get; } = CompressionType.Brotli;

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

        protected override byte[] InternalCompress(Stream stream, CompressionLevel level, bool leaveOpen)
        {
            using (var memoryStream = new MemoryStream())
            {
#if NETSTANDARD2_0
                using (var brotliStream = new Brotli.BrotliStream(memoryStream, CompressionMode.Compress, leaveOpen: false))
#else
                using (var brotliStream = new BrotliStream(memoryStream, level, leaveOpen: false))
#endif
                {
                    stream.CopyTo(brotliStream);
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

        protected override async Task<byte[]> InternalCompressAsync(Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
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
                    await stream.CopyToAsync(brotliStream).ConfigureAwait(false);
#else
                    await stream.CopyToAsync(brotliStream, cancellationToken).ConfigureAwait(false);
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

        protected override async Task<Stream> InternalCompressToStreamAsync(Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
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
                    await stream.CopyToAsync(brotliStream).ConfigureAwait(false);
#else
                    await stream.CopyToAsync(brotliStream, cancellationToken).ConfigureAwait(false);
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

        protected override Stream InternalCompressToStream(Stream stream, CompressionLevel level, bool leaveOpen)
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
                    stream.CopyTo(brotliStream);
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
    }
}