using NEbml.Core;
using System;
using System.IO;

namespace Matroska.Net
{
    /// <summary>
    /// Frames using references SHOULD be stored in "coding order". That means the references first, and then the frames referencing them. A consequence is that timestamps might not be consecutive. But a frame with a past timestamp MUST reference a frame already known, otherwise it's considered bad/void.
    /// </summary>
    public class Block
    {
        internal Block(ArraySegment<byte> blockBinary)
        {
            var trackNumber = blockBinary[0];
            Span<byte> trackNumberArr = stackalloc byte[8];
            var readOffset = 0;
            if ((trackNumber | 0b01111111) == 0xFF)
            {
                // 1 octet
                trackNumberArr[0] = trackNumber;
                readOffset = 1;
            }
            else if ((trackNumber | 0b10111111) == 0xFF)
            {
                // 2 octet
                trackNumberArr[0] = blockBinary[1];
                trackNumberArr[1] = trackNumber;
                readOffset = 2;
            }
            else if ((trackNumber | 0b11011111) == 0xFF)
            {
                trackNumberArr[0] = blockBinary[2];
                trackNumberArr[1] = blockBinary[1];
                trackNumberArr[2] = trackNumber;
                readOffset = 3;
                // 3 octet
            }
            else if ((trackNumber | 0b11101111) == 0xFF)
            {
                trackNumberArr[0] = blockBinary[3];
                trackNumberArr[1] = blockBinary[2];
                trackNumberArr[2] = blockBinary[1];
                trackNumberArr[3] = trackNumber;
                readOffset = 4;
                // 4 octet
            }
            else if ((trackNumber | 0b11110111) == 0xFF)
            {
                trackNumberArr[0] = blockBinary[4];
                trackNumberArr[1] = blockBinary[3];
                trackNumberArr[2] = blockBinary[2];
                trackNumberArr[3] = blockBinary[1];
                trackNumberArr[4] = trackNumber;
                readOffset = 5;
                // 5 octet
            }
            else if ((trackNumber | 0b11111011) == 0xFF)
            {
                trackNumberArr[0] = blockBinary[5];
                trackNumberArr[1] = blockBinary[4];
                trackNumberArr[2] = blockBinary[3];
                trackNumberArr[3] = blockBinary[2];
                trackNumberArr[4] = blockBinary[1];
                trackNumberArr[5] = trackNumber;
                readOffset = 6;
                // 6 octet
            }
            else if ((trackNumber | 0b11111101) == 0xFF)
            {
                trackNumberArr[0] = blockBinary[6];
                trackNumberArr[1] = blockBinary[5];
                trackNumberArr[2] = blockBinary[4];
                trackNumberArr[3] = blockBinary[3];
                trackNumberArr[4] = blockBinary[2];
                trackNumberArr[5] = blockBinary[1];
                trackNumberArr[6] = trackNumber;
                readOffset = 7;
                // 7 octet
            }
            else if ((trackNumber | 0b11111110) == 0xFF)
            {
                trackNumberArr[0] = blockBinary[7];
                trackNumberArr[1] = blockBinary[6];
                trackNumberArr[2] = blockBinary[5];
                trackNumberArr[3] = blockBinary[4];
                trackNumberArr[4] = blockBinary[3];
                trackNumberArr[5] = blockBinary[2];
                trackNumberArr[6] = blockBinary[1];
                trackNumberArr[7] = trackNumber;
                readOffset = 8;
                // 8 octet
            }
            else
            {
                throw new ArgumentException("The TrackNumber is out of Range of the Parser");
            }
            ulong encodedTrackNumber = BitConverter.ToUInt64(trackNumberArr);
            VInt trackId = VInt.FromEncoded(encodedTrackNumber);
            TrackId = trackId;
            Span<byte> timecodeArr = stackalloc byte[2];
            timecodeArr[0] = blockBinary[readOffset + 1];
            timecodeArr[1] = blockBinary[readOffset];
            readOffset += 2;
            Timestamp = BitConverter.ToInt16(timecodeArr);
            SetFlags(blockBinary[readOffset]);
            readOffset++;
            FrameData = new ArraySegment<byte>(blockBinary.Array, blockBinary.Offset + readOffset, blockBinary.Count - readOffset);
        }
        public Block(
            ulong trackId,
            ArraySegment<byte> frameData,
            short timestamp = 0,
            bool invisible = false,
            BlockLacing lacing = BlockLacing.NoLacing)
        {
            TrackId = VInt.EncodeSize(trackId);
            FrameData = frameData;
            Timestamp = timestamp;
            Invisible = invisible;
            Lacing = lacing;
        }
        /// <summary>
        /// Track Number (Track Entry). It is coded in EBML like form (1 octet if the value is < 0x80, 2 if < 0x4000, etc) (most significant bits set to increase the range).
        /// </summary>
        public VInt TrackId { get; set; }
        /// <summary>
        /// Timestamp (relative to Cluster timestamp, signed int16)
        /// </summary>
        public short Timestamp { get; set; }
        /// <summary>
        /// Invisible, the codec SHOULD decode this frame but not display it
        /// </summary>
        public bool Invisible { get; set; }
        /// <summary>
        /// Lacing
        /// </summary>
        public BlockLacing Lacing { get; set; }
        /// <summary>
        /// Laced Frame Data or for no lacing single frame Data
        /// </summary>
        public ArraySegment<byte> FrameData { get; set; }

        protected virtual void SetFlags(byte flags)
        {
            Invisible = (flags | 0b11110111) == 0xFF;
            if((flags | 0b11111001) == 0xFF)
            {
                Lacing = BlockLacing.EBMLLacing;
            }
            else if((flags | 0b11111011) == 0xFF)
            {
                Lacing = BlockLacing.FixedSizeLacing;
            }
            else if((flags | 0b11111101) == 0xFF)
            {
                Lacing = BlockLacing.XiphLacing;
            }
            else if((flags | 0b11111111) == 0xFF)
            {
                Lacing = BlockLacing.NoLacing;
            }
        }

        protected virtual byte GetFlags()
        {
            var flags = Invisible ? (byte)0b00001000 : (byte)0b00000000;
            flags |= (byte)Lacing;
            return flags;
        }
        protected virtual void Write(Stream stream)
        {
            TrackId.Write(stream);
            stream.WriteByte((byte)(Timestamp >> 8));
            stream.WriteByte((byte)Timestamp);
            var flags = GetFlags();
            stream.WriteByte(flags);
            stream.Write(FrameData);
        }

        public virtual ArraySegment<byte> GetBlock()
        {
            using MemoryStream data = new MemoryStream();
            Write(data);
            var result = new ArraySegment<byte>(new byte[data.Length], 0, (int)data.Length);
            data.Seek(0, SeekOrigin.Begin);
            data.ReadFully(result.Array, result.Offset, result.Count);
            return result;
        }
    }
}
