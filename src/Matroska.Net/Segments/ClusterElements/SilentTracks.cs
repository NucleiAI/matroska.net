using NEbml.Core;
using System;
using System.Collections.Generic;

namespace Matroska.Net.Segments.ClusterElements
{
    /// <summary>
    /// The list of tracks that are not used in that part of the stream.
    /// It is useful when using overlay tracks on seeking or to decide what track to use.
    /// </summary>
    [Obsolete("Silent Tracks are since version 1 of matroska obsolete and should not be used")]
    public class SilentTracks
    {
        internal SilentTracks(EbmlReader reader,ulong version)
        {
            reader.EnterContainer();
            List<ulong>? silentTrackNumbers = null;
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.SilentTrackNumber)
                {
                    if(silentTrackNumbers == null)
                    {
                        silentTrackNumbers = new List<ulong>();
                    }
                    silentTrackNumbers.Add(reader.ReadUInt());
                }
            }
            SilentTrackNumbers = silentTrackNumbers;
            reader.LeaveContainer();
        }
        public SilentTracks(IEnumerable<ulong>? silentTrackNumbers = null)
        {
            SilentTrackNumbers = silentTrackNumbers;
        }
        /// <summary>
        /// One of the track number that are not used from now on in the stream.
        /// It could change later if not specified as silent in a further Cluster.
        /// </summary>
        [Obsolete("Silent Tracks are since version 1 of matroska obsolete and should not be used")]
        public IEnumerable<ulong>? SilentTrackNumbers { get; set; }
    }
}
