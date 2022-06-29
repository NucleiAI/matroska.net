namespace Matroska.Net.Segments.InfoElements
{
    public enum ChapterTranslateCodec : ulong
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
