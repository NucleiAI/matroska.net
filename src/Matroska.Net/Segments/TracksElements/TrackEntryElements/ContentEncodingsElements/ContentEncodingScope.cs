namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.ContentEncodingsElements
{
    public enum ContentEncodingScope : ulong
    {
        /// <summary>
        /// All frame contents, excluding lacing data.
        /// </summary>
        Block = 1,
        /// <summary>
        /// The track's private data.
        /// </summary>
        Private = 2,
        /// <summary>
        /// The next ContentEncoding (next `ContentEncodingOrder`. Either the data inside `ContentCompression` and/or `ContentEncryption`).
        /// <usage>This value **SHOULD NOT** be used.</usage>
        /// </summary>
        Next = 4
    }
}
