using NEbml.Core;
using System;

namespace Matroska.Net.Segments.AttachmentsElements
{
    /// <summary>
    /// An attached file.
    /// </summary>
    public class AttachedFile
    {
        internal AttachedFile(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.FileDescription)
                {
                    FileDescription = reader.ReadUtf();
                }
                else if(reader.ElementId == Elements.FileName)
                {
                    FileName = reader.ReadUtf();
                }
                else if(reader.ElementId == Elements.FileMimeType)
                {
                    FileMimeType = reader.ReadAscii();
                }
                else if(reader.ElementId == Elements.FileData)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    FileData = new ArraySegment<byte>(data, 0, read);
                }
                else if(reader.ElementId == Elements.FileUID)
                {
                    FileUID = reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.FileReferral)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    FileReferral = new ArraySegment<byte>(data, 0, read);
                }
                else if(version == 0 && reader.ElementId == Elements.FileUsedStartTime)
                {
                    FileUsedStartTime = reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.FileUsedEndTime)
                {
                    FileUsedEndTime = reader.ReadUInt();
                }
            }
            reader.LeaveContainer();
        }
        public AttachedFile(
            string fileName,
            string fileMimeType,
            ArraySegment<byte> fileData,
            ulong fileUID,
            string? fileDescription = null)
        {
            FileDescription = fileDescription;
            FileName = fileName;
            FileMimeType = fileMimeType;
            FileData = fileData;
            FileUID = fileUID;
        }
        /// <summary>
        /// A human-friendly name for the attached file.
        /// </summary>
        public string? FileDescription { get; set; }
        /// <summary>
        /// Filename of the attached file.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// MIME type of the file.
        /// </summary>
        public string FileMimeType { get; set; }
        /// <summary>
        /// The data of the file.
        /// </summary>
        public ArraySegment<byte> FileData { get; set; }
        /// <summary>
        /// Unique ID representing the file, as random as possible.
        /// </summary>
        public ulong FileUID { get; set; }
        /// <summary>
        /// A binary value that a track/codec can refer to when the attachment is needed.
        /// </summary>
        [Obsolete("FileReferral is since version 1 of matroska obsolete and should not be used")]
        public ArraySegment<byte>? FileReferral { get; set; }
        /// <summary>
        /// The timestamp at which this optimized font attachment comes into context, expressed in Segment Ticks which is based on TimestampScale. See [@?DivXWorldFonts].
        /// <usage>This element is reserved for future use and if written **MUST** be the segment start timestamp.</usage>
        /// </summary>
        [Obsolete("FileUsedStartTime is since version 1 of matroska obsolete and should not be used")]
        public ulong? FileUsedStartTime { get; set; }
        /// <summary>
        /// The timestamp at which this optimized font attachment goes out of context, expressed in Segment Ticks which is based on TimestampScale. See [@?DivXWorldFonts].
        /// <usage>This element is reserved for future use and if written **MUST** be the segment end timestamp.</usage>
        /// </summary>
        [Obsolete("FileUsedEndTime is since version 1 of matroska obsolete and should not be used")]
        public ulong? FileUsedEndTime { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var attachedFile = writer.StartMasterElement(Elements.AttachedFile);
            if(FileDescription != null)
            {
                attachedFile.WriteUtf(Elements.FileDescription, FileDescription);
            }
            attachedFile.WriteUtf(Elements.FileName, FileName);
            attachedFile.WriteAscii(Elements.FileMimeType, FileMimeType);
            attachedFile.Write(Elements.FileData, FileData.Array, FileData.Offset, FileData.Count);
            attachedFile.Write(Elements.FileUID, FileUID);
            attachedFile.Dispose();
        }
    }
}
