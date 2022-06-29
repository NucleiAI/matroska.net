using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments.ChaptersElements.EditionEntryElements.ChapterAtomElements
{
    /// <summary>
    /// List of tracks on which the chapter applies. If this Element is not present, all tracks apply
    /// </summary>
    public class ChapterTrack
    {
        internal ChapterTrack(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<ulong> chapterTrackUIDs = new List<ulong>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.ChapterTrackUID)
                {
                    chapterTrackUIDs.Add(reader.ReadUInt());
                }
            }
            ChapterTrackUID = chapterTrackUIDs;
            reader.LeaveContainer();
        }
        public ChapterTrack(
            IEnumerable<ulong> chapterTrackUID)
        {
            ChapterTrackUID = chapterTrackUID;
        }
        /// <summary>
        /// UID of the Track to apply this chapter to.
        /// In the absence of a control track, choosing this chapter will select the listed Tracks and deselect unlisted tracks.
        /// Absence of this Element indicates that the Chapter** SHOULD** be applied to any currently used Tracks.
        /// </summary>
        public IEnumerable<ulong> ChapterTrackUID { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var chapterTrack = writer.StartMasterElement(Elements.ChapterTrack);
            foreach(var chapterTrackUID in ChapterTrackUID)
            {
                chapterTrack.Write(Elements.ChapterTrackUID, chapterTrackUID);
            }
            chapterTrack.Dispose();
        }
    }
}
