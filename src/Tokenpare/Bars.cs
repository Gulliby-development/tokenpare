namespace Tokenpare;

/// <summary>The hash marks of a value against a full bar.</summary>
public static class Bars
{
    /// <summary>value * 20 / full rounded down, at least one mark for a value above 0 and at most 20; no mark for 0.</summary>
    public static string Bar(int value, int full)
    {
        if (value <= 0 || full <= 0)
        {
            return "";
        }
        return new string('#', (int)Math.Min(20L, Math.Max(1L, (long)value * 20 / full)));
    }
}
