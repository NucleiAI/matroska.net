using NEbml.Core;
using System;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.VideoElements
{
    /// <summary>
    /// Describes the video projection details. Used to render spherical, VR videos or flipping videos horizontally/vertically.
    /// </summary>
    public class Projection
    {
        internal Projection(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.ProjectionType)
                {
                    ProjectionType = (ProjectionType)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ProjectionPrivate)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    ProjectionPrivate = new ArraySegment<byte>(data, 0, read);
                }
                else if(reader.ElementId == Elements.ProjectionPoseYaw)
                {
                    ProjectionPoseYaw = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.ProjectionPosePitch)
                {
                    ProjectionPosePitch = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.ProjectionPoseRoll)
                {
                    ProjectionPoseRoll = reader.ReadFloat();
                }
            }
            reader.LeaveContainer();
        }
        public Projection(
            ProjectionType projectionType = ProjectionType.Rectangular,
            ArraySegment<byte>? projectionPrivate = null,
            double projectionPoseYaw = 0,
            double projectionPosePitch = 0,
            double projectionPoseRoll = 0)
        {
            ProjectionType = projectionType;
            ProjectionPrivate = projectionPrivate;
            ProjectionPoseYaw = projectionPoseYaw;
            ProjectionPosePitch = projectionPosePitch;
            ProjectionPoseRoll = projectionPoseRoll;
        }
        /// <summary>
        /// Describes the projection used for this video track.
        /// </summary>
        public ProjectionType ProjectionType { get; set; }
        /// <summary>
        /// Private data that only applies to a specific projection.
        /// *  If `ProjectionType` equals 0 (Rectangular),
        /// then this element must not be present.
        /// * If `ProjectionType` equals 1 (Equirectangular), then this element must be present and contain the same binary data that would be stored inside
        /// an ISOBMFF Equirectangular Projection Box ('equi').
        /// *  If `ProjectionType` equals 2 (Cubemap), then this element must be present and contain the same binary data that would be stored
        /// inside an ISOBMFF Cubemap Projection Box ('cbmp').
        /// *  If `ProjectionType` equals 3 (Mesh), then this element must be present and contain the same binary data that would be stored inside
        /// an ISOBMFF Mesh Projection Box ('mshp').
        /// <usage>ISOBMFF box size and fourcc fields are not included in the binary data,
        /// but the FullBox version and flag fields are.This is to avoid
        /// redundant framing information while preserving versioning and semantics between the two container formats.</usage>
        /// </summary>
        public ArraySegment<byte>? ProjectionPrivate { get; set; }
        /// <summary>
        /// Specifies a yaw rotation to the projection.
        /// Value represents a clockwise rotation, in degrees, around the up vector.This rotation must be applied
        /// before any `ProjectionPosePitch` or `ProjectionPoseRoll` rotations.
        /// The value of this element** MUST** be in the -180 to 180 degree range, both included.
        /// Setting `ProjectionPoseYaw` to 180 or -180 degrees, with the `ProjectionPoseRoll` and `ProjectionPosePitch` set to 0 degrees flips the image horizontally.
        /// </summary>
        public double ProjectionPoseYaw { get; set; }
        /// <summary>
        /// Specifies a pitch rotation to the projection.
        /// Value represents a counter-clockwise rotation, in degrees, around the right vector.This rotation must be applied
        /// after the `ProjectionPoseYaw` rotation and before the `ProjectionPoseRoll` rotation.
        /// The value of this element** MUST** be in the -90 to 90 degree range, both included.
        /// </summary>
        public double ProjectionPosePitch { get; set; }
        /// <summary>
        /// Specifies a roll rotation to the projection.
        /// Value represents a counter-clockwise rotation, in degrees, around the forward vector.This rotation must be applied
        /// after the `ProjectionPoseYaw` and `ProjectionPosePitch` rotations.
        /// The value of this element** MUST** be in the -180 to 180 degree range, both included.
        /// Setting `ProjectionPoseRoll` to 180 or -180 degrees, the `ProjectionPoseYaw` to 180 or -180 degrees with `ProjectionPosePitch` set to 0 degrees flips the image vertically.
        /// Setting `ProjectionPoseRoll` to 180 or -180 degrees, with the `ProjectionPoseYaw` and `ProjectionPosePitch` set to 0 degrees flips the image horizontally and vertically.
        /// </summary>
        public double ProjectionPoseRoll { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var projection = writer.StartMasterElement(Elements.Projection);
            projection.Write(Elements.ProjectionType, (ulong)ProjectionType);
            if (ProjectionPrivate.HasValue)
            {
                projection.Write(Elements.ProjectionPrivate, ProjectionPrivate.Value.Array, ProjectionPrivate.Value.Offset, ProjectionPrivate.Value.Count);
            }
            projection.Write(Elements.ProjectionPoseYaw, ProjectionPoseYaw);
            projection.Write(Elements.ProjectionPosePitch, ProjectionPosePitch);
            projection.Write(Elements.ProjectionPoseRoll, ProjectionPoseRoll);
            projection.Dispose();
        }
    }
}
