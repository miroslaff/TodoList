using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Security;
using WebMatrix.WebData;
using TodoList.Web.Filters;
using TodoList.Web.Models;
using System.Web.Http;
using System.Net.Http;

namespace TodoList.Web.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : ApiController
    {
        //
        // POST: api/Account/Login

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(model.Username, model.Password, model.RememberMe))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);

                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                                          new { errors = GetErrorsFromModelState() });
        }

        //
        // GET: /Account/Logout

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public HttpResponseMessage Logout()
        {
            WebSecurity.Logout();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //
        // POST: /Account/Register
        [HttpPut]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public HttpResponseMessage Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.Username, model.Password);
                    WebSecurity.Login(model.Username, model.Password);

                    FormsAuthentication.SetAuthCookie(model.Username, createPersistentCookie: false);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                                          new { errors = GetErrorsFromModelState() });
        }
        /*
                //
                // GET: /Account/Manage

                public ActionResult Manage(ManageMessageId? message)
                {
                    ViewBag.StatusMessage =
                        message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                        : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                        : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                        : "";

                    ViewBag.ReturnUrl = Url.Action("Manage");
                    return View();
                }

                //
                // POST: /Account/Manage

                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Manage(LocalPasswordModel model)
                {
                    ViewBag.ReturnUrl = Url.Action("Manage");

                    if (ModelState.IsValid)
                    {
                        // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                        bool changePasswordSucceeded;
                        try
                        {
                            changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                        }
                        catch (Exception)
                        {
                            changePasswordSucceeded = false;
                        }

                        if (changePasswordSucceeded)
                        {
                            return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                        }
                        else
                        {
                            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                        }
                    }

                    // If we got this far, something failed, redisplay form
                    return View(model);
                }

                #region Helpers
                private ActionResult RedirectToLocal(string returnUrl)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                */
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        //#endregion
    }
}