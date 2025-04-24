using System;

namespace Ogu.Compressions.Abstractions
{
    public class CompressorNotAvailableException : Exception
    {
        private const string MessageFormat = "The requested compressor is not available: {0}";

        public CompressorNotAvailableException(CompressionType type) : this($"{type}")
        {
        }

        public CompressorNotAvailableException(string encodingName)
            : base(string.Format(MessageFormat, encodingName))
        {
            Requested = encodingName;
        }

        /// <summary>
        /// The requested compressor encoding name or <see cref="CompressionType" />.
        /// </summary>
        public string Requested { get; }
    }
}