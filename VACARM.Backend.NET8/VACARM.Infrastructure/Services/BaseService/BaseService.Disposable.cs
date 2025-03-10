namespace VACARM.Infrastructure.Services
{
  public partial class BaseService
    <
      TRepository,
      TBaseModel
    >
  {
    #region Logic

    protected new virtual void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        base.Dispose();

        this.Repository
          .Dispose();

        this.Repository = null;
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}