using System.Diagnostics;
using VACARM.Infrastructure.Observers.Observer;
using VACARM.Infrastructure.Subject;

namespace VACARM.Infrastructure.Observers.Observer
{
  public class Observer<TItem> :
    IObserver<TItem>
    where TItem :
    class
  {
    #region Parameters

    private TItem Item { get; set; }

    #endregion

    #region Logic

    public virtual void Update(TItem item)
    {
      this.Item = item;
      Debug.WriteLine(nameof(Observer) + ": Reacted to the event.");
    }

    #endregion
  }
}