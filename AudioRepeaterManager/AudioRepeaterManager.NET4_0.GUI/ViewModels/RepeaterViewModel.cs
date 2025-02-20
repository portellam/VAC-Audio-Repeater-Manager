using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using AudioRepeaterManager.NET4_0.GUI.Extensions;
using AudioRepeaterManager.NET4_0.Backend.Models;
using static System.Windows.Forms.LinkLabel;

namespace AudioRepeaterManager.NET4_0.Backend.ViewModels
{
  public class RepeaterViewModel
  {
    public RepeaterModel RepeaterModel { get; private set; }

    /// <summary>
    /// The UI element representing a relationship between the Input and Output device.
    /// </summary>
    public Line Line { get; }

    public RepeaterViewModel()
    {
      // FIXME
      DeviceModel inputDeviceModel = new DeviceModel();
      DeviceModel outputDeviceModel = new DeviceModel();

      RepeaterModel = new RepeaterModel(inputDeviceModel, outputDeviceModel);

      Line = new Line
      {
        Stroke = new SolidColorBrush(ColorExtension.ToMediaColor
              (FormColorUpdater.ForeColor)),

        StrokeThickness = 2
      };

    }
  }
}
