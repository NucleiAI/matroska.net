namespace Matroska.Net.Segments.TracksElements.TrackEntryElements
{
    public enum AlphaMode : ulong
    {
        /// <summary>
        /// The BlockAdditional Element with BlockAddID of "1" does not exist or **SHOULD NOT** be considered as containing such data.
        /// </summary>
        None = 0,
        /// <summary>
        /// The BlockAdditional Element with BlockAddID of "1" contains alpha channel data.
        /// </summary>
        Present = 1
    }
}
