using System;
using System.Collections.Generic;
using System.Linq;

namespace Matroska.Net.AVC
{
    public struct AVCDecoderConfigurationRecord
    {
        public AVCDecoderConfigurationRecord(ArraySegment<byte> config)
        {
            Version = config[0];
            AVCProfileIndication = (AVCProfileIndication)config[1];
            ProfileCompatibility = config[2];
            AVCLevelIndication = (AVCLevelIndication)config[3];
            LengthSize = (LengthSize)config[4];
            var spsCount = config[5];
            var sps = new List<ArraySegment<byte>>();
            var position = 6;
            for (int i = spsCount; i > 0; i--)
            {
                Span<byte> s = stackalloc byte[2];
                s[0] = config[position];
                position++;
                s[1] = config[position];
                position++;
                var length = BitConverter.ToInt16(s);
                var data = new byte[length];
                Array.Copy(config.Array,config.Offset + position, data, 0, length);
                position += length;
                sps.Add(new ArraySegment<byte>(data));
            }
            SequenceParameterSets = sps;
            var ppsCount = config[position];
            var pps = new List<ArraySegment<byte>>();
            for(int i = ppsCount; i > 0; i--)
            {
                Span<byte> s = stackalloc byte[2];
                s[0] = config[position];
                position++;
                s[1] = config[position];
                position++;
                var length = BitConverter.ToInt16(s);
                var data = new byte[length];
                Array.Copy(config.Array, config.Offset + position, data, 0, length);
                position += length;
                pps.Add(new ArraySegment<byte>(data));
            }
            PictureParameterSets = pps;
        }
        public AVCDecoderConfigurationRecord(
            byte version,
            AVCProfileIndication profileIndication,
            byte profileCompatibility,
            AVCLevelIndication levelIndication,
            LengthSize lengthSize,
            IEnumerable<ArraySegment<byte>> sequenceParameterSets,
            IEnumerable<ArraySegment<byte>> pictureParameterSets)
        {
            Version = version;
            AVCProfileIndication = profileIndication;
            ProfileCompatibility = profileCompatibility;
            AVCLevelIndication = levelIndication;
            LengthSize = lengthSize;
            SequenceParameterSets = sequenceParameterSets;
            PictureParameterSets = pictureParameterSets;
        }
        public byte Version { get; set; }
        public AVCProfileIndication AVCProfileIndication { get; set; }
        public byte ProfileCompatibility { get; set; }
        public AVCLevelIndication AVCLevelIndication { get; set; }
        public LengthSize LengthSize { get; set; }
        public IEnumerable<ArraySegment<byte>> SequenceParameterSets { get; set; }
        public IEnumerable<ArraySegment<byte>> PictureParameterSets { get; set; }
        public ArraySegment<byte> GetConfiguration()
        {
            var size = 7;
            foreach (var sps in SequenceParameterSets)
            {
                size++;
                size += sps.Count;
            }
            foreach (var pps in PictureParameterSets)
            {
                size++;
                size += pps.Count;
            }
            var bin = new byte[size];
            bin[0] = Version;
            bin[1] = (byte)AVCProfileIndication;
            bin[2] = ProfileCompatibility;
            bin[3] = (byte)AVCLevelIndication;
            bin[4] = (byte)LengthSize;
            bin[5] = (byte)SequenceParameterSets.Count();
            var position = 6;
            foreach (var sps in SequenceParameterSets)
            {
                bin[position] = (byte)sps.Count;
                position++;
                Array.Copy(sps.Array, sps.Offset, bin, position, sps.Count);
                position += sps.Count;
            }
            bin[position] = (byte)PictureParameterSets.Count();
            position++;
            foreach (var pps in PictureParameterSets)
            {
                bin[position] = (byte)pps.Count;
                position++;
                Array.Copy(pps.Array, pps.Offset, bin, position, pps.Count);
                position += pps.Count;
            }
            return new ArraySegment<byte>(bin);
        }
    }
}
