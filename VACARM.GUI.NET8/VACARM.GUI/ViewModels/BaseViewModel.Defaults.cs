using VACARM.GUI.Structs;

namespace VACARM.GUI.ViewModels
{
  public partial class BaseViewModel
    <
      TBaseGroupService,
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TBaseModel
    >
  {
    #region Parameters

    public virtual Size DefaultSize { get; set; } = DefaultBaseViewModel.Size;
    public virtual string SelectDefaultName { get; set; } = string.Empty;

    #endregion
  }
}