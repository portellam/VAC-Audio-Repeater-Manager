using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
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
  public partial class MMDeviceService
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
        return this.defaultCommunicationsReadonlyRepository;
      }
      set
      {
        this.defaultCommunicationsReadonlyRepository = value;
        base.OnPropertyChanged(nameof(this.DefaultCommunicationsReadonlyRepository));
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
        base.OnPropertyChanged(nameof(this.DefaultConsoleReadonlyRepository));
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
        base.OnPropertyChanged(nameof(this.DefaultMultimediaReadonlyRepository));
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

      this.DefaultCommunicationsReadonlyRepository =
        new ReadonlyRepository<TMMDevice>();

      DefaultConsoleReadonlyRepository =
        new ReadonlyRepository<TMMDevice>();

      this.DefaultMultimediaReadonlyRepository =
        new ReadonlyRepository<TMMDevice>();

      this.MMNotificationClient = new MMNotificationClient(this.UpdateService);
      this.UpdateService();
    }
    public TMMDevice Get(string id)
    {
      Func<TMMDevice, bool> func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      return base.Repository
        .Get(func);
    }

    public TMMDevice GetDefaultCommunications(DataFlow dataFlow)
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.DataFlow == dataFlow;

      return this.DefaultCommunicationsReadonlyRepository
        .Get(func);
    }

    public TMMDevice GetDefaultConsole(DataFlow dataFlow)
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.DataFlow == dataFlow;

      return DefaultConsoleReadonlyRepository
        .Get(func);
    }

    public TMMDevice GetDefaultMultimedia(DataFlow dataFlow)
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.DataFlow == dataFlow;

      return this.DefaultMultimediaReadonlyRepository
        .Get(func);
    }

    public IEnumerable<TMMDevice> GetAll()
    {
      return base.Repository
        .GetAll();
    }

    public IEnumerable<TMMDevice> GetRange(IEnumerable<string> idEnumerable)
    {
      Func<TMMDevice, bool> func =
        MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      return base.Repository
        .GetRange(func);
    }

    public void Reset(string id)
    {
      Func<TMMDevice, bool> func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      base.DoAction
        (
          MMDeviceCommands.Reset,
          func
        );
    }

    public void ResetAll()
    {
      base.DoActionAll(MMDeviceCommands.Reset);
    }

    public void ResetRange(IEnumerable<string> idEnumerable)
    {
      Func<TMMDevice, bool> func = 
        MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      base.DoActionRange
        (
          MMDeviceCommands.Reset,
          func
        );
    }

    public void Start(string id)
    {
      Func<TMMDevice, bool> func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      base.DoAction
        (
          MMDeviceCommands.Start,
          func
        );
    }

    public void StartAll()
    {
      base.DoActionAll(MMDeviceCommands.Start);
    }

    public void StartRange(IEnumerable<string> idEnumerable)
    {
      Func<TMMDevice, bool> func = 
        MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      base.DoActionRange
        (
          MMDeviceCommands.Start,
          func
        );
    }

    public void Stop(string id)
    {
      Func<TMMDevice, bool> func = MMDeviceFunctions<TMMDevice>.ContainsId(id);

      base.DoAction
        (
          MMDeviceCommands.Stop,
          func
        );
    }

    public void StopRange(IEnumerable<string> idEnumerable)
    {
      Func<TMMDevice, bool> func = 
        MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      base.DoActionRange
        (
          MMDeviceCommands.Stop,
          func
        );
    }

    public void StopAll()
    {
      base.DoActionAll(MMDeviceCommands.Stop);
    }

    public void Update(string id)
    {
      Func<TMMDevice, bool> func = 
        MMDeviceFunctions<TMMDevice>.ContainsId(id);

      base.DoAction
        (
          MMDeviceCommands.Update,
          func
        );
    }

    public void UpdateAll()
    {
      base.DoActionAll(MMDeviceCommands.Update);
    }

    public void UpdateRange(IEnumerable<string> idEnumerable)
    {
      Func<TMMDevice, bool> func = 
        MMDeviceFunctions<TMMDevice>.ContainsIdEnumerable(idEnumerable);

      base.DoActionRange
        (
          MMDeviceCommands.Update,
          func
        );
    }

    public void UpdateService()
    {
      var enumerable = this.MMNotificationClient
        .Enumerable
        .Cast<TMMDevice>();

      base.Repository = new ReadonlyRepository<TMMDevice>(enumerable);

      enumerable = this.MMNotificationClient
        .GetDefaultRange(Role.Communications)
        .Cast<TMMDevice>();

      this.DefaultCommunicationsReadonlyRepository =
        new ReadonlyRepository<TMMDevice>(enumerable);

      enumerable = this.MMNotificationClient
        .GetDefaultRange(Role.Console)
        .Cast<TMMDevice>();

      DefaultConsoleReadonlyRepository =
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