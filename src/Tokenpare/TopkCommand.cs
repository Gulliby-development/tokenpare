namespace Tokenpare;

/// <summary>The topk command: the k heaviest candidates.</summary>
public static class TopkCommand
{
    public static Reply Run(Options options, List<Item> items) =>
        new(0, Report.RenderRows(Filters.Table(Filters.Apply(items, "topk", options.K!.Value)), options.Format), "");
}
