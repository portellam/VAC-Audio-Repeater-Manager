using System.ComponentModel;
using System.Diagnostics;
using VACARM.Application.Services;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  public class BaseServiceRepository
    <
      TBaseService,
      TBaseModel
    > :
    IDisposable,
    INotifyPropertyChanged,
    IBaseServiceRepository
    <
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >,
      TBaseModel
    >
    where TBaseService :
    BaseService
    <
      BaseRepository<TBaseModel>,
      TBaseModel
    >
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    private int maxCount { get; set; } = SafeMaxCount;
    private int selectedIndex { get; set; } = MinCount;
    private readonly static int MinCount = 0;

    private List<BaseService<BaseRepository<TBaseModel>, TBaseModel>> list
    { get; set; }

    public BaseService<BaseRepository<TBaseModel>, TBaseModel>? SelectedBaseService
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

    private List<BaseService<BaseRepository<TBaseModel>, TBaseModel>> List
    {
      get
      {
        return this.list;
      }
      set
      {
        if (value.Count() >= this.MaxCount)
        {
          return;
        }

        this.list = value;
        this.OnPropertyChanged(nameof(List));
      }
    }

    public readonly static int SafeMaxCount = byte.MaxValue;
    public virtual bool HasDisposed { get; set; }
    public virtual event PropertyChangedEventHandler PropertyChanged;

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
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    internal virtual void OnPropertyChanged(string propertyName)
    {
      this
        .PropertyChanged?
        .Invoke
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
    public BaseServiceRepository()
    {
      this.List =
        new List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The service list</param>
    public BaseServiceRepository
    (List<BaseService<BaseRepository<TBaseModel>, TBaseModel>> list)
    {
      this.List = list;
    }

    /// <summary>
    /// Dispose of unmanaged objects and true/false dispose of managed objects.
    /// </summary>
    /// <param name="isDisposed">True/false</param>
    protected virtual void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        this.List =
          new List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>();
      }

      this.HasDisposed = true;
    }

    /// <summary>
    /// Do not change this code. 
    /// Put cleanup code in Dispose(<paramref name="bool"/>
    ///  <typeparamref name="isDisposed"/>) method.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    public BaseService<BaseRepository<TBaseModel>, TBaseModel>? Get(int index)
    {
      if
      (
        IEnumerableExtension<BaseService<BaseRepository<TBaseModel>, TBaseModel>>
          .IsNullOrEmpty(this.List)
      )
      {
        return null;
      }

      return this.List
        .ElementAt(this.SelectedIndex);
    }

    public bool Remove(int index)
    {
      return this.Remove(index);
    }

    public void Add(BaseService<BaseRepository<TBaseModel>, TBaseModel> baseService)
    {
      this.List
        .Add(baseService);
    }

    #endregion
  }
}