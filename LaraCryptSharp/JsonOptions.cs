using System.Text.Json;

namespace LaraCryptSharp;

internal abstract class JsonOptions
{
    public static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}