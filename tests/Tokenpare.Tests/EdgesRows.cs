namespace Tokenpare.Tests;

public class EdgesRows
{
    [Theory]
    [InlineData(new[] { "check" }, "a 999999\nb 999999\n", 0, "ok, 2 candidates, total 1999998\n", "")]
    [InlineData(new[] { "share" }, "a 999999\nb 0\n", 0, "a 1000\nb 0\n", "")]
    [InlineData(new[] { "topk", "--k", "1" }, "a 999999\nb 999999\n", 0, "a 999999\n", "")]
    [InlineData(new[] { "share", "--unit", "ppm" }, "a 1\nb 999999\n", 0, "a 1\nb 999999\n", "")]
    [InlineData(new[] { "share" }, "a 1\nb 0\nc 0\n", 0, "a 1000\nb 0\nc 0\n", "")]
    [InlineData(new[] { "minp", "--ratio", "1000" }, "a 5\nb 5\nc 4\n", 0, "a 5\nb 5\n", "")]
    [InlineData(new[] { "check" }, "a 0\n# b 1\nc 1\n", 0, "ok, 2 candidates, total 1\n", "")]
    [InlineData(new[] { "draw", "--at", "999998" }, "a 999999\n", 0, "a\n", "")]
    [InlineData(new[] { "chain", "--steps", "topp:1" }, "a 0\nb 1\n", 0, "b 1\n", "")]
    public void Rows(string[] args, string stdin, int code, string expectedOut, string expectedErr) => Harness.Check(args, stdin, code, expectedOut, expectedErr);
}
