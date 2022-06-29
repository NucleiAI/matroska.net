using Matroska.Net.Segments.ChaptersElements.EditionEntryElements.ChapterAtomElements.ChapProcessElements;
using NEbml.Core;
using System;
using System.Collections.Generic;

namespace Matroska.Net.Segments.ChaptersElements.EditionEntryElements.ChapterAtomElements
{
    /// <summary>
    /// Contains all the commands associated to the Atom.
    /// </summary>
    public class ChapProcess
    {
        internal ChapProcess(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<ChapProcessCommand>? chapProcessCommands = null;
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.ChapProcessCodecID)
                {
                    ChapProcessCodecID = reader.ReadUInt();
                }
                else if(reader.ElementId == Elements.ChapProcessPrivate)
                {
                    var data = new byte[reader.ElementSize];
                    var read = reader.ReadBinary(data, 0, data.Length);
                    ChapProcessPrivate = new ArraySegment<byte>(data, 0, read);
                }
                else if(reader.ElementId == Elements.ChapProcessCommand)
                {
                    if(chapProcessCommands == null)
                    {
                        chapProcessCommands = new List<ChapProcessCommand>();
                    }
                    chapProcessCommands.Add(new ChapProcessCommand(reader, version));
                }
            }
            ChapProcessCommands = chapProcessCommands;
            reader.LeaveContainer();
        }
        public ChapProcess(
            ulong chapProcessCodecID = 0,
            ArraySegment<byte>? chapProcessPrivate = null,
            IEnumerable<ChapProcessCommand>? chapProcessCommands = null)
        {
            ChapProcessCodecID = chapProcessCodecID;
            ChapProcessPrivate = chapProcessPrivate;
            ChapProcessCommands = chapProcessCommands;
        }
        /// <summary>
        /// Contains the type of the codec used for the processing.
        /// A value of 0 means native Matroska processing(to be defined), a value of 1 means the DVD command set is used; see(#menu-features) on DVD menus.
        /// More codec IDs can be added later.
        /// </summary>
        public ulong ChapProcessCodecID { get; set; }
        /// <summary>
        /// Some optional data attached to the ChapProcessCodecID information.
        /// For ChapProcessCodecID = 1, it is the "DVD level" equivalent; see(#menu-features) on DVD menus.
        /// </summary>
        public ArraySegment<byte>? ChapProcessPrivate { get; set; }
        /// <summary>
        /// Contains all the commands associated to the Atom.
        /// </summary>
        public IEnumerable<ChapProcessCommand>? ChapProcessCommands { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var chapProcess = writer.StartMasterElement(Elements.ChapProcess);
            chapProcess.Write(Elements.ChapProcessCodecID, ChapProcessCodecID);
            if (ChapProcessPrivate.HasValue)
            {
                chapProcess.Write(Elements.ChapProcessPrivate, ChapProcessPrivate.Value.Array, ChapProcessPrivate.Value.Offset, ChapProcessPrivate.Value.Count);
            }
            if(ChapProcessCommands != null)
            {
                foreach (var chapProcessCommand in ChapProcessCommands)
                {
                    chapProcessCommand.WriteTo(chapProcess);
                }
            }
            chapProcess.Dispose();
        }
    }
}
