namespace VACARM.Infrastructure.Services
{
  public partial interface IBaseService<TBaseModel>
  {
    #region Logic

    /// <summary>
    /// Write the repository to file.
    /// </summary>
    Task WriteAllToFile();

    /// <summary>
    /// Read the repository from file.
    /// </summary>
    Task ReadRangeFromFile();

    #endregion
  }
}
