using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

                foreach (string propertyName in RepeaterInfo.repeaterInfoPropertyList)
                {
                    repeaterInfo.OnPropertyChanged(propertyName);
                }
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
        /// Removes edge given button click.
        /// </summary>
        /// <param name="sender">The sender value</param>
        /// <param name="routedEventArgs">The routed event</param>
        private void deleteButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            bipartiteDeviceGraph.RemoveEdge(repeaterInfo.Capture, repeaterInfo.Render);
            Close();
        }

        /// <summary>
        /// Closes window given button click.
        /// </summary>
        /// <param name="sender">The sender value</param>
        /// <param name="routedEventArgs">The routed event</param>
        private void okay_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }
    }
}