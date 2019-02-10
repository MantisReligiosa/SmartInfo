using System;
using System.Text.RegularExpressions;

namespace Display_control
{
    public static class ColorConverter
    {
        public static bool TryToParseRGB(string colorString, out string colorHexString)
        {
            if (Regex.IsMatch(colorString, @"\#\w{6}"))
            {
                colorHexString = colorString;
                return true;
            }
            var regular = @"^rgb\(\s?(\d{1,3})\,\s?(\d{1,3})\,\s?(\d{1,3})\)$";
            colorHexString = string.Empty;
            if (Regex.IsMatch(colorString, regular))
            {
                var t = Regex.Matches(colorString, regular);
                var group = t[0].Groups;
                var red = Convert.ToByte(group[1].Value);
                var green = Convert.ToByte(group[2].Value);
                var blue = Convert.ToByte(group[3].Value);
                colorHexString = $"#{red:x2}{green:x2}{blue:x2}";
                return true;
            }
            else
            {
                regular = @"^rgba\(\s?(\d{1,3})\,\s?(\d{1,3})\,\s?(\d{1,3})\,\s?(0\.\d{1,2})\)$";
                colorHexString = string.Empty;
                if (Regex.IsMatch(colorString, regular))
                {
                    var t = Regex.Matches(colorString, regular);
                    var group = t[0].Groups;
                    var red = Convert.ToByte(group[1].Value);
                    var green = Convert.ToByte(group[2].Value);
                    var blue = Convert.ToByte(group[3].Value);
                    var opacity = Convert.ToDouble(group[4].Value.Replace(".", ","));
                    var opacityByte = Convert.ToByte(Math.Truncate(opacity * 0xff));
                    colorHexString = $"#{opacityByte:x2}{red:x2}{green:x2}{blue:x2}";
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
