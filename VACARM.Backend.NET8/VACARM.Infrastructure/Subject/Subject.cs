using System;
using System.Diagnostics;
using VACARM.Infrastructure.Repositories;

//TODO: add business logic?

namespace VACARM.Infrastructure.Subject
{
  public class Subject<> :
    Repository
    <
      List<WeakReference<TObserver>>,
      WeakReference<IObserver>
    >,
    ISubject
  {
    #region Parameters

    private readonly static string MessagePrefix =
      nameof(Subject) + ": {0}.";

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public Subject() :
      base(new List<WeakReference<IObserver>>())
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">the list of observer(s)</param>
    public Subject(List<WeakReference<IObserver>> list) :
      base(list)
    {
      this.Enumerable = list;
    }

    public override void Add(WeakReference<IObserver> observerWeakReference)
    {
      lock (this.Enumerable)
      {
        if (observerWeakReference == null)
        {
          return;
        }

        base.Add(observerWeakReference);

        string message = string.Format
          (
            MessagePrefix,
            "Added an observer."
          );

        Debug.WriteLine(message);
      }
    }

    public override void AddRange(IEnumerable<WeakReference<IObserver>> enumerable)
    {
      lock (this.Enumerable)
      {
        if (enumerable.IsNullOrEmpty())
        {
          return;
        }

        base.AddRange(enumerable);

        string message = string.Format
          (
            MessagePrefix,
            "Added {1} observer(s).",
            enumerable.Count()
          );

        Debug.WriteLine(message);
      }
    }

    public void RemoveRange(IObserver observer)
    {
      lock (this.Enumerable)
      {
        if (observer == null)
        {
          return;
        }

        if (this.IsNullOrEmpty)
        {
          return;
        }

        var oldCount = this.Count;

        this.Enumerable
          .RemoveAll
          (
            weakReference =>
            weakReference.TryGetTarget(out var targetObserver)
            && targetObserver == observer
          );

        var difference = this.Count - oldCount;

        string message = string.Format
          (
            MessagePrefix,
            "Removed {1} observer(s).",
            difference
          );

        Debug.WriteLine(message);
      }
    }

    public void NotifyAll()
    {
      if (base.IsNullOrEmpty)
      {
        return;
      }

      foreach (var weakReference in this.GetAll())
      {
        if (weakReference.TryGetTarget(out var targetObserver))
        {
          continue;
        }

        targetObserver.Update(this);
      }
    }

    #endregion
  }
}