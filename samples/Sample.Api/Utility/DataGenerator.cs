using Ogu.Compressions.Abstractions;
using Sample.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Sample.Api.Utility
{
    internal static class DataGenerator
    {
        public const string GeneratedDataFolderName = "generated-data";
        public const string GeneratedDataFileNameFormat = "generated-data-{0}-{1}";
        public const string GeneratedCompressedDataFileNameFormat = "generated-compressed-data-{0}-{1}-{2}-{3}";

        private static readonly string DataDirectory = Path.Combine(AppContext.BaseDirectory, GeneratedDataFolderName);

        public static string GetGeneratedFilePath(string dataType, long size) => Path.Combine(DataDirectory, string.Format(GeneratedDataFileNameFormat, dataType, size));
        public static string GetGeneratedCompressedFilePath(CompressionType type, CompressionLevel level, string dataType, long size) => Path.Combine(DataDirectory, string.Format(GeneratedCompressedDataFileNameFormat, type, level, dataType, size));

        public static DataGeneratorStatistic[] EnsureDataGenerated(ICompression[] compressions, long size, CompressionLevel? level = null, bool overwrite = false)
        {
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }

            var dataTypes = Enum.GetValues<DataType>();
            
            var dataGeneratorStatistics = new List<DataGeneratorStatistic>();

            foreach (var dataType in dataTypes)
            {
                var dataTypeName = $"{dataType}";

                var filePath = GetGeneratedFilePath(dataTypeName, size) ?? throw new NullReferenceException("FilePath cannot be null!");

                if (overwrite)
                {
                    File.Delete(filePath);

                    foreach (var compression in compressions)
                    {
                        var compressionLevel = level ?? compression.Level;

                        var compressedFilePath = GetGeneratedCompressedFilePath(compression.Type, compressionLevel, dataTypeName, size) ?? throw new NullReferenceException("CompressedFilePath cannot be null!");

                        File.Delete(compressedFilePath);
                    }
                }

                byte[] data;

                if (File.Exists(filePath))
                {
                    data = File.ReadAllBytes(filePath);
                }
                else
                {
                    data = GenerateData(dataType, size);

                    File.WriteAllBytes(filePath, data);
                }

                foreach (var compression in compressions)
                {
                    var compressionLevel = level ?? compression.Level;

                    var compressedFilePath = GetGeneratedCompressedFilePath(compression.Type, compressionLevel, dataTypeName, size) ?? throw new NullReferenceException("CompressedFilePath cannot be null!");

                    if (File.Exists(compressedFilePath))
                    {
                        dataGeneratorStatistics.Add(new DataGeneratorStatistic
                        {
                            CompressionType = compression.Type,
                            CompressionLevel = compressionLevel,
                            DataType = dataType,
                            RawPayloadSize = data.Length,
                            RawPayloadFilePath = filePath,
                            CompressedPayloadSize = new FileInfo(compressedFilePath).Length,
                            CompressedPayloadFilePath = compressedFilePath
                        });

                        continue;
                    }

                    var compressedData = compression.Compress(data, compressionLevel);

                    File.WriteAllBytes(compressedFilePath, compressedData);

                    dataGeneratorStatistics.Add(new DataGeneratorStatistic
                    {
                        CompressionType = compression.Type,
                        CompressionLevel = compressionLevel,
                        DataType = dataType,
                        RawPayloadSize = data.Length,
                        RawPayloadFilePath = filePath,
                        CompressedPayloadSize = new FileInfo(compressedFilePath).Length,
                        CompressedPayloadFilePath = compressedFilePath
                    });
                }
            }

            return dataGeneratorStatistics.ToArray();
        }

        public static byte[] GenerateData(DataType dataType, long size)
        {
            return dataType switch
            {
                DataType.Repetitive => GenerateRepetitive(size),
                DataType.Random => GenerateRandom(size),
                DataType.Blob => GenerateBinaryBlob(size),
                _ => throw new InvalidOperationException($"Unknown dataType: {dataType}")
            };
        }

        public static byte[] GenerateRepetitive(long length, char character = 'A')
        {
            if (length <= int.MaxValue)
            {
                return Encoding.UTF8.GetBytes(new string(character, (int)length));
            }

            var parts = new List<string>();
            var remaining = length;

            while (remaining > 0)
            {
                var chunkSize = remaining > int.MaxValue ? int.MaxValue : (int)remaining;
                parts.Add(new string(character, chunkSize));
                remaining -= chunkSize;
            }

            return Encoding.UTF8.GetBytes(string.Concat(parts));
        }

        public static byte[] GenerateRandom(long length)
        {
            var buffer = new byte[length];
            Random.Shared.NextBytes(buffer);
            return buffer;
        }

        public static byte[] GenerateBinaryBlob(long length)
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