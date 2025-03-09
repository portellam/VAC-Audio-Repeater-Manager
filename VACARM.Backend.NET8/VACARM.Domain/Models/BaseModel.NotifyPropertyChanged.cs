using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.Domain.Models
{
  public partial class BaseModel :
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
    internal void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?
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