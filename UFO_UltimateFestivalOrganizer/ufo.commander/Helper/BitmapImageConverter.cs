using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ufo.commander.Helper
{
    class BitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return new BitmapImage(new Uri(value.ToString(), UriKind.Absolute));
            }
            catch
            {
                return new BitmapImage(new Uri("../Resource/images/userplacehold.png", UriKind.Relative));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
