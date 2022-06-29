namespace Matroska.Net
{
    public enum BlockLacing : byte
    {
        NoLacing = 0b00000000,
        XiphLacing = 0b00000010,
        EBMLLacing = 0b00000110,
        FixedSizeLacing = 0b00000100
    }
}
