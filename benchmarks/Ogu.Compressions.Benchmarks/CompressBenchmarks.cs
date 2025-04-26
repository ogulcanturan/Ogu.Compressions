using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Ogu.Compressions.Abstractions;
using Ogu.Compressions.Benchmarks.Utility;
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
        private ICompressionProvider _provider;

        [GlobalSetup]
        public void Setup()
        {
            var compressions = new ICompression[]
            {
                new BrotliCompression(new BrotliCompressionOptions(Level, BufferSize)),
                new DeflateCompression(new DeflateCompressionOptions(Level, BufferSize)),
                new GzipCompression(new GzipCompressionOptions(Level, BufferSize)),
                new SnappyCompression(new SnappyCompressionOptions(Level, BufferSize)),
                new ZstdCompression(new ZstdCompressionOptions(Level, BufferSize)),
            };

            _provider = new CompressionProvider(compressions, new CompressionTypeResolver());

            DataGenerator.EnsureDataGenerated(compressions, Size);

            _data = DataGenerator.LoadGeneratedData(DataType, Size);
        }

        [BenchmarkCategory(Constants.Categories.Compress)]
        [Benchmark]
        public async Task<byte[]> Brotli() => await _provider.GetRequiredCompression(CompressionType.Brotli).CompressAsync(_data);

        [BenchmarkCategory(Constants.Categories.Compress)]
        [Benchmark]
        public async Task<byte[]> Deflate() => await _provider.GetRequiredCompression(CompressionType.Deflate).CompressAsync(_data);

        [BenchmarkCategory(Constants.Categories.Compress)]
        [Benchmark]
        public async Task<byte[]> Gzip() => await _provider.GetRequiredCompression(CompressionType.Gzip).CompressAsync(_data);

        [BenchmarkCategory(Constants.Categories.Compress)]
        [Benchmark]
        public async Task<byte[]> Snappy() => await _provider.GetRequiredCompression(CompressionType.Snappy).CompressAsync(_data);

        [BenchmarkCategory(Constants.Categories.Compress)]
        [Benchmark]
        public async Task<byte[]> Zstd() => await _provider.GetRequiredCompression(CompressionType.Zstd).CompressAsync(_data);
    }
}