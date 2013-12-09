using System;
using DarumaBLL.Common.Abstractions;

namespace DarumaBLL.FirstStartUseCase
{
    public class FirstStartHandler
    {
        private ISettingsStorage _settings;
        private const string KeyString = "IsNotFirstStart";

        public FirstStartHandler(ISettingsStorage settings)
        {
            _settings = settings;
        }

        public void HandleFirstStart(Action action)
        {
            var isNotFirstStart = _settings.IsContains(KeyString);
            if (!isNotFirstStart)
            {
                _settings.Add(KeyString, true);
                action();
            }
        }
    }
}
