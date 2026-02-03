using System.Text.Json;

namespace CSharpSyntheticRepo.Infrastructure;

public static class JsonUtil
{
    // Overload set #2
    public static string Serialize(object value)
        => JsonSerializer.Serialize(value);
        

    // Overload set #2 (same param count as Serialize(object) but different param types)
    public static string Serialize(string value)
        => JsonSerializer.Serialize(value);

    // Overload set #2
    public static string Serialize<T>(T value, bool indented)
        => JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = indented });

    // Overload set #2 (Deserialize has framework overloads, but we keep this simple)
    public static T? Deserialize<T>(string json)
        => JsonSerializer.Deserialize<T>(json);
}


