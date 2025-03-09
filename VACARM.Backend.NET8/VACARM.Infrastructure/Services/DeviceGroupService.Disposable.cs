namespace VACARM.Infrastructure.Services
{
  public partial class DeviceGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
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

        this.CoreAudioService
          .Dispose();

        this.CoreAudioService = null;

        this.MMDeviceService
          .Dispose();

        this.MMDeviceService = null;
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}