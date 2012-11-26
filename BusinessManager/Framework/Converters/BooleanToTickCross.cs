using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BusinessManager.Framework.Converters
{
	[ValueConversion(typeof(Boolean?), typeof(string))]
	public class BooleanToTickCross : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Boolean? val = (Boolean?)value;
			if (val == null)
			{
				return null;
			}
			else if (val == true)
			{
				return "/BusinessManager;component/Resources/Accept.png";
			}
			
			return "/BusinessManager;component/Resources/Delete.png";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
