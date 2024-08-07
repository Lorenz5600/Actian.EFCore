namespace Actian.EFCore
{
    /// <summary>
    ///     Indicates type of data compression used on a index.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public enum DataCompressionType
    {
        /// <summary>
        ///     Index is not compressed.
        /// </summary>
        None,

        /// <summary>
        ///     Index is compressed by using row compression.
        /// </summary>
        Row,

        /// <summary>
        ///     Index is compressed by using page compression.
        /// </summary>
        Page
    }
}
