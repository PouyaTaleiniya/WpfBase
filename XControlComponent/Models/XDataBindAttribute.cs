using System.Windows;

namespace XControlComponents.Models;

[AttributeUsage(AttributeTargets.Property)]
public class XDataBindAttribute : Attribute
{
    public string DisplayName { get; set; }
    public int Order { get; set; }
    public bool Visible { get; set; } = true;
    public double Width { get; set; }
}
