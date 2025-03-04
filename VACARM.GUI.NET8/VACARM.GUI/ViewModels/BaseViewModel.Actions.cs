namespace VACARM.GUI.ViewModels
{
  internal partial class BaseViewModel
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Logic

    /// <summary>
    /// Select/Deselect given <see langword="isChecked"/> is true/false.
    /// </summary>
    /// <param name="isChecked">True/false</param>
    private void SelectAllOnCheck(bool isChecked)
    {
      if (isChecked)
      {
        this.GroupService
          .SelectedRepository
          .SelectAll();
      }

      else
      {
        this.GroupService
          .SelectedRepository
          .DeselectAll();
      }
    }

    /// <summary>
    /// Select/Deselect given <see langword="isChecked"/> is true/false.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="isChecked">True/false</param>
    private void SelectOnCheck
    (
      uint id,
      bool isChecked
    )
    {
      if (isChecked)
      {
        this.GroupService
          .Select(id);
      }

      else
      {
        this.GroupService
          .Deselect(id);
      }
    }

    /// <summary>
    /// Select/Deselect given <see langword="isChecked"/> is true/false.
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <param name="isChecked">True/false</param>
    private void SelectRangeOnCheck
    (
      IEnumerable<uint> idEnumerable,
      bool isChecked
    )
    {
      if (isChecked)
      {
        this.GroupService
          .SelectRange(idEnumerable);
      }

      else
      {
        this.GroupService
          .DeselectRange(idEnumerable);
      }
    }

    #endregion
  }
}