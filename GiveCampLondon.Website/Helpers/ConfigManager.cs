using System.Configuration;

namespace GiveCampLondon.Website.Helpers
{
    public class ConfigManager: IConfigManager
    {
        public string GetAppSettingsValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }

    public interface IConfigManager
    {
        string GetAppSettingsValue(string key);
    }
}