using System.Reflection;

namespace XControlComponents.Models.XGrid;

public class XGridColumn
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public bool Visible { get; set; }

    public int Order { get; set; }

    public double Width { get; set; }

    public PropertyInfo Property { get; init; }

    public XDataBindAttribute Attribute { get; init; }
}
