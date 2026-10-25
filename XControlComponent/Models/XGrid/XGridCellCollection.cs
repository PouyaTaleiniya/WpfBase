namespace XControlComponents.Models.XGrid;

public class XGridCellCollection : List<XGridCell>
{
    public XGridCell this[string columnName]
    {
        get
        {
            var cell = this.FirstOrDefault(x =>
                string.Equals(
                    x.Column.FieldName,
                    columnName,
                    StringComparison.OrdinalIgnoreCase));

            if (cell == null)
                throw new KeyNotFoundException(
                    $"Cell for column '{columnName}' was not found.");

            return cell;
        }
    }
}