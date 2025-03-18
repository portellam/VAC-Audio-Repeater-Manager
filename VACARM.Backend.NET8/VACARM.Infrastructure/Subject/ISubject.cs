using VACARM.Infrastructure.Observers.Observer;

namespace VACARM.Infrastructure.Subject
{
  public interface ISubject
  {
    #region Logic

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="IObserver"/>(s).
    /// </summary>
    /// <param name="observer">The observer</param>
    void RemoveRange(IObserver observer);

    /// <summary>
    /// Notify all subscribed observers when the state of the subject is changed.
    /// </summary>
    void NotifyAll();

    #endregion
  }
}