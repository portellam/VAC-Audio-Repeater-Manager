namespace VACARM.GUI.ViewModels
{
  public partial class RepeaterViewModel
    <
      TBaseGroupService,
      TRepeaterModel
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

        this.DeviceViewModel
          .Dispose();

        this.DeviceViewModel = null;
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}