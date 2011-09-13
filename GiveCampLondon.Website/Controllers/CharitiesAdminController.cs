using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Models.Charity;
using MvcMembership;

namespace GiveCampLondon.Website.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CharitiesAdminController : Controller
    {
        private readonly IRolesService _rolesService;
        private readonly IMembershipService _membershipService;
        private readonly ICharityRepository _charityRepository;

        public CharitiesAdminController(IRolesService rolesService, IMembershipService membershipService,
            ICharityRepository charityRepository)
        {
            _rolesService = rolesService;
            _membershipService = membershipService;
            _charityRepository = charityRepository;
        }

        public ActionResult Charities()
        {
            IEnumerable<CharitySummaryModel> charitySummeries = GetCharitySummeries();
            return View(charitySummeries);
        }

        public ActionResult Approve(int id)
        {
            Charity charity = ApproveCharity(id, true);
            return View("CharityDetails", charity);
        }

        public ActionResult Disapprove(int id)
        {
            Charity charity = ApproveCharity(id, false);
            return View("CharityDetails", charity);
        }

        public ActionResult CharityDetails(int id)
        {
            Charity charity = _charityRepository.Get(id);
            return View(charity);
        }

        public ActionResult AddCharity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCharity(Charity charity)
        {
            if (ModelState.IsValid)
            {
                _charityRepository.Save(charity);
                RedirectToAction("Charities");
            }

            return View(charity);
        }

        private Charity ApproveCharity(int id, bool approve)
        {
            Charity charity = _charityRepository.Get(id);
            charity.Approved = approve;
            _charityRepository.Save(charity);
            return charity;
        }

        private IEnumerable<CharitySummaryModel> GetCharitySummeries()
        {
            return _rolesService.FindUserNamesByRole("Charity")
                .Select(username => _membershipService.GetUserByName(username))
                .Select(membership => _charityRepository.Get((Guid)membership.ProviderUserKey))
                .Select(charity => new CharitySummaryModel { Email = charity.Email, Name = charity.CharityName, Id = charity.Id, Approved = charity.Approved });
        }


    }
}
