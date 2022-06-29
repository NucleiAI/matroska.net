using Matroska.Net.Segments.TagsElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments
{
    /// <summary>
    /// Element containing metadata describing Tracks, Editions, Chapters, Attachments, or the Segment as a whole.
    /// A list of valid tags can be found in [@!MatroskaTags].
    /// </summary>
    public class Tags
    {
        internal Tags(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<Tag> tags = new List<Tag>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.Tag)
                {
                    tags.Add(new Tag(reader, version));
                }
            }
            Tag = tags;
            reader.LeaveContainer();
        }
        public Tags(IEnumerable<Tag> tags)
        {
            Tag = tags;
        }
        /// <summary>
        /// A single metadata descriptor.
        /// </summary>
        public IEnumerable<Tag> Tag { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var tags = writer.StartMasterElement(Elements.Tags);
            foreach (var tag in Tag)
            {
                tag.WriteTo(tags);
            }
            tags.Dispose();
        }
    }
}
