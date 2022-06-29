using NEbml.Core;

namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.ContentEncodingsElements.ContentEncodingElements.ContentEncryptionElements
{
    /// <summary>
    /// Settings describing the encryption algorithm used.
    /// It** MUST** be ignored if `ContentEncAlgo` is not AES(5).
    /// </summary>
    public class ContentEncAESSettings
    {
        internal ContentEncAESSettings(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.AESSettingsCipherMode)
                {
                    AESSettingsCipherMode = (AESSettingsCipherMode)reader.ReadUInt();
                }
            }
            reader.LeaveContainer();
        }
        public ContentEncAESSettings(
            AESSettingsCipherMode aESSettingsCipherMode)
        {
            AESSettingsCipherMode = aESSettingsCipherMode;
        }
        /// <summary>
        /// The AES cipher mode used in the encryption.
        /// It** MUST** be ignored if `ContentEncAlgo` is not AES(5).
        /// </summary>
        public AESSettingsCipherMode AESSettingsCipherMode { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var contentEncAESSettings = writer.StartMasterElement(Elements.ContentEncAESSettings);
            contentEncAESSettings.Write(Elements.AESSettingsCipherMode, (ulong)AESSettingsCipherMode);
            contentEncAESSettings.Dispose();
        }
    }
}
