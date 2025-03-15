using VACARM.GUI.Structs;

namespace VACARM.GUI.ViewModels
{
  public partial class BaseViewModel
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    public virtual Size DefaultSize { get; set; } = DefaultBaseViewModel.Size;
    public virtual string SelectDefaultText { get; set; } = string.Empty;

    #endregion
  }
}