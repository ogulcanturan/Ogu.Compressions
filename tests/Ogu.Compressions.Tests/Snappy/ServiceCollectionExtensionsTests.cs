using Microsoft.Extensions.DependencyInjection;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.Snappy
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddSnappyCompression_CorrectlyRegisters()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Fastest;
            const int bufferSize = CompressionDefaults.BufferSize;
            var services = new ServiceCollection();

            // Act
            services.AddSnappyCompression();

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var snappyCompression = serviceProvider.GetService<ISnappyCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(snappyCompression);
            Assert.IsType<SnappyCompression>(compression);
            Assert.Equal(compression, snappyCompression);
            Assert.Equal(level, snappyCompression.Level);
            Assert.Equal(bufferSize, snappyCompression.BufferSize);
        }

        [Fact]
        public void AddSnappyCompression_WithCompressionOptions_CorrectlyRegisters()
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
            services.AddSnappyCompression(options);

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var snappyCompression = serviceProvider.GetService<ISnappyCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(snappyCompression);
            Assert.IsType<SnappyCompression>(compression);
            Assert.Equal(compression, snappyCompression);
            Assert.Equal(level, snappyCompression.Level);
            Assert.Equal(bufferSize, snappyCompression.BufferSize);
        }
    }
}