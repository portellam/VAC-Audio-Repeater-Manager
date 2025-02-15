using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// The readonly repository.
  /// </summary>
  public class ReadonlyRepository<TItem> :
    IReadonlyRepository<TItem> where TItem :
    class
  {
    #region Parameters

    /// <summary>
    /// The enumerable of all <typeparamref name="TItem"/>(s).
    /// </summary>
    protected IEnumerable<TItem> Enumerable
    {
      get
      {
        return this.enumerable;
      }
      set
      {
        this.enumerable = value;
        this.OnPropertyChanged(nameof(Enumerable));
      }
    }

    private IEnumerable<TItem> enumerable { get; set; }

    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    internal void OnPropertyChanged(string propertyName)
    {
      this.PropertyChanged?.Invoke
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
    [ExcludeFromCodeCoverage]
    public ReadonlyRepository()
    {
      this.Enumerable = Array.Empty<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public ReadonlyRepository(IEnumerable<TItem> enumerable)
    {
      this.Enumerable = enumerable;
    }

    public bool ContainsIndex(int index)
    {
      return index >= 0
        && index <= this.Enumerable.Count();
    }

    public bool IsNullOrEmpty(IEnumerable<TItem> enumerable)
    {
      return IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable);
    }

    public TItem? Get(Func<TItem, bool> func)
    {
      if (this.IsNullOrEmpty(this.Enumerable))
      {
        return null;
      }

      return this.Enumerable.
        FirstOrDefault(func);
    }

    public IEnumerable<TItem> GetAll()
    {
      if (IsNullOrEmpty(this.Enumerable))
      {
        return Array.Empty<TItem>();
      }

      return this.Enumerable
        .AsEnumerable();
    }

    public IEnumerable<TItem> GetRange(Func<TItem, bool> func)
    {
      if (IsNullOrEmpty(this.Enumerable))
      {
        return Array.Empty<TItem>();
      }

      return this.Enumerable
        .Where(x => func(x))
        .AsEnumerable();
    }

    #endregion
  }
}