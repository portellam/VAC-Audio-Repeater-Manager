using System.Collections.Generic;
using System.Threading.Tasks;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Functions;

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

    public async Task<IEnumerable<int?>> RestartAll()
    {
      return await this.SelectedService
        .DoActionAll(this.RestartAsync);
    }

    public async Task<IEnumerable<int?>> RestartRange
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

      return await this.SelectedService
        .DoActionRange
        (
          this.RestartAsync,
          func
        );
    }

    public async Task<IEnumerable<int?>> RestartRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return await this.SelectedService
        .DoActionRange
        (
          this.RestartAsync,
          func
        );
    }

    public async Task<IEnumerable<int?>> StartAll()
    {
      return await this.SelectedService
        .DoActionAll(this.StartAsync);
    }

    public async Task<IEnumerable<int?>> StartRange
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

      return await this.SelectedService
        .DoActionRange
        (
          this.StartAsync,
          func
        );
    }

    public async Task<IEnumerable<int?>> StartRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return await this.SelectedService
        .DoActionRange
        (
          this.StartAsync,
          func
        );
    }

    public async Task<IEnumerable<int?>> StopAll()
    {
      return await this.SelectedService
        .DoActionAll(this.StopAsync);
    }

    public async Task<IEnumerable<int?>> StopRange
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

      return await this.SelectedService
        .DoActionRange
        (
          this.StopAsync,
          func
        );
    }

    public async Task<IEnumerable<int?>> StopRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return await this.SelectedService
        .DoActionRange
        (
          this.StopAsync,
          func
        );
    }

    #endregion
  }
}