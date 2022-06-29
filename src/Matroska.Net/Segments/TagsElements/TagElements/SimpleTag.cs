using NEbml.Core;
using System;

namespace Matroska.Net.Segments.TagsElements.TagElements
{
    /// <summary>
    /// Contains general information about the target.
    /// </summary>
    public class SimpleTag
    {
        internal SimpleTag(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.TagName)
                {
                    TagName = reader.ReadUtf();
                }
                else if(reader.ElementId == Elements.TagLanguage)
                {
                    TagLanguage = reader.ReadAscii();
                }
                else if(reader.ElementId == Elements.TagLanguageIETF)
                {
                    TagLanguageIETF = reader.ReadAscii();
                }
                else if(reader.ElementId == Elements.TagDefault)
                {
                    TagDefault = reader.ReadUInt() != 0;
                }
                else if(version == 0 && reader.ElementId == Elements.TagDefaultBogus)
                {
                    TagDefaultBogus = reader.ReadUInt() != 0;
                }
                else if(reader.ElementId == Elements.TagString)
                {
                    TagString = reader.ReadUtf();
                }
                else if(reader.ElementId == Elements.TagBinary)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    TagBinary = new ArraySegment<byte>(data, 0, read);
                }
            }
            if(TagLanguage == null)
            {
                TagLanguage = "und";
            }
            reader.LeaveContainer();
        }
        public SimpleTag(
            string tagName,
            string tagLanguage = "und",
            string? tagLanguageIETF = null,
            bool tagDefault = true,
            string? tagString = null,
            ArraySegment<byte>? tagBinary = null)
        {
            TagName = tagName;
            TagLanguage = tagLanguage;
            TagLanguageIETF = tagLanguageIETF;
            TagDefault = tagDefault;
            TagString = tagString;
            TagBinary = tagBinary;
        }
        /// <summary>
        /// The name of the Tag that is going to be stored.
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// Specifies the language of the tag specified, in the Matroska languages form;
        /// see(#language-codes) on language codes.
        /// This Element **MUST** be ignored if the TagLanguageIETF Element is used within the same SimpleTag Element.
        /// </summary>
        public string TagLanguage { get; set; }
        /// <summary>
        /// Specifies the language used in the TagString according to [@!BCP47]
        /// and using the IANA Language Subtag Registry[@!IANALangRegistry].
        /// If this Element is used, then any TagLanguage Elements used in the same SimpleTag** MUST** be ignored.
        /// </summary>
        public string? TagLanguageIETF { get; set; }
        /// <summary>
        /// A boolean value to indicate if this is the default/original language to use for the given tag.
        /// </summary>
        public bool TagDefault { get; set; }
        /// <summary>
        /// A variant of the TagDefault element with a bogus Element ID; see (#tagdefault-element).
        /// </summary>
        [Obsolete("TagDefaultBogus is since version 1 of matroska obsolete and should not be used")]
        public bool? TagDefaultBogus { get; set; }
        /// <summary>
        /// The value of the Tag.
        /// </summary>
        public string? TagString { get; set; }
        /// <summary>
        /// The values of the Tag, if it is binary. Note that this cannot be used in the same SimpleTag as TagString.
        /// </summary>
        public ArraySegment<byte>? TagBinary { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var simpleTag = writer.StartMasterElement(Elements.SimpleTag);
            simpleTag.WriteUtf(Elements.TagName, TagName);
            simpleTag.WriteAscii(Elements.TagLanguage, TagLanguage);
            if(TagLanguageIETF != null)
            {
                simpleTag.WriteAscii(Elements.TagLanguageIETF, TagLanguageIETF);
            }
            simpleTag.Write(Elements.TagDefault, (ulong)(TagDefault ? 1 : 0));
            if(TagString != null)
            {
                simpleTag.WriteUtf(Elements.TagString, TagString);
            }
            if (TagBinary.HasValue)
            {
                simpleTag.Write(Elements.TagBinary, TagBinary.Value.Array, TagBinary.Value.Offset, TagBinary.Value.Count);
            }
            simpleTag.Dispose();
        }
    }
}
