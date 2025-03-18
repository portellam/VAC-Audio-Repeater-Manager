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
  public partial class MMDeviceService<TMMDevice>
  {
    #region Logic

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

    #endregion
  }
}