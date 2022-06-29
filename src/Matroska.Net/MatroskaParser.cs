using NEbml.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matroska.Net
{
    public static class MatroskaParser
    {
        public static MatroskaDocument ParseStream(Stream stream)
        {
            var reader = new EbmlReader(stream);
            var isHeader = reader.ReadNext();
            if (!isHeader || reader.ElementId != VInt.FromEncoded(0x1A45DFA3))
            {
                throw new InvalidOperationException(" Invalid Stream, The Document does not start with an EBML Header");
            }
            reader.EnterContainer();
            ulong? version = null;
            ulong? readVersion = null;
            ulong? maxIDLength = null;
            ulong? maxSizeLength = null;
            string? docType = null;
            ulong? docTypeVersion = null;
            ulong? docTypeReadVersion = null;
            while (reader.ReadNext())
            {
                if (reader.ElementId == VInt.FromEncoded(0x4286))
                {
                    version = reader.ReadUInt();
                }
                else if (reader.ElementId == VInt.FromEncoded(0x42F7))
                {
                    readVersion = reader.ReadUInt();
                }
                else if (reader.ElementId == VInt.FromEncoded(0x42F2))
                {
                    maxIDLength = reader.ReadUInt();
                }
                else if (reader.ElementId == VInt.FromEncoded(0x42F3))
                {
                    maxSizeLength = reader.ReadUInt();
                }
                else if (reader.ElementId == VInt.FromEncoded(0x4282))
                {
                    docType = reader.ReadAscii();
                }
                else if(reader.ElementId == VInt.FromEncoded(0x4287))
                {
                    docTypeVersion = reader.ReadUInt();
                }
                else if(reader.ElementId == VInt.FromEncoded(0x4285))
                {
                    docTypeReadVersion = reader.ReadUInt();
                }
            }
            reader.LeaveContainer();
            if(!version.HasValue || !readVersion.HasValue || !maxIDLength.HasValue || !maxSizeLength.HasValue || docType == null || !docType.Equals("matroska",StringComparison.OrdinalIgnoreCase) || !docTypeVersion.HasValue || !docTypeReadVersion.HasValue)
            {
                throw new InvalidOperationException("The EBML Header of the Data is invalid for the Matroska parser");
            }
            if(!reader.ReadNext() || reader.ElementId != VInt.FromEncoded(0x18538067))
            {
                throw new InvalidOperationException("The Matroska segment Element is missing");
            }
            return new MatroskaDocument(reader, docTypeReadVersion.Value);
        }
    }
}
