using System.IO.IsolatedStorage;
using DarumaBLL.Common.Abstractions;

namespace Daruma.Infrastructure.Storages
{
    public class SettingsStorage : ISettingsStorage
    {
        private IsolatedStorageSettings _settings = IsolatedStorageSettings.ApplicationSettings;

        public bool IsContains(string key)
        {
            return _settings.Contains(key);
        }

        public void Add(string key, object item)
        {
            _settings.Add(key, item);
            _settings.Save();
        }
    }
}
