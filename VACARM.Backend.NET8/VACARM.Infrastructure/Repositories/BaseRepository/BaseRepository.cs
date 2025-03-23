using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;

namespace VACARM.Infrastructure.Repositories
{
  public partial class BaseRepository<TBaseModel> :
    Repository
    <
      ObservableCollection<TBaseModel>,
      TBaseModel
    >,
    IBaseRepository<TBaseModel>
    where TBaseModel :
    class,
    IBaseModel,
    new()
  {
    #region Parameters

    internal TBaseModel EmptyModel
    {
      get
      {
        return new TBaseModel()
        {
          Id = this.NextId
        };
      }
    }



    /// <summary>
    /// The next valid ID.
    /// </summary>
    internal uint NextId
    {
      get
      {
        return this.Next(this.IdEnumerable);
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
              (this.SelectedIdEnumerable)
          )
          .Select(x => x.Id);
      }
    }

    public ObservableCollection<uint> SelectedIdEnumerable { get; set; }

    public readonly static uint MinCount = uint.MinValue;
    public readonly static int SafeMaxCount = byte.MaxValue;

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
    /// Get an enumerable of <typeparamref name="TBaseModel"/>(s) of which the
    /// property <see langword="Id"/> is unique. Removes any duplicate(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    private IEnumerable<TBaseModel> GetModifiedUniqueRange
    (this IEnumerable<TBaseModel> enumerable)
    {
      if (enumerable.IsNullOrEmpty())
      {
        return Structs.BaseRepository<TBaseModel>.EmptyEnumerable;
      }

      return enumerable.GroupBy(x => x.Id)
        .Select(x => x.First());
    }

    /// <summary>
    /// Get the next valid <see langword="uint"/>.
    /// </summary>
    /// <param name="enumerable">The enumerable</param>
    /// <returns>The next valid value.</returns>
    private uint Next(this IEnumerable<uint> enumerable)
    {
      if (enumerable.IsNullOrEmpty())
      {
        return uint.MinValue;
      }

      uint value = enumerable.Max();

      if (value == uint.MaxValue)
      {
        return value;
      }

      value++;
      return value;
    }

    /// <summary>
    /// Get an enumerable of <typeparamref name="TBaseModel"/>(s) of which the
    /// property <see langword="Id"/> is unique. Updates any duplicate(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    private IEnumerable<TBaseModel> GetUniqueRange
    (this IEnumerable<TBaseModel> enumerable)
    {
      var newEnumerable = Structs.BaseRepository<TBaseModel>.EmptyEnumerable;

      if (enumerable.IsNullOrEmpty())
      {
        return newEnumerable;
      }

      var uniqueEnumerable = this.GetModifiedUniqueRange(enumerable);
      var maxId = uniqueEnumerable.Max(x => x.Id);

      if (uniqueEnumerable.Count() == maxId)
      {
        return uniqueEnumerable;
      }

      var duplicateEnumerable = enumerable.GroupBy(x => x.Id)
        .Where(x => x.Count() > 1)
        .Select(x => x.Key.Va);

      var duplicateIdEnumerable = duplicateEnumerable.Select(x => x.Id);

      foreach (var item in duplicateEnumerable)
      {
        while (true)
        {
          bool hasDuplicates = enumerable.Where(x => x.Id == item.Id)
            .Count() > 1;

          if (hasDuplicates)
          {
            item.Id = this.Next(duplicateIdEnumerable);
          }

          newEnumerable.Add(item);

          if (!hasDuplicates)
          {
            break;
          }
        }
      }


      return newEnumerable;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public BaseRepository() :
      base(Structs.BaseRepository<TBaseModel>.EmptyEnumerable)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public BaseRepository(ObservableCollection<TBaseModel> enumerable) :
      base(enumerable)
    {
      var idEnumerable = enumerable.GroupBy(x => x.Id)
        .Where(x => x.Count() > 1)
        .Select(x => x.Key);

      idEnumerable = this.Next(idEnumerable);

      enumerable.Select(x =>)
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <param name="maxCount">The maximum count of item(s)</param>
    [ExcludeFromCodeCoverage]
    public BaseRepository
    (
      ObservableCollection<TBaseModel> enumerable,
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

    public IEnumerable<TBaseModel> GetAntiRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.NotContainsIdEnumerable(idEnumerable);
      return base.GetRange(func);
    }

    public IEnumerable<TBaseModel> GetAntiRange
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TBaseModel>.NotContainsIdRange
        (
          startId,
          endId
        );

      return base.GetRange(func);
    }

    public IEnumerable<TBaseModel> GetRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);
      return base.GetRange(func);
    }

    public IEnumerable<TBaseModel> GetRange
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdRange
        (
          startId,
          endId
        );

      return base.GetRange(func);
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

      base.AddRange(enumerable);
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

      this.SelectedIdEnumerable
        .Remove(model.Id);
    }

    public void Deselect(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);
      this.Deselect(func);
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

    public void DeselectRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);
      this.DeselectRange(func);
    }

    public bool Remove(Func<TBaseModel, bool> func)
    {
      if (func == null)
      {
        return false;
      }

      var model = this.Get(func);

      return this.Enumerable
        .Remove(model);
    }

    public bool Remove(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);
      return this.Remove(func);
    }

    public IEnumerable<bool> RemoveRange(Func<TBaseModel, bool> func)
    {
      if (func == null)
      {
        yield break;
      }

      var enumerable = base.GetRange(func);

      if (enumerable.IsNullOrEmpty())
      {
        yield break;
      }

      foreach (var item in enumerable)
      {
        yield return base.Enumerable
          .Remove(item);
      }
    }

    public IEnumerable<bool> RemoveRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);
      return this.RemoveRange(func);
    }

    public IEnumerable<bool> RemoveRange
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdRange
        (
          startId,
          endId
        );

      return this.RemoveRange(func);
    }

    public void DeselectAll()
    {
      foreach (var item in base.GetAll())
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

      this.SelectedIdEnumerable
        .Add(model.Id);
    }

    public void Select(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);
      this.Select(func);
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
      if (enumerable.IsNullOrEmpty())
      {
        return;
      }

      foreach (var item in enumerable)
      {
        this.Select(item);
      }
    }

    public void SelectRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);
      this.SelectRange(func);
    }

    public void SelectAll()
    {
      foreach (var item in base.GetAll())
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

      if (!this.IsValidId(model.Id))
      {
        return;
      }

      var func = BaseFunctions<TBaseModel>.ContainsId(model.Id);
      var result = this.Remove(func);

      if (result)
      {
        return;
      }

      base.Add(model);
    }

    public void UpdateRange(IEnumerable<TBaseModel> enumerable)
    {
      if (enumerable.IsNullOrEmpty())
      {
        return;
      }

      foreach (var item in enumerable)
      {
        this.Update(item);
      }
    }

    #endregion
  }
}