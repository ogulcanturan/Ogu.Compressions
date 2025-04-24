using Moq;
using Ogu.Compressions.Abstractions;
using System.Net;

namespace Ogu.Compressions.Tests
{
    public class DecompressionHandlerTests
    {
        [Fact]
        public async Task SendAsync_ResponseWithContentEncoding_ShouldDecompressContent()
        {
            // Arrange
            var decompressedBytes = "Hello, World!"u8.ToArray();
            var mockCompression = new Mock<ICompression>();
            var mockCompressionFactory = new Mock<ICompressionFactory>();

            var decompressedContent = new MemoryStream(decompressedBytes);

            mockCompressionFactory.Setup(factory => factory.GetCompression(CompressionType.Brotli)).Returns(mockCompression.Object);
            mockCompression
                .Setup(factory => factory.DecompressToStreamAsync(It.IsAny<HttpContent>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(decompressedContent);

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(decompressedBytes)
            };

            responseMessage.Content.Headers.ContentEncoding.Add(EncodingNames.Brotli);

            var handler = new TestHandler(responseMessage);
            var decompressionHandler = new DecompressionHandler(mockCompressionFactory.Object)
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

            mockCompression.Verify(factory =>
                factory.DecompressToStreamAsync(It.IsAny<HttpContent>(), It.IsAny<CancellationToken>()), Times.Once);
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
