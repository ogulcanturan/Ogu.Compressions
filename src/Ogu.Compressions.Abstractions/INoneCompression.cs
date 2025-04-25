namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Interface used internally to indicate no compression; does not correspond to a real compression algorithm.
    /// </summary>
    public interface INoneCompression : ICompression { }
}