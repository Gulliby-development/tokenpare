namespace Tokenpare;

/// <summary>A failure that ends the command: status 1 for the list and for a command, 2 for the command line. Cli.Run turns it into `tokenpare: MESSAGE` on standard error.</summary>
public sealed class Failure(int status, string message) : Exception(message)
{
    public int Status { get; } = status;
}
