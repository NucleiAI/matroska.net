using NEbml.Core;
using System;

namespace Matroska.Net.Segments.ClusterElements.BlockGroupElements.SlicesElements
{
    /// <summary>
    /// Contains extra time information about the data contained in the Block.
    /// Being able to interpret this Element is not** REQUIRED** for playback.
    /// </summary>
    [Obsolete("TimeSlice is since version 1 of matroska obsolete and should not be used")]
    public class TimeSlice
    {
        internal TimeSlice(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(version == 0 && reader.ElementId == Elements.LaceNumber)
                {
                    LaceNumber = reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.FrameNumber)
                {
                    FrameNumber = reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.BlockAdditionID)
                {
                    BlockAdditionID = reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.Delay)
                {
                    Delay = reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.SliceDuration)
                {
                    SliceDuration = reader.ReadUInt();
                }
            }
            reader.LeaveContainer();
        }
        public TimeSlice(
            ulong? laceNumber = null,
            ulong? frameNumber = 0,
            ulong? blockAdditionID = 0,
            ulong? delay = 0,
            ulong? sliceDuration = 0)
        {
            LaceNumber = laceNumber;
            FrameNumber = frameNumber;
            BlockAdditionID = blockAdditionID;
            Delay = delay; 
            SliceDuration = sliceDuration;
        }
        /// <summary>
        /// The reverse number of the frame in the lace (0 is the last frame, 1 is the next to last, etc).
        /// Being able to interpret this Element is not** REQUIRED** for playback.
        /// </summary>
        [Obsolete("TimeSlice is since version 1 of matroska obsolete and should not be used")]
        public ulong? LaceNumber { get; set; }
        /// <summary>
        /// The number of the frame to generate from this lace with this delay
        /// (allow you to generate many frames from the same Block/Frame).
        /// </summary>
        [Obsolete("TimeSlice is since version 1 of matroska obsolete and should not be used")]
        public ulong? FrameNumber { get; set; }
        /// <summary>
        /// The ID of the BlockAdditional Element (0 is the main Block).
        /// </summary>
        [Obsolete("TimeSlice is since version 1 of matroska obsolete and should not be used")]
        public ulong? BlockAdditionID { get; set; }
        /// <summary>
        /// The delay to apply to the Element, expressed in Track Ticks; see (#timestamp-ticks).
        /// </summary>
        [Obsolete("TimeSlice is since version 1 of matroska obsolete and should not be used")]
        public ulong? Delay { get; set; }
        /// <summary>
        /// The duration to apply to the Element, expressed in Track Ticks; see (#timestamp-ticks).
        /// </summary>
        [Obsolete("TimeSlice is since version 1 of matroska obsolete and should not be used")]
        public ulong? SliceDuration { get; set; }
    }
}
