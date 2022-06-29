using Matroska.Net.Segments.CuesElements.CuePointElements.CueTrackPositionsElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments.CuesElements.CuePointElements
{
    /// <summary>
    /// Contain positions for different tracks corresponding to the timestamp.
    /// </summary>
    public class CueTrackPositions
    {
        internal CueTrackPositions(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<CueReference>? references = null;
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.CueTrack)
                {
                    CueTrack = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.CueClusterPosition)
                {
                    CueClusterPosition = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.CueRelativePosition)
                {
                    CueRelativePosition = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.CueDuration)
                {
                    CueDuration = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.CueBlockNumber)
                {
                    CueBlockNumber = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.CueCodecState)
                {
                    CueCodecState = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.CueReference)
                {
                    if(references == null)
                    {
                        references = new List<CueReference>();
                    }
                    references.Add(new CueReference(reader, version));
                }
            }
            CueReferences = references;
            reader.LeaveContainer();
        }
        public CueTrackPositions(
            ulong cueTrack,
            ulong cueClusterPosition,
            ulong? cueRelativePosition = null,
            ulong? cueDuration = null,
            ulong? cueBlockNumber = null,
            ulong cueCodecState = 0,
            IEnumerable<CueReference>? cueReferences = null)
        {
            CueTrack = cueTrack;
            CueClusterPosition = cueClusterPosition;
            CueRelativePosition = cueRelativePosition;
            CueDuration = cueDuration;
            CueBlockNumber = cueBlockNumber;
            CueCodecState = cueCodecState;
            CueReferences = cueReferences;
        }
        /// <summary>
        /// The track for which a position is given.
        /// </summary>
        public ulong CueTrack { get; set; }
        /// <summary>
        /// The Segment Position of the Cluster containing the associated Block.
        /// </summary>
        public ulong CueClusterPosition { get; set; }
        /// <summary>
        /// The relative position inside the Cluster of the referenced SimpleBlock or BlockGroup
        /// with 0 being the first possible position for an Element inside that Cluster.
        /// </summary>
        public ulong? CueRelativePosition { get; set; }
        /// <summary>
        /// The duration of the block, expressed in Segment Ticks which is based on TimestampScale; see (#timestamp-ticks).
        /// If missing, the track's DefaultDuration does not apply and no duration information is available in terms of the cues.
        /// </summary>
        public ulong? CueDuration { get; set; }
        /// <summary>
        /// Number of the Block in the specified Cluster.
        /// </summary>
        public ulong? CueBlockNumber { get; set; }
        /// <summary>
        /// The Segment Position of the Codec State corresponding to this Cue Element.
        /// 0 means that the data is taken from the initial Track Entry.
        /// </summary>
        public ulong CueCodecState { get; set; }
        /// <summary>
        /// The Clusters containing the referenced Blocks.
        /// </summary>
        public IEnumerable<CueReference>? CueReferences { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var cueTrackPositions = writer.StartMasterElement(Elements.CueTrackPositions);
            cueTrackPositions.Write(Elements.CueTrack, CueTrack);
            cueTrackPositions.Write(Elements.CueClusterPosition, CueClusterPosition);
            if (CueRelativePosition.HasValue)
            {
                cueTrackPositions.Write(Elements.CueRelativePosition, CueRelativePosition.Value);
            }
            if(CueDuration.HasValue)
            {
                cueTrackPositions.Write(Elements.CueDuration, CueDuration.Value);
            }
            if (CueBlockNumber.HasValue)
            {
                cueTrackPositions.Write(Elements.CueBlockNumber, CueBlockNumber.Value);
            }
            cueTrackPositions.Write(Elements.CueCodecState, CueCodecState);
            if(CueReferences != null)
            {
                foreach (var reference in CueReferences)
                {
                    reference.WriteTo(cueTrackPositions);
                }
            }
            cueTrackPositions.Dispose();
        }
    }
}
