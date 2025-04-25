using Microsoft.Extensions.DependencyInjection;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.Deflate
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddDeflateCompression_CorrectlyRegisters()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Fastest;
            const int bufferSize = CompressionDefaults.BufferSize;
            var services = new ServiceCollection();

            // Act
            services.AddDeflateCompression();

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var deflateCompression = serviceProvider.GetService<IDeflateCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(deflateCompression);
            Assert.IsType<DeflateCompression>(compression);
            Assert.Equal(compression, deflateCompression);
            Assert.Equal(level, deflateCompression.Level);
            Assert.Equal(bufferSize, deflateCompression.BufferSize);
        }

        [Fact]
        public void AddDeflateCompression_WithCompressionOptions_CorrectlyRegisters()
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
            services.AddDeflateCompression(options);

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var deflateCompression = serviceProvider.GetService<IDeflateCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(deflateCompression);
            Assert.IsType<DeflateCompression>(compression);
            Assert.Equal(compression, deflateCompression);
            Assert.Equal(level, deflateCompression.Level);
            Assert.Equal(bufferSize, deflateCompression.BufferSize);
        }
    }
}