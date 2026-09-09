using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using XControlHelper;

namespace XControlComponents
{
    public static class XCheckBoxDefault
    {
        //Label
        public static double LabelGridWidth { get; set; } = 80;
        public static double LabelFontSize { get; set; } = 13.9;
        public static double LabelOpacity { get; set; } = 1;
        public static string LabelTitle { get; set; } = "عنوان لیبل";
        public static bool TriggerCheckedChanged { get; set; } = false;
        public static Brush LabelColor { get; set; } = XAppMethods.Color_Blue_0D47A1();
        public static Brush CheckBoxColor { get; set; } = XAppMethods.Color_Blue_0D47A1();
    }
}
