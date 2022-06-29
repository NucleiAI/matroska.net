using NEbml.Core;
using System.IO;

namespace Matroska.Net
{
    public class MatroskaBuilder
    {
        private readonly EbmlWriter _writer;
        private readonly MasterBlockWriter _segmentWriter;

        public MatroskaBuilder(Stream stream)
        {
            _writer = new EbmlWriter(stream);
            WriteEbmlHeader();
            _segmentWriter = _writer.StartMasterElement(VInt.MakeId(0x18538067));
        }

        private void WriteEbmlHeader()
        {
            var ebmlHeader = _writer.StartMasterElement(VInt.MakeId(0x1A45DFA3));
            //EBMLVersion Element
            ebmlHeader.Write(VInt.MakeId(0x4286), (ulong)1);
            //EBMLReadVersion Element
            ebmlHeader.Write(VInt.MakeId(0x42F7), (ulong)1);
            //EBMLMaxIDLength Element
            ebmlHeader.Write(VInt.MakeId(0x42F2), (ulong)4);
            //EBMLMaxSizeLength Element
            ebmlHeader.Write(VInt.MakeId(0x42F3), (ulong)8);
            //DocType Element
            ebmlHeader.WriteAscii(VInt.MakeId(0x4282), "matroska");
            //DocTypeVersion Element
            ebmlHeader.Write(VInt.MakeId(0x4287), (ulong)4);
            //DocTypeReadVersion Element
            ebmlHeader.Write(VInt.MakeId(0x4285), (ulong)4);
            ebmlHeader.Dispose();
        }
    }
}