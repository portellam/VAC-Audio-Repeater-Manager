using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Extensions
{
  public static class Task
  {
    #region Logic

    /// <summary>
    /// Starts the new <see cref="Task"/> from <paramref name="action"/> on the
    /// Default (usually <typeparamref name="ThreadPool"/>) task scheduler
    /// (not on the <typeparamref name="TaskScheduler.Current"/>).
    /// It is a NET Framework 4.0 method nearly analogous to 4.5 Task.Run.
    /// </summary>
    /// <typeparam name="T">The type of the return value.</typeparam>
    /// <param name="taskFactory">The task factory to start from.</param>
    /// <param name="action">The action to execute.</param>
    /// <returns>The task representing the execution of the
    /// <paramref name="action"/>.</returns>
    public static System.Threading.Tasks.Task StartNewOnDefaultScheduler
    (
      this System.Threading.Tasks.TaskFactory taskFactory,
      Action action
    )
    {
      Contract.Requires(taskFactory != null);
      Contract.Requires(action != null);

      return taskFactory.StartNew
        (
          action,
          cancellationToken: CancellationToken.None,
          creationOptions: TaskCreationOptions.None,
          scheduler: TaskScheduler.Default
        );
    }

    /// <summary>
    /// Starts the new <see cref="Task"/> from <paramref name="func"/> on the
    /// Default (usually <typeparamref name="ThreadPool"/>) task scheduler
    /// (not on the <typeparamref name="TaskScheduler.Current"/>).
    /// It is a NET Framework 4.0 method nearly analogous to 4.5 Task.Run.
    /// </summary>
    /// <typeparam name="T">The type of the return value.</typeparam>
    /// <param name="taskFactory">The task factory to start from.</param>
    /// <param name="func">The function to execute.</param>
    /// <returns>The task representing the execution of the
    /// <paramref name="func"/>.</returns>
    public static System.Threading.Tasks.Task StartNewOnDefaultScheduler<T>
    (
      this TaskFactory taskFactory,
      Func<T> func
    )
    {
      Contract.Requires(taskFactory != null);
      Contract.Requires(func != null);

      return taskFactory.StartNew
        (
          func,
          cancellationToken: CancellationToken.None,
          creationOptions: TaskCreationOptions.None,
          scheduler: TaskScheduler.Default
        );
    }

    #endregion
  }
}