using VACARM.Domain.Models;

namespace VACARM.Application.Controllers
{
  public class DeviceController :
    GenericController<DeviceModel>,
    IDeviceController
  {
  }
}
