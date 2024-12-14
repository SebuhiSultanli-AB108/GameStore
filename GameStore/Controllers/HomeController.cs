using GameStore.DataAccess;
using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public class HomeController(GameStoreDbContext _context, UserManager<User> _userManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                User user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                ViewBag.UserName = user.UserName;
            }
            return View(await _context.Games.Where(x => x.IsDeleted != true).ToListAsync());
        }
    }
}
