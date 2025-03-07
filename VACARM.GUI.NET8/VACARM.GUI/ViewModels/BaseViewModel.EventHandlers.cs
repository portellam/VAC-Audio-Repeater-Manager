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

    /// <summary>
    /// Select/Deselect a <typeparamref name="TBaseModel"/>.
    /// </summary>
    private EventHandler? SelectCheckedChangedEventHandler
    {
      get
      {
        return
          (
            sender,
            eventArgs
          ) =>
          {
            if (sender == null)
            {
              return;
            }

            if (sender.GetType() != typeof(ToolStripMenuItem))
            {
              return;
            }

            var toolStripMenuItem = sender as ToolStripMenuItem;
            string idString = IdFunc(toolStripMenuItem);

            uint id;

            var result = uint.TryParse
              (
                idString,
                out id
              );

            if (!result)
            {
              return;
            }

            var isChecked = SelectedFunc(toolStripMenuItem);

            this.SelectOnCheck
              (
                id,
                isChecked
              );
          };
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Select/Deselect all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="isChecked">True/false</param>
    private EventHandler? SelectAllCheckedChangedEventHandler(bool? isChecked)
    {
      return
        (
          sender,
          eventArgs
        ) =>
        {
          this.SelectAllOnCheck(isChecked);
        };
    }

    /// <summary>
    /// Select/Deselect a range of <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <param name="isChecked">True/false</param>
    private EventHandler? SelectRangeCheckedChangedEventHandler
    (
      IEnumerable<uint> idEnumerable,
      bool? isChecked
    )
    {
      return
        (
          sender,
          eventArgs
        ) =>
        {
          this.SelectRangeOnCheck
          (
            idEnumerable,
            isChecked
          );
        };
    }

    #endregion
  }
}