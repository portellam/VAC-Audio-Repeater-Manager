using System;

namespace VACARM.Infrastructure.Services
{
  public partial class BaseGroupService<TBaseModel>
  {
    #region Parameters

    public new Func<BaseService<TBaseModel>, bool> IsValidIndex
    {
      get
      {
        return (BaseService<TBaseModel> x) => x.Model
          .Id == this.SelectedId;
      }
    }

    #endregion
  }
}