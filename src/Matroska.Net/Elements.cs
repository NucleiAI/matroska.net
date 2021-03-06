using NEbml.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matroska.Net
{
    internal static class Elements
    {
        internal static VInt EBMLHeader = VInt.FromEncoded(0x1A45DFA3);
        internal static VInt EBMLVersion = VInt.FromEncoded(0x4286);
        internal static VInt EBMLReadVersion = VInt.FromEncoded(0x42F7);
        internal static VInt EBMLMaxIDLength = VInt.FromEncoded(0x42F2);
        internal static VInt EBMLMaxSizeLength = VInt.FromEncoded(0x42F3);
        internal static VInt DocType = VInt.FromEncoded(0x4282);
        internal static VInt DocTypeVersion = VInt.FromEncoded(0x4287);
        internal static VInt DocTypeReadVersion = VInt.FromEncoded(0x4285);
        internal static VInt DocTypeExtension = VInt.FromEncoded(0x4281);
        internal static VInt DocTypeExtensionName = VInt.FromEncoded(0x4283);
        internal static VInt DocTypeExtensionVersion = VInt.FromEncoded(0x4284);
        internal static VInt CRC32 = VInt.FromEncoded(0xBF);
        internal static VInt Void = VInt.FromEncoded(0xEC);
        internal static VInt Segment = VInt.FromEncoded(0x18538067);
        internal static VInt SeekHead = VInt.FromEncoded(0x114D9B74);
        internal static VInt Seek = VInt.FromEncoded(0x4DBB);
        internal static VInt SeekID = VInt.FromEncoded(0x53AB);
        internal static VInt SeekPosition = VInt.FromEncoded(0x53AC);
        internal static VInt Info = VInt.FromEncoded(0x1549A966);
        internal static VInt SegmentUID = VInt.FromEncoded(0x73A4);
        internal static VInt SegmentFilename = VInt.FromEncoded(0x7384);
        internal static VInt PrevUID = VInt.FromEncoded(0x3CB923);
        internal static VInt PrevFilename = VInt.FromEncoded(0x3C83AB);
        internal static VInt NextUID = VInt.FromEncoded(0x3EB923);
        internal static VInt NextFilename = VInt.FromEncoded(0x3E83BB);
        internal static VInt SegmentFamily = VInt.FromEncoded(0x4444);
        internal static VInt ChapterTranslate = VInt.FromEncoded(0x6924);
        internal static VInt ChapterTranslateID = VInt.FromEncoded(0x69A5);
        internal static VInt ChapterTranslateCodec = VInt.FromEncoded(0x69BF);
        internal static VInt ChapterTranslateEditionUID = VInt.FromEncoded(0x69FC);
        internal static VInt TimestampScale = VInt.FromEncoded(0x2AD7B1);
        internal static VInt Duration = VInt.FromEncoded(0x4489);
        internal static VInt DateUTC = VInt.FromEncoded(0x4461);
        internal static VInt Title = VInt.FromEncoded(0x7BA9);
        internal static VInt MuxingApp = VInt.FromEncoded(0x4D80);
        internal static VInt WritingApp = VInt.FromEncoded(0x5741);
        internal static VInt Cluster = VInt.FromEncoded(0x1F43B675);
        internal static VInt Timestamp = VInt.FromEncoded(0xE7);
        internal static VInt SilentTracks = VInt.FromEncoded(0x5854);
        internal static VInt SilentTrackNumber = VInt.FromEncoded(0x58D7);
        internal static VInt Position = VInt.FromEncoded(0xA7);
        internal static VInt PrevSize = VInt.FromEncoded(0xAB);
        internal static VInt SimpleBlock = VInt.FromEncoded(0xA3);
        internal static VInt BlockGroup = VInt.FromEncoded(0xA0);
        internal static VInt Block = VInt.FromEncoded(0xA1);
        internal static VInt BlockVirtual = VInt.FromEncoded(0xA2);
        internal static VInt BlockAdditions = VInt.FromEncoded(0x75A1);
        internal static VInt BlockMore = VInt.FromEncoded(0xA6);
        internal static VInt BlockAddID = VInt.FromEncoded(0xEE);
        internal static VInt BlockAdditional = VInt.FromEncoded(0xA5);
        internal static VInt BlockDuration = VInt.FromEncoded(0x9B);
        internal static VInt ReferencePriority = VInt.FromEncoded(0xFA);
        internal static VInt ReferenceBlock = VInt.FromEncoded(0xFB);
        internal static VInt ReferenceVirtual = VInt.FromEncoded(0xFD);
        internal static VInt CodecState = VInt.FromEncoded(0xA4);
        internal static VInt DiscardPadding = VInt.FromEncoded(0x75A2);
        internal static VInt Slices = VInt.FromEncoded(0x8E);
        internal static VInt TimeSlice = VInt.FromEncoded(0xE8);
        internal static VInt LaceNumber = VInt.FromEncoded(0xCC);
        internal static VInt FrameNumber = VInt.FromEncoded(0xCD);
        internal static VInt BlockAdditionID = VInt.FromEncoded(0xCB);
        internal static VInt Delay = VInt.FromEncoded(0xCE);
        internal static VInt SliceDuration = VInt.FromEncoded(0xCF);
        internal static VInt ReferenceFrame = VInt.FromEncoded(0xC8);
        internal static VInt ReferenceOffset = VInt.FromEncoded(0xC9);
        internal static VInt ReferenceTimestamp = VInt.FromEncoded(0xCA);
        internal static VInt EncryptedBlock = VInt.FromEncoded(0xAF);
        internal static VInt Tracks = VInt.FromEncoded(0x1654AE6B);
        internal static VInt TrackEntry = VInt.FromEncoded(0xAE);
        internal static VInt TrackNumber = VInt.FromEncoded(0xD7);
        internal static VInt TrackUID = VInt.FromEncoded(0x73C5);
        internal static VInt TrackType = VInt.FromEncoded(0x83);
        internal static VInt FlagEnabled = VInt.FromEncoded(0xB9);
        internal static VInt FlagDefault = VInt.FromEncoded(0x88);
        internal static VInt FlagForced = VInt.FromEncoded(0x55AA);
        internal static VInt FlagHearingImpaired = VInt.FromEncoded(0x55AB);
        internal static VInt FlagVisualImpaired = VInt.FromEncoded(0x55AC);
        internal static VInt FlagTextDescriptions = VInt.FromEncoded(0x55AD);
        internal static VInt FlagOriginal = VInt.FromEncoded(0x55AE);
        internal static VInt FlagCommentary = VInt.FromEncoded(0x55AF);
        internal static VInt FlagLacing = VInt.FromEncoded(0x9C);
        internal static VInt MinCache = VInt.FromEncoded(0x6DE7);
        internal static VInt MaxCache = VInt.FromEncoded(0x6DF8);
        internal static VInt DefaultDuration = VInt.FromEncoded(0x23E383);
        internal static VInt DefaultDecodedFieldDuration = VInt.FromEncoded(0x234E7A);
        internal static VInt TrackTimestampScale = VInt.FromEncoded(0x23314F);
        internal static VInt TrackOffset = VInt.FromEncoded(0x537F);
        internal static VInt MaxBlockAdditionID = VInt.FromEncoded(0x55EE);
        internal static VInt BlockAdditionMapping = VInt.FromEncoded(0x41E4);
        internal static VInt BlockAddIDValue = VInt.FromEncoded(0x41F0);
        internal static VInt BlockAddIDName = VInt.FromEncoded(0x41A4);
        internal static VInt BlockAddIDType = VInt.FromEncoded(0x41E7);
        internal static VInt BlockAddIDExtraData = VInt.FromEncoded(0x41ED);
        internal static VInt Name = VInt.FromEncoded(0x536E);
        internal static VInt Language = VInt.FromEncoded(0x22B59C);
        internal static VInt LanguageIETF = VInt.FromEncoded(0x22B59D);
        internal static VInt CodecID = VInt.FromEncoded(0x86);
        internal static VInt CodecPrivate = VInt.FromEncoded(0x63A2);
        internal static VInt CodecName = VInt.FromEncoded(0x258688);
        internal static VInt AttachmentLink = VInt.FromEncoded(0x7446);
        internal static VInt CodecSettings = VInt.FromEncoded(0x3A9697);
        internal static VInt CodecInfoURL = VInt.FromEncoded(0x3B4040);
        internal static VInt CodecDownloadURL = VInt.FromEncoded(0x26B240);
        internal static VInt CodecDecodeAll = VInt.FromEncoded(0xAA);
        internal static VInt TrackOverlay = VInt.FromEncoded(0x6FAB);
        internal static VInt CodecDelay = VInt.FromEncoded(0x56AA);
        internal static VInt SeekPreRoll = VInt.FromEncoded(0x56BB);
        internal static VInt TrackTranslate = VInt.FromEncoded(0x6624);
        internal static VInt TrackTranslateTrackID = VInt.FromEncoded(0x66A5);
        internal static VInt TrackTranslateCodec = VInt.FromEncoded(0x66BF);
        internal static VInt TrackTranslateEditionUID = VInt.FromEncoded(0x66FC);
        internal static VInt Video = VInt.FromEncoded(0xE0);
        internal static VInt FlagInterlaced = VInt.FromEncoded(0x9A);
        internal static VInt FieldOrder = VInt.FromEncoded(0x9D);
        internal static VInt StereoMode = VInt.FromEncoded(0x53B8);
        internal static VInt AlphaMode = VInt.FromEncoded(0x53C0);
        internal static VInt OldStereoMode = VInt.FromEncoded(0x53B9);
        internal static VInt PixelWidth = VInt.FromEncoded(0xB0);
        internal static VInt PixelHeight = VInt.FromEncoded(0xBA);
        internal static VInt PixelCropBottom = VInt.FromEncoded(0x54AA);
        internal static VInt PixelCropTop = VInt.FromEncoded(0x54BB);
        internal static VInt PixelCropLeft = VInt.FromEncoded(0x54CC);
        internal static VInt PixelCropRight = VInt.FromEncoded(0x54DD);
        internal static VInt DisplayWidth = VInt.FromEncoded(0x54B0);
        internal static VInt DisplayHeight = VInt.FromEncoded(0x54BA);
        internal static VInt DisplayUnit = VInt.FromEncoded(0x54B2);
        internal static VInt AspectRatioType = VInt.FromEncoded(0x54B3);
        internal static VInt UncompressedFourCC = VInt.FromEncoded(0x2EB524);
        internal static VInt GammaValue = VInt.FromEncoded(0x2FB523);
        internal static VInt FrameRate = VInt.FromEncoded(0x2383E3);
        internal static VInt Colour = VInt.FromEncoded(0x55B0);
        internal static VInt MatrixCoefficients = VInt.FromEncoded(0x55B1);
        internal static VInt BitsPerChannel = VInt.FromEncoded(0x55B2);
        internal static VInt ChromaSubsamplingHorz = VInt.FromEncoded(0x55B3);
        internal static VInt ChromaSubsamplingVert = VInt.FromEncoded(0x55B4);
        internal static VInt CbSubsamplingHorz = VInt.FromEncoded(0x55B5);
        internal static VInt CbSubsamplingVert = VInt.FromEncoded(0x55B6);
        internal static VInt ChromaSitingHorz = VInt.FromEncoded(0x55B7);
        internal static VInt ChromaSitingVert = VInt.FromEncoded(0x55B8);
        internal static VInt Range = VInt.FromEncoded(0x55B9);
        internal static VInt TransferCharacteristics = VInt.FromEncoded(0x55BA);
        internal static VInt Primaries = VInt.FromEncoded(0x55BB);
        internal static VInt MaxCLL = VInt.FromEncoded(0x55BC);
        internal static VInt MaxFALL = VInt.FromEncoded(0x55BD);
        internal static VInt MasteringMetadata = VInt.FromEncoded(0x55D0);
        internal static VInt PrimaryRChromaticityX = VInt.FromEncoded(0x55D1);
        internal static VInt PrimaryRChromaticityY = VInt.FromEncoded(0x55D2);
        internal static VInt PrimaryGChromaticityX = VInt.FromEncoded(0x55D3);
        internal static VInt PrimaryGChromaticityY = VInt.FromEncoded(0x55D4);
        internal static VInt PrimaryBChromaticityX = VInt.FromEncoded(0x55D5);
        internal static VInt PrimaryBChromaticityY = VInt.FromEncoded(0x55D6);
        internal static VInt WhitePointChromaticityX = VInt.FromEncoded(0x55D7);
        internal static VInt WhitePointChromaticityY = VInt.FromEncoded(0x55D8);
        internal static VInt LuminanceMax = VInt.FromEncoded(0x55D9);
        internal static VInt LuminanceMin = VInt.FromEncoded(0x55DA);
        internal static VInt Projection = VInt.FromEncoded(0x7670);
        internal static VInt ProjectionType = VInt.FromEncoded(0x7671);
        internal static VInt ProjectionPrivate = VInt.FromEncoded(0x7672);
        internal static VInt ProjectionPoseYaw = VInt.FromEncoded(0x7673);
        internal static VInt ProjectionPosePitch = VInt.FromEncoded(0x7674);
        internal static VInt ProjectionPoseRoll = VInt.FromEncoded(0x7675);
        internal static VInt Audio = VInt.FromEncoded(0xE1);
        internal static VInt SamplingFrequency = VInt.FromEncoded(0xB5);
        internal static VInt OutputSamplingFrequency = VInt.FromEncoded(0x78B5);
        internal static VInt Channels = VInt.FromEncoded(0x9F);
        internal static VInt ChannelPositions = VInt.FromEncoded(0x7D7B);
        internal static VInt BitDepth = VInt.FromEncoded(0x6264);
        internal static VInt TrackOperation = VInt.FromEncoded(0xE2);
        internal static VInt TrackCombinePlanes = VInt.FromEncoded(0xE3);
        internal static VInt TrackPlane = VInt.FromEncoded(0xE4);
        internal static VInt TrackPlaneUID = VInt.FromEncoded(0xE5);
        internal static VInt TrackPlaneType = VInt.FromEncoded(0xE6);
        internal static VInt TrackJoinBlocks = VInt.FromEncoded(0xE9);
        internal static VInt TrackJoinUID = VInt.FromEncoded(0xED);
        internal static VInt TrickTrackUID = VInt.FromEncoded(0xC0);
        internal static VInt TrickTrackSegmentUID = VInt.FromEncoded(0xC1);
        internal static VInt TrickTrackFlag = VInt.FromEncoded(0xC6);
        internal static VInt TrickMasterTrackUID = VInt.FromEncoded(0xC7);
        internal static VInt TrickMasterTrackSegmentUID = VInt.FromEncoded(0xC4);
        internal static VInt ContentEncodings = VInt.FromEncoded(0x6D80);
        internal static VInt ContentEncoding = VInt.FromEncoded(0x6240);
        internal static VInt ContentEncodingOrder = VInt.FromEncoded(0x5031);
        internal static VInt ContentEncodingScope = VInt.FromEncoded(0x5032);
        internal static VInt ContentEncodingType = VInt.FromEncoded(0x5033);
        internal static VInt ContentCompression = VInt.FromEncoded(0x5034);
        internal static VInt ContentCompAlgo = VInt.FromEncoded(0x4254);
        internal static VInt ContentCompSettings = VInt.FromEncoded(0x4255);
        internal static VInt ContentEncryption = VInt.FromEncoded(0x5035);
        internal static VInt ContentEncAlgo = VInt.FromEncoded(0x47E1);
        internal static VInt ContentEncKeyID = VInt.FromEncoded(0x47E2);
        internal static VInt ContentEncAESSettings = VInt.FromEncoded(0x47E7);
        internal static VInt AESSettingsCipherMode = VInt.FromEncoded(0x47E8);
        internal static VInt ContentSignature = VInt.FromEncoded(0x47E3);
        internal static VInt ContentSigKeyID = VInt.FromEncoded(0x47E4);
        internal static VInt ContentSigAlgo = VInt.FromEncoded(0x47E5);
        internal static VInt ContentSigHashAlgo = VInt.FromEncoded(0x47E6);
        internal static VInt Cues = VInt.FromEncoded(0x1C53BB6B);
        internal static VInt CuePoint = VInt.FromEncoded(0xBB);
        internal static VInt CueTime = VInt.FromEncoded(0xB3);
        internal static VInt CueTrackPositions = VInt.FromEncoded(0xB7);
        internal static VInt CueTrack = VInt.FromEncoded(0xF7);
        internal static VInt CueClusterPosition = VInt.FromEncoded(0xF1);
        internal static VInt CueRelativePosition = VInt.FromEncoded(0xF0);
        internal static VInt CueDuration = VInt.FromEncoded(0xB2);
        internal static VInt CueBlockNumber = VInt.FromEncoded(0x5378);
        internal static VInt CueCodecState = VInt.FromEncoded(0xEA);
        internal static VInt CueReference = VInt.FromEncoded(0xDB);
        internal static VInt CueRefTime = VInt.FromEncoded(0x96);
        internal static VInt CueRefCluster = VInt.FromEncoded(0x97);
        internal static VInt CueRefNumber = VInt.FromEncoded(0x535F);
        internal static VInt CueRefCodecState = VInt.FromEncoded(0xEB);
        internal static VInt Attachments = VInt.FromEncoded(0x1941A469);
        internal static VInt AttachedFile = VInt.FromEncoded(0x61A7);
        internal static VInt FileDescription = VInt.FromEncoded(0x467E);
        internal static VInt FileName = VInt.FromEncoded(0x466E);
        internal static VInt FileMimeType = VInt.FromEncoded(0x4660);
        internal static VInt FileData = VInt.FromEncoded(0x465C);
        internal static VInt FileUID = VInt.FromEncoded(0x46AE);
        internal static VInt FileReferral = VInt.FromEncoded(0x4675);
        internal static VInt FileUsedStartTime = VInt.FromEncoded(0x4661);
        internal static VInt FileUsedEndTime = VInt.FromEncoded(0x4662);
        internal static VInt Chapters = VInt.FromEncoded(0x1043A770);
        internal static VInt EditionEntry = VInt.FromEncoded(0x45B9);
        internal static VInt EditionUID = VInt.FromEncoded(0x45BC);
        internal static VInt EditionFlagHidden = VInt.FromEncoded(0x45BD);
        internal static VInt EditionFlagDefault = VInt.FromEncoded(0x45DB);
        internal static VInt EditionFlagOrdered = VInt.FromEncoded(0x45DD);
        internal static VInt ChapterAtom = VInt.FromEncoded(0xB6);
        internal static VInt ChapterUID = VInt.FromEncoded(0x73C4);
        internal static VInt ChapterStringUID = VInt.FromEncoded(0x5654);
        internal static VInt ChapterTimeStart = VInt.FromEncoded(0x91);
        internal static VInt ChapterTimeEnd = VInt.FromEncoded(0x92);
        internal static VInt ChapterFlagHidden = VInt.FromEncoded(0x98);
        internal static VInt ChapterFlagEnabled = VInt.FromEncoded(0x4598);
        internal static VInt ChapterSegmentUID = VInt.FromEncoded(0x6E67);
        internal static VInt ChapterSegmentEditionUID = VInt.FromEncoded(0x6EBC);
        internal static VInt ChapterPhysicalEquiv = VInt.FromEncoded(0x63C3);
        internal static VInt ChapterTrack = VInt.FromEncoded(0x8F);
        internal static VInt ChapterTrackUID = VInt.FromEncoded(0x89);
        internal static VInt ChapterDisplay = VInt.FromEncoded(0x80);
        internal static VInt ChapString = VInt.FromEncoded(0x85);
        internal static VInt ChapLanguage = VInt.FromEncoded(0x437C);
        internal static VInt ChapLanguageIETF = VInt.FromEncoded(0x437D);
        internal static VInt ChapCountry = VInt.FromEncoded(0x437E);
        internal static VInt ChapProcess = VInt.FromEncoded(0x6944);
        internal static VInt ChapProcessCodecID = VInt.FromEncoded(0x6955);
        internal static VInt ChapProcessPrivate = VInt.FromEncoded(0x450D);
        internal static VInt ChapProcessCommand = VInt.FromEncoded(0x6911);
        internal static VInt ChapProcessTime = VInt.FromEncoded(0x6922);
        internal static VInt ChapProcessData = VInt.FromEncoded(0x6933);
        internal static VInt Tags = VInt.FromEncoded(0x1254C367);
        internal static VInt Tag = VInt.FromEncoded(0x7373);
        internal static VInt Targets = VInt.FromEncoded(0x63C0);
        internal static VInt TargetTypeValue = VInt.FromEncoded(0x68CA);
        internal static VInt TargetType = VInt.FromEncoded(0x63CA);
        internal static VInt TagTrackUID = VInt.FromEncoded(0x63C5);
        internal static VInt TagEditionUID = VInt.FromEncoded(0x63C9);
        internal static VInt TagChapterUID = VInt.FromEncoded(0x63C4);
        internal static VInt TagAttachmentUID = VInt.FromEncoded(0x63C6);
        internal static VInt SimpleTag = VInt.FromEncoded(0x67C8);
        internal static VInt TagName = VInt.FromEncoded(0x45A3);
        internal static VInt TagLanguage = VInt.FromEncoded(0x447A);
        internal static VInt TagLanguageIETF = VInt.FromEncoded(0x447B);
        internal static VInt TagDefault = VInt.FromEncoded(0x4484);
        internal static VInt TagDefaultBogus = VInt.FromEncoded(0x44B4);
        internal static VInt TagString = VInt.FromEncoded(0x4487);
        internal static VInt TagBinary = VInt.FromEncoded(0x4485);
    }
}
