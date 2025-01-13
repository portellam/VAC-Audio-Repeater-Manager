﻿using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AudioRepeaterManager.NET8_0.Backend.Models;

namespace AudioRepeaterManager.NET8_0.Backend.Repositories
{
  public class RepeaterRepository :
    INotifyPropertyChanged,
    IRepeaterRepository
  {
    #region Parameters

    /// <summary>
    /// The collection of repeaters.
    /// </summary>
    private HashSet<RepeaterModel> HashSet;

    /// <summary>
    /// The list of executables.
    /// </summary>
    private List<string> ExecutableNameList
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
          (
            x.InputDeviceId == firstDeviceId
            && x.OutputDeviceId == secondDeviceId
          ) || (
            x.InputDeviceId == secondDeviceId
            && x.OutputDeviceId == firstDeviceId
          )
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
    /// Get repeater list.
    /// </summary>
    /// <returns>The repeater list.</returns>
    public List<RepeaterModel> GetAll()
    {
      if (HashSet is null)
      {
        Debug.WriteLine("Failed to get repeater(s). Repeater collection is null.");
        return new List<RepeaterModel>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got repeater(s) => Count: {0}",
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
    /// Get a list of repeaters.
    /// </summary>
    /// <param name="idList">the repeater ID list</param>
    /// <returns>A list of repeaters.</returns>
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
      string windowName,
      uint channelMask,
      uint sampleRateKHz,
      ushort bufferDurationMs
    )
    {
      RepeaterModel model = new RepeaterModel
      (
        id,
        inputDeviceId,
        outputDeviceId
      )
      {
        BitsPerSample = bitsPerSample,
        BufferAmount = bufferAmount,
        PrefillPercentage = prefillPercentage,
        ResyncAtPercentage = resyncAtPercentage,
        InputDeviceName = inputDeviceName,
        OutputDeviceName = outputDeviceName,
        PathName = pathName,
        ChannelMask = channelMask,
        SampleRateKHz = sampleRateKHz,
        BufferDurationMs = bufferDurationMs
      };

      Insert(model);
    }

    /// <summary>
    /// Remove a repeater.
    /// </summary>
    /// <param name="id">The repeater ID</param>
    public void Remove(uint? id)
    {
      if (id is null)
      {
        Debug.WriteLine("Failed to remove repeater. Repeater ID is null.");
        return;
      }

      int count = HashSet
        .RemoveWhere(x => x.Id == id);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove repeater. Repeater does not exist\t=> Id: {0}",
            id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed repeater\t=> Id: {0}",
          id
        )
      );
    }

    /// <summary>
    /// Remove a repeater.
    /// </summary>
    /// <param name="firstDeviceId">The first device ID</param>
    /// <param name="secondDeviceId">The second device ID</param>
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
        Debug.WriteLine("Failed to remove repeater. Repeater is null.");
        return;
      }

      int count = HashSet
        .RemoveWhere(x => x.Id == model.Id);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove Repeater. " +
            "Repeater does not exist\t=> FirstDeviceId: {0}, SecondDeviceId {1}",
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
          "Removed repeater(s)\t=> Count: {0}",
          count
        )
      );
    }

    /// <summary>
    /// Remove a list of repeaters.
    /// </summary>
    /// <param name="deviceName">The input or output device name</param>
    public void RemoveRange(string deviceName)
    {
      if (string.IsNullOrWhiteSpace(deviceName))
      {
        Debug.WriteLine
        (
          "Failed to remove repeater. " +
          "Input or output device name is null or whitespace."
        );

        return;
      }

      int count = HashSet
        .RemoveWhere
        (
          x =>
          x.InputDeviceName == deviceName
          || x.OutputDeviceName == deviceName
        );

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove repeater. " +
            "Repeater name does not exist\t=> DeviceName: {0}",
            deviceName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed repeater(s)\t=> Count: {0}",
          count
        )
      );
    }

    /// <summary>
    /// Update a repeater.
    /// </summary>
    /// <param name="model">The repeater to update.</param>
    public void Update(RepeaterModel model)
    {
      if (model is null)
      {
        Debug.WriteLine("Failed to update repeater. Repeater is null.");
        return;
      }

      if
      (
        HashSet
          .RemoveWhere
          (x => x.Id == model.Id) == 0
      )
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update repeater. Repeater does not exist\t=> Id: {0}",
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
            "Failed to update repeater\t=> Id: {0}",
            model.Id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Updated repeater\t=> Id: {0}",
          model.Id
        )
      );
    }

    /// <summary>
    /// Update a repeater.
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
    public void Update
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
      string windowName,
      uint channelMask,
      uint sampleRateKHz,
      ushort bufferDurationMs
    )
    {
      RepeaterModel model = new RepeaterModel
      (
        id,
        inputDeviceId,
        outputDeviceId
      )
      {
        BitsPerSample = bitsPerSample,
        BufferAmount = bufferAmount,
        PrefillPercentage = prefillPercentage,
        ResyncAtPercentage = resyncAtPercentage,
        InputDeviceName = inputDeviceName,
        OutputDeviceName = outputDeviceName,
        PathName = pathName,
        ChannelMask = channelMask,
        SampleRateKHz = sampleRateKHz,
        BufferDurationMs = bufferDurationMs
      };

      Update(model);
    }

    #endregion
  }
}