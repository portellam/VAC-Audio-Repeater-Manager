using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Repositories
{
  public partial class BaseRepository<TBaseModel> :
    Repository
    <
      IEnumerable<TBaseModel>,
      TBaseModel
    >,
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
        var idEnumerable = base.Enumerable
          .Select(x => x.Id);

        idEnumerable.OrderBy(x => x);
        return idEnumerable;
      }
    }

    private int maxCount { get; set; } = int.MaxValue;

    public IEnumerable<uint> DeselectedIdEnumerable
    {
      get
      {
        return this.GetAll()
          .Where
          (
            BaseFunctions<TBaseModel>.NotContainsIdEnumerable
              (this.SelectedIdHashSet)
          )
          .Select(x => x.Id);
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
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public BaseRepository(IEnumerable<TBaseModel> enumerable) :
      base(enumerable)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <param name="maxCount">The maximum count of item(s)</param>
    [ExcludeFromCodeCoverage]
    public BaseRepository
    (
      IEnumerable<TBaseModel> enumerable,
      int maxCount
    ) :
      base(enumerable)
    {
      this.MaxCount = maxCount;
    }

    public TBaseModel Get(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);
      return base.Get(func);
    }

    public override void Add(TBaseModel model)
    {
      if (this.Count >= this.MaxCount)
      {
        return;
      }

      if (model == null)
      {
        return;
      }

      if
      (
        this.IdEnumerable
          .Contains(model.Id)
      )
      {
        model.Id = NextId;
      }

      base.Add(model);
    }

    public override void AddRange(IEnumerable<TBaseModel> enumerable)
    {
      if (this.Count >= this.MaxCount)
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
      if (enumerable.IsNullOrEmpty())
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
      if
      (
        base.Enumerable
          .IsNullOrEmpty()
      )
      {
        return;
      }

      foreach (var item in base.Enumerable)
      {
        this.Deselect(item);
      }
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

    #endregion
  }
}