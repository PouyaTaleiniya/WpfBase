using System.Windows.Media;
using System.Windows;
using XControlComponents.Tools;
using XControlComponents.Tools.ControlTypes;

namespace XControlHelper
{
    public static class XComboBoxDefaults
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
        public static Brush BorderBrushColorFloating { get; set; } = XAppMethods.Color_Blue_4a78b0();
        public static Brush BorderBrushColorFocused { get; set; } = XAppMethods.Color_Blue_4a78b0();
        public static double FontSize { get; set; } = 13.9;
        public static bool IsGenerateEmptyLabel { get; set; } = true;
        public static XElementTypes ElementType { get; set; } = XElementTypes.Normal;

        //Popup
        public static Brush PopupBackgroundColor { get; set; } = XAppMethods.Color_White_f3f3f3();
        public static Brush PopupBackgroundColorFocused { get; set; } = XAppMethods.Color_Gray_d9d9d9();
        public static Brush PopupBorderBrushColor { get; set; } = XAppMethods.Color_Gray_959595();
        public static Brush PopupTextColor { get; set; } = XAppMethods.Color_Black_424242();
        public static double PopupTextFontSize { get; set; } = 13.9;

        //Label 
        public static bool LabelTop { get; set; } = false;
        public static double LabelTopHeight { get; set; } = 27;
        public static double LabelGridWidth { get; set; } = 135;
        public static double LabelFontSize { get; set; } = 13.9;
        public static double LabelOpacity { get; set; } = 1;
        public static string LabelTitle { get; set; } = "عنوان لیبل";
        public static Thickness LabelPadding { get; set; } = new Thickness(10, 0, 0, 0);
        public static Brush LabelColor { get; set; } = XAppMethods.Color_Blue_0D47A1();

        //Text
        public static double TextFontSize { get; set; } = 13.9;
        public static double TextOpacity { get; set; } = 1;
        public static Thickness TextPadding { get; set; } = new Thickness(5, 0, 5, 0);
        public static Thickness TextBorderPadding { get; set; } = new Thickness(0, 0, 5, 0);
        public static Thickness TextFloatingPadding { get; set; } = new Thickness(0);
        public static Brush TextColor { get; set; } = XAppMethods.Color_Black_424242();

        //Place Holder
        public static double PlaceHolderFontSize { get; set; } = 13.9;
        public static double PlaceHolderOpacity { get; set; } = 0.7;
        public static string PlaceHolder { get; set; } = "انتخاب کنید";

        //Search
        public static double TextSearchFontSize { get; set; } = 13.9;
        public static double TextSearchOpacity { get; set; } = 1;
        public static Thickness TextSearchPadding { get; set; } = new Thickness(5, 0, 5, 0);
        public static Brush TextSearchColor { get; set; } = XAppMethods.Color_Black_424242();
        public static Brush BorderBrushSearchColor { get; set; } = XAppMethods.Color_Gray_959595();
        public static Brush BorderBrushSearchColorFocused { get; set; } = XAppMethods.Color_Blue_4a78b0();
        public static bool BorderBrushSearchFocused { get; set; } = true;
        public static bool IsEnableSearch { get; set; } = true;
        public static bool EqualSearchMode { get; set; } = false;
        public static bool CaseSensitiveSearchMode { get; set; } = false;
        public static double FontSearchSize { get; set; } = 13.9;
        public static double PlaceHolderSearchFontSize { get; set; } = 13.9;
        public static double PlaceHolderSearchOpacity { get; set; } = 0.7;
        public static string PlaceHolderSearchText { get; set; } = "جستجو";
         
        //Value
        public static bool TriggerValueChanged { get; set; } = false;

    }
}
