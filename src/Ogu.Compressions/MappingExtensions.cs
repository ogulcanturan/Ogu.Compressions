using System.IO.Compression;
using ZstdSharp;

namespace Ogu.Compressions
{
    public static class MappingExtensions
    {
        public static int ToZstd(this CompressionLevel compressionLevel)
        {
            switch (compressionLevel)
            {
                case CompressionLevel.Optimal:
                    return Compressor.MaxCompressionLevel;
                case CompressionLevel.Fastest:
                    return Compressor.MinCompressionLevel;
                case CompressionLevel.NoCompression:
                default:
                    return Compressor.DefaultCompressionLevel;
            }
        }
    }
}