using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services.MMDeviceService;
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
    Service
    <
      Repository<TMMDevice>,
      TMMDevice
    >,
    IDisposable,
    IMMDeviceService
    <
      TRepository,
      TMMDevice
    >
    where TRepository :
    Repository<MMDevice>
    where TMMDevice :
    MMDevice
  {
    #region Parameters

    internal Repository<TMMDevice> DefaultCommunicationsReadonlyRepository
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

    internal Repository<TMMDevice> DefaultConsoleReadonlyRepository
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

    internal Repository<TMMDevice> DefaultMultimediaReadonlyRepository
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

    private Repository<TMMDevice> defaultCommunicationsReadonlyRepository
    { get; set; } = new Repository<TMMDevice>();

    private Repository<TMMDevice> defaultConsoleReadonlyRepository
    { get; set; } = new Repository<TMMDevice>();

    private Repository<TMMDevice> defaultMultimediaReadonlyRepository
    { get; set; } = new Repository<TMMDevice>();

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public MMDeviceService() :
      base()
    {
      base.Repository = new Repository<TMMDevice>();

      this.DefaultCommunicationsReadonlyRepository =
        new Repository<TMMDevice>();

      DefaultConsoleReadonlyRepository =
        new Repository<TMMDevice>();

      this.DefaultMultimediaReadonlyRepository =
        new Repository<TMMDevice>();

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