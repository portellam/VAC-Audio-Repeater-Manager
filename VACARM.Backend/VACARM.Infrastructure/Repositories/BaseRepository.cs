using System.ComponentModel;
using System.Diagnostics;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public class BaseRepository<T> :
    GenericRepository<T>,
    IBaseRepository<BaseModel> where T : BaseModel
  {
    #region Parameters

    private IGenericRepository<BaseModel> genericRepository { get; set; } =
      new GenericRepository<BaseModel>();

    private IGenericRepository<BaseModel> GenericRepository
    {
      get
      {
        return genericRepository;
      }
      set
      {
        genericRepository = value;
        OnPropertyChanged(nameof(genericRepository));
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
    public BaseRepository()
    {
      GenericRepository = new GenericRepository<BaseModel>();
    }

    public BaseModel? Get(uint id)
    {
      Func<BaseModel, bool> func = (BaseModel x) => x.Id == id;
      return GenericRepository.Get(func);
    }

    public IEnumerable<BaseModel> GetRange
    (
      uint startId,
      uint endId
    )
    {
      Func<BaseModel, bool> func = (BaseModel x) =>
        x.Id >= startId
        && x.Id <= endId;

      return GenericRepository.GetRange(func);
    }

    public IEnumerable<BaseModel> GetRange(List<uint> idList)
    {
      Func<BaseModel, bool> func = (BaseModel x) => idList.Contains(x.Id);
      return GenericRepository.GetRange(func);
    }

    public void RemoveRange
    (
      uint startId,
      uint endId
    )
    {
      Func<BaseModel, bool> func = (BaseModel x) =>
        x.Id >= startId
        && x.Id <= endId;

      GenericRepository.RemoveRange(func);
    }

    public void RemoveRange(List<uint> idList)
    {
      Func<BaseModel, bool> func = (BaseModel x) => idList.Contains(x.Id);
      GenericRepository.RemoveRange(func);
    }

    #endregion
  }
}