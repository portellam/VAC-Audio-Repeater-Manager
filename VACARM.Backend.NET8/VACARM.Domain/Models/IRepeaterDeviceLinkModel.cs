using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VACARM.Domain.Models
{
  public interface IRepeaterDeviceLinkModel
  {
    #region Parameters

    RepeaterModel RepeaterModel { get; set; }

    /// <summary>
    /// The input <typeparamref name="DeviceModel"/>.
    /// </summary>
    DeviceModel InputDeviceModel { get; set; }

    /// <summary>
    /// The output <typeparamref name="DeviceModel"/>.
    /// </summary>    
    DeviceModel OutputDeviceModel { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Deconstructor
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="inputDeviceModel">
    /// The input<typeparamref name="DeviceModel"/>
    /// </param>
    /// <param name="outputDeviceModel">
    /// The output <typeparamref name="DeviceModel"/>
    /// </param>
    /// <param name="repeaterModel">The repeater</param>
    void Deconstruct
    (
      out uint id,
      out DeviceModel inputDeviceModel,
      out DeviceModel outputDeviceModel,
      out RepeaterModel repeaterModel
    );

    #endregion
  }
}