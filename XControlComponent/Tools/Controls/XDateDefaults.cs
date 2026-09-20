using System.Windows;
using System.Windows.Media;

namespace XControlComponents.Tools.Controls;

public static class XDateDefaults
{
    //Border Box 
    public static double Height { get; set; } = 27;
    public static double Width { get; set; } = 333;
    public static bool WidthAuto { get; set; } = false;
    public static bool BorderBrushFocused { get; set; } = true;
    public static bool RequiredVisibility { get; set; } = true;
    public static Brush BackgroundColor { get; set; } = XElementHelper.GetBrushColor(Colors.White);
    public static Brush RequiredColor { get; set; } = XAppMethods.Color_Red_de0030();
    public static Brush BorderBrushColor { get; set; } = XAppMethods.Color_Gray_959595();
    public static Brush BorderBrushColorFocused { get; set; } = XAppMethods.Color_Blue_4a78b0();
    public static Brush BorderBrushColorFloating { get; set; } = XAppMethods.Color_Blue_4a78b0();
    public static double FontSize { get; set; } = 13.9;

    //Label
    public static bool LabelTop { get; set; } = false;
    public static double LabelTopHeight { get; set; } = 27;
    public static double LabelGridWidth { get; set; } = 135;
    public static double LabelFontSize { get; set; } = 13.9;
    public static double LabelOpacity { get; set; } = 1;
    public static string LabelTitle { get; set; } = "عنوان تاریخ";
    public static Thickness LabelPadding { get; set; } = new Thickness(10, 0, 0, 0);
    public static Brush LabelColor { get; set; } = XAppMethods.Color_Blue_0D47A1();
    public static VerticalAlignment LabelVerticalAlignment { get; set; } = VerticalAlignment.Center;

    //Text
    public static double TextFontSize { get; set; } = 13.9;
    public static double TextOpacity { get; set; } = 1;
    public static Thickness TextPadding { get; set; } = new Thickness(5, 0, 5, 0);
    public static Brush TextColor { get; set; } = XAppMethods.Color_Black_424242();
    public static Brush TextCaretColor { get; set; } = XAppMethods.Color_Black_212121();


    //Place Holder
    public static double PlaceHolderFontSize { get; set; } = 13.9;
    public static double PlaceHolderOpacity { get; set; } = 0.7;
    public static string PlaceHolder { get; set; } = "وارد کنید";
}
