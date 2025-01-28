using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VACARM.Core.Entities
{
  public partial interface IEntity
  {
    #region Parameters

    bool IsSelected { get; set; }
    bool IsSelectable { get; set; }

    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    void NotifyPropertyChanged([CallerMemberName] string propertyName = "")

    #endregion
  }
}