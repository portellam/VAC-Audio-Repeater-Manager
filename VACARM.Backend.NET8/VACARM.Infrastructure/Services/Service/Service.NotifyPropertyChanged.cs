using System.ComponentModel;
using System.Diagnostics;

namespace VACARM.Infrastructure.Services
{
  public partial class Service
    <
      TEnumerable,
      TItem
    > :
    INotifyPropertyChanged
  {
    #region Parameters

    public virtual event PropertyChangedEventHandler PropertyChanged;

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

    #endregion
  }
}