using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VACARM;

namespace VACARM
{
	/// <summary>
	/// Interaction logic for RepeaterMenu.xaml
	/// </summary>
	public partial class RepeaterMenu : Window
    {
        private BipartiteDeviceGraph bipartiteDeviceGraph;

        private RepeaterInfo repeaterInfo;

        public RepeaterInfo RepeaterInfo
        {
            get
            {
                return repeaterInfo;
            }
            set
            {
                repeaterInfo = value;
                repeaterInfo.OnPropertyChanged("SamplingRate");
                repeaterInfo.OnPropertyChanged("BitsPerSample");
                repeaterInfo.OnPropertyChanged("ChannelConfig");
                repeaterInfo.OnPropertyChanged("ChannelMask");
                repeaterInfo.OnPropertyChanged("BufferMs");
                repeaterInfo.OnPropertyChanged("Buffers");
                repeaterInfo.OnPropertyChanged("Prefill");
                repeaterInfo.OnPropertyChanged("ResyncAt");
            }
        }

        /// <summary>
        /// Populates submenu with each repeater and its' information.
        /// </summary>
        /// <param name="repeaterInfo">The repeater info</param>
        /// <param name="bipartiteDeviceGraph">The graph</param>
        public RepeaterMenu(RepeaterInfo repeaterInfo, BipartiteDeviceGraph bipartiteDeviceGraph)
        {
            InitializeComponent();

            List<Channel> channelList = Enum.GetValues(typeof(Channel)).Cast<Channel>().ToList();

            for (int i = 0; i < channelList.Count; i++)
            {
                Channel channel = channelList[i];

                TextBlock textBlock = new TextBlock();
                textBlock.Text = channel.ToString();
                Grid.SetRow(textBlock, 0);
                Grid.SetColumn(textBlock, i);

                CheckBox checkBox = new CheckBox();
                checkBox.Tag = channel;
                Grid.SetRow(checkBox, 1);
                Grid.SetColumn(checkBox, i);
                Binding bindChannel = new Binding("ChannelMask");
                bindChannel.Converter = new ChannelConverter(repeaterInfo);
                bindChannel.ConverterParameter = (int)channel;
                bindChannel.Source = repeaterInfo;
                checkBox.SetBinding(CheckBox.IsCheckedProperty, bindChannel);

                channels.Children.Add(textBlock);
                channels.Children.Add(checkBox);
            }

            RepeaterInfo = repeaterInfo;
            DataContext = RepeaterInfo;
            this.bipartiteDeviceGraph = bipartiteDeviceGraph;
        }

        /// <summary>
        /// Closes window given button click.
        /// </summary>
        /// <param name="sender">The sender value</param>
        /// <param name="routedEventArgs">The routed event arguments</param>
        private void Okay_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }

        /// <summary>
        /// Removes edge given button click.
        /// </summary>
        /// <param name="sender">The sender value</param>
        /// <param name="routedEventArgs">The routed event arguments</param>
        private void deleteButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            bipartiteDeviceGraph.RemoveEdge(repeaterInfo.Capture, repeaterInfo.Render);
            Close();
        }
    }
}
[ValueConversion(typeof(int), typeof(bool))]
public class ChannelConverter : IValueConverter
{
    RepeaterInfo repeaterInfo;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repeaterInfo">The repeater info</param>
    public ChannelConverter(RepeaterInfo repeaterInfo)
    {
        this.repeaterInfo = repeaterInfo;
    }

    /// <summary>
    /// Convert channel mask to boolean.
    /// </summary>
    /// <param name="value">The boolean value</param>
    /// <param name="targetType">The target data type</param>
    /// <param name="parameter">The mask integer value</param>
    /// <param name="cultureInfo">The culture info</param>
    /// <returns>True/False</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
    {
        int bit = (int)parameter;
        int val = (int)value;
        return (val & bit) != 0;
    }

    /// <summary>
    /// Convert boolean to channel mask.
    /// </summary>
    /// <param name="value">The boolean value</param>
    /// <param name="targetType">The target data type</param>
    /// <param name="parameter">The mask integer value</param>
    /// <param name="cultureInfo">The culture info</param>
    /// <returns>The channel mask</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
    {
        int mask = repeaterInfo.ChannelMask;
        int bit = (int)parameter;
        bool check = (bool)value;

        if (check)
        {
            return mask | bit;
        }
        
        return mask & ~bit;
    }
}