﻿using VACARM.Application.Services;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The repository of <typeparamref name="TBaseService"/>(s).
  /// </summary>
  public class BaseGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TBaseModel
    > :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >
    >,
    IBaseGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          BaseRepository<TBaseModel>,
          TBaseModel
        >
      >,
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >,
      BaseRepository<TBaseModel>,
      TBaseModel
    >
    where TGroupReadonlyRepository :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >
    >
    where TBaseService :
    BaseService
    <
      BaseRepository<TBaseModel>,
      TBaseModel
    >
    where TBaseRepository :
    BaseRepository<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    private int maxCount { get; set; } = SafeMaxCount;
    private int selectedIndex { get; set; } = MinCount;
    private readonly static int MinCount = 0;

    protected BaseRepository<TBaseModel>? SelectedRepository
    {
      get
      {
        return this.SelectedService
          .Repository;
      }
    }

    protected BaseService<BaseRepository<TBaseModel>, TBaseModel>?
    SelectedService
    {
      get
      {
        try
        {
          return this.List
            .ElementAt(this.SelectedIndex);
        }
        catch
        {
          return null;
        }
      }
    }

    protected List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>?
    List
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

    public int SelectedIndex
    {
      get
      {
        return this.selectedIndex;
      }
      set
      {
        if (value < MinCount)
        {
          value = MinCount;
        }

        this.selectedIndex = value;
        this.OnPropertyChanged(nameof(SelectedIndex));
      }
    }

    public readonly static int SafeMaxCount = byte.MaxValue;

    public virtual int MaxCount
    {
      get
      {
        return this.maxCount;
      }
      internal set
      {
        if (value < MinCount)
        {
          value = MinCount;
        }

        this.maxCount = value;
        this.OnPropertyChanged(nameof(MaxCount));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseGroupService() :
      base()
    {
      this.List = new List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of services(s)</param>
    /// <param name="maxCount">The maximum count of service(s)</param>
    public BaseGroupService
    (
      List<BaseService<BaseRepository<TBaseModel>, TBaseModel>> list,
      int maxCount
    )
    {
      this.List = list;
      this.MaxCount = maxCount;
    }

    public bool Add
    (BaseService<BaseRepository<TBaseModel>, TBaseModel> baseService)
    {
      if (this.IsNullOrEmpty)
      {
        return false;
      }

      if
      (
        this.Enumerable
          .Count() >= this.MaxCount
      )
      {
        return false;
      }

      this.List
        .Add(baseService);

      return true;
    }

    public bool Remove(int index)
    {
      if (this.IsNullOrEmpty)
      {
        return false;
      }

      if (!this.ContainsIndex(index))
      {
        return false;
      }

      this.List
        .RemoveAt(index);

      return true;
    }

    public BaseService<BaseRepository<TBaseModel>, TBaseModel>? Get(int index)
    {
      try
      {
        return this.List
          .ElementAt(index);
      }
      catch
      {
        return null;
      }
    }

    #endregion
  }
}