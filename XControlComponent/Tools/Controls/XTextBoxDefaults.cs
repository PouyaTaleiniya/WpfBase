using System.Windows;
using System.Windows.Media;
using XControlComponents.Tools.ControlTypes;

namespace XControlComponents.Tools.Controls;

public static class XTextBoxDefaults
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
    public static XElementTypes ElementType { get; set; } = XElementTypes.Normal;

    //Label
    public static bool LabelTop { get; set; } = false;
    public static double LabelTopHeight { get; set; } = 27;
    public static double LabelGridWidth { get; set; } = 135;
    public static double LabelFontSize { get; set; } = 13.9;
    public static double LabelOpacity { get; set; } = 1;
    public static string LabelTitle { get; set; } = "عنوان لیبل";
    public static Thickness LabelPadding { get; set; } = new Thickness(10, 0, 0, 0);
    public static Brush LabelColor { get; set; } = XAppMethods.Color_Blue_0D47A1();
    public static VerticalAlignment LabelVerticalAlignment { get; set; } = VerticalAlignment.Center;

    //TextBox
    public static double TextBoxFontSize { get; set; } = 13.9;
    public static double TextBoxOpacity { get; set; } = 1;
    public static Thickness TextBoxPadding { get; set; } = new Thickness(5, 0, 5, 0);
    public static Thickness TextBoxBorderPadding { get; set; } = new Thickness(0, 0, 5, 0);
    public static Thickness TextBoxFloatingPadding { get; set; } = new Thickness(0);
    public static Brush TextBoxColor { get; set; } = XAppMethods.Color_Black_424242();
    public static Brush TextBoxCaretColor { get; set; } = XAppMethods.Color_Black_212121();
    public static Brush TextBoxSelectionColor { get; set; } = XAppMethods.Color_Gray_959595();

    //Multi Line
    public static bool MultiLine { get; set; } = false;
    public static double MultiLineHeight { get; set; } = 100;
    public static int MultiLineRowCount { get; set; } = 0;

    //MaxLength
    public static double MaxLengthGridWidth { get; set; } = 50;
    public static double MaxLengthOpacity { get; set; } = 1;
    public static bool MaxLengthBorder { get; set; } = true;
    public static bool MaxLengthOutbox { get; set; } = false;
    public static Brush MaxLengthColor { get; set; } = XElementHelper.GetBrushColor(Colors.Gray);
    public static Thickness MaxLengthMargin { get; set; } = new Thickness(0, 5, 5, 0);
    public static Thickness MaxLengthFloatingMargin { get; set; } = new Thickness(0, 3, 5, 0);

    //Place Holder
    public static double PlaceHolderFontSize { get; set; } = 13.9;
    public static double PlaceHolderOpacity { get; set; } = 0.7;
    public static string PlaceHolder { get; set; } = "وارد کنید";

    //Value
    public static XInputTypes InputType { get; set; } = XInputTypes.Text;
    public static XDecimalPoints DecimalPoint { get; set; } = XDecimalPoints.Two;
    public static bool AllowPaste { get; set; } = true;
    public static bool TriggerValueChanged { get; set; } = false;
    public static bool AllowImojiPaste { get; set; } = false;
    public static bool IsNumberGenerate { get; set; } = true;
}
