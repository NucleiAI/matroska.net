using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments.ChaptersElements.EditionEntryElements.ChapterAtomElements
{
    /// <summary>
    /// Contains all possible strings to use for the chapter display.
    /// </summary>
    public class ChapterDisplay
    {
        internal ChapterDisplay(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<string> chapLanguage = new List<string>();
            List<string>? chapLanguageIETF = null;
            List<string>? chapCountry = null;
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.ChapString)
                {
                    ChapString = reader.ReadUtf();
                }
                else if(reader.ElementId == Elements.ChapLanguage)
                {
                    chapLanguage.Add(reader.ReadAscii());
                }
                else if(reader.ElementId == Elements.ChapLanguageIETF)
                {
                    if(chapLanguageIETF == null)
                    {
                        chapLanguageIETF = new List<string>();
                    }
                    chapLanguageIETF.Add(reader.ReadAscii());
                }
                else if(reader.ElementId == Elements.ChapCountry)
                {
                    if(chapCountry == null)
                    {
                        chapCountry = new List<string>();
                    }
                    chapCountry.Add(reader.ReadAscii());
                }
            }
            ChapLanguage = chapLanguage;
            ChapLanguageIETF = chapLanguageIETF;
            ChapCountry = chapCountry;
            reader.LeaveContainer();
        }
        public ChapterDisplay(
            string chapString,
            IEnumerable<string>? chapLanguage = null,
            IEnumerable<string>? chapLanguageIETF = null,
            IEnumerable<string>? chapCountry = null)
        {
            chapLanguage ??= new List<string>() { "eng" };
            ChapString = chapString;
            ChapLanguage = chapLanguage;
            ChapLanguageIETF=chapLanguageIETF;
            ChapCountry = chapCountry;
        }
        /// <summary>
        /// Contains the string to use as the chapter atom.
        /// </summary>
        public string ChapString { get; set; }
        /// <summary>
        /// A language corresponding to the string,
        /// in the bibliographic ISO-639-2 form[@!ISO639-2].
        /// This Element **MUST** be ignored if a ChapLanguageIETF Element is used within the same ChapterDisplay Element.
        /// </summary>
        public IEnumerable<string> ChapLanguage { get; set; }
        /// <summary>
        /// Specifies a language corresponding to the ChapString in the format defined in [@!BCP47]
        /// and using the IANA Language Subtag Registry[@!IANALangRegistry].
        /// If a ChapLanguageIETF Element is used, then any ChapLanguage and ChapCountry Elements used in the same ChapterDisplay** MUST** be ignored.
        /// </summary>
        public IEnumerable<string>? ChapLanguageIETF { get; set; }
        /// <summary>
        /// A country corresponding to the string, using the same 2 octets country-codes as in Internet domains [@!IANADomains] based on [@!ISO3166-1] alpha-2 codes.
        /// This Element **MUST** be ignored if a ChapLanguageIETF Element is used within the same ChapterDisplay Element.
        /// </summary>
        public IEnumerable<string>? ChapCountry { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var chapterDisplay = writer.StartMasterElement(Elements.ChapterDisplay);
            chapterDisplay.WriteUtf(Elements.ChapString, ChapString);
            foreach(var chapLanguage in ChapLanguage)
            {
                chapterDisplay.WriteAscii(Elements.ChapLanguage, chapLanguage);
            }
            if(ChapLanguageIETF != null)
            {
                foreach(var chapLanguageIETF in ChapLanguageIETF)
                {
                    chapterDisplay.WriteAscii(Elements.ChapLanguageIETF, chapLanguageIETF);
                }
            }
            if(ChapCountry != null)
            {
                foreach(var chapCountry in ChapCountry)
                {
                    chapterDisplay.WriteAscii(Elements.ChapCountry, chapCountry);
                }
            }
            chapterDisplay.Dispose();
        }
    }
}
