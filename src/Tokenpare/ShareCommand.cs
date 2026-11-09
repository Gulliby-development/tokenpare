namespace Tokenpare;

/// <summary>The share command: each weight as a share of the total in a unit.</summary>
public static class ShareCommand
{
    public static Reply Run(Options options, List<Item> items)
    {
        var scale = Registry.Units[options.Unit];
        long total = 0;
        foreach (var item in items)
        {
            total += item.Weight;
        }
        var rows = new List<Row>();
        foreach (var item in items)
        {
            var number = (int)(((long)item.Weight * scale + total / 2) / total);
            rows.Add(new Row(item.Token, number, scale));
        }
        return new Reply(0, Report.RenderRows(rows, options.Format), "");
    }
}
