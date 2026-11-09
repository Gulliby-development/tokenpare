namespace Tokenpare;

/// <summary>The check command: validates the list and reports the count and the total.</summary>
public static class CheckCommand
{
    public static Reply Run(Options options, List<Item> items)
    {
        var total = 0;
        foreach (var item in items)
        {
            total += item.Weight;
        }
        var text = "ok, " + Report.Plural(items.Count, "candidate") + ", total " + total.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\n";
        return new Reply(0, text, "");
    }
}
