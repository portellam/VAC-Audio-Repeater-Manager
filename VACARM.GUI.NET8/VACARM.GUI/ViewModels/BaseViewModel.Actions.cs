namespace VACARM.GUI.ViewModels
{
  public partial class BaseViewModel
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Logic

    /// <summary>
    /// Select/Deselect all given <see langword="isChecked"/> is true/false.
    /// </summary>
    /// <param name="isChecked">True/false</param>
    private void SelectAllOnCheck(bool? isChecked)
    {
      if (this.GroupService == null)
      {
        return;
      }

      if (!isChecked.HasValue)
      {
        isChecked = false;
      }

      if (isChecked.Value)
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
      bool? isChecked
    )
    {
      if (this.GroupService == null)
      {
        return;
      }

      if (!isChecked.HasValue)
      {
        isChecked = false;
      }

      if (isChecked.Value)
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
    /// Select/Deselect a range given <see langword="isChecked"/> is true/false.
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <param name="isChecked">True/false</param>
    private void SelectRangeOnCheck
    (
      IEnumerable<uint> idEnumerable,
      bool? isChecked
    )
    {
      if (this.GroupService == null)
      {
        return;
      }

      if (!isChecked.HasValue)
      {
        isChecked = false;
      }

      if (isChecked.Value)
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