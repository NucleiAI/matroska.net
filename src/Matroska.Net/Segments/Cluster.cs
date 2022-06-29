using Matroska.Net.Segments.ClusterElements;
using NEbml.Core;
using System;
using System.Collections.Generic;

namespace Matroska.Net.Segments
{
    /// <summary>
    /// The Top-Level Element containing the (monolithic) Block structure.
    /// </summary>
    public class Cluster
    {
        internal Cluster(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<SimpleBlock>? simpleBlocks = null;
            List<BlockGroup>? blockGroups = null;
            List<ArraySegment<byte>>? encryptedBlocks = null;
            while (reader.ReadNext())
            {
                if (reader.ElementId == Elements.Timestamp)
                {
                    Timestamp = reader.ReadUInt();
                }
                else if (version == 0 && reader.ElementId == Elements.SilentTracks)
                {
                    SilentTracks = new SilentTracks(reader, version);
                }
                else if (reader.ElementId == Elements.Position)
                {
                    Position = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.PrevSize)
                {
                    PrevSize = reader.ReadUInt();
                }
                else if (reader.ElementId == Elements.SimpleBlock)
                {
                    if (simpleBlocks == null)
                    {
                        simpleBlocks = new List<SimpleBlock>();
                    }
                    var simpleBlockData = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(simpleBlockData, 0, simpleBlockData.Length);
                    simpleBlocks.Add(new SimpleBlock(new ArraySegment<byte>(simpleBlockData, 0, read)));
                }
                else if (reader.ElementId == Elements.BlockGroup)
                {
                    if(blockGroups == null)
                    {
                        blockGroups = new List<BlockGroup>();
                    }
                    blockGroups.Add(new BlockGroup(reader, version));
                }
                else if(version == 0 && reader.ElementId == Elements.EncryptedBlock)
                {
                    if(encryptedBlocks == null)
                    {
                        encryptedBlocks = new List<ArraySegment<byte>>();
                    }
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);                   
                    encryptedBlocks.Add(new ArraySegment<byte>(data, 0, read));
                }
            }
            EncryptedBlocks = encryptedBlocks;
            SimpleBlocks = simpleBlocks;
            BlockGroups = blockGroups;
            reader.LeaveContainer();
        }
        public Cluster(
            ulong timestamp,
            ulong? position = null,
            ulong?  prevSize = null,
            IEnumerable<SimpleBlock>? simpleBlocks = null,
            IEnumerable<BlockGroup>? blockGroups = null)
        {
            Timestamp = timestamp;
            Position = position;
            PrevSize = prevSize;
            SimpleBlocks = simpleBlocks;
            BlockGroups = blockGroups;
        }
        /// <summary>
        /// Absolute timestamp of the cluster, expressed in Segment Ticks which is based on TimestampScale; see (#timestamp-ticks).
        /// <usage>This element **SHOULD** be the first child element of the Cluster it belongs to,
        /// or the second if that Cluster contains a CRC-32 element((#crc-32)).</usage>
        /// </summary>
        public ulong Timestamp { get; set; }
        /// <summary>
        /// The list of tracks that are not used in that part of the stream.
        /// It is useful when using overlay tracks on seeking or to decide what track to use.
        /// </summary>
        [Obsolete("Silent Tracks are since version 1 of matroska obsolete and should not be used")]
        public SilentTracks? SilentTracks { get; set; }
        /// <summary>
        /// The Segment Position of the Cluster in the Segment (0 in live streams).
        /// It might help to resynchronise offset on damaged streams.
        /// </summary>
        public ulong? Position { get; set; }
        /// <summary>
        /// Size of the previous Cluster, in octets. Can be useful for backward playing.
        /// </summary>
        public ulong? PrevSize { get; set; }
        /// <summary>
        /// Similar to Block, see (#block-structure), but without all the extra information,
        /// mostly used to reduced overhead when no extra feature is needed; see(#simpleblock-structure) on SimpleBlock Structure.
        /// </summary>
        public IEnumerable<SimpleBlock>? SimpleBlocks { get; set; }
        /// <summary>
        /// Basic container of information containing a single Block and information specific to that Block.
        /// </summary>
        public IEnumerable<BlockGroup>? BlockGroups { get; set; }
        /// <summary>
        /// Similar to SimpleBlock, see (#simpleblock-structure),
        /// but the data inside the Block are Transformed(encrypt and/or signed).
        /// </summary>
        [Obsolete("EncryptedBlock is since version 1 of matroska obsolete and should not be used")]
        public IEnumerable<ArraySegment<byte>>? EncryptedBlocks { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var cluster = writer.StartMasterElement(Elements.Cluster);
            cluster.Write(Elements.Timestamp, Timestamp);
            if (Position.HasValue)
            {
                cluster.Write(Elements.Position, Position.Value);
            }
            if (PrevSize.HasValue)
            {
                cluster.Write(Elements.PrevSize, PrevSize.Value);
            }
            if(SimpleBlocks != null)
            {
                foreach (var simpleBlock in SimpleBlocks)
                {
                    var block = simpleBlock.GetBlock();
                    cluster.Write(Elements.SimpleBlock, block.Array, block.Offset, block.Count);
                }
            }
            if(BlockGroups != null)
            {
                foreach(var blockGroup in BlockGroups)
                {
                    blockGroup.WriteTo(cluster);
                }
            }
            cluster.Dispose();
        }
    }
}
