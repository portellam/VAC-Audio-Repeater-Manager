namespace VACARM.GUI.Controllers
{
  internal partial class MainController
  {
    #region Parameters

    protected virtual bool HasDisposed { get; set; }

    #endregion

    #region Logic

    protected void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        this.DeviceController
          .Dispose();

        this.DeviceController = null;
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