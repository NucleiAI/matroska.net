using NEbml.Core;
using System;

namespace Matroska.Net.Segments.ClusterElements.BlockGroupElements.BlockAdditionsElements
{
    /// <summary>
    /// Contain the BlockAdditional and some parameters.
    /// </summary>
    public class BlockMore
    {
        internal BlockMore(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.BlockAddID)
                {
                    BlockAddId = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.BlockAdditional)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0 , data.Length);
                    BlockAdditional = new ArraySegment<byte>(data, 0, read);
                }
            }
            reader.LeaveContainer();
        }
        public BlockMore(ArraySegment<byte> blockAdditional, ulong blockAddId = 1)
        {
            BlockAddId = blockAddId;
            BlockAdditional = blockAdditional;
        }
        /// <summary>
        /// An ID to identify the BlockAdditional level.
        /// If BlockAddIDType of the corresponding block is 0, this value is also the value of BlockAddIDType for the meaning of the content of BlockAdditional.
        /// </summary>
        public ulong BlockAddId { get; set; }
        /// <summary>
        /// Interpreted by the codec as it wishes (using the BlockAddID).
        /// </summary>
        public ArraySegment<byte> BlockAdditional { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var blockMore = writer.StartMasterElement(Elements.BlockMore);
            blockMore.Write(Elements.BlockAddID, BlockAddId);
            blockMore.Write(Elements.BlockAdditional, BlockAdditional.Array, BlockAdditional.Offset, BlockAdditional.Count);
            blockMore.Dispose();
        }
    }
}
