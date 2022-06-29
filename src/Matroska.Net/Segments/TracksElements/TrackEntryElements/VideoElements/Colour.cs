using Matroska.Net.Segments.TracksElements.TrackEntryElements.VideoElements.ColourElements;
using NEbml.Core;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.VideoElements
{
    /// <summary>
    /// Settings describing the colour format.
    /// </summary>
    public class Colour
    {
        internal Colour(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.MatrixCoefficients)
                {
                    MatrixCoefficients = (MatrixCoefficients)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.BitsPerChannel)
                {
                    BitsPerChannel = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ChromaSubsamplingHorz)
                {
                    ChromaSubsamplingHorz = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ChromaSubsamplingVert)
                {
                    ChromaSubsamplingVert = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.CbSubsamplingHorz)
                {
                    CbSubsamplingHorz = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.CbSubsamplingVert)
                {
                    CbSubsamplingVert = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ChromaSitingHorz)
                {
                    ChromaSitingHorz = (ChromaSitingHorz)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ChromaSitingVert)
                {
                    ChromaSitingVert = (ChromaSitingVert)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.Range)
                {
                    Range = (Range)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.TransferCharacteristics)
                {
                    TransferCharacteristics = (TransferCharacteristics)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.Primaries)
                {
                    Primaries = (Primaries)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.MaxCLL)
                {
                    MaxCLL = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.MaxFALL)
                {
                    MaxFALL = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.MasteringMetadata)
                {
                    MasteringMetadata = new MasteringMetadata(reader, version);
                }
            }
            reader.LeaveContainer();
        }
        public Colour(
            MatrixCoefficients matrixCoefficients = MatrixCoefficients.Unspecified,
            ulong bitsPerChannel = 0,
            ulong? chromaSubsamplingHorz = null,
            ulong? chromaSubsamplingVert = null,
            ulong? cbSubsamplingHorz = null,
            ulong? cbSubsamplingVert = null,
            ChromaSitingHorz chromaSitingHorz = ChromaSitingHorz.Unspecified,
            ChromaSitingVert chromaSitingVert = ChromaSitingVert.Unspecified,
            Range range = Range.Unspecified,
            TransferCharacteristics transferCharacteristics = TransferCharacteristics.Unspecified,
            Primaries primaries = Primaries.Unspecified,
            ulong? maxCLL = null,
            ulong? maxFALL = null,
            MasteringMetadata? masteringMetadata = null)
        {
            MatrixCoefficients = matrixCoefficients;
            BitsPerChannel = bitsPerChannel;
            ChromaSubsamplingHorz = chromaSubsamplingHorz;
            ChromaSubsamplingVert = chromaSubsamplingVert;
            CbSubsamplingHorz = cbSubsamplingHorz;
            CbSubsamplingVert = cbSubsamplingVert;
            ChromaSitingHorz = chromaSitingHorz;
            ChromaSitingVert = chromaSitingVert;
            Range = range;
            TransferCharacteristics = transferCharacteristics;
            Primaries = primaries;
            MaxCLL = maxCLL;
            MaxFALL = maxFALL;
            MasteringMetadata = masteringMetadata;
        }
        /// <summary>
        /// The Matrix Coefficients of the video used to derive luma and chroma values from red, green, and blue color primaries.
        /// For clarity, the value and meanings for MatrixCoefficients are adopted from Table 4 of ISO/IEC 23001-8:2016 or ITU-T H.273.
        /// </summary>
        public MatrixCoefficients MatrixCoefficients { get; set; }
        /// <summary>
        /// Number of decoded bits per channel. A value of 0 indicates that the BitsPerChannel is unspecified.
        /// </summary>
        public ulong BitsPerChannel { get; set; }
        /// <summary>
        /// The amount of pixels to remove in the Cr and Cb channels for every pixel not removed horizontally.
        /// Example: For video with 4:2:0 chroma subsampling, the ChromaSubsamplingHorz** SHOULD** be set to 1.
        /// </summary>
        public ulong? ChromaSubsamplingHorz { get; set; }
        /// <summary>
        /// The amount of pixels to remove in the Cr and Cb channels for every pixel not removed vertically.
        /// Example: For video with 4:2:0 chroma subsampling, the ChromaSubsamplingVert** SHOULD** be set to 1.
        /// </summary>
        public ulong? ChromaSubsamplingVert { get; set; }
        /// <summary>
        /// The amount of pixels to remove in the Cb channel for every pixel not removed horizontally.
        /// This is additive with ChromaSubsamplingHorz.Example: For video with 4:2:1 chroma subsampling,
        /// the ChromaSubsamplingHorz** SHOULD** be set to 1 and CbSubsamplingHorz **SHOULD** be set to 1.
        /// </summary>
        public ulong? CbSubsamplingHorz { get; set; }
        /// <summary>
        /// The amount of pixels to remove in the Cb channel for every pixel not removed vertically.
        /// This is additive with ChromaSubsamplingVert.
        /// </summary>
        public ulong? CbSubsamplingVert { get; set; }
        /// <summary>
        /// How chroma is subsampled horizontally.
        /// </summary>
        public ChromaSitingHorz ChromaSitingHorz { get; set; }
        /// <summary>
        /// How chroma is subsampled vertically.
        /// </summary>
        public ChromaSitingVert ChromaSitingVert { get; set; }
        /// <summary>
        /// Clipping of the color ranges.
        /// </summary>
        public Range Range { get; set; }
        /// <summary>
        /// The transfer characteristics of the video. For clarity,
        /// the value and meanings for TransferCharacteristics are adopted from Table 3 of ISO/IEC 23091-4 or ITU-T H.273.
        /// </summary>
        public TransferCharacteristics TransferCharacteristics { get; set; }
        /// <summary>
        /// The colour primaries of the video. For clarity,
        /// the value and meanings for Primaries are adopted from Table 2 of ISO/IEC 23091-4 or ITU-T H.273.
        /// </summary>
        public Primaries Primaries { get; set; }
        /// <summary>
        /// Maximum brightness of a single pixel (Maximum Content Light Level)
        /// in candelas per square meter(cd/m^2^).
        /// </summary>
        public ulong? MaxCLL { get; set; }
        /// <summary>
        /// Maximum brightness of a single full frame (Maximum Frame-Average Light Level)
        /// in candelas per square meter(cd/m^2^).
        /// </summary>
        public ulong? MaxFALL { get; set; }
        /// <summary>
        /// SMPTE 2086 mastering data.
        /// </summary>
        public MasteringMetadata? MasteringMetadata { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var colour = writer.StartMasterElement(Elements.Colour);
            colour.Write(Elements.MatrixCoefficients, (ulong)MatrixCoefficients);
            colour.Write(Elements.BitsPerChannel, BitsPerChannel);
            if (ChromaSubsamplingHorz.HasValue)
            {
                colour.Write(Elements.ChromaSubsamplingHorz, ChromaSubsamplingHorz.Value);
            }
            if (ChromaSubsamplingVert.HasValue)
            {
                colour.Write(Elements.ChromaSubsamplingVert, ChromaSubsamplingVert.Value);
            }
            if (CbSubsamplingHorz.HasValue)
            {
                colour.Write(Elements.CbSubsamplingHorz, CbSubsamplingHorz.Value);
            }
            if (CbSubsamplingVert.HasValue)
            {
                colour.Write(Elements.CbSubsamplingVert, CbSubsamplingVert.Value);
            }
            colour.Write(Elements.ChromaSitingHorz, (ulong)ChromaSitingHorz);
            colour.Write(Elements.ChromaSitingVert, (ulong)ChromaSitingVert);
            colour.Write(Elements.Range, (ulong)Range);
            colour.Write(Elements.TransferCharacteristics, (ulong)TransferCharacteristics);
            colour.Write(Elements.Primaries, (ulong)Primaries);
            if (MaxCLL.HasValue)
            {
                colour.Write(Elements.MaxCLL, MaxCLL.Value);
            }
            if (MaxFALL.HasValue)
            {
                colour.Write(Elements.MaxFALL, MaxFALL.Value);
            }
            if(MasteringMetadata != null)
            {
                MasteringMetadata.WriteTo(colour);
            }
            colour.Dispose();
        }
    }
}
