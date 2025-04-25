using Microsoft.Extensions.DependencyInjection;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.None
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddNoneCompression_CorrectlyRegisters()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Fastest;
            const int bufferSize = 0;
            var services = new ServiceCollection();

            // Act
            services.AddNoneCompression();

            var serviceProvider = services.BuildServiceProvider();

            var compression = serviceProvider.GetService<ICompression>();
            var noneCompression = serviceProvider.GetService<INoneCompression>();

            // Assert
            Assert.NotNull(compression);
            Assert.NotNull(noneCompression);
            Assert.IsType<NoneCompression>(compression);
            Assert.Equal(compression, noneCompression);
            Assert.Equal(level, noneCompression.Level);
            Assert.Equal(bufferSize, noneCompression.BufferSize);
        }
    }
}