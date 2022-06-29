using Matroska.Net.Segments.AttachmentsElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments
{
    /// <summary>
    /// Contain attached files.
    /// </summary>
    public class Attachments
    {
        internal Attachments(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<AttachedFile> attachments = new List<AttachedFile>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.AttachedFile)
                {
                    attachments.Add(new AttachedFile(reader, version));
                }
            }
            AttachedFiles = attachments;
            reader.LeaveContainer();
        }
        public Attachments(IEnumerable<AttachedFile> attachedFiles)
        {
            AttachedFiles = attachedFiles;
        }
        /// <summary>
        /// An attached file.
        /// </summary>
        public IEnumerable<AttachedFile> AttachedFiles { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var attachments = writer.StartMasterElement(Elements.Attachments);
            foreach(var attachedFile in AttachedFiles)
            {
                attachedFile.WriteTo(attachments);
            }
            attachments.Dispose();
        }
    }
}
