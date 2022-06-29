using NEbml.Core;
using System;

namespace Matroska.Net.Segments.ClusterElements.BlockGroupElements
{
    /// <summary>
    /// Contains information about the last reference frame. See [@?DivXTrickTrack].
    /// </summary>
    [Obsolete("ReferenceFrame is since version 1 of matroska obsolete and should not be used")]
    public class ReferenceFrame
    {
        internal ReferenceFrame(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(version == 0 && reader.ElementId == Elements.ReferenceOffset)
                {
                    ReferenceOffset = reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.ReferenceTimestamp)
                {
                    ReferenceTimestamp = reader.ReadUInt();
                }
            }
            reader.LeaveContainer();
        }
        public ReferenceFrame(ulong referenceOffset, ulong referenceTimestamp)
        {
            ReferenceOffset = referenceOffset;
            ReferenceTimestamp = referenceTimestamp;
        }
        /// <summary>
        /// The relative offset, in bytes, from the previous BlockGroup element for this Smooth FF/RW video track to the containing BlockGroup element. See [@?DivXTrickTrack].
        /// </summary>
        [Obsolete("ReferenceFrame is since version 1 of matroska obsolete and should not be used")]
        public ulong ReferenceOffset { get; set; }
        /// <summary>
        /// The timestamp of the BlockGroup pointed to by ReferenceOffset, expressed in Track Ticks; see (#timestamp-ticks). See [@?DivXTrickTrack].
        /// </summary>
        [Obsolete("ReferenceFrame is since version 1 of matroska obsolete and should not be used")]
        public ulong ReferenceTimestamp { get; set; }
    }
}
