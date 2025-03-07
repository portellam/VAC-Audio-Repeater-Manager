namespace VACARM.GUI.ViewModels
{
  public partial class BaseViewModel
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    /// <summary>
    /// Select/Deselect the corresponding <typeparamref name="TBaseModel"/>.
    /// </summary>
    public EventHandler? CheckedChangedEventHandler
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
    /// Select/Deselect the corresponding enumerable of all
    /// <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="isChecked">True/false</param>
    private EventHandler? SelectAllCheckedChangedEventHandler(bool? isChecked)
    {
      if (isChecked == null)
      {
        isChecked = false;
      }

      return
        (
          sender,
          eventArgs
        ) =>
        {
          this.SelectAllOnCheck(isChecked.Value);
        };
    }

    /// <summary>
    /// Select/Deselect the corresponding enumerable of some
    /// <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <param name="isChecked">True/false</param>
    private EventHandler? SelectRangeCheckedChangedEventHandler
    (
      IEnumerable<uint> idEnumerable,
      bool isChecked
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

    // TOOD: implement?

    /// <summary>
    /// Select/Deselect the corresponding <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="isChecked">True/false</param>
    private EventHandler? SelectCheckedChangedEventHandler(bool? isChecked)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}