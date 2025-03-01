namespace VACARM.GUI.Controllers
{
  internal partial class BaseController
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    /// <summary>
    /// Select/Deselect the corresponding <typeparamref name="TBaseModel"/>.
    /// </summary>
    internal EventHandler? CheckedChangedEventHandler
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
    internal EventHandler? AllCheckedChangedEventHandler(bool isChecked)
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
    /// Select/Deselect the corresponding enumerable of some
    /// <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <param name="isChecked">True/false</param>
    internal EventHandler? RangeCheckedChangedEventHandler
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

    #endregion
  }
}