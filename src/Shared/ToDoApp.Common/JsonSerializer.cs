using System.Text;
using System.Text.Json;
using ToDoApp.Common.Interfaces;

namespace ToDoApp.Common;

public class JsonSerializer : ISerializer
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public byte[] Serialize<T>(T value)
        => Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(value, SerializerOptions));

    public T Deserialize<T>(byte[] value)
        => System.Text.Json.JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(value), SerializerOptions);

    public object Deserialize(byte[] value, Type type)
        => System.Text.Json.JsonSerializer.Deserialize(Encoding.UTF8.GetString(value), type, SerializerOptions);

    public object TranslateType(object value, Type type)
        => Deserialize(Serialize(value), type);

    public T TranslateType<T>(object value)
        => Deserialize<T>(Serialize(value));
}
