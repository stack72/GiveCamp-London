using System.Web.Mvc;
using GiveCampLondon.Repositories;

namespace GiveCampLondon.Website.Controllers
{
    public class BaseController : Controller
    {
        private ISettingRepository _settingRepository;

        public BaseController(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
            ViewBag.SiteSetting = _settingRepository.GetSetting();
        }


    }
}
