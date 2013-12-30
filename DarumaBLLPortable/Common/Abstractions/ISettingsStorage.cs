namespace DarumaBLLPortable.Common.Abstractions
{
    public interface ISettingsStorage
    {
        bool IsContains(string key);
        void Add(string key, object item);
    }
}
