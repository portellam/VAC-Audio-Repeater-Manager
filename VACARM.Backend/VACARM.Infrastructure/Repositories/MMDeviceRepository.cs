using System.Diagnostics.CodeAnalysis;
using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public class MMDeviceRepository : IMMDeviceRepository
  {
    #region Parameters

    /// <summary>
    /// The list of audio devices.
    /// </summary>
    private List<MMDevice> List { get; set; } = new List<MMDevice>();

    /// <summary>
    /// The enumerator of audio devices.
    /// </summary>
    private MMDeviceEnumerator Enumerator { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public MMDeviceRepository()
    {
      Enumerator = new MMDeviceEnumerator();
    }

    public bool IsStarted(MMDevice? model)
    {
      if (model == null)
      {
        return false;
      }

      DeviceState deviceState = model.State;
      return deviceState != DeviceState.Disabled;
    }

    public bool IsPresent(MMDevice? model)
    {
      if (model == null)
      {
        return false;
      }

      DeviceState deviceState = model.State;
      return deviceState == DeviceState.Active
        || deviceState == DeviceState.Disabled
        || deviceState == DeviceState.Unplugged;
    }

    public MMDevice? Get(Func<MMDevice, bool> func)
    {
      if (func == null)
      {
        return null;
      }

      return List.FirstOrDefault(x => func(x));
    }

    public MMDevice? Get(string id)
    {
      Func<MMDevice, bool> func = (MMDevice x) => x.ID == id;
      return Get(func);
    }

    public IEnumerable<MMDevice> GetAll()
    {
      return List.AsEnumerable();
    }

    public IEnumerable<MMDevice> GetRange(Func<MMDevice, bool> func)
    {
      return List
        .Where(x => func(x))
        .AsEnumerable();
    }

    public List<MMDevice> GetAllAbsent()
    {
      Func<MMDevice, bool> func = (MMDevice x) => !IsPresent(x);

      return GetRange(func)
        .ToList();
    }

    public List<MMDevice> GetAllPresent()
    {
      Func<MMDevice, bool> func = (MMDevice x) => IsPresent(x);

      return GetRange(func)
        .ToList();
    }

    public List<MMDevice> GetAllStarted()
    {
      Func<MMDevice, bool> func = (MMDevice x) => IsStarted(x);

      return GetRange(func)
        .ToList();
    }

    public List<MMDevice> GetAllStopped()
    {
      Func<MMDevice, bool> func = (MMDevice x) => !IsStarted(x);

      return GetRange(func)
        .ToList();
    }

    public List<MMDevice> GetRange(List<string> idList)
    {
      List<MMDevice> modelList = new List<MMDevice>();

      idList.ForEach
        (
          x =>
          {

            MMDevice? model = Get(x);

            if (model != null)
            {
              modelList.Add(model);
            }
          }
        );

      return modelList;
    }

    #endregion
  }
}