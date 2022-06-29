namespace Matroska.Net.Segments.TracksElements.TrackEntryElements
{
    public enum StereoMode : ulong
    {
        Mono = 0,
        SideBySideLeftEyeFirst = 1,
        TopBottomRightEyeIsFirst = 2,
        TopBottomLeftEyeIsFirst = 3,
        CheckboardRightEyeIsFirst = 4,
        CheckboardLeftEyeIsFirst = 5,
        RowInterleavedRightEyeIsFirst = 6,
        RowInterleavedLeftEyeIsFirst = 7,
        ColumnInterleavedRightEyeIsFirst = 8,
        ColumnInterleavedLeftEyeIsFirst = 9,
        AnaglyphCyanRed = 10,
        SideBySideRightEyeFirst = 11,
        AnaglyphGreenMagenta = 12,
        BothEyesLacedInOneBlockLeftEyeIsFirst = 13,
        BothEyesLacedInOneBlockRightEyeIsFirst = 14
    }
}
