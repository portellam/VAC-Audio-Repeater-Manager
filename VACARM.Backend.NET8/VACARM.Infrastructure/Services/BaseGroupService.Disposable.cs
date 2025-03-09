namespace VACARM.Infrastructure.Services
{
  public partial class BaseGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
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
        this.List = null;
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}