namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Interface for resolving compression types based on encoding names.
    /// </summary>
    public interface ICompressionTypeResolver
    {
        /// <summary>
        /// Attempts to resolve the corresponding <see cref="CompressionType"/> for a given encoding name.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="type">The resolved <see cref="CompressionType"/>, if successful.</param>
        /// <returns><c>true</c> if resolved; otherwise, <c>false</c>.</returns>
        bool TryResolve(string encodingName, out CompressionType type);
    }
}