namespace Matroska.Net.Segments.TracksElements.TrackEntryElements.ContentEncodingsElements.ContentEncodingElements
{
    public enum ContentEncAlgo : ulong
    {
        NotEncrypted = 0,
        /// <summary>
        /// Data Encryption Standard (DES) [@!FIPS.46-3].
        /// </summary>
        DES = 1,
        /// <summary>
        /// Triple Data Encryption Algorithm [@!SP.800-67].
        /// </summary>
        DES3 = 2,
        /// <summary>
        /// Twofish Encryption Algorithm [@!Twofish].
        /// </summary>
        Twofish = 3,
        /// <summary>
        /// Blowfish Encryption Algorithm [@!Blowfish].
        /// </summary>
        Blowfish = 4,
        /// <summary>
        /// Advanced Encryption Standard (AES) [@!FIPS.197].
        /// </summary>
        AES = 5
    }
}
