namespace VACARM.Infrastructure.Services
{
  public interface IBaseService
    <
      TRepository,
      TBaseModel
    >
  {
    #region Parameters

    string FilePathName { get; set; }

    #endregion
  }
}
