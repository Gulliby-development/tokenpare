namespace Tokenpare.Tests;

/// <summary>The helpers of the row tests: one call checks one command line.</summary>
public static class Harness
{
    /// <summary>Runs Cli.Run on the arguments with the standard input text and checks the exit status, the standard output text and the standard error text.</summary>
    public static void Check(string[] args, string stdin, int code, string expectedOut, string expectedErr)
    {
        var reply = Cli.Run(args, stdin);
        Assert.Equal(expectedErr, reply.Err);
        Assert.Equal(expectedOut, reply.Out);
        Assert.Equal(code, reply.Status);
    }

    /// <summary>Checks whether Cli.ReadsStdin says that the command line reads standard input.</summary>
    public static void Reads(string[] args, bool expected) => Assert.Equal(expected, Cli.ReadsStdin(args));
}
