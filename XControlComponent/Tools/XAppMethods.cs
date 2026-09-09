using System.Windows;
using System.Windows.Media;

namespace XControlHelper
{
    public static class XAppMethods
    {
        public static Brush Color_White_f3f3f3()
        {
            return Application.Current.TryFindResource("Color_White_f3f3f3") as Brush;
        }

        public static Brush Color_Blue_cbdeec()
        {
            return Application.Current.TryFindResource("Color_Blue_cbdeec") as Brush;
        }

        public static Brush Color_Red_c42b1c()
        {
            return Application.Current.TryFindResource("Color_Red_c42b1c") as Brush;
        }

        public static Brush Color_Red_de0030()
        {
            return Application.Current.TryFindResource("Color_Red_de0030") as Brush;
        }
        
        public static Brush Color_White_f8f9ff()
        {
            return Application.Current.TryFindResource("Color_White_f8f9ff") as Brush;
        }

        public static Brush Color_Gray_959595()
        {
            return Application.Current.TryFindResource("Color_Gray_959595") as Brush;
        }

        public static Brush Color_Gray_d9d9d9()
        {
            return Application.Current.TryFindResource("Color_Gray_d9d9d9") as Brush;
        }
        
        public static Brush Color_Blue_4a78b0()
        {
            return Application.Current.TryFindResource("Color_Blue_4a78b0") as Brush;
        }

        public static Brush Color_Blue_2196f3()
        {
            return Application.Current.TryFindResource("Color_Blue_2196f3") as Brush;
        }

        public static Brush Color_Black_424242()
        {
            return Application.Current.TryFindResource("Color_Black_424242") as Brush;
        }

        public static Brush Color_Blue_0D47A1()
        {
            return Application.Current.TryFindResource("Color_Blue_0D47A1") as Brush;
        }

        public static Brush Color_Black_212121()
        {
            return Application.Current.TryFindResource("Color_Black_212121") as Brush;
        }

        public static Style BorderWindow()
        {
            return Application.Current.TryFindResource("BorderWindow") as Style;
        }

        public static ImageSource Img_Close()
        {
            return Application.Current.TryFindResource("Img_Close") as ImageSource;
        }

        public static ImageSource Img_CloseWhite()
        {
            return Application.Current.TryFindResource("Img_CloseWhite") as ImageSource;
        }
    }

}

