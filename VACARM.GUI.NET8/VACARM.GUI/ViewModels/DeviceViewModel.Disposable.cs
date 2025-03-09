namespace VACARM.GUI.ViewModels
{
  public partial class DeviceViewModel
    <
      TBaseGroupService,
      TDeviceModel
    >
  {
    #region Logic

    protected override void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        base.Dispose();

        this.GroupService
          .Dispose();

        this.GroupService = null;
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}