namespace Tokenpare;

/// <summary>Renders tables and counts.</summary>
public static class Report
{
    public static string RenderRows(IEnumerable<Row> rows, string format)
    {
        var sb = new System.Text.StringBuilder();
        foreach (var row in rows)
        {
            var number = row.Number.ToString(System.Globalization.CultureInfo.InvariantCulture);
            sb.Append(row.Token);
            sb.Append(format == "csv" ? ',' : ' ');
            sb.Append(number);
            if (format == "bar")
            {
                sb.Append(' ');
                sb.Append(Bars.Bar(row.Number, row.Full));
            }
            sb.Append('\n');
        }
        return sb.ToString();
    }

    public static string Plural(int count, string noun)
    {
        var n = count.ToString(System.Globalization.CultureInfo.InvariantCulture);
        return count == 1 ? n + " " + noun : n + " " + noun + "s";
    }
}
