using VACARM.Application.Services;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The service repository of <typeparamref name="TService"/>(s).
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
        TRepository,
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
    protected List<ReadonlyService<TRepository, TItem>> ServiceList
    {
      get
      {
        return this.Enumerable
          .ToList();
      }
      set
      {
        this.Enumerable = value;
        this.OnPropertyChanged(nameof(ServiceList));
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

    public ReadonlyService<TRepository, TItem>? SelectedService
    {
      get
      {
        try
        {
          return this.ServiceList
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
    GroupService() :
      base()
    {
      this.ServiceList = new List<ReadonlyService<TRepository, TItem>>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceList">The service list</param>
    /// <param name="maxCount">The maximum count of service(s)</param>
    public GroupService
    (
      List<ReadonlyService<TRepository, TItem>> serviceList,
      int maxCount
    )
    {
      this.ServiceList = serviceList;
      this.MaxCount = maxCount;
    }

    public bool Add(ReadonlyService<TRepository, TItem> service)
    {
      if (this.Enumerable.Count() >= this.MaxCount)
      {
        return false;
      }

      this.ServiceList
        .Add(service);

      return true;
    }

    public bool Remove(int index)
    {
      if (!this.ContainsIndex(index))
      {
        return false;
      }

      this.ServiceList
        .RemoveAt(index);

      return true;
    }

    public ReadonlyService<TRepository, TItem>? Get(int index)
    {
      if (this.IsNullOrEmpty(this.ServiceList))
      {
        return null;
      }

      try
      {
        return this.ServiceList
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