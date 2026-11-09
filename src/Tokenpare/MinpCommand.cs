namespace Tokenpare;

/// <summary>The minp command: keeps the candidates that weigh at least a ratio of the heaviest one.</summary>
public static class MinpCommand
{
    public static Reply Run(Options options, List<Item> items) =>
        new(0, Report.RenderRows(Filters.Table(Filters.Apply(items, "minp", options.Ratio!.Value)), options.Format), "");
}
