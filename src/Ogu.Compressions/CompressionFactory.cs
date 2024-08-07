using Microsoft.Extensions.DependencyInjection;
using Ogu.Compressions.Abstractions;
using System;

namespace Ogu.Compressions
{
    public class CompressionFactory : ICompressionFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CompressionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public ICompression Get(CompressionType compressionType)
        {
            switch (compressionType)
            {
                case CompressionType.Brotli:
                    return _serviceProvider.GetService<IBrotliCompression>();
                case CompressionType.Deflate:
                    return _serviceProvider.GetService<IDeflateCompression>();
                case CompressionType.Snappy:
                    return _serviceProvider.GetService<ISnappyCompression>();
                case CompressionType.Zstd:
                    return _serviceProvider.GetService<IZstdCompression>();
                case CompressionType.Gzip:
                    return _serviceProvider.GetService<IGzipCompression>();
                case CompressionType.None:
                default:
                    return _serviceProvider.GetService<INoneCompression>();
            }
        }

        /// <inheritdoc/>
        public ICompression Get(string encodingName)
        {
            _ = CompressionHelpers.TryConvertEncodingNameToCompressionType(encodingName, out var compressionType);

            return Get(compressionType);
        }
    }
}