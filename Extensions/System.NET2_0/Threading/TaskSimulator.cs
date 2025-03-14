using System;
using System.Threading;

namespace System.Threading
{
  public static class TaskSimulator
  {
    #region Logic

    public static void Run(Action action)
    {
      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      ThreadPool.QueueUserWorkItem(_ => action());
    }

    public async static void RunAsync
    (
      Action action, 
      Action callbackAction = null
    )
    {
      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      await ThreadPool.QueueUserWorkItem
      (
        _ =>
        {
          try
          {
            action();
          }

          finally
          {
            callbackAction?.Invoke();
          }
        }
      );
    }

    public static void RunAsync<T>
    (
      Func<T> func,
      Action callbackAction = null
    )
    {
      if (func == null)
      {
        throw new ArgumentNullException(nameof(func));
      }

      ThreadPool.QueueUserWorkItem
      (
        _ =>
        {
          try
          {
            func();
          }

          finally
          {
            callbackAction?.Invoke();
          }
        }
      );
    }

    #endregion
  }
}