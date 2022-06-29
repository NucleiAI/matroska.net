using Matroska.Net.Segments.ChaptersElements.EditionEntryElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments.ChaptersElements
{
    /// <summary>
    /// Contains all information about a Segment edition.
    /// </summary>
    public class EditionEntry
    {
        internal EditionEntry(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<ChapterAtom> chapters = new List<ChapterAtom>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.EditionUID)
                {
                    EditionUID = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.EditionFlagHidden)
                {
                    EditionFlagHidden = reader.ReadUInt() == 1;
                }
                else if(reader.ElementId == Elements.EditionFlagDefault)
                {
                    EditionFlagDefault = reader.ReadUInt() == 1;
                }
                else if(reader.ElementId == Elements.EditionFlagOrdered)
                {
                    EditionFlagOrdered = reader.ReadUInt() == 1;
                }
                else if(reader.ElementId == Elements.ChapterAtom)
                {
                    chapters.Add(new ChapterAtom(reader, version));
                }
            }
            ChapterAtoms = chapters;
            reader.LeaveContainer();
        }
        public EditionEntry(
            IEnumerable<ChapterAtom> chapterAtoms,
            ulong? editionUID = null,
            bool editionFlagHidden = false,
            bool editionFlagDefault = false,
            bool editionFlagOrdered = false)
        {
            EditionUID = editionUID;
            EditionFlagHidden = editionFlagHidden;
            EditionFlagDefault = editionFlagDefault;
            EditionFlagOrdered = editionFlagOrdered;
            ChapterAtoms = chapterAtoms;
        }
        /// <summary>
        /// A unique ID to identify the edition. It's useful for tagging an edition.
        /// </summary>
        public ulong? EditionUID { get; set; }
        /// <summary>
        /// Set to 1 if an edition is hidden. Hidden editions **SHOULD NOT** be available to the user interface
        /// (but still to Control Tracks; see(#chapter-flags) on Chapter flags).
        /// </summary>
        public bool EditionFlagHidden { get; set; }
        /// <summary>
        /// Set to 1 if the edition **SHOULD** be used as the default one.
        /// </summary>
        public bool EditionFlagDefault { get; set; }
        /// <summary>
        /// Set to 1 if the chapters can be defined multiple times and the order to play them is enforced; see (#editionflagordered).
        /// </summary>
        public bool EditionFlagOrdered { get; set; }
        /// <summary>
        /// Contains the atom information to use as the chapter atom (apply to all tracks).
        /// </summary>
        public IEnumerable<ChapterAtom> ChapterAtoms { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var editionEntry = writer.StartMasterElement(Elements.EditionEntry);
            if (EditionUID.HasValue)
            {
                editionEntry.Write(Elements.EditionUID, EditionUID.Value);
            }
            editionEntry.Write(Elements.EditionFlagHidden, (ulong)(EditionFlagHidden ? 1 : 0));
            editionEntry.Write(Elements.EditionFlagDefault, (ulong)(EditionFlagDefault ? 1 : 0));
            editionEntry.Write(Elements.EditionFlagOrdered, (ulong)(EditionFlagOrdered ? 1 : 0));
            foreach(var chapterAtom in ChapterAtoms)
            {
                chapterAtom.WriteTo(editionEntry);
            }
            editionEntry.Dispose();
        }
    }
}
