using Microsoft.Extensions.DependencyInjection;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddCompressions_CorrectlyRegisters()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Fastest;
            const int bufferSize = CompressionDefaults.BufferSize;
            var services = new ServiceCollection();

            // Act
            services.AddCompressions();

            var serviceProvider = services.BuildServiceProvider();

            var brotliCompression = serviceProvider.GetService<IBrotliCompression>();
            var deflateCompression = serviceProvider.GetService<IDeflateCompression>();
            var snappyCompression = serviceProvider.GetService<ISnappyCompression>();
            var zstdCompression = serviceProvider.GetService<IZstdCompression>();
            var gzipCompression = serviceProvider.GetService<IGzipCompression>();
            var noneCompression = serviceProvider.GetService<INoneCompression>();
            var compressionProvider = serviceProvider.GetService<ICompressionProvider>();
            var compressions = serviceProvider.GetService<IEnumerable<ICompression>>()?.ToArray();

            Assert.NotNull(compressionProvider);

            var brotliCompressionFromProvider = compressionProvider.GetCompression(CompressionType.Brotli);
            var deflateCompressionFromProvider = compressionProvider.GetCompression(CompressionType.Deflate);
            var snappyCompressionFromProvider = compressionProvider.GetCompression(CompressionType.Snappy);
            var zstdCompressionFromProvider = compressionProvider.GetCompression(CompressionType.Zstd);
            var gzipCompressionFromProvider = compressionProvider.GetCompression(CompressionType.Gzip);
            var noneCompressionFromProvider = compressionProvider.GetCompression(CompressionType.None);

            using var scope = serviceProvider.CreateScope();

            var brotliCompressionFromAnotherScope = scope.ServiceProvider.GetService<IBrotliCompression>();
            var deflateCompressionFromAnotherScope = scope.ServiceProvider.GetService<IDeflateCompression>();
            var snappyCompressionFromAnotherScope = scope.ServiceProvider.GetService<ISnappyCompression>();
            var zstdCompressionFromAnotherScope = scope.ServiceProvider.GetService<IZstdCompression>();
            var gzipCompressionFromAnotherScope = scope.ServiceProvider.GetService<IGzipCompression>();
            var noneCompressionFromAnotherScope = scope.ServiceProvider.GetService<INoneCompression>();
            var compressionProviderFromAnotherScope = scope.ServiceProvider.GetRequiredService<ICompressionProvider>();
            var compressionsFromAnotherScope = scope.ServiceProvider.GetService<IEnumerable<ICompression>>()?.ToArray();

            // Assert
            Assert.NotNull(brotliCompression);
            Assert.NotNull(deflateCompression);
            Assert.NotNull(snappyCompression);
            Assert.NotNull(zstdCompression);
            Assert.NotNull(gzipCompression);
            Assert.NotNull(noneCompression);
            Assert.NotNull(compressions);
            Assert.Null(compressionProvider.GetCompression(string.Empty));
            Assert.Equal(brotliCompression, brotliCompressionFromProvider);
            Assert.Equal(brotliCompression, brotliCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.Brotli, brotliCompression.EncodingName);
            Assert.Equal(level, brotliCompression.Level);
            Assert.Equal(bufferSize, brotliCompression.BufferSize);
            Assert.Equal(deflateCompression, deflateCompressionFromProvider);
            Assert.Equal(deflateCompression, deflateCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.Deflate, deflateCompression.EncodingName);
            Assert.Equal(level, deflateCompression.Level);
            Assert.Equal(bufferSize, deflateCompression.BufferSize);
            Assert.Equal(snappyCompression, snappyCompressionFromProvider);
            Assert.Equal(snappyCompression, snappyCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.Snappy, snappyCompression.EncodingName);
            Assert.Equal(level, snappyCompression.Level);
            Assert.Equal(bufferSize, snappyCompression.BufferSize);
            Assert.Equal(zstdCompression, zstdCompressionFromProvider);
            Assert.Equal(zstdCompression, zstdCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.Zstd, zstdCompression.EncodingName);
            Assert.Equal(level, zstdCompression.Level);
            Assert.Equal(bufferSize, zstdCompression.BufferSize);
            Assert.Equal(gzipCompression, gzipCompressionFromProvider);
            Assert.Equal(gzipCompression, gzipCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.Gzip, gzipCompression.EncodingName);
            Assert.Equal(level, gzipCompression.Level);
            Assert.Equal(bufferSize, gzipCompression.BufferSize);
            Assert.Equal(noneCompression, noneCompressionFromProvider);
            Assert.Equal(noneCompression, noneCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.None, noneCompression.EncodingName);
            Assert.Equal(level, noneCompression.Level);
            Assert.Equal(0, noneCompression.BufferSize);
            Assert.Equal(compressionProvider, compressionProviderFromAnotherScope);
            Assert.Equal(compressions, compressionsFromAnotherScope);
            Assert.Equal(6, compressions.Length);
        }

        [Fact]
        public void AddCompressions_WithCompressionOptions_CorrectlyRegisters()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Optimal;
            const int bufferSize = 4096;
            var services = new ServiceCollection();

            Action<CompressionOptions> options = opts =>
            {
                opts.Level = level;
                opts.BufferSize = bufferSize;
            };

            // Act
            services.AddCompressions(options);

            var serviceProvider = services.BuildServiceProvider();

            var brotliCompression = serviceProvider.GetService<IBrotliCompression>();
            var deflateCompression = serviceProvider.GetService<IDeflateCompression>();
            var snappyCompression = serviceProvider.GetService<ISnappyCompression>();
            var zstdCompression = serviceProvider.GetService<IZstdCompression>();
            var gzipCompression = serviceProvider.GetService<IGzipCompression>();
            var noneCompression = serviceProvider.GetService<INoneCompression>();
            var compressionProvider = serviceProvider.GetService<ICompressionProvider>();
            var compressions = serviceProvider.GetService<IEnumerable<ICompression>>()?.ToArray();

            Assert.NotNull(compressionProvider);

            var brotliCompressionFromProvider = compressionProvider.GetCompression(CompressionType.Brotli);
            var deflateCompressionFromProvider = compressionProvider.GetCompression(CompressionType.Deflate);
            var snappyCompressionFromProvider = compressionProvider.GetCompression(CompressionType.Snappy);
            var zstdCompressionFromProvider = compressionProvider.GetCompression(CompressionType.Zstd);
            var gzipCompressionFromProvider = compressionProvider.GetCompression(CompressionType.Gzip);
            var noneCompressionFromProvider = compressionProvider.GetCompression(CompressionType.None);

            using var scope = serviceProvider.CreateScope();

            var brotliCompressionFromAnotherScope = scope.ServiceProvider.GetService<IBrotliCompression>();
            var deflateCompressionFromAnotherScope = scope.ServiceProvider.GetService<IDeflateCompression>();
            var snappyCompressionFromAnotherScope = scope.ServiceProvider.GetService<ISnappyCompression>();
            var zstdCompressionFromAnotherScope = scope.ServiceProvider.GetService<IZstdCompression>();
            var gzipCompressionFromAnotherScope = scope.ServiceProvider.GetService<IGzipCompression>();
            var noneCompressionFromAnotherScope = scope.ServiceProvider.GetService<INoneCompression>();
            var compressionProviderFromAnotherScope = scope.ServiceProvider.GetRequiredService<ICompressionProvider>();
            var compressionsFromAnotherScope = scope.ServiceProvider.GetService<IEnumerable<ICompression>>()?.ToArray();

            // Assert
            Assert.NotNull(brotliCompression);
            Assert.NotNull(deflateCompression);
            Assert.NotNull(snappyCompression);
            Assert.NotNull(zstdCompression);
            Assert.NotNull(gzipCompression);
            Assert.NotNull(noneCompression);
            Assert.NotNull(compressions);
            Assert.Null(compressionProvider.GetCompression(string.Empty));
            Assert.Equal(brotliCompression, brotliCompressionFromProvider);
            Assert.Equal(brotliCompression, brotliCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.Brotli, brotliCompression.EncodingName);
            Assert.Equal(level, brotliCompression.Level);
            Assert.Equal(bufferSize, brotliCompression.BufferSize);
            Assert.Equal(deflateCompression, deflateCompressionFromProvider);
            Assert.Equal(deflateCompression, deflateCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.Deflate, deflateCompression.EncodingName);
            Assert.Equal(level, deflateCompression.Level);
            Assert.Equal(bufferSize, deflateCompression.BufferSize);
            Assert.Equal(snappyCompression, snappyCompressionFromProvider);
            Assert.Equal(snappyCompression, snappyCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.Snappy, snappyCompression.EncodingName);
            Assert.Equal(level, snappyCompression.Level);
            Assert.Equal(bufferSize, snappyCompression.BufferSize);
            Assert.Equal(zstdCompression, zstdCompressionFromProvider);
            Assert.Equal(zstdCompression, zstdCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.Zstd, zstdCompression.EncodingName);
            Assert.Equal(level, zstdCompression.Level);
            Assert.Equal(bufferSize, zstdCompression.BufferSize);
            Assert.Equal(gzipCompression, gzipCompressionFromProvider);
            Assert.Equal(gzipCompression, gzipCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.Gzip, gzipCompression.EncodingName);
            Assert.Equal(level, gzipCompression.Level);
            Assert.Equal(bufferSize, gzipCompression.BufferSize);
            Assert.Equal(noneCompression, noneCompressionFromProvider);
            Assert.Equal(noneCompression, noneCompressionFromAnotherScope);
            Assert.Equal(CompressionDefaults.EncodingNames.None, noneCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, noneCompression.Level);
            Assert.Equal(0, noneCompression.BufferSize);
            Assert.Equal(compressionProvider, compressionProviderFromAnotherScope);
            Assert.Equal(compressions, compressionsFromAnotherScope);
            Assert.Equal(6, compressions.Length);
        }
    }
}