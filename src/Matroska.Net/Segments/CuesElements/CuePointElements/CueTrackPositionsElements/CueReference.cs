using NEbml.Core;
using System;

namespace Matroska.Net.Segments.CuesElements.CuePointElements.CueTrackPositionsElements
{
    /// <summary>
    /// The Clusters containing the referenced Blocks.
    /// </summary>
    public class CueReference
    {
        internal CueReference(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.CueRefTime)
                {
                    CueRefTime = reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.CueRefCluster)
                {
                    CueRefCluster = reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.CueRefNumber)
                {
                    CueRefNumber = reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.CueRefCodecState)
                {
                    CueRefCodecState = reader.ReadUInt();
                }
            }
            reader.LeaveContainer();
        }
        public CueReference(
            ulong cueRefTime)
        {
            CueRefTime = cueRefTime;
        }
        /// <summary>
        /// Timestamp of the referenced Block, expressed in Matroska Ticks -- ie in nanoseconds; see (#timestamp-ticks).
        /// </summary>
        public ulong CueRefTime { get; set; }
        /// <summary>
        /// The Segment Position of the Cluster containing the referenced Block.
        /// </summary>
        [Obsolete("CueRefCluster is since version 1 of matroska obsolete and should not be used")]
        public ulong? CueRefCluster { get; set; }
        /// <summary>
        /// Number of the referenced Block of Track X in the specified Cluster.
        /// </summary>
        [Obsolete("CueRefNumber is since version 1 of matroska obsolete and should not be used")]
        public ulong? CueRefNumber { get; set; }
        /// <summary>
        /// The Segment Position of the Codec State corresponding to this referenced Element.
        /// 0 means that the data is taken from the initial Track Entry.
        /// </summary>
        [Obsolete("CueRefCodecState is since version 1 of matroska obsolete and should not be used")]
        public ulong? CueRefCodecState { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var cueReference = writer.StartMasterElement(Elements.CueReference);
            cueReference.Write(Elements.CueRefTime, CueRefTime);
            cueReference.Dispose();
        }
    }
}
