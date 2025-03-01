using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Extensions;
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
    /// The next valid ID.
    /// </summary>
    internal uint NextId
    {
      get
      {
        uint id = this.IdEnumerable
          .Max();

        id++;
        return id;
      }
    }

    /// <summary>
    /// The enumerable of ID(s).
    /// </summary>
    private IEnumerable<uint> IdEnumerable
    {
      get
      {
        IEnumerable<uint> idEnumerable = base.Enumerable
          .Select(x => x.Id);

        idEnumerable.Order();
        return idEnumerable;
      }
    }

    private List<TBaseModel> enumerable { get; set; }

    protected new virtual List<TBaseModel> Enumerable
    {
      get
      {
        return this.enumerable;
      }
      set
      {
        this.enumerable = value;
        base.Enumerable = value;
        base.OnPropertyChanged(nameof(this.Enumerable));
      }
    }

    private int maxCount { get; set; } = int.MaxValue;

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

    public IEnumerable<uint> DeselectedIdEnumerable
    {
      get
      {
        return this.Enumerable
          .Where
          (
            BaseFunctions<TBaseModel>.NotContainsIdEnumerable
              (this.SelectedIdHashSet)
          ).Select(x => x.Id);
      }
    }

    public HashSet<uint> SelectedIdHashSet { get; set; }

    public virtual int MaxCount
    {
      get
      {
        return this.maxCount;
      }
      internal set
      {
        this.maxCount = value;
        base.OnPropertyChanged(nameof(this.MaxCount));
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
      this.Enumerable = new List<TBaseModel>();
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

    protected override void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        base.Dispose();
        this.Enumerable = null;
      }

      this.HasDisposed = true;
    }

    public bool Remove(Func<TBaseModel, bool> func)
    {
      if (func == null)
      {
        return false;
      }

      if (base.IsNullOrEmpty)
      {
        return false;
      }

      var item = base.Get(func);

      if (item == null)
      {
        return false;
      }

      return this.Enumerable
        .Remove(item);
    }

    public bool Remove(TBaseModel model)
    {
      if (model == null)
      {
        return false;
      }

      if (base.IsNullOrEmpty)
      {
        return false;
      }

      return this.Enumerable
        .Remove(model);
    }

    public IEnumerable<bool> RemoveRange(Func<TBaseModel, bool> func)
    {
      if (func == null)
      {
        yield return false;
      }

      if (base.IsNullOrEmpty)
      {
        yield return false;
      }

      var enumerable = base.GetRange(func);

      foreach (var item in enumerable)
      {
        yield return this.Enumerable
          .Remove(item);
      }
    }

    public IEnumerable<bool> RemoveRange(IEnumerable<TBaseModel> enumerable)
    {
      if (IEnumerableExtension<TBaseModel>.IsNullOrEmpty(enumerable))
      {
        yield return false;
      }

      if (base.IsNullOrEmpty)
      {
        yield return false;
      }

      foreach (var item in enumerable)
      {
        yield return this.Enumerable
          .Remove(item);
      }
    }

    public TBaseModel? Get(uint id)
    {
      if (this.IsNullOrEmpty)
      {
        return null;
      }

      var func = BaseFunctions<TBaseModel>.ContainsId(id);
      return base.Get(func);
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
        .Add(model);
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

    public void Deselect(Func<TBaseModel, bool> func)
    {
      var model = this.Get(func);
      this.Deselect(model);
    }


    public void Deselect(TBaseModel model)
    {
      if (model == null)
      {
        return;
      }

      this.SelectedIdHashSet
        .Remove(model.Id);
    }

    public void DeselectRange(Func<TBaseModel, bool> func)
    {
      if (func == null)
      {
        return;
      }

      var enumerable = this.GetRange(func);
      this.DeselectRange(enumerable);
    }

    public void DeselectRange(IEnumerable<TBaseModel> enumerable)
    {
      if (enumerable == null)
      {
        return;
      }

      foreach (var item in enumerable)
      {
        this.Deselect(item);
      }
    }

    public void DeselectAll()
    {
      if (base.Enumerable == null)
      {
        return;
      }

      foreach (var item in base.Enumerable)
      {
        this.Deselect(item);
      }
    }

    public void RemoveAll()
    {
      base.Enumerable = new List<TBaseModel>();
    }

    public void Select(Func<TBaseModel, bool> func)
    {
      var model = this.Get(func);
      this.Select(model);
    }


    public void Select(TBaseModel model)
    {
      if (model == null)
      {
        return;
      }

      this.SelectedIdHashSet
        .Add(model.Id);
    }

    public void SelectRange(Func<TBaseModel, bool> func)
    {
      if (func == null)
      {
        return;
      }

      var enumerable = this.GetRange(func);
      this.SelectRange(enumerable);
    }

    public void SelectRange(IEnumerable<TBaseModel> enumerable)
    {
      if (enumerable == null)
      {
        return;
      }

      foreach (var item in enumerable)
      {
        this.Select(item);
      }
    }

    public void SelectAll()
    {
      if (base.Enumerable == null)
      {
        return;
      }

      foreach (var item in base.Enumerable)
      {
        this.Select(item);
      }
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
      if (IEnumerableExtension<TBaseModel>.IsNullOrEmpty(enumerable))
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