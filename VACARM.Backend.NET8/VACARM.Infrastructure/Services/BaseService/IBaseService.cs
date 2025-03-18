namespace VACARM.Infrastructure.Services
{
  public partial interface IBaseService<TBaseModel>
  {
    #region Parameters

    string FilePathName { get; set; }

    #endregion
  }
}
