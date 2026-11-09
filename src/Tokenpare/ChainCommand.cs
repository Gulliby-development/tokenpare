using System.Globalization;

namespace Tokenpare;

/// <summary>The chain command: the steps one after the other, each on the candidates the step before kept.</summary>
public static class ChainCommand
{
    public static Reply Run(Options options, List<Item> items)
    {
        var kept = items;
        foreach (var step in options.Steps!.Split(','))
        {
            var parts = step.Split(':');
            kept = Filters.Apply(kept, parts[0], int.Parse(parts[1], CultureInfo.InvariantCulture));
        }
        return new Reply(0, Report.RenderRows(Filters.Table(kept), options.Format), "");
    }
}
