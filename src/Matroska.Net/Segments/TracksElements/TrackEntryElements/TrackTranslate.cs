using NEbml.Core;
using System;
using System.Collections.Generic;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements
{
    /// <summary>
    /// The mapping between this `TrackEntry` and a track value in the given Chapter Codec.
    /// <rationale>Chapter Codec may need to address content in specific track, but they may not know of the way to identify tracks in Matroska.
    /// This element and its child elements add a way to map the internal tracks known to the Chapter Codec to the track IDs in Matroska.
    /// This allows remuxing a file with Chapter Codec without changing the content of the codec data, just the track mapping.</rationale>
    /// </summary>
    public class TrackTranslate
    {
        internal TrackTranslate(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<ulong>? trackTranslateEditionUIDs = null;
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.TrackTranslateTrackID)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    TrackTranslateTrackID = new ArraySegment<byte>(data, 0, read);
                }
                else if(reader.ElementId == Elements.TrackTranslateCodec)
                {
                    TrackTranslateCodec = (TrackTranslateCodec)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.TrackTranslateEditionUID)
                {
                    if (trackTranslateEditionUIDs == null)
                    {
                        trackTranslateEditionUIDs = new List<ulong>();
                    }
                    trackTranslateEditionUIDs.Add(reader.ReadUInt());
                }
            }
            TrackTranslateEditionUID = trackTranslateEditionUIDs;
            reader.LeaveContainer();
        }
        public TrackTranslate(
            ArraySegment<byte> trackTranslateTrackID,
            TrackTranslateCodec trackTranslateCodec,
            IEnumerable<ulong>? trackTranslateEditionUID = null)
        {
            TrackTranslateTrackID = trackTranslateTrackID;
            TrackTranslateCodec = trackTranslateCodec;
            TrackTranslateEditionUID = trackTranslateEditionUID;
        }
        /// <summary>
        /// The binary value used to represent this `TrackEntry` in the chapter codec data.
        /// The format depends on the `ChapProcessCodecID` used; see(#chapprocesscodecid-element).
        /// </summary>
        public ArraySegment<byte> TrackTranslateTrackID { get; set; }
        /// <summary>
        /// This `TrackTranslate` applies to this chapter codec of the given chapter edition(s); see (#chapprocesscodecid-element).
        /// </summary>
        public TrackTranslateCodec TrackTranslateCodec { get; set; }
        /// <summary>
        /// Specify a chapter edition UID on which this `TrackTranslate` applies.
        /// <usage>When no `TrackTranslateEditionUID` is specified in the `TrackTranslate`, the `TrackTranslate` applies to all chapter editions found in the Segment using the given `TrackTranslateCodec`.</usage>
        /// </summary>
        public IEnumerable<ulong>? TrackTranslateEditionUID { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var trackTranslate = writer.StartMasterElement(Elements.TrackTranslate);
            trackTranslate.Write(Elements.TrackTranslateTrackID, TrackTranslateTrackID.Array, TrackTranslateTrackID.Offset, TrackTranslateTrackID.Count);
            trackTranslate.Write(Elements.TrackTranslateCodec, (ulong)TrackTranslateCodec);
            if(TrackTranslateEditionUID != null)
            {
                foreach(var trackTranslateEdition in TrackTranslateEditionUID)
                {
                    trackTranslate.Write(Elements.TrackTranslateEditionUID, trackTranslateEdition);
                }
            }
            trackTranslate.Dispose();
        }
    }
}
