namespace Matroska.Net.Segments.TracksElements.TrackEntryElements
{
    public enum TrackTranslateCodec : long
    {
        /// <summary>
        /// Chapter commands using the Matroska Script codec.
        /// </summary>
        MatroskaScript = 0,
        /// <summary>
        /// Chapter commands using the DVD-like codec.
        /// </summary>
        DVDMenu = 1
    }
}
