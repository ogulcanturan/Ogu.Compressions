using System;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Represents an exception that is thrown when a requested compression algorithm is not available.
    /// </summary>
    public class CompressionNotAvailableException : Exception
    {
        private const string MessageFormat = "The requested compression is not available: {0}";

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionNotAvailableException"/> class with a specified error message.
        /// </summary>
        /// <param name="type">The requested compression type.</param>
        public CompressionNotAvailableException(CompressionType type) : this($"{type}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionNotAvailableException"/> class with a specified error message.
        /// </summary>
        /// <param name="encodingName">The requested encoding name.</param>
        public CompressionNotAvailableException(string encodingName)
            : base(string.Format(MessageFormat, encodingName))
        {
            Requested = encodingName;
        }

        /// <summary>
        /// The requested compression encoding name or <see cref="CompressionType" />.
        /// </summary>
        public string Requested { get; }
    }
}