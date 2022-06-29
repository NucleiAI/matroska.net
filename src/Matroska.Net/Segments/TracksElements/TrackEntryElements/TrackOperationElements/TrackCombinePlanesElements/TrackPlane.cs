using NEbml.Core;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.TrackOperationElements.TrackCombinePlanesElements
{
    /// <summary>
    /// Contains a video plane track that need to be combined to create this 3D track
    /// </summary>
    public class TrackPlane
    {
        internal TrackPlane(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.TrackPlaneUID)
                {
                    TrackPlaneUID = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.TrackPlaneType)
                {
                    TrackPlaneType = (TrackPlaneType)reader.ReadUInt();
                }
            }
            reader.LeaveContainer();
        }
        public TrackPlane(
            ulong trackPlaneUID,
            TrackPlaneType trackPlaneType)
        {
            TrackPlaneUID = trackPlaneUID;
            TrackPlaneType = trackPlaneType;
        }
        /// <summary>
        /// The trackUID number of the track representing the plane.
        /// </summary>
        public ulong TrackPlaneUID { get; set; }
        /// <summary>
        /// The kind of plane this track corresponds to.
        /// </summary>
        public TrackPlaneType TrackPlaneType { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var trackPlane = writer.StartMasterElement(Elements.TrackPlane);
            trackPlane.Write(Elements.TrackPlaneUID, TrackPlaneUID);
            trackPlane.Write(Elements.TrackPlaneType, (ulong)TrackPlaneType);
            trackPlane.Dispose();
        }
    }
}
