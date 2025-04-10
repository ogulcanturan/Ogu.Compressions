﻿using System.IO.Compression;
using System.Text;

namespace Ogu.Compressions.Tests.BrotliCompression
{
    public partial class BrotliCompressionTests
    {
        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StringInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var compressed = await _brotliCompression.CompressAsync(input);
            var decompressed = await _brotliCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = await _brotliCompression.CompressAsync(input);
            var decompressed = await _brotliCompression.DecompressAsync(compressed);

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
            var compressed = await _brotliCompression.CompressAsync(stream);
            var decompressed = await _brotliCompression.DecompressAsync(compressed);

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
            var compressed = await _brotliCompression.CompressAsync(stream, leaveOpen: true);
            var decompressed = await _brotliCompression.DecompressAsync(compressed);

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
            var optimalCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _brotliCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _brotliCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _brotliCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _brotliCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _brotliCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _brotliCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _brotliCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _brotliCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _brotliCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _brotliCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _brotliCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _brotliCompression.CompressAsync(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _brotliCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _brotliCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _brotliCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _brotliCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _brotliCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _brotliCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _brotliCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _brotliCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _brotliCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _brotliCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _brotliCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _brotliCompression.DecompressAsync(smallestSizeCompressed);

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
            var compressed = await _brotliCompression.CompressToStreamAsync(input);
            var decompressed = await _brotliCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = await _brotliCompression.CompressToStreamAsync(input);
            var decompressed = await _brotliCompression.DecompressAsync(compressed);

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
            var compressed = await _brotliCompression.CompressToStreamAsync(stream);
            var decompressed = await _brotliCompression.DecompressAsync(compressed);

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
            var compressed = await _brotliCompression.CompressToStreamAsync(stream, leaveOpen: true);
            var decompressed = await _brotliCompression.DecompressAsync(compressed);

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
            var optimalCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _brotliCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _brotliCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _brotliCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _brotliCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _brotliCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _brotliCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _brotliCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _brotliCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _brotliCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _brotliCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _brotliCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _brotliCompression.CompressToStreamAsync(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _brotliCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _brotliCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _brotliCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _brotliCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _brotliCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _brotliCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _brotliCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _brotliCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _brotliCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _brotliCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _brotliCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _brotliCompression.DecompressAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _brotliCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _brotliCompression.DecompressToStreamAsync(optimalCompressed);
            var fastestDecompressed = await _brotliCompression.DecompressToStreamAsync(fastestCompressed);
            var noCompressionDecompressed = await _brotliCompression.DecompressToStreamAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _brotliCompression.DecompressToStreamAsync(smallestSizeCompressed);

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
            var optimalCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _brotliCompression.DecompressToStreamAsync(optimalCompressed);
            var fastestDecompressed = await _brotliCompression.DecompressToStreamAsync(fastestCompressed);
            var noCompressionDecompressed = await _brotliCompression.DecompressToStreamAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _brotliCompression.DecompressToStreamAsync(smallestSizeCompressed);

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
            var compressed = _brotliCompression.Compress(input);
            var decompressed = _brotliCompression.Decompress(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public void CompressAndDecompress_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = _brotliCompression.Compress(input);
            var decompressed = _brotliCompression.Decompress(compressed);

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
            var compressed = _brotliCompression.Compress(stream);
            var decompressed = _brotliCompression.Decompress(compressed);

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
            var compressed = _brotliCompression.Compress(stream, leaveOpen: true);
            var decompressed = _brotliCompression.Decompress(compressed);

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
            var optimalCompressed = _brotliCompression.Compress(input, CompressionLevel.Optimal);
            var fastestCompressed = _brotliCompression.Compress(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _brotliCompression.Compress(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _brotliCompression.Compress(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _brotliCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _brotliCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _brotliCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _brotliCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _brotliCompression.Compress(input, CompressionLevel.Optimal);
            var fastestCompressed = _brotliCompression.Compress(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _brotliCompression.Compress(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _brotliCompression.Compress(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _brotliCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _brotliCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _brotliCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _brotliCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _brotliCompression.Compress(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _brotliCompression.Compress(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _brotliCompression.Compress(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _brotliCompression.Compress(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = _brotliCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _brotliCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _brotliCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _brotliCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _brotliCompression.Compress(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _brotliCompression.Compress(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _brotliCompression.Compress(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _brotliCompression.Compress(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = _brotliCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _brotliCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _brotliCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _brotliCompression.Decompress(smallestSizeCompressed);

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
            var compressed = _brotliCompression.CompressToStream(input);
            var decompressed = _brotliCompression.Decompress(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed =  _brotliCompression.CompressToStream(input);
            var decompressed =  _brotliCompression.Decompress(compressed);

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
            var compressed = _brotliCompression.CompressToStream(stream);
            var decompressed = _brotliCompression.Decompress(compressed);

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
            var compressed = _brotliCompression.CompressToStream(stream, leaveOpen: true);
            var decompressed = _brotliCompression.Decompress(compressed);

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
            var optimalCompressed = _brotliCompression.CompressToStream(input, CompressionLevel.Optimal);
            var fastestCompressed = _brotliCompression.CompressToStream(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _brotliCompression.CompressToStream(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _brotliCompression.CompressToStream(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _brotliCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _brotliCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _brotliCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _brotliCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _brotliCompression.CompressToStream(input, CompressionLevel.Optimal);
            var fastestCompressed = _brotliCompression.CompressToStream(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _brotliCompression.CompressToStream(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _brotliCompression.CompressToStream(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _brotliCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _brotliCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _brotliCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _brotliCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _brotliCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _brotliCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _brotliCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _brotliCompression.CompressToStream(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = _brotliCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _brotliCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _brotliCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _brotliCompression.Decompress(smallestSizeCompressed);

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
            var optimalCompressed = _brotliCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _brotliCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _brotliCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _brotliCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = _brotliCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _brotliCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _brotliCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _brotliCompression.Decompress(smallestSizeCompressed);

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