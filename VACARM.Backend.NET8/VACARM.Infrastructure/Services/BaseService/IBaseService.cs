namespace VACARM.Infrastructure.Services
{
  public interface IBaseService<TBaseModel>
  {
    #region Parameters

    string FilePathName { get; set; }

    #endregion
  }
}
