using System.ComponentModel;
using System.Diagnostics;

namespace VACARM.Infrastructure.Repositories
{
  public partial class Repository
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

    private void OnPropertyChangedCallback(string )

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    protected void OnPropertyChanged(string propertyName)
    {
      this.PropertyChanged?
        .Invoke
        (
          this,
          new PropertyChangedEventArgs(propertyName)
        );

      OnPropertyChangedCallback?.Invoke(propertyName);

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