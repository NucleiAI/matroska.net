using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.TrackOperationElements
{
    /// <summary>
    /// Contains the list of all tracks whose Blocks need to be combined to create this virtual track
    /// </summary>
    public class TrackJoinBlocks
    {
        internal TrackJoinBlocks(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            var trackJoinUID = new List<ulong>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.TrackJoinUID)
                {
                    trackJoinUID.Add(reader.ReadUInt());
                }
            }
            TrackJoinUID = trackJoinUID;
            reader.LeaveContainer();
        }
        public TrackJoinBlocks(
            IEnumerable<ulong> trackJoinUID)
        {
            TrackJoinUID = trackJoinUID;
        }
        /// <summary>
        /// The trackUID number of a track whose blocks are used to create this virtual track.
        /// </summary>
        public IEnumerable<ulong> TrackJoinUID { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var trackJoinBlocks = writer.StartMasterElement(Elements.TrackJoinBlocks);
            foreach(var trackJoin in TrackJoinUID)
            {
                trackJoinBlocks.Write(Elements.TrackJoinUID, trackJoin);
            }
            trackJoinBlocks.Dispose();
        }

    }
}
