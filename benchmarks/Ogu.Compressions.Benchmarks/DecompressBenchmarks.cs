using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Ogu.Compressions.Abstractions;
using Ogu.Compressions.Benchmarks.Utility;
using System.IO.Compression;
using Ogu.Compressions.Brotli.Native;

namespace Ogu.Compressions.Benchmarks
{
    [MemoryDiagnoser]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    public class DecompressBenchmarks
    {
        [Params(CompressionLevel.Fastest, CompressionLevel.Optimal, CompressionLevel.SmallestSize)]
        public CompressionLevel Level;

        [Params(4096, 8192, 81920)]
        public int BufferSize;

        [Params(1024, 10_000)] // 1024, 10_000, 100_000, 1_000_000, 10_000_000, 100_000_000 - 1KB, 10KB, 100KB, 1MB, 10MB, 100MB
        public int Size;

        [Params(Constants.DataTypes.Repetitive, Constants.DataTypes.Random, Constants.DataTypes.Blob)]
        public string DataType;

        private ICompressionProvider _provider;
        private byte[] _brotliCompressedData;
        private byte[] _nativeBrotliCompressedData;
        private byte[] _deflateCompressedData;
        private byte[] _gzipCompressedData;
        private byte[] _snappyCompressedData;
        private byte[] _zstdCompressedData;
        private ICompression _brotliCompression;
        private ICompression _nativeBrotliCompression;
        private ICompression _deflateCompression;
        private ICompression _gzipCompression;
        private ICompression _snappyCompression;
        private ICompression _zstdCompression;

        [GlobalSetup]
        public void Setup()
        {
            _brotliCompression = new BrotliCompression(new BrotliCompressionOptions(Level, BufferSize));
            _nativeBrotliCompression = new NativeBrotliCompression(new NativeBrotliCompressionOptions(Level, BufferSize));
            _deflateCompression = new DeflateCompression(new DeflateCompressionOptions(Level, BufferSize));
            _gzipCompression = new GzipCompression(new GzipCompressionOptions(Level, BufferSize));
            _snappyCompression = new SnappyCompression(new SnappyCompressionOptions(Level, BufferSize));
            _zstdCompression = new ZstdCompression(new ZstdCompressionOptions(Level, BufferSize));

            var compressions = new ICompression[]
            {
                _brotliCompression,
                _nativeBrotliCompression,
                _deflateCompression,
                _gzipCompression,
                _snappyCompression,
                _zstdCompression
            };

            _provider = new CompressionProvider(compressions, new CompressionTypeResolver());

            DataGenerator.EnsureDataGenerated(compressions, Size);

            _brotliCompressedData = DataGenerator.LoadGeneratedCompressedData(nameof(BrotliCompression), Level, DataType, Size);
            _nativeBrotliCompressedData = DataGenerator.LoadGeneratedCompressedData(nameof(NativeBrotliCompression), Level, DataType, Size);
            _deflateCompressedData = DataGenerator.LoadGeneratedCompressedData(nameof(DeflateCompression), Level, DataType, Size);
            _gzipCompressedData = DataGenerator.LoadGeneratedCompressedData(nameof(GzipCompression), Level, DataType, Size);
            _snappyCompressedData = DataGenerator.LoadGeneratedCompressedData(nameof(SnappyCompression), Level, DataType, Size);
            _zstdCompressedData = DataGenerator.LoadGeneratedCompressedData(nameof(ZstdCompression), Level, DataType, Size);
        }

        [BenchmarkCategory(Constants.Categories.Decompress)]
        [Benchmark]
        public async Task<byte[]> Brotli() => await _brotliCompression.DecompressAsync(_brotliCompressedData);

        [BenchmarkCategory(Constants.Categories.Decompress)]
        [Benchmark]
        public async Task<byte[]> NativeBrotli() => await _nativeBrotliCompression.DecompressAsync(_nativeBrotliCompressedData);

        [BenchmarkCategory(Constants.Categories.Decompress)]
        [Benchmark]
        public async Task<byte[]> Deflate() => await _deflateCompression.DecompressAsync(_deflateCompressedData);

        [BenchmarkCategory(Constants.Categories.Decompress)]
        [Benchmark]
        public async Task<byte[]> Gzip() => await _gzipCompression.DecompressAsync(_gzipCompressedData);

        [BenchmarkCategory(Constants.Categories.Decompress)]
        [Benchmark]
        public async Task<byte[]> Snappy() => await _snappyCompression.DecompressAsync(_snappyCompressedData);

        [BenchmarkCategory(Constants.Categories.Decompress)]
        [Benchmark]
        public async Task<byte[]> Zstd() => await _zstdCompression.DecompressAsync(_zstdCompressedData);
    }
}
