using System;
using System.Globalization;
using System.Windows.Data;

namespace VACARM_GUI_NET_4
{
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
}