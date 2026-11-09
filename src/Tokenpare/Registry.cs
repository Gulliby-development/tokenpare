namespace Tokenpare;

/// <summary>A candidate of the list: the token and its weight.</summary>
public sealed record Item(string Token, int Weight);

/// <summary>The options of a run: the format and the unit (defaults plain and permille), and the numbers of the options that were given (null when absent).</summary>
public sealed record Options(string Format, string Unit, int? K, int? P, int? Ratio, string? Steps, int? At);

/// <summary>The reply of a run: the exit status, the whole standard output text and the standard error text.</summary>
public sealed record Reply(int Status, string Out, string Err);

/// <summary>One line of a table: the token, the number written for it and the number that a full bar stands for.</summary>
public sealed record Row(string Token, int Number, int Full);

/// <summary>A command of the table: its word, the options it accepts and the option that must be given (or "").</summary>
public sealed record Command(string Name, string[] Allowed, string Required);

/// <summary>The version, the limit, the usage text, the formats, the units, the options and the command table.</summary>
public static class Registry
{
    public const string Version = "tokenpare 0.1.0";
    public const int MaxLines = 32;

    public const string Usage =
        "usage: tokenpare check\n"
        + "       tokenpare share [--unit UNIT] [--format FORMAT]\n"
        + "       tokenpare topk --k N [--format FORMAT]\n"
        + "       tokenpare topp --p PERMILLE [--format FORMAT]\n"
        + "       tokenpare minp --ratio PERMILLE [--format FORMAT]\n"
        + "       tokenpare chain --steps STEPS [--format FORMAT]\n"
        + "       tokenpare draw --at N\n";

    public static readonly string[] Formats = ["plain", "csv", "bar"];

    public static readonly Dictionary<string, int> Units = new() { ["permille"] = 1000, ["percent"] = 100, ["ppm"] = 1000000 };

    public static readonly string[] OptionNames = ["--format", "--unit", "--k", "--p", "--ratio", "--steps", "--at"];

    /// <summary>One entry per command, in the order of the usage text. The class NAMECommand (NAME with a capital first letter) has the function Run(Options options, List&lt;Item&gt; items).</summary>
    public static readonly Command[] Commands =
    [
        new("check", [], ""),
        new("share", ["--unit", "--format"], ""),
        new("topk", ["--k", "--format"], "--k"),
        new("topp", ["--p", "--format"], "--p"),
        new("minp", ["--ratio", "--format"], "--ratio"),
        new("chain", ["--steps", "--format"], "--steps"),
        new("draw", ["--at"], "--at"),
    ];
}
