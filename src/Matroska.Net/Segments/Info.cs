using Matroska.Net.Segments.InfoElements;
using NEbml.Core;
using System;
using System.Collections.Generic;

namespace Matroska.Net.Segments
{
    /// <summary>
    /// Contains general information about the Segment.
    /// </summary>
    public class Info
    {
        internal Info(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<Guid>? segmentFamilies = null;
            List<ChapterTranslate>? chapterTranslates = null;
            while (reader.ReadNext())
            {
                if (reader.ElementId == Elements.SegmentUID)
                {
                    var bin = new byte[16];
                    reader.ReadBinary(bin, 0, 16);
                    SegmentUID = new Guid(bin);
                }
                else if (reader.ElementId == Elements.SegmentFilename)
                {
                    SegmentFilename = reader.ReadUtf();
                }
                else if (reader.ElementId == Elements.PrevUID)
                {
                    var bin = new byte[16];
                    reader.ReadBinary(bin, 0, 16);
                    PrevUID = new Guid(bin);
                }
                else if (reader.ElementId == Elements.PrevFilename)
                {
                    PrevFilename = reader.ReadUtf();
                }
                else if (reader.ElementId == Elements.NextUID)
                {
                    var bin = new byte[16];
                    reader.ReadBinary(bin, 0, 16);
                    NextUID = new Guid(bin);
                }
                else if (reader.ElementId == Elements.NextFilename)
                {
                    NextFilename = reader.ReadUtf();
                }
                else if (reader.ElementId == Elements.SegmentFamily)
                {
                    if (segmentFamilies == null)
                    {
                        segmentFamilies = new List<Guid>();
                    }
                    var bin = new byte[16];
                    reader.ReadBinary(bin, 0, 16);
                    segmentFamilies.Add(new Guid(bin));
                }
                else if (reader.ElementId == Elements.ChapterTranslate)
                {
                    if (chapterTranslates == null)
                    {
                        chapterTranslates = new List<ChapterTranslate>();
                    }
                    chapterTranslates.Add(new ChapterTranslate(reader, version));
                }
                else if (reader.ElementId == Elements.TimestampScale)
                {
                    TimestampScale = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.Duration)
                {
                    Duration = reader.ReadFloat();
                }
                else if (reader.ElementId == Elements.DateUTC)
                {
                    DateUTC = reader.ReadDate();
                }
                else if (reader.ElementId == Elements.Title)
                {
                    Title = reader.ReadUtf();
                }
                else if (reader.ElementId == Elements.MuxingApp)
                {
                    MuxingApp = reader.ReadUtf();
                }
                else if (reader.ElementId == Elements.WritingApp)
                {
                    WritingApp = reader.ReadUtf();
                }
            }
            SegmentFamilys = segmentFamilies;
            ChapterTranslates = chapterTranslates;
            reader.LeaveContainer();
        }
        public Info(
            string muxingApp,
            string writingApp,
            ulong timestampScale = 1000000,
            Guid? segmentUID = null,
            string? segmentFilename = null,
            Guid? prevUID = null,
            string? prevFilename = null,
            Guid? nextUID = null,
            string? nextFilename = null,
            IEnumerable<Guid>? segmentFamilys = null,
            IEnumerable<ChapterTranslate>? chapterTranslates = null,
            double? duration = null,
            DateTime? dateUTC = null,
            string? title = null)
        {
            SegmentUID = segmentUID;
            SegmentFilename = segmentFilename;
            PrevUID = prevUID;
            PrevFilename = prevFilename;
            NextUID = nextUID;
            NextFilename = nextFilename;
            SegmentFamilys = segmentFamilys;
            ChapterTranslates = chapterTranslates;
            TimestampScale = timestampScale;
            Duration = duration;
            DateUTC = dateUTC;
            Title = title;
            MuxingApp = muxingApp;
            WritingApp = writingApp;
        }
        /// <summary>
        /// A randomly generated unique ID to identify the Segment amongst many others (128 bits).
        /// <usage>If the Segment is a part of a Linked Segment, then this Element is **REQUIRED**.</usage>
        /// </summary>
        public Guid? SegmentUID { get; set; }
        /// <summary>
        /// A filename corresponding to this Segment.
        /// </summary>
        public string? SegmentFilename { get; set; }
        /// <summary>
        /// A unique ID to identify the previous Segment of a Linked Segment (128 bits).
        /// <usage>If the Segment is a part of a Linked Segment that uses Hard Linking,
        /// then either the PrevUID or the NextUID Element is **REQUIRED**. If a Segment contains a PrevUID but not a NextUID,
        /// then it** MAY** be considered as the last Segment of the Linked Segment.The PrevUID **MUST NOT** be equal to the SegmentUID.</usage>
        /// </summary>
        public Guid? PrevUID { get; set; }
        /// <summary>
        /// A filename corresponding to the file of the previous Linked Segment.
        /// <usage>Provision of the previous filename is for display convenience,
        /// but PrevUID **SHOULD** be considered authoritative for identifying the previous Segment in a Linked Segment.</usage>
        /// </summary>
        public string? PrevFilename { get; set; }
        /// <summary>
        /// A unique ID to identify the next Segment of a Linked Segment (128 bits).
        /// <usage>If the Segment is a part of a Linked Segment that uses Hard Linking,
        /// then either the PrevUID or the NextUID Element is **REQUIRED**. If a Segment contains a NextUID but not a PrevUID,
        /// then it** MAY** be considered as the first Segment of the Linked Segment.The NextUID **MUST NOT** be equal to the SegmentUID.</usage>
        /// </summary>
        public Guid? NextUID { get; set; }
        /// <summary>
        /// A filename corresponding to the file of the next Linked Segment.
        /// <usage>Provision of the next filename is for display convenience,
        /// but NextUID **SHOULD** be considered authoritative for identifying the Next Segment.</usage>
        /// </summary>
        public string? NextFilename { get; set; }
        /// <summary>
        /// A randomly generated unique ID that all Segments of a Linked Segment **MUST** share (128 bits).
        /// <usage>If the Segment Info contains a `ChapterTranslate` element, this Element is **REQUIRED**.</usage>
        /// </summary>
        public IEnumerable<Guid>? SegmentFamilys { get; set; }
        /// <summary>
        /// The mapping between this `Segment` and a segment value in the given Chapter Codec.
        /// <rationale>Chapter Codec may need to address different segments, but they may not know of the way to identify such segment when stored in Matroska.
        /// This element and its child elements add a way to map the internal segments known to the Chapter Codec to the Segment IDs in Matroska.
        /// This allows remuxing a file with Chapter Codec without changing the content of the codec data, just the Segment mapping.</rationale>
        /// </summary>
        public IEnumerable<ChapterTranslate>? ChapterTranslates { get; set; }
        /// <summary>
        /// Base unit for Segment Ticks and Track Ticks, in nanoseconds. A TimestampScale value of 1.000.000 means scaled timestamps in the Segment are expressed in milliseconds; see (#timestamps) on how to interpret timestamps.
        /// </summary>
        public ulong TimestampScale { get; set; }
        /// <summary>
        /// Duration of the Segment, expressed in Segment Ticks which is based on TimestampScale; see (#timestamp-ticks).
        /// </summary>
        public double? Duration { get; set; }
        /// <summary>
        /// The date and time that the Segment was created by the muxing application or library.
        /// </summary>
        public DateTime? DateUTC { get; set; }
        /// <summary>
        /// General name of the Segment.
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Muxing application or library (example: "libmatroska-0.4.3").
        /// <usage>Include the full name of the application or library followed by the version number.</usage>
        /// </summary>
        public string MuxingApp { get; set; }
        /// <summary>
        /// Writing application (example: "mkvmerge-0.3.3").
        /// <usage>Include the full name of the application followed by the version number.</usage>
        /// </summary>
        public string WritingApp { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var info = writer.StartMasterElement(Elements.Info);
            if (SegmentUID.HasValue)
            {
                info.Write(Elements.SegmentUID, SegmentUID.Value.ToByteArray());
            }
            if (SegmentFilename != null)
            {
                info.WriteUtf(Elements.SegmentFilename, SegmentFilename);
            }
            if (PrevUID.HasValue)
            {
                info.Write(Elements.PrevUID, PrevUID.Value.ToByteArray());
            }
            if (PrevFilename != null)
            {
                info.WriteUtf(Elements.PrevFilename, PrevFilename);
            }
            if (NextUID.HasValue)
            {
                info.Write(Elements.NextUID, NextUID.Value.ToByteArray());
            }
            if(NextFilename != null)
            {
                info.WriteUtf(Elements.NextFilename, NextFilename);
            }
            if (SegmentFamilys != null)
            {
                foreach (var segmentFamily in SegmentFamilys)
                {
                    info.Write(Elements.SegmentFamily, segmentFamily.ToByteArray());
                }
            }
            if(ChapterTranslates != null)
            {
                foreach(var chapterTranslate in ChapterTranslates)
                {
                    chapterTranslate.WriteTo(info);
                }
            }
            info.Write(Elements.TimestampScale, TimestampScale);
            if (Duration.HasValue)
            {
                info.Write(Elements.Duration, Duration.Value);
            }
            if (DateUTC.HasValue)
            {
                info.Write(Elements.DateUTC, DateUTC.Value);
            }
            if(Title != null)
            {
                info.WriteUtf(Elements.Title, Title);
            }
            info.WriteUtf(Elements.MuxingApp, MuxingApp);
            info.WriteUtf(Elements.WritingApp, WritingApp);
            info.Dispose();
        }
    }
}
