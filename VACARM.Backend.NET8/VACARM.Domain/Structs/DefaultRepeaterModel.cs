using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VACARM.Domain.Enums;

namespace VACARM.Domain.Structs
{
  public struct DefaultRepeaterModel
  {
    #region Parameters

    /// <summary>
    /// Enumerable of the Channel Config.
    /// </summary>
    public IEnumerable<ChannelConfig> ChannelConfigEnum
    {
      get
      {
        return Enum
          .GetValues(typeof(ChannelConfig))
          .Cast<ChannelConfig>();
      }
    }

    public static byte BitsPerSample
    {
      get
      {
        return BitsPerSampleOptions[2];
      }
    }

    public static byte BufferAmount
    {
      get
      {
        return BufferAmountOptions[2];
      }
    }

    public static byte PrefillPercentage
    {
      get
      {
        return PrefillPercentageOptions[2];
      }
    }

    public static byte ResyncAtPercentage
    {
      get
      {
        return ResyncAtPercentageOptions[2];
      }
    }

    public static uint SampleRateKHz
    {
      get
      {
        return SampleRateKHzOptions[2];
      }
    }

    public static ushort BufferDurationMs
    {
      get
      {
        return BufferDurationMsOptions[2];
      }
    }

    public static ChannelConfig ChannelConfig
    {
      get
      {
        return ChannelConfig.Stereo;
      }
    }

    /// <summary>
    /// Available choices for BitsPerSample.
    /// </summary>
    public static ReadOnlyCollection<byte> BitsPerSampleOptions =
      new ReadOnlyCollection<byte>
      (
        new byte[]
        {
          8,
          16,
          18,
          20,
          22,
          24,
          32
        }
      );

    /// <summary>
    /// Available choices for Buffer.
    /// </summary>
    public static ReadOnlyCollection<byte> BufferAmountOptions =
      new ReadOnlyCollection<byte>
      (
        new byte[]
        {
          2,
          4,
          8,
          12,
          16,
          20,
          24,
          32
        }
      );

    /// <summary>
    /// Available choices for Prefill percentage.
    /// </summary>
    public static ReadOnlyCollection<byte> PrefillPercentageOptions =
      new ReadOnlyCollection<byte>
      (
        new byte[]
        {
          0,
          20,
          50,
          70,
          100
        }
      );

    /// <summary>
    /// Available choices for ResyncAt percentage.
    /// </summary>
    public static ReadOnlyCollection<byte> ResyncAtPercentageOptions =
      new ReadOnlyCollection<byte>(
        new byte[]
        {
          0,
          10,
          15,
          20,
          25,
          30,
          40,
          50
        }
      );

    /// <summary>
    /// Available choices for sample rate in KiloHertz.
    /// </summary>
    public static ReadOnlyCollection<uint> SampleRateKHzOptions =
      new ReadOnlyCollection<uint>(
        new uint[]
        {
          5000,
          8000,
          11025,
          22050,
          44100,
          48000,
          96000,
          192000
        }
      );

    /// <summary>
    /// Available choices for Buffer time in milliseconds.
    /// </summary>
    public static ReadOnlyCollection<ushort> BufferDurationMsOptions =
      new ReadOnlyCollection<ushort>
      (
        new ushort[]
        {
          20,
          50,
          100,
          200,
          400,
          800,
          1000,
          2000,
          4000,
          8000
        }
      );

    #endregion
  }
}