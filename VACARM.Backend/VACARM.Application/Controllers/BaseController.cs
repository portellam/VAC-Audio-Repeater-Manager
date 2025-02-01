using System.ComponentModel;
using System.Diagnostics;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public class BaseController<T> :
    GenericController<T>,
    IBaseController<BaseModel> where T : BaseModel
  {
    #region Parameters

    private IBaseRepository<BaseModel> repository { get; set; } =
      new BaseRepository<BaseModel>();

    private IBaseRepository<BaseModel> Repository
    {
      get
      {
        return repository;
      }
      set
      {
        repository = value;
        OnPropertyChanged(nameof(repository));
      }
    }

    public override event PropertyChangedEventHandler PropertyChanged;

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
      Repository = new BaseRepository<BaseModel>();
    }

    public BaseModel? Get(uint id)
    {
      return Repository.Get(id);
    }

    public IEnumerable<BaseModel> GetRange(List<uint> idList)
    {
      return Repository.GetRange(idList);
    }

    public IEnumerable<BaseModel> GetRange
    (
      uint startId,
      uint endId
    )
    {
      return Repository.GetRange
        (
          startId,
          endId
        );
    }

    #endregion
  }
}