using Matroska.Net.Segments.ChaptersElements.EditionEntryElements.ChapterAtomElements;
using NEbml.Core;
using System;
using System.Collections.Generic;

namespace Matroska.Net.Segments.ChaptersElements.EditionEntryElements
{
    /// <summary>
    /// Contains the atom information to use as the chapter atom (apply to all tracks).
    /// </summary>
    public class ChapterAtom
    {
        internal ChapterAtom(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<ChapterDisplay>? chapterDisplays = null;
            List<ChapProcess>? chapProcesses = null;
            while (reader.ReadNext())
            {
                if (reader.ElementId == Elements.ChapterUID)
                {
                    ChapterUID = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.ChapterStringUID)
                {
                    ChapterStringUID = reader.ReadUtf();
                }
                else if (reader.ElementId == Elements.ChapterTimeStart)
                {
                    ChapterTimeStart = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.ChapterTimeEnd)
                {
                    ChapterTimeEnd = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.ChapterFlagHidden)
                {
                    ChapterFlagHidden = reader.ReadUInt() == 1;
                }
                else if (reader.ElementId == Elements.ChapterFlagEnabled)
                {
                    ChapterFlagEnabled = reader.ReadUInt() != 0;
                }
                else if (reader.ElementId == Elements.ChapterSegmentUID)
                {
                    var data = new byte[16];
                    reader.ReadBinary(data, 0, 16);
                    ChapterSegmentUID = new Guid(data);
                }
                else if (reader.ElementId == Elements.ChapterSegmentEditionUID)
                {
                    ChapterSegmentEditionUID = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.ChapterPhysicalEquiv)
                {
                    ChapterPhysicalEquiv = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.ChapterTrack)
                {
                    ChapterTrack = new ChapterTrack(reader, version);
                }
                else if (reader.ElementId == Elements.ChapterDisplay)
                {
                    if (chapterDisplays == null)
                    {
                        chapterDisplays = new List<ChapterDisplay>();
                    }
                    chapterDisplays.Add(new ChapterDisplay(reader, version));
                }
                else if (reader.ElementId == Elements.ChapProcess)
                {
                    if (chapProcesses == null)
                    {
                        chapProcesses = new List<ChapProcess>();
                    }
                    chapProcesses.Add(new ChapProcess(reader, version));
                }
            }
            reader.LeaveContainer();
        }
        public ChapterAtom(
            ulong chapterUID,
            ulong chapterTimeStart,
            string? chapterStringUID = null,
            ulong? chapterTimeEnd = null,
            bool chapterFlagHidden = false,
            bool chapterFlagEnabled = true,
            Guid? chapterSegmentUID = null,
            ulong? chapterSegmentEditionUID = null,
            ulong? chapterPhysicalEquiv = null,
            ChapterTrack? chapterTrack = null,
            IEnumerable<ChapterDisplay>? chapterDisplays = null,
            IEnumerable<ChapProcess>? chapProcesses = null)
        {
            ChapterUID = chapterUID;
            ChapterStringUID = chapterStringUID;
            ChapterTimeStart = chapterTimeStart;
            ChapterTimeEnd = chapterTimeEnd;
            ChapterFlagHidden = chapterFlagHidden;
            ChapterFlagEnabled = chapterFlagEnabled;
            ChapterSegmentUID = chapterSegmentUID;
            ChapterSegmentEditionUID = chapterSegmentEditionUID;
            ChapterPhysicalEquiv = chapterPhysicalEquiv;
            ChapterTrack = chapterTrack;
            ChapterDisplays = chapterDisplays;
            ChapProcesses = chapProcesses;
        }
        /// <summary>
        /// A unique ID to identify the Chapter.
        /// </summary>
        public ulong ChapterUID { get; set; }
        /// <summary>
        /// A unique string ID to identify the Chapter.
        /// Use for WebVTT cue identifier storage[@!WebVTT].
        /// </summary>
        public string? ChapterStringUID { get; set; }
        /// <summary>
        /// Timestamp of the start of Chapter, expressed in Matroska Ticks -- ie in nanoseconds; see (#timestamp-ticks).
        /// </summary>
        public ulong ChapterTimeStart { get; set; }
        /// <summary>
        /// Timestamp of the end of Chapter timestamp excluded, expressed in Matroska Ticks -- ie in nanoseconds; see (#timestamp-ticks).
        /// The value **MUST** be greater than or equal to the `ChapterTimeStart` of the same `ChapterAtom`.</documentation>
        /// <usage>The `ChapterTimeEnd` timestamp value being excluded, it **MUST** take in account the duration of
        /// the last frame it includes, especially for the `ChapterAtom` using the last frames of the `Segment`.</usage>
        /// <implementation>ChapterTimeEnd **MUST** be set (minOccurs=1) if the Edition is an ordered edition; see (#editionflagordered), unless it's a `Parent Chapter`; see (#nested-chapters)</implementation>
        /// </summary>
        public ulong? ChapterTimeEnd { get; set; }
        /// <summary>
        /// Set to 1 if a chapter is hidden. Hidden chapters **SHOULD NOT** be available to the user interface
        /// (but still to Control Tracks; see(#chapterflaghidden) on Chapter flags).
        /// </summary>
        public bool ChapterFlagHidden { get; set; }
        /// <summary>
        /// Set to 1 if the chapter is enabled. It can be enabled/disabled by a Control Track.
        /// When disabled, the movie** SHOULD** skip all the content between the TimeStart and TimeEnd of this chapter; see(#chapter-flags) on Chapter flags.
        /// </summary>
        public bool ChapterFlagEnabled { get; set; }
        /// <summary>
        /// The SegmentUID of another Segment to play during this chapter.
        /// <usage>The value **MUST NOT** be the `SegmentUID` value of the `Segment` it belongs to.</usage>
        /// <implementation>ChapterSegmentUID **MUST** be set (minOccurs=1) if ChapterSegmentEditionUID is used; see (#medium-linking) on medium-linking Segments.</implementation>
        /// </summary>
        public Guid? ChapterSegmentUID { get; set; }
        /// <summary>
        /// The EditionUID to play from the Segment linked in ChapterSegmentUID.
        /// If ChapterSegmentEditionUID is undeclared, then no Edition of the linked Segment is used; see(#medium-linking) on medium-linking Segments.
        /// </summary>
        public ulong? ChapterSegmentEditionUID { get; set; }
        /// <summary>
        /// Specify the physical equivalent of this ChapterAtom like "DVD" (60) or "SIDE" (50);
        /// see(#physical-types) for a complete list of values.
        /// </summary>
        public ulong? ChapterPhysicalEquiv { get; set; }
        /// <summary>
        /// List of tracks on which the chapter applies. If this Element is not present, all tracks apply
        /// </summary>
        public ChapterTrack? ChapterTrack { get; set; }
        /// <summary>
        /// Contains all possible strings to use for the chapter display.
        /// </summary>
        public IEnumerable<ChapterDisplay>? ChapterDisplays { get; set; }
        /// <summary>
        /// Contains all the commands associated to the Atom.
        /// </summary>
        public IEnumerable<ChapProcess>? ChapProcesses { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var chapterAtom = writer.StartMasterElement(Elements.ChapterAtom);
            chapterAtom.Write(Elements.ChapterUID, ChapterUID);
            if (ChapterStringUID != null)
            {
                chapterAtom.WriteUtf(Elements.ChapterStringUID, ChapterStringUID);
            }
            chapterAtom.Write(Elements.ChapterTimeStart, ChapterTimeStart);
            if (ChapterTimeEnd.HasValue)
            {
                chapterAtom.Write(Elements.ChapterTimeEnd, ChapterTimeEnd.Value);
            }
            chapterAtom.Write(Elements.ChapterFlagHidden, (ulong)(ChapterFlagHidden ? 1 : 0));
            chapterAtom.Write(Elements.ChapterFlagEnabled, (ulong)(ChapterFlagEnabled ? 1 : 0));
            if (ChapterSegmentUID.HasValue)
            {
                chapterAtom.Write(Elements.ChapterSegmentUID, ChapterSegmentUID.Value.ToByteArray());
            }
            if (ChapterSegmentEditionUID.HasValue)
            {
                chapterAtom.Write(Elements.ChapterSegmentEditionUID, ChapterSegmentEditionUID.Value);
            }
            if (ChapterPhysicalEquiv.HasValue)
            {
                chapterAtom.Write(Elements.ChapterPhysicalEquiv, ChapterPhysicalEquiv.Value);
            }
            if(ChapterTrack != null)
            {
                ChapterTrack.WriteTo(chapterAtom);
            }
            if(ChapterDisplays != null)
            {
                foreach(var chapterDisplay in ChapterDisplays)
                {
                    chapterDisplay.WriteTo(chapterAtom);
                }
            }
            if(ChapProcesses != null)
            {
                foreach(var chapProcess in ChapProcesses)
                {
                    chapProcess.WriteTo(chapterAtom);
                }
            }
            chapterAtom.Dispose();
        }
    }
}
