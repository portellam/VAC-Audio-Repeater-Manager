using System.ComponentModel;
using System.Diagnostics;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public class BaseController<T> :
    GenericController<T>,
    IBaseController<BaseModel> where T :
    BaseModel
  {
    #region Parameters

    private GenericController<BaseModel> genericController { get; set; } =
      new GenericController<BaseModel>();

    private GenericController<BaseModel> GenericController
    {
      get
      {
        return genericController;
      }
      set
      {
        genericController = value;
        OnPropertyChanged(nameof(GenericController));
      }
    }

    internal BaseRepository<BaseModel> Repository
    {
      get
      {
        return (BaseRepository<BaseModel>)GenericController.Repository;
      }
      set
      {
        GenericController.Repository = value;
        OnPropertyChanged(nameof(Repository));
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
      GenericRepository<BaseModel> repository = new BaseRepository<BaseModel>();
      GenericController = new GenericController<BaseModel>(repository);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseController(BaseRepository<BaseModel> repository)
    {
      GenericController = new GenericController<BaseModel>(repository);
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