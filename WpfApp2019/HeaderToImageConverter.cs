using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfApp2019.View
{
    // Converts a full path to a specific image type of a drive, folder or file
    [ValueConversion(typeof(TreeView.TreeViewItemType), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Trace.WriteLine("VALUE: " + value);
            return new BitmapImage(new Uri(@"pack://application:,,,/Images/" + value + ".png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
