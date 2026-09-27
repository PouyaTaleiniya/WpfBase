namespace XControlComponents.Models.XGrid;

public class XGridColumnCollection : List<XGridColumn>
{
    public XGridColumn this[string name]
    {
        get
        {
            var column = this.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));

            if (column == null)
                throw new KeyNotFoundException($"Column '{name}' was not found.");

            return column;
        }
    }
}
