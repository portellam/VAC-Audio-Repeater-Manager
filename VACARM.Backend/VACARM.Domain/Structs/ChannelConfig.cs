namespace VACARM.Domain.Structs
{
  /// <summary>
  /// The masks of speaker layout/channel amounts.
  /// </summary>
  public enum ChannelConfig
  {
    Custom = -1,
    Mono = 0x4,
    Stereo = 0x3,
    Quadraphonic = 0x33,
    Surround = 0x107,
    Back51 = 0x3F,
    Surround51 = 0x60F,
    Wide71 = 0xFF,
    Surround71 = 0x63F
  }
}