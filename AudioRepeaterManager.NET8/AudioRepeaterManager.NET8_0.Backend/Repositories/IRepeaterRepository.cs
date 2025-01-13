using AudioRepeaterManager.NET8_0.Backend.Models;

namespace AudioRepeaterManager.NET8_0.Backend.Repositories
{
  public interface IRepeaterRepository
  {
    #region Logic

    RepeaterModel Get(uint? id);

    RepeaterModel Get
    (
      uint? firstDeviceId,
      uint? secondDeviceId
    );

    List<RepeaterModel> GetAll();
    List<RepeaterModel> GetAllStarted();
    List<RepeaterModel> GetAllStopped();

    List<RepeaterModel> GetRange
    (
      string deviceName,
      bool isInputDevice,
      bool isOutputDevice
    );

    List<RepeaterModel> GetRange(List<uint?> idList);
    Task<int> Restart(RepeaterModel model);
    Task<int> RestartByDeviceId(uint id);
    Task<int> RestartByRepeaterId(uint id);
    Task<int> RestartRange(List<RepeaterModel> model);
    Task<int> RestartRangeByDeviceId(List<uint?> idList);
    Task<int> RestartRangeByRepeaterId(List<uint?> idList);
    Task<int> Start(RepeaterModel model);
    Task<int> StartByDeviceId(uint id);
    Task<int> StartByRepeaterId(uint id);
    Task<int> StartRange(List<RepeaterModel> model);
    Task<int> StartRangeByDeviceId(List<uint?> idList);
    Task<int> StartRangeByRepeaterId(List<uint?> idList);
    Task<int> Stop(RepeaterModel model);
    Task<int> StopByDeviceId(uint id);
    Task<int> StopByRepeaterId(uint id);
    Task<int> StopRange(List<RepeaterModel> model);
    Task<int> StopRangeByDeviceId(List<uint?> idList);
    Task<int> StopRangeByRepeaterId(List<uint?> idList);

    void Insert(RepeaterModel model);

    void Insert
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
    );

    void Remove
    (
      uint? id
    );

    void Remove
    (
      uint? firstDeviceId,
      uint? secondDeviceId
    );

    void RemoveRange
    (
      string deviceName
    );

    void Update(RepeaterModel model);

    void Update
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
    );

    #endregion
  }
}