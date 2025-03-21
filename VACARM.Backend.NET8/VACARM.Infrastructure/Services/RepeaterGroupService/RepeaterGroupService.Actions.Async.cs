using System.Collections.Generic;
using System.Threading.Tasks;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Functions;

namespace VACARM.Infrastructure.Services
{
  public partial class RepeaterGroupService
    <
      TBaseService,
      TRepeaterModel
    >
  {
    #region Logic

    /// <summary>
    /// Restart a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="model">The item</param>
    private async Task<int?> RestartAsync(TRepeaterModel model)
    {
      return await ExecutableCommands.RestartAsync
        (
          model.ProcessId,
          this.ExecutableFullPathName,
          model.StartArguments,
          model.StopArguments
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// Start a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="model">The item</param>
    private async Task<int?> StartAsync(TRepeaterModel model)
    {
      return await ExecutableCommands.StartAsync
        (
          model.ProcessId,
          this.ExecutableFullPathName,
          model.StartArguments
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// Stop a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="model">The item</param>
    private async Task<int?> StopAsync(TRepeaterModel model)
    {
      return await ExecutableCommands.StopAsync
        (
          model.ProcessId,
          model.StopArguments
        ).ConfigureAwait(false);
    }

    public async Task<int?> RestartAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);

      return await this.SelectedService
        .DoActionAsync
        (
          this.RestartAsync,
          func
        ).ConfigureAwait(false);
    }

    public async Task<int?> StartAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);

      return await this.SelectedService
        .DoActionAsync
        (
          this.StartAsync,
          func
        ).ConfigureAwait(false);
    }

    public async Task<int?> StopAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);

      return await this.SelectedService
        .DoActionAsync
        (
          this.StopAsync,
          func
        ).ConfigureAwait(false);
    }

    #endregion
  }
}