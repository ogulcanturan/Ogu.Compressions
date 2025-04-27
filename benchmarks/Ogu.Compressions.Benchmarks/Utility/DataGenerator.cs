using Ogu.Compressions.Abstractions;
using System.IO.Compression;
using System.Text;

namespace Ogu.Compressions.Benchmarks.Utility
{
    internal static class DataGenerator
    {
        public const string GeneratedDataFolderName = "generated-data";
        public const string GeneratedDataFileNameFormat = "generated-data-{0}-{1}";
        public const string GeneratedCompressedDataFileNameFormat = "generated-compressed-data-{0}-{1}-{2}-{3}";

        private static readonly string DataDirectory = Path.Combine(AppContext.BaseDirectory, GeneratedDataFolderName);

        public static string GetGeneratedFilePath(string dataType, int size) => Path.Combine(DataDirectory, string.Format(GeneratedDataFileNameFormat, dataType, size));
        public static string GetGeneratedCompressedFilePath(CompressionType type, CompressionLevel level, string dataType, int size) => Path.Combine(DataDirectory, string.Format(GeneratedCompressedDataFileNameFormat, type, level, dataType, size));

        public static void EnsureDataGenerated(ICompression[] compressions, int size)
        {
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }

            var dataTypes = new string[]
            {
                Constants.DataTypes.Repetitive,
                Constants.DataTypes.Random,
                Constants.DataTypes.Blob,
            };

            foreach (var dataType in dataTypes)
            {
                var filePath = GetGeneratedFilePath(dataType, size) ?? throw new NullReferenceException("FilePath cannot be null!");

                if (File.Exists(filePath))
                {
                    continue;
                }

                var data = GenerateData(dataType, size);

                File.WriteAllBytes(filePath, data);

                foreach (var compression in compressions)
                {
                    var compressedData = compression.Compress(data, compression.Level);

                    File.WriteAllBytes(GetGeneratedCompressedFilePath(compression.Type, compression.Level, dataType, size), compressedData);
                }
            }
        }

        public static byte[] GenerateData(string dataType, int size)
        {
            return dataType switch
            {
                Constants.DataTypes.Repetitive => GenerateRepetitive(size),
                Constants.DataTypes.Random => GenerateRandom(size),
                Constants.DataTypes.Blob => GenerateBinaryBlob(size),
                _ => throw new InvalidOperationException($"Unknown dataType: {dataType}")
            };
        }

        public static byte[] LoadGeneratedData(string dataType, int size)
        {
            var filePath = GetGeneratedFilePath(dataType, size);

            if (File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }

            throw new FileNotFoundException($"Data file for dataType: {dataType} and size: {size} not found.");
        }

        public static byte[] LoadGeneratedCompressedData(CompressionType type, CompressionLevel level, string dataType, int size)
        {
            var filePath = GetGeneratedCompressedFilePath(type, level, dataType, size);

            if (File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }

            throw new FileNotFoundException($"Data file for compression type: {type}, level: {level}, dataType: {dataType} and size {size} not found.");
        }

        public static byte[] GenerateRepetitive(int length, char character = 'A') => Encoding.UTF8.GetBytes(new string(character, length));

        public static byte[] GenerateRandom(int length)
        {
            var buffer = new byte[length];
            Random.Shared.NextBytes(buffer);
            return buffer;
        }

        public static byte[] GenerateBinaryBlob(int length)
        {
            var data = new byte[length];

            for (var i = 0; i < length; i++)
            {
                data[i] = (byte)(i % 256);
            }

            return data;
        }
    }
}