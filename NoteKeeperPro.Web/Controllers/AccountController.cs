using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteKeeperPro.Application.Common.Services.EmailSettings;
using NoteKeeperPro.Domain.Entities.ApplicationUsers;
using NoteKeeperPro.Infrastructure.Identity;
using NoteKeeperPro.Web.ViewModels.Identity;
using NuGet.Common;

namespace NoteKeeperPro.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Services
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSettings _emailSettings;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSettings emailSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSettings = emailSettings;
        }
        #endregion

        #region Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerViewModel.Email.Split('@')[0],
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FName,
                    LastName = registerViewModel.LName,
                    IsAgree = registerViewModel.IsAgree,
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }

                // If creation fails, add the error descriptions to the model state
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerViewModel);
        }
        #endregion

        #region Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                        if (result.Succeeded)
                        {
                            // Redirect the user to the "Note/Index" page after successful login
                            return RedirectToAction("Index", "Note");
                        }
                    }
                    else
                    {
                        // If password is incorrect, add an error to the model state
                        ModelState.AddModelError(string.Empty, "Password is incorrect.");
                    }
                }
                else
                {
                    // If the email is not found, add an error to the model state
                    ModelState.AddModelError(string.Empty, "Email not found.");
                }
            }

            return View(loginViewModel);
        }
        #endregion

        #region SignOut
        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            // Sign out the user
            await _signInManager.SignOutAsync();
            // Redirect to the Login page
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region SendResetPasswordUrl
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SendResetPasswordUrl()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new { email = forgetPasswordViewModel.Email, token }, Request.Scheme);

                    var email = new Email
                    {
                        To = forgetPasswordViewModel.Email,
                        subject = "Reset your password",
                        body = url
                    };

                    // Send the reset password email
                    _emailSettings.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }

                // If the email is not found, add an error to the model state
                ModelState.AddModelError(string.Empty, "Email not found.");
            }

            return View(forgetPasswordViewModel);
        }
        #endregion

        #region CheckYourInbox
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        #endregion

        #region ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email, string token)
        {
            var model = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token, resetPasswordViewModel.Password);
                    if (result.Succeeded)
                    {
                        // If password reset is successful, redirect to the login page
                        return RedirectToAction(nameof(Login));
                    }
                }
                // If there's an issue, add an error to the model state
                ModelState.AddModelError(string.Empty, "Invalid operation, please try again.");
            }

            return View(resetPasswordViewModel);
        }
        #endregion
    }
}