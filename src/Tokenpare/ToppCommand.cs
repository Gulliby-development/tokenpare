namespace Tokenpare;

/// <summary>The topp command: the smallest group of heaviest candidates reaching a share of the total.</summary>
public static class ToppCommand
{
    public static Reply Run(Options options, List<Item> items) =>
        new(0, Report.RenderRows(Filters.Table(Filters.Apply(items, "topp", options.P!.Value)), options.Format), "");
}
