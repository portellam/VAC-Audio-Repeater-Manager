namespace VACARM.Core.Entities
{
  public partial interface IEntity
  {
    #region Logic

    void BeginEdit();
    void CancelEdit();
    void EndEdit();

    #endregion
  }
}