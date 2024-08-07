using Ogu.Compressions;
using Ogu.Compressions.Abstractions;
using System.Text;
using SnappyCompressionOptions = Ogu.Compressions.SnappyCompressionOptions;

namespace Sample.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var compressionTypes = Enum.GetValues<CompressionType>().Select(e => $"{e} => {(int)e}").ToList();
            Console.WriteLine("*** Enter compression type ***");
            Console.WriteLine(string.Join('\n', compressionTypes));

            var selectedCompressionType = Enum.Parse<CompressionType>(Console.ReadLine());
            var compression = GetCompression(selectedCompressionType);

            Console.WriteLine("\n*** Select which process you want to handle ***\nCompress => 0\nDecompress => 1");

            var isCompressSelected = Convert.ToInt32(Console.ReadLine()) == 0;

            while (true)
            {
                Console.WriteLine("Input: ");
                var input = Console.ReadLine();
                Console.WriteLine();

                if (isCompressSelected)
                {
                    // Compression
                    var compressedInput = compression.Compress(input);
                    var output = Convert.ToBase64String(compressedInput);
                    Console.WriteLine(output);
                }
                else
                {
                    // Decompression
                    var decompressedBytes = compression.Decompress(Convert.FromBase64String(input));
                    var output = Encoding.UTF8.GetString(decompressedBytes);
                    Console.WriteLine(output);
                }
                Console.WriteLine();
            }
        }

        private static ICompression GetCompression(CompressionType compressionType)
        {
            switch (compressionType)
            {
                case CompressionType.Brotli:
                    return new BrotliCompression(new BrotliCompressionOptions());
                case CompressionType.Deflate:
                    return new DeflateCompression(new DeflateCompressionOptions());
                case CompressionType.Snappy:
                    return new SnappyCompression(new SnappyCompressionOptions());
                case CompressionType.Zstd:
                    return new ZstdCompression(new ZstdCompressionOptions());
                case CompressionType.Gzip:
                    return new GzipCompression(new GzipCompressionOptions());
                case CompressionType.None:
                default:
                    return new NoneCompression();
            }
        }
    }
}