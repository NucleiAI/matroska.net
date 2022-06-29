namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.ContentEncodingsElements.ContentEncodingElements
{
    public enum ContentCompAlgo : ulong
    {
        /// <summary>
        /// zlib compression [@!RFC1950].
        /// </summary>
        ZLib = 0,
        /// <summary>
        /// bzip2 compression [@!BZIP2], **SHOULD NOT** be used; see usage notes.
        /// </summary>
        BzLib = 1,
        /// <summary>
        /// Lempel-Ziv-Oberhumer compression [@!LZO], **SHOULD NOT** be used; see usage notes.
        /// </summary>
        LZO1x = 2,
        /// <summary>
        /// Octets in `ContentCompSettings` ((#contentcompsettings-element)) have been stripped from each frame.
        /// </summary>
        HeaderStripping = 3
    }
}
