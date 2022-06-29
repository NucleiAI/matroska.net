namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.VideoElements
{
    public enum MatrixCoefficients : ulong
    {
        Identity = 0,
        ITURBT709 = 1,
        Unspecified = 2,
        Reserved = 3,
        USFCC73682 = 4,
        ITURBT470BG = 5,
        SMPTE170M = 6,
        SMPTE240M = 7,
        YCoCg = 8,
        BT2020NonConstantLuminance = 9,
        BT2020ConstantLuminance = 10,
        SMPTEST2085 = 11,
        ChromaDerivedNonConstantLuminance = 12,
        ChromaDerivedConstantLuminance = 13,
        ITURBT21000
    }
}
