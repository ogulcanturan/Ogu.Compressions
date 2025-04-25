using Microsoft.Extensions.DependencyInjection;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.Brotli
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddBrotliCompression_CorrectlyRegisters()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Fastest;
            const int bufferSize = CompressionDefaults.BufferSize;
            var services = new ServiceCollection();

            // Act
            services.AddBrotliCompression();

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var brotliCompression = serviceProvider.GetService<IBrotliCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(brotliCompression);
            Assert.IsType<BrotliCompression>(compression);
            Assert.Equal(compression, brotliCompression);
            Assert.Equal(level, brotliCompression.Level);
            Assert.Equal(bufferSize, brotliCompression.BufferSize);
        }

        [Fact]
        public void AddBrotliCompression_WithCompressionOptions_CorrectlyRegisters()
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
            services.AddBrotliCompression(options);

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var brotliCompression = serviceProvider.GetService<IBrotliCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(brotliCompression);
            Assert.IsType<BrotliCompression>(compression);
            Assert.Equal(compression, brotliCompression);
            Assert.Equal(level, brotliCompression.Level);
            Assert.Equal(bufferSize, brotliCompression.BufferSize);
        }
    }
}