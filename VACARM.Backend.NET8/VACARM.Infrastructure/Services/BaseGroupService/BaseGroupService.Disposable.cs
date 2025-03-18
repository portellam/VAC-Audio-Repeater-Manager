namespace VACARM.Infrastructure.Services
{
  public partial class BaseGroupService<TBaseModel>
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
        foreach
        (
          var item in base.Repository
            .GetAll()
        )
        {
          item.Dispose();
        }

        base.Dispose();
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}