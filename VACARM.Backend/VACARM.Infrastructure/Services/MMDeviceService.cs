using NAudio.CoreAudioApi;
using System.Diagnostics.CodeAnalysis;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Watchers;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service to retrieve current and updated system audio devices.
  /// </summary>
  public class MMDeviceService<TRepository, TMMDevice> :
    ReadonlyService<ReadonlyRepository<TMMDevice>, TMMDevice>,
    IDisposable,
    IMMDeviceService<TRepository, TMMDevice> where TRepository :
    ReadonlyRepository<MMDevice> where TMMDevice :
    MMDevice
  {
    #region Parameters

    internal ReadonlyRepository<TMMDevice> DefaultCommunicationsReadonlyRepository
    {
      get
      {
        return this.defaultCommunicationsReadonlyRepository;
      }
      set
      {
        this.defaultCommunicationsReadonlyRepository = value;
        this.OnPropertyChanged(nameof(DefaultCommunicationsReadonlyRepository));
      }
    }

    internal ReadonlyRepository<TMMDevice> DefaultConsoleReadonlyRepository
    {
      get
      {
        return this.defaultConsoleReadonlyRepository;
      }
      set
      {
        this.defaultConsoleReadonlyRepository = value;
        this.OnPropertyChanged(nameof(DefaultConsoleReadonlyRepository));
      }
    }

    internal ReadonlyRepository<TMMDevice> DefaultMultimediaReadonlyRepository
    {
      get
      {
        return this.defaultMultimediaReadonlyRepository;
      }
      set
      {
        this.defaultMultimediaReadonlyRepository = value;
        this.OnPropertyChanged(nameof(DefaultMultimediaReadonlyRepository));
      }
    }

    private bool HasDisposed;

    private MMNotificationClient MMNotificationClient { get; set; }

    private ReadonlyRepository<TMMDevice> defaultCommunicationsReadonlyRepository
    { get; set; } = new ReadonlyRepository<TMMDevice>();

    private ReadonlyRepository<TMMDevice> defaultConsoleReadonlyRepository
    { get; set; } = new ReadonlyRepository<TMMDevice>();

    private ReadonlyRepository<TMMDevice> defaultMultimediaReadonlyRepository
    { get; set; } = new ReadonlyRepository<TMMDevice>();

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public MMDeviceService() :
      base()
    {
      this.ReadonlyRepository = new ReadonlyRepository<TMMDevice>();

      this.DefaultCommunicationsReadonlyRepository =
        new ReadonlyRepository<TMMDevice>();

      this.DefaultConsoleReadonlyRepository =
        new ReadonlyRepository<TMMDevice>();

      this.DefaultMultimediaReadonlyRepository =
        new ReadonlyRepository<TMMDevice>();

      this.MMNotificationClient = new MMNotificationClient(this.UpdateService);
      this.UpdateService();
    }

    /// <summary>
    /// Dispose of unmanaged objects and true/false dispose of managed objects.
    /// </summary>
    /// <param name="isDisposed">True/false</param>
    protected virtual void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        this.MMNotificationClient
          .Dispose();

        this.ReadonlyRepository
          .Dispose();

        this.DefaultCommunicationsReadonlyRepository
          .Dispose();

        this.DefaultConsoleReadonlyRepository
          .Dispose();

        this.DefaultMultimediaReadonlyRepository
          .Dispose();
      }

      this.HasDisposed = true;
    }

    /// <summary>
    /// Do not change this code. 
    /// Put cleanup code in Dispose(<paramref name="bool"/>
    ///  <typeparamref name="isDisposed"/>) method.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    public TMMDevice? Get(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      return this.ReadonlyRepository
        .Get(func);
    }

    public IEnumerable<TMMDevice> GetAll()
    {
      var enumerable = this.ReadonlyRepository
        .GetAll();

      if (IEnumerableExtension<TMMDevice>.IsNullOrEmpty(enumerable))
      {
        return Array.Empty<TMMDevice>();
      }

      return enumerable
        .AsEnumerable();
    }

    public TMMDevice? GetDefaultCommunications(DataFlow dataFlow)
    {
      var func = (TMMDevice x) => x.DataFlow == dataFlow;

      return this.DefaultCommunicationsReadonlyRepository
        .Get(func);
    }

    public TMMDevice? GetDefaultConsole(DataFlow dataFlow)
    {
      var func = (TMMDevice x) => x.DataFlow == dataFlow;

      return this.DefaultConsoleReadonlyRepository
        .Get(func);
    }

    public TMMDevice? GetDefaultMultimedia(DataFlow dataFlow)
    {
      var func = (TMMDevice x) => x.DataFlow == dataFlow;

      return this.DefaultMultimediaReadonlyRepository
        .Get(func);
    }

    public IEnumerable<TMMDevice> GetRange(IEnumerable<string> idEnumerable)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      return this.ReadonlyRepository
        .GetRange(func);
    }

    public void Reset(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      this.DoWork
        (
          MMDeviceCommands.Reset,
          func
        );
    }

    public void ResetAll()
    {
      this.DoWorkAll(MMDeviceCommands.Reset);
    }

    public void ResetRange(IEnumerable<string> idEnumerable)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      this.DoWorkRange
        (
          MMDeviceCommands.Reset,
          func
        );
    }

    public void Start(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      this.DoWork
        (
          MMDeviceCommands.Start,
          func
        );
    }

    public void StartAll()
    {
      this.DoWorkAll(MMDeviceCommands.Start);
    }

    public void StartRange(IEnumerable<string> idEnumerable)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      this.DoWorkRange
        (
          MMDeviceCommands.Start,
          func
        );
    }

    public void Stop(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      this.DoWork
        (
          MMDeviceCommands.Stop,
          func
        );
    }

    public void StopRange(IEnumerable<string> idEnumerable)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      this.DoWorkRange
        (
          MMDeviceCommands.Stop,
          func
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
      this.DoWorkAll(MMDeviceCommands.Update);
    }

    public void UpdateRange(IEnumerable<string> idEnumerable)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      this.DoWorkRange
        (
          MMDeviceCommands.Update,
          func
        );
    }

    public void UpdateService()
    {
      var enumerable = this.MMNotificationClient
        .MMDeviceCollection
        .Cast<TMMDevice>();

      this.ReadonlyRepository = new ReadonlyRepository<TMMDevice>(enumerable);

      enumerable = this.MMNotificationClient
        .GetDefaultRange(Role.Communications)
        .Cast<TMMDevice>();

      this.DefaultCommunicationsReadonlyRepository =
        new ReadonlyRepository<TMMDevice>(enumerable);

      enumerable = this.MMNotificationClient
        .GetDefaultRange(Role.Console)
        .Cast<TMMDevice>();

      this.DefaultConsoleReadonlyRepository =
        new ReadonlyRepository<TMMDevice>(enumerable);

      enumerable = this.MMNotificationClient
        .GetDefaultRange(Role.Multimedia)
        .Cast<TMMDevice>();

      this.DefaultMultimediaReadonlyRepository =
        new ReadonlyRepository<TMMDevice>(enumerable);
    }

    #endregion
  }
}