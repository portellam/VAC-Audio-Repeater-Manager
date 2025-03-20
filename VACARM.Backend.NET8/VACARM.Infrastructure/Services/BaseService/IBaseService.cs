namespace VACARM.Infrastructure.Services
{
  public partial interface IBaseService<TBaseModel>
  {
    #region Parameters

    string FilePathName { get; set; }
    uint Id { get; set; }

    #endregion
  }
}