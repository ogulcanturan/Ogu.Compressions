using System.IO.Compression;
using System.Text;

namespace Ogu.Compressions.Tests.NoneCompression
{
    public partial class NoneCompressionTests
    {
        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StringInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var compressed = await _noneCompression.CompressAsync(input);
            var decompressed = await _noneCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = await _noneCompression.CompressAsync(input);
            var decompressed = await _noneCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StreamInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = await _noneCompression.CompressAsync(stream);
            var decompressed = await _noneCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StreamInput_WithLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var compressed = await _noneCompression.CompressAsync(stream, leaveOpen);
            var decompressed = await _noneCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);

            await stream.DisposeAsync();
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StringInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var inputBytes = Encoding.UTF8.GetBytes(input);

            // Act
            var optimalCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _noneCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _noneCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _noneCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _noneCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(inputBytes, optimalDecompressed);
            Assert.Equal(inputBytes, fastestDecompressed);
            Assert.Equal(inputBytes, noCompressionDecompressed);
            Assert.Equal(inputBytes, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _noneCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _noneCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _noneCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _noneCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StreamInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var optimalCompressed = await _noneCompression.CompressAsync(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = await _noneCompression.CompressAsync(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = await _noneCompression.CompressAsync(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = await _noneCompression.CompressAsync(stream, CompressionLevel.SmallestSize, leaveOpen: false);

            var optimalDecompressed = await _noneCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _noneCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _noneCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _noneCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var optimalCompressed = await _noneCompression.CompressAsync(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = await _noneCompression.CompressAsync(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = await _noneCompression.CompressAsync(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = await _noneCompression.CompressAsync(stream, CompressionLevel.SmallestSize, leaveOpen);

            var optimalDecompressed = await _noneCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _noneCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _noneCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _noneCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StringInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var compressed = await _noneCompression.CompressToStreamAsync(input);
            var decompressed = await _noneCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = await _noneCompression.CompressToStreamAsync(input);
            var decompressed = await _noneCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StreamInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = await _noneCompression.CompressToStreamAsync(stream);
            var decompressed = await _noneCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StreamInput_WithLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = await _noneCompression.CompressToStreamAsync(stream, leaveOpen: true);
            var decompressed = await _noneCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);

            await stream.DisposeAsync();
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StringInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var inputBytes = Encoding.UTF8.GetBytes(input);

            // Act
            var optimalCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _noneCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _noneCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _noneCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _noneCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(inputBytes, optimalDecompressed);
            Assert.Equal(inputBytes, fastestDecompressed);
            Assert.Equal(inputBytes, noCompressionDecompressed);
            Assert.Equal(inputBytes, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _noneCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _noneCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _noneCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _noneCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StreamInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var optimalCompressed = await _noneCompression.CompressToStreamAsync(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = await _noneCompression.CompressToStreamAsync(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = await _noneCompression.CompressToStreamAsync(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = await _noneCompression.CompressToStreamAsync(stream, CompressionLevel.SmallestSize, leaveOpen: false);

            var optimalDecompressed = await _noneCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _noneCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _noneCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _noneCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var optimalCompressed = await _noneCompression.CompressToStreamAsync(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = await _noneCompression.CompressToStreamAsync(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = await _noneCompression.CompressToStreamAsync(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = await _noneCompression.CompressToStreamAsync(stream, CompressionLevel.SmallestSize, leaveOpen);

            var optimalDecompressed = await _noneCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _noneCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _noneCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _noneCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressToStreamAsync_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _noneCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _noneCompression.DecompressToStreamAsync(optimalCompressed);
            var fastestDecompressed = await _noneCompression.DecompressToStreamAsync(fastestCompressed);
            var noCompressionDecompressed = await _noneCompression.DecompressToStreamAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _noneCompression.DecompressToStreamAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, ((MemoryStream)optimalDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)fastestDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)noCompressionDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)smallestSizeDecompressed).ToArray());

            await Task.WhenAll(
                optimalDecompressed.DisposeAsync().AsTask(),
                fastestDecompressed.DisposeAsync().AsTask(),
                noCompressionDecompressed.DisposeAsync().AsTask(),
                smallestSizeDecompressed.DisposeAsync().AsTask()
            );
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressToStreamAsync_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _noneCompression.DecompressToStreamAsync(optimalCompressed);
            var fastestDecompressed = await _noneCompression.DecompressToStreamAsync(fastestCompressed);
            var noCompressionDecompressed = await _noneCompression.DecompressToStreamAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _noneCompression.DecompressToStreamAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, ((MemoryStream)optimalDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)fastestDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)noCompressionDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)smallestSizeDecompressed).ToArray());

            Assert.Equal(optimalCompressed, optimalDecompressed);
            Assert.Equal(fastestCompressed, fastestDecompressed);
            Assert.Equal(noCompressionCompressed, noCompressionDecompressed);
            Assert.Equal(smallestSizeCompressed, smallestSizeDecompressed);

            await Task.WhenAll(
                optimalDecompressed.DisposeAsync().AsTask(),
                fastestDecompressed.DisposeAsync().AsTask(),
                noCompressionDecompressed.DisposeAsync().AsTask(),
                smallestSizeDecompressed.DisposeAsync().AsTask()
            );
        }

        [Fact]
        public void CompressAndDecompress_StringInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var compressed = _noneCompression.Compress(input);
            var decompressed = _noneCompression.Decompress(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public void CompressAndDecompress_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = _noneCompression.Compress(input);
            var decompressed = _noneCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public void CompressAndDecompress_StreamInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = _noneCompression.Compress(stream);
            var decompressed = _noneCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public void CompressAndDecompress_StreamInput_WithLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = _noneCompression.Compress(stream, leaveOpen: true);
            var decompressed = _noneCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);

            stream.Dispose();
        }

        [Fact]
        public void CompressAndDecompress_StringInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var inputBytes = Encoding.UTF8.GetBytes(input);

            // Act
            var optimalCompressed = _noneCompression.Compress(input, CompressionLevel.Optimal);
            var fastestCompressed = _noneCompression.Compress(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _noneCompression.Compress(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _noneCompression.Compress(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _noneCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _noneCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _noneCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _noneCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(inputBytes, optimalDecompressed);
            Assert.Equal(inputBytes, fastestDecompressed);
            Assert.Equal(inputBytes, noCompressionDecompressed);
            Assert.Equal(inputBytes, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressAndDecompress_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = _noneCompression.Compress(input, CompressionLevel.Optimal);
            var fastestCompressed = _noneCompression.Compress(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _noneCompression.Compress(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _noneCompression.Compress(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _noneCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _noneCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _noneCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _noneCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressAndDecompress_StreamInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var optimalCompressed = _noneCompression.Compress(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = _noneCompression.Compress(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = _noneCompression.Compress(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = _noneCompression.Compress(stream, CompressionLevel.SmallestSize, leaveOpen: false);

            var optimalDecompressed = _noneCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _noneCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _noneCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _noneCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressAndDecompress_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var optimalCompressed = _noneCompression.Compress(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = _noneCompression.Compress(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = _noneCompression.Compress(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = _noneCompression.Compress(stream, CompressionLevel.SmallestSize, leaveOpen);

            var optimalDecompressed = _noneCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _noneCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _noneCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _noneCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStreamAndDecompress_StringInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var compressed = _noneCompression.CompressToStream(input);
            var decompressed = _noneCompression.Decompress(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = _noneCompression.CompressToStream(input);
            var decompressed = _noneCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompress_StreamInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = _noneCompression.CompressToStream(stream);
            var decompressed = _noneCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompress_StreamInput_WithLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = _noneCompression.CompressToStream(stream, leaveOpen: true);
            var decompressed = _noneCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);

            stream.Dispose();
        }

        [Fact]
        public void CompressToStreamAndDecompress_StringInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var inputBytes = Encoding.UTF8.GetBytes(input);

            // Act
            var optimalCompressed = _noneCompression.CompressToStream(input, CompressionLevel.Optimal);
            var fastestCompressed = _noneCompression.CompressToStream(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _noneCompression.CompressToStream(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _noneCompression.CompressToStream(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _noneCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _noneCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _noneCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _noneCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(inputBytes, optimalDecompressed);
            Assert.Equal(inputBytes, fastestDecompressed);
            Assert.Equal(inputBytes, noCompressionDecompressed);
            Assert.Equal(inputBytes, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompress_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = _noneCompression.CompressToStream(input, CompressionLevel.Optimal);
            var fastestCompressed = _noneCompression.CompressToStream(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _noneCompression.CompressToStream(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _noneCompression.CompressToStream(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _noneCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _noneCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _noneCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _noneCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompress_StreamInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var optimalCompressed = _noneCompression.CompressToStream(stream, CompressionLevel.Optimal);
            var fastestCompressed = _noneCompression.CompressToStream(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = _noneCompression.CompressToStream(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = _noneCompression.CompressToStream(stream, CompressionLevel.SmallestSize, leaveOpen: false);

            var optimalDecompressed = _noneCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _noneCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _noneCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _noneCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStreamAndDecompress_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            const bool leaveOpen = true;

            // Act
            var optimalCompressed = _noneCompression.CompressToStream(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = _noneCompression.CompressToStream(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = _noneCompression.CompressToStream(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = _noneCompression.CompressToStream(stream, CompressionLevel.SmallestSize, leaveOpen);

            var optimalDecompressed = _noneCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _noneCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _noneCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _noneCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}