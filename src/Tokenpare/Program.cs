using System.Text;
using Tokenpare;

var utf8 = new UTF8Encoding(false);
var stdin = Cli.ReadsStdin(args) ? new StreamReader(Console.OpenStandardInput(), utf8).ReadToEnd() : "";
var reply = Cli.Run(args, stdin);
Write(Console.OpenStandardOutput(), reply.Out);
Write(Console.OpenStandardError(), reply.Err);
return reply.Status;

void Write(Stream stream, string text)
{
    var bytes = utf8.GetBytes(text);
    stream.Write(bytes, 0, bytes.Length);
    stream.Flush();
}
