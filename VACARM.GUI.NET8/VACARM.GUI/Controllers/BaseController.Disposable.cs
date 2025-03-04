namespace VACARM.GUI.Controllers
{
  internal partial class BaseViewModel
    <
      TBaseGroupService,
      TBaseModel
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
        this.GroupService = null;

        this.ToolStripMenuItemRepository
          .Dispose();

        this.ToolStripMenuItemRepository = null;
      }

      this.HasDisposed = true;
    }

    /// <summary>
    /// Do not change this code. 
    /// Put cleanup code in Dispose(<paramref name="bool"/>
    ///  <typeparamref name="isDisposed"/>) method.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}