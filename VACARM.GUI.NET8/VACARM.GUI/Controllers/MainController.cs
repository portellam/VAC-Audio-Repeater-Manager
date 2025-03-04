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

    internal MainForm MainForm { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="argumentEnumerable">The enumerable of argument(s)</param>
    internal MainController(IEnumerable<string> argumentEnumerable)
    {
      var argumentController = new ArgumentController(argumentEnumerable);
      argumentController.Dispose();

      this.DeviceController = new DeviceController
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
      >();

      //NOTE: how to take in current device IDs (either by file or from DeviceController)?
      this.RepeaterController = new RepeaterController
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
      >();

      //NOTE: how to take in from BaseControllers, and be updated when they update?
      this.MainForm = new MainForm();
    }

    #endregion
  }
}