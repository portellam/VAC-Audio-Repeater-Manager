using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace VACARM.Core.Entities
{
  public partial class Entity :
    INotifyPropertyChanged,
    IEntity
  {
    #region Parameters

    private bool isSelected;

    /// <summary>
    /// True/false is the entity selected.
    /// </summary>
    [NotMapped]
    public virtual bool IsSelected
    {
      get
      {
        return isSelected;
      }
      set
      {
        if (isSelected != value)
        {
          isSelected = value;
          NotifyPropertyChanged();
        }
      }
    }

    private bool isSelectable = true;

    /// <summary>
    /// True/false is the entity able to be selected.
    /// </summary>
    [NotMapped]
    public virtual bool IsSelectable
    {
      get
      {
        return isSelectable;
      }
      set
      {
        if (isSelectable != value)
        {
          isSelectable = value;
          NotifyPropertyChanged();
        }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <summary>
    /// <param name="propertyName">The property name</param>
    public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      if (PropertyChanged != null)
      {
        PropertyChanged
          (
            this, new PropertyChangedEventArgs(propertyName)
          );
      }
    }

    #endregion
  }
}