using Matroska.Net.Segments.TagsElements.TagElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments.TagsElements
{
    /// <summary>
    /// A single metadata descriptor.
    /// </summary>
    public class Tag
    {
        internal Tag(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<SimpleTag> tags = new List<SimpleTag>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.Targets)
                {
                    Targets = new Targets(reader, version);
                }
                else if(reader.ElementId == Elements.SimpleTag)
                {
                    tags.Add(new SimpleTag(reader, version));
                }
            }
            SimpleTags = tags;
            reader.LeaveContainer();
        }
        public Tag(
            Targets targets,
            IEnumerable<SimpleTag> simpleTags)
        {
            Targets = targets;
            SimpleTags = simpleTags;
        }
        /// <summary>
        /// Specifies which other elements the metadata represented by the Tag applies to.
        /// If empty or not present, then the Tag describes everything in the Segment.
        /// </summary>
        public Targets Targets { get; set; }
        /// <summary>
        /// Contains general information about the target.
        /// </summary>
        public IEnumerable<SimpleTag> SimpleTags { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var tag = writer.StartMasterElement(Elements.Tag);
            Targets.WriteTo(tag);
            foreach(var simpleTag in SimpleTags)
            {
                simpleTag.WriteTo(tag);
            }
            tag.Dispose();
        }
    }
}
