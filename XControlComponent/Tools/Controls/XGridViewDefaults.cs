using System.Windows.Media;

namespace XControlComponents.Tools.Controls;

public static class XGridViewDefaults
{
    //Border
    public static Brush BorderColor { get; set; } = XAppMethods.Color_Gray_B8B8B8();
    public static Brush InnerBorderColor { get; set; } = XAppMethods.Color_Gray_B8B8B8();

    //Grid
    public static double RowHeaderHeight { get; set; } = 32;


    //No Data
    public static Brush NoDataBackGround { get; set; } = XAppMethods.Color_Red_FFE6E6();
    public static Brush NoDataForeground { get; set; } = XAppMethods.Color_Black_444040();
    public static string NoDataText { get; set; } = "No Data Exist";
    public static double NoDataFontSize { get; set; } = 18;
}
