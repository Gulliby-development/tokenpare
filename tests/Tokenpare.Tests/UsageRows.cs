namespace Tokenpare.Tests;

public class UsageRows
{
    [Theory]
    [InlineData(new[] { "help" }, "", 0, "usage: tokenpare check\n       tokenpare share [--unit UNIT] [--format FORMAT]\n       tokenpare topk --k N [--format FORMAT]\n       tokenpare topp --p PERMILLE [--format FORMAT]\n       tokenpare minp --ratio PERMILLE [--format FORMAT]\n       tokenpare chain --steps STEPS [--format FORMAT]\n       tokenpare draw --at N\n", "")]
    [InlineData(new[] { "-h" }, "", 0, "usage: tokenpare check\n       tokenpare share [--unit UNIT] [--format FORMAT]\n       tokenpare topk --k N [--format FORMAT]\n       tokenpare topp --p PERMILLE [--format FORMAT]\n       tokenpare minp --ratio PERMILLE [--format FORMAT]\n       tokenpare chain --steps STEPS [--format FORMAT]\n       tokenpare draw --at N\n", "")]
    [InlineData(new[] { "--help", "--bogus" }, "", 0, "usage: tokenpare check\n       tokenpare share [--unit UNIT] [--format FORMAT]\n       tokenpare topk --k N [--format FORMAT]\n       tokenpare topp --p PERMILLE [--format FORMAT]\n       tokenpare minp --ratio PERMILLE [--format FORMAT]\n       tokenpare chain --steps STEPS [--format FORMAT]\n       tokenpare draw --at N\n", "")]
    [InlineData(new string[] { }, "", 2, "", "usage: tokenpare check\n       tokenpare share [--unit UNIT] [--format FORMAT]\n       tokenpare topk --k N [--format FORMAT]\n       tokenpare topp --p PERMILLE [--format FORMAT]\n       tokenpare minp --ratio PERMILLE [--format FORMAT]\n       tokenpare chain --steps STEPS [--format FORMAT]\n       tokenpare draw --at N\n")]
    [InlineData(new[] { "--version" }, "", 0, "tokenpare 0.1.0\n", "")]
    [InlineData(new[] { "--version", "x" }, "", 0, "tokenpare 0.1.0\n", "")]
    [InlineData(new[] { "frob" }, "", 2, "", "tokenpare: unknown command frob\n")]
    [InlineData(new[] { "" }, "", 2, "", "tokenpare: unknown command \n")]
    [InlineData(new[] { "Check" }, "", 2, "", "tokenpare: unknown command Check\n")]
    [InlineData(new[] { "-H" }, "", 2, "", "tokenpare: unknown command -H\n")]
    public void Rows(string[] args, string stdin, int code, string expectedOut, string expectedErr) => Harness.Check(args, stdin, code, expectedOut, expectedErr);
}
