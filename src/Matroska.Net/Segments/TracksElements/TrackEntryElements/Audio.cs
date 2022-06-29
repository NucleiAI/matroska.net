using NEbml.Core;
using System;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements
{
    /// <summary>
    /// Audio settings.
    /// </summary>
    public class Audio
    {
        internal Audio(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.SamplingFrequency)
                {
                    SamplingFrequency = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.OutputSamplingFrequency)
                {
                    OutputSamplingFrequency = reader.ReadFloat();
                }
                else if(reader.ElementId == Elements.Channels)
                {
                    Channels = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ChannelPositions)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    ChannelPositions = new ArraySegment<byte>(data, 0, read);
                }
                else if(reader.ElementId == Elements.BitDepth)
                {
                    BitDepth = reader.ReadUInt();
                }
            }
            reader.LeaveContainer();
        }
        public Audio(
            double samplingFrequency,
            double? outputSamplingFrequency = null,
            ulong channels = 1,
            ArraySegment<byte>? channelPositions = null,
            ulong? bitDepth = null)
        {
            SamplingFrequency = samplingFrequency;
            OutputSamplingFrequency = outputSamplingFrequency;
            Channels = channels;
            ChannelPositions = channelPositions;
            BitDepth = bitDepth;

        }
        /// <summary>
        /// Sampling frequency in Hz.
        /// </summary>
        public double SamplingFrequency { get; set; }
        /// <summary>
        /// Real output sampling frequency in Hz (used for SBR techniques).
        /// <implementation>The default value for OutputSamplingFrequency of the same TrackEntry is equal to the SamplingFrequency.</implementation>
        /// </summary>
        public double? OutputSamplingFrequency { get; set; }
        /// <summary>
        /// Numbers of channels in the track.
        /// </summary>
        public ulong Channels { get; set; }
        /// <summary>
        /// Table of horizontal angles for each successive channel.
        /// </summary>
        public ArraySegment<byte>? ChannelPositions { get; set; }
        /// <summary>
        /// Bits per sample, mostly used for PCM.
        /// </summary>
        public ulong? BitDepth { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var audio = writer.StartMasterElement(Elements.Audio);
            audio.Write(Elements.SamplingFrequency, SamplingFrequency);
            if (OutputSamplingFrequency.HasValue)
            {
                audio.Write(Elements.OutputSamplingFrequency, OutputSamplingFrequency.Value);
            }
            audio.Write(Elements.Channels, Channels);
            if (ChannelPositions.HasValue)
            {
                audio.Write(Elements.ChannelPositions, ChannelPositions.Value.Array, ChannelPositions.Value.Offset, ChannelPositions.Value.Count);
            }
            if (BitDepth.HasValue)
            {
                audio.Write(Elements.BitDepth, BitDepth.Value);
            }
            audio.Dispose();
        }
    }
}
