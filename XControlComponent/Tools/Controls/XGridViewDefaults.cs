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
    public static Brush HeaderTxtForeground { get; set; } = XAppMethods.Color_Black_424242();
    public static double HeaderTxtFontSize { get; set; } = 14;
    public static FontWeight HeaderTxtFontWeight { get; set; } = FontWeights.Normal;
    public static FontStyle HeaderTxtFontStyle { get; set; } = FontStyles.Normal;
    public static bool HeaderTxtSelectable { get; set; } = true;
    public static Brush HeaderTxtSelectedBrush { get; set; } = XAppMethods.Color_SelectedBrush_Blue_FF0078D7();
    public static double HeaderTxtSelectionOpacity { get; set; } = 0.4;

    //Grid Row
    public static double RowHeight { get; set; } = 32;
    public static Brush RowSelectedBackground { get; set; } = XAppMethods.Color_Blue_CDE8FF();
    public static bool RowSelectable { get; set; } = false;
    public static bool RowMultiSelectable { get; set; } = false;
    public static Brush RowTxtForeground { get; set; } = XAppMethods.Color_Black_424242();
    public static double RowTxtFontSize { get; set; } = 14;
    public static FontWeight RowTxtFontWeight { get; set; } = FontWeights.Normal;
    public static FontStyle RowTxtFontStyle { get; set; } = FontStyles.Normal;
    public static bool RowTxtSelectable { get; set; } = true;
    public static Brush RowTxtSelectedBrush { get; set; } = XAppMethods.Color_SelectedBrush_Blue_FF0078D7();
    public static double RowTxtSelectionOpacity { get; set; } = 0.4;

    //No Data
    public static Brush NoDataBackGround { get; set; } = XAppMethods.Color_Red_FFE6E6();
    public static Brush NoDataForeground { get; set; } = XAppMethods.Color_Black_444040();
    public static string NoDataText { get; set; } = "No Data Exist";
    public static double NoDataFontSize { get; set; } = 18;

    //Names
    internal static string N_GR_Column_Scroll { get; } = "GR_Column_Scroll";
}
