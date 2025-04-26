# Sample.Api

The Sample.Api project provides a practical example of how to use the Ogu.Compressions.* & Ogu.AspNetCore.Compressions.* libraries in a real-world ASP.NET Core Web API.
It exposes multiple endpoints through Swagger UI, demonstrating how to compress and decompress data using different algorithms.

Query param: CompressionType - Type

| CompressionType  | Value |
|:-----------------|:------|
| None             | 0     |
| Snappy           | 1     |
| Deflate          | 2     |
| Gzip             | 3     |
| Zstd             | 4     |
| Brotli           | 5     |

Query param: CompressionLevel - Level

| CompressionLevel     | Value |
|:---------------------|:------|
| Optimal              | 0     |
| Fastest              | 1     |
| NoCompression        | 2     |
| Smallest (.NET6.0+)  | 3     |


![Sample Api Swagger UI](../../docs/resources/sample-api-swagger-ui.png)

Each compression type (Brotli, Deflate, Gzip, Snappy, Zstd) has its own dedicated controller for both compression and decompression operations.

The Compressions controller provides a generic endpoint where you can specify which compression algorithm to use.

The DecompressionHandler demonstrates how HTTP responses are automatically decompressed if a proper decompression handler is configured.
