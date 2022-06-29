using Matroska.Net.Segments.TracksElements.TrackEntryElements.ContentEncodingsElements.ContentEncodingElements.ContentEncryptionElements;
using NEbml.Core;
using System;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.ContentEncodingsElements.ContentEncodingElements
{
    /// <summary>
    /// Settings describing the encryption used.
    /// This Element ** MUST** be present if the value of `ContentEncodingType` is 1 (encryption) and ** MUST** be ignored otherwise.
    /// </summary>
    public class ContentEncryption
    {
        internal ContentEncryption(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.ContentEncAlgo)
                {
                    ContentEncAlgo = (ContentEncAlgo)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ContentEncKeyID)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    ContentEncKeyID = new ArraySegment<byte>(data, 0, read);
                }
                else if(reader.ElementId == Elements.ContentEncAESSettings)
                {
                    ContentEncAESSettings = new ContentEncAESSettings(reader, version);
                }
                else if(version == 0 && reader.ElementId == Elements.ContentSignature)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data,0, data.Length);
                    ContentSignature = new ArraySegment<byte>(data, 0, read);
                }
                else if(version == 0 && reader.ElementId == Elements.ContentSigKeyID)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    ContentSigKeyID = new ArraySegment<byte>(data, 0, read);
                }
                else if(version == 0 && reader.ElementId == Elements.ContentSigAlgo)
                {
                    ContentSigAlgo = reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.ContentSigHashAlgo)
                {
                    ContentSigHashAlgo = reader.ReadUInt();
                }
            }
            reader.LeaveContainer();
        }
        public ContentEncryption(
            ContentEncAlgo contentEncAlgo = ContentEncAlgo.NotEncrypted,
            ArraySegment<byte>? contentEncKeyID = null,
            ContentEncAESSettings? contentEncAESSettings = null)
        {
            ContentEncAlgo = contentEncAlgo;
            ContentEncKeyID = contentEncKeyID;
            ContentEncAESSettings = contentEncAESSettings;
        }
        /// <summary>
        /// The encryption algorithm used.
        /// The value "0" means that the contents have not been encrypted.
        /// </summary>
        public ContentEncAlgo ContentEncAlgo { get; set; }
        /// <summary>
        /// For public key algorithms this is the ID of the public key the the data was encrypted with.
        /// </summary>
        public ArraySegment<byte>? ContentEncKeyID { get; set; }
        /// <summary>
        /// Settings describing the encryption algorithm used.
        /// It** MUST** be ignored if `ContentEncAlgo` is not AES(5).
        /// </summary>
        public ContentEncAESSettings? ContentEncAESSettings { get; set; }
        /// <summary>
        /// A cryptographic signature of the contents.
        /// </summary>
        [Obsolete("ContentSignature is since version 1 of matroska obsolete and should not be used")]
        public ArraySegment<byte>? ContentSignature { get; set; }
        /// <summary>
        /// This is the ID of the private key the data was signed with.
        /// </summary>
        [Obsolete("ContentSigKeyID is since version 1 of matroska obsolete and should not be used")]
        public ArraySegment<byte>? ContentSigKeyID { get; set; }
        /// <summary>
        /// The algorithm used for the signature.
        /// </summary>
        [Obsolete("ContentSigAlgo is since version 1 of matroska obsolete and should not be used")]
        public ulong? ContentSigAlgo { get; set; }
        /// <summary>
        /// The hash algorithm used for the signature.
        /// </summary>
        [Obsolete("ContentSigHashAlgo is since version 1 of matroska obsolete and should not be used")]
        public ulong? ContentSigHashAlgo { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var contentEncryption = writer.StartMasterElement(Elements.ContentEncryption);
            contentEncryption.Write(Elements.ContentEncAlgo, (ulong)ContentEncAlgo);
            if (ContentEncKeyID.HasValue)
            {
                contentEncryption.Write(Elements.ContentEncKeyID, ContentEncKeyID.Value.Array, ContentEncKeyID.Value.Offset, ContentEncKeyID.Value.Count);
            }
            if(ContentEncAESSettings != null)
            {
                ContentEncAESSettings.WriteTo(contentEncryption);
            }
            contentEncryption.Dispose();
        }

    }
}
