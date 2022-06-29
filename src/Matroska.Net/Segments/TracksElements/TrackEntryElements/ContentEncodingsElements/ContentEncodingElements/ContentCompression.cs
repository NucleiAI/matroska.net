using NEbml.Core;
using System;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.ContentEncodingsElements.ContentEncodingElements
{
    /// <summary>
    /// Settings describing the compression used.
    /// This Element ** MUST** be present if the value of ContentEncodingType is 0 and absent otherwise.
    /// Each block ** MUST** be decompressable even if no previous block is available in order not to prevent seeking.
    /// </summary>
    public class ContentCompression
    {
        internal ContentCompression(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.ContentCompAlgo)
                {
                    ContentCompAlgo = (ContentCompAlgo)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ContentCompSettings)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    ContentCompSettings = new ArraySegment<byte>(data, 0, read);
                }
            }
            reader.LeaveContainer();
        }
        public ContentCompression(
            ContentCompAlgo contentCompAlgo = ContentCompAlgo.ZLib,
            ArraySegment<byte>? contentCompSettings = null)
        {
            ContentCompAlgo = contentCompAlgo;
            ContentCompSettings = contentCompSettings;
        }
        /// <summary>
        /// The compression algorithm used.
        /// <usage>Compression method "1" (bzlib) and "2" (lzo1x) are lacking proper documentation on the format which limits implementation possibilities.
        /// Due to licensing conflicts on commonly available libraries compression methods "2" (lzo1x) does not offer widespread interoperability.
        /// Decoding implementations** MAY** support methods "1" and "2" as possible.The use of these compression methods **SHOULD NOT** be used as a default.</usage>
        /// </summary>
        public ContentCompAlgo ContentCompAlgo { get; set; }
        /// <summary>
        /// Settings that might be needed by the decompressor. For Header Stripping (`ContentCompAlgo`=3),
        /// the bytes that were removed from the beginning of each frames of the track.
        /// </summary>
        public ArraySegment<byte>? ContentCompSettings { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var contentCompression = writer.StartMasterElement(Elements.ContentCompression);
            writer.Write(Elements.ContentCompAlgo, (ulong)ContentCompAlgo);
            if (ContentCompSettings.HasValue)
            {
                writer.Write(Elements.ContentCompSettings, ContentCompSettings.Value.Array, ContentCompSettings.Value.Offset, ContentCompSettings.Value.Count);
            }
            contentCompression.Dispose();
        }
    }
}
