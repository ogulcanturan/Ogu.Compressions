using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Ogu.Compressions.Abstractions;
using Ogu.Compressions.Benchmarks.Utility;
using Ogu.Compressions.Brotli.Native;
using System.IO.Compression;

namespace Ogu.Compressions.Benchmarks
{
    [MemoryDiagnoser]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    public class CompressBenchmarks
    {
        [Params(CompressionLevel.Fastest, CompressionLevel.Optimal, CompressionLevel.SmallestSize)]
        public CompressionLevel Level;

        [Params(4096, 8192, 81920)]
        public int BufferSize;

        [Params(1024, 10_000)] // 1024, 10_000, 100_000, 1_000_000, 10_000_000, 100_000_000 - 1KB, 10KB, 100KB, 1MB, 10MB, 100MB
        public int Size;

        [Params(Constants.DataTypes.Repetitive, Constants.DataTypes.Random, Constants.DataTypes.Blob)]
        public string DataType;

        private byte[] _data;
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

            DataGenerator.EnsureDataGenerated(compressions, Size);

            _data = DataGenerator.LoadGeneratedData(DataType, Size);
        }

        [BenchmarkCategory(Constants.Categories.Compress)]
        [Benchmark]
        public async Task<byte[]> Brotli() => await _brotliCompression.CompressAsync(_data);

        [BenchmarkCategory(Constants.Categories.Compress)]
        [Benchmark]
        public async Task<byte[]> NativeBrotli() => await _nativeBrotliCompression.CompressAsync(_data);

        [BenchmarkCategory(Constants.Categories.Compress)]
        [Benchmark]
        public async Task<byte[]> Deflate() => await _deflateCompression.CompressAsync(_data);

        [BenchmarkCategory(Constants.Categories.Compress)]
        [Benchmark]
        public async Task<byte[]> Gzip() => await _gzipCompression.CompressAsync(_data);

        [BenchmarkCategory(Constants.Categories.Compress)]
        [Benchmark]
        public async Task<byte[]> Snappy() => await _snappyCompression.CompressAsync(_data);

        [BenchmarkCategory(Constants.Categories.Compress)]
        [Benchmark]
        public async Task<byte[]> Zstd() => await _zstdCompression.CompressAsync(_data);
    }
}