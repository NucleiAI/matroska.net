namespace Matroska.Net.Segments.TracksElements.TrackEntryElements
{
    public enum FlagInterlaced : ulong
    {
        /// <summary>
        /// Unknown status.
        /// This value **SHOULD** be avoided.
        /// </summary>
        Undetermined = 0,
        /// <summary>
        /// Interlaced frames.
        /// </summary>
        Interlaced = 1,
        /// <summary>
        /// No interlacing.
        /// </summary>
        Progressive = 2
    }
}
