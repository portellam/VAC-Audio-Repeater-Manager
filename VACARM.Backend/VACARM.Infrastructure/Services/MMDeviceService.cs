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
    GenericService<MMDeviceRepository<TMMDevice>, TMMDevice> where TRepository :
    MMDeviceRepository<TMMDevice> where TMMDevice :
    MMDevice
  {
    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public MMDeviceService()
    {
      _Repository = new MMDeviceRepository<TMMDevice>();
    }

    /// <summary>
    /// Reset a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    private void Reset(TMMDevice? item)
    {
      MMDeviceCommands.Reset(item);
    }

    /// <summary>
    /// Start a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    private void Start(TMMDevice? item)
    {
      MMDeviceCommands.Start(item);
    }

    /// <summary>
    /// Stop a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    private void Stop(TMMDevice? item)
    {
      MMDeviceCommands.Stop(item);
    }

    /// <summary>
    /// Update a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    private void Update(TMMDevice? item)
    {
      MMDeviceCommands.Update(item);
    }

    public void Reset(string id)
    {
      this.Reset(base._Repository.Get(id));
    }

    public void ResetAll()
    {
      Action<TMMDevice> action = (TMMDevice x) => this.Reset(x);
      base.DoWorkAll(action);
    }

    public void Start(string id)
    {
      this.Start(base._Repository.Get(id));
    }

    public void StartAll()
    {
      Action<TMMDevice> action = (TMMDevice x) => this.Start(x);
      base.DoWorkAll(action);
    }

    public void Stop(string id)
    {
      this.Stop(base._Repository.Get(id));
    }

    public void StopAll()
    {
      Action<TMMDevice> action = (TMMDevice x) => this.Stop(x);
      base.DoWorkAll(action);
    }

    public void Update(string id)
    {
      this.Update(base._Repository.Get(id));
    }

    public void UpdateAll()
    {
      Action<TMMDevice> action = (TMMDevice x) => this.Update(x);
      base.DoWorkAll(action);
    }
  }
}