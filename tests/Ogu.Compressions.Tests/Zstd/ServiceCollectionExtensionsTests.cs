using Microsoft.Extensions.DependencyInjection;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.Zstd
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddZstdCompression_CorrectlyRegisters()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Fastest;
            const int bufferSize = CompressionDefaults.BufferSize;
            var services = new ServiceCollection();

            // Act
            services.AddZstdCompression();

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var zstdCompression = serviceProvider.GetService<IZstdCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(zstdCompression);
            Assert.IsType<ZstdCompression>(compression);
            Assert.Equal(compression, zstdCompression);
            Assert.Equal(level, zstdCompression.Level);
            Assert.Equal(bufferSize, zstdCompression.BufferSize);
        }

        [Fact]
        public void AddZstdCompression_WithCompressionOptions_CorrectlyRegisters()
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
            services.AddZstdCompression(options);

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var zstdCompression = serviceProvider.GetService<IZstdCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(zstdCompression);
            Assert.IsType<ZstdCompression>(compression);
            Assert.Equal(compression, zstdCompression);
            Assert.Equal(level, zstdCompression.Level);
            Assert.Equal(bufferSize, zstdCompression.BufferSize);
        }
    }
}