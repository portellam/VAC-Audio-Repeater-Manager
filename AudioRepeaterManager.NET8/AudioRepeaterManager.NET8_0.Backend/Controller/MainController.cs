using AudioRepeaterManager.NET8_0.Backend.Models;
using AudioRepeaterManager.NET8_0.Backend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRepeaterManager.NET8_0.Backend.Controller
{
  public class MainController
  {
    public DeviceRepository DeviceRepository { get; private set; }
    public RepeaterRepository RepeaterRepository { get; private set; }
    public ProcessRepository ProcessRepository { get; private set; }


    public MainController()
    {
      DeviceRepository = new DeviceRepository();
      RepeaterRepository = new RepeaterRepository();

      ProcessRepository = new ProcessRepository
        (RepeaterRepository.ExecutableNameList);

      
    }


    public void AddRepeater
    (
      uint inputDeviceId,
      uint outputDeviceId
    )
    {
      DeviceModel inputDeviceModel = DeviceRepository.Get(inputDeviceId);
      DeviceModel outputDeviceModel = DeviceRepository.Get(outputDeviceId);

      //RepeaterRepository.Insert()

    }


    /// <summary>
    /// Get a list of all repeaters with any disabled devices.
    /// </summary>
    /// <returns>The list of repeaters</returns>
    public List<RepeaterModel> GetAllDisabledRepeaters()
    {
      List<RepeaterModel> repeaterModelList = new List<RepeaterModel>();

      DeviceRepository
        .GetAllDisabled()
        .ForEach
        (
          x =>
          {

            repeaterModelList.AddRange
              (RepeaterRepository.GetRangeByDeviceId(x.Id));
          }
        );

      return repeaterModelList;
    }
  }
}
