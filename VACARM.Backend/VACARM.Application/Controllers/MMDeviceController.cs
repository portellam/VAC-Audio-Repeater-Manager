using NAudio.CoreAudioApi;
using VACARM.Application.Commands;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public class MMDeviceController
  {
    private MMDeviceRepository mMDeviceRepository;

    public MMDeviceRepository MMDeviceRepository
    {
      get
      {
        return mMDeviceRepository;
      }
      set
      {
        mMDeviceRepository = value;
      }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public MMDeviceController()
    {
      MMDeviceRepository = new MMDeviceRepository();
    }

    private void Reset(MMDevice? mMDevice)
    {
      MMDeviceCommands.Reset(mMDevice);
    }

    private void Start(MMDevice? mMDevice)
    {
      MMDeviceCommands.Start(mMDevice);
    }

    private void Stop(MMDevice? mMDevice)
    {
      MMDeviceCommands.Stop(mMDevice);
    }

    public void StopAll()
    {
      Action<MMDevice> action = (MMDevice x) => Stop(x);
      DoAll(action);
    }

    public void DoAll(Action<MMDevice> action)
    {
      MMDeviceRepository
        .GetAll()
        .ForEach(x => action(x));
    }

    public void DoRange
    (
      Action<MMDevice> action,
      List<MMDevice> mMDeviceList
    )
    {
      mMDeviceList
        .ForEach(x => action(x));
    }


    private void Update(MMDevice? mMDevice)
    {
      MMDeviceCommands.Update(mMDevice);
    }

    public MMDevice? Get(string id)
    {
      return MMDeviceRepository.Get(id);
    }

    public List<MMDevice> GetAll()
    {
      return MMDeviceRepository.GetAll();
    }

    public List<MMDevice> GetRange(List<string> idList)
    {
      return MMDeviceRepository.GetRange(idList);
    }


  }
}
