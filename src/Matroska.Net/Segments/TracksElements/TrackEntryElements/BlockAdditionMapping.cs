using NEbml.Core;
using System;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements
{
    /// <summary>
    /// Contains elements that extend the track format, by adding content either to each frame,
    /// with BlockAddID((#blockaddid-element)), or to the track as a whole
    /// with BlockAddIDExtraData.
    /// </summary>
    public class BlockAdditionMapping
    {
        internal BlockAdditionMapping(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while(reader.ReadNext())
            {
                if(reader.ElementId == Elements.BlockAddIDValue)
                {
                    BlockAddIDValue = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.BlockAddIDName)
                {
                    BlockAddIDName = reader.ReadAscii();
                }
                else if(reader.ElementId == Elements.BlockAddIDType)
                {
                    BlockAddIDType = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.BlockAddIDExtraData)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0 , data.Length);
                    BlockAddIDExtraData = new ArraySegment<byte>(data, 0, read);
                }
            }
            reader.LeaveContainer();
        }
        public BlockAdditionMapping(
            ulong? blockAddIDValue = null,
            string? blockAddIDName = null,
            ulong blockAddIDType = 0,
            ArraySegment<byte>? blockAddIDExtraData = null)
        {
            BlockAddIDValue = blockAddIDValue;
            BlockAddIDName = blockAddIDName;
            BlockAddIDType = blockAddIDType;
            BlockAddIDExtraData = blockAddIDExtraData;
        }
        /// <summary>
        /// If the track format extension needs content beside frames,
        /// the value refers to the BlockAddID((#blockaddid-element)), value being described.
        /// To keep MaxBlockAdditionID as low as possible, small values **SHOULD** be used.
        /// </summary>
        public ulong? BlockAddIDValue { get; set; }
        /// <summary>
        /// A human-friendly name describing the type of BlockAdditional data,
        /// as defined by the associated Block Additional Mapping.
        /// </summary>
        public string? BlockAddIDName { get; set; }
        /// <summary>
        /// Stores the registered identifier of the Block Additional Mapping
        /// to define how the BlockAdditional data should be handled.
        /// </summary>
        public ulong BlockAddIDType { get; set; }
        /// <summary>
        /// Extra binary data that the BlockAddIDType can use to interpret the BlockAdditional data.
        /// The interpretation of the binary data depends on the BlockAddIDType value and the corresponding Block Additional Mapping.
        /// </summary>
        public ArraySegment<byte>? BlockAddIDExtraData { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var blockAdditionMap = writer.StartMasterElement(Elements.BlockAdditionMapping);
            if (BlockAddIDValue.HasValue)
            {
                blockAdditionMap.Write(Elements.BlockAddIDValue, BlockAddIDValue.Value);
            }
            if(BlockAddIDName != null)
            {
                blockAdditionMap.WriteAscii(Elements.BlockAddIDName, BlockAddIDName);
            }
            blockAdditionMap.Write(Elements.BlockAddIDType, BlockAddIDType);
            if (BlockAddIDExtraData.HasValue)
            {
                blockAdditionMap.Write(Elements.BlockAddIDExtraData, BlockAddIDExtraData.Value.Array, BlockAddIDExtraData.Value.Offset, BlockAddIDExtraData.Value.Count);
            }
            blockAdditionMap.Dispose();
         
        }
    }
}
