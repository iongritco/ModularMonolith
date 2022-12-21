namespace ToDoApp.Common.Interfaces
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T value);

        T Deserialize<T>(byte[] value);

        object TranslateType(object value, Type type);

        T TranslateType<T>(object value);
    }
}
