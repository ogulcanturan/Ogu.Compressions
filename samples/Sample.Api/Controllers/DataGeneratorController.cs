using Microsoft.AspNetCore.Mvc;
using Ogu.Compressions.Abstractions;
using Sample.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Sample.Api.Utility;
using DataType = Sample.Api.Models.DataType;

namespace Sample.Api.Controllers
{
    [Route("api/[controller]")]
    public class DataGeneratorController : ControllerBase
    {
        private readonly ICompression[] _compressions;

        public DataGeneratorController(IEnumerable<ICompression> compressions)
        {
            _compressions = compressions.ToArray();
        }

        [HttpGet("get-data")]
        public async Task<IActionResult> GetData([Required] DataSize size)
        {
            var dataTypes = Enum.GetValues<DataType>();

            var dataTypeToData = new Dictionary<DataType, object>();

            foreach (var dataType in dataTypes)
            {
                var filePath = DataGenerator.GetGeneratedFilePath($"{dataType}", (long)size);

                if (!System.IO.File.Exists(filePath))
                {
                    continue;
                }

                if (dataType == DataType.Repetitive)
                {
                    dataTypeToData[dataType] = await System.IO.File.ReadAllTextAsync(filePath);
                }

                dataTypeToData[dataType] = Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync(filePath));
            }

            return dataTypeToData.Count == 0
                ? NotFound()
                : Ok(dataTypeToData);
        }

        [HttpPost("generate-data")]
        public IActionResult GenerateData([Required] DataSize size, CompressionLevel? level = null, bool overwrite = false)
        {
            var statistics = DataGenerator.EnsureDataGenerated(_compressions, (long)size, level, overwrite);

            var groupedStatistics = statistics
                .GroupBy(g => g.DataType)
                .ToDictionary(
                    dataTypeGroup => dataTypeGroup.Key,
                    dataTypeGroup => dataTypeGroup
                        .GroupBy(g => g.RawPayloadSize)
                        .ToDictionary(
                            payloadSizeGroup => payloadSizeGroup.Key,
                            payloadSizeGroup => payloadSizeGroup
                                .GroupBy(g => g.CompressionLevel)
                                .ToDictionary(
                                    compressionLevelGroup => compressionLevelGroup.Key,
                                    compressionLevelGroup => compressionLevelGroup
                                        .ToDictionary(
                                            g => g.CompressionType,
                                            g => g.CompressedPayloadSize
                                        )
                                )
                        )
                );

            return Ok(groupedStatistics);
        }
    }
}