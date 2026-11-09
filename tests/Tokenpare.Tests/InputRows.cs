namespace Tokenpare.Tests;

public class InputRows
{
    [Theory]
    [InlineData(new[] { "check" }, "", 1, "", "tokenpare: no candidates\n")]
    [InlineData(new[] { "check" }, "\n", 1, "", "tokenpare: no candidates\n")]
    [InlineData(new[] { "check" }, "# only\n\n   \n", 1, "", "tokenpare: no candidates\n")]
    [InlineData(new[] { "check" }, "a 1", 0, "ok, 1 candidate, total 1\n", "")]
    [InlineData(new[] { "check" }, "a 1\nb 2", 0, "ok, 2 candidates, total 3\n", "")]
    [InlineData(new[] { "check" }, "a 1\r\nb 2\r\n", 0, "ok, 2 candidates, total 3\n", "")]
    [InlineData(new[] { "check" }, "a 1\nb 2 3\n", 1, "", "tokenpare: line 2: expected TOKEN WEIGHT\n")]
    [InlineData(new[] { "check" }, "\n# c\na 1\nb\n", 1, "", "tokenpare: line 4: expected TOKEN WEIGHT\n")]
    [InlineData(new[] { "check" }, "  a 1  \n\tb\t2\t\n", 0, "ok, 2 candidates, total 3\n", "")]
    [InlineData(new[] { "check" }, "a 1\n   # note\nb 2\n#c 3\n", 0, "ok, 2 candidates, total 3\n", "")]
    [InlineData(new[] { "check" }, "a 1\nfrob\nb 2\nx y\n", 1, "", "tokenpare: line 2: expected TOKEN WEIGHT\n")]
    [InlineData(new[] { "check" }, "a 1\nb 2 #c\n", 1, "", "tokenpare: line 2: expected TOKEN WEIGHT\n")]
    [InlineData(new[] { "check" }, "a 1\n\n\nb\n", 1, "", "tokenpare: line 4: expected TOKEN WEIGHT\n")]
    [InlineData(new[] { "check" }, "a 0\nb 0\n", 1, "", "tokenpare: total weight is zero\n")]
    [InlineData(new[] { "check" }, "a 0\nb 1\n", 0, "ok, 2 candidates, total 1\n", "")]
    public void Rows(string[] args, string stdin, int code, string expectedOut, string expectedErr) => Harness.Check(args, stdin, code, expectedOut, expectedErr);
}
