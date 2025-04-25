namespace Ogu.Compressions.Tests.Gzip
{
    public partial class GzipCompressionTests
    {
        [Fact]
        public async Task DecompressAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = await _gzipCompression.DecompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var stream = new MemoryStream(input);
            
            // Act
            var actual = await _gzipCompression.DecompressAsync(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var stream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var actual = await _gzipCompression.DecompressAsync(stream, leaveOpen);

            // Assert
            Assert.NotEmpty(actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = await _gzipCompression.DecompressToStreamAsync(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var rawStream = new MemoryStream(input);

            // Act
            var stream = await _gzipCompression.DecompressToStreamAsync(rawStream);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var rawStream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var stream = await _gzipCompression.DecompressToStreamAsync(rawStream, leaveOpen);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(13, stream.Length);

            await rawStream.DisposeAsync();
            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_HttpContentInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var rawStream = new MemoryStream(input);
            var httpContent = new StreamContent(rawStream);

            // Act
            var stream = await _gzipCompression.DecompressToStreamAsync(httpContent);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () => _ = (await httpContent.ReadAsStreamAsync()).Length);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_HttpContentInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var rawStream = new MemoryStream(input);
            var httpContent = new StreamContent(rawStream);
            const bool leaveOpen = true;

            // Act
            var stream = await _gzipCompression.DecompressToStreamAsync(httpContent, leaveOpen);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(33, rawStream.Length);

            await stream.DisposeAsync();
            httpContent.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () => _ = (await httpContent.ReadAsStreamAsync()).Length);
        }

        [Fact]
        public void Decompress_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = _gzipCompression.Decompress(input);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void Decompress_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var stream = new MemoryStream(input);
#if DEBUG
            var expected = "Hello, World!"u8.ToArray();
#endif
            // Act
            var actual = _gzipCompression.Decompress(stream);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Decompress_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var stream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var actual = _gzipCompression.Decompress(stream, leaveOpen);

            // Assert
            Assert.NotEmpty(actual);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}