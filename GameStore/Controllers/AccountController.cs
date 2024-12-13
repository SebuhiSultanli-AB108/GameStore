using GameStore.Models;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager) : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Register() { return View(); }
        public IActionResult Login() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid) { return View(); }
            User? user = await _userManager.FindByEmailAsync(vm.EmailAdress);
            if (user is null)
            {
                ModelState.AddModelError("", "Email or password is wrong!");
                return View("");
            }
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    ModelState.AddModelError("", "Wait until" + user.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                if (result.IsNotAllowed)
                    ModelState.AddModelError("", "Confirm your account!");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            User user = new()
            {
                Fullname = vm.Firstname + " " + vm.Lastname,
                UserName = vm.Username,
                Email = vm.EmailAdress,
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(Login));
        }
    }
}
