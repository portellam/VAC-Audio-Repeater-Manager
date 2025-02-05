using System.ComponentModel;
using System.Diagnostics;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public class BaseController<T1, T2> :
  GenericListController<T1, T2>,
  IBaseController<T1, T2> where T1 :
  BaseRepository<T2> where T2 :
  BaseModel
  {
    #region Parameters

    internal BaseRepository<T2> BaseRepository
    {
      get
      {
        return (BaseRepository<T2>)Repository;
      }
      set
      {
        Repository = value;
        OnPropertyChanged(nameof(BaseRepository));
      }
    }

    public override event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke
      (
        this,
        new PropertyChangedEventArgs(propertyName)
      );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: {0}",
          propertyName
        )
      );
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseController()
    {
      Repository = new BaseRepository<T2>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseController(BaseRepository<T2> repository)
    {
      BaseRepository = repository;
    }

    public BaseModel? Get(uint id)
    {
      return BaseRepository.Get(id);
    }

    public IEnumerable<BaseModel> GetRange(List<uint> idList)
    {
      return BaseRepository.GetRange(idList);
    }

    public IEnumerable<BaseModel> GetRange
    (
      uint startId,
      uint endId
    )
    {
      return BaseRepository.GetRange
        (
          startId,
          endId
        );
    }

    #endregion
  }
}