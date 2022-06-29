namespace Matroska.Net.Segments.ChaptersElements.EditionEntryElements.ChapterAtomElements.ChapProcessElements
{
    public enum ChapProcessTime : ulong
    {
        DuringWholeChapter = 0,
        BeforeStartingPlayback = 1,
        AfterPlaybackOfTheChapter = 2
    }
}
