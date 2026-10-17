using System.Windows;
using System.Windows.Media;

namespace XControlComponents.Tools.Controls;

public static class XGridViewDefaults
{
    //Border
    public static Brush BorderColor { get; set; } = XAppMethods.Color_Gray_B8B8B8();
    public static Brush InnerBorderColor { get; set; } = XAppMethods.Color_Gray_B8B8B8();

    //Grid
    public static Brush GridBackground { get; set; } = XAppMethods.Color_White_fafafa();
    public static Thickness GridMargin { get; set; } = new Thickness(10);

    //Grid Header
    public static double HeaderHeight { get; set; } = 32;
    public static Brush HeaderBackground { get; set; } = XAppMethods.Color_Gray_F0F0F0();
    public static Brush HeaderForeground { get; set; } = XAppMethods.Color_Black_424242();
    public static double HeaderFontSize { get; set; } = 14;
    public static FontWeight HeaderFontWeight { get; set; } = FontWeights.Normal;
    public static FontStyle HeaderFontStyle { get; set; } = FontStyles.Normal;

    //Grid Row
    public static double RowHeight { get; set; } = 32;
    public static Brush RowForeground { get; set; } = XAppMethods.Color_Black_424242();
    public static double RowFontSize { get; set; } = 14;
    public static FontWeight RowFontWeight { get; set; } = FontWeights.Normal;
    public static FontStyle RowFontStyle { get; set; } = FontStyles.Normal;

    //No Data
    public static Brush NoDataBackGround { get; set; } = XAppMethods.Color_Red_FFE6E6();
    public static Brush NoDataForeground { get; set; } = XAppMethods.Color_Black_444040();
    public static string NoDataText { get; set; } = "No Data Exist";
    public static double NoDataFontSize { get; set; } = 18;

    //Names
    internal static string N_GR_Column_Scroll { get; } = "GR_Column_Scroll";
}
