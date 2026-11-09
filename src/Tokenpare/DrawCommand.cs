using System.Globalization;

namespace Tokenpare;

/// <summary>The draw command: names the candidate that a position in the cumulative weights falls on.</summary>
public static class DrawCommand
{
    public static Reply Run(Options options, List<Item> items)
    {
        long at = options.At!.Value;
        long running = 0;
        foreach (var item in items)
        {
            running += item.Weight;
            if (running > at)
            {
                return new Reply(0, item.Token + "\n", "");
            }
        }
        throw new Failure(1, "draw position " + at.ToString(CultureInfo.InvariantCulture) + " is outside the total " + running.ToString(CultureInfo.InvariantCulture));
    }
}
