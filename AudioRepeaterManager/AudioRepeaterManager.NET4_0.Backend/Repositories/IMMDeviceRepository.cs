﻿using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.ComponentModel;

namespace AudioRepeaterManager.NET4_0.Backend.Repositories
{
  public interface IMMDeviceRepository
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    List<MMDevice> GetAll();
    List<MMDevice> GetRange(List<string> idList);
    MMDevice Get(string id);
    void Disable(string id);
    void Enable(string id);
    void Update();

    #endregion
  }
}