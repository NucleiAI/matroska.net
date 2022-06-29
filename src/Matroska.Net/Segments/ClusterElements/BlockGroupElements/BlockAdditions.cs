using Matroska.Net.Segments.ClusterElements.BlockGroupElements.BlockAdditionsElements;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net.Segments.ClusterElements.BlockGroupElements
{
    /// <summary>
    /// Contain additional blocks to complete the main one.
    /// An EBML parser that has no knowledge of the Block structure could still see and use/skip these data.
    /// </summary>
    public class BlockAdditions
    {
        internal BlockAdditions(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<BlockMore> blockMores = new List<BlockMore>();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.BlockAdditions)
                {
                    blockMores.Add(new BlockMore(reader, version));
                }
            }
            BlockMores = blockMores;
            reader.LeaveContainer();
        }
        public BlockAdditions(IEnumerable<BlockMore> blockMores)
        {
            BlockMores = blockMores;
        }
        /// <summary>
        /// Contain the BlockAdditional and some parameters.
        /// </summary>
        public IEnumerable<BlockMore> BlockMores { get; set; }
        internal void WriteTo(EbmlWriter writer)
        {
            var blockAdditions = writer.StartMasterElement(Elements.BlockAdditions);
            foreach(var blockMore in BlockMores)
            {
                blockMore.WriteTo(blockAdditions);
            }
            blockAdditions.Dispose();
        }
    }
}
