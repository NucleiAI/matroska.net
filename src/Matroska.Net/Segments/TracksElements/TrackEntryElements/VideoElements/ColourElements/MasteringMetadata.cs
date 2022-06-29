using NEbml.Core;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.VideoElements.ColourElements
{
    /// <summary>
    /// SMPTE 2086 mastering data.
    /// </summary>
    public class MasteringMetadata
    {
        internal MasteringMetadata(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.PrimaryRChromaticityX)
                {
                    PrimaryRChromaticityX = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.PrimaryRChromaticityY)
                {
                    PrimaryRChromaticityY = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.PrimaryGChromaticityX)
                {
                    PrimaryGChromaticityX = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.PrimaryGChromaticityY)
                {
                    PrimaryGChromaticityY = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.PrimaryBChromaticityX)
                {
                    PrimaryBChromaticityX = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.PrimaryBChromaticityY)
                {
                    PrimaryBChromaticityY = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.WhitePointChromaticityX)
                {
                    WhitePointChromaticityX = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.WhitePointChromaticityY)
                {
                    WhitePointChromaticityY = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.LuminanceMax)
                {
                    LuminanceMax = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.LuminanceMin)
                {
                    LuminanceMin = reader.ReadFloat();
                }
            }
            reader.LeaveContainer();
        }
        public MasteringMetadata(
            double? primaryRChromaticityX = null,
            double? primaryRChromaticityY = null,
            double? primaryGChromaticityX = null,
            double? primaryGChromaticityY = null,
            double? primaryBChromaticityX = null,
            double? primaryBChromaticityY = null,
            double? whitePointChromaticityX = null,
            double? whitePointChromaticityY = null,
            double? luminanceMax = null,
            double? luminanceMin = null)
        {
            PrimaryRChromaticityX = primaryRChromaticityX;
            PrimaryRChromaticityY = primaryRChromaticityY;
            PrimaryGChromaticityX = primaryGChromaticityX;
            PrimaryGChromaticityY = primaryGChromaticityY;
            PrimaryBChromaticityX = primaryBChromaticityX;
            PrimaryBChromaticityY = primaryBChromaticityY;
            WhitePointChromaticityX = whitePointChromaticityX;
            WhitePointChromaticityY = whitePointChromaticityY;
            LuminanceMax = luminanceMax;
            LuminanceMin = luminanceMin;
        }
        /// <summary>
        /// Red X chromaticity coordinate, as defined by CIE 1931.
        /// </summary>
        public double? PrimaryRChromaticityX { get; set; }
        /// <summary>
        /// Red Y chromaticity coordinate, as defined by CIE 1931.
        /// </summary>
        public double? PrimaryRChromaticityY { get; set; }
        /// <summary>
        /// Green X chromaticity coordinate, as defined by CIE 1931.
        /// </summary>
        public double? PrimaryGChromaticityX { get; set; }
        /// <summary>
        /// Green Y chromaticity coordinate, as defined by CIE 1931.
        /// </summary>
        public double? PrimaryGChromaticityY { get; set; }
        /// <summary>
        /// Blue X chromaticity coordinate, as defined by CIE 1931.
        /// </summary>
        public double? PrimaryBChromaticityX { get; set; }
        /// <summary>
        /// Blue Y chromaticity coordinate, as defined by CIE 1931.
        /// </summary>
        public double? PrimaryBChromaticityY { get; set; }
        /// <summary>
        /// White X chromaticity coordinate, as defined by CIE 1931.
        /// </summary>
        public double? WhitePointChromaticityX { get; set; }
        /// <summary>
        /// White Y chromaticity coordinate, as defined by CIE 1931.
        /// </summary>
        public double? WhitePointChromaticityY { get; set; }
        /// <summary>
        /// Maximum luminance. Represented in candelas per square meter (cd/m^2^).
        /// </summary>
        public double? LuminanceMax { get; set; }
        /// <summary>
        /// Minimum luminance. Represented in candelas per square meter (cd/m^2^).
        /// </summary>
        public double? LuminanceMin { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var masteringMetadata = writer.StartMasterElement(Elements.MasteringMetadata);
            if (PrimaryRChromaticityX.HasValue)
            {
                masteringMetadata.Write(Elements.PrimaryRChromaticityX, PrimaryRChromaticityX.Value);
            }
            if (PrimaryRChromaticityY.HasValue)
            {
                masteringMetadata.Write(Elements.PrimaryRChromaticityY, PrimaryRChromaticityY.Value);
            }
            if (PrimaryGChromaticityX.HasValue)
            {
                masteringMetadata.Write(Elements.PrimaryGChromaticityX, PrimaryGChromaticityX.Value);
            }
            if (PrimaryGChromaticityY.HasValue)
            {
                masteringMetadata.Write(Elements.PrimaryGChromaticityY, PrimaryGChromaticityY.Value);
            }
            if (PrimaryBChromaticityX.HasValue)
            {
                masteringMetadata.Write(Elements.PrimaryBChromaticityX, PrimaryBChromaticityX.Value);
            }
            if (PrimaryBChromaticityY.HasValue)
            {
                masteringMetadata.Write(Elements.PrimaryBChromaticityY, PrimaryBChromaticityY.Value);
            }
            if (WhitePointChromaticityX.HasValue)
            {
                masteringMetadata.Write(Elements.WhitePointChromaticityX, WhitePointChromaticityX.Value);
            }
            if (WhitePointChromaticityY.HasValue)
            {
                masteringMetadata.Write(Elements.WhitePointChromaticityY, WhitePointChromaticityY.Value);
            }
            if (LuminanceMax.HasValue)
            {
                masteringMetadata.Write(Elements.LuminanceMax, LuminanceMax.Value);
            }
            if (LuminanceMin.HasValue)
            {
                masteringMetadata.Write(Elements.LuminanceMin, LuminanceMin.Value);
            }
            masteringMetadata.Dispose();
        }
    }
}
