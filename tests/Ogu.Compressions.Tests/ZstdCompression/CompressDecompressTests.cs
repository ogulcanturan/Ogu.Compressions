using System.IO.Compression;
using System.Text;

namespace Ogu.Compressions.Tests.ZstdCompression
{
    public partial class ZstdCompressionTests
    {
        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StringInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var compressed = await _zstdCompression.CompressAsync(input);
            var decompressed = await _zstdCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = await _zstdCompression.CompressAsync(input);
            var decompressed = await _zstdCompression.DecompressAsync(compressed);

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
            var compressed = await _zstdCompression.CompressAsync(stream);
            var decompressed = await _zstdCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StreamInput_WithLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = await _zstdCompression.CompressAsync(stream, leaveOpen: true);
            var decompressed = await _zstdCompression.DecompressAsync(compressed);

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
            var optimalCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _zstdCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _zstdCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _zstdCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _zstdCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _zstdCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _zstdCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _zstdCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _zstdCompression.DecompressAsync(smallestSizeCompressed);

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

            // Act
            var optimalCompressed = await _zstdCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _zstdCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _zstdCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _zstdCompression.CompressAsync(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _zstdCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _zstdCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _zstdCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _zstdCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = await _zstdCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _zstdCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _zstdCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _zstdCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _zstdCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _zstdCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _zstdCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _zstdCompression.DecompressAsync(smallestSizeCompressed);

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
            var compressed = await _zstdCompression.CompressToStreamAsync(input);
            var decompressed = await _zstdCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = await _zstdCompression.CompressToStreamAsync(input);
            var decompressed = await _zstdCompression.DecompressAsync(compressed);

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
            var compressed = await _zstdCompression.CompressToStreamAsync(stream);
            var decompressed = await _zstdCompression.DecompressAsync(compressed);

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
            var compressed = await _zstdCompression.CompressToStreamAsync(stream, leaveOpen: true);
            var decompressed = await _zstdCompression.DecompressAsync(compressed);

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
            var optimalCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _zstdCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _zstdCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _zstdCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _zstdCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _zstdCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _zstdCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _zstdCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _zstdCompression.DecompressAsync(smallestSizeCompressed);

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

            // Act
            var optimalCompressed = await _zstdCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _zstdCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _zstdCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _zstdCompression.CompressToStreamAsync(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _zstdCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _zstdCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _zstdCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _zstdCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = await _zstdCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _zstdCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _zstdCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _zstdCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _zstdCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _zstdCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _zstdCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _zstdCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _zstdCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _zstdCompression.DecompressToStreamAsync(optimalCompressed);
            var fastestDecompressed = await _zstdCompression.DecompressToStreamAsync(fastestCompressed);
            var noCompressionDecompressed = await _zstdCompression.DecompressToStreamAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _zstdCompression.DecompressToStreamAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _zstdCompression.DecompressToStreamAsync(optimalCompressed);
            var fastestDecompressed = await _zstdCompression.DecompressToStreamAsync(fastestCompressed);
            var noCompressionDecompressed = await _zstdCompression.DecompressToStreamAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _zstdCompression.DecompressToStreamAsync(smallestSizeCompressed);

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
            var compressed = _zstdCompression.Compress(input);
            var decompressed = _zstdCompression.Decompress(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public void CompressAndDecompress_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = _zstdCompression.Compress(input);
            var decompressed = _zstdCompression.Decompress(compressed);

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
            var compressed = _zstdCompression.Compress(stream);
            var decompressed = _zstdCompression.Decompress(compressed);

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
            var compressed = _zstdCompression.Compress(stream, leaveOpen: true);
            var decompressed = _zstdCompression.Decompress(compressed);

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
            var optimalCompressed = _zstdCompression.Compress(input, CompressionLevel.Optimal);
            var fastestCompressed = _zstdCompression.Compress(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _zstdCompression.Compress(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _zstdCompression.Compress(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _zstdCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _zstdCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _zstdCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _zstdCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _zstdCompression.Compress(input, CompressionLevel.Optimal);
            var fastestCompressed = _zstdCompression.Compress(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _zstdCompression.Compress(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _zstdCompression.Compress(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _zstdCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _zstdCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _zstdCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _zstdCompression.Decompress(smallestSizeCompressed);

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

            // Act
            var optimalCompressed = _zstdCompression.Compress(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _zstdCompression.Compress(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _zstdCompression.Compress(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _zstdCompression.Compress(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = _zstdCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _zstdCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _zstdCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _zstdCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressAndDecompress_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = _zstdCompression.Compress(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _zstdCompression.Compress(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _zstdCompression.Compress(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _zstdCompression.Compress(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = _zstdCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _zstdCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _zstdCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _zstdCompression.Decompress(smallestSizeCompressed);

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
            var compressed = _zstdCompression.CompressToStream(input);
            var decompressed = _zstdCompression.Decompress(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = _zstdCompression.CompressToStream(input);
            var decompressed = _zstdCompression.Decompress(compressed);

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
            var compressed = _zstdCompression.CompressToStream(stream);
            var decompressed = _zstdCompression.Decompress(compressed);

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
            var compressed = _zstdCompression.CompressToStream(stream, leaveOpen: true);
            var decompressed = _zstdCompression.Decompress(compressed);

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
            var optimalCompressed = _zstdCompression.CompressToStream(input, CompressionLevel.Optimal);
            var fastestCompressed = _zstdCompression.CompressToStream(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _zstdCompression.CompressToStream(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _zstdCompression.CompressToStream(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _zstdCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _zstdCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _zstdCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _zstdCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _zstdCompression.CompressToStream(input, CompressionLevel.Optimal);
            var fastestCompressed = _zstdCompression.CompressToStream(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _zstdCompression.CompressToStream(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _zstdCompression.CompressToStream(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _zstdCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _zstdCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _zstdCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _zstdCompression.Decompress(smallestSizeCompressed);

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

            // Act
            var optimalCompressed = _zstdCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _zstdCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _zstdCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _zstdCompression.CompressToStream(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = _zstdCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _zstdCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _zstdCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _zstdCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompress_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = _zstdCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _zstdCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _zstdCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _zstdCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = _zstdCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _zstdCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _zstdCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _zstdCompression.Decompress(smallestSizeCompressed);

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