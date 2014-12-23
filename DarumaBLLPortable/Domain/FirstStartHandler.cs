using System;
using DarumaBLLPortable.Common.Abstractions;

namespace DarumaBLLPortable.Domain
{
    public class FirstStartHandler
    {
        private ISettingsStorage _settings;
        private const string KeyString = "IsNotFirstStart";

        public FirstStartHandler(ISettingsStorage settings)
        {
            _settings = settings;
        }

        public bool HandleFirstStart(Action action)
        {
            var isFirstStart = !_settings.IsContains(KeyString);
            if (isFirstStart)
            {
                _settings.Add(KeyString, true);
                action();
            }

            return isFirstStart;
        }
    }
}
