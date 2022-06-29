using NEbml.Core;
using System;

namespace Matroska.Net.Segments.ChaptersElements.EditionEntryElements.ChapterAtomElements.ChapProcessElements
{
    /// <summary>
    /// Contains all the commands associated to the Atom.
    /// </summary>
    public class ChapProcessCommand
    {
        internal ChapProcessCommand(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.ChapProcessTime)
                {
                    ChapProcessTime = (ChapProcessTime)reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ChapProcessData)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    ChapProcessData = new ArraySegment<byte>(data, 0, read);
                }
            }
            reader.LeaveContainer();
        }
        public ChapProcessCommand(
            ChapProcessTime chapProcessTime,
            ArraySegment<byte> chapProcessData)
        {
            ChapProcessTime = chapProcessTime;
            ChapProcessData = chapProcessData;
        }
        /// <summary>
        /// Defines when the process command **SHOULD** be handled
        /// </summary>
        public ChapProcessTime ChapProcessTime { get; set; }
        /// <summary>
        /// Contains the command information.
        /// The data **SHOULD** be interpreted depending on the ChapProcessCodecID value.For ChapProcessCodecID = 1,
        /// the data correspond to the binary DVD cell pre/post commands; see(#menu-features) on DVD menus.
        /// </summary>
        public ArraySegment<byte> ChapProcessData { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var chapProcessCommand = writer.StartMasterElement(Elements.ChapProcessCommand);
            chapProcessCommand.Write(Elements.ChapProcessTime, (ulong)ChapProcessTime);
            chapProcessCommand.Write(Elements.ChapProcessData, ChapProcessData.Array, ChapProcessData.Offset, ChapProcessData.Count);
            chapProcessCommand.Dispose();
        }
    }
}
