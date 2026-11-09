namespace Tokenpare.Tests;

public class TokensRows
{
    [Theory]
    [InlineData(new[] { "check" }, "a 1\n", 0, "ok, 1 candidate, total 1\n", "")]
    [InlineData(new[] { "check" }, "abcdefghijkl 1\n", 0, "ok, 1 candidate, total 1\n", "")]
    [InlineData(new[] { "check" }, "abcdefghijklm 1\n", 1, "", "tokenpare: line 1: bad token abcdefghijklm\n")]
    [InlineData(new[] { "check" }, "A.b_c-D9 1\n", 0, "ok, 1 candidate, total 1\n", "")]
    [InlineData(new[] { "check" }, "a:b 1\n", 1, "", "tokenpare: line 1: bad token a:b\n")]
    [InlineData(new[] { "check" }, "a/b 1\n", 1, "", "tokenpare: line 1: bad token a/b\n")]
    [InlineData(new[] { "check" }, "caf? 1\n", 1, "", "tokenpare: line 1: bad token caf?\n")]
    [InlineData(new[] { "check" }, "a 0\n", 1, "", "tokenpare: total weight is zero\n")]
    [InlineData(new[] { "check" }, "a 999999\n", 0, "ok, 1 candidate, total 999999\n", "")]
    [InlineData(new[] { "check" }, "a 1000000\n", 1, "", "tokenpare: line 1: bad weight 1000000\n")]
    [InlineData(new[] { "check" }, "a 01\n", 1, "", "tokenpare: line 1: bad weight 01\n")]
    [InlineData(new[] { "check" }, "a -1\n", 1, "", "tokenpare: line 1: bad weight -1\n")]
    [InlineData(new[] { "check" }, "a +1\n", 1, "", "tokenpare: line 1: bad weight +1\n")]
    [InlineData(new[] { "check" }, "a 1.5\n", 1, "", "tokenpare: line 1: bad weight 1.5\n")]
    [InlineData(new[] { "check" }, "a 1e3\n", 1, "", "tokenpare: line 1: bad weight 1e3\n")]
    [InlineData(new[] { "check" }, "a 1\na 2\n", 1, "", "tokenpare: line 2: duplicate token a\n")]
    [InlineData(new[] { "check" }, "a 1\nA 2\n", 0, "ok, 2 candidates, total 3\n", "")]
    [InlineData(new[] { "check" }, "a 1\nb 2\na 3\n", 1, "", "tokenpare: line 3: duplicate token a\n")]
    [InlineData(new[] { "check" }, "a x\n", 1, "", "tokenpare: line 1: bad weight x\n")]
    [InlineData(new[] { "check" }, "a 0x10\n", 1, "", "tokenpare: line 1: bad weight 0x10\n")]
    public void Rows(string[] args, string stdin, int code, string expectedOut, string expectedErr) => Harness.Check(args, stdin, code, expectedOut, expectedErr);
}
