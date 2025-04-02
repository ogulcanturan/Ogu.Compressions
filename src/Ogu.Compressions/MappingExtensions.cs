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
#if NET6_0_OR_GREATER
                case CompressionLevel.SmallestSize:
#endif
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