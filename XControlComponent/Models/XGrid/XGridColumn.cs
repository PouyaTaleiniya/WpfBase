using System.Reflection;

namespace XControlComponents.Models.XGrid;

public class XGridColumn
{
    internal string FieldName { get; set; }

    public string DisplayName { get; set; }

    public bool Visible { get; set; }

    public int Order { get; set; }

    public double Width { get; set; }

    internal PropertyInfo Property { get; init; }

    internal XDataBindAttribute Attribute { get; init; }
}
