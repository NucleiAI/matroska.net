using Matroska.Net.Segments.ClusterElements.BlockGroupElements.SlicesElements;
using NEbml.Core;
using System;
using System.Collections.Generic;

namespace Matroska.Net.Segments.ClusterElements.BlockGroupElements
{
    /// <summary>
    /// Contains slices description.
    /// </summary>
    [Obsolete("Slices is since version 1 of matroska obsolete and should not be used")]
    public class Slices
    {
        internal Slices(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<TimeSlice>? timeSlices = null;
            while (reader.ReadNext())
            {
                if(version == 0 && reader.ElementId == Elements.TimeSlice)
                {
                    if(timeSlices == null)
                    {
                        timeSlices = new List<TimeSlice>();
                    }
                    timeSlices.Add(new TimeSlice(reader, version));
                }
            }
            TimeSlices = timeSlices;
            reader.LeaveContainer();
        }
        public Slices(IEnumerable<TimeSlice>? timeSlices = null)
        {
            TimeSlices = timeSlices;
        }
        /// <summary>
        /// Contains extra time information about the data contained in the Block.
        /// Being able to interpret this Element is not** REQUIRED** for playback.
        /// </summary>
        [Obsolete("TimeSlice is since version 1 of matroska obsolete and should not be used")]
        public IEnumerable<TimeSlice>? TimeSlices { get; set; }
    }
}
