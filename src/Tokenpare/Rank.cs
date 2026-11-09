namespace Tokenpare;

/// <summary>The ranking of candidates: heaviest first, equal weights in input order.</summary>
public static class Rank
{
    /// <summary>Returns a new list sorted by weight, heaviest first; the sort is stable, so equal weights keep their input order.</summary>
    public static List<Item> Ranking(List<Item> items) => items.OrderByDescending(i => i.Weight).ToList();
}
