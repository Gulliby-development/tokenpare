namespace Tokenpare.Tests;

public class ValuesRows
{
    [Theory]
    [InlineData(new[] { "share", "--unit", "Permille" }, "", 2, "", "tokenpare: bad value for --unit\n")]
    [InlineData(new[] { "share", "--unit", "" }, "", 2, "", "tokenpare: bad value for --unit\n")]
    [InlineData(new[] { "share", "--format", "table" }, "", 2, "", "tokenpare: bad value for --format\n")]
    [InlineData(new[] { "topk", "--k", "0" }, "", 2, "", "tokenpare: bad value for --k\n")]
    [InlineData(new[] { "topk", "--k", "33" }, "", 2, "", "tokenpare: bad value for --k\n")]
    [InlineData(new[] { "topk", "--k", "05" }, "", 2, "", "tokenpare: bad value for --k\n")]
    [InlineData(new[] { "topk", "--k", "-1" }, "", 2, "", "tokenpare: bad value for --k\n")]
    [InlineData(new[] { "topk", "--k", "2.5" }, "", 2, "", "tokenpare: bad value for --k\n")]
    [InlineData(new[] { "topp", "--p", "0" }, "", 2, "", "tokenpare: bad value for --p\n")]
    [InlineData(new[] { "topp", "--p", "1001" }, "", 2, "", "tokenpare: bad value for --p\n")]
    [InlineData(new[] { "topp", "--p", "01" }, "", 2, "", "tokenpare: bad value for --p\n")]
    [InlineData(new[] { "minp", "--ratio", "1001" }, "", 2, "", "tokenpare: bad value for --ratio\n")]
    [InlineData(new[] { "minp", "--ratio", "-5" }, "", 2, "", "tokenpare: bad value for --ratio\n")]
    [InlineData(new[] { "draw", "--at", "1000000000" }, "", 2, "", "tokenpare: bad value for --at\n")]
    [InlineData(new[] { "draw", "--at", "007" }, "", 2, "", "tokenpare: bad value for --at\n")]
    [InlineData(new[] { "draw", "--at", "" }, "", 2, "", "tokenpare: bad value for --at\n")]
    [InlineData(new[] { "chain", "--steps", "topk" }, "", 2, "", "tokenpare: bad value for --steps\n")]
    [InlineData(new[] { "chain", "--steps", "topk:0" }, "", 2, "", "tokenpare: bad value for --steps\n")]
    [InlineData(new[] { "topp", "--p", "1000" }, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", 0, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", "")]
    [InlineData(new[] { "minp", "--ratio", "0" }, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", 0, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", "")]
    [InlineData(new[] { "topk", "--k", "32" }, "a 1\nb 2\n", 0, "b 2\na 1\n", "")]
    [InlineData(new[] { "chain", "--steps", "topk:4,topp:900,minp:100,topk:1" }, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", 0, "the 500\n", "")]
    public void Rows(string[] args, string stdin, int code, string expectedOut, string expectedErr) => Harness.Check(args, stdin, code, expectedOut, expectedErr);
}
