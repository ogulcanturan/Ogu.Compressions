﻿using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public partial class DeflateCompression : CompressionBase, IDeflateCompression
    {
        public DeflateCompression(IOptions<DeflateCompressionOptions> opts) : base(opts.Value.Level, opts.Value.BufferSize) { }

        public override string EncodingName { get; } = EncodingNames.Deflate;

        public override CompressionType CompressionType { get; } = CompressionType.Deflate;

        protected override byte[] InternalCompress(byte[] bytes, CompressionLevel level)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(memoryStream, level))
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

        protected override byte[] InternalCompress(Stream stream, CompressionLevel level, bool leaveOpen)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(memoryStream, level))
                {
                    stream.CopyTo(deflateStream);
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
                using (var deflateStream = new DeflateStream(memoryStream, level))
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

        protected override async Task<byte[]> InternalCompressAsync(Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(memoryStream, level))
                {

#if NETSTANDARD2_0
                    await stream.CopyToAsync(deflateStream).ConfigureAwait(false);
#else
                    await stream.CopyToAsync(deflateStream, cancellationToken).ConfigureAwait(false);
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
                using (var deflateStream = new DeflateStream(memoryStream, level))
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

        protected override async Task<Stream> InternalCompressToStreamAsync(Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            var memoryStream = new MemoryStream();

            try
            {
                using (var deflateStream = new DeflateStream(memoryStream, level))
                {
#if NETSTANDARD2_0
                    await stream.CopyToAsync(deflateStream).ConfigureAwait(false);
#else
                    await stream.CopyToAsync(deflateStream, cancellationToken).ConfigureAwait(false);
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