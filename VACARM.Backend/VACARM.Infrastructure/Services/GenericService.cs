using System.ComponentModel;
using System.Diagnostics;
using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial class GenericService<TRepository, TItem> :
    IGenericService<TRepository, TItem> where TRepository :
    GenericRepository<TItem> where TItem :
    class
  {
    #region Parameters

    internal virtual TRepository _Repository
    {
      get
      {
        return (TRepository)repository;
      }
      set
      {
        repository = value;
        OnPropertyChanged(nameof(_Repository));
      }
    }

    private GenericRepository<TItem> repository { get; set; } =
      new GenericRepository<TItem>();

    public TRepository Repository
    {
      get
      {
        return this._Repository;
      }
    }

    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    internal virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke
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
    public GenericService()
    {
      repository = new GenericRepository<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public GenericService(GenericRepository<TItem> repository)
    {
      this.repository = repository;
    }

    public void DoWork
    (
      Action<TItem> action,
      Func<TItem, bool> func
    )
    {
      if (action == null)
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      var item = this._Repository.Get(func);

      this.DoWork
      (
        action,
        item
      );
    }

    public void DoWork
    (
      Action<TItem> action,
      TItem item
    )
    {
      if (action == null)
      {
        return;
      }

      if (item == null)
      {
        return;
      }

      action(item);
    }

    public void DoWorkAll(Action<TItem> action)
    {
      if (action == null)
      {
        return;
      }

      foreach (var item in this._Repository.GetAll())
      {
        this.DoWork
        (
          action,
          item
        );
      }
    }

    public void DoWorkRange
    (
      Action<TItem> action,
      IEnumerable<TItem> enumerable
    )
    {
      if (IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable))
      {
        return;
      }

      foreach (var item in enumerable)
      {
        this.DoWork
        (
          action,
          item
        );
      }
    }

    public void DoWorkRange
    (
      Action<TItem> action,
      Func<TItem, bool> func
    )
    {
      if (action == null)
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      this.DoWorkRange
      (
        action,
        this._Repository.GetRange(func)
      );
    }

    #endregion
  }
}