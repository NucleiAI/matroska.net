using NEbml.Core;

namespace Matroska.Net.Segments.SeekHeadElements
{
    /// <summary>
    /// Contains a single seek entry to an EBML Element.
    /// </summary>
    public class Seek
    {
        internal Seek(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.SeekID)
                {
                    SeekId = VInt.FromEncoded(reader.ReadUInt());
                }
                else if( reader.ElementId == Elements.SeekPosition)
                {
                    SeekPosition = reader.ReadUInt();
                }
            }
            reader.LeaveContainer();
        }
        public Seek(VInt seekId, ulong seekPosition)
        {
            SeekId = seekId;
            SeekPosition = seekPosition;
        }
        /// <summary>
        /// The binary ID corresponding to the Element name.
        /// </summary>
        public VInt SeekId { get; set; }
        /// <summary>
        /// The Segment Position of the Element.
        /// </summary>
        public ulong SeekPosition { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var seek = writer.StartMasterElement(Elements.Seek);
            seek.Write(Elements.SeekID, SeekId.EncodedValue);
            seek.Write(Elements.SeekPosition, SeekPosition);
            seek.Dispose();
        }
    }
}
