namespace Tokenpare.Tests;

public class CliRows
{
    [Theory]
    [InlineData(new[] { "check" }, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", 0, "ok, 5 candidates, total 1000\n", "")]
    [InlineData(new[] { "share" }, "a 1\nb 3\n", 0, "a 250\nb 750\n", "")]
    [InlineData(new[] { "share", "--unit", "percent", "--format", "csv" }, "a 1\nb 1\n", 0, "a,50\nb,50\n", "")]
    [InlineData(new[] { "topk", "--k", "2" }, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", 0, "the 500\ncat 300\n", "")]
    [InlineData(new[] { "minp", "--ratio", "100" }, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", 0, "the 500\ncat 300\ndog 150\n", "")]
    [InlineData(new[] { "share", "--format", "bar" }, "a 1\nb 3\n", 0, "a 250 #####\nb 750 ###############\n", "")]
    [InlineData(new[] { "chain", "--steps", "topk:3,minp:200" }, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", 0, "the 500\ncat 300\ndog 150\n", "")]
    [InlineData(new[] { "draw", "--at", "520" }, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", 0, "cat\n", "")]
    public void Rows(string[] args, string stdin, int code, string expectedOut, string expectedErr) => Harness.Check(args, stdin, code, expectedOut, expectedErr);
}
