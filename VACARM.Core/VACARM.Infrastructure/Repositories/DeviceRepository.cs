using VACARM.Core.Models;

namespace VACARM.Core.Repositories
{
  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="list">the device list</param>
  public class DeviceRepository<T>(List<DeviceModel> list) :
    IDeviceRepository
    where T : DeviceModel
  {
    #region Parameters

    private ListRepository<DeviceModel> ListRepository =
      new ListRepository<DeviceModel>(list);

    #endregion

    #region Logic

    public List<DeviceModel> GetAllAbsent()
    {
      return ListRepository
        .GetAll()
        .Where(x => !x.IsPresent)
        .ToList();
    }

    public List<DeviceModel> GetAllInAlphabeticalOrder()
    {
      return ListRepository
        .GetAll()
        .OrderBy(x => x.Name)
        .ToList();
    }

    public List<DeviceModel> GetAllInReverseAlphabeticalOrder()
    {
      return ListRepository
        .GetAll()
        .OrderByDescending(x => x.Name)
        .ToList();
    }

    public List<DeviceModel> GetAllInput()
    {
      return ListRepository
        .GetAll()
        .Where(x => x.IsInput)
        .ToList();
    }

    public List<DeviceModel> GetAllOutput()
    {
      return ListRepository
        .GetAll()
        .Where(x => x.IsOutput)
        .ToList();
    }

    public List<DeviceModel> GetAllPresent()
    {
      return ListRepository
        .GetAll()
        .Where(x => x.IsPresent)
        .ToList();
    }

    #endregion
  }
}