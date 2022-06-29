using Matroska.Net.Segments.ChaptersElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments
{
    /// <summary>
    /// A system to define basic menus and partition data.
    /// For more detailed information, look at the Chapters explanation in (#chapters).
    /// </summary>
    public class Chapters
    {
        internal Chapters(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<EditionEntry> editions = new List<EditionEntry>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.EditionEntry)
                {
                    editions.Add(new EditionEntry(reader, version));
                }
            }
            EditionEntries = editions;
            reader.LeaveContainer();
        }
        public Chapters(
            IEnumerable<EditionEntry> editionEntries)
        {
            EditionEntries = editionEntries;
        }
        /// <summary>
        /// Contains all information about a Segment edition.
        /// </summary>
        public IEnumerable<EditionEntry> EditionEntries { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var chapters = writer.StartMasterElement(Elements.Chapters);
            foreach(var editionEntry in EditionEntries)
            {
                editionEntry.WriteTo(chapters);
            }
            chapters.Dispose();
        }
    }
}
