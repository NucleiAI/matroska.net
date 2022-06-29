using Matroska.Net.Segments.TracksElements.TrackEntryElements.VideoElements;
using NEbml.Core;
using System;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements
{
    /// <summary>
    /// Video settings.
    /// </summary>
    public class Video
    {
        internal Video(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.FlagInterlaced)
                {
                    FlagInterlaced = (FlagInterlaced)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.FieldOrder)
                {
                    FieldOrder = (FieldOrder)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.StereoMode)
                {
                    StereoMode = (StereoMode)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.AlphaMode)
                {
                    AlphaMode = (AlphaMode)reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.OldStereoMode)
                {
                    OldStereoMode = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.PixelWidth)
                {
                    PixelWidth = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.PixelHeight)
                {
                    PixelHeight = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.PixelCropBottom)
                {
                    PixelCropBottom = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.PixelCropTop)
                {
                    PixelCropTop = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.PixelCropLeft)
                {
                    PixelCropLeft = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.PixelCropRight)
                {
                    PixelCropRight = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.DisplayWidth)
                {
                    DisplayWidth = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.DisplayHeight)
                {
                    DisplayHeight = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.DisplayUnit)
                {
                    DisplayUnit = (DisplayUnit)reader.ReadUInt();
                }
                else if(version == 0 && reader.ElementId == Elements.AspectRatioType)
                {
                    AspectRatioType = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.UncompressedFourCC)
                {
                    var data = new byte[4];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    UncompressedFourCC = new ArraySegment<byte>(data, 0, read);
                }
                else if(version == 0 && reader.ElementId == Elements.GammaValue)
                {
                    GammaValue = reader.ReadFloat();
                }
                else if(version == 0 && reader.ElementId == Elements.FrameRate)
                {
                    FrameRate = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.Colour)
                {
                    Colour = new Colour(reader, version);
                }
                else if(reader.ElementId == Elements.Projection)
                {
                    Projection = new Projection(reader, version);
                }
            }
            reader.LeaveContainer();
        }
        public Video(
            ulong pixelWidth,
            ulong pixelHeight,
            FlagInterlaced flagInterlaced = FlagInterlaced.Undetermined,
            FieldOrder fieldOrder = FieldOrder.Undetermined,
            StereoMode stereoMode = StereoMode.Mono,
            AlphaMode alphaMode = AlphaMode.None,
            ulong pixelCropBottom = 0,
            ulong pixelCropTop = 0,
            ulong pixelCropLeft = 0,
            ulong pixelCropRight = 0,
            ulong? displayWidth = null,
            ulong? displayHeight = null,
            DisplayUnit displayUnit = DisplayUnit.Pixels,
            ArraySegment<byte>? uncompressedFourCC = null,
            Colour? colour = null,
            Projection? projection = null)
        {
            FlagInterlaced = flagInterlaced;
            FieldOrder = fieldOrder;
            StereoMode = stereoMode;
            AlphaMode = alphaMode;
            PixelWidth = pixelWidth;
            PixelHeight = pixelHeight;
            PixelCropBottom = pixelCropBottom;
            PixelCropTop = pixelCropTop;
            PixelCropLeft = pixelCropLeft;
            PixelCropRight = pixelCropRight;
            DisplayWidth = displayWidth;
            DisplayHeight = displayHeight;
            DisplayUnit = displayUnit;
            UncompressedFourCC = uncompressedFourCC;
            Colour = colour;
            Projection = projection;
        }
        /// <summary>
        /// Specify whether the video frames in this track are interlaced or not.
        /// </summary>
        public FlagInterlaced FlagInterlaced { get; set; }
        /// <summary>
        /// Specify the field ordering of video frames in this track.
        /// <usage>If FlagInterlaced is not set to 1, this Element **MUST** be ignored.</usage>
        /// </summary>
        public FieldOrder FieldOrder { get; set; }
        /// <summary>
        /// Stereo-3D video mode. There are some more details in (#multi-planar-and-3d-videos).
        /// </summary>
        public StereoMode StereoMode { get; set; }
        /// <summary>
        /// Indicate whether the BlockAdditional Element with BlockAddID of "1" contains Alpha data, as defined by to the Codec Mapping for the `CodecID`.
        /// Undefined values **SHOULD NOT** be used as the behavior of known implementations is different(considered either as 0 or 1).
        /// </summary>
        public AlphaMode AlphaMode { get; set; }
        /// <summary>
        /// DEPRECATED, DO NOT USE. Bogus StereoMode value used in old versions of libmatroska.
        /// </summary>
        [Obsolete("TrackOffset is since version 1 of matroska obsolete and should not be used")]
        public ulong? OldStereoMode { get; set; }
        /// <summary>
        /// Width of the encoded video frames in pixels.
        /// </summary>
        public ulong PixelWidth { get; set; }
        /// <summary>
        /// Height of the encoded video frames in pixels.
        /// </summary>
        public ulong PixelHeight { get; set; }
        /// <summary>
        /// The number of video pixels to remove at the bottom of the image.
        /// </summary>
        public ulong PixelCropBottom { get; set; }
        /// <summary>
        /// The number of video pixels to remove at the top of the image.
        /// </summary>
        public ulong PixelCropTop { get; set; }
        /// <summary>
        /// The number of video pixels to remove on the left of the image.
        /// </summary>
        public ulong PixelCropLeft { get; set; }
        /// <summary>
        /// The number of video pixels to remove on the right of the image.
        /// </summary>
        public ulong PixelCropRight { get; set; }
        /// <summary>
        /// Width of the video frames to display. Applies to the video frame after cropping (PixelCrop* Elements).
        /// <implementation>If the DisplayUnit of the same TrackEntry is 0, then the default value for DisplayWidth is equal to
        /// PixelWidth - PixelCropLeft - PixelCropRight, else there is no default value.</implementation>
        /// </summary>
        public ulong? DisplayWidth { get; set; }
        /// <summary>
        /// Height of the video frames to display. Applies to the video frame after cropping (PixelCrop* Elements).
        /// <implementation>If the DisplayUnit of the same TrackEntry is 0, then the default value for DisplayHeight is equal to
        /// PixelHeight - PixelCropTop - PixelCropBottom, else there is no default value.</implementation>
        /// </summary>
        public ulong? DisplayHeight { get; set; }
        /// <summary>
        /// How DisplayWidth & DisplayHeight are interpreted.
        /// </summary>
        public DisplayUnit DisplayUnit { get; set; }
        /// <summary>
        /// Specify the possible modifications to the aspect ratio.
        /// </summary>
        [Obsolete("AspectRatioType is since version 1 of matroska obsolete and should not be used")]
        public ulong? AspectRatioType { get; set; }
        /// <summary>
        /// Specify the uncompressed pixel format used for the Track's data as a FourCC.
        /// This value is similar in scope to the biCompression value of AVI's `BITMAPINFO` [@?AVIFormat]. See the YUV video formats [@?FourCC-YUV] and RGB video formats [@?FourCC-RGB] for common values.
        /// <implementation>UncompressedFourCC **MUST** be set (minOccurs=1) in TrackEntry, when the CodecID Element of the TrackEntry is set to "V_UNCOMPRESSED".</implementation>
        /// <usage>This Element **MUST NOT** be used if the CodecID Element of the TrackEntry is set to "V_UNCOMPRESSED".</usage>
        /// </summary>
        public ArraySegment<byte>? UncompressedFourCC { get; set; }
        /// <summary>
        /// Gamma Value.
        /// </summary>
        [Obsolete("GammaValue is since version 1 of matroska obsolete and should not be used")]
        public double? GammaValue { get; set; }
        /// <summary>
        /// Number of frames per second. This value is Informational only. It is intended for constant frame rate streams, and **SHOULD NOT** be used for a variable frame rate TrackEntry.
        /// </summary>
        [Obsolete("FrameRate is since version 1 of matroska obsolete and should not be used")]
        public double? FrameRate { get; set; }
        /// <summary>
        /// Settings describing the colour format.
        /// </summary>
        public Colour? Colour { get; set; }
        /// <summary>
        /// Describes the video projection details. Used to render spherical, VR videos or flipping videos horizontally/vertically.
        /// </summary>
        public Projection? Projection { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var video = writer.StartMasterElement(Elements.Video);
            video.Write(Elements.FlagInterlaced, (ulong)FlagInterlaced);
            video.Write(Elements.FieldOrder, (ulong)FieldOrder);
            video.Write(Elements.StereoMode, (ulong)StereoMode);
            video.Write(Elements.AlphaMode, (ulong)AlphaMode);
            video.Write(Elements.PixelWidth, PixelWidth);
            video.Write(Elements.PixelHeight, PixelHeight);
            video.Write(Elements.PixelCropBottom, PixelCropBottom);
            video.Write(Elements.PixelCropTop, PixelCropTop);
            video.Write(Elements.PixelCropLeft, PixelCropLeft);
            video.Write(Elements.PixelCropRight, PixelCropRight);
            if (DisplayWidth.HasValue)
            {
                video.Write(Elements.DisplayWidth, DisplayWidth.Value);
            }
            if (DisplayHeight.HasValue)
            {
                video.Write(Elements.DisplayHeight, DisplayHeight.Value);
            }
            video.Write(Elements.DisplayUnit, (ulong)DisplayUnit);
            if (UncompressedFourCC.HasValue)
            {
                video.Write(Elements.UncompressedFourCC, UncompressedFourCC.Value.Array, UncompressedFourCC.Value.Offset, UncompressedFourCC.Value.Count);
            }
            if(Colour != null)
            {
                Colour.WriteTo(video);
            }
            if(Projection != null)
            {
                Projection.WriteTo(video);
            }
            video.Dispose();
        }
    }
}
