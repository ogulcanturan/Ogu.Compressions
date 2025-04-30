using Microsoft.Extensions.DependencyInjection;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;
using System.Reflection;
using Ogu.Compressions.Brotli.Native;

namespace Ogu.Compressions.Tests.Brotli.Native
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddNativeBrotliCompression_CorrectlyRegisters()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Fastest;
            const int bufferSize = CompressionDefaults.BufferSize;
            var services = new ServiceCollection();

            // Act
            services.AddNativeBrotliCompression();

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var brotliCompression = serviceProvider.GetService<IBrotliCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(brotliCompression);
            Assert.IsType<NativeBrotliCompression>(compression);
            Assert.Equal(compression, brotliCompression);
            Assert.Equal(level, brotliCompression.Level);
            Assert.Equal(bufferSize, brotliCompression.BufferSize);

            var type = brotliCompression.GetType();

            var actualWindowSize = (uint)type
                .GetField("_windowSize", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(brotliCompression);

            Assert.Equal(CompressionDefaults.NativeBrotli.WindowSize, actualWindowSize);
        }

        [Fact]
        public void AddNativeBrotliCompression_WithCompressionOptions_CorrectlyRegisters()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Optimal;
            const int bufferSize = 4096;
            const uint windowSize = 10;
            var services = new ServiceCollection();

            Action<NativeBrotliCompressionOptions> options = opts =>
            {
                opts.Level = level;
                opts.BufferSize = bufferSize;
                opts.WindowSize = windowSize;
            };

            // Act
            services.AddNativeBrotliCompression(options);

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var brotliCompression = serviceProvider.GetService<IBrotliCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(brotliCompression);
            Assert.IsType<NativeBrotliCompression>(compression);
            Assert.Equal(compression, brotliCompression);
            Assert.Equal(level, brotliCompression.Level);
            Assert.Equal(bufferSize, brotliCompression.BufferSize);


            var type = brotliCompression.GetType();

            var actualWindowSize = (uint)type
                .GetField("_windowSize", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(brotliCompression);

            Assert.Equal(windowSize, actualWindowSize);
        }
    }
}