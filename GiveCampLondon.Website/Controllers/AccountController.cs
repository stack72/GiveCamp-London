using System;
using System.Globalization;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using GiveCampLondon.Website.Models;
using GiveCampLondon.Website.Models.ServiceFacade;
using GiveCampLondon.Website.Utils;

namespace GiveCampLondon.Website.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {

        private readonly IFormsAuthentication _formsAuthentication;
        private readonly IServiceProxy _serviceProxy;
        private readonly IUserUtility _userUtility;
        private readonly IConfigurationManager _configManager;

        public AccountController(IFormsAuthentication formsAuth, IServiceProxy serviceProxy, IUserUtility userUtility, IConfigurationManager configurationManager)
        {
            _formsAuthentication = formsAuth;
            _serviceProxy = serviceProxy;
            _userUtility = userUtility;
            _configManager = configurationManager;
        }

        //GET: /LogOn
        public ActionResult LogOn()
        {
            return View();
        }

        //POST: /LogOn
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel user, string returnUrl)
        {
            if (!_serviceProxy.ValidateUser(user.UserName, EncryptPassword(user.Password)))
            {
                return View();
            }

            ViewBag.AuthenticatedUser = _serviceProxy.GetUserByUserName(user.UserName);
            
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //GET: /LogOff
        public ActionResult LogOff()
        {
            _formsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //GET: /Register
        public ActionResult Register()
        {
            //ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        //POST: /Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel user, string confirmPassword)
        {
            if (ValidateRegistration(user.UserName, user.EmailAddress, user.Password, confirmPassword))
            {
                // Attempt to register the user
                var registeredUser = _serviceProxy.CreateNewMember(user);

                if (registeredUser != null)
                {
                    ViewBag.AuthenticatedUser = registeredUser;
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }



        //[Authorize]
        //public ActionResult ChangePassword()
        //{

        //    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

        //    return View();
        //}

        //[Authorize]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        //{

        //    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

        //    if (!ValidateChangePassword(currentPassword, newPassword, confirmPassword))
        //    {
        //        return View();
        //    }

        //    try
        //    {
        //        if (MembershipService.ChangePassword(User.Identity.Name, currentPassword, newPassword))
        //        {
        //            return RedirectToAction("ChangePasswordSuccess");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
        //            return View();
        //        }
        //    }
        //    catch
        //    {
        //        ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
        //        return View();
        //    }
        //}

        public ActionResult ChangePasswordSuccess()
        {

            return View();
        }

        private string EncryptPassword(string password)
        {
            var encryptedPassword = _userUtility.EncryptPassword(password.Trim(),
                                                                 _configManager.GetConfigurationAppSettingValue("salt"),
                                                                 _configManager.GetConfigurationAppSettingValue("encryptionPassword"),
                                                                 _configManager.GetConfigurationAppSettingValue("initialisationVector"));

            return encryptedPassword;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }

            var minPasswordLength =
                Convert.ToInt32(_configManager.GetConfigurationAppSettingValue("MinimumPasswordLength"));
            if (password == null || password.Length < minPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         minPasswordLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }
            return ModelState.IsValid;
        }

        #region Validation Methods

        //private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        //{
        //    if (String.IsNullOrEmpty(currentPassword))
        //    {
        //        ModelState.AddModelError("currentPassword", "You must specify a current password.");
        //    }
        //    if (newPassword == null || newPassword.Length < MembershipService.MinPasswordLength)
        //    {
        //        ModelState.AddModelError("newPassword",
        //            String.Format(CultureInfo.CurrentCulture,
        //                 "You must specify a new password of {0} or more characters.",
        //                 MembershipService.MinPasswordLength));
        //    }

        //    if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
        //    {
        //        ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
        //    }

        //    return ModelState.IsValid;
        //}

        #endregion
    }
}
