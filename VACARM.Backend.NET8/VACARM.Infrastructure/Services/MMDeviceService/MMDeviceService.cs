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
  public partial class MMDeviceService<TMMDevice> :
    Service
    <
      IList<TMMDevice>,
      TMMDevice
    >,
    IMMDeviceService<TMMDevice>
    where TMMDevice :
    MMDevice
  {
    #region Parameters

    internal Repository
      <
        IList<TMMDevice>,
        TMMDevice
      > DefaultCommunicationsRepository
    {
      get
      {
        return this.defaultCommunicationsRepository;
      }
      set
      {
        this.defaultCommunicationsRepository = value;
        base.OnPropertyChanged(nameof(this.DefaultCommunicationsRepository));
      }
    }

    internal Repository
      <
        IList<TMMDevice>,
        TMMDevice
      > DefaultConsoleRepository
    {
      get
      {
        return this.defaultConsoleRepository;
      }
      set
      {
        this.defaultConsoleRepository = value;
        base.OnPropertyChanged(nameof(this.DefaultConsoleRepository));
      }
    }

    internal Repository
      <
        IList<TMMDevice>,
        TMMDevice
      > DefaultMultimediaRepository
    {
      get
      {
        return this.defaultMultimediaRepository;
      }
      set
      {
        this.defaultMultimediaRepository = value;
        base.OnPropertyChanged(nameof(this.DefaultMultimediaRepository));
      }
    }

    private MMNotificationClient MMNotificationClient { get; set; }

    private Repository
      <
        IList<TMMDevice>,
        TMMDevice
      > defaultCommunicationsRepository
    { get; set; }

    private Repository
      <
        IList<TMMDevice>,
        TMMDevice
      > defaultConsoleRepository
    { get; set; }

    private Repository
      <
        IList<TMMDevice>,
        TMMDevice
      > defaultMultimediaRepository
    { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public MMDeviceService() :
      base(new List<TMMDevice>())
    {
      this.DefaultCommunicationsRepository =
        new Repository
        <
          IList<TMMDevice>,
          TMMDevice
        >(new List<TMMDevice>());

      DefaultConsoleRepository =
        new Repository
        <
          IList<TMMDevice>,
          TMMDevice
        >(new List<TMMDevice>());

      this.DefaultMultimediaRepository =
        new Repository
        <
          IList<TMMDevice>,
          TMMDevice
        >(new List<TMMDevice>());

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

      return this.DefaultCommunicationsRepository
        .Get(func);
    }

    public TMMDevice GetDefaultConsole(DataFlow dataFlow)
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.DataFlow == dataFlow;

      return DefaultConsoleRepository
        .Get(func);
    }

    public TMMDevice GetDefaultMultimedia(DataFlow dataFlow)
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.DataFlow == dataFlow;

      return this.DefaultMultimediaRepository
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
      var list = this.MMNotificationClient
        .Enumerable
        .Cast<TMMDevice>()
        .ToList();

      base.Repository = new Repository
        <
          IList<TMMDevice>,
          TMMDevice
        >(list);

      list = this.MMNotificationClient
        .GetDefaultRange(Role.Communications)
        .Cast<TMMDevice>()
        .ToList();

      this.DefaultCommunicationsRepository = new Repository
        <
          IList<TMMDevice>,
          TMMDevice
        >(list);

      list = this.MMNotificationClient
        .GetDefaultRange(Role.Console)
        .Cast<TMMDevice>()
        .ToList();

      DefaultConsoleRepository = new Repository
        <
          IList<TMMDevice>,
          TMMDevice
        >(list);

      list = this.MMNotificationClient
        .GetDefaultRange(Role.Multimedia)
        .Cast<TMMDevice>()
        .ToList();

      this.DefaultMultimediaRepository = new Repository
        <
          IList<TMMDevice>,
          TMMDevice
        >(list);
    }

    #endregion
  }
}