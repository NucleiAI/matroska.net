namespace Matroska.Net.Segments.TracksElements.TrackEntryElements
{
    public enum FieldOrder : ulong
    {
        /// <summary>
        /// Interlaced frames.
        /// <usage>This value **SHOULD** be avoided, setting FlagInterlaced to 2 is sufficient.</usage>
        /// </summary>
        Progressive = 0,
        /// <summary>
        /// Top field displayed first. Top field stored first.
        /// </summary>
        Tff = 1,
        /// <summary>
        /// Unknown field order.
        /// <usage>This value **SHOULD** be avoided.</usage>
        /// </summary>
        Undetermined = 2,
        /// <summary>
        /// Bottom field displayed first. Bottom field stored first.
        /// </summary>
        Bff = 6,
        /// <summary>
        /// Top field displayed first. Fields are interleaved in storage
        /// with the top line of the top field stored first.
        /// </summary>
        SwappedBff = 9,
        /// <summary>
        /// Bottom field displayed first. Fields are interleaved in storage
        /// with the top line of the top field stored first.
        /// </summary>
        SwappedTff = 14
    }
}
