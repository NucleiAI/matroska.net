using NEbml.Core;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Matroska.Net
{
    /// <summary>
    /// Frames using references SHOULD be stored in "coding order". That means the references first, and then the frames referencing them. A consequence is that timestamps might not be consecutive. But a frame with a past timestamp MUST reference a frame already known, otherwise it's considered bad/void.
    /// </summary>
    public class SimpleBlock : Block
    {
        internal SimpleBlock(ArraySegment<byte> blockBinary) : base(blockBinary)
        {         
        }
        public SimpleBlock(
            ulong trackId,
            ArraySegment<byte> frameData,
            bool keyframe = false,
            short timestamp = 0,
            bool invisible = false,
            bool discardable = false,
            BlockLacing lacing = BlockLacing.NoLacing)
            : base(trackId, frameData, timestamp, invisible, lacing)
        {
            Keyframe = keyframe;
            Discardable = discardable;
        }
        /// <summary>
        /// Keyframe, set when the Block contains only keyframes
        /// </summary>
        public bool Keyframe { get; set; }
        public bool Discardable { get; set; }

        protected override void SetFlags(byte flags)
        {
            base.SetFlags(flags);
            Keyframe = (flags | 0b01111111) == 0xFF;
            Discardable = (flags | 0b11111110) == 0xFF;
        }
        protected override byte GetFlags()
        {
            var flags = base.GetFlags();
            flags |= Keyframe ? (byte)0b10000000 : (byte)0b00000000;
            flags |= Discardable ? (byte)0b00000001 : (byte)0b00000000;
            return flags;
        }
    }
}
