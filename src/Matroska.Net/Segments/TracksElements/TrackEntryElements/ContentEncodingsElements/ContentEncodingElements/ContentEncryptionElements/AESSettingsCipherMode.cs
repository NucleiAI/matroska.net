namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.ContentEncodingsElements.ContentEncodingElements.ContentEncryptionElements
{
    public enum AESSettingsCipherMode : ulong
    {
        /// <summary>
        /// Counter [@!SP.800-38A].
        /// </summary>
        AESCTR = 1,
        /// <summary>
        /// Cipher Block Chaining [@!SP.800-38A].
        /// </summary>
        AESCBC = 2
    }
}
