using Matroska.Net.Segments.TracksElements.TrackEntryElements.ContentEncodingsElements.ContentEncodingElements;
using NEbml.Core;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.ContentEncodingsElements
{
    /// <summary>
    /// Settings for one content encoding like compression or encryption.
    /// </summary>
    public class ContentEncoding
    {
        internal ContentEncoding(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.ContentEncodingOrder)
                {
                    ContentEncodingOrder = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ContentEncodingScope)
                {
                    ContentEncodingScope = (ContentEncodingScope)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ContentEncodingType)
                {
                    ContentEncodingType = (ContentEncodingType)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ContentCompression)
                {
                    ContentCompression = new ContentCompression(reader, version);
                }
                else if(reader.ElementId == Elements.ContentEncryption)
                {
                    ContentEncryption = new ContentEncryption(reader, version);
                }
            }
            reader.LeaveContainer();
        }
        public ContentEncoding(
            ulong contentEncodingOrder = 0,
            ContentEncodingScope contentEncodingScope = ContentEncodingScope.Block,
            ContentEncodingType contentEncodingType = ContentEncodingType.Compression,
            ContentCompression? contentCompression = null,
            ContentEncryption? contentEncryption = null)
        {
            ContentEncodingOrder = contentEncodingOrder;
            ContentEncodingScope = contentEncodingScope;
            ContentEncodingType = contentEncodingType;
            ContentCompression = contentCompression;
            ContentEncryption = contentEncryption;
        }
        /// <summary>
        /// Tell in which order to apply each `ContentEncoding` of the `ContentEncodings`.
        /// The decoder/demuxer** MUST** start with the `ContentEncoding` with the highest `ContentEncodingOrder` and work its way down to the `ContentEncoding` with the lowest `ContentEncodingOrder`.
        /// This value **MUST** be unique over for each `ContentEncoding` found in the `ContentEncodings` of this `TrackEntry`.
        /// </summary>
        public ulong ContentEncodingOrder { get; set; }
        /// <summary>
        /// A bit field that describes which Elements have been modified in this way.
        /// Values(big-endian) can be OR'ed.
        /// </summary>
        public ContentEncodingScope ContentEncodingScope { get; set; }
        /// <summary>
        /// A value describing what kind of transformation is applied.
        /// </summary>
        public ContentEncodingType ContentEncodingType { get; set; }
        /// <summary>
        /// Settings describing the compression used.
        /// This Element **MUST** be present if the value of ContentEncodingType is 0 and absent otherwise.
        /// Each block **MUST** be decompressable even if no previous block is available in order not to prevent seeking.
        /// </summary>
        public ContentCompression? ContentCompression { get; set; }
        /// <summary>
        /// Settings describing the encryption used.
        /// This Element **MUST** be present if the value of `ContentEncodingType` is 1 (encryption) and **MUST** be ignored otherwise.
        /// </summary>
        public ContentEncryption? ContentEncryption { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var contentEncoding = writer.StartMasterElement(Elements.ContentEncoding);
            contentEncoding.Write(Elements.ContentEncodingOrder, ContentEncodingOrder);
            contentEncoding.Write(Elements.ContentEncodingScope, (ulong)ContentEncodingScope);
            contentEncoding.Write(Elements.ContentEncodingType, (ulong)ContentEncodingType);
            if(ContentCompression != null)
            {
                ContentCompression.WriteTo(contentEncoding);
            }
            if(ContentEncryption != null)
            {
                ContentEncryption.WriteTo(contentEncoding);
            }
            contentEncoding.Dispose();
        }
    }
}
