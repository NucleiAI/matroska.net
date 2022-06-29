using Matroska.Net.Segments.SeekHeadElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments
{
    /// <summary>
    /// Contains the Segment Position of other Top-Level Elements.
    /// </summary>
    public class SeekHead
    {
        internal SeekHead(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<Seek> seeks = new List<Seek>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.Seek)
                {
                    seeks.Add(new Seek(reader, version));
                }
            }
            Seeks = seeks;
            reader.LeaveContainer();
        }
        public SeekHead(IEnumerable<Seek> seeks)
        {
            Seeks = seeks;
        }
        /// <summary>
        /// Contains a single seek entry to an EBML Element.
        /// </summary>
        public IEnumerable<Seek> Seeks { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var seekHead = writer.StartMasterElement(Elements.SeekHead);
            foreach(var seek in Seeks)
            {
                seek.WriteTo(seekHead);
            }
            seekHead.Dispose();
        }
    }
}
