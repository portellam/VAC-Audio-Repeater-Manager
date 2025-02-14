using NAudio.CoreAudioApi;
using System.Diagnostics.CodeAnalysis;
using VACARM.Application.Commands;
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
    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public MMDeviceService()
    {
      WritableRepository = new MMDeviceRepository<TMMDevice>();
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

    /// <summary>
    /// Update a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    private Task<bool> Update(TMMDevice? item)
    {
      MMDeviceCommands.Update(item);
    }

    public Task<bool> Reset(string id)
    {
      this.Reset(base.WritableRepository.Get(id));
    }

    public IAsyncEnumerable<bool> ResetAll()
    {
      Action<TMMDevice> action = (TMMDevice x) => this.Reset(x);
      return base.DoWorkAllAsync(action);
    }

    public Task<bool> StartAsync(string id)
    {
      this.Start(base.WritableRepository.Get(id));
    }

    public IAsyncEnumerable<bool> StartAll()
    {
      Func<TMMDevice, Task<bool>> func = (TMMDevice x) => this.Start(x);
      return base.DoWorkAllAsync(func);
    }

    public Task<bool> StopAsync(string id)
    {
      return this.Stop(base.WritableRepository.Get(id));
    }

    public IAsyncEnumerable<bool> StopAll()
    {
      Func<TMMDevice, Task<bool>> func = (TMMDevice x) => this.Stop(x);
      return base.DoWorkAllAsync(func);
    }

    public Task<bool> Update(string id)
    {
      MMDevice? item = base.WritableRepository.Get(id);
      return this.Update(item);
    }

    public IAsyncEnumerable<bool> UpdateAll()
    {
      Func<TMMDevice, Task<bool>> func = (TMMDevice x) => this.Update(x);
      return base.DoWorkAllAsync(func);
    }
  }
}