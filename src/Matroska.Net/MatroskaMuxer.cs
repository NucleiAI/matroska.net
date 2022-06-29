using NEbml.Core;
using System.IO;

namespace Matroska.Net
{
    public static class MatroskaMuxer
    {
        public static void MuxDocumentToStream(MatroskaDocument doc, Stream stream)
        {
            var ebmlWriter = new EbmlWriter(stream);
            var ebmlHeader = ebmlWriter.StartMasterElement(Elements.EBMLHeader);
            ebmlHeader.Write(Elements.EBMLVersion, (ulong)1);
            ebmlHeader.Write(Elements.EBMLReadVersion, (ulong)1);
            ebmlHeader.Write(Elements.EBMLMaxIDLength, (ulong)4);
            ebmlHeader.Write(Elements.EBMLMaxSizeLength, (ulong)8); ;
            ebmlHeader.WriteAscii(Elements.DocType, "matroska");
            ebmlHeader.Write(Elements.DocTypeVersion, (ulong)4);
            ebmlHeader.Write(Elements.DocTypeReadVersion, (ulong)2);
            ebmlHeader.Dispose();           
            doc.WriteTo(ebmlWriter);
        }
    }
}
