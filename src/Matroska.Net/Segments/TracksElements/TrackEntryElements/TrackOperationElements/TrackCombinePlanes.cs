using Matroska.Net.Segments.TracksElements.TrackEntryElements.TrackOperationElements.TrackCombinePlanesElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.TrackOperationElements
{
    /// <summary>
    /// Contains the list of all video plane tracks that need to be combined to create this 3D track
    /// </summary>
    public class TrackCombinePlanes
    {
        internal TrackCombinePlanes(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<TrackPlane> planes = new List<TrackPlane>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.TrackPlane)
                {
                    planes.Add(new TrackPlane(reader, version));
                }
            }
            TrackPlanes = planes;
            reader.LeaveContainer();
        }
        public TrackCombinePlanes(
            IEnumerable<TrackPlane> trackPlanes)
        {
            TrackPlanes = trackPlanes;
        }
        /// <summary>
        /// Contains a video plane track that need to be combined to create this 3D track
        /// </summary>
        public IEnumerable<TrackPlane> TrackPlanes { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var trackCombinePlanes = writer.StartMasterElement(Elements.TrackCombinePlanes);
            foreach(var trackPlane in TrackPlanes)
            {
                trackPlane.WriteTo(trackCombinePlanes);
            }
            trackCombinePlanes.Dispose();
        }
    }
}
