using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public class DeviceRepository :
    Repository<DeviceModel>,
    IDeviceRepository
  {
    #region Logic

    public List<DeviceModel> GetAllAbsent()
    {
      return GetAll()
        .Where(x => !x.IsPresent)
        .ToList();
    }

    public List<DeviceModel> GetAllInAlphabeticalOrder()
    {
      return GetAll()
        .OrderBy(x => x.Name)
        .ToList();
    }

    public List<DeviceModel> GetAllInReverseAlphabeticalOrder()
    {
      return GetAll()
        .OrderByDescending(x => x.Name)
        .ToList();
    }

    public List<DeviceModel> GetAllInput()
    {
      return GetAll()
        .Where(x => x.IsInput)
        .ToList();
    }

    public List<DeviceModel> GetAllOutput()
    {
      return GetAll()
        .Where(x => x.IsOutput)
        .ToList();
    }

    public List<DeviceModel> GetAllPresent()
    {
      return GetAll()
        .Where(x => x.IsPresent)
        .ToList();
    }

    #endregion
  }
}