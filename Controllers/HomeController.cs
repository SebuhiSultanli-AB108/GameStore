using GameStore.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public class HomeController(GameStoreDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Games.ToListAsync());
        }
        public IActionResult ProductDetail()
        {
            return View();
        }
    }
}
