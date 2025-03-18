namespace VACARM.Infrastructure.Services
{
  public partial class CoreAudioService<TDevice> :
    IDisposable
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

        this.Controller
          .Dispose();

        this.Controller = null;
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}