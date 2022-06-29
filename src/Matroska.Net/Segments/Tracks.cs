using Matroska.Net.Segments.TracksElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments
{
    /// <summary>
    /// A Top-Level Element of information with many tracks described.
    /// </summary>
    public class Tracks
    {
        internal Tracks(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<TrackEntry> trackEntries = new List<TrackEntry>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.TrackEntry)
                {
                    trackEntries.Add(new TrackEntry(reader, version));
                }
            }
            TrackEntries = trackEntries;
            reader.LeaveContainer();
        }
        public Tracks(IEnumerable<TrackEntry> trackEntries)
        {
            TrackEntries = trackEntries;
        }
        /// <summary>
        /// Describes a track with all Elements.
        /// </summary>
        public IEnumerable<TrackEntry> TrackEntries { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var tracks = writer.StartMasterElement(Elements.Tracks);
            foreach(var trackEntry in TrackEntries)
            {
                trackEntry.WriteTo(tracks);
            }
            tracks.Dispose();
        }
    }
}
