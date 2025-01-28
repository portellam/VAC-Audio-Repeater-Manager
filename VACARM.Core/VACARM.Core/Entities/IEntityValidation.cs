using System.ComponentModel;

namespace VACARM.Core.Entities
{
  public partial interface IEntity
  {
    #region Parameters

    bool HasErrors { get; }
    event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    #endregion

    #region Logic

    void OnErrorsChanged(string propertyName);
    Task ValidateAsync();
    void Validate();

    #endregion
  }
}