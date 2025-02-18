namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// The <typeparamref name="TRepository"/> repository.
  /// </summary>
  public class RecursiveRepository
    <
      TRecursiveRepository,
      TRepository,
      TItem
    > :
    ReadonlyRepository<TRepository>,
    IRecursiveRepository
    <
      TRecursiveRepository,
      TRepository,
      TItem
    >
    where TRecursiveRepository :
    ReadonlyRepository<TRepository>
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
    /// The list of all <typeparamref name="TRepository"/>(s).
    /// </summary>
    protected List<TRepository> RepositoryEnumerable
    {
      get
      {
        return this.Enumerable
          .ToList();
      }
      set
      {
        this.Enumerable = value;
        this.OnPropertyChanged(nameof(RepositoryEnumerable));
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

    public ReadonlyRepository<TItem>? SelectedRepository
    {
      get
      {
        try
        {
          return this.RepositoryEnumerable
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
    public RecursiveRepository() :
      base()
    {
      this.RepositoryEnumerable = new List<TRepository>();
    }

    public bool Add(TRepository repository)
    {
      if (this.Enumerable.Count() >= this.MaxCount)
      {
        return false;
      }


      this.RepositoryEnumerable
        .Add(repository);

      return true;
    }

    public bool Remove(int index)
    {
      if (!this.ContainsIndex(index))
      {
        return false;
      }

      this.RepositoryEnumerable
        .RemoveAt(index);

      return true;
    }

    public ReadonlyRepository<TItem>? Get(int index)
    {
      if (this.IsNullOrEmpty(this.RepositoryEnumerable))
      {
        return null;
      }

      try
      {
        return this.RepositoryEnumerable
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