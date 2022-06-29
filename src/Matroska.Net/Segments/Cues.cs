using Matroska.Net.Segments.CuesElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments
{
    /// <summary>
    /// A Top-Level Element to speed seeking access.
    /// All entries are local to the Segment.
    /// <implementation>This Element **SHOULD** be set when the Segment is not transmitted as a live stream (see #livestreaming).</implementation>
    /// </summary>
    public class Cues
    {
        internal Cues(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<CuePoint> points = new List<CuePoint>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.CuePoint)
                {
                    points.Add(new CuePoint(reader, version));
                }
            }
            CuePoints = points;
            reader.LeaveContainer();
        }
        public Cues(IEnumerable<CuePoint> cuePoints)
        {
            CuePoints = cuePoints;
        }
        /// <summary>
        /// Contains all information relative to a seek point in the Segment.
        /// </summary>
        public IEnumerable<CuePoint> CuePoints { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var cues = writer.StartMasterElement(Elements.Cues);
            foreach(var cuePoint in CuePoints)
            {
                cuePoint.WriteTo(cues);
            }
            cues.Dispose();
        }
    }
}
