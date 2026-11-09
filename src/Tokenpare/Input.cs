namespace Tokenpare;

/// <summary>Reads the list of candidates from the input text.</summary>
public static class Input
{
    private static readonly char[] Separators = [' ', '\t'];

    public static List<Item> Read(string text)
    {
        var lines = new List<string>();
        if (text.Length > 0)
        {
            lines.AddRange(text.Split('\n'));
            if (lines[^1].Length == 0)
            {
                lines.RemoveAt(lines.Count - 1);
            }
        }
        if (lines.Count > Registry.MaxLines)
        {
            throw new Failure(1, "input is too long (max " + Registry.MaxLines.ToString(System.Globalization.CultureInfo.InvariantCulture) + " lines)");
        }

        var items = new List<Item>();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        var total = 0;
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (line.EndsWith('\r'))
            {
                line = line[..^1];
            }
            var words = line.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0 || words[0].StartsWith('#'))
            {
                continue;
            }
            var prefix = "line " + (i + 1).ToString(System.Globalization.CultureInfo.InvariantCulture) + ": ";
            if (words.Length != 2)
            {
                throw new Failure(1, prefix + "expected TOKEN WEIGHT");
            }
            if (!IsToken(words[0]))
            {
                throw new Failure(1, prefix + "bad token " + words[0]);
            }
            if (!IsWeight(words[1]))
            {
                throw new Failure(1, prefix + "bad weight " + words[1]);
            }
            if (!seen.Add(words[0]))
            {
                throw new Failure(1, prefix + "duplicate token " + words[0]);
            }
            var weight = int.Parse(words[1], System.Globalization.CultureInfo.InvariantCulture);
            total += weight;
            items.Add(new Item(words[0], weight));
        }

        if (items.Count == 0)
        {
            throw new Failure(1, "no candidates");
        }
        if (total == 0)
        {
            throw new Failure(1, "total weight is zero");
        }
        return items;
    }

    private static bool IsToken(string word)
    {
        if (word.Length < 1 || word.Length > 12)
        {
            return false;
        }
        foreach (var c in word)
        {
            if (!(char.IsAsciiLetterOrDigit(c) || c == '_' || c == '.' || c == '-'))
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsWeight(string word)
    {
        if (word == "0")
        {
            return true;
        }
        if (word.Length < 1 || word.Length > 6 || word[0] == '0')
        {
            return false;
        }
        foreach (var c in word)
        {
            if (!char.IsAsciiDigit(c))
            {
                return false;
            }
        }
        return true;
    }
}
