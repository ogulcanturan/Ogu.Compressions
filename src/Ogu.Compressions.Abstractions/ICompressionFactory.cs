namespace Ogu.Compressions.Abstractions
{
    public interface ICompressionFactory
    {
        /// <summary>
        /// Returns specified type compression or null if there is no such service
        /// </summary>
        /// <param name="compressionType"></param>
        /// <returns></returns>
        ICompression Get(CompressionType compressionType);

        /// <summary>
        /// Returns specified type compression or none compression if not matches or null if there is no such service
        /// </summary>
        /// <param name="encodingName"></param>
        /// <returns></returns>
        ICompression Get(string encodingName);
    }
}