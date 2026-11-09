namespace Tokenpare;

/// <summary>The command line: help, version, the option rules and the call of the command.</summary>
public static class Cli
{
    public static bool ReadsStdin(string[] args)
    {
        if (args.Length == 0 || FindCommand(args[0]) is not { } command)
        {
            return false;
        }
        try
        {
            Parse(command, args);
            return true;
        }
        catch (Failure)
        {
            return false;
        }
    }

    public static Reply Run(string[] args, string input)
    {
        if (args.Length == 0)
        {
            return new Reply(2, "", Registry.Usage);
        }
        if (args[0] is "help" or "-h" or "--help")
        {
            return new Reply(0, Registry.Usage, "");
        }
        if (args[0] == "--version")
        {
            return new Reply(0, Registry.Version + "\n", "");
        }
        try
        {
            var command = FindCommand(args[0]) ?? throw new Failure(2, "unknown command " + args[0]);
            var options = Parse(command, args);
            var items = Input.Read(input);
            return Dispatch(command.Name, options, items);
        }
        catch (Failure f)
        {
            return new Reply(f.Status, "", "tokenpare: " + f.Message + "\n");
        }
    }

    private static Reply Dispatch(string name, Options options, List<Item> items) => name switch
    {
        "check" => CheckCommand.Run(options, items),
        "share" => ShareCommand.Run(options, items),
        "topk" => TopkCommand.Run(options, items),
        "topp" => ToppCommand.Run(options, items),
        "minp" => MinpCommand.Run(options, items),
        "chain" => ChainCommand.Run(options, items),
        _ => DrawCommand.Run(options, items),
    };

    private static Command? FindCommand(string word)
    {
        foreach (var command in Registry.Commands)
        {
            if (command.Name == word)
            {
                return command;
            }
        }
        return null;
    }

    private static Options Parse(Command command, string[] args)
    {
        var format = "plain";
        var unit = "permille";
        int? k = null;
        int? p = null;
        int? ratio = null;
        int? at = null;
        string? steps = null;
        var seen = new HashSet<string>(StringComparer.Ordinal);
        for (var i = 1; i < args.Length; i++)
        {
            var word = args[i];
            if (word.Length == 0 || word[0] != '-' || word == "-")
            {
                throw new Failure(2, "unexpected argument " + word);
            }
            if (Array.IndexOf(Registry.OptionNames, word) < 0)
            {
                throw new Failure(2, "unknown option " + word);
            }
            if (Array.IndexOf(command.Allowed, word) < 0)
            {
                throw new Failure(2, "option " + word + " is not valid for " + command.Name);
            }
            if (!seen.Add(word))
            {
                throw new Failure(2, "given twice: " + word);
            }
            if (i + 1 >= args.Length)
            {
                throw new Failure(2, "missing value for " + word);
            }
            var value = args[++i];
            var bad = new Failure(2, "bad value for " + word);
            switch (word)
            {
                case "--format":
                    format = Array.IndexOf(Registry.Formats, value) >= 0 ? value : throw bad;
                    break;
                case "--unit":
                    unit = Registry.Units.ContainsKey(value) ? value : throw bad;
                    break;
                case "--k":
                    k = Ranged(value, 1, 32) ?? throw bad;
                    break;
                case "--p":
                    p = Ranged(value, 1, 1000) ?? throw bad;
                    break;
                case "--ratio":
                    ratio = Ranged(value, 0, 1000) ?? throw bad;
                    break;
                case "--at":
                    at = Ranged(value, 0, 999999999) ?? throw bad;
                    break;
                default:
                    steps = StepsValid(value) ? value : throw bad;
                    break;
            }
        }
        if (command.Required.Length > 0 && !seen.Contains(command.Required))
        {
            throw new Failure(2, "missing " + command.Required);
        }
        return new Options(format, unit, k, p, ratio, steps, at);
    }

    private static int? Ranged(string word, int min, int max)
    {
        var number = Number(word, max);
        return number >= min ? number : null;
    }

    private static int Number(string word, int max)
    {
        if (word.Length == 0 || word.Length > max.ToString(System.Globalization.CultureInfo.InvariantCulture).Length)
        {
            return -1;
        }
        if (word.Length > 1 && word[0] == '0')
        {
            return -1;
        }
        long value = 0;
        foreach (var c in word)
        {
            if (!char.IsAsciiDigit(c))
            {
                return -1;
            }
            value = (value * 10) + (c - '0');
        }
        return value <= max ? (int)value : -1;
    }

    private static bool StepsValid(string value)
    {
        var parts = value.Split(',');
        if (parts.Length < 1 || parts.Length > 4)
        {
            return false;
        }
        foreach (var part in parts)
        {
            var ok = part.StartsWith("topk:", StringComparison.Ordinal) ? Ranged(part[5..], 1, 32) != null
                : part.StartsWith("topp:", StringComparison.Ordinal) ? Ranged(part[5..], 1, 1000) != null
                : part.StartsWith("minp:", StringComparison.Ordinal) && Ranged(part[5..], 0, 1000) != null;
            if (!ok)
            {
                return false;
            }
        }
        return true;
    }
}
