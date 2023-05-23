using Demo.DAL.Entites;
using Demo.NL.Helper;
using Demo.NL.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.NL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signManger;
        public AccountController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signinManger)
        {
            _userManger = userManger;
            _signManger = signinManger;
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email.Split('@')[0],
                    IsAgree = registerViewModel.isAgree,
                };
                var result = await _userManger.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View(registerViewModel);
            }
            return View(registerViewModel);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManger.FindByEmailAsync(loginViewModel.Email); //to check he exist
                if (user is not null)
                {
                    var password = await _userManger.CheckPasswordAsync(user, loginViewModel.Password);
                    if (password)
                    {
                        var result = await _signManger.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid Password");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Email");
                }
            }
            return View(loginViewModel);
        }
        public async Task<IActionResult> SignOut()
        {
            await _signManger.SignOutAsync();
            return RedirectToAction("SignIn");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();

        }

        [HttpPost]

        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel ForgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManger.FindByEmailAsync(ForgetPasswordViewModel.Email);

                if (user is not null)
                {
                    var token = await _userManger.GeneratePasswordResetTokenAsync(user);
                    var restPasswordLink = Url.Action("ResetPassword", "Account", new { Email = ForgetPasswordViewModel.Email, Token = token }, Request.Scheme);

                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = restPasswordLink,
                        To = ForgetPasswordViewModel.Email
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction("ResetPassword");
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
            }
            return View(ForgetPasswordViewModel);
        }
        public IActionResult ResetPassword(string email,string token)
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(RestPasswordViewModel restmodelview)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManger.FindByEmailAsync(restmodelview.Email);

                if (user is not null)
                {
                    var result = await _userManger.ResetPasswordAsync(user, restmodelview.Token, restmodelview.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SigIn");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(restmodelview);
        }
    }
}
