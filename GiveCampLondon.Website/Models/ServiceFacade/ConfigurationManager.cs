namespace GiveCampLondon.Website.Models.ServiceFacade
{
    public interface IConfigurationManager
    {
        string GetConfigurationAppSettingValue(string key);
    }

    public class ConfigurationManager : IConfigurationManager
    {
        public string GetConfigurationAppSettingValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(key);
        }
    }

}