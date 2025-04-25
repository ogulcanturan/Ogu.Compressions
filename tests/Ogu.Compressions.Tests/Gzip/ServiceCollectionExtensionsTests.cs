using Microsoft.Extensions.DependencyInjection;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.Gzip
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddGzipCompression_CorrectlyRegisters()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Fastest;
            const int bufferSize = CompressionDefaults.BufferSize;
            var services = new ServiceCollection();

            // Act
            services.AddGzipCompression();

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var gzipCompression = serviceProvider.GetService<IGzipCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(gzipCompression);
            Assert.IsType<GzipCompression>(compression);
            Assert.Equal(compression, gzipCompression);
            Assert.Equal(level, gzipCompression.Level);
            Assert.Equal(bufferSize, gzipCompression.BufferSize);
        }

        [Fact]
        public void AddGzipCompression_WithCompressionOptions_CorrectlyRegisters()
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
            services.AddGzipCompression(options);

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var gzipCompression = serviceProvider.GetService<IGzipCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(gzipCompression);
            Assert.IsType<GzipCompression>(compression);
            Assert.Equal(compression, gzipCompression);
            Assert.Equal(level, gzipCompression.Level);
            Assert.Equal(bufferSize, gzipCompression.BufferSize);
        }
    }
}