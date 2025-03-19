using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IBaseService<TBaseModel>
  {
    #region Parameters

    BaseModel Model { get; }
    string FilePathName { get; set; }

    #endregion
  }
}
