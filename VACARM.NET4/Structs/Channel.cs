namespace VACARM.NET4.Structs
{
    /// <summary>
    /// The masks of individual speakers/channels.
    /// </summary>
    public enum Channel
    {
        FL = 0x1,
        FR = 0x2,
        FC = 0x4,
        LF = 0x8,
        BL = 0x10,
        BR = 0x20,
        FLC = 0x40,
        FRC = 0x80,
        BC = 0x100,
        SL = 0x200,
        SR = 0x400
    }
}