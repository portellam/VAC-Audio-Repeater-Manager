using VACARM.Domain.Models;
using VACARM.GUI.Views;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
  internal partial class MainController :
    IDisposable
  {
    #region Parameters

    internal DeviceController
      <
        DeviceGroupService
        <
          ReadonlyRepository
          <
            BaseService
            <
              BaseRepository<DeviceModel>,
              DeviceModel
            >
          >,
          BaseService
          <
            BaseRepository<DeviceModel>,
            DeviceModel
          >,
          BaseRepository<DeviceModel>,
          DeviceModel
        >,
        DeviceModel
      >
    DeviceController
    { get; set; }

    internal RepeaterController
      <
        RepeaterGroupService
        <
          ReadonlyRepository
          <
            BaseService
            <
              BaseRepository<RepeaterModel>,
              RepeaterModel
            >
          >,
          BaseService
          <
            BaseRepository<RepeaterModel>,
            RepeaterModel
          >,
          BaseRepository<RepeaterModel>,
          RepeaterModel
        >,
        RepeaterModel
      >
    RepeaterController
    { get; set; }

    internal AboutForm AboutForm { get; set; }
    internal DeviceFindForm DeviceFindForm { get; set; }
    internal MainForm MainForm { get; set; }

    #endregion

    #region Logic

    internal MainController(string[] arguments)

    #endregion
  }
}
