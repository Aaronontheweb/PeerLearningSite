using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using PeerLearn.Data;
using PeerLearn.Data.Entities;
using PeerLearn.Web.Helpers;
using PeerLearn.Web.Models;
using PeerLearn.Web.Service;

namespace PeerLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository _repository;
        private readonly PasswordHelper _passwordCrypto;
        private readonly IFormsAuthenticationService _authentication;

        public AccountController(IRepository repository, PasswordHelper crypto, IFormsAuthenticationService authService)
        {
            _repository = repository;
            _passwordCrypto = crypto;
            _authentication = authService;
        }

        #region Action methods

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(NewUser newUser)
        {
            if(ModelState.IsValid)
            {
                newUser.EmailAddress = Server.HtmlEncode(newUser.EmailAddress);
                newUser.UserName = Server.HtmlEncode(newUser.UserName);

                var user = new User
                               {
                                   UserName = newUser.UserName,
                                   Email = newUser.EmailAddress,
                                   Password = _passwordCrypto.EncodePassword(newUser.Password.Trim()),
                                   DateJoined = DateTime.Now,
                                   IsEnabled = true,
                                   IsPublic = true
                               };

                var result = _repository.CreateUser(user);

                if(result)
                {
                    _authentication.SignIn(newUser.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "We were unable to register your account.");
                }

                
            }

            return View(newUser);
        }

        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(ExistingUser user)
        {
            if(ModelState.IsValid)
            {
                var dbUser = _repository.GetUserByName(user.UserName);
                if(dbUser == null)
                {
                    ModelState.AddModelError("", string.Format("user {0} does not exist. Please try again"));
                    return View(user);
                }

                //If the password is invalid
                if(!_passwordCrypto.CheckPassword(user.Password.Trim(), dbUser.Password))
                {
                    ModelState.AddModelError("", string.Format("incorrect password."));
                    return View(user);
                }

                //Login successful!
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        public ActionResult LogOff()
        {
            _authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Remote validation helpers

        public JsonResult UserNameExists(string username)
        {
            var user = _repository.GetUserByName(username.Trim());
            return user == null ? Json(true, JsonRequestBehavior.AllowGet) : Json(string.Format("{0} is not available.", username), JsonRequestBehavior.AllowGet);
        }


        /*NOTE: Small gotcha here - when I had this parameter named "email" instead of "emailaddress" the jQuery validation library
          passing in a null value - this is because the parameter name here needs to match the name of the Model property being validated.
          Tricky! */
        public JsonResult EmailExists(string emailaddress)
        {
            var user = _repository.GetUserByEmail(emailaddress.Trim());
            return user == null ? Json(true, JsonRequestBehavior.AllowGet) : Json(string.Format("an account for address {0} already exists.", emailaddress), JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
