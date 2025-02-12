using NAudio.CoreAudioApi;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  /// <summary>
  /// A controller for the <typeparamref name="MMDeviceRepository"/>.
  /// </summary>
  /// <typeparam name="T1">The repository</typeparam>
  /// <typeparam name="T2">The item</typeparam>
  public class MMDeviceController<T1, T2> :
    GenericController<GenericRepository<MMDevice>, MMDevice> where T1 :
    MMDeviceRepository<MMDevice> where T2 :
    MMDevice
  {
    private MMDeviceRepository<MMDevice> mMDeviceRepository { get; set; }

    internal override GenericRepository<MMDevice> Repository
    {
      get
      {
        return mMDeviceRepository;
      }
      set
      {
        mMDeviceRepository = (MMDeviceRepository<MMDevice>)value;
        OnPropertyChanged(nameof(Repository));
      }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public MMDeviceController()
    {
      Repository = new MMDeviceRepository<MMDevice>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public MMDeviceController(MMDeviceRepository<MMDevice> repository)
    {
      Repository = repository;
    }

    private void Reset(MMDevice? mMDevice)
    {
      MMDeviceCommands.Reset(mMDevice);
    }

    public MMDevice? Get(string id)
    {
      Func<MMDevice, bool> func = (MMDevice x) => x.ID == id;
      return Get(func);
    }

    public void Reset(string id)
    {
      Reset(Get(id));
    }

    private void Start(MMDevice? mMDevice)
    {
      MMDeviceCommands.Start(mMDevice);
    }

    public void Start(string id)
    {
      Start(Get(id));
    }

    private void Stop(MMDevice? mMDevice)
    {
      MMDeviceCommands.Stop(mMDevice);
    }

    public void Stop(string id)
    {
      Stop(Get(id));
    }

    public void StopAll()
    {
      Action<MMDevice> action = (MMDevice x) => Stop(x);
      DoAll(action);
    }

    public void DoAll(Action<MMDevice> action)
    {
      DoRange
        (
          action,
          GetAll()
        );
    }

    public void DoRange
    (
      Action<MMDevice> action,
      IEnumerable<MMDevice> enumerable
    )
    {
      foreach(var item in enumerable)
      {
        action(item);
      }
    }

    private void Update(MMDevice? mMDevice)
    {
      MMDeviceCommands.Update(mMDevice);
    }

    public List<MMDevice> GetRange(List<string> idList)
    {
      return Repository.GetRange(idList);
    }


  }
}
