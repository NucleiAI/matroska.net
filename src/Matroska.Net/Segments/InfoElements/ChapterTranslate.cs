using NEbml.Core;
using System;
using System.Collections.Generic;

namespace Matroska.Net.Segments.InfoElements
{
    public class ChapterTranslate
    {
        internal ChapterTranslate(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<ulong>? chapterTranslateEditionUids = null;
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.ChapterTranslateID)
                {
                    var arr = new byte[reader.ElementSize];
                    var length = reader.ReadBinary(arr, 0, arr.Length);
                    ChapterTranslateID = new ArraySegment<byte>(arr,0,length);
                }
                else if(reader.ElementId == Elements.ChapterTranslateCodec)
                {
                    ChapterTranslateCodec = (ChapterTranslateCodec)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ChapterTranslateEditionUID)
                {
                    if(chapterTranslateEditionUids == null)
                    {
                        chapterTranslateEditionUids = new List<ulong>();
                    }
                    chapterTranslateEditionUids.Add(reader.ReadUInt());
                }
            }
            ChapterTranslateEditionUIDs = chapterTranslateEditionUids;
            reader.LeaveContainer();
        }
        /// <summary>
        /// The mapping between this `Segment` and a segment value in the given Chapter Codec.
        /// <rationale>Chapter Codec may need to address different segments, but they may not know of the way to identify such segment when stored in Matroska.
        /// This element and its child elements add a way to map the internal segments known to the Chapter Codec to the Segment IDs in Matroska.
        /// This allows remuxing a file with Chapter Codec without changing the content of the codec data, just the Segment mapping.</rationale>
        /// </summary>
        public ChapterTranslate(ArraySegment<byte> chapterTranslateID, ChapterTranslateCodec chapterTranslateCodec, IEnumerable<ulong>? chapterTranslateEditionUIDs = null)
        {
            ChapterTranslateID = chapterTranslateID;
            ChapterTranslateCodec = chapterTranslateCodec;
            ChapterTranslateEditionUIDs = chapterTranslateEditionUIDs;
        }
        /// <summary>
        /// The binary value used to represent this Segment in the chapter codec data.
        /// The format depends on the ChapProcessCodecID used; see(#chapprocesscodecid-element).
        /// </summary>
        public ArraySegment<byte> ChapterTranslateID { get; set; }
        /// <summary>
        /// This `ChapterTranslate` applies to this chapter codec of the given chapter edition(s); see (#chapprocesscodecid-element).
        /// </summary>
        public ChapterTranslateCodec ChapterTranslateCodec { get; set; }
        /// <summary>
        /// Specify a chapter edition UID on which this `ChapterTranslate` applies.
        /// <usage>When no `ChapterTranslateEditionUID` is specified in the `ChapterTranslate`, the `ChapterTranslate` applied to all chapter editions found in the Segment using the given `ChapterTranslateCodec`.</usage>
        /// </summary>
        public IEnumerable<ulong>? ChapterTranslateEditionUIDs { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var chapterTranslate = writer.StartMasterElement(Elements.ChapterTranslate);
            writer.Write(Elements.ChapterTranslateID, ChapterTranslateID.Array, ChapterTranslateID.Offset, ChapterTranslateID.Count);
            writer.Write(Elements.ChapterTranslateCodec, (ulong)ChapterTranslateCodec);
            if(ChapterTranslateEditionUIDs != null)
            {
                foreach(var chapterTranslateEditionUID in ChapterTranslateEditionUIDs)
                {
                    writer.Write(Elements.ChapterTranslateEditionUID, chapterTranslateEditionUID);
                }
            }
            chapterTranslate.Dispose();
        }
    }
}
