using System;

namespace GiveCampLondon.Website.Helpers
{
    public class WaitListHelper: IWaitListHelper
    {
        private readonly IConfigManager _configManager;

        public WaitListHelper(IConfigManager configManager)
        {
            _configManager = configManager;
        }

        public bool SetWaitListStatus()
        {
            if (Convert.ToBoolean(_configManager.GetAppSettingsValue("WaitlistEnabled")))
            {
                return true;
            }

            return false;
        }
    }
}