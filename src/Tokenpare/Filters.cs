namespace Tokenpare;

/// <summary>The filter steps topk, topp and minp, and the table of what a step keeps.</summary>
public static class Filters
{
    /// <summary>Ranks the items and keeps those the filter of the given kind keeps, in ranking order.</summary>
    public static List<Item> Apply(List<Item> items, string kind, int value)
    {
        var ranked = Rank.Ranking(items);
        switch (kind)
        {
            case "topk":
                return ranked.Take(value).ToList();
            case "topp":
                return KeepNucleus(ranked, value);
            case "minp":
                long heaviest = ranked.Count > 0 ? ranked[0].Weight : 0;
                return ranked.Where(i => (long)i.Weight * 1000 >= (long)value * heaviest).ToList();
            default:
                throw new ArgumentException("unknown filter kind: " + kind, nameof(kind));
        }
    }

    /// <summary>One row per kept item; a full bar stands for the heaviest weight of kept.</summary>
    public static List<Row> Table(List<Item> kept)
    {
        var full = kept.Count > 0 ? kept.Max(i => i.Weight) : 0;
        return kept.Select(i => new Row(i.Token, i.Weight, full)).ToList();
    }

    private static List<Item> KeepNucleus(List<Item> ranked, int value)
    {
        long total = 0;
        foreach (var item in ranked)
        {
            total += item.Weight;
        }
        var result = new List<Item>();
        long kept = 0;
        foreach (var item in ranked)
        {
            result.Add(item);
            kept += item.Weight;
            if (kept * 1000L >= (long)value * total)
            {
                break;
            }
        }
        return result;
    }
}
