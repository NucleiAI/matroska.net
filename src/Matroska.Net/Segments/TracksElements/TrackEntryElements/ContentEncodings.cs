using Matroska.Net.Segments.TracksElements.TrackEntryElements.ContentEncodingsElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements
{
    /// <summary>
    /// Settings for several content encoding mechanisms like compression or encryption.
    /// </summary>
    public class ContentEncodings
    {
        internal ContentEncodings(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<ContentEncoding> contentEncodings = new List<ContentEncoding>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.ContentEncoding)
                {
                    contentEncodings.Add(new ContentEncoding(reader, version));
                }
            }
            ContentEncoding = contentEncodings;
            reader.LeaveContainer();
        }
        public ContentEncodings(IEnumerable<ContentEncoding> contentEncodings)
        {
            ContentEncoding = contentEncodings;
        }
        /// <summary>
        /// Settings for one content encoding like compression or encryption.
        /// </summary>
        public IEnumerable<ContentEncoding> ContentEncoding { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var contentEncodings = writer.StartMasterElement(Elements.ContentEncodings);
            foreach (var contentEncoding in ContentEncoding)
            {
                contentEncoding.WriteTo(contentEncodings);
            }
            contentEncodings.Dispose();
        }
    }
}
