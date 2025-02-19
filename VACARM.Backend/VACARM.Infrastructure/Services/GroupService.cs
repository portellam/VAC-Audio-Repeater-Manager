using VACARM.Application.Services;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The repository of <typeparamref name="TService"/>(s).
  /// </summary>
  public class GroupService
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
        ReadonlyRepository<TItem>,
        TItem
      >
    >,
    IGroupService
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
      ReadonlyRepository<TItem>,
      TItem
    >
    where TServiceRepository :
    ReadonlyRepository
    <
      ReadonlyService
      <
        ReadonlyRepository<TItem>,
        TItem
      >
    >
    where TService :
    ReadonlyService
    <
      ReadonlyRepository<TItem>,
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
    protected List<ReadonlyService<ReadonlyRepository<TItem>, TItem>>
      ReadonlyServiceList
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

    public ReadonlyService<ReadonlyRepository<TItem>, TItem>?
    SelectedReadonlyService
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
    public GroupService() :
      base()
    {
      this.ReadonlyServiceList =
        new List<ReadonlyService<ReadonlyRepository<TItem>, TItem>>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="readonlyServiceList">The list of services(s)</param>
    /// <param name="maxCount">The maximum count of service(s)</param>
    public GroupService
    (
      List<ReadonlyService<ReadonlyRepository<TItem>, TItem>> readonlyServiceList,
      int maxCount
    )
    {
      this.ReadonlyServiceList = readonlyServiceList;
      this.MaxCount = maxCount;
    }

    public bool Add
    (ReadonlyService<ReadonlyRepository<TItem>, TItem> readonlyService)
    {
      if (this.Enumerable.Count() >= this.MaxCount)
      {
        return false;
      }

      this.ReadonlyServiceList
        .Add(readonlyService);

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

    public ReadonlyService<ReadonlyRepository<TItem>, TItem>? Get(int index)
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