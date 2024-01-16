using Rezeptify.AppComponents.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify
{
    public class ShowMengeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var ingr = (Ingredients)value;
            if(ingr == null) return "-";
            if(String.IsNullOrEmpty(ingr.Unit)) return ingr.Quantity;
            return $"{ingr.Quantity} {ingr.Unit}";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
