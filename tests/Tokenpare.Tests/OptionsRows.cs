namespace Tokenpare.Tests;

public class OptionsRows
{
    [Theory]
    [InlineData(new[] { "share", "extra" }, "", 2, "", "tokenpare: unexpected argument extra\n")]
    [InlineData(new[] { "share", "-" }, "", 2, "", "tokenpare: unexpected argument -\n")]
    [InlineData(new[] { "share", "" }, "", 2, "", "tokenpare: unexpected argument \n")]
    [InlineData(new[] { "share", "--bogus" }, "", 2, "", "tokenpare: unknown option --bogus\n")]
    [InlineData(new[] { "check", "--format", "plain" }, "", 2, "", "tokenpare: option --format is not valid for check\n")]
    [InlineData(new[] { "share", "--k", "3" }, "", 2, "", "tokenpare: option --k is not valid for share\n")]
    [InlineData(new[] { "share", "--unit", "ppm", "--unit", "ppm" }, "", 2, "", "tokenpare: given twice: --unit\n")]
    [InlineData(new[] { "share", "--unit" }, "", 2, "", "tokenpare: missing value for --unit\n")]
    [InlineData(new[] { "share", "-x", "--unit" }, "", 2, "", "tokenpare: unknown option -x\n")]
    [InlineData(new[] { "check", "--unit" }, "", 2, "", "tokenpare: option --unit is not valid for check\n")]
    [InlineData(new[] { "share", "--unit", "x", "--bogus" }, "", 2, "", "tokenpare: bad value for --unit\n")]
    [InlineData(new[] { "topk" }, "", 2, "", "tokenpare: missing --k\n")]
    [InlineData(new[] { "topp" }, "", 2, "", "tokenpare: missing --p\n")]
    [InlineData(new[] { "minp" }, "", 2, "", "tokenpare: missing --ratio\n")]
    [InlineData(new[] { "chain" }, "", 2, "", "tokenpare: missing --steps\n")]
    [InlineData(new[] { "draw" }, "", 2, "", "tokenpare: missing --at\n")]
    [InlineData(new[] { "draw", "--format", "csv" }, "", 2, "", "tokenpare: option --format is not valid for draw\n")]
    [InlineData(new[] { "topk", "--unit", "ppm", "--k", "2" }, "", 2, "", "tokenpare: option --unit is not valid for topk\n")]
    [InlineData(new[] { "topk", "--format", "csv", "--k", "1" }, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", 0, "the,500\n", "")]
    [InlineData(new[] { "minp", "--format", "bar", "--ratio", "500" }, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", 0, "the 500 ####################\ncat 300 ############\n", "")]
    [InlineData(new[] { "draw", "--at", "0" }, "a 1\n", 0, "a\n", "")]
    [InlineData(new[] { "chain", "--format", "csv", "--steps", "topk:1" }, "the 500\ncat 300\ndog 150\nsat 40\nmat 10\n", 0, "the,500\n", "")]
    public void Rows(string[] args, string stdin, int code, string expectedOut, string expectedErr) => Harness.Check(args, stdin, code, expectedOut, expectedErr);
}
