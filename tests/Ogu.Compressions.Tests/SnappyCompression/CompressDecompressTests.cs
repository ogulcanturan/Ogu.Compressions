using System.IO.Compression;
using System.Text;

namespace Ogu.Compressions.Tests.SnappyCompression
{
    public partial class SnappyCompressionTests
    {
        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StringInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var compressed = await _snappyCompression.CompressAsync(input);
            var decompressed = await _snappyCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = await _snappyCompression.CompressAsync(input);
            var decompressed = await _snappyCompression.DecompressAsync(compressed);

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
            var compressed = await _snappyCompression.CompressAsync(stream);
            var decompressed = await _snappyCompression.DecompressAsync(compressed);

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
            var compressed = await _snappyCompression.CompressAsync(stream, leaveOpen);
            var decompressed = await _snappyCompression.DecompressAsync(compressed);

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
            var optimalCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _snappyCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _snappyCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _snappyCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _snappyCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _snappyCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _snappyCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _snappyCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _snappyCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _snappyCompression.CompressAsync(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = await _snappyCompression.CompressAsync(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = await _snappyCompression.CompressAsync(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = await _snappyCompression.CompressAsync(stream, CompressionLevel.SmallestSize, leaveOpen: false);

            var optimalDecompressed = await _snappyCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _snappyCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _snappyCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _snappyCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _snappyCompression.CompressAsync(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = await _snappyCompression.CompressAsync(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = await _snappyCompression.CompressAsync(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = await _snappyCompression.CompressAsync(stream, CompressionLevel.SmallestSize, leaveOpen);

            var optimalDecompressed = await _snappyCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _snappyCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _snappyCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _snappyCompression.DecompressAsync(smallestSizeCompressed);

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
            var compressed = await _snappyCompression.CompressToStreamAsync(input);
            var decompressed = await _snappyCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = await _snappyCompression.CompressToStreamAsync(input);
            var decompressed = await _snappyCompression.DecompressAsync(compressed);

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
            var compressed = await _snappyCompression.CompressToStreamAsync(stream);
            var decompressed = await _snappyCompression.DecompressAsync(compressed);

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
            var compressed = await _snappyCompression.CompressToStreamAsync(stream, leaveOpen: true);
            var decompressed = await _snappyCompression.DecompressAsync(compressed);

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
            var optimalCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _snappyCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _snappyCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _snappyCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _snappyCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _snappyCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _snappyCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _snappyCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _snappyCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _snappyCompression.CompressToStreamAsync(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = await _snappyCompression.CompressToStreamAsync(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = await _snappyCompression.CompressToStreamAsync(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = await _snappyCompression.CompressToStreamAsync(stream, CompressionLevel.SmallestSize, leaveOpen: false);

            var optimalDecompressed = await _snappyCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _snappyCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _snappyCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _snappyCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _snappyCompression.CompressToStreamAsync(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = await _snappyCompression.CompressToStreamAsync(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = await _snappyCompression.CompressToStreamAsync(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = await _snappyCompression.CompressToStreamAsync(stream, CompressionLevel.SmallestSize, leaveOpen);

            var optimalDecompressed = await _snappyCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _snappyCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _snappyCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _snappyCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _snappyCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _snappyCompression.DecompressToStreamAsync(optimalCompressed);
            var fastestDecompressed = await _snappyCompression.DecompressToStreamAsync(fastestCompressed);
            var noCompressionDecompressed = await _snappyCompression.DecompressToStreamAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _snappyCompression.DecompressToStreamAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _snappyCompression.DecompressToStreamAsync(optimalCompressed);
            var fastestDecompressed = await _snappyCompression.DecompressToStreamAsync(fastestCompressed);
            var noCompressionDecompressed = await _snappyCompression.DecompressToStreamAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _snappyCompression.DecompressToStreamAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, ((MemoryStream)optimalDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)fastestDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)noCompressionDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)smallestSizeDecompressed).ToArray());

            Assert.Throws<ObjectDisposedException>(() => optimalCompressed.Length);
            Assert.Throws<ObjectDisposedException>(() => fastestCompressed.Length);
            Assert.Throws<ObjectDisposedException>(() => noCompressionCompressed.Length);
            Assert.Throws<ObjectDisposedException>(() => smallestSizeCompressed.Length);

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
            var compressed = _snappyCompression.Compress(input);
            var decompressed = _snappyCompression.Decompress(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public void CompressAndDecompress_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = _snappyCompression.Compress(input);
            var decompressed = _snappyCompression.Decompress(compressed);

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
            var compressed = _snappyCompression.Compress(stream);
            var decompressed = _snappyCompression.Decompress(compressed);

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
            var compressed = _snappyCompression.Compress(stream, leaveOpen: true);
            var decompressed = _snappyCompression.Decompress(compressed);

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
            var optimalCompressed = _snappyCompression.Compress(input, CompressionLevel.Optimal);
            var fastestCompressed = _snappyCompression.Compress(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _snappyCompression.Compress(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _snappyCompression.Compress(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _snappyCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _snappyCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _snappyCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _snappyCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _snappyCompression.Compress(input, CompressionLevel.Optimal);
            var fastestCompressed = _snappyCompression.Compress(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _snappyCompression.Compress(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _snappyCompression.Compress(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _snappyCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _snappyCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _snappyCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _snappyCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _snappyCompression.Compress(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = _snappyCompression.Compress(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = _snappyCompression.Compress(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = _snappyCompression.Compress(stream, CompressionLevel.SmallestSize, leaveOpen: false);

            var optimalDecompressed = _snappyCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _snappyCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _snappyCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _snappyCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _snappyCompression.Compress(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = _snappyCompression.Compress(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = _snappyCompression.Compress(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = _snappyCompression.Compress(stream, CompressionLevel.SmallestSize, leaveOpen);

            var optimalDecompressed = _snappyCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _snappyCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _snappyCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _snappyCompression.Decompress(smallestSizeCompressed);

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
            var compressed = _snappyCompression.CompressToStream(input);
            var decompressed = _snappyCompression.Decompress(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = _snappyCompression.CompressToStream(input);
            var decompressed = _snappyCompression.Decompress(compressed);

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
            var compressed = _snappyCompression.CompressToStream(stream);
            var decompressed = _snappyCompression.Decompress(compressed);

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
            var compressed = _snappyCompression.CompressToStream(stream, leaveOpen: true);
            var decompressed = _snappyCompression.Decompress(compressed);

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
            var optimalCompressed = _snappyCompression.CompressToStream(input, CompressionLevel.Optimal);
            var fastestCompressed = _snappyCompression.CompressToStream(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _snappyCompression.CompressToStream(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _snappyCompression.CompressToStream(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _snappyCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _snappyCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _snappyCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _snappyCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _snappyCompression.CompressToStream(input, CompressionLevel.Optimal);
            var fastestCompressed = _snappyCompression.CompressToStream(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _snappyCompression.CompressToStream(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _snappyCompression.CompressToStream(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _snappyCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _snappyCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _snappyCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _snappyCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _snappyCompression.CompressToStream(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = _snappyCompression.CompressToStream(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = _snappyCompression.CompressToStream(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = _snappyCompression.CompressToStream(stream, CompressionLevel.SmallestSize, leaveOpen: false);

            var optimalDecompressed = _snappyCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _snappyCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _snappyCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _snappyCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _snappyCompression.CompressToStream(stream, CompressionLevel.Optimal, leaveOpen);
            var fastestCompressed = _snappyCompression.CompressToStream(stream, CompressionLevel.Fastest, leaveOpen);
            var noCompressionCompressed = _snappyCompression.CompressToStream(stream, CompressionLevel.NoCompression, leaveOpen);
            var smallestSizeCompressed = _snappyCompression.CompressToStream(stream, CompressionLevel.SmallestSize, leaveOpen);

            var optimalDecompressed = _snappyCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _snappyCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _snappyCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _snappyCompression.Decompress(smallestSizeCompressed);

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