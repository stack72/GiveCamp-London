using System.Linq;

namespace GiveCampLondon.Repositories
{
    public interface ISettingRepository
    {
        Setting GetSetting();
        void SaveSetting(Setting setting);
    }
    public class SettingRepository : ISettingRepository
    {
        private SiteDataContext _dataContext;

        public SettingRepository(SiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Setting GetSetting()
        {
            return _dataContext.Settings.FirstOrDefault();
        }

        public void SaveSetting(Setting setting)
        {
            if(GetSetting() == null && setting.Id == 0)
                _dataContext.Settings.Add(setting); 
            
            _dataContext.SaveChanges();
        }
    }
}
