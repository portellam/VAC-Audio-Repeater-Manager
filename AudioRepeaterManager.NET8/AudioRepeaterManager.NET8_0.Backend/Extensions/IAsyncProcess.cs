namespace AudioRepeaterManager.NET8_0.Backend.Extensions
{
  public interface IAsyncProcess
  {
    #region Logic

    async Task<int> RunProcessAsync
    (
      string fileName,
      string arguments
    ) => throw new NotImplementedException();

    #endregion
  }
}