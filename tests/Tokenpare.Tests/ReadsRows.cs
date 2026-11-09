namespace Tokenpare.Tests;

public class ReadsRows
{
    [Theory]
    [InlineData(new[] { "check" }, true)]
    [InlineData(new[] { "share" }, true)]
    [InlineData(new[] { "share", "--unit", "ppm" }, true)]
    [InlineData(new[] { "share", "--format", "bar", "--unit", "percent" }, true)]
    [InlineData(new[] { "topk", "--k", "3" }, true)]
    [InlineData(new[] { "topp", "--p", "900" }, true)]
    [InlineData(new[] { "minp", "--ratio", "0" }, true)]
    [InlineData(new[] { "chain", "--steps", "topk:2,minp:100" }, true)]
    [InlineData(new[] { "draw", "--at", "7" }, true)]
    [InlineData(new[] { "share", "--format", "csv" }, true)]
    [InlineData(new string[] { }, false)]
    [InlineData(new[] { "help" }, false)]
    [InlineData(new[] { "-h" }, false)]
    [InlineData(new[] { "--help" }, false)]
    [InlineData(new[] { "--version" }, false)]
    [InlineData(new[] { "frob" }, false)]
    [InlineData(new[] { "check", "--format", "plain" }, false)]
    [InlineData(new[] { "share", "--unit", "x" }, false)]
    [InlineData(new[] { "topk" }, false)]
    [InlineData(new[] { "topk", "--k", "0" }, false)]
    [InlineData(new[] { "draw", "--at", "x" }, false)]
    [InlineData(new[] { "chain", "--steps", "topk:2," }, false)]
    [InlineData(new[] { "Check" }, false)]
    public void Rows(string[] args, bool expected) => Harness.Reads(args, expected);
}
