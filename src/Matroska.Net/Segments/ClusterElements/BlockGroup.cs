using Matroska.Net.Segments.ClusterElements.BlockGroupElements;
using NEbml.Core;
using System;
using System.Collections.Generic;

namespace Matroska.Net.Segments.ClusterElements
{
    /// <summary>
    /// Basic container of information containing a single Block and information specific to that Block.
    /// </summary>
    public class BlockGroup
    {
        internal BlockGroup(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<long>? referenceBlocks = null;
            while (reader.ReadNext())
            {
                if (reader.ElementId == Elements.Block)
                {
                    var blockData = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(blockData, 0, blockData.Length);
                    Block = new Block(new ArraySegment<byte>(blockData, 0, read));
                }
                else if(version == 0 && reader.ElementId == Elements.BlockVirtual)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    BlockVirtual = new ArraySegment<byte>(data, 0, read);
                }
                else if(reader.ElementId == Elements.BlockAdditions)
                {
                    BlockAdditions = new BlockAdditions(reader, version);
                }
                else if(reader.ElementId == Elements.BlockDuration)
                {
                    BlockDuration = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ReferencePriority)
                {
                    ReferencePriority = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ReferenceBlock)
                {
                    if(referenceBlocks == null)
                    {
                        referenceBlocks = new List<long>();
                    }
                    referenceBlocks.Add(reader.ReadInt());
                }
                else if(version == 0 && reader.ElementId == Elements.ReferenceVirtual)
                {
                    ReferenceVirtual = reader.ReadInt();
                }
                else if(reader.ElementId == Elements.CodecState)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    CodecState = new ArraySegment<byte>(data, 0, read);
                }
                else if(reader.ElementId == Elements.DiscardPadding)
                {
                    DiscardPadding = reader.ReadInt();
                }
                else if(version == 0 && reader.ElementId == Elements.Slices)
                {
                    Slices = new Slices(reader, version);
                }
                else if(version == 0 && reader.ElementId == Elements.ReferenceFrame)
                {
                    ReferenceFrame = new ReferenceFrame(reader, version);
                }
            }
            ReferenceBlocks = referenceBlocks;
            reader.LeaveContainer();
        }
        public BlockGroup(
            Block block,
            BlockAdditions? blockAdditions = null,
            ulong? blockDuration = null,
            ulong referencePriority = 0,
            IEnumerable<long>? referenceBlocks = null,
            ArraySegment<byte>? codecState = null,
            long? discardPadding = null)
        {
            Block = block;
            BlockAdditions = blockAdditions;
            BlockDuration = blockDuration;
            ReferencePriority = referencePriority;
            ReferenceBlocks = referenceBlocks;
            CodecState = codecState;
            DiscardPadding = discardPadding;
        }
        /// <summary>
        /// Block containing the actual data to be rendered and a timestamp relative to the Cluster Timestamp;
        /// see(#block-structure) on Block Structure.
        /// </summary>
        public Block Block { get; set; }
        /// <summary>
        /// A Block with no data. It **MUST** be stored in the stream at the place the real Block would be in display order.
        /// </summary>
        [Obsolete("BlockVirtual is since version 1 of matroska obsolete and should not be used")]
        public ArraySegment<byte>? BlockVirtual { get; set; }
        /// <summary>
        /// Contain additional blocks to complete the main one.
        /// An EBML parser that has no knowledge of the Block structure could still see and use/skip these data.
        /// </summary>
        public BlockAdditions? BlockAdditions { get; set; }
        /// <summary>
        /// The duration of the Block, expressed in Track Ticks; see (#timestamp-ticks).
        /// The BlockDuration Element can be useful at the end of a Track to define the duration of the last frame(as there is no subsequent Block available),
        /// or when there is a break in a track like for subtitle tracks.
        /// <note>BlockDuration **MUST** be set (minOccurs=1) if the associated TrackEntry stores a DefaultDuration value.</note>
        /// <note>When not written and with no DefaultDuration, the value is assumed to be the difference between the timestamp
        /// of this Block and the timestamp of the next Block in "display" order (not coding order).</note>
        /// </summary>
        public ulong? BlockDuration { get; set; }
        /// <summary>
        /// This frame is referenced and has the specified cache priority.
        ///In cache only a frame of the same or higher priority can replace this frame.A value of 0 means the frame is not referenced.
        /// </summary>
        public ulong ReferencePriority { get; set; }
        /// <summary>
        /// A timestamp value, relative to the timestamp of the Block in this BlockGroup, expressed in Track Ticks; see(#timestamp-ticks).
        /// This is used to reference other frames necessary to decode this frame.
        /// The relative value** SHOULD** correspond to a valid `Block` this `Block` depends on.
        /// Historically Matroska Writer didn't write the actual `Block(s)` this `Block` depends on, but *some* `Block` in the past.
        /// The value "0" **MAY** also be used to signify this `Block` cannot be decoded on its own, but without knownledge of which `Block` is necessary.In this case, other `ReferenceBlock` **MUST NOT** be found in the same `BlockGroup`.
        /// If the `BlockGroup` doesn't have any `ReferenceBlock` element, then the `Block` it contains can be decoded without using any other `Block` data.
        /// </summary>
        public IEnumerable<long>? ReferenceBlocks { get; set; }
        /// <summary>
        /// The Segment Position of the data that would otherwise be in position of the virtual block.
        /// </summary>
        [Obsolete("ReferenceVirtual is since version 1 of matroska obsolete and should not be used")]
        public long? ReferenceVirtual { get; set; }
        /// <summary>
        /// The new codec state to use. Data interpretation is private to the codec.
        /// This information **SHOULD** always be referenced by a seek entry.
        /// </summary>
        public ArraySegment<byte>? CodecState { get; set; }
        /// <summary>
        /// Duration of the silent data added to the Block, expressed in Matroska Ticks -- ie in nanoseconds; see (#timestamp-ticks)
        /// (padding at the end of the Block for positive value, at the beginning of the Block for negative value).
        /// The duration of DiscardPadding is not calculated in the duration of the TrackEntry and **SHOULD** be discarded during playback.
        /// </summary>
        public long? DiscardPadding { get; set; }
        /// <summary>
        /// Contains slices description.
        /// </summary>
        [Obsolete("Slices is since version 1 of matroska obsolete and should not be used")]
        public Slices? Slices { get; set; }
        /// <summary>
        /// Contains information about the last reference frame. See [@?DivXTrickTrack].
        /// </summary>
        [Obsolete("ReferenceFrame is since version 1 of matroska obsolete and should not be used")]
        public ReferenceFrame? ReferenceFrame { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var blockGroup = writer.StartMasterElement(Elements.BlockGroup);
            var block = Block.GetBlock();
            blockGroup.Write(Elements.Block, block.Array, block.Offset, block.Count);
            if(BlockAdditions != null)
            {
                BlockAdditions.WriteTo(blockGroup);
            }
            if (BlockDuration.HasValue)
            {
                blockGroup.Write(Elements.BlockDuration, BlockDuration.Value);
            }
            blockGroup.Write(Elements.ReferencePriority, ReferencePriority);
            if(ReferenceBlocks != null)
            {
                foreach(var referenceBlock in ReferenceBlocks)
                {
                    blockGroup.Write(Elements.ReferenceBlock, referenceBlock);
                }
            }
            if (CodecState.HasValue)
            {
                blockGroup.Write(Elements.CodecState, CodecState.Value.Array, CodecState.Value.Offset, CodecState.Value.Count);
            }
            if (DiscardPadding.HasValue)
            {
                blockGroup.Write(Elements.DiscardPadding, DiscardPadding.Value);
            }
            blockGroup.Dispose();
        }
    }
}
