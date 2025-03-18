using VACARM.Infrastructure.Subject;

namespace VACARM.Infrastructure.Observers.Observer
{
  public interface IObserver<TItem>
  {
    #region Logic

    /// <summary>
    /// Receive an update from a <typeparamref name="TItem"/>.
    /// </summary>
    /// <param name="item">the item</param>
    void Update(TItem item);

    #endregion
  }
}