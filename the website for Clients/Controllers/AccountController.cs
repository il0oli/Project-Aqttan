using ClientAqttan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientAqttan.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
       

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager   )
        {
            _userManager = userManager;
            _signInManager = signInManager;
           
           
        }


        public IActionResult Index()
        {

            return View();



        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }





        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public async  Task<IActionResult> Edit()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(); // جلب المستخدم الافتراضي الوحيد
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var model = new EditAccountViewModel
            {
                Username = user.UserName
            };

            return View();
        }

        //public async Task<IdentityResult> ChangPasswordAsync(EditAccountViewModel model)
        //{
        //  var userId = _userService.GetUser
        //}

        [HttpPost]
        public async Task<IActionResult> Edit(EditAccountViewModel model)
        {





            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (user.UserName != model.Username)
            {
                user.UserName = model.Username;
                var usernameResult = await _userManager.UpdateAsync(user);
                if (!usernameResult.Succeeded)
                {
                    foreach (var error in usernameResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                var passwordCheckResult = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
                if (!passwordCheckResult)
                {
                    ModelState.AddModelError(string.Empty, "CurrentPassword is Wrong.");
                    return View(model);
                }
                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!passwordChangeResult.Succeeded)
                {
                    foreach (var error in passwordChangeResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }
            

            await _signInManager.RefreshSignInAsync(user); // تحديث جلسة المستخدم
            return RedirectToAction("Account", "Login");
        }
    }


}

