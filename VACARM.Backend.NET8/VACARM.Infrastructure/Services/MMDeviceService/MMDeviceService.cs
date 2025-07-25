using NAudio.CoreAudioApi;
using System.Diagnostics.CodeAnalysis;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Watchers;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The service to retrieve current and/or updated system audio device(s).
  /// </summary>
  public class MMDeviceService
    <
      TRepository,
      TMMDevice
    > :
    ReadonlyService
    <
      ReadonlyRepository<TMMDevice>,
      TMMDevice
    >,
    IDisposable,
    IMMDeviceService
    <
      TRepository,
      TMMDevice
    >
    where TRepository :
    ReadonlyRepository<MMDevice>
    where TMMDevice :
    MMDevice
  {
    #region Parameters

    internal ReadonlyRepository<TMMDevice> DefaultCommunicationsReadonlyRepository
    {
      get
      {
        return defaultCommunicationsReadonlyRepository;
      }
      set
      {
        defaultCommunicationsReadonlyRepository = value;
        base.OnPropertyChanged(nameof(DefaultCommunicationsReadonlyRepository));
      }
    }

    internal ReadonlyRepository<TMMDevice> DefaultConsoleReadonlyRepository
    {
      get
      {
        return defaultConsoleReadonlyRepository;
      }
      set
      {
        defaultConsoleReadonlyRepository = value;
        base.OnPropertyChanged(nameof(DefaultConsoleReadonlyRepository));
      }
    }

    internal ReadonlyRepository<TMMDevice> DefaultMultimediaReadonlyRepository
    {
      get
      {
        return defaultMultimediaReadonlyRepository;
      }
      set
      {
        defaultMultimediaReadonlyRepository = value;
        base.OnPropertyChanged(nameof(DefaultMultimediaReadonlyRepository));
      }
    }

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
      base.Repository = new ReadonlyRepository<TMMDevice>();

      DefaultCommunicationsReadonlyRepository =
        new ReadonlyRepository<TMMDevice>();

      DefaultConsoleReadonlyRepository =
        new ReadonlyRepository<TMMDevice>();

      DefaultMultimediaReadonlyRepository =
        new ReadonlyRepository<TMMDevice>();

      MMNotificationClient = new MMNotificationClient(UpdateService);
      UpdateService();
    }

    protected override void Dispose(bool isDisposed)
    {
      if (base.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        MMNotificationClient
          .Dispose();

        base.Repository
          .Dispose();

        DefaultCommunicationsReadonlyRepository
          .Dispose();

        DefaultConsoleReadonlyRepository
          .Dispose();

        DefaultMultimediaReadonlyRepository
          .Dispose();
      }

      base.HasDisposed = true;
    }

    public TMMDevice? Get(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      return base.Repository
        .Get(func);
    }

    public TMMDevice? GetDefaultCommunications(DataFlow dataFlow)
    {
      var func = (TMMDevice x) => x.DataFlow == dataFlow;

      return DefaultCommunicationsReadonlyRepository
        .Get(func);
    }

    public TMMDevice? GetDefaultConsole(DataFlow dataFlow)
    {
      var func = (TMMDevice x) => x.DataFlow == dataFlow;

      return DefaultConsoleReadonlyRepository
        .Get(func);
    }

    public TMMDevice? GetDefaultMultimedia(DataFlow dataFlow)
    {
      var func = (TMMDevice x) => x.DataFlow == dataFlow;

      return DefaultMultimediaReadonlyRepository
        .Get(func);
    }

    public IEnumerable<TMMDevice> GetAll()
    {
      return base.Repository
        .GetAll();
    }

    public IEnumerable<TMMDevice> GetRange(IEnumerable<string> idEnumerable)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      return base.Repository
        .GetRange(func);
    }

    public void Reset(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      DoAction
        (
          MMDeviceCommands.Reset,
          func
        );
    }

    public void ResetAll()
    {
      DoActionAll(MMDeviceCommands.Reset);
    }

    public void ResetRange(IEnumerable<string> idEnumerable)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      DoActionRange
        (
          MMDeviceCommands.Reset,
          func
        );
    }

    public void Start(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      DoAction
        (
          MMDeviceCommands.Start,
          func
        );
    }

    public void StartAll()
    {
      DoActionAll(MMDeviceCommands.Start);
    }

    public void StartRange(IEnumerable<string> idEnumerable)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      DoActionRange
        (
          MMDeviceCommands.Start,
          func
        );
    }

    public void Stop(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      DoAction
        (
          MMDeviceCommands.Stop,
          func
        );
    }

    public void StopRange(IEnumerable<string> idEnumerable)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      DoActionRange
        (
          MMDeviceCommands.Stop,
          func
        );
    }

    public void StopAll()
    {
      DoActionAll(MMDeviceCommands.Stop);
    }

    public void Update(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      DoAction
        (
          MMDeviceCommands.Update,
          func
        );
    }

    public void UpdateAll()
    {
      DoActionAll(MMDeviceCommands.Update);
    }

    public void UpdateRange(IEnumerable<string> idEnumerable)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      DoActionRange
        (
          MMDeviceCommands.Update,
          func
        );
    }

    public void UpdateService()
    {
      var enumerable = MMNotificationClient
        .Enumerable
        .Cast<TMMDevice>();

      base.Repository = new ReadonlyRepository<TMMDevice>(enumerable);

      enumerable = MMNotificationClient
        .GetDefaultRange(Role.Communications)
        .Cast<TMMDevice>();

      DefaultCommunicationsReadonlyRepository =
        new ReadonlyRepository<TMMDevice>(enumerable);

      enumerable = MMNotificationClient
        .GetDefaultRange(Role.Console)
        .Cast<TMMDevice>();

      DefaultConsoleReadonlyRepository =
        new ReadonlyRepository<TMMDevice>(enumerable);

      enumerable = MMNotificationClient
        .GetDefaultRange(Role.Multimedia)
        .Cast<TMMDevice>();

      DefaultMultimediaReadonlyRepository =
        new ReadonlyRepository<TMMDevice>(enumerable);
    }

    #endregion
  }
}