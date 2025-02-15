using NAudio.CoreAudioApi;
using System.Diagnostics.CodeAnalysis;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  /// <summary>
  /// A service for the <typeparamref name="MMDeviceRepository"/>.
  /// </summary>
  public class MMDeviceService<TRepository, TMMDevice> :
    Service<MMDeviceRepository<TMMDevice>, TMMDevice>,
    IMMDeviceService<MMDeviceRepository<TMMDevice>, TMMDevice> where TRepository :
    MMDeviceRepository<TMMDevice> where TMMDevice :
    MMDevice
  {
    public new MMDeviceRepository<TMMDevice> Repository
    {
      get
      {
        return this.Repository as MMDeviceRepository<TMMDevice>;
      }
    }


    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public MMDeviceService()
    {
      base.Repository = new MMDeviceRepository<TMMDevice>();
    }

    /// <summary>
    /// Reset a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    private Task<bool> Reset(TMMDevice? item)
    {
      MMDeviceCommands.Reset(item);
    }

    /// <summary>
    /// Start a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    private Task<bool> Start(TMMDevice? item)
    {
      MMDeviceCommands.Start(item);
    }

    /// <summary>
    /// Stop a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    private Task<bool> Stop(TMMDevice? item)
    {
      MMDeviceCommands.Stop(item);
    }

    public Task<bool> Reset(string id)
    {
      this.Reset(base.Repository.Get(id));
    }

    public IAsyncEnumerable<bool> ResetAll()
    {
      Action<TMMDevice> action = (TMMDevice x) => this.Reset(x);
      return base.DoWorkAllAsync(action);
    }

    public Task<bool> StartAsync(string id)
    {
      this.Start(base.Repository.Get(id));
    }

    public IAsyncEnumerable<bool> StartAll()
    {
      Func<TMMDevice, Task<bool>> func = (TMMDevice x) => this.Start(x);
      return base.DoWorkAllAsync(func);
    }

    public void Stop(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);
      var item = this.Repository.Get(func);
      MMDeviceCommands.Stop(item);
    }



    public void StopRange(IEnumerable<string> idEnumerable)
    {
      var enumerable = this.Repository
        .GetRange(idEnumerable);

      this.DoWorkRange
        (
          MMDeviceCommands.Stop,
          enumerable
        );
    }

    public void StopAll()
    {
      this.DoWorkAll(MMDeviceCommands.Stop);
    }

    public void Update(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      this.DoWork
        (
          MMDeviceCommands.Update,
          func
        );
    }

    public void UpdateAll()
    {
      this.Repository.UpdateAll();
    }
  }
}