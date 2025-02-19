using VACARM.Application.Services;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The readonly repository of <typeparamref name="TService"/>(s).
  /// </summary>
  public class ReadonlyGroupService
    <
      TServiceRepository,
      TService,
      TRepository,
      TItem
    > :
    ReadonlyRepository
    <
      ReadonlyService
      <
        TRepository,
        TItem
      >
    >,
    IReadonlyGroupService
    <
      ReadonlyRepository
      <
        ReadonlyService
        <
          ReadonlyRepository<TItem>,
          TItem
        >
      >,
      ReadonlyService
      <
        ReadonlyRepository<TItem>,
        TItem
      >,
      TRepository,
      TItem
    >
    where TServiceRepository :
    ReadonlyRepository<TService>
    where TService :
    ReadonlyService
    <
      TRepository,
      TItem
    >
    where TRepository :
    ReadonlyRepository<TItem>
    where TItem :
    class
  {
    #region Parameters

    private int maxCount { get; set; } = SafeMaxCount;
    private int selectedIndex { get; set; } = MinCount;
    private readonly static int MinCount = 0;

    /// <summary>
    /// The list of all <typeparamref name="TService"/>(s).
    /// </summary>
    protected List<ReadonlyService<TRepository, TItem>> ReadonlyServiceList
    {
      get
      {
        return this.Enumerable
          .ToList();
      }
      set
      {
        this.Enumerable = value;
        this.OnPropertyChanged(nameof(ReadonlyServiceList));
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

    public ReadonlyService<TRepository, TItem>? SelectedReadonlyService
    {
      get
      {
        try
        {
          return this.ReadonlyServiceList
            .ElementAt(this.SelectedIndex);
        }
        catch
        {
          return null;
        }
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
    ReadonlyGroupService() :
      base()
    {
      this.ReadonlyServiceList = new List<ReadonlyService<TRepository, TItem>>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceList">The service list</param>
    /// <param name="maxCount">The maximum count of service(s)</param>
    public ReadonlyGroupService
    (
      List<ReadonlyService<TRepository, TItem>> serviceList,
      int maxCount
    )
    {
      this.ReadonlyServiceList = serviceList;
      this.MaxCount = maxCount;
    }

    public bool Add(ReadonlyService<TRepository, TItem> service)
    {
      if (this.Enumerable.Count() >= this.MaxCount)
      {
        return false;
      }

      this.ReadonlyServiceList
        .Add(service);

      return true;
    }

    public bool Remove(int index)
    {
      if (!this.ContainsIndex(index))
      {
        return false;
      }

      this.ReadonlyServiceList
        .RemoveAt(index);

      return true;
    }

    public ReadonlyService<TRepository, TItem>? Get(int index)
    {
      if (this.IsNullOrEmpty(this.ReadonlyServiceList))
      {
        return null;
      }

      try
      {
        return this.ReadonlyServiceList
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