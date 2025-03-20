using System.Collections.ObjectModel;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Structs
{
  public struct BaseRepository<TBaseModel>
    where TBaseModel :
    class,
    IBaseModel
  {
    #region Parameters

    public readonly static ObservableCollection<TBaseModel> EmptyEnumerable =
      new ObservableCollection<TBaseModel>();

    public readonly static IBaseRepository<TBaseModel> EmptyRepository =
      new Repositories.BaseRepository<TBaseModel>(EmptyEnumerable);

    #endregion
  }
}