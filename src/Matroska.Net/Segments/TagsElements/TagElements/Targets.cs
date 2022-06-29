using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments.TagsElements.TagElements
{
    /// <summary>
    /// Specifies which other elements the metadata represented by the Tag applies to.
    /// If empty or not present, then the Tag describes everything in the Segment.
    /// </summary>
    public class Targets
    {
        internal Targets(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<ulong>? tagTrackUID = null;
            List<ulong>? tagEditionUID = null;
            List<ulong>? tagChapterUID = null;
            List<ulong>? tagAttachmentUID = null;
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.TargetTypeValue)
                {
                    TargetTypeValue = (TargetTypeValue)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.TargetType)
                {
                    TargetType = new TargetType(reader.ReadAscii());
                }
                else if(reader.ElementId == Elements.TagTrackUID)
                {
                    if(tagTrackUID == null)
                    {
                        tagTrackUID = new List<ulong>();
                    }
                    tagTrackUID.Add(reader.ReadUInt());
                }
                else if(reader.ElementId == Elements.TagEditionUID)
                {
                    if(tagEditionUID == null)
                    {
                        tagEditionUID = new List<ulong>();
                    }
                    tagEditionUID.Add(reader.ReadUInt());
                }
                else if(reader.ElementId == Elements.TagChapterUID)
                {
                    if(tagChapterUID == null)
                    {
                        tagChapterUID = new List<ulong>();
                    }
                    tagChapterUID.Add(reader.ReadUInt());
                }
                else if(reader.ElementId == Elements.TagAttachmentUID)
                {
                    if(tagAttachmentUID == null)
                    {
                        tagAttachmentUID = new List<ulong>();
                    }
                    tagAttachmentUID.Add(reader.ReadUInt());
                }
            }
            TagAttachmentUID = tagAttachmentUID;
            TagChapterUID = tagChapterUID;
            TagEditionUID = tagEditionUID;
            TagTrackUID = tagTrackUID;
            reader.LeaveContainer();
        }
        public Targets(
            TargetTypeValue targetTypeValue = TargetTypeValue.AlbumOperaConcertMovieEpisode,
            TargetType? targetType = null,
            IEnumerable<ulong>? tagTrackUID = null,
            IEnumerable<ulong>? tagEditionUID = null,
            IEnumerable<ulong>? tagChapterUID = null,
            IEnumerable<ulong>? tagAttachmentUID = null)
        {
            TargetTypeValue = targetTypeValue;
            TargetType = targetType;
            TagTrackUID = tagTrackUID;
            TagEditionUID = tagEditionUID;
            TagChapterUID = tagChapterUID;
            TagAttachmentUID = tagAttachmentUID;
        }
        /// <summary>
        /// A number to indicate the logical level of the target.
        /// </summary>
        public TargetTypeValue TargetTypeValue { get; set; }
        /// <summary>
        /// An informational string that can be used to display the logical level of the target like "ALBUM", "TRACK", "MOVIE", "CHAPTER", etc;
        /// see Section 6.4 of[@!MatroskaTags].
        /// </summary>
        public TargetType? TargetType { get; set; }
        /// <summary>
        /// A unique ID to identify the Track(s) the tags belong to.</documentation>
        /// <usage>If the value is 0 at this level, the tags apply to all tracks in the Segment.
        /// If set to any other value, it** MUST** match the `TrackUID` value of a track found in this Segment.</usage>
        /// </summary>
        public IEnumerable<ulong>? TagTrackUID { get; set; }
        /// <summary>
        /// A unique ID to identify the EditionEntry(s) the tags belong to.</documentation>
        /// <usage>If the value is 0 at this level, the tags apply to all editions in the Segment.
        /// If set to any other value, it** MUST** match the `EditionUID` value of an edition found in this Segment.</usage>
        /// </summary>
        public IEnumerable<ulong>? TagEditionUID { get; set; }
        /// <summary>
        /// A unique ID to identify the Chapter(s) the tags belong to.
        /// <usage>If the value is 0 at this level, the tags apply to all chapters in the Segment.
        /// If set to any other value, it **MUST** match the `ChapterUID` value of a chapter found in this Segment.</usage>
        /// </summary>
        public IEnumerable<ulong>? TagChapterUID { get; set; }
        /// <summary>
        /// A unique ID to identify the Attachment(s) the tags belong to.
        /// <usage>If the value is 0 at this level, the tags apply to all the attachments in the Segment.
        /// If set to any other value, it **MUST** match the `FileUID` value of an attachment found in this Segment.</usage>
        /// </summary>
        public IEnumerable<ulong>? TagAttachmentUID { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var targets = writer.StartMasterElement(Elements.Targets);
            targets.Write(Elements.TargetTypeValue, (ulong)TargetTypeValue);
            if(TargetType != null)
            {
                targets.WriteAscii(Elements.TargetType, TargetType.Type);
            }
            if(TagTrackUID != null)
            {
                foreach(var tagTrackUID in TagTrackUID)
                {
                    targets.Write(Elements.TagTrackUID, tagTrackUID);
                }            
            }
            if(TagEditionUID != null)
            {
                foreach(var tagEditionUID in TagEditionUID)
                {
                    targets.Write(Elements.TagEditionUID, tagEditionUID);
                }
            }
            if(TagChapterUID != null)
            {
                foreach(var tagChapterUID in TagChapterUID)
                {
                    targets.Write(Elements.TagChapterUID, tagChapterUID);
                }
            }
            if(TagAttachmentUID != null)
            {
                foreach(var tagAttachmentUID in TagAttachmentUID)
                {
                    targets.Write(Elements.TagAttachmentUID, tagAttachmentUID);
                }
            }
            targets.Dispose();
        }
    }
}
