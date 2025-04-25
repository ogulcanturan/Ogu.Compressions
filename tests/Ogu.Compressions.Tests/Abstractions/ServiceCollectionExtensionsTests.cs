using Microsoft.Extensions.DependencyInjection;
using Ogu.Compressions.Abstractions;

namespace Ogu.Compressions.Tests.Abstractions
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddCompressionProvider_CorrectlyRegisters()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCompressionProvider();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Act
            var compressionProvider = serviceProvider.GetRequiredService<ICompressionProvider>();

            // Assert
            Assert.Empty(compressionProvider.Compressions);
        }
    }
}