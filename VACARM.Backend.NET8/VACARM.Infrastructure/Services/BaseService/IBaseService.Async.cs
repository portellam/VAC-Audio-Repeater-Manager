namespace VACARM.Infrastructure.Services
{
  public partial interface IBaseService<TBaseModel>
  {
    #region Logic

    /// <summary>
    /// Write the repository to file.
    /// </summary>
    void Save();

    /// <summary>
    /// Read the repository from file.
    /// </summary>
    void Update();

    #endregion
  }
}
