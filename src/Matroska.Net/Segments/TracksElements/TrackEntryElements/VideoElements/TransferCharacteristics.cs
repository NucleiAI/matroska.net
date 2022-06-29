namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.VideoElements
{
    public enum TransferCharacteristics : ulong
    {
        Reserved = 0,
        ITURBT709 = 1,
        Unspecified = 2,
        Reserved1 = 3,
        Gamma22CurveBT470M = 4,
        Gamma28CurveBT470BG = 5,
        SMPTE170M = 6,
        SMPTE240M = 7,
        Linear = 8,
        Log = 9,
        LogSqrt = 10,
        IEC6196624 = 11,
        ITURBT1361ExtendedColourGamut = 12,
        IEC6196621 =13,
        ITURBT202010Bit = 14,
        ITURBT202012Bit = 15,
        ITURBT2100PerceptualQuantization = 16,
        SMPTEST4281 = 17,
        ARIBSTDB67 = 18
    }
}
