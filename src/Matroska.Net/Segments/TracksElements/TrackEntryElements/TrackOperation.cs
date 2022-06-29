using Matroska.Net.Segments.TracksElements.TrackEntryElements.TrackOperationElements;
using NEbml.Core;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements
{
    /// <summary>
    /// Operation that needs to be applied on tracks to create this virtual track.
    /// For more details look at(#track-operation).
    /// </summary>
    public class TrackOperation
    {
        internal TrackOperation(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.TrackCombinePlanes)
                {
                    TrackCombinePlanes = new TrackCombinePlanes(reader, version);
                }
                else if(reader.ElementId == Elements.TrackJoinBlocks)
                {
                    TrackJoinBlocks = new TrackJoinBlocks(reader, version);
                }
            }
            reader.LeaveContainer();
        }
        public TrackOperation(
            TrackCombinePlanes? trackCombinePlanes = null,
            TrackJoinBlocks? trackJoinBlocks = null)
        {
            TrackCombinePlanes = trackCombinePlanes;
            TrackJoinBlocks = trackJoinBlocks;
        }
        /// <summary>
        /// Contains the list of all video plane tracks that need to be combined to create this 3D track
        /// </summary>
        public TrackCombinePlanes? TrackCombinePlanes { get; set; }
        /// <summary>
        /// Contains the list of all tracks whose Blocks need to be combined to create this virtual track
        /// </summary>
        public TrackJoinBlocks? TrackJoinBlocks { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var trackOperation = writer.StartMasterElement(Elements.TrackOperation);
            if(TrackCombinePlanes != null)
            {
                TrackCombinePlanes.WriteTo(trackOperation);
            }
            if(TrackJoinBlocks != null)
            {
                TrackJoinBlocks.WriteTo(trackOperation);
            }
            trackOperation.Dispose();
        }

    }
}
