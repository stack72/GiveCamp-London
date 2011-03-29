using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiveCampStarterKit.Repositories;

namespace GiveCampStarterKit.Website.Controllers
{
    public class BaseController: Controller
    {
        private ISettingRepository _settingRepository;

        public BaseController(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
            ViewBag.SiteSetting = _settingRepository.GetSetting();
        }


    }
}
