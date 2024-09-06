﻿// <copyright file="ConversionSettingsToString.cs" company="AAllard">License: http://www.gnu.org/licenses/gpl.html GPL version 3.</copyright>

namespace FileConverter.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ConversionSettingsToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (!(value is IConversionSettings))
            {
                throw new ArgumentException("The value must be a conversion preset array.");
            }

            IConversionSettings settings = (IConversionSettings)value;

            string parameterString = parameter as string;
            if (parameterString == null)
            {
                throw new ArgumentException("The parameter must be a string value.");
            }

            string[] parameters = parameterString.Split(',');
            if (parameters.Length < 1 || parameters.Length > 2)
            {
                throw new ArgumentException("The parameter format must be 'SettingsKey[,DefaultValue]'.");
            }

            string key = parameters[0];
            string setting;
            if (settings.TryGetValue(key, out setting))
            {
                return setting;
            }

            if (parameters.Length >= 2)
            {
                return parameters[1];
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (!(value is string))
            {
                throw new ArgumentException("value");
            }

            string settingsValue = (string)value;

            if (!(parameter is string))
            {
                throw new ArgumentException("parameter");
            }

            string settingsKey = (string)parameter;

            return new ConversionSettingsOverride(settingsKey, settingsValue);
        }
    }
}