namespace XControlComponents.Models.XGrid;

public class XGridData
{
    public List<XGridColumn> Columns { get; } = new();

    internal List<XGridColumn> VisibleColumns => Columns.Where(x => x.Visible).ToList();

    public List<XGridRow> Rows { get; } = new();
}
