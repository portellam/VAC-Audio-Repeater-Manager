using AudioRepeaterManager.NET8_0.Domain.Models;
using AudioRepeaterManager.NET8_0.Domain.Structs;

namespace AudioRepeaterManager.NET8_0.Domain.Repositories
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
    List<RepeaterModel> GetAllDisabled();
    List<RepeaterModel> GetAllEnabled();
    List<RepeaterModel> GetAllStarted();
    List<RepeaterModel> GetAllStopped();
    List<RepeaterModel> GetRange(string deviceName);
    List<RepeaterModel> GetRange(uint? id);
    List<RepeaterModel> GetRange(List<string> deviceNameList);
    List<RepeaterModel> GetRange(List<uint?> idList);
    List<RepeaterModel> GetRangeByDeviceId(List<uint?> deviceIdList);
    Task<int> Restart(RepeaterModel model);
    Task<int> Restart(uint? id);
    Task<int> Restart(KeyValuePair<uint?, uint?> inputAndOutputDeviceId);
    Task<int> RestartRange(List<RepeaterModel> model);
    Task<int> RestartRange(List<uint?> idList);
    Task<int> RestartRange(KeyValuePair<uint?, uint?> inputAndOutputDeviceId);
    Task<int> Start(RepeaterModel model);
    Task<int> Start(uint? id);
    Task<int> Start(KeyValuePair<uint?, uint?> inputAndOutputDeviceId);
    Task<int> StartRange(List<RepeaterModel> model);
    Task<int> StartRange(List<uint?> idList);
    Task<int> StartRange(KeyValuePair<uint?, uint?> inputAndOutputDeviceId);
    Task<int> Stop(RepeaterModel model);
    Task<int> Stop(uint? id);
    Task<int> Stop(KeyValuePair<uint?, uint?> inputAndOutputDeviceId);
    Task<int> StopRange(List<RepeaterModel> model);
    Task<int> StopRange(List<uint?> idList);
    Task<int> StopRange(KeyValuePair<uint?, uint?> inputAndOutputDeviceId);

    void Add
    (
      uint? inputDeviceId,
      uint? outputDeviceId,
      string inputDeviceName,
      string outputDeviceName,
      string pathName
    );

    void Add
    (
      uint? inputDeviceId,
      uint? outputDeviceId,
      string inputDeviceName,
      string outputDeviceName,
      string pathName,
      byte bitsPerSample,
      byte bufferAmount,
      byte prefillPercentage,
      byte resyncAtPercentage,
      ChannelConfig channelConfig,
      uint? sampleRateKHz,
      ushort bufferDurationMs
    );

    void AddRange(List<RepeaterModel> modelList);

    void Insert(RepeaterModel model);
    void InsertRange(List<RepeaterModel> modelList);
    void Remove(uint? id);

    void Remove
    (
      uint? firstDeviceId,
      uint? secondDeviceId
    );

    void RemoveRange(List<uint?> idList);
    void RemoveRangeByDeviceId(uint? deviceId);
    void RemoveRangeByDeviceId(List<uint?> deviceIdList);
    void Update(RepeaterModel model);
    void UpdateRange(List<RepeaterModel> modelList);

    #endregion
  }
}