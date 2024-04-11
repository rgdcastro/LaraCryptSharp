namespace LaraCryptSharp;

internal class Payload
{
    public required string Iv { get; set; }
    public required string Value { get; set; }
    public required string Mac { get; set; }
}