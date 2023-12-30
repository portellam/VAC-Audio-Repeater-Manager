using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace VACARM_GUI_NET_4
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
            if (repeaterInfo is null)
            {
                throw new ArgumentNullException(nameof(repeaterInfo));
            }

            if (bipartiteDeviceGraph is null)
            {
                throw new ArgumentNullException(nameof(bipartiteDeviceGraph));
            }

            //InitializeComponent();    //TODO: remove if "LoadViewFromUri" works as intended and is unit-testable.

            string namespaceString = typeof(RepeaterMenu).Namespace;
            string xamlName = $"{typeof(RepeaterMenu).Name}.xaml";
            string uri = $"/{namespaceString};component/{xamlName}";
            Extension.LoadViewFromUri(this, uri);

            List<Channel> channelList = Enum.GetValues(typeof(Channel)).Cast<Channel>().ToList();
            const string channelMaskString = "ChannelMask";

            for (int i = 0; i < channelList.Count; i++)
            {
                Channel channel = channelList[i];

                TextBlock textBlock = new TextBlock()
                {
                    Text = channel.ToString()
                };

                Grid.SetRow(textBlock, 0);
                Grid.SetColumn(textBlock, i);

                CheckBox checkBox = new CheckBox()
                {
                    Tag = channel
                };

                Grid.SetRow(checkBox, 1);
                Grid.SetColumn(checkBox, i);

                Binding bindChannel = new Binding(channelMaskString)
                {
                    Converter = new ChannelConverter(repeaterInfo),
                    ConverterParameter = (int)channel,
                    Source = repeaterInfo
                };

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
        protected internal virtual void DeleteButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            bipartiteDeviceGraph.RemoveEdge(repeaterInfo.CaptureDeviceControl, repeaterInfo.RenderDeviceControl);
            Close();
        }

        /// <summary>
        /// Closes window given button click.
        /// </summary>
        /// <param name="sender">The sender value</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void Okay_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }
    }
}