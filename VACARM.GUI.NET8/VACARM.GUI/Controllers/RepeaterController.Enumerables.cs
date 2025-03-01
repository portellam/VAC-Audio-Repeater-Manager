using VACARM.Infrastructure.Extensions;

namespace VACARM.GUI.Controllers
{
  internal partial class RepeaterController
  {
    #region Parameters

    private IEnumerable<ToolStripMenuItem> allToolStripMenuItemEnumerable
    { get; set; }

    /*
     * TODO:
     * 1. populate this with models from RepeaterGroupService
     * 2. using the individual getters, define the properties' of each group of
     * items, append to this enumerable, and sort by ID?
     * Q: will this be performant?
     */
    internal IEnumerable<ToolStripMenuItem> AllToolStripMenuItemEnumerable
    {
      get;
      set;
    }

    internal IEnumerable<ToolStripMenuItem>
    AbsentToolStripMenuItemEnumerable
    {
      get
      {
        return this.GetToolStripMenuItemEnumerable(this.AbsentIdEnumerable);
      }
    }

    internal IEnumerable<ToolStripMenuItem>
    CheckedToolStripMenuItemEnumerable
    {
      get
      {
        return this.AllToolStripMenuItemEnumerable
          .Where(this.CheckedFunc);
      }
    }

    internal IEnumerable<ToolStripMenuItem>
    DisabledToolStripMenuItemEnumerable
    {
      get
      {
        return this.GetToolStripMenuItemEnumerable(this.DisabledIdEnumerable);
      }
    }

    internal IEnumerable<ToolStripMenuItem>
    EnabledToolStripMenuItemEnumerable
    {
      get
      {
        return this.GetToolStripMenuItemEnumerable(this.EnabledIdEnumerable);
      }
    }

    internal IEnumerable<ToolStripMenuItem>
    PresentToolStripMenuItemEnumerable
    {
      get
      {
        return this.GetToolStripMenuItemEnumerable(this.PresentIdEnumerable);
      }
    }

    internal IEnumerable<ToolStripMenuItem>
    StartedToolStripMenuItemEnumerable
    {
      get
      {
        return this.GetToolStripMenuItemEnumerable(this.StartedIdEnumerable);
      }
    }

    internal IEnumerable<ToolStripMenuItem>
    StoppedToolStripMenuItemEnumerable
    {
      get
      {
        return this.GetToolStripMenuItemEnumerable(this.StoppedIdEnumerable);
      }
    }

    internal IEnumerable<uint> AllIdEnumerable
    {
      get
      {
        return this.GroupService
          .SelectedRepository
          .GetAll()
          .Select(x => x.Id);
      }
    }

    internal IEnumerable<uint> AbsentIdEnumerable
    {
      get
      {
        return this.GroupService
          .DeviceGroupService
          .GetAllAbsent()
          .Select(x => x.Id);
      }
    }
    internal IEnumerable<uint> CheckedIdEnumerable
    {
      get
      {
        var idStringEnumerable = this.AllToolStripMenuItemEnumerable
          .Where(this.CheckedFunc)
          .Select(this.IdFunc);

        return uintExtension.TryParse(idStringEnumerable);
      }
    }

    internal IEnumerable<uint> DisabledIdEnumerable
    {
      get
      {
        return this.GroupService
          .DeviceGroupService
          .GetAllDisabled()
          .Select(x => x.Id);
      }
    }

    internal IEnumerable<uint> EnabledIdEnumerable
    {
      get
      {
        return this.GroupService
          .DeviceGroupService
          .GetAllEnabled()
          .Select(x => x.Id);
      }
    }
    internal IEnumerable<uint> PresentIdEnumerable
    {
      get
      {
        return this.GroupService
          .DeviceGroupService
          .GetAllPresent()
          .Select(x => x.Id);
      }
    }

    internal IEnumerable<uint> StartedIdEnumerable
    {
      get
      {
        return this.GroupService
          .GetAllStarted()
          .Select(x => x.Id);
      }
    }

    internal IEnumerable<uint> StoppedIdEnumerable
    {
      get
      {
        return this.GroupService
          .GetAllStarted()
          .Select(x => x.Id);
      }
    }

    #endregion

    #region Logic

    internal IEnumerable<ToolStripMenuItem> GetToolStripMenuItemEnumerable
    (IEnumerable<uint> propertyIdEnumerable)
    {
      if (propertyIdEnumerable == null)
      {
        yield break;
      }

      foreach (var item in propertyIdEnumerable)
      {
        yield return this.AllToolStripMenuItemEnumerable
            .Cast<ToolStripMenuItem>()
            .First(this.ContainsId(item));
      }
    }

    #endregion
  }
}