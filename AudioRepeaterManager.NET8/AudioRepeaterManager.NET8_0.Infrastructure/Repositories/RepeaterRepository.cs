using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AudioRepeaterManager.NET8_0.Domain.Models;
using AudioRepeaterManager.NET8_0.Domain.Shared;
using AudioRepeaterManager.NET8_0.Domain.Structs;
using AudioRepeaterManager.NET8_0.Domain.Repositories;
using System.Reflection;
using System.Collections.Generic;
using AudioSwitcher.AudioApi;

namespace AudioRepeaterManager.NET8_0.Infrastructure.Repositories
{
  public class RepeaterRepository : IRepeaterRepository
  {
    #region Parameters

    /// <summary>
    /// The collection of repeater(s).
    /// </summary>
    private HashSet<RepeaterModel> HashSet { get; set; }

    /// <summary>
    /// The list of IDs.
    /// </summary>
    private List<uint> IdList
    {
      get
      {
        List<uint> idList =
          HashSet
            .Select(x => x.Id)
            .ToList();

        idList.Sort();
        return idList;
      }
    }

    /// <summary>
    /// The max ID.
    /// </summary>
    private uint MaxId
    {
      get
      {
        return Global.MaxRepeaterCount;
      }
    }

    /// <summary>
    /// The next valid ID.
    /// </summary>
    private uint NextId
    {
      get
      {
        uint id = IdList.LastOrDefault();
        id++;
        return id;
      }
    }

    /// <summary>
    /// The list of executables.
    /// </summary>
    public List<string> ExecutableNameList
    {
      get
      {
        return new List<string>
        {
          Global.KSExecutableName,
          Global.MMEExecutableName,
        };
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public RepeaterRepository()
    {
      HashSet = new HashSet<RepeaterModel>();
    }

    /// <summary>
    /// Does repeater contain device.
    /// </summary>
    /// <param name="model">The repeater</param>
    /// <param name="deviceName">The device name</param>
    /// <param name="isInputDevice">True/false is input device</param>
    /// <param name="isOutputDevice">True/false is output device</param>
    /// <returns>True/false does repeater contain device.</returns>
    private bool RepeaterContainsDevice
    (
      RepeaterModel model,
      string deviceName,
      bool isInputDevice,
      bool isOutputDevice
    )
    {
      if
      (
        isInputDevice
        && model.InputDeviceName == deviceName
      )
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Found input device => Name: {0}",
            deviceName
          )
        );

        return true;
      }

      else if
      (
        isOutputDevice
        && model.OutputDeviceName == deviceName
      )
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Found output device => Name: {0}",
            deviceName
          )
        );

        return true;
      }

      else if
      (
        model.InputDeviceName == deviceName
        || model.OutputDeviceName == deviceName
      )
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Found duplex device => Name: {0}",
            deviceName
          )
        );

        return true;
      }

      return false;
    }

    /// <summary>
    /// Get repeater.
    /// </summary>
    /// <param name="id">the repeater ID</param>
    /// <returns>The repeater.</returns>
    public RepeaterModel Get(uint? id)
    {
      if (id is null)
      {
        Debug.WriteLine
        (
          "Failed to get repeater. " +
          "Repeater ID is null."
        );

        return null;
      }

      RepeaterModel model = HashSet
        .FirstOrDefault(x => x.Id == id);

      if (model is null)
      {
        Debug.WriteLine("Repeater is null.");
      }

      else
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Got repeater\t=> Id: {0}",
            model.Id
          )
        );
      }

      return model;
    }

    /// <summary>
    /// Get repeater.
    /// </summary>
    /// <param name="firstDeviceId">The first device ID</param>
    /// <param name="secondDeviceId">The second device ID</param>
    /// <returns>The repeater.</returns>
    public RepeaterModel Get
    (
      uint? firstDeviceId,
      uint? secondDeviceId
    )
    {
      if
      (
        firstDeviceId is null
        && secondDeviceId is null
      )
      {
        Debug.WriteLine
        (
          "Failed to get repeater. " +
          "Either first device ID or second device ID is null."
        );

        return null;
      }

      RepeaterModel model = HashSet
        .FirstOrDefault
        (
          x =>

            x.InputDeviceId == firstDeviceId
            && x.OutputDeviceId == secondDeviceId
           ||
            x.InputDeviceId == secondDeviceId
            && x.OutputDeviceId == firstDeviceId

        );

      if (model is null)
      {
        Debug.WriteLine("Repeater is null.");
      }

      else
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Got repeater\t=> Id: {0}",
            model.Id
          )
        );
      }

      return model;
    }

    /// <summary>
    /// Get the list of audio repeater(s).
    /// </summary>
    /// <returns>The audio repeater list.</returns>
    public List<RepeaterModel> GetAll()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
        (
          "Failed to get the list of audio repeater(s). " +
          "The audio repeater collection is null."
        );

        return new List<RepeaterModel>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got the list of audio repeater(s)\t=> Count: {0}",
          HashSet.Count()
        )
      );

      return HashSet.ToList();
    }

    /// <summary>
    /// Get repeater list.
    /// </summary>
    /// <param name="deviceName">The device name</param>
    /// <param name="isInputDevice">True/false is input device</param>
    /// <param name="isOutputDevice">True/false is output device</param>
    /// <returns>The repeater list.</returns>
    public List<RepeaterModel> GetRange
    (
      string deviceName,
      bool isInputDevice,
      bool isOutputDevice
    )
    {
      if (string.IsNullOrWhiteSpace(deviceName))
      {
        Debug.WriteLine("Failed to get repeater(s).");
        return new List<RepeaterModel>();
      }

      List<RepeaterModel> modelList = new List<RepeaterModel>();

      HashSet
        .ToList()
        .ForEach
        (
          x =>
          {
            if
            (
              RepeaterContainsDevice
              (
                x,
                deviceName,
                isInputDevice,
                isOutputDevice
              )
            )
            {
              modelList.Add(x);
            }
          }
        );

      Debug.WriteLine
      (
        string.Format
        (
          "Got repeater(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get repeater list by device ID.
    /// </summary>
    /// <param name="deviceId">The input or output device ID</param>
    /// <returns>The repeater list.</returns>
    public List<RepeaterModel> GetRangeByDeviceId(uint? deviceId)
    {
      List<RepeaterModel> modelList = new List<RepeaterModel>();

      if
      (
        deviceId is null
        || deviceId < uint.MinValue
      )
      {
        Debug.WriteLine("Failed to get repeater(s).");
        return modelList;
      }

      modelList =
        HashSet
          .Where
          (
            x =>
            x.InputDeviceId == deviceId
            || x.OutputDeviceId == deviceId
          )
          .ToList();

      Debug.WriteLine
      (
        string.Format
        (
          "Got repeater(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get a list of repeater(s).
    /// </summary>
    /// <param name="idList">the repeater ID list</param>
    /// <returns>A list of repeater(s).</returns>
    public List<RepeaterModel> GetRange(List<uint?> idList)
    {
      if
      (
        HashSet is null
        || HashSet.Count == 0
        || idList is null
        || idList.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get repeater(s). " +
          "Repeater ID list is either null or empty, " +
          "or repeater collection is either null or empty."
        );

        return new List<RepeaterModel>();
      }

      List<RepeaterModel> modelList = new List<RepeaterModel>();

      idList
        .ForEach
        (
          id =>
          modelList
            .Add(Get(id))
        );

      Debug.WriteLine
      (
        string.Format
        (
          "Got repeater(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Insert a repeater.
    /// </summary>
    /// <param name="model">The repeater</param>
    public void Insert(RepeaterModel model)
    {
      if (model is null)
      {
        Debug.WriteLine("Failed to insert repeater. Repeater is null.");
        return;
      }

      if (HashSet.Count() >= Global.MaxRepeaterCount)
      {
        Console.WriteLine
        (
          string.Format
          (
            "Failed to insert repeater. Repeater list will exceed maximum of {0}.",
            Global.MaxRepeaterCount
          )
        );

        return;
      }

      uint id = model.Id;

      if (IdList.Contains(id))
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Repeater ID is not valid\t=> Id: {0}",
            id
          )
        );

        id = NextId;
      }

      if (!HashSet.Add(model))
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to insert repeater\t=> Id: {0}",
            id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Inserted repeater\t=> Id: {0}",
          id
        )
      );
    }

    /// <summary>
    /// Add a repeater.
    /// </summary>
    /// <param name="inputDeviceId">The input device ID</param>
    /// <param name="outputDeviceId">The output device ID</param>
    /// <param name="inputDeviceName">The input device name</param>
    /// <param name="outputDeviceName">The output device name</param>
    /// <param name="pathName">The path name</param>

    public void Add
    (
      uint inputDeviceId,
      uint outputDeviceId,
      string inputDeviceName,
      string outputDeviceName,
      string pathName
    )
    {
      RepeaterModel model = new RepeaterModel
        (
          NextId,
          inputDeviceId,
          outputDeviceId,
          inputDeviceName,
          outputDeviceName,
          pathName
        );

      HashSet.Add(model);

      Debug.WriteLine
        (
          string.Format
          (
            "Added repeater\t=> Id: {0}",
            model.Id
          )
        );
    }

    /// <summary>
    /// Add a repeater.
    /// </summary>
    /// <param name="inputDeviceId">The input device ID</param>
    /// <param name="outputDeviceId">The output device ID</param>
    /// <param name="inputDeviceName">The input device name</param>
    /// <param name="outputDeviceName">The output device name</param>
    /// <param name="pathName">The path name</param>
    /// <param name="bitsPerSample">The amount of bits per sample</param>
    /// <param name="bufferAmount">The buffer amount</param>
    /// <param name="bufferDurationMs">The buffer duration in milliseconds</param>
    /// <param name="channelConfig">The channel config</param>
    /// <param name="pathName">The path name</param>
    /// <param name="prefillPercentage">The prefill percentage</param>
    /// <param name="resyncAtPercentage">The resync at percentage</param>
    /// <param name="sampleRateKHz">The sample rate in KiloHertz</param>

    public void Add
    (
      uint inputDeviceId,
      uint outputDeviceId,
      string inputDeviceName,
      string outputDeviceName,
      string pathName,
      byte bitsPerSample,
      byte bufferAmount,
      byte prefillPercentage,
      byte resyncAtPercentage,
      ChannelConfig channelConfig,
      uint sampleRateKHz,
      ushort bufferDurationMs
    )
    {
      RepeaterModel model = new RepeaterModel
        (
          NextId,
          inputDeviceId,
          outputDeviceId,
          inputDeviceName,
          outputDeviceName,
          pathName
      );

      HashSet.Add(model);

      Debug.WriteLine
        (
          string.Format
          (
            "Added repeater\t=> Id: {0}",
            model.Id
          )
        );
    }

    /// <summary>
    /// Insert a repeater.
    /// </summary>
    /// <param name="id">The repeater ID</param>
    /// <param name="inputDeviceId">The input device ID</param>
    /// <param name="outputDeviceId">The output device ID</param>
    /// <param name="inputDeviceName">The input device name</param>
    /// <param name="outputDeviceName">The output device name</param>
    /// <param name="pathName">The path name</param>
    /// <param name="bitsPerSample">The amount of bits per sample</param>
    /// <param name="bufferAmount">The buffer amount</param>
    /// <param name="bufferDurationMs">The buffer duration in milliseconds</param>
    /// <param name="channelConfig">The channel config</param>
    /// <param name="pathName">The path name</param>
    /// <param name="prefillPercentage">The prefill percentage</param>
    /// <param name="resyncAtPercentage">The resync at percentage</param>
    /// <param name="sampleRateKHz">The sample rate in KiloHertz</param>
    public void Insert
    (
      uint id,
      uint inputDeviceId,
      uint outputDeviceId,
      string inputDeviceName,
      string outputDeviceName,
      string pathName,
      byte bitsPerSample,
      byte bufferAmount,
      byte prefillPercentage,
      byte resyncAtPercentage,
      ChannelConfig channelConfig,
      uint sampleRateKHz,
      ushort bufferDurationMs
    )
    {
      RepeaterModel model = new RepeaterModel
      (
        id,
        inputDeviceId,
        outputDeviceId,
        inputDeviceName,
        outputDeviceName,
        pathName
      )
      {
        BitsPerSample = bitsPerSample,
        BufferAmount = bufferAmount,
        PrefillPercentage = prefillPercentage,
        ResyncAtPercentage = resyncAtPercentage,
        ChannelConfig = channelConfig,
        SampleRateKHz = sampleRateKHz,
        BufferDurationMs = bufferDurationMs
      };

      Insert(model);
    }

    /// <summary>
    /// Insert a repeater.
    /// </summary>
    /// <param name="id">The repeater ID</param>
    /// <param name="inputDeviceId">The input device ID</param>
    /// <param name="outputDeviceId">The output device ID</param>
    /// <param name="bitsPerSample">The amount of bits per sample</param>
    /// <param name="bufferAmount">The buffer amount</param>
    /// <param name="bufferDurationMs">The buffer duration in milliseconds</param>
    /// <param name="channelMask">The channel mask</param>
    /// <param name="pathName">The path name</param>
    /// <param name="prefillPercentage">The prefill percentage</param>
    /// <param name="resyncAtPercentage">The resync at percentage</param>
    /// <param name="sampleRateKHz">The sample rate in KiloHertz</param>
    /// <param name="windowName">The window name</param>
    public void Insert
    (
      uint id,
      uint inputDeviceId,
      uint outputDeviceId,
      byte bitsPerSample,
      byte bufferAmount,
      byte prefillPercentage,
      byte resyncAtPercentage,
      string inputDeviceName,
      string outputDeviceName,
      string pathName,
      uint channelMask,
      uint sampleRateKHz,
      ushort bufferDurationMs
    )
    {
      RepeaterModel model = new RepeaterModel
      (
        id,
        inputDeviceId,
        outputDeviceId,
        inputDeviceName,
        outputDeviceName,
        pathName
      )
      {
        BitsPerSample = bitsPerSample,
        BufferAmount = bufferAmount,
        PrefillPercentage = prefillPercentage,
        ResyncAtPercentage = resyncAtPercentage,
        ChannelMask = channelMask,
        SampleRateKHz = sampleRateKHz,
        BufferDurationMs = bufferDurationMs
      };

      Insert(model);
    }

    /// <summary>
    /// Remove the audio repeater.
    /// </summary>
    /// <param name="id">The repeater ID</param>
    public void Remove(uint? id)
    {
      if (id is null)
      {
        Debug.WriteLine
        (
          "Failed to remove the audio repeater. " +
          "The ID is null."
        );

        return;
      }

      int count = HashSet.RemoveWhere(x => x.Id == id);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove the audio repeater. " +
            "The audio repeater does not exist\t=> ID: {0}",
            id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed the audio repeater\t=> ID: {0}",
          id
        )
      );
    }

    /// <summary>
    /// Remove the audio repeater.
    /// </summary>
    /// <param name="firstDeviceId">The first audio device ID</param>
    /// <param name="secondDeviceId">The second audio device ID</param>
    public void Remove
    (
      uint? firstDeviceId,
      uint? secondDeviceId
    )
    {
      RepeaterModel model = Get
      (
        firstDeviceId,
        secondDeviceId
      );

      if (model is null)
      {
        Debug.WriteLine
        (
          "Failed to remove the audio repeater. " +
          "Repeater is null."
        );

        return;
      }

      int count = HashSet.RemoveWhere(x => x.Id == model.Id);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove the audio repeater(s). " +
            "No match(es) found" +
            "\t=> First Device ID: {0}, Second Device ID: {1}",
            firstDeviceId,
            secondDeviceId
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed the audio repeater(s)\t=> Count: {0}",
          count
        )
      );
    }

    /// <summary>
    /// Remove the list of audio repeater(s).
    /// </summary>
    /// <param name="idList">the ID list</param>
    public void RemoveRange(List<uint?> idList)
    {
      if
      (
        idList is null
        || idList.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to remove the audio repeater(s)." +
          "The ID list is either empty or null."
        );

        return;
      }

      int count = 0;

      idList
        .ForEach
        (
          x =>
          {
            HashSet.RemoveWhere
            (
              y =>
              {
                if (y.Id != x)
                {
                  return false;
                }

                Remove(x);
                count++;
                return true;
              }
            );
          }
        );

      if (idList.Count == 0)
      {
        Debug.WriteLine
        (
          "Failed to remove the audio repeater(s). " +
          "No match(es) found."
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed the audio repeater(s)\t=> Count: {0}",
          idList.Count
        )
      );
    }

    /// <summary>
    /// Remove the list of audio repeater(s).
    /// </summary>
    /// <param name="deviceName">The input or output audio device name</param>
    public void RemoveRange(string deviceName)
    {
      if (string.IsNullOrWhiteSpace(deviceName))
      {
        Debug.WriteLine
        (
          "Failed to remove the audio repeater. " +
          "The input or output audio device name is null or whitespace."
        );

        return;
      }

      List<uint?> idList = HashSet
        .Where
        (
          x =>
          {
            return x.InputDeviceName == deviceName
              || x.OutputDeviceName == deviceName;
          }
        ).Select(y => (uint?)y.Id)
        .ToList();

      RemoveRange(idList);
    }

    /// <summary>
    /// Remove the list of audio repeater(s).
    /// </summary>
    /// <param name="deviceId">The list of input or output audio device ID(s)</param>
    public void RemoveRangeByDeviceId(List<uint?> deviceIdList)
    {
      if
      (
        deviceIdList is null
        || deviceIdList.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to remove the audio repeater(s)." +
          "The input or output audio device ID list is either empty or null."
        );

        return;
      }

      deviceIdList
        .ForEach
        (
          x =>
          {
            List<uint?> idList =
              HashSet
              .Where
              (
                y =>
                {
                  return y.InputDeviceId == x
                || y.OutputDeviceId == x;
                }
              ).Select(y => (uint?)y.Id)
              .ToList();

            RemoveRange(idList);
          }
        );
    }

    /// <summary>
    /// Remove the list of audio repeater(s).
    /// </summary>
    /// <param name="deviceId">The input or output audio device ID</param>
    public void RemoveRangeByDeviceId(uint? deviceId)
    {
      if (deviceId is null)
      {
        Debug.WriteLine
        (
          "Failed to remove the audio repeater. " +
          "The input or output audio device ID is null."
        );

        return;
      }

      List<uint?> idList = HashSet
        .Where
        (
          x =>
          {
            return x.InputDeviceId == deviceId
              || x.OutputDeviceId == deviceId;
          }
        ).Select(y => (uint?)y.Id)
        .ToList();

      RemoveRange(idList);
    }

    /// <summary>
    /// Update the audio repeater.
    /// </summary>
    /// <param name="model">the audio repeater</param>
    public void Update(RepeaterModel model)
    {
      if (model is null)
      {
        Debug.WriteLine
        (
          "Failed to update the audio repeater. " +
          "The audio repeater is null."
        );

        return;
      }

      if (HashSet.RemoveWhere(x => x.Id == model.Id) == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update the audio repeater. " +
            "The audio repeater does not exist\t=> ID: {0}",
            model.Id
          )
        );

        return;
      }

      if (!HashSet.Add(model))
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update the audio repeater\t=> ID: {0}",
            model.Id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Updated the audio repeater\t=> ID: {0}",
          model.Id
        )
      );
    }

    /// <summary>
    /// Update the list of audio repeater(s).
    /// </summary>
    /// <param name="modelList">the list of audio repeater(s)</param>
    public void UpdateRange(List<RepeaterModel> modelList)
    {
      if
      (
        modelList is null
        || modelList.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to update the audio repeater(s)." +
          "The audio repeater list is either empty or null."
        );

        return;
      }

      List<uint> idList = new List<uint>();

      modelList
        .ForEach
        (
          x =>
          {
            bool hasMatch = HashSet
              .Select(y => y.Id == x.Id)
              .Any();

            if (hasMatch)
            {
              HashSet.RemoveWhere(y => y.Id == x.Id);
              idList.Add(x.Id);
            }
          }
        );

      idList
        .ForEach
        (
          x =>
          {
            RepeaterModel model = modelList.First(y => y.Id == x);
            HashSet.Add(model);

            Debug.WriteLine
              (
                string.Format
                (
                  "Updated the audio repeater\t=> ID: {0}",
                  x
                )
              );
          }
        );

      if (idList.Count == 0)
      {
        Debug.WriteLine
        (
          "Failed to remove the audio repeater(s). " +
          "No match(es) found."
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed the audio repeater(s)\t=> Count: {0}",
          idList.Count
        )
      );
    }

    #endregion
  }
}