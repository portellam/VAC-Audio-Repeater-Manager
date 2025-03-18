namespace VACARM.Infrastructure.Services
{
  public partial class DeviceGroupService<TDeviceModel>
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

        this.MMDeviceService
          .Dispose();
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}