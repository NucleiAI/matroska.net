using Matroska.Net.Segments.TracksElements.TrackEntryElements;
using NEbml.Core;
using System;
using System.Collections.Generic;

namespace Matroska.Net.Segments.TracksElements
{
    /// <summary>
    /// Describes a track with all Elements.
    /// </summary>
    public class TrackEntry
    {
        internal TrackEntry(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<BlockAdditionMapping>? blockAdditionMappings = null;
            List<string>? codecInfoUrl = null;
            List<string>? codecDownloadUrl = null;
            List<ulong>? trackOverlays = null;
            List<TrackTranslate>? trackTranslates = null;
            while (reader.ReadNext())
            {
                if (reader.ElementId == Elements.TrackNumber)
                {
                    TrackNumber = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.TrackUID)
                {
                    TrackUID = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.TrackType)
                {
                    TrackType = (TrackType)reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.FlagEnabled)
                {
                    FlagEnabled = reader.ReadUInt() != 0;
                }
                else if (reader.ElementId == Elements.FlagDefault)
                {
                    FlagDefault = reader.ReadUInt() != 0;
                }
                else if (reader.ElementId == Elements.FlagForced)
                {
                    FlagForced = reader.ReadUInt() == 1;
                }
                else if (reader.ElementId == Elements.FlagHearingImpaired)
                {
                    FlagHearingImpaired = reader.ReadUInt() == 1;
                }
                else if (reader.ElementId == Elements.FlagVisualImpaired)
                {
                    FlagVisualImpaired = reader.ReadUInt() == 1;
                }
                else if (reader.ElementId == Elements.FlagTextDescriptions)
                {
                    FlagTextDescriptions = reader.ReadUInt() == 1;
                }
                else if (reader.ElementId == Elements.FlagOriginal)
                {
                    FlagOriginal = reader.ReadUInt() == 1;
                }
                else if (reader.ElementId == Elements.FlagCommentary)
                {
                    FlagCommentary = reader.ReadUInt() == 1;
                }
                else if (reader.ElementId == Elements.FlagLacing)
                {
                    FlagLacing = reader.ReadUInt() != 0;
                }
                else if (reader.ElementId == Elements.MinCache)
                {
                    MinCache = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.MaxCache)
                {
                    MaxCache = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.DefaultDuration)
                {
                    DefaultDuration = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.DefaultDecodedFieldDuration)
                {
                    DefaultDecodedFieldDuration = reader.ReadUInt();
                }
                else if (version <= 3 && reader.ElementId == Elements.TrackTimestampScale)
                {
                    TrackTimestampScale = reader.ReadUInt();
                }
                else if (version == 0 && reader.ElementId == Elements.TrackOffset)
                {
                    TrackOffset = reader.ReadInt();
                }
                else if (reader.ElementId == Elements.MaxBlockAdditionID)
                {
                    MaxBlockAdditionID = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.BlockAdditionMapping)
                {
                    if (blockAdditionMappings == null)
                    {
                        blockAdditionMappings = new List<BlockAdditionMapping>();
                    }
                    blockAdditionMappings.Add(new BlockAdditionMapping(reader, version));
                }
                else if (reader.ElementId == Elements.Name)
                {
                    Name = reader.ReadUtf();
                }
                else if (reader.ElementId == Elements.Language)
                {
                    Language = reader.ReadAscii();
                }
                else if (reader.ElementId == Elements.LanguageIETF)
                {
                    LanguageIETF = reader.ReadAscii();
                }
                else if (reader.ElementId == Elements.CodecID)
                {
                    CodecID = reader.ReadAscii();
                }
                else if (reader.ElementId == Elements.CodecPrivate)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    CodecPrivate = new ArraySegment<byte>(data, 0, read);
                }
                else if (reader.ElementId == Elements.CodecName)
                {
                    CodecName = reader.ReadUtf();
                }
                else if (version <= 3 && reader.ElementId == Elements.AttachmentLink)
                {
                    AttachmentLink = reader.ReadUInt();
                }
                else if (version == 0 && reader.ElementId == Elements.CodecSettings)
                {
                    CodecSettings = reader.ReadUtf();
                }
                else if (version == 0 && reader.ElementId == Elements.CodecInfoURL)
                {
                    if (codecInfoUrl == null)
                    {
                        codecInfoUrl = new List<string>();
                    }
                    codecInfoUrl.Add(reader.ReadAscii());
                }
                else if (version == 0 && reader.ElementId == Elements.CodecDownloadURL)
                {
                    if (codecDownloadUrl == null)
                    {
                        codecDownloadUrl = new List<string>();
                    }
                    codecDownloadUrl.Add(reader.ReadAscii());
                }
                else if (version == 0 && reader.ElementId == Elements.CodecDecodeAll)
                {
                    CodecDecodeAll = reader.ReadUInt() != 0;
                }
                else if (reader.ElementId == Elements.TrackOverlay)
                {
                    if (trackOverlays == null)
                    {
                        trackOverlays = new List<ulong>();
                    }
                    trackOverlays.Add(reader.ReadUInt());
                }
                else if (reader.ElementId == Elements.CodecDelay)
                {
                    CodecDelay = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.SeekPreRoll)
                {
                    SeekPreRoll = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.TrackTranslate)
                {
                    if (trackTranslates == null)
                    {
                        trackTranslates = new List<TrackTranslate>();
                    }
                    trackTranslates.Add(new TrackTranslate(reader, version));
                }
                else if (reader.ElementId == Elements.Video)
                {
                    Video = new Video(reader, version);
                }
                else if (reader.ElementId == Elements.Audio)
                {
                    Audio = new Audio(reader, version);
                }
                else if (reader.ElementId == Elements.TrackOperation)
                {
                    TrackOperation = new TrackOperation(reader, version);
                }
                else if (version == 0 && reader.ElementId == Elements.TrickTrackUID)
                {
                    TrickTrackUID = reader.ReadUInt();
                }
                else if (version == 0 && reader.ElementId == Elements.TrickTrackSegmentUID)
                {
                    var data = new byte[16];
                    reader.ReadBinary(data, 0, data.Length);
                    TrickTrackSegmentUID = new Guid(data);
                }
                else if (version == 0 && reader.ElementId == Elements.TrickTrackFlag)
                {
                    TrickTrackFlag = reader.ReadUInt();
                }
                else if (version == 0 && reader.ElementId == Elements.TrickMasterTrackUID)
                {
                    TrickMasterTrackUID = reader.ReadUInt();
                }
                else if (version == 0 && reader.ElementId == Elements.TrickMasterTrackSegmentUID)
                {
                    var data = new byte[16];
                    reader.ReadBinary(data, 0, 16);
                    TrickMasterTrackSegmentUID = new Guid(data);
                }
                else if (reader.ElementId == Elements.ContentEncodings)
                {
                    ContentEncodings = new ContentEncodings(reader, version);
                }
            }
            TrackTranslates = trackTranslates;
            TrackOverlay = trackOverlays;
            CodecDownloadURL = codecDownloadUrl;
            CodecInfoURL = codecInfoUrl;
            BlockAdditionMappings = blockAdditionMappings;
            reader.LeaveContainer();
        }
        public TrackEntry(
            ulong trackNumber,
            ulong trackUID,
            TrackType trackType,
            bool flagLacing,
            string codecID,
            bool flagEnabled = true,
            bool flagDefault = true,
            bool flagForced = false,
            bool? flagHearingImpaired = null,
            bool? flagVisualImpaired = null,
            bool? flagTextDescriptions = null,
            bool? flagOriginal = null,
            bool? flagCommentary = null,
            ulong minCache = 0,
            ulong? maxCache = null,
            ulong? defaultDuration = null,
            ulong? defaultDecodedFieldDuration = null,
            ulong maxBlockAdditionID = 0,
            IEnumerable<BlockAdditionMapping>? blockAdditionMappings = null,
            string? name = null,
            string language = "und",
            string? languageIETF = null,
            ArraySegment<byte>? codecPrivate = null,
            string? codecName = null,
            IEnumerable<ulong>? trackOverlay = null,
            ulong codecDelay = 0,
            ulong seekPreRoll = 0,
            IEnumerable<TrackTranslate>? trackTranslates = null,
            Video? video = null,
            Audio? audio = null,
            TrackOperation? trackOperation = null,
            ContentEncodings? contentEncodings = null)
        {
            TrackNumber = trackNumber;
            TrackUID = trackUID;
            TrackType = trackType;
            FlagEnabled = flagEnabled;
            FlagDefault = flagDefault;
            FlagForced = flagForced;
            FlagHearingImpaired = flagHearingImpaired;
            FlagVisualImpaired = flagVisualImpaired;
            FlagTextDescriptions = flagTextDescriptions;
            FlagOriginal = flagOriginal;
            FlagCommentary = flagCommentary;
            FlagLacing = flagLacing;
            MinCache = minCache;
            MaxCache = maxCache;
            DefaultDuration = defaultDuration;
            DefaultDecodedFieldDuration = defaultDecodedFieldDuration;
            MaxBlockAdditionID = maxBlockAdditionID;
            BlockAdditionMappings = blockAdditionMappings;
            Name = name;
            Language = language;
            LanguageIETF = languageIETF;
            CodecID = codecID;
            CodecPrivate = codecPrivate;
            CodecName = codecName;
            TrackOverlay = trackOverlay;
            CodecDelay = codecDelay;
            SeekPreRoll = seekPreRoll;
            TrackTranslates = trackTranslates;
            Video = video;
            Audio = audio;
            TrackOperation = trackOperation;
            ContentEncodings = contentEncodings;

        }
        /// <summary>
        /// The track number as used in the Block Header (using more than 127 tracks is not encouraged,
        /// though the design allows an unlimited number).
        /// </summary>
        public ulong TrackNumber { get; set; }
        /// <summary>
        /// A unique ID to identify the Track.
        /// </summary>
        public ulong TrackUID { get; set; }
        /// <summary>
        /// The `TrackType` defines the type of each frame found in the Track.
        /// The value **SHOULD** be stored on 1 octet.
        /// </summary>
        public TrackType TrackType { get; set; }
        /// <summary>
        /// Set to 1 if the track is usable. It is possible to turn a not usable track into a usable track using chapter codecs or control tracks.
        /// </summary>
        public bool FlagEnabled { get; set; }
        /// <summary>
        /// Set if that track (audio, video or subs) **SHOULD** be eligible for automatic selection by the player; see (#default-track-selection) for more details.
        /// </summary>
        public bool FlagDefault { get; set; }
        /// <summary>
        /// Applies only to subtitles. Set if that track **SHOULD** be eligible for automatic selection by the player if it matches the user's language preference,
        /// even if the user's preferences would normally not enable subtitles with the selected audio track;
        /// this can be used for tracks containing only translations of foreign-language audio or onscreen text.
        /// See(#default-track-selection) for more details.
        /// </summary>
        public bool FlagForced { get; set; }
        /// <summary>
        /// Set to 1 if that track is suitable for users with hearing impairments, set to 0 if it is unsuitable for users with hearing impairments.
        /// </summary>
        public bool? FlagHearingImpaired { get; set; }
        /// <summary>
        /// Set to 1 if that track is suitable for users with visual impairments, set to 0 if it is unsuitable for users with visual impairments.
        /// </summary>
        public bool? FlagVisualImpaired { get; set; }
        /// <summary>
        /// Set to 1 if that track contains textual descriptions of video content, set to 0 if that track does not contain textual descriptions of video content.
        /// </summary>
        public bool? FlagTextDescriptions { get; set; }
        /// <summary>
        /// Set to 1 if that track is in the content's original language, set to 0 if it is a translation.
        /// </summary>
        public bool? FlagOriginal { get; set; }
        /// <summary>
        /// Set to 1 if that track contains commentary, set to 0 if it does not contain commentary.
        /// </summary>
        public bool? FlagCommentary { get; set; }
        /// <summary>
        /// Set to 1 if the track **MAY** contain blocks using lacing. When set to 0 all blocks **MUST** have their lacing flags set to No lacing; see (#block-lacing) on Block Lacing.
        /// </summary>
        public bool FlagLacing { get; set; }
        /// <summary>
        /// he minimum number of frames a player **SHOULD** be able to cache during playback.
        /// If set to 0, the reference pseudo-cache system is not used.
        /// </summary>
        public ulong MinCache { get; set; }
        /// <summary>
        /// The maximum cache size necessary to store referenced frames in and the current frame.
        /// 0 means no cache is needed.
        /// </summary>
        public ulong? MaxCache { get; set; }
        /// <summary>
        /// Number of nanoseconds per frame, expressed in Matroska Ticks -- ie in nanoseconds; see (#timestamp-ticks)
        /// (frame in the Matroska sense -- one Element put into a(Simple)Block).
        /// </summary>
        public ulong? DefaultDuration { get; set; }
        /// <summary>
        /// The period between two successive fields at the output of the decoding process, expressed in Matroska Ticks -- ie in nanoseconds; see (#timestamp-ticks).
        /// see(#defaultdecodedfieldduration) for more information
        /// </summary>
        public ulong? DefaultDecodedFieldDuration { get; set; }
        /// <summary>
        /// DEPRECATED, DO NOT USE. The scale to apply on this track to work at normal speed in relation with other tracks
        /// (mostly used to adjust video speed when the audio length differs).
        /// </summary>
        [Obsolete("Track Timestamp Scale is since version 4 of matroska obsolete and should not be used")]
        public double? TrackTimestampScale { get; set; }
        /// <summary>
        /// A value to add to the Block's Timestamp, expressed in Matroska Ticks -- ie in nanoseconds; see (#timestamp-ticks).
        /// This can be used to adjust the playback offset of a track.
        /// </summary>
        [Obsolete("TrackOffset is since version 1 of matroska obsolete and should not be used")]
        public long? TrackOffset { get; set; }
        /// <summary>
        /// The maximum value of BlockAddID ((#blockaddid-element)).
        /// A value 0 means there is no BlockAdditions((#blockadditions-element)) for this track.
        /// </summary>
        public ulong MaxBlockAdditionID { get; set; }
        /// <summary>
        /// Contains elements that extend the track format, by adding content either to each frame,
        /// with BlockAddID((#blockaddid-element)), or to the track as a whole
        /// with BlockAddIDExtraData.
        /// </summary>
        public IEnumerable<BlockAdditionMapping>? BlockAdditionMappings { get; set; }
        /// <summary>
        /// A human-readable track name.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Specifies the language of the track in the Matroska languages form;
        /// see(#language-codes) on language codes.
        /// This Element **MUST** be ignored if the LanguageIETF Element is used in the same TrackEntry.
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Specifies the language of the track according to [@!BCP47]
        /// and using the IANA Language Subtag Registry[@!IANALangRegistry].
        /// If this Element is used, then any Language Elements used in the same TrackEntry** MUST** be ignored.
        /// </summary>
        public string? LanguageIETF { get; set; }
        /// <summary>
        /// An ID corresponding to the codec,
        /// see[@!MatroskaCodec] for more info.
        /// </summary>
        public string CodecID { get; set; }
        /// <summary>
        /// Private data only known to the codec.
        /// </summary>
        public ArraySegment<byte>? CodecPrivate { get; set; }
        /// <summary>
        /// A human-readable string specifying the codec.
        /// </summary>
        public string? CodecName { get; set; }
        /// <summary>
        /// The UID of an attachment that is used by this codec.
        /// <usage>The value **MUST** match the `FileUID` value of an attachment found in this Segment.</usage>
        /// </summary>
        [Obsolete("TrackOffset is since version 4 of matroska obsolete and should not be used")]
        public ulong? AttachmentLink { get; set; }
        /// <summary>
        /// A string describing the encoding setting used.
        /// </summary>
        [Obsolete("CodecSettings is since version 1 of matroska obsolete and should not be used")]
        public string? CodecSettings { get; set; }
        /// <summary>
        /// A URL to find information about the codec used.
        /// </summary>
        [Obsolete("CodecInfoURL is since version 1 of matroska obsolete and should not be used")]
        public IEnumerable<string>? CodecInfoURL { get; set; }
        /// <summary>
        /// A URL to download about the codec used.
        /// </summary>
        [Obsolete("CodecDownloadURL is since version 1 of matroska obsolete and should not be used")]
        public IEnumerable<string>? CodecDownloadURL { get; set; }
        /// <summary>
        /// Set to 1 if the codec can decode potentially damaged data.
        /// </summary>
        [Obsolete("CodecDecodeAll is since version 1 of matroska obsolete and should not be used")]
        public bool? CodecDecodeAll { get; set; }
        /// <summary>
        /// Specify that this track is an overlay track for the Track specified (in the u-integer).
        /// That means when this track has a gap, see (#silenttracks-element) on SilentTracks,
        /// the overlay track **SHOULD** be used instead. The order of multiple TrackOverlay matters, the first one is the one that** SHOULD** be used.
        /// If not found it **SHOULD** be the second, etc.
        /// </summary>
        public IEnumerable<ulong>? TrackOverlay { get; set; }
        /// <summary>
        /// CodecDelay is The codec-built-in delay, expressed in Matroska Ticks -- ie in nanoseconds; see (#timestamp-ticks).
        /// It represents the amount of codec samples that will be discarded by the decoder during playback.
        /// This timestamp value **MUST** be subtracted from each frame timestamp in order to get the timestamp that will be actually played.
        /// The value **SHOULD** be small so the muxing of tracks with the same actual timestamp are in the same Cluster.
        /// </summary>
        public ulong CodecDelay { get; set; }
        /// <summary>
        /// After a discontinuity, SeekPreRoll is the duration of the data
        /// the decoder **MUST** decode before the decoded data is valid, expressed in Matroska Ticks -- ie in nanoseconds; see(#timestamp-ticks).
        /// </summary>
        public ulong SeekPreRoll { get; set; }
        /// <summary>
        /// The mapping between this `TrackEntry` and a track value in the given Chapter Codec.
        /// <rationale>Chapter Codec may need to address content in specific track, but they may not know of the way to identify tracks in Matroska.
        /// This element and its child elements add a way to map the internal tracks known to the Chapter Codec to the track IDs in Matroska.
        /// This allows remuxing a file with Chapter Codec without changing the content of the codec data, just the track mapping.</rationale>
        /// </summary>
        public IEnumerable<TrackTranslate>? TrackTranslates { get; set; }
        /// <summary>
        /// Video settings.
        /// </summary>
        public Video? Video { get; set; }
        /// <summary>
        /// Audio settings.
        /// </summary>
        public Audio? Audio { get; set; }
        /// <summary>
        /// Operation that needs to be applied on tracks to create this virtual track.
        /// For more details look at(#track-operation).
        /// </summary>
        public TrackOperation? TrackOperation { get; set; }
        /// <summary>
        /// The TrackUID of the Smooth FF/RW video in the paired EBML structure corresponding to this video track. See [@?DivXTrickTrack].
        /// </summary>
        [Obsolete("TrickTrackUID is since version 1 of matroska obsolete and should not be used")]
        public ulong? TrickTrackUID { get; set; }
        /// <summary>
        /// The SegmentUID of the Segment containing the track identified by TrickTrackUID. See [@?DivXTrickTrack].
        /// </summary>
        [Obsolete("TrickTrackSegmentUID is since version 1 of matroska obsolete and should not be used")]
        public Guid? TrickTrackSegmentUID { get; set; }
        /// <summary>
        /// Set to 1 if this video track is a Smooth FF/RW track. If set to 1, MasterTrackUID and MasterTrackSegUID should must be present and BlockGroups for this track must contain ReferenceFrame structures.
        /// Otherwise, TrickTrackUID and TrickTrackSegUID must be present if this track has a corresponding Smooth FF/RW track.See [@?DivXTrickTrack].
        /// </summary>
        [Obsolete("TrickTrackSegmentUID is since version 1 of matroska obsolete and should not be used")]
        public ulong? TrickTrackFlag { get; set; }
        /// <summary>
        /// The TrackUID of the video track in the paired EBML structure that corresponds to this Smooth FF/RW track. See [@?DivXTrickTrack].
        /// </summary>
        [Obsolete("TrickMasterTrackUID is since version 1 of matroska obsolete and should not be used")]
        public ulong? TrickMasterTrackUID { get; set; }
        /// <summary>
        /// The SegmentUID of the Segment containing the track identified by MasterTrackUID. See [@?DivXTrickTrack].
        /// </summary>
        [Obsolete("TrickMasterTrackSegmentUID is since version 1 of matroska obsolete and should not be used")]
        public Guid? TrickMasterTrackSegmentUID { get; set; }
        /// <summary>
        /// Settings for several content encoding mechanisms like compression or encryption.
        /// </summary>
        public ContentEncodings? ContentEncodings { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var trackEntry = writer.StartMasterElement(Elements.TrackEntry);
            trackEntry.Write(Elements.TrackNumber, TrackNumber);
            trackEntry.Write(Elements.TrackUID, TrackUID);
            trackEntry.Write(Elements.TrackType, (ulong)TrackType);
            trackEntry.Write(Elements.FlagEnabled, (ulong)(FlagEnabled ? 1 : 0));
            trackEntry.Write(Elements.FlagDefault, (ulong)(FlagDefault ? 1 : 0));
            trackEntry.Write(Elements.FlagForced, (ulong)(FlagForced ? 1 : 0));
            if (FlagHearingImpaired.HasValue)
            {
                trackEntry.Write(Elements.FlagHearingImpaired, (ulong)(FlagHearingImpaired.Value ? 1 : 0));
            }
            if (FlagVisualImpaired.HasValue)
            {
                trackEntry.Write(Elements.FlagVisualImpaired, (ulong)(FlagVisualImpaired.Value ? 1 : 0));
            }
            if (FlagTextDescriptions.HasValue)
            {
                trackEntry.Write(Elements.FlagTextDescriptions, (ulong)(FlagTextDescriptions.Value ? 1 : 0));
            }
            if (FlagOriginal.HasValue)
            {
                trackEntry.Write(Elements.FlagOriginal, (ulong)(FlagOriginal.Value ? 1 : 0));
            }
            if (FlagCommentary.HasValue)
            {
                trackEntry.Write(Elements.FlagCommentary, (ulong)(FlagCommentary.Value ? 1 : 0));
            }
            trackEntry.Write(Elements.FlagLacing, (ulong)(FlagLacing ? 1 : 0));
            trackEntry.Write(Elements.MinCache, MinCache);
            if (MaxCache.HasValue)
            {
                trackEntry.Write(Elements.MaxCache, MaxCache.Value);
            }
            if (DefaultDuration.HasValue)
            {
                trackEntry.Write(Elements.DefaultDuration, DefaultDuration.Value);
            }
            if (DefaultDecodedFieldDuration.HasValue)
            {
                trackEntry.Write(Elements.DefaultDecodedFieldDuration, DefaultDecodedFieldDuration.Value);
            }
            trackEntry.Write(Elements.MaxBlockAdditionID, MaxBlockAdditionID);
            if(BlockAdditionMappings != null)
            {
                foreach (var mapping in BlockAdditionMappings)
                {
                    mapping.WriteTo(trackEntry);
                }
            }
            if(Name != null)
            {
                trackEntry.WriteUtf(Elements.Name, Name);
            }
            trackEntry.WriteAscii(Elements.Language, Language);
            if(LanguageIETF != null)
            {
                trackEntry.WriteAscii(Elements.LanguageIETF, LanguageIETF);
            }
            trackEntry.WriteAscii(Elements.CodecID, CodecID);
            if (CodecPrivate.HasValue)
            {
                trackEntry.Write(Elements.CodecPrivate, CodecPrivate.Value.Array, CodecPrivate.Value.Offset, CodecPrivate.Value.Count);
            }
            if(CodecName != null)
            {
                trackEntry.WriteUtf(Elements.CodecName, CodecName);
            }
            if(TrackOverlay != null)
            {
                foreach(var overlay in TrackOverlay)
                {
                    trackEntry.Write(Elements.TrackOverlay, overlay);
                }
            }
            trackEntry.Write(Elements.CodecDelay, CodecDelay);
            trackEntry.Write(Elements.SeekPreRoll, SeekPreRoll);
            if(TrackTranslates != null)
            {
                foreach(var translate in TrackTranslates)
                {
                    translate.WriteTo(trackEntry);
                }
            }
            if(Video != null)
            {
                Video.WriteTo(trackEntry);
            }
            if(Audio != null)
            {
                Audio.WriteTo(trackEntry);
            }
            if(TrackOperation != null)
            {
                TrackOperation.WriteTo(trackEntry);
            }
            if(ContentEncodings != null)
            {
                ContentEncodings.WriteTo(trackEntry);
            }
            trackEntry.Dispose();
        }
    }
}
