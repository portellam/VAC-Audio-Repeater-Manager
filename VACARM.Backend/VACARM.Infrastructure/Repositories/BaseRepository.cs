using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;

namespace VACARM.Infrastructure.Repositories
{
  public class BaseRepository<TBaseModel> :
    ReadonlyRepository<TBaseModel>,
    IBaseRepository<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    /// <summary>
    /// The enumerable of ID(s).
    /// </summary>
    private IEnumerable<uint> IdEnumerable
    {
      get
      {
        IEnumerable<uint> idEnumerable = this.Enumerable
          .Select(x => x.Id);

        idEnumerable.Order();
        return idEnumerable;
      }
    }

    private List<TBaseModel> List
    {
      get
      {
        return this.Enumerable
          .ToList();
      }
      set
      {
        this.Enumerable = value;
        this.OnPropertyChanged(nameof(List));
      }
    }

    private int maxCount { get; set; } = int.MaxValue;

    /// <summary>
    /// The next valid ID.
    /// </summary>
    private uint NextId
    {
      get
      {
        uint id = this.IdEnumerable
          .Max();

        id++;
        return id;
      }
    }

    public Func<int, bool> IsValidIndex
    {
      get
      {
        return new Func<int, bool>
          (
            x =>
            {
              return x >= 0
              && x <= this.MaxCount;
            }
          );
      }
    }

    public virtual int MaxCount
    {
      get
      {
        return this.maxCount;
      }
      internal set
      {
        this.maxCount = value;
        this.OnPropertyChanged(nameof(MaxCount));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public BaseRepository() :
      base()
    {
      this.List = new List<TBaseModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of item(s)</param>
    /// <param name="maxCount">The maximum count of item(s)</param>
    [ExcludeFromCodeCoverage]
    public BaseRepository
    (
      List<TBaseModel> list,
      int maxCount
    ) :
      base(list)
    {
      this.Enumerable = list;
      this.MaxCount = maxCount;
    }

    public bool Remove(Func<TBaseModel, bool> func)
    {
      if (func == null)
      {
        return false;
      }

      if (this.IsNullOrEmpty(this.Enumerable))
      {
        return false;
      }

      var item = this.Get(func);

      if (item == null)
      {
        return false;
      }

      return this.List
        .Remove(item);
    }

    public bool Remove(TBaseModel model)
    {
      if (model == null)
      {
        return false;
      }

      if (this.IsNullOrEmpty(this.Enumerable))
      {
        return false;
      }

      return this.List
        .Remove(model);
    }

    public IEnumerable<bool> RemoveRange(Func<TBaseModel, bool> func)
    {
      if (func == null)
      {
        yield return false;
      }

      if (this.IsNullOrEmpty(this.Enumerable))
      {
        yield return false;
      }

      var enumerable = this.GetRange(func);

      foreach (var item in enumerable)
      {
        yield return this.List
          .Remove(item);
      }
    }

    public IEnumerable<bool> RemoveRange(IEnumerable<TBaseModel> enumerable)
    {
      if (this.IsNullOrEmpty(enumerable))
      {
        yield return false;
      }

      if (this.IsNullOrEmpty(this.Enumerable))
      {
        yield return false;
      }

      foreach (var item in enumerable)
      {
        yield return this.List
          .Remove(item);
      }
    }

    public void Add(TBaseModel? model)
    {
      if (model == null)
      {
        return;
      }

      if (IdEnumerable.Contains(model.Id))
      {
        model.Id = NextId;
      }

      this.Enumerable
        .Append(model);
    }

    public void AddRange(IEnumerable<TBaseModel> enumerable)
    {
      if (enumerable == null)
      {
        return;
      }

      foreach (var item in enumerable)
      {
        this.Add(item);
      }
    }

    public void RemoveAll()
    {
      this.Enumerable = new List<TBaseModel>();
    }

    public void Update(TBaseModel model)
    {
      if (model == null)
      {
        return;
      }

      var func = BaseFunctions<TBaseModel>.ContainsId(model.Id);

      if (!func(model))
      {
        return;
      }

      if (!this.Remove(func))
      {
        return;
      }

      this.Add(model);
    }

    public void UpdateRange(IEnumerable<TBaseModel> enumerable)
    {
      if (this.IsNullOrEmpty(enumerable))
      {
        return;
      }

      var idEnumerable = enumerable.Select(x => x.Id);
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      foreach (var item in enumerable)
      {
        this.Update(item);
      }
    }

    #endregion
  }
}