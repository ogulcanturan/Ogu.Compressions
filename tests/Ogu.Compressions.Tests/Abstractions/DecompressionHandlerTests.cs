using Moq;
using Ogu.Compressions.Abstractions;
using System.Net;

namespace Ogu.Compressions.Tests.Abstractions
{
    public class DecompressionHandlerTests
    {
        [Fact]
        public async Task SendAsync_ResponseWithContentEncoding_ShouldDecompressContent()
        {
            // Arrange
            var decompressedBytes = "Hello, World!"u8.ToArray();
            const string encodingName = CompressionDefaults.EncodingNames.Brotli;
            var compressionMock = new Mock<ICompression>();
            var compressionProviderMock = new Mock<ICompressionProvider>();

            var decompressedContent = new MemoryStream(decompressedBytes);

            compressionProviderMock.Setup(provider => provider.GetCompression(encodingName)).Returns(compressionMock.Object);
            compressionMock.Setup(compression => compression.Type).Returns(CompressionType.Brotli);
            compressionMock
                .Setup(compression => compression.DecompressToStreamAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(decompressedContent);

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(decompressedBytes)
            };

            responseMessage.Content.Headers.ContentEncoding.Add(encodingName);

            var handler = new TestHandler(responseMessage);
            var decompressionHandler = new DecompressionHandler(compressionProviderMock.Object)
            {
                InnerHandler = handler
            };

            var httpClient = new HttpClient(decompressionHandler);

            // Act
            var response = await httpClient.GetAsync("https://google.com");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(decompressedBytes, await response.Content.ReadAsByteArrayAsync());
            Assert.Empty(response.Content.Headers.ContentEncoding);

            compressionMock.Verify(factory =>
                factory.DecompressToStreamAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        private class TestHandler : DelegatingHandler
        {
            private readonly HttpResponseMessage _response;

            public TestHandler(HttpResponseMessage response)
            {
                _response = response;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_response);
            }
        }
    }
}