namespace XControlComponents.Models.XGrid;

public class XGridRow
{
    public XGridCellCollection Cells { get; } = new();

    public List<XGridCell> VisibleCells => Cells.Where(x => x.Column.Visible).ToList();
}
