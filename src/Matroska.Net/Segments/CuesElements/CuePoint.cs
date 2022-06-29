using Matroska.Net.Segments.CuesElements.CuePointElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments.CuesElements
{
    /// <summary>
    /// Contains all information relative to a seek point in the Segment.
    /// </summary>
    public class CuePoint
    {
        internal CuePoint(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<CueTrackPositions> positions = new List<CueTrackPositions>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.CueTime)
                {
                    CueTime = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.CueTrackPositions)
                {
                    positions.Add(new CueTrackPositions(reader, version));
                }
            }
            CueTrackPositions = positions;
            reader.LeaveContainer();
        }
        public CuePoint(
            ulong cueTime,
            IEnumerable<CueTrackPositions> cueTrackPositions)
        {
            CueTime = cueTime;
            CueTrackPositions = cueTrackPositions;
        }
        /// <summary>
        /// Absolute timestamp of the seek point, expressed in Matroska Ticks -- ie in nanoseconds; see (#timestamp-ticks).
        /// </summary>
        public ulong CueTime { get; set; }
        /// <summary>
        /// Contain positions for different tracks corresponding to the timestamp.
        /// </summary>
        public IEnumerable<CueTrackPositions> CueTrackPositions { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var cuePoint = writer.StartMasterElement(Elements.CuePoint);
            cuePoint.Write(Elements.CueTime, CueTime);
            foreach(var position in CueTrackPositions)
            {
                position.WriteTo(cuePoint);
            }
            cuePoint.Dispose();
        }

    }
}
