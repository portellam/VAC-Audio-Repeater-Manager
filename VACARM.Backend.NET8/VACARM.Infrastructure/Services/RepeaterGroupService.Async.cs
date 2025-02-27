using VACARM.Application.Commands;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public partial class RepeaterGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
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

    public async Task<bool> UpdateServiceAsync()
    {
      if (this.DeviceGroupService == null)
      {
        this.DeviceGroupService =
          new DeviceGroupService
          <
            ReadonlyRepository
            <
              BaseService
              <
                BaseRepository<DeviceModel>,
                DeviceModel
              >
            >,
            BaseService
            <
              BaseRepository<DeviceModel>,
              DeviceModel
            >,
            BaseRepository<DeviceModel>,
            DeviceModel
          >();

        return true;
      }

      return await this.DeviceGroupService
        .UpdateServiceAsync()
        .ConfigureAwait(false);
    }

    public async Task<int?> RestartAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);

      return await this.SelectedService
        .DoWorkAsync
        (
          this.RestartAsync,
          func
        ).ConfigureAwait(false);
    }

    public IAsyncEnumerable<int?> RestartAllAsync()
    {
      return this.SelectedService
        .DoWorkAllAsync(this.RestartAsync);
    }

    public IAsyncEnumerable<int?> RestartRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdRange
        (
          startId,
          endId
        );

      return this.SelectedService
        .DoWorkRangeAsync
        (
          this.RestartAsync,
          func
        );
    }

    public IAsyncEnumerable<int?> RestartRangeAsync(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return this.SelectedService
        .DoWorkRangeAsync
        (
          this.RestartAsync,
          func
        );
    }

    public async Task<int?> StartAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);

      return await this.SelectedService
        .DoWorkAsync
        (
          this.StartAsync,
          func
        ).ConfigureAwait(false);
    }

    public IAsyncEnumerable<int?> StartAllAsync()
    {
      return this.SelectedService
        .DoWorkAllAsync(this.StartAsync);
    }

    public IAsyncEnumerable<int?> StartRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdRange
        (
          startId,
          endId
        );

      return this.SelectedService
        .DoWorkRangeAsync
        (
          this.StartAsync,
          func
        );
    }

    public IAsyncEnumerable<int?> StartRangeAsync(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return this.SelectedService
        .DoWorkRangeAsync
        (
          this.StartAsync,
          func
        );
    }

    public async Task<int?> StopAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);

      return await this.SelectedService
        .DoWorkAsync
        (
          this.StopAsync,
          func
        ).ConfigureAwait(false);
    }

    public IAsyncEnumerable<int?> StopAllAsync()
    {
      return this.SelectedService
        .DoWorkAllAsync(this.StopAsync);
    }

    public IAsyncEnumerable<int?> StopRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdRange
        (
          startId,
          endId
        );

      return this.SelectedService
        .DoWorkRangeAsync
        (
          this.StopAsync,
          func
        );
    }

    public IAsyncEnumerable<int?> StopRangeAsync(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return this.SelectedService
        .DoWorkRangeAsync
        (
          this.StopAsync,
          func
        );
    }

    #endregion
  }
}