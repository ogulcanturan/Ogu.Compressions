namespace Ogu.Compressions.Tests.GzipCompression
{
    public partial class GzipCompressionTests
    {
        [Fact]
        public async Task DecompressAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
#if DEBUG
            var expected = "Hello, World!"u8.ToArray();
#endif
            // Act
            var actual = await _gzipCompression.DecompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var stream = new MemoryStream(input);
#if DEBUG
            var expected = "Hello, World!"u8.ToArray();
#endif
            // Act
            var actual = await _gzipCompression.DecompressAsync(stream);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var stream = new MemoryStream(input);
#if DEBUG
            var expected = "Hello, World!"u8.ToArray();
#endif
            // Act
            var actual = await _gzipCompression.DecompressAsync(stream, true);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
            Assert.Equal(33, stream.Length);
#endif
            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
#if DEBUG
            var expected = "Hello, World!"u8.ToArray();
#endif
            // Act
            var stream = await _gzipCompression.DecompressToStreamAsync(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif
            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var rawStream = new MemoryStream(input);
#if DEBUG
            var expected = "Hello, World!"u8.ToArray();
#endif
            // Act
            var stream = await _gzipCompression.DecompressToStreamAsync(rawStream);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif
            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
            var rawStream = new MemoryStream(input);
#if DEBUG
            var expected = "Hello, World!"u8.ToArray();
#endif
            // Act
            var stream = await _gzipCompression.DecompressToStreamAsync(rawStream, true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(13, stream.Length);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif
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
#if DEBUG
            var expected = "Hello, World!"u8.ToArray();
#endif

            // Act
            var stream = await _gzipCompression.DecompressToStreamAsync(httpContent);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () =>
            {
                var streamLength = (await httpContent.ReadAsStreamAsync()).Length;
            });

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif
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
#if DEBUG
            var expected = "Hello, World!"u8.ToArray();
#endif
            // Act
            var stream = await _gzipCompression.DecompressToStreamAsync(httpContent, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(33, rawStream.Length);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif
            await stream.DisposeAsync();
            httpContent.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () =>
            {
                var streamLength = (await httpContent.ReadAsStreamAsync()).Length;
            });
        }

        [Fact]
        public void Decompress_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
#if DEBUG
            var expected = "Hello, World!"u8.ToArray();
#endif
            // Act
            var actual = _gzipCompression.Decompress(input);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
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
#if DEBUG
            var expected = "Hello, World!"u8.ToArray();
#endif
            // Act
            var actual = _gzipCompression.Decompress(stream, true);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
            Assert.Equal(33, stream.Length);
#endif
            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}